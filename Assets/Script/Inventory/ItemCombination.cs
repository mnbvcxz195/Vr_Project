using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombinationReciepeManager
{
    public static Dictionary<int, CombinationReciepe> _reciepeList = new Dictionary<int, CombinationReciepe>() //�����Ǹ���Ʈ�� ��ųʸ��� �����(Ű���� ������ �������� �ε��� / ������ ������)
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
        if (_reciepeList.ContainsKey(itemCode))//(��ųʸ�)�����Ǹ���Ʈ�� ��Ÿ��Ű(�������ڵ�)�� �ִٸ� �������� ����Ʈ�� �������� ���ϰ���
            return _reciepeList[itemCode];

        return null;//�ƴ϶�� ��
    }
}

public class CombinationReciepe//Ŭ������ ���վ������� �ε����� ���� ��Ʈ����
{
    public int item1;
    public int item2;
    public ItemType type;
    public int CombinationIdx;
    public CombinationReciepe(int item1, int item2, ItemType type, int CombinationIdx)//������ 2�����⿡ (int(key),int(value),int(value))
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
    //[SerializeField] public Dictionary<int, Slot> _slots = new Dictionary<int, Slot>(); // slot�� ��ųʸ����
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

            if (CheckItem(curRecp.item1) && CheckItem(curRecp.item2)) // UseItem�Լ��� ���������Լ��� �߰������ؾ��ҵ�?
            {
                type = curRecp.type;
                itemnumber = curRecp.CombinationIdx;
                _ItemBase = _ScriptableItem.GetItemList(type)[itemnumber];
                InventoryManager.GetInstance().AcquireItem(_ItemBase, 1);
                Debug.Log("���� ����");
                return;
            }
            else
            {
                Debug.Log("���� ����");
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


