using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody rigid;

    public int JumpPower;
    public int MoveSpeed;

    [SerializeField]
    private KeyCode keyCodeReload = KeyCode.R;                         //재장전 키


    private WeaponPistol weapon;                 //무기를 이용한 공격 제어

    private void Awake()
    {
        GameManager.GetInstance().CheckTrap();
        rigid = GetComponent<Rigidbody>();
        weapon = GetComponentInChildren<WeaponPistol>();
    }

    void Start()
    {

    }
    private void Update()
    {
        Move();
        Jump();
        ttest();
        UpdateWeaponAction();
    }
    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        transform.Translate((new Vector3(h, 0, v) * MoveSpeed) * Time.deltaTime);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigid.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
        }
    }
    void ttest()
    {
        Debug.Log("2");

        int tarpCount = GameManager.GetInstance().trapList.Count;
        for (int i = 0; i < tarpCount; i++)
        {
            var tarpp = GameManager.GetInstance().trapList[i];
            if (Vector3.Distance(transform.position, tarpp.transform.position) < .1f)
            {
                tarpp.gameObject.SetActive(false);
            }
        }
    }

    private void UpdateWeaponAction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            weapon.StartWeaponAction();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            weapon.StopWeaponAction();
        }

        if (Input.GetKeyDown(keyCodeReload))
        {
            weapon.StartReload();
        }
    }
}
