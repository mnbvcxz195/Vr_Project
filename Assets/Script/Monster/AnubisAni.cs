using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnubisAni : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] AudioSource monsfx;

    Vector3 camerori;
    public enum State // 몬스터의 상태
    { 
    Idle,
    Run,
    Jump,
    Attck0,
    Attck1,
    Attck2,
    }
    public State state = State.Idle;
    [SerializeField] Animator atr;
    [SerializeField] GameObject pla;

    public int speed = 1;
    public float traceDist = 15f; //플레이어 추적범위 넘어가면 점프애니메이션발동
    public float MonAttck = 3f; //몬스터가 공격을할수있는범위
    public bool isDie = false;
    Vector3 vete;
    Vector3 vetea;


    void Start()
    {
        camerori = cam.transform.localPosition;

        StartCoroutine(CheckMonState());
        StartCoroutine(MonsterAction());
    }
    void Update()
    {
        AnubisDie();
        switch (state) // 상태마다 움직이는 속도체크
        {
            case State.Run:
            case State.Jump:
                if (atr.GetCurrentAnimatorStateInfo(0).IsName("jump"))
                {
                    MonsterMove(6);
                }
                MonsterMove(1);
                break;
        }
    }
    IEnumerator CheckMonState() //거리상태에따라 상태변경 또한 공격패턴 랜덤
    {
        while(!isDie)
        {
           if(MonsterManager.GetInstance().battle == true)
            {
                atr.SetBool("IsBattle", true);

            }
            yield return new WaitForSeconds(0.3f);
            for (int i = 0; i < 3; i++)
            {
                atr.SetBool($"IsAttck_{i}", false);
            }
            atr.SetBool("IsJump", false);
            CameraShake();

            float distance = Vector3.Distance(pla.transform.position, transform.position);
            Debug.Log($"{distance}");
            if(distance <= MonAttck)
            {
                int random = Random.Range(0, 3);
                switch(random)
                {
                    case 0:
                        state = State.Attck0;
                        break;
                    case 1:
                        state = State.Attck1;
                        break;
                    case 2:
                        state = State.Attck2;
                        break;
                }
            }
            else if(distance <= traceDist)
            {
                state = State.Run;
            }
            else
            {
                state = State.Jump;
            }
        }
    }
    IEnumerator MonsterAction() // 몬스터 상태에따른 애니메이션 변경
    {
        while(!isDie)
        {
            switch(state)
            {
                case State.Run:
                    atr.SetBool("IsRun", true);
                    break;
                case State.Jump:
                    atr.SetBool("IsJump", true);
                    AudioManager.GetInstance().MonSfxPlay(monsfx, 1,true);
                    break;
                case State.Attck0:
                    atr.SetBool("IsAttck_0", true);
                    AudioManager.GetInstance().MonSfxPlay(monsfx, 2, true);
                    break;
                case State.Attck1:
                    atr.SetBool("IsAttck_1", true);
                    AudioManager.GetInstance().MonSfxPlay(monsfx, 3,true);
                    break;
                case State.Attck2:
                    atr.SetBool("IsAttck_2", true);
                    AudioManager.GetInstance().MonSfxPlay(monsfx, 4, true);
                    break;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    IEnumerator camerashake(float duration, float manitude) //카메라 진동
    {
        float timer = 0;
        while (timer <= duration)
        {
            cam.transform.localPosition = Random.insideUnitSphere * manitude + camerori;

            timer += Time.deltaTime;
            yield return null;
        }
        cam.transform.localPosition = camerori;
    }
    void MonsterMove(int i) //몬스터가 플레이어를 추적하겠금
    {
        if(!isDie && MonsterManager.GetInstance().battle)
        {
            Vector3 l_vector = pla.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(new Vector3(l_vector.x, vete.y, l_vector.z)).normalized;

            if (atr.GetCurrentAnimatorStateInfo(0).IsName("walk") || atr.GetCurrentAnimatorStateInfo(0).IsName("jump"))
            {
                transform.position = Vector3.MoveTowards(transform.position, pla.transform.position, (speed * i) * Time.deltaTime);
            }

        }
    }
    void CameraShake() // 애니메이션이 점프일때 그 애니메이션이 0.8이상일때 진동하겠금
    {
        if(atr.GetCurrentAnimatorStateInfo(0).IsName("Jump")&&atr.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f)
        {
            StartCoroutine(camerashake(0.5f, 0.1f));
        }
    }
    void AnubisDie()
    {
        if(MonsterManager.GetInstance().Newmonster.MonsterHp <= 0)
        {
            AudioManager.GetInstance().MonSfxPlay(monsfx, 5, false);
            isDie = true;
            atr.SetBool("IsDie", true);
        }
    }
}

