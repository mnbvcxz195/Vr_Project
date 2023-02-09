using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    //public List<ItemBase> items;          //획득한 아이템
    private bool inventoryActivated;      //인벤토리 활성화 여부

    [SerializeField]
    private Transform slotParent;         //인벤토리 배경
    [SerializeField]
    private Slot[] slots;                 //아이템 슬롯

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
    /// 인벤토리 오픈
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
            Debug.Log($"꽉참");
    }

    public void UseItem(int idx)
    {
        if (slots[idx].Item != null)
        {
            slots[idx].SetItemCount(-1);
            Debug.Log($"아이템 사용");
        }
    }
}