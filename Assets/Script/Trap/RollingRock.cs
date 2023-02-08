using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RollingRock : MonoBehaviour
{
    [SerializeField] private float deactivateTime = 1.0f;          //�� ���� �� ��Ȱ��ȭ �Ǵ� �ð�
    [SerializeField] private float casingSpin = 1.0f;              //���� ȸ���ϴ� �ӷ� ���
    [SerializeField] private float speed = 10.0f;                  //�ӵ�

    private Rigidbody rigidbody3D;
    private MemoryPool memoryPool;
    
    public void Setup(MemoryPool pool, Transform EndPos)
    {
        rigidbody3D = GetComponent<Rigidbody>();
        memoryPool = pool;


        /*        //���� �̵�, �ӷ°� ȸ�� �ӷ� ����
                rigidbody3D.velocity = new Vector3(direction.x, 0, 0);
                rigidbody3D.angularVelocity = new Vector3(0, 0, casingSpin);
        */
        transform.DORotate(new Vector3(0, 0, 1080), 5, RotateMode.FastBeyond360);
        transform.DOMove(EndPos.position, 5);

        //�� ���� �ڵ� ��Ȱ��ȭ�� ���� �ڷ�ƾ ����
        StartCoroutine("DeactivateAfterTime");
    }

    private IEnumerator DeactivateAfterTime()
    {
        yield return new WaitForSeconds(deactivateTime);

        memoryPool.DeactivatePoolItem(this.gameObject);
    }


}
