using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float deactivateTime = 1.0f;          //돌 등장 후 비활성화 되는 시간
    [SerializeField] private float speed = 10.0f;                  //속도

    private MemoryPool memoryPool;

    public void Setup(MemoryPool pool, Transform endPos)
    {
        memoryPool = pool;

        transform.DORotate(new Vector3(0, 0, 720), 1, RotateMode.FastBeyond360);
        transform.DOMove(endPos.position, 1);

        //돌 함정 자동 비활성화를 위한 코루틴 실행
        StartCoroutine("DeactivateAfterTime");
    }

    private IEnumerator DeactivateAfterTime()
    {
        yield return new WaitForSeconds(deactivateTime);

        memoryPool.DeactivatePoolItem(this.gameObject);
    }
}
