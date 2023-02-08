using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap : MonoBehaviour
{
    private RollingRockMemoryPool rollingRockMemoryPool;  //�� ���� �� Ȱ��/��Ȱ�� ����

    [Header("Spawn Points")]
    [SerializeField]
    private Transform rockSpawnPoint;        //�� ���� ��ġ
    [SerializeField]
    private Transform rockEndPoint;        //�� ������ ��ġ


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
