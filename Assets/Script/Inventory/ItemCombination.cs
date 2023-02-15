using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombinationReciepeManager
{
    public static Dictionary<int, CombinationReciepe> _reciepeList = new Dictionary<int, CombinationReciepe>() //레시피리스트를 딕셔너리로 만든다(키값은 나오는 아이템의 인덱스 / 벨류는 변수들)
    {
        { 0, new CombinationReciepe(4, 5,ItemType.Weapon,1) },
        { 1, new CombinationReciepe(6, 7 ,ItemType.Consumable,0) },
        { 2, new CombinationReciepe(5, 5 ,ItemType.Consumable,1) },
        { 3, new CombinationReciepe(4, 4 ,ItemType.ETC,0) },
        { 4, new CombinationReciepe(6, 6 ,ItemType.ETC,1) },
        { 5, new CombinationReciepe(7, 7 ,ItemType.ETC,2) }


    };

    public CombinationReciepe GetReciepe(int itemCode)
    {
        if (_reciepeList.ContainsKey(itemCode))//(딕셔너리)레서피리스트의 콘타인키(아이템코드)가 있다면 리턴으로 리스트의 벨류값을 리턴가능
            return _reciepeList[itemCode];

        return null;//아니라면 끝
    }
}

public class CombinationReciepe//클래스로 조합아이템의 인덱스를 받을 인트생성
{
    public int item1;
    public int item2;
    public ItemType type;
    public int CombinationIdx;
    public CombinationReciepe(int item1, int item2, ItemType type, int CombinationIdx)//벨류를 2개쓰기에 (int(key),int(value),int(value))
    {
        this.item1 = item1;
        this.item2 = item2;
        this.type = type;
        this.CombinationIdx = CombinationIdx;

    }
}
public class ItemCombination : MonoBehaviour
{
    public ScriptableItem _ScriptableItem;
    public int itemIdx;
    //bool item1 = false;
    //int positionItem1;
    //int positionItem2;
    [SerializeField] Button Combinationbnt1;
    [SerializeField] Button Combinationbnt2;
    //[SerializeField] public Dictionary<int, Slot> _slots = new Dictionary<int, Slot>(); // slot가 딕셔너리라면
    [SerializeField] UICreateSlot[] _UICreateSlot;
    [SerializeField] Button bb;
    Inventory _Inventory;
    ItemBase _ItemBase;
    InventoryManager _InventoryManager;
    ItemType testtype;
    int itemnumber;

    CombinationReciepeManager _combinationR = new CombinationReciepeManager();



    void Start()
    {
        _Inventory = GameObject.FindWithTag("Inventory").GetComponent<Inventory>();

        bb.onClick.AddListener(() =>
        {
            Combination(testtype, itemnumber);
        });
    }
    public void Combination(ItemType type, int code)
    {
        for (int i = 0; i < CombinationReciepeManager._reciepeList.Count; i++)
        {
            var curRecp = _combinationR.GetReciepe(i);

            if (CheckItem(curRecp.item1) && CheckItem(curRecp.item2)) // UseItem함수를 조합전용함수로 추가생성해야할듯?
            {
                type = curRecp.type;
                itemnumber = curRecp.CombinationIdx;
                _ItemBase = _ScriptableItem.GetItemList(type)[itemnumber];
                InventoryManager.GetInstance().AcquireItem(_ItemBase, 1);
                Debug.Log("조합 성공");
                return;
            }
            else
            {
                Debug.Log("조합 실패");
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


