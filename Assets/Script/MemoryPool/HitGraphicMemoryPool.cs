using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitGraphicMemoryPool : MonoBehaviour
{
    [SerializeField] GameObject hitGraphic;
    private MemoryPool memoryPool;

    private void Awake()
    {
        memoryPool = new MemoryPool(hitGraphic);
    }

    public void SpawnHitGraphic(Vector3 parent)
    {
        GameObject item = memoryPool.ActivatePoolItem();

        item.transform.position = parent;
        item.GetComponent<HitGraphic>().Setup(memoryPool);
    }
}
