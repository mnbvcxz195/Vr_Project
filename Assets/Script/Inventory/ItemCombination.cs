using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombinationReciepeManager
{
    public static Dictionary<int, CombinationReciepe> _reciepeList = new Dictionary<int, CombinationReciepe>() //�����Ǹ���Ʈ�� ��ųʸ��� �����(Ű���� ������ �������� �ε���? / ������ ������ �� �������� �ε���)
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
        if (_reciepeList.ContainsKey(itemCode))//(��ųʸ�)�����Ǹ���Ʈ�� ��Ÿ��Ű(�������ڵ�)�� �ִٸ� �������� ����Ʈ�� Ű���� ����?
        {
            return _reciepeList[itemCode];
        }

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
        _Inventory = GameObject.FindWithTag("Inventory").GetComponent<Inventory>();//�κ��丮 ã���ֱ�

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
                Debug.Log("���� ����");
                return;
            }
            else
            {
                Debug.Log("���� ����");
            }
        }
    }

    public bool CheckItem(int code)// ���� return���� ������ �ٽõ��Ƽ� ���� ö+ö ���� ������ ��Ḧ������ 1���� �־���յ�
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

