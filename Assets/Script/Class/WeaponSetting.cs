using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponName
{
    Pistol = 0
}

[System.Serializable]
public struct WeaponSetting
{
    public WeaponName weaponName;     //���� �̸�
    public int currentMagazine;       //���� źâ ��
    public int mazMagazine;           //�ִ� źâ ��
    public int currentAmmo;           //���� ź�� ��
    public int maxAmmo;               //�ִ� ź�� ��
    public float attackRate;          //���� �ӵ�
    public float attackDistance;      //���� ��Ÿ�
    public bool isAutomaticAttack;    //���� ���� ����
}