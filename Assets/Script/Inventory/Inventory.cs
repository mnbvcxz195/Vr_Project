using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    public List<ItemBase> items;

    [SerializeField]
    private Transform slotParent;
    [SerializeField]
    private Slot[] slots;

    private bool inventoryActivated;
    private int itemCount;

    private void OnValidate()
    {
        slots = slotParent.GetComponentsInChildren<Slot>();
    }

    void Awake()
    {
        FreshSlot();
        for (int i = 0; i < slots.Length; i++)
        {
            int idx = i;
            slots[i].btnUsed.onClick.AddListener(() => { UseItem(idx); });
        }
    }

    private void Update()
    {
        OpenInventory();
    }

    /// <summary>
    /// �κ��丮 ����
    /// </summary>
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

    public void FreshSlot()
    {
        int i = 0;
        for (; i < items.Count && i < slots.Length; i++)
        {
            slots[i].Item = items[i];       //_item set ���� ����ȭ, ������ ī��Ʈ++
        }
        for (; i < slots.Length; i++)
        {
            slots[i].Item = null;           //_item set ���� �񰡽�ȭ
        }
    }

    public void AddItem(ItemBase _item)
    {
        if (items.Count < slots.Length)
        {
            items.Add(_item);
            FreshSlot();
        }
        else
        {
            Debug.Log($"������ ���� �� �ֽ��ϴ�.");
        }
    }

    public void UseItem(int idx)
    {
        if (slots[idx].Item != null)
        {
            slots[idx].Item = null;
            items.RemoveAt(idx);
            Debug.Log($"�������� ����Ͽ����ϴ�.");
        }
        else
            Debug.Log($"�������� �������� �ʽ��ϴ�.");
    }
}