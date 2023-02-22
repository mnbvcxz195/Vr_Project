using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitGraphic : MonoBehaviour
{
    [SerializeField] private float deactivateTime = 1.0f;          //�� ���� �� ��Ȱ��ȭ �Ǵ� �ð�
    [SerializeField] private float speed = 10.0f;                  //�ӵ�

    private MemoryPool memoryPool;

    public void Setup(MemoryPool pool)
    {
        memoryPool = pool;

        //�� ���� �ڵ� ��Ȱ��ȭ�� ���� �ڷ�ƾ ����
        StartCoroutine("DeactivateAfterTime");
    }

    private IEnumerator DeactivateAfterTime()
    {
        yield return new WaitForSeconds(deactivateTime);

        memoryPool.DeactivatePoolItem(this.gameObject);
    }
}
