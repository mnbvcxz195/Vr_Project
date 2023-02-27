using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerJump : MonoBehaviour
{
    private InputDevice targetDevice;

    public CharacterController SelectPlayer; // 제어할 캐릭터 컨트롤러
    public float Speed;  // 이동속도
    public float JumpPow;

    private float Gravity; // 중력   
    private Vector3 MoveDir; // 캐릭터의 움직이는 방향.
    private bool JumpButtonPressed;  //  최종 점프 버튼 눌림 상태

    private bool onclik = false;
    [SerializeField] GameObject go;
    [SerializeField] public AudioSource PlayerSfx;
    int randam;
    //GameObject[] Traps;
    private void Start()
    {
        Speed = 5.0f;
        Gravity = 1;
        MoveDir = Vector3.zero;
        JumpPow = 0f;
        JumpButtonPressed = false;
        SelectPlayer = GetComponent<CharacterController>();

        // 특정 컨트롤러하나만 가져오는 방법
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics rightControllerCharacteristics =
            InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devices);

        if (devices.Count > 0)
            targetDevice = devices[0];
        PlayerSfx = GetComponent<AudioSource>();
        randam = Random.Range(0, 3);
    }

    private void Update()
    {
        Jump();
        MonsterAttckCheck();
    }

    void Jump()
    {
        targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue);

        if (SelectPlayer.isGrounded)
        {
            // 키보드에 따른 X, Z 축 이동방향을 새로 결정합니다.
            //MoveDir = SelectPlayer.transform.position ;
            // 오브젝트가 바라보는 앞방향으로 이동방향을 돌려서 조정합니다.
            //MoveDir = SelectPlayer.transform.TransformDirection(MoveDir);
            // 속도를 곱해서 적용합니다.
            //MoveDir *= Speed;


            // 스페이스 버튼에 따른 점프 : 최종 점프버튼이 눌려있지 않았던 경우만 작동
            if (JumpButtonPressed == false && primaryButtonValue)
            {
                Debug.Log("JumpButtonPressed false1" + JumpButtonPressed);
                JumpButtonPressed = true;
                MoveDir.y = JumpPow;
                Debug.Log(JumpPow);
                Debug.Log("JumpButtonPressed true2" + JumpButtonPressed);
            }
        }
        // 캐릭터가 바닥에 붙어 있지 않다면
        else
        {
            // 중력의 영향을 받아 아래쪽으로 하강합니다.           
            MoveDir.y -= Gravity * Time.deltaTime;
        }

        // 점프버튼이 눌려지지 않은 경우
        if (!primaryButtonValue)
        {
            //Debug.Log("JumpButtonPressed true3" + JumpButtonPressed);
            JumpButtonPressed = false;  // 최종점프 버튼 눌림 상태 해제
            //Debug.Log("JumpButtonPressed false4" + JumpButtonPressed);

        }
        // 앞 단계까지는 캐릭터가 이동할 방향만 결정하였으며,
        // 실제 캐릭터의 이동은 여기서 담당합니다.
        SelectPlayer.Move(MoveDir * Time.deltaTime);

    }
    void MonsterAttckCheck()
    {
        if (go != null)
        {
            if (MonsterManager.GetInstance().Newmonster.MonsterHp > 0)
            {
                float sss = Vector3.Distance(go.transform.position, gameObject.transform.position);
                if (sss < 0.8f)
                {
                    PlayerManager.GetInstance().Damage(40);
                    AudioManager.GetInstance().PlayerSfxPlay(PlayerSfx, randam);
                    Debug.Log("칼맞음");

                }
            }
        }
        else
            return;
    }
     void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.GetComponent<Traps>())
        {
            PlayerManager.GetInstance().Damage(40);
            AudioManager.GetInstance().PlayerSfxPlay(PlayerSfx, randam);

            Debug.Log("함정충돌");
        }
    }

}