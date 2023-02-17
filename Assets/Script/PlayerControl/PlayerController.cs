using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Input KeyCode")]
    [SerializeField]
    private KeyCode keyCodeRun = KeyCode.LeftShift;                //�޸��� Ű

    [SerializeField]
    private KeyCode keyCodeJump = KeyCode.Space;                   //���� Ű

    [SerializeField]
    private KeyCode keyCodeReload = KeyCode.R;                         //������ Ű

    private RotateToMouse rotateToMouse;               //���콺 �̵����� ī�޶� ȸ��
    private MovementCharacterController movement;      //Ű���� �Է����� �÷��̾� �̵�, ����
    private Status status;                             //�̵��ӵ� ���� �÷��̾� ����
    private WeaponPistol weapon;                 //���⸦ �̿��� ���� ����

    private void Awake()
    {
        //���콺 Ŀ���� ������ �ʰ� �����ϰ�, ���� ��ġ�� ����
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

        //�̵��� �� �� (�ȱ� or �ٱ�)
        if (x != 0 || z != 0)
        {
            bool isRun = false;

            //���̳� �ڷ� �̵��� ���� �޸� �� ����
            if (z > 0)
                isRun = Input.GetKey(keyCodeRun);

            movement.MoveSpeed = isRun == true ? status.RunSpeed : status.WalkSpeed;

        }
        //���ڸ��� �������� ��
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
