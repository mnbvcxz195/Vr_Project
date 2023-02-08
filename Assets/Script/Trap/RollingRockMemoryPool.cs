using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingRockMemoryPool : MonoBehaviour
{
    [SerializeField] private GameObject rockPrefab;     //돌 오브젝트
    private MemoryPool memoryPool;                      //돌 메모리풀

    private void Awake()
    {
        memoryPool = new MemoryPool(rockPrefab);
    }

    public void SpawnRock(Vector3 position, Vector3 direction)
    {
        GameObject item = memoryPool.ActivatePoolItem();
        item.transform.position = position;
        item.transform.rotation = Random.rotation;
        item.GetComponent<RollingRock>().Setup(memoryPool, direction);
    }
}
