using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using static UnityEditor.Progress;

[Serializable]
public class Serialization<TKey, Tvalue> : ISerializationCallbackReceiver
{
    [SerializeField]
    List<TKey> keys;
    [SerializeField]
    List<Tvalue> values;

    Dictionary<TKey, Tvalue> target;
    public Dictionary<TKey, Tvalue> ToDictionary() { return target; }

    public Serialization()
    {
        keys = new List<TKey>();
        values = new List<Tvalue>();
    }

    public Serialization(Dictionary<TKey, Tvalue> target)
    {
        this.target = target;

        keys = new List<TKey>(target.Keys);
        values = new List<Tvalue>(target.Values);
    }

    public void Add(TKey key, Tvalue value)
    {
        keys.Add(key);
        values.Add(value);
    }

    public void OnBeforeSerialize()
    {
        Debug.Log("target : " + target);
        Debug.Log("key : " + keys);
        Debug.Log("value : " + values);
        Debug.Log("OnBeforeSerialize");
    }

    public void OnAfterDeserialize()
    {
        Debug.Log("target : " + target);
        Debug.Log("key : " + keys);
        Debug.Log("value : " + values);

        var count = Mathf.Min(keys.Count, values.Count);
        target = new Dictionary<TKey, Tvalue>(count);

        Debug.Log("target00 : " + target);
        for (var i = 0; i < count; ++i)
        {
            target.Add(keys[i], values[i]);
        }

        Debug.Log("OnAfterDeserialize");
    }

    public IEnumerator GetEnumerator()
    {
        throw new NotImplementedException();
    }
}

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
    public bool curEquip = true;

    public int keyCount;
    public bool key;
    public bool is1st;

    string path;
    string filename = "save.text";

    public ItemCombination _ItemCombination;
    ItemEffectDatabase itemEffectDatabase;

    public delegate void ItemUpdateHandler(Item item);
    public event ItemUpdateHandler OnItemAddHandler;
    public event ItemUpdateHandler OnItemUseHandler;
        public CombinationReciepeManager _combinationR = new CombinationReciepeManager();

    
    private void Awake()
    {
        path = Application.persistentDataPath + "/";

        Items = new Dictionary<ItemType, ItemList>();
        
        foreach (ItemType type in Enum.GetValues(typeof(ItemType)))
            Items.Add(type, new ItemList());

        //LoadData();

        for (int i = 0; i < _inventoryCount; i++)
            _itemPosition.Push(i);
        _ItemCombination = GameObject.FindWithTag("ItemCombination").GetComponent<ItemCombination>();//인벤토리 찾아주기
        itemEffectDatabase = GameObject.FindWithTag("Database").GetComponent<ItemEffectDatabase>();

        //SaveData();
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

        //SaveData();
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
        Debug.Log($"{type} | {idx}");
        var itemList = Items[type];

        if (type == ItemType.Weapon || type == ItemType.ETC)
        {
            if (itemList[idx].item != null)
            {
                if (_curIdx < 10)
                {
                    if (_curIdx == idx && curUse == type)
                    {
                        Debug.Log($"이미 장착 중인 아이템입니다.");

                    }
                    else
                    {
                        Debug.Log($"{Items[curUse][_curIdx].item.ItemName} 장착 해제");
                        Items[curUse][_curIdx].use = false;
                        curEquip = true;
                    }
                    _preIdx = _curIdx;
                    preUse = curUse;
                }

                Equip = true;
                itemList[idx].use = true;
                _curIdx = idx;
                curUse = type;
                test = itemList[idx].itemCount;

                if (curEquip)
                {
                    Debug.Log($"[{itemList[idx].item.ItemName}] 아이템을 장착하였습니다.");
                    itemEffectDatabase.UseItemEffect(type, itemList[idx].item.ItemName);
                    curEquip = false;
                }

                if (type == ItemType.ETC)
                {
                    var count = itemList[idx].SetItemCount(-1);
                    test = itemList[idx].itemCount;
                    itemList[idx].use = false;
                    Equip = false;
                    if (count <= 0)
                    {
                        _itemPosition.Push(itemList[idx].itemPosition);
                        itemList.Remove(idx);
                    }
                    _curIdx = 10;
                    curEquip = true;
                }
            }
            else
                Debug.Log($"텅");
        }
        else if (type == ItemType.Consumable)
        {
            if (itemList[idx].item != null)
            {

                itemEffectDatabase.UseConsumable(type, itemList[idx].item.ItemName);
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
        else
        {
            test = itemList[idx].itemCount;
            Debug.Log($"직접 사용할 수 없는 아이템입니다.");
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

    public void SaveData()
    {

        var serializedDictionary = new Serialization<ItemType, Serialization<int, Item>>();
        foreach (var item in Items)
            serializedDictionary.Add(item.Key, new Serialization<int, Item>(item.Value));

        var myItemData = JsonUtility.ToJson(serializedDictionary);
        File.WriteAllText(path + filename, myItemData);
        Debug.Log(myItemData);
        Debug.Log("저장");
    }

    public void LoadData()
    {
        var myItemData = File.ReadAllText(path + filename);
        var SerializedData = JsonUtility.FromJson<Serialization<ItemType, Serialization<int, Item>>>(myItemData).ToDictionary();

        Items = new Dictionary<ItemType, ItemList>();
        foreach (var data in SerializedData)
        {
            var itemDictionary = data.Value.ToDictionary();
            var itemList = new ItemList();
            foreach (var itemData in itemDictionary)
                itemList.Add(itemData.Key, itemData.Value);

            Items.Add(data.Key, itemList);
        }

        Debug.Log(myItemData);
        Debug.Log("로드");
    }
}
