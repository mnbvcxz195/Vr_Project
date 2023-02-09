using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    //public List<ItemBase> items;          //ȹ���� ������
    private bool inventoryActivated;      //�κ��丮 Ȱ��ȭ ����

    [SerializeField]
    private Transform slotParent;         //�κ��丮 ���
    [SerializeField]
    private Slot[] slots;                 //������ ����

    private void OnValidate()
    {
        slots = slotParent.GetComponentsInChildren<Slot>();
    }

    void Awake()
    {
        //FreshSlot();
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
                        slots[i].SetItemCount(_count);
                        return;
                    }
                }
            }
        }

        int num = 0;
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].Item == null)
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

    public void UseItem(int idx)
    {
        if (slots[idx].Item != null)
        {
            slots[idx].SetItemCount(-1);
            Debug.Log($"������ ���");
        }
    }
}