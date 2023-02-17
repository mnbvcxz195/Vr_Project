using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Input KeyCode")]
    [SerializeField]
    private KeyCode keyCodeRun = KeyCode.LeftShift;                //달리기 키

    [SerializeField]
    private KeyCode keyCodeJump = KeyCode.Space;                   //점프 키

    [SerializeField]
    private KeyCode keyCodeReload = KeyCode.R;                         //재장전 키

    private RotateToMouse rotateToMouse;               //마우스 이동으로 카메라 회전
    private MovementCharacterController movement;      //키보드 입력으로 플레이어 이동, 점프
    private Status status;                             //이동속도 등의 플레이어 정보
    private WeaponPistol weapon;                 //무기를 이용한 공격 제어

    private void Awake()
    {
        //마우스 커서를 보이지 않게 설정하고, 현재 위치에 고정
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        rotateToMouse = GetComponent<RotateToMouse>();
        movement = GetComponent<MovementCharacterController>();
        status = GetComponent<Status>();
        weapon = GetComponentInChildren<WeaponPistol>();
    }

    private void Update()
    {
        UpdateRotate();
        UpdateMove();
        UpdateJump();
        monte();
        //UpdateWeaponAction();
    }

    private void UpdateRotate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        rotateToMouse.UpdateRotate(mouseX, mouseY);
    }

    private void UpdateMove()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        //이동중 일 때 (걷기 or 뛰기)
        if (x != 0 || z != 0)
        {
            bool isRun = false;

            //옆이나 뒤로 이동할 때는 달릴 수 없음
            if (z > 0)
                isRun = Input.GetKey(keyCodeRun);

            movement.MoveSpeed = isRun == true ? status.RunSpeed : status.WalkSpeed;

        }
        //제자리에 멈춰있을 때
        else
        {
            movement.MoveSpeed = 0;

        }

        movement.MoveTo(new Vector3(x, 0, z));
    }

    private void UpdateJump()
    {
        if (Input.GetKeyDown(keyCodeJump))
        {
            movement.Jump();
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
    void monte()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            MonsterManager.GetInstance().battle = true;
            MonsterManager.GetInstance().Newmonster.MonsterHp -= 10;
            Debug.Log($"{MonsterManager.GetInstance().Newmonster.MonsterHp}");
        }
    }
}
