using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap : MonoBehaviour
{
    private WaitForSecondsRealtime waitForSeconds_CreateRock = new WaitForSecondsRealtime(1.5f);

    private RollingRockMemoryPool rollingRockMemoryPool;  //�� ���� �� Ȱ��/��Ȱ�� ����

    [Header("PlayerPos")]
    [SerializeField]
    private Transform player;                             //�÷��̾� ��ġ

    [Header("Rolling Rock")]
    [SerializeField]
    private Transform rockSpawnPoint;                     //�� ���� ��ġ
    [SerializeField]
    private Transform rockEndPoint;                       //�� ������ ��ġ
    [SerializeField]
    private Transform rockActivePoint;                    //�� ���� �ߵ� ��ġ(��ó�� �ö� ���� �ߵ�)
    private float User_Rock_dis;                          //�÷��̾�, �� ���� Ʈ���� �Ÿ�
    [SerializeField]
    private bool isStopableRock = false;                  //�� ���� ���� �� �ִ� ����ġ
    [SerializeField]
    private bool isActiveRock = false;                    //�� ���� �ߵ�



    private void Awake()
    {
        rollingRockMemoryPool = GetComponent<RollingRockMemoryPool>();
    }

    private void Update()
    {
        CheckCloseRockTrigger();
    }

    private void Start()
    {
        StartCoroutine(CreateRock());
    }

/*    public void CreateRock()
    {
        float distance = Vector3.Distance(player.position, rockActivePoint.position);
        //Debug.Log(distance);

        if(distance < 1.5f)
            rollingRockMemoryPool.SpawnRock(rockSpawnPoint, rockEndPoint);
    }*/

    IEnumerator CreateRock()       //���� �ߵ� Ʈ���� ��ó �ý� �� ���� ����
    {
        while (true)
        {
            if (isActiveRock && !isStopableRock)
            {
                rollingRockMemoryPool.SpawnRock(rockSpawnPoint, rockEndPoint);
            }

            yield return waitForSeconds_CreateRock;
        }
    }
    void CheckCloseRockTrigger()
    {
        User_Rock_dis = Vector3.Distance(player.position, rockActivePoint.position);

        if (User_Rock_dis < 1.5f)
        {
            isActiveRock = true;
        }

    }
}
