using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using static UnityEditor.Progress;

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
    public int _curIdx = 10;
    public int _preIdx = 10;
    public ItemType curUse;
    public ItemType preUse;
    int test;
    Item dragItem;
    Item tempItem;
    int tempPosNum;
    public bool Equip;



    public ItemCombination _ItemCombination;

    public delegate void ItemUpdateHandler(Item item);
    public event ItemUpdateHandler OnItemAddHandler;
    public event ItemUpdateHandler OnItemUseHandler;
        public CombinationReciepeManager _combinationR = new CombinationReciepeManager();

    
    private void Awake()
    {
        Items = new Dictionary<ItemType, ItemList>();
        
        foreach (ItemType type in Enum.GetValues(typeof(ItemType)))
            Items.Add(type, new ItemList());
        
        for (int i = 0; i < _inventoryCount; i++)
            _itemPosition.Push(i);
        _ItemCombination = GameObject.FindWithTag("ItemCombination").GetComponent<ItemCombination>();//인벤토리 찾아주기

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
            itemList.Add(item.ItemIdx, new Item(itemPos, _count, item, false));
            
            OnItemAddHandler?.Invoke(itemList[item.ItemIdx]);
        }
        else
            Debug.Log($"꽉참");
    }

    public int mixItem1;
    public int mixItem2; 
    public int mixItemCount1;
    public int mixItemCount2;
    public bool mix;

    public void MixItem(ItemType type1, int idx1, ItemType type2, int idx2)
    {
        var itemList1 = Items[type1];
        var itemList2 = Items[type2];
        if (itemList1[idx1].item != null && itemList2[idx2].item != null)
        {
            if (type1 == type2 && idx1 == idx2)
            {
                if (itemList1[idx1].itemCount < 2)
                {
                    mix = false;
                    Debug.Log("재료가 부족합니다.");
                    return;
                }
                _ItemCombination.Combination(_ItemCombination.Createtype, _ItemCombination.itemnumber);
                if (_ItemCombination.Createbool == true)
                {
                    Debug.Log($"[{itemList1[idx1].item.ItemName}] 아이템과 [{itemList2[idx2].item.ItemName}]아이템을 사용하였습니다.");
                    mix = true;

                    var count1 = itemList1[idx1].SetItemCount(-1);
                    var count2 = itemList2[idx2].SetItemCount(-1);

                    mixItem1 = itemList1[idx1].itemPosition;
                    mixItem2 = itemList2[idx2].itemPosition;
                    mixItemCount1 = itemList1[idx1].itemCount;
                    mixItemCount2 = itemList2[idx2].itemCount;

                    if (count1 <= 0)
                    {
                        _itemPosition.Push(itemList1[idx1].itemPosition);
                        itemList1.Remove(idx1);
                    }
                    if (count2 <= 0)
                    {
                        _itemPosition.Push(itemList2[idx2].itemPosition);
                        itemList2.Remove(idx2);
                    }

                }
                else
                    mix = false;
            }
            else
            {
                _ItemCombination.Combination(_ItemCombination.Createtype, _ItemCombination.itemnumber);
                if (_ItemCombination.Createbool == true)
                {
                    Debug.Log($"[{itemList1[idx1].item.ItemName}] 아이템과 [{itemList2[idx2].item.ItemName}]아이템을 사용하였습니다.");
                    mix = true;

                    var count1 = itemList1[idx1].SetItemCount(-1);
                    var count2 = itemList2[idx2].SetItemCount(-1);

                    mixItem1 = itemList1[idx1].itemPosition;
                    mixItem2 = itemList2[idx2].itemPosition;
                    mixItemCount1 = itemList1[idx1].itemCount;
                    mixItemCount2 = itemList2[idx2].itemCount;

                    if (count1 <= 0)
                    {
                        _itemPosition.Push(itemList1[idx1].itemPosition);
                        itemList1.Remove(idx1);
                    }
                    if (count2 <= 0)
                    {
                        _itemPosition.Push(itemList2[idx2].itemPosition);
                        itemList2.Remove(idx2);
                    }

                }
                else
                    mix = false;
            }
        }
    }

    /// <summary> 아이템 사용 </summary>
    public void UseItem(ItemType type, int idx)
    {
        var itemList = Items[type];

        if (type == ItemType.Weapon || type == ItemType.ETC)
        {
            if (itemList[idx].item != null)
            {
                if (_curIdx < 10)
                {
                    Debug.Log($"{Items[curUse][_curIdx].item.ItemName} 장착 해제");
                    Items[curUse][_curIdx].use = false;
                    _preIdx = _curIdx;
                    preUse = curUse;
                }

                itemList[idx].use = true;
                _curIdx = idx;
                curUse = type;
                test = itemList[idx].itemCount;
                Equip = true;
                Debug.Log($"[{itemList[idx].item.ItemName}] 아이템을 장착하였습니다.");
            }
            else
                Debug.Log($"텅");
        }
        else
        {
            if (itemList[idx].item != null)
            {
                Debug.Log($"[{itemList[idx].item.ItemName}] 아이템을 사용하였습니다.");
                Equip = false;
                var count = itemList[idx].SetItemCount(-1);
                test = itemList[idx].itemCount;
                //OnItemUseHandler?.Invoke(itemList[idx]);

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

    public void DragItem(ItemType type, int idx)
    {
        dragItem = Items[type][idx];
    }

    public void DropItem(int posNum)
    {
        tempPosNum = dragItem.itemPosition;
        dragItem.itemPosition = posNum;
        //Debug.Log($"{dragItem.item.ItemName}의 위치를 {posNum}번 칸으로");
    }
    public void TempItem(ItemType type, int idx)
    {
        tempItem = Items[type][idx];
    }
    public void ChanheTempItem()
    {
        tempItem.itemPosition = tempPosNum;
        //Debug.Log($"{tempItem.item.ItemName}의 위치를 {tempPosNum}번 칸으로");
    }

    public int CurSlot()
    {
        return test;
    }
}