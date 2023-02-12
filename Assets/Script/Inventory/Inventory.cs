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
    
    #region 내 아이템 목록 (테스트용)
    /// <summary> 내 아이템 목록 </summary>
    #endregion
    [SerializeField] private Item[] myItem;

    void Awake()
    {
        _inventoryManager = InventoryManager.GetInstance();
        _inventoryManager.OnItemAddHandler += ItemAdd;
        _inventoryManager.OnItemUseHandler += ItemUse;
        
        slots = slotParent.GetComponentsInChildren<UISlot>();
        
        for (int i = 0; i < slots.Length; i++)
        {
            int idx = i;
            
            slots[i].btnUsed.onClick.AddListener(() =>
            {
                if(slots[idx].Item == null)
                    return;
                
                ItemType itemType = slots[idx].Item.Type;
                int itemIdx = slots[idx].Item.ItemIdx; 
                
                _inventoryManager.UseItem(itemType, itemIdx);
            });
        }
    }

    public void Start()
    {
        myItem = new Item[slots.Length];
        for (int i = 0; i < myItem.Length; i++)
            myItem[i] = new Item();

        // var inventoryItem = _inventoryManager.Items;
        // foreach (var item in inventoryItem.Values)
        //     slots[item.itemPosition].AddItem(item.item, item.itemCount);
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

        TestList();
    }

    void ItemUse(Item item)
    {
        var slot = slots[item.itemPosition];
        slot.SetItemCount(item.itemCount);
        
        TestList();
    }
    
    
    #region 테스트용
    /// <summary> 인스펙터에 슬롯의 아이템 목록 표시 </summary>
    public void TestList()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            int idx = i;
            myItem[i].item = slots[idx].Item;
            myItem[i].itemCount = slots[idx].ItemCount;
        }
    }
    #endregion

}