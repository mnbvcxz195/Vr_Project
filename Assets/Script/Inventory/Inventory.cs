using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    #region 인벤토리 활성화 여부
    /// <summary> 인벤토리 활성화 여부 </summary>
    #endregion
    private bool inventoryActivated;

    #region 인벤토리 매니저
    /// <summary> 인벤토리 매니저 </summary>
    #endregion
    private InventoryManager _inventoryManager;
    
    #region 인벤토리 오브젝트
    /// <summary> 인벤토리 오브젝트 </summary>
    #endregion
    [SerializeField] private Transform slotParent;

    #region 인벤토리 슬롯
    /// <summary> 인벤토리 슬롯 (배열) </summary>
    #endregion
    [SerializeField] private UISlot[] slots;

    private int changeSlotIdx;
    public int ChangeIdx => changeSlotIdx;


    void Awake()
    {
        _inventoryManager = InventoryManager.GetInstance();
        _inventoryManager.OnItemAddHandler += ItemAdd;
        
        slots = slotParent.GetComponentsInChildren<UISlot>();
    }

    private void Update()
    {
        OpenInventory();
    }

    /// <summary> 인벤토리 오픈 관리 </summary>
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

    void ItemAdd(Item item)
    {
        var slot = slots[item.itemPosition];
        
        if(slot.Item == null)
            slot.AddItem(item.item, item.itemCount);
        else
            slot.SetItemCount(item.itemCount);
    }

    public int ChangeSlotIdx()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].dropItem)
            {
                changeSlotIdx = i;
                slots[i].dropItem = false;
            }
        }

        return changeSlotIdx;
    }

    public void EquipItem()
    {
        if(!_inventoryManager.Equip)
            return;

        if (_inventoryManager._preIdx < 10)
            slots[_inventoryManager.Items[_inventoryManager.preUse][_inventoryManager._preIdx].itemPosition].SetEquipColor(1);
        if (_inventoryManager._curIdx < 10)
            slots[_inventoryManager.Items[_inventoryManager.curUse][_inventoryManager._curIdx].itemPosition].SetEquipColor(0.5f);
    }

    public void SpendSelectItem(int idx, int count)
    {
        slots[idx].SetItemCount(count);
    }
}