using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerJump : MonoBehaviour
{
    private InputDevice targetDevice;

    public CharacterController SelectPlayer; // ������ ĳ���� ��Ʈ�ѷ�
    public float Speed;  // �̵��ӵ�
    public float JumpPow;

    private float Gravity; // �߷�   
    private Vector3 MoveDir; // ĳ������ �����̴� ����.
    private bool JumpButtonPressed;  //  ���� ���� ��ư ���� ����

    private bool onclik = false;
    [SerializeField] GameObject go;

    private void Start()
    {
        Speed = 5.0f;
        Gravity = 10.0f;
        MoveDir = Vector3.zero;
        JumpPow = 3.0f;
        JumpButtonPressed = false;
        SelectPlayer = GetComponent<CharacterController>();

        // Ư�� ��Ʈ�ѷ��ϳ��� �������� ���
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics rightControllerCharacteristics =
            InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devices);

        if (devices.Count > 0)
            targetDevice = devices[0];
    }

    private void Update()
    {
        Jump();
        monte();
        MonsterAttckCheck();
    }

    void Jump()
    {
        targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue);

        if (SelectPlayer.isGrounded)
        {
            // Ű���忡 ���� X, Z �� �̵������� ���� �����մϴ�.
            //MoveDir = SelectPlayer.transform.position ;
            // ������Ʈ�� �ٶ󺸴� �չ������� �̵������� ������ �����մϴ�.
            //MoveDir = SelectPlayer.transform.TransformDirection(MoveDir);
            // �ӵ��� ���ؼ� �����մϴ�.
            //MoveDir *= Speed;
            

            // �����̽� ��ư�� ���� ���� : ���� ������ư�� �������� �ʾҴ� ��츸 �۵�
            if (JumpButtonPressed == false && primaryButtonValue)
            {
                Debug.Log("JumpButtonPressed false1" + JumpButtonPressed);
                JumpButtonPressed = true;
                MoveDir.y = JumpPow;
                Debug.Log(JumpPow);
                Debug.Log("JumpButtonPressed true2" + JumpButtonPressed);
            }
        }
        // ĳ���Ͱ� �ٴڿ� �پ� ���� �ʴٸ�
        else
        {
            // �߷��� ������ �޾� �Ʒ������� �ϰ��մϴ�.           
            MoveDir.y -= Gravity * Time.deltaTime;
        }

        // ������ư�� �������� ���� ���
        if (!primaryButtonValue)
        {
            Debug.Log("JumpButtonPressed true3" + JumpButtonPressed);
            JumpButtonPressed = false;  // �������� ��ư ���� ���� ����
            Debug.Log("JumpButtonPressed false4" + JumpButtonPressed);

        }
        // �� �ܰ������ ĳ���Ͱ� �̵��� ���⸸ �����Ͽ�����,
        // ���� ĳ������ �̵��� ���⼭ ����մϴ�.
        SelectPlayer.Move(MoveDir * Time.deltaTime);

    }
    void monte()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            MonsterManager.GetInstance().battle = true;
            MonsterManager.GetInstance().Newmonster.MonsterHp -= 10;
            Debug.Log($"{MonsterManager.GetInstance().Newmonster.MonsterHp}");
        }
    }
    void MonsterAttckCheck()
    {
        if (MonsterManager.GetInstance().Newmonster.MonsterHp > 0)
        {
            float sss = Vector3.Distance(go.transform.position, gameObject.transform.position);
            //Debug.Log($"P{sss}");
            if (sss < 0.8f)
            {
                PlayerManager.GetInstance().Damage(40);
                Debug.Log("Į����");

            }
        }
    }

}