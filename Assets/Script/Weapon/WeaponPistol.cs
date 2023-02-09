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
    private Transform casingSpawnPoint;        //탄피 생성 위치
    [SerializeField]
    private Transform bulletSpawnPoint;        //총알 생성 위치

    [Header("Weapon Setting")]
    [SerializeField]
    private WeaponSetting weaponSetting;       //무기 설정

    private float lastAttackTime = 0;          //마지막 발사시간 체크
    private bool isReload = false;             //재장전 중인지 체크
    private bool isAttack = false;             //공격 여부 체크용

    [SerializeField] private Transform endPos;  //총알 마지막 도달 위치

    private CasingMemoryPool casingMemoryPool; //탄피 생성 후 활성/비활성 관리
    private BulletMemoryPool bulletMemoryPool; //총알 생성 후 활성/비활성 관리
    [SerializeField]private Camera mainCamera;                 //광선 발사

    /*    //외부에서 필요한 정보를 열람하기 위해 정의한 Get Property's
        public WeaponName WeaponName => weaponSetting.weaponName;
        public int CurrentMagazine => weaponSetting.currentMagazine;
        public int MaxMagazine => weaponSetting.mazMagazine;*/
    public float AttackDistance => weaponSetting.attackDistance;

    private void Awake()
    {
        casingMemoryPool = GetComponent<CasingMemoryPool>();
        bulletMemoryPool = GetComponent<BulletMemoryPool>();
        mainCamera = Camera.main;

        //처음 탄창 수는 최대로 설정
        weaponSetting.currentMagazine = weaponSetting.mazMagazine;
        //처음 탄 수는 최대로 설정
        weaponSetting.currentAmmo = weaponSetting.maxAmmo;
    }

    private void OnEnable()
    {
        //무기가 활성화될 때 해당 무기의 탄창 정보를 갱신한다.
        onMagazineEvent.Invoke(weaponSetting.currentMagazine);
        //무기가 활성화될 때 해당 무기의 탄 수 정보를 갱신한다.
        onAmmoEvent.Invoke(weaponSetting.currentAmmo, weaponSetting.maxAmmo);

        ResetVariables();
    }

    public void StartWeaponAction(int type = 0)
    {
        //재장전 중일 때는 무기 액션 불가
        if (isReload == true)
            return;

        //마우스 왼쪽 클릭(공격 시작)
        if (type == 0)
        {
            //연속 공격
            if (weaponSetting.isAutomaticAttack == true)
            {
                isAttack = true;
                StartCoroutine("OnAttackLoop");
            }
            //단발 공격
            else
            {
                OnAttack();
            }
        }
    }

    public void StopWeaponAction(int type = 0)
    {
        //마우스 왼쪽 클릭(공격 종료)
        if (type == 0)
        {
            isAttack = false;
            StopCoroutine("OnAttackLoop");
        }
    }

    public void StartReload()
    {
        //현재 재장전 중이면 재장전 불가능
        if (isReload == true || weaponSetting.currentAmmo == weaponSetting.maxAmmo || weaponSetting.currentMagazine <= 0)
            return;

        //무기 액션 도중에 'R'키를 눌러 재장전을 시도하면 무기 액션 종료 후 재장전
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

            //공격주기가 되어야 공격할 수 있도록 하기 위해 현재 시간 저장
            lastAttackTime = Time.time;

            //탄 수가 없으면 공격 불가능
            if (weaponSetting.currentAmmo <= 0)
            {
                return;
            }
            //공격시 currentAmno 1 감소, 탄 수 UI 업데이트
            weaponSetting.currentAmmo--;
            onAmmoEvent.Invoke(weaponSetting.currentAmmo, weaponSetting.maxAmmo);

            //총알 생성
            bulletMemoryPool.SpawnBullet(bulletSpawnPoint, endPos);

            //탄피 생성
            casingMemoryPool.SpawnCasing(casingSpawnPoint, transform.right);

            //광선을 발사해 원하는 위치 공격 (+Impact Effect)
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

                //현재 탄창 수를 1 감소시키고, 바뀐 탄창 정보를 Text UI에 업데이트
                weaponSetting.currentMagazine--;
                onMagazineEvent.Invoke(weaponSetting.currentMagazine);

                //현재 탄 수를 최대로 설정하고, 바뀐 탄 수 정보를 Text UI에 업데이트
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

        //화면의 중앙 좌표(Aim 기준으로 Raycast 연산
        ray = mainCamera.ViewportPointToRay(Vector2.one * 0.5f);
        //공격 사거리(attackDistance) 안에 부딪히는 오브젝트가 있으면 targetPoint는 광선에 부딪힌 위치
        if (Physics.Raycast(ray, out hit, weaponSetting.attackDistance))
        {
            targetPoint = hit.point;
        }
        //공격 사거리 안에 부딪히는 오브젝트가 없으면 targetPoint는 최대 사거리 위치
        else
        {
            targetPoint = ray.origin + ray.direction * weaponSetting.attackDistance;
        }
        Debug.DrawRay(ray.origin, ray.direction * weaponSetting.attackDistance, Color.red);

        //첫번째 Raycast연산으로 얻은 targetPoint를 목표지점으로 설정하고
        //총구를 시작 지점으로 하여 Raycast 연산
        Vector3 attackDirection = (targetPoint - bulletSpawnPoint.position).normalized;

        Debug.DrawRay(bulletSpawnPoint.position, attackDirection * weaponSetting.attackDistance, Color.blue);

    }

    private void ResetVariables()
    {
        isReload = false;
        isAttack = false;
    }
}
