using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMemoryPool : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;     //총알 오브젝트
    private MemoryPool memoryPool;                        //총알 메모리풀

    private void Awake()
    {
        memoryPool = new MemoryPool(bulletPrefab);
    }

    public void SpawnBullet(Transform parent, Transform endPos)
    {
        GameObject item = memoryPool.ActivatePoolItem();

        item.transform.position = parent.position;
        item.GetComponent<Bullet>().Setup(memoryPool, endPos);
    }
}
