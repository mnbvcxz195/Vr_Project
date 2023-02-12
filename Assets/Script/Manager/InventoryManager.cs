using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ItemList : Dictionary<int, Item> { }

public class InventoryManager : MonoBehaviour
{
    #region instance

    private static InventoryManager instance = null;

    public static InventoryManager GetInstance()
    {
        if (instance == null)
        {
            GameObject go = new GameObject("@InventoryManager");
            instance = go.AddComponent<InventoryManager>();

            DontDestroyOnLoad(go);
        }
        return instance;

    }
    #endregion

    public Dictionary<ItemType, ItemList> Items { get; private set; }
    private PriorityQueue<int> _itemPosition = new PriorityQueue<int>();
    
    private int _inventoryCount = 16;
    private int _curIdx = 0;

    
    public delegate void ItemUpdateHandler(Item item);
    public event ItemUpdateHandler OnItemAddHandler;
    public event ItemUpdateHandler OnItemUseHandler;
    
    private void Awake()
    {
        Items = new Dictionary<ItemType, ItemList>();
        
        foreach (ItemType type in Enum.GetValues(typeof(ItemType)))
            Items.Add(type, new ItemList());
        
        for (int i = 0; i < _inventoryCount; i++)
            _itemPosition.Push(i);
    }

    /// <summary> 아이템 획득 </summary>
    public void AcquireItem(ItemBase item, int _count = 1)
    {
        var itemList = Items[item.Type];
        switch (item.Type)
        {
            case ItemType.Ingredient:
            case ItemType.Consumable:
                if (itemList.ContainsKey(item.ItemIdx))
                {
                    itemList[item.ItemIdx].SetItemCount(_count);

                    OnItemAddHandler?.Invoke(itemList[item.ItemIdx]);
                    return;
                }
                break;
            
            default:
                if (itemList.ContainsKey(item.ItemIdx))
                {
                    return;
                }
                break;
        }

        if (_itemPosition.Count > 0)
        {
            var itemPos = _itemPosition.Pop();
            itemList.Add(item.ItemIdx, new Item(itemPos, _count, item));
            
            OnItemAddHandler?.Invoke(itemList[item.ItemIdx]);
        }
        else
            Debug.Log($"꽉참");
    }

    
    /// <summary> 아이템 사용 </summary>
    public void UseItem(ItemType type, int idx)
    {
        var itemList = Items[type];
        
        if (itemList[idx].item != null)
        {
            Debug.Log($"아이템 사용");
            
            var count = itemList[idx].SetItemCount(-1);
            OnItemUseHandler?.Invoke(itemList[idx]);

            if (count <= 0)
            {
                _itemPosition.Push(itemList[idx].itemPosition);
                itemList.Remove(idx);
            }
        }
        else
            Debug.Log($"텅");
    }
}