using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasingMemoryPool : MonoBehaviour
{
    [SerializeField]
    private GameObject casingPrefab;       //탄피 오브젝트
    private MemoryPool memoryPool;         //탄피 메모리풀

    private void Awake()
    {
        memoryPool = new MemoryPool(casingPrefab);
    }

    public void SpawnCasing(Transform parent, Vector3 direction)
    {
        GameObject item = memoryPool.ActivatePoolItem();

        item.transform.position = parent.position;
        item.GetComponent<Casing>().Setup(memoryPool, direction);
    }
}
