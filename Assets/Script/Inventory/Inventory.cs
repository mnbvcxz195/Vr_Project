using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

#region Inven: �� ������ ��� (�׽�Ʈ��)
/// <summary> �� ������ ��� class </summary>
[Serializable]
public class Inven
{
    [SerializeField] public int itemCount;
    [SerializeField] public ItemBase Item;
}
#endregion

public class Inventory : MonoBehaviour
{
    #region �κ��丮 Ȱ��ȭ ����
    /// <summary> �κ��丮 Ȱ��ȭ ���� </summary>
    #endregion
    private bool inventoryActivated;

    #region �κ��丮 ������Ʈ
    /// <summary> �κ��丮 ������Ʈ </summary>
    #endregion
    [SerializeField] private Transform slotParent;

    #region �κ��丮 ����
    /// <summary> �κ��丮 ���� </summary>
    #endregion
    [SerializeField] private Slot[] slots;

    #region �� ������ ��� (�׽�Ʈ��)
    /// <summary> �� ������ ��� </summary>
    #endregion
    [SerializeField] private Inven[] myItem;


    private void OnValidate()
    {
        slots = slotParent.GetComponentsInChildren<Slot>();
    }

    void Awake()
    {
        myItem = new Inven[slots.Length];

        for (int i = 0; i < slots.Length; i++)
        {
            int idx = i;
            slots[i].btnUsed.onClick.AddListener(() => { UseItem(idx); });
        }

        TestList();
    }

    private void Update()
    {
        OpenInventory();
    }

    #region �׽�Ʈ��
    /// <summary> �ν����Ϳ� ������ ������ ��� ǥ�� </summary>
    public void TestList()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            int idx = i;
            myItem[i].Item = slots[idx].Item;
            myItem[i].itemCount = slots[idx].ItemCount;
        }
    }
    #endregion

    /// <summary> �κ��丮 ���� ���� </summary>
    private void OpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!inventoryActivated)
            {
                slotParent.gameObject.SetActive(true);
                inventoryActivated = true;
            }
            else
            {
                slotParent.gameObject.SetActive(false);
                inventoryActivated = false;
            }
        }

    }

    /// <summary> ������ ȹ�� </summary>
    public void AcquireItem(ItemBase item, int _count = 1)
    {
        if (item.Type == ItemType.Ingredient || item.Type == ItemType.Consumable)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].Item != null)
                {
                    if (slots[i].Item.ItemName == item.ItemName)
                    {
                        slots[i].SetItemCount(_count); //���� ���� �ߺ����� ��� ������ ���� +1
                        return;                        //�ߺ����� ���Կ� �߰� X
                    }
                }
            }
        }

        int num = 0;
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].Item == null)                 //���Կ� �ڸ� ���������� ���� ������ �߰�
            {
                slots[i].AddItem(item, _count);
                return;
            }
            else
                num++;
        }
        if (slots.Length <= num)
            Debug.Log($"����");
    }

    /// <summary> ������ ��� </summary>
    public void UseItem(int idx)
    {
        if (slots[idx].Item != null)
        {
            Debug.Log($"������ ���");
            slots[idx].SetItemCount(-1);
            TestList();
        }
        else
            Debug.Log($"��");
    }
}