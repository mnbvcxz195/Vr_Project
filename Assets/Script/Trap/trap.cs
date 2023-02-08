using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap : MonoBehaviour
{
    private RollingRockMemoryPool rollingRockMemoryPool;  //돌 생성 후 활성/비활성 관리

    [Header("Spawn Points")]
    [SerializeField]
    private Transform rockSpawnPoint;        //돌 생성 위치
    [SerializeField]
    private Transform rockEndPoint;        //돌 마지막 위치


    private void Awake()
    {
        rollingRockMemoryPool = GetComponent<RollingRockMemoryPool>();
    }

    private void Update()
    {
        
    }

    private void Start()
    {
        //CreateRock();
    }

    public void CreateRock()
    {
        rollingRockMemoryPool.SpawnRock(rockSpawnPoint, rockEndPoint);
    }
}
