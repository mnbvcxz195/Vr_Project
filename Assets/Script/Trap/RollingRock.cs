using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingRock : MonoBehaviour
{
    [SerializeField] private float deactivateTime = 5.0f;          //돌 등장 후 비활성화 되는 시간
    [SerializeField] private float casingSpin = 1.0f;              //돌이 회전하는 속력 계수

    private Rigidbody rigidbody3D;
    private MemoryPool memoryPool;

    public void Setup(MemoryPool pool, Vector3 direction)
    {
        rigidbody3D = GetComponent<Rigidbody>();
        memoryPool = pool;

        //돌의 이동, 속력과 회전 속력 설정
        rigidbody3D.velocity = new Vector3(direction.x, 0, 0);
        rigidbody3D.angularVelocity = new Vector3(0, 0, casingSpin);

        //돌 함정 자동 비활성화를 위한 코루틴 실행
        StartCoroutine("DeactivateAfterTime");
    }

    private IEnumerator DeactivateAfterTime()
    {
        yield return new WaitForSeconds(deactivateTime);

        memoryPool.DeactivatePoolItem(this.gameObject);
    }
}
