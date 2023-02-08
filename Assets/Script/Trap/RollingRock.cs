using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RollingRock : MonoBehaviour
{
    [SerializeField] private float deactivateTime = 1.0f;          //돌 등장 후 비활성화 되는 시간
    [SerializeField] private float casingSpin = 1.0f;              //돌이 회전하는 속력 계수
    [SerializeField] private float speed = 10.0f;                  //속도

    private Rigidbody rigidbody3D;
    private MemoryPool memoryPool;
    
    public void Setup(MemoryPool pool, Transform EndPos)
    {
        rigidbody3D = GetComponent<Rigidbody>();
        memoryPool = pool;


        /*        //돌의 이동, 속력과 회전 속력 설정
                rigidbody3D.velocity = new Vector3(direction.x, 0, 0);
                rigidbody3D.angularVelocity = new Vector3(0, 0, casingSpin);
        */
        transform.DORotate(new Vector3(0, 0, 1080), 5, RotateMode.FastBeyond360);
        transform.DOMove(EndPos.position, 5);

        //돌 함정 자동 비활성화를 위한 코루틴 실행
        StartCoroutine("DeactivateAfterTime");
    }

    private IEnumerator DeactivateAfterTime()
    {
        yield return new WaitForSeconds(deactivateTime);

        memoryPool.DeactivatePoolItem(this.gameObject);
    }


}
