using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ ����
/// </summary>
public enum ItemType
{
    Weapon,           //���� Type
    Consumable,       //�Ҹ�ǰ Type(���� ��)
    Ingredient,       //��� Type
    ETC,              //��Ÿ Type(���۵� �������̳� ���� ��)
}

/// <summary>
/// ������ ������
/// </summary>
[Serializable]
public class ItemBase
{
    [SerializeField] private int itemIdx;            //������ idx
    [SerializeField] private string itemName;        //������ �̸�
    [SerializeField] private Sprite itemImage;       //������ Sprite
    [SerializeField] private GameObject itemPrefab;  //������ Prefab

    private ItemType _ItemType;                      //������ ����

    public int ItemIdx => itemIdx;                   //������ idx get
    public string ItemName => itemName;              //������ �̸� get
    public Sprite ItemImage => itemImage;            //������ Sprite get
    public GameObject ItemPrefab => itemPrefab;      //������ Prefab get

    public ItemType Type                             //������ ���� get set
    {
        get => _ItemType;
        set => _ItemType = value;
    }
}

//������Ʈ â���� ���� ���� ����
//�⺻�̸�:ItemList
//��Ŭ�� > Create > Scriptable Object > ItemList
[CreateAssetMenu(fileName = "ItemList", menuName = "Scriptable Object/ItemList")]
public class ScriptableItem : ScriptableObject
{
    [SerializeField] private List<ItemBase> weaponList;      //���� ����Ʈ
    [SerializeField] private List<ItemBase> consumableList;  //�Ҹ�ǰ ����Ʈ
    [SerializeField] private List<ItemBase> ingredientList;  //��� ����Ʈ
    [SerializeField] private List<ItemBase> etcList;         //��Ÿ ����Ʈ

    private List<List<ItemBase>> _itemList = null;           //������ ����Ʈ ����

    private int ItemListIdx;

    public List<List<ItemBase>> ItemList                     //������ ����Ʈ ���� get
    {
        get
        {
            if (_itemList == null)
            {
                _itemList = new List<List<ItemBase>>();
                SetItemTypeAndRegisterToList(weaponList, ItemType.Weapon);
                SetItemTypeAndRegisterToList(consumableList, ItemType.Consumable);
                SetItemTypeAndRegisterToList(ingredientList, ItemType.Ingredient);
                SetItemTypeAndRegisterToList(etcList, ItemType.ETC);
                //������ ����Ʈ ������ (����, �Ҹ�ǰ, ���, ��Ÿ ����Ʈ �߰�)
            }
            return _itemList;
        }
    }

    public void SetItemList(int idx)     //������ ����Ʈ Setter
    {
        ItemListIdx = idx;
    }

    public List<ItemBase> GetItemList()  //������ ����Ʈ Getter
    {
        return ItemList[ItemListIdx];
    }

    /// <summary>
    /// ������ ����Ʈ ������ �ش� ������ ����Ʈ�� �߰�
    /// </summary>
    /// <param name="items"></param>
    /// <param name="type"></param>
    private void SetItemTypeAndRegisterToList(List<ItemBase> items, ItemType type)
    {
        SetItemType(items, type);
        _itemList.Add(items);
    }

    /// <summary>
    /// �ش� ������ ����Ʈ�� ��� ������ ������ �޾ƿ� type���� ����
    /// </summary>
    /// <param name="items"></param>
    /// <param name="type"></param>
    private void SetItemType(List<ItemBase> items, ItemType type)
    {
        for (var i = 0; i < items.Count; i++)
        {
            items[i].Type = type;
        }
    }
}