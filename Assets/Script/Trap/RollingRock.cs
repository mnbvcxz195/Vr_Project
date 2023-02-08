using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingRock : MonoBehaviour
{
    [SerializeField] private float deactivateTime = 5.0f;          //�� ���� �� ��Ȱ��ȭ �Ǵ� �ð�
    [SerializeField] private float casingSpin = 1.0f;              //���� ȸ���ϴ� �ӷ� ���

    private Rigidbody rigidbody3D;
    private MemoryPool memoryPool;

    public void Setup(MemoryPool pool, Vector3 direction)
    {
        rigidbody3D = GetComponent<Rigidbody>();
        memoryPool = pool;

        //���� �̵�, �ӷ°� ȸ�� �ӷ� ����
        rigidbody3D.velocity = new Vector3(direction.x, 0, 0);
        rigidbody3D.angularVelocity = new Vector3(0, 0, casingSpin);

        //�� ���� �ڵ� ��Ȱ��ȭ�� ���� �ڷ�ƾ ����
        StartCoroutine("DeactivateAfterTime");
    }

    private IEnumerator DeactivateAfterTime()
    {
        yield return new WaitForSeconds(deactivateTime);

        memoryPool.DeactivatePoolItem(this.gameObject);
    }
}
