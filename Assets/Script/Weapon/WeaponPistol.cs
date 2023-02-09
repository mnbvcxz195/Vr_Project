using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public class AmmoEvent : UnityEvent<int, int> { }

[System.Serializable]
public class MagazineEvent : UnityEvent<int> { }

public class WeaponPistol : MonoBehaviour
{
    [HideInInspector]
    public AmmoEvent onAmmoEvent = new AmmoEvent();
    [HideInInspector]
    public MagazineEvent onMagazineEvent = new MagazineEvent();

    [Header("Spawn Points")]
    [SerializeField]
    private Transform casingSpawnPoint;        //ź�� ���� ��ġ
    [SerializeField]
    private Transform bulletSpawnPoint;        //�Ѿ� ���� ��ġ

    [Header("Weapon Setting")]
    [SerializeField]
    private WeaponSetting weaponSetting;       //���� ����

    private float lastAttackTime = 0;          //������ �߻�ð� üũ
    private bool isReload = false;             //������ ������ üũ
    private bool isAttack = false;             //���� ���� üũ��

    [SerializeField] private Transform endPos;  //�Ѿ� ������ ���� ��ġ

    private CasingMemoryPool casingMemoryPool; //ź�� ���� �� Ȱ��/��Ȱ�� ����
    private BulletMemoryPool bulletMemoryPool; //�Ѿ� ���� �� Ȱ��/��Ȱ�� ����
    [SerializeField]private Camera mainCamera;                 //���� �߻�

    /*    //�ܺο��� �ʿ��� ������ �����ϱ� ���� ������ Get Property's
        public WeaponName WeaponName => weaponSetting.weaponName;
        public int CurrentMagazine => weaponSetting.currentMagazine;
        public int MaxMagazine => weaponSetting.mazMagazine;*/
    public float AttackDistance => weaponSetting.attackDistance;

    private void Awake()
    {
        casingMemoryPool = GetComponent<CasingMemoryPool>();
        bulletMemoryPool = GetComponent<BulletMemoryPool>();
        mainCamera = Camera.main;

        //ó�� źâ ���� �ִ�� ����
        weaponSetting.currentMagazine = weaponSetting.mazMagazine;
        //ó�� ź ���� �ִ�� ����
        weaponSetting.currentAmmo = weaponSetting.maxAmmo;
    }

    private void OnEnable()
    {
        //���Ⱑ Ȱ��ȭ�� �� �ش� ������ źâ ������ �����Ѵ�.
        onMagazineEvent.Invoke(weaponSetting.currentMagazine);
        //���Ⱑ Ȱ��ȭ�� �� �ش� ������ ź �� ������ �����Ѵ�.
        onAmmoEvent.Invoke(weaponSetting.currentAmmo, weaponSetting.maxAmmo);

        ResetVariables();
    }

    public void StartWeaponAction(int type = 0)
    {
        //������ ���� ���� ���� �׼� �Ұ�
        if (isReload == true)
            return;

        //���콺 ���� Ŭ��(���� ����)
        if (type == 0)
        {
            //���� ����
            if (weaponSetting.isAutomaticAttack == true)
            {
                isAttack = true;
                StartCoroutine("OnAttackLoop");
            }
            //�ܹ� ����
            else
            {
                OnAttack();
            }
        }
    }

    public void StopWeaponAction(int type = 0)
    {
        //���콺 ���� Ŭ��(���� ����)
        if (type == 0)
        {
            isAttack = false;
            StopCoroutine("OnAttackLoop");
        }
    }

    public void StartReload()
    {
        //���� ������ ���̸� ������ �Ұ���
        if (isReload == true || weaponSetting.currentAmmo == weaponSetting.maxAmmo || weaponSetting.currentMagazine <= 0)
            return;

        //���� �׼� ���߿� 'R'Ű�� ���� �������� �õ��ϸ� ���� �׼� ���� �� ������
        StopWeaponAction();

        StartCoroutine("OnReload");
    }

    private IEnumerator OnAttackLoop()
    {
        while (true)
        {
            OnAttack();
            yield return null;
        }
    }

    public void OnAttack()
    {
        if (Time.time - lastAttackTime > weaponSetting.attackRate)
        {

            //�����ֱⰡ �Ǿ�� ������ �� �ֵ��� �ϱ� ���� ���� �ð� ����
            lastAttackTime = Time.time;

            //ź ���� ������ ���� �Ұ���
            if (weaponSetting.currentAmmo <= 0)
            {
                return;
            }
            //���ݽ� currentAmno 1 ����, ź �� UI ������Ʈ
            weaponSetting.currentAmmo--;
            onAmmoEvent.Invoke(weaponSetting.currentAmmo, weaponSetting.maxAmmo);

            //�Ѿ� ����
            bulletMemoryPool.SpawnBullet(bulletSpawnPoint, endPos);

            //ź�� ����
            casingMemoryPool.SpawnCasing(casingSpawnPoint, transform.right);

            //������ �߻��� ���ϴ� ��ġ ���� (+Impact Effect)
            TwoStepRaycast();
        }
    }

    private IEnumerator OnReload()
    {
        isReload = true;

        while (true)
        {
            if (isReload)
            {
                isReload = false;

                //���� źâ ���� 1 ���ҽ�Ű��, �ٲ� źâ ������ Text UI�� ������Ʈ
                weaponSetting.currentMagazine--;
                onMagazineEvent.Invoke(weaponSetting.currentMagazine);

                //���� ź ���� �ִ�� �����ϰ�, �ٲ� ź �� ������ Text UI�� ������Ʈ
                weaponSetting.currentAmmo = weaponSetting.maxAmmo;
                onAmmoEvent.Invoke(weaponSetting.currentAmmo, weaponSetting.maxAmmo);

                yield break;
            }
            yield return null;
        }
    }

    private void TwoStepRaycast()
    {
        Ray ray;
        RaycastHit hit;
        Vector3 targetPoint = Vector3.zero;

        //ȭ���� �߾� ��ǥ(Aim �������� Raycast ����
        ray = mainCamera.ViewportPointToRay(Vector2.one * 0.5f);
        //���� ��Ÿ�(attackDistance) �ȿ� �ε����� ������Ʈ�� ������ targetPoint�� ������ �ε��� ��ġ
        if (Physics.Raycast(ray, out hit, weaponSetting.attackDistance))
        {
            targetPoint = hit.point;
        }
        //���� ��Ÿ� �ȿ� �ε����� ������Ʈ�� ������ targetPoint�� �ִ� ��Ÿ� ��ġ
        else
        {
            targetPoint = ray.origin + ray.direction * weaponSetting.attackDistance;
        }
        Debug.DrawRay(ray.origin, ray.direction * weaponSetting.attackDistance, Color.red);

        //ù��° Raycast�������� ���� targetPoint�� ��ǥ�������� �����ϰ�
        //�ѱ��� ���� �������� �Ͽ� Raycast ����
        Vector3 attackDirection = (targetPoint - bulletSpawnPoint.position).normalized;

        Debug.DrawRay(bulletSpawnPoint.position, attackDirection * weaponSetting.attackDistance, Color.blue);

    }

    private void ResetVariables()
    {
        isReload = false;
        isAttack = false;
    }
}
