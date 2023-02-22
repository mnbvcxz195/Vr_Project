using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitGraphic : MonoBehaviour
{
    [SerializeField] private float deactivateTime = 1.0f;          //돌 등장 후 비활성화 되는 시간
    [SerializeField] private float speed = 10.0f;                  //속도

    private MemoryPool memoryPool;

    public void Setup(MemoryPool pool)
    {
        memoryPool = pool;

        //돌 함정 자동 비활성화를 위한 코루틴 실행
        StartCoroutine("DeactivateAfterTime");
    }

    private IEnumerator DeactivateAfterTime()
    {
        yield return new WaitForSeconds(deactivateTime);

        memoryPool.DeactivatePoolItem(this.gameObject);
    }
}
