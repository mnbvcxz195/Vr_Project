using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombinationReciepeManager
{
    public static Dictionary<int, CombinationReciepe> _reciepeList = new Dictionary<int, CombinationReciepe>() //레시피리스트를 딕셔너리로 만든다(키값은 나오는 아이템의 인덱스? / 벨류는 조합이 될 아이템의 인덱스)
    {
        { 0, new CombinationReciepe(14,15,ItemType.Weapon,1) },
        { 1, new CombinationReciepe(16,17,ItemType.Consumable,0) },
        { 2, new CombinationReciepe(15,15,ItemType.Consumable,1) },
        { 3, new CombinationReciepe(14,14,ItemType.ETC,0) },
        { 4, new CombinationReciepe(16,16,ItemType.ETC,1) },
        { 5, new CombinationReciepe(17,17,ItemType.ETC,2) }
    };

    public CombinationReciepe GetReciepe(int itemCode)
    {
        if (_reciepeList.ContainsKey(itemCode))//(딕셔너리)레서피리스트의 콘타인키(아이템코드)가 있다면 리턴으로 리스트의 키값을 리턴?
        {
            return _reciepeList[itemCode];
        }

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
    [SerializeField] UICreateSlot[] _UICreateSlot;
    [SerializeField] Button Create;
    Inventory _Inventory;
    public ItemBase _ItemBase;
    public ItemType type;
    public int itemnumber;
    public bool testtt = false;
    public CombinationReciepeManager _combinationR = new CombinationReciepeManager();



    void Start()
    {
        _Inventory = GameObject.FindWithTag("Inventory").GetComponent<Inventory>();//인벤토리 찾아주기

        //Create.onClick.AddListener(() =>
        //{
        //    Combination(type, itemnumber);
        //});
    }
    public void Combination(ItemType type, int code)
    {
        testtt = false;

        for (int i = 0; i < CombinationReciepeManager._reciepeList.Count; i++)
        {
            Debug.Log(i);
            var curRecp = _combinationR.GetReciepe(i);
            if (CheckItem(curRecp.item1) && CheckItem(curRecp.item2))
            {
                testtt = true;
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

    public bool CheckItem(int code)// 문제 return으로 나가서 다시돌아서 같은 철+철 같은 동일한 재료를가지면 1개만 있어도조합됨
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

