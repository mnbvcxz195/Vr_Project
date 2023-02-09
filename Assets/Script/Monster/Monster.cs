using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] Camera cam;
    Vector3 camerori;
    public enum State // ������ ����
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
    public float traceDist = 15f; //�÷��̾� �������� �Ѿ�� �����ִϸ��̼ǹߵ�
    public float MonAttck = 4f; //���Ͱ� �������Ҽ��ִ¹���
    public bool isDie = false;

    void Start()
    {
        camerori = cam.transform.localPosition;

        StartCoroutine(CheckMonState());
        StartCoroutine(MonsterAction());
    }
    void Update()
    {

        switch (state) // ���¸��� �����̴� �ӵ�üũ
        {
            case State.Run:
                MonsterMove(1);
                break;
            case State.Jump:
                MonsterMove(6);
                break;
        }
    }
    IEnumerator CheckMonState() //�Ÿ����¿����� ���º��� ���� �������� ����
    {
        while(!isDie)
        {
            yield return new WaitForSeconds(0.3f);
            for (int i = 0; i < 3; i++)
            {
                atr.SetBool($"IsAttck_{i}", false);
            }
            atr.SetBool("IsJump", false);
            CameraShake();

            float distance = Vector3.Distance(pla.transform.position, transform.position);
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
    IEnumerator MonsterAction() // ���� ���¿����� �ִϸ��̼� ����
    {
        while(!isDie)
        {
            switch(state)
            {
                case State.Run:
                    atr.SetBool("IsRun", true);
                    Debug.Log("�̽���");
                    break;
                case State.Jump:
                    atr.SetBool("IsJump", true);
                    Debug.Log("�̽�����");
                    break;
                case State.Attck0:
                    atr.SetBool("IsAttck_0", true);
                    Debug.Log("����1");

                    break;
                case State.Attck1:
                    atr.SetBool("IsAttck_1", true);
                    Debug.Log("����2");
                    break;
                case State.Attck2:
                    atr.SetBool("IsAttck_2", true);
                    Debug.Log("����3");
                    break;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    IEnumerator camerashake(float duration, float manitude) //ī�޶� ����
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
    void MonsterMove(int i) //���Ͱ� �÷��̾ �����ϰڱ�
    {
        transform.position = Vector3.MoveTowards(transform.position, pla.transform.position, (speed * i) * Time.deltaTime);
        Vector3 lookPos = pla.transform.position - transform.position;
        Vector3 l_vector = pla.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(l_vector).normalized;
    }
    void CameraShake() // �ִϸ��̼��� �����϶� �� �ִϸ��̼��� 0.8�̻��϶� �����ϰڱ�
    {
        if(atr.GetCurrentAnimatorStateInfo(0).IsName("Jump")&&atr.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f)
        {
            StartCoroutine(camerashake(0.5f, 0.1f));
        }
    }
}

