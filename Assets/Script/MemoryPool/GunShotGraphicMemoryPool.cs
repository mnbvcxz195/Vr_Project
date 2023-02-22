using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShotGraphicMemoryPool : MonoBehaviour
{
    [SerializeField] GameObject gunShotGraphic;
    private MemoryPool memoryPool;

    private void Awake()
    {
        memoryPool = new MemoryPool(gunShotGraphic);
    }

    public void SpawnGunShotGraphic(Transform parent, Quaternion rot)
    {
        GameObject item = memoryPool.ActivatePoolItem();

        item.transform.position = parent.position;
        item.transform.rotation = rot;
        item.GetComponent<GunShotGraphic>().Setup(memoryPool);

    }
}

