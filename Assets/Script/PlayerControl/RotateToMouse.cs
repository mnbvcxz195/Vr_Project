using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToMouse : MonoBehaviour
{
    [SerializeField] private float rotCamXAxisSpeed = 5;   //ī�޶� x�� ȸ�� �ӵ�
    [SerializeField] private float rotCamYAxisSpeed = 3;   //ī�޶� y�� ȸ�� �ӵ�

    private float limitMinX = -80;   //ī�޶� x�� ȸ�� ���� (�ּ�)
    private float limitMaxX = 50;   //ī�޶� x�� ȸ�� ���� (�ִ�)
    private float eulerAngleX;
    private float eulerAngleY;
    bool ondemege = false;
    [SerializeField] GameObject go;
    private void Update()
    {
        MonsterAttckCheck();
    }
    public void UpdateRotate(float mouseX, float mouseY)
    {
        eulerAngleY += mouseX * rotCamYAxisSpeed;  //���콺 ��/�� �̵����� ī�޶� y�� ȸ��
        eulerAngleX -= mouseY * rotCamXAxisSpeed;  //���콺 ��/�Ʒ� �̵����� ī�޶� x�� ȸ��

        //ī�޶� x�� ȸ���� ��� ȸ�� ������ ����
        eulerAngleX = ClampAngle(eulerAngleX, limitMinX, limitMaxX);

        transform.rotation = Quaternion.Euler(eulerAngleX, eulerAngleY, 0);
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) angle += 360;
        if (angle > 360) angle -= 360;

        return Mathf.Clamp(angle, min, max);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "trap")
        {
            PlayerManager.GetInstance().Damage(40);
            Debug.Log("�����浹");
        }
    }
    void MonsterAttckCheck()
    {
        if(MonsterManager.GetInstance().Newmonster.MonsterHp > 0)
        {
            float sss = Vector3.Distance(go.transform.position, gameObject.transform.position);
            Debug.Log($"P{sss}");
            if (sss < 0.8f)
            {
                PlayerManager.GetInstance().Damage(40);
                Debug.Log("Į����");

            }
        }
    }
}
