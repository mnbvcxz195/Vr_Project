using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float deactivateTime = 1.0f;          //�� ���� �� ��Ȱ��ȭ �Ǵ� �ð�
    [SerializeField] private float speed = 10.0f;                  //�ӵ�

    private MemoryPool memoryPool;

    public void Setup(MemoryPool pool, Transform endPos)
    {
        memoryPool = pool;

        transform.DORotate(new Vector3(0, 0, 720), 1, RotateMode.FastBeyond360);
        transform.DOMove(endPos.position, 1);

        //�� ���� �ڵ� ��Ȱ��ȭ�� ���� �ڷ�ƾ ����
        StartCoroutine("DeactivateAfterTime");
    }

    private IEnumerator DeactivateAfterTime()
    {
        yield return new WaitForSeconds(deactivateTime);

        memoryPool.DeactivatePoolItem(this.gameObject);
    }
}
