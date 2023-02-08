using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap : MonoBehaviour
{
    private WaitForSecondsRealtime waitForSeconds_CreateRock = new WaitForSecondsRealtime(1.5f);

    private RollingRockMemoryPool rollingRockMemoryPool;  //돌 생성 후 활성/비활성 관리

    [Header("PlayerPos")]
    [SerializeField]
    private Transform player;                             //플레이어 위치

    [Header("Rolling Rock")]
    [SerializeField]
    private Transform rockSpawnPoint;                     //돌 생성 위치
    [SerializeField]
    private Transform rockEndPoint;                       //돌 마지막 위치
    [SerializeField]
    private Transform rockActivePoint;                    //돌 함정 발동 위치(근처로 올때 함정 발동)
    private float User_Rock_dis;                          //플레이어, 돌 함정 트리거 거리
    [SerializeField]
    private bool isStopableRock = false;                  //돌 함정 멈출 수 있는 스위치
    [SerializeField]
    private bool isActiveRock = false;                    //돌 함정 발동



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

    IEnumerator CreateRock()       //함정 발동 트리거 근처 올시 돌 함정 실행
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
