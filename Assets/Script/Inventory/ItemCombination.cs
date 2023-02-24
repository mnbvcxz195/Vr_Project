using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombinationReciepeManager
{
    public static Dictionary<int, CombinationReciepe> _reciepeList = new Dictionary<int, CombinationReciepe>()
    {
        { 0, new CombinationReciepe(14, 15,ItemType.Weapon,1) },
        { 1, new CombinationReciepe(16, 17 ,ItemType.Consumable,0) },
        { 2, new CombinationReciepe(15, 18 ,ItemType.Ingredient,9) },
        { 3, new CombinationReciepe(10, 11 ,ItemType.ETC,0) },
        { 4, new CombinationReciepe(12, 13 ,ItemType.ETC,1) },


    };

    public CombinationReciepe GetReciepe(int itemCode)
    {
        if (_reciepeList.ContainsKey(itemCode))
            return _reciepeList[itemCode];

        return null;
    }
}

public class CombinationReciepe
{
    public int item1;
    public int item2;
    public ItemType type;
    public int CombinationIdx;
    public CombinationReciepe(int item1, int item2, ItemType type, int CombinationIdx)
    {
        this.item1 = item1;
        this.item2 = item2;
        this.type = type;
        this.CombinationIdx = CombinationIdx;

    }
}
public class ItemCombination : MonoBehaviour
{
    [SerializeField] ScriptableItem _ScriptableItem;
    [SerializeField] UICreateSlot[] _UICreateSlot;
    Inventory _Inventory;
    ItemBase _ItemBase;
    InventoryManager _InventoryManager;
    public ItemType Createtype;
    public int itemnumber;
    public bool Createbool = false;

    CombinationReciepeManager _combinationR = new CombinationReciepeManager();



    void Start()
    {
        _Inventory = GameObject.FindWithTag("Inventory").GetComponent<Inventory>();
        _InventoryManager = InventoryManager.GetInstance();
        _InventoryManager.AcquireItem(_ScriptableItem.GetItemList(ItemType.Weapon)[0]);
    }
    public void Combination(ItemType type, int code)
    {
        for (int i = 0; i < CombinationReciepeManager._reciepeList.Count; i++)
        {
            Createbool = false;

            var curRecp = _combinationR.GetReciepe(i);
            if (CheckItem(curRecp.item1) && CheckItem(curRecp.item2))
            {
                Createbool = true;
                type = curRecp.type;
                itemnumber = curRecp.CombinationIdx;
                _ItemBase = _ScriptableItem.GetItemList(type)[itemnumber];
                InventoryManager.GetInstance().AcquireItem(_ItemBase, 1);
                return;
            }
            else
            {
                Createbool = false;
            }
        }
    }

    public bool CheckItem(int code)
    {
        for (int i = 0; i < _UICreateSlot.Length; i++)
        {
            if (_UICreateSlot[i]._item.ItemIdx == code)
            {
                return true;
            }

        }
        return false;

    }
}

