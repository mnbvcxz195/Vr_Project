using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> ������ ���� </summary>
public enum ItemType
{
    /// <summary> ������ ����: ���� </summary>
    Weapon,
    /// <summary> ������ ����: �Ҹ�ǰ </summary>
    Consumable,
    /// <summary> ������ ����: ��� </summary>
    Ingredient,
    /// <summary> ������ ����: ��Ÿ </summary>
    ETC,
}

/// <summary> ������ ������ </summary>
[Serializable]
public class ItemBase
{
    [SerializeField] private int itemIdx;            //������ idx
    [SerializeField] private string itemName;        //������ �̸�
    [SerializeField] private Sprite itemImage;       //������ Sprite
    [SerializeField] private GameObject itemPrefab;  //������ Prefab

    private ItemType _ItemType;                      //������ ����


    /// <summary> ������ Idx (get) </summary>
    public int ItemIdx => itemIdx;

    /// <summary> ������ �̸� (get) </summary>
    public string ItemName => itemName;

    /// <summary> ������ �̹��� (get) </summary>
    public Sprite ItemImage => itemImage;

    /// <summary> ������ ������ (get) </summary>
    public GameObject ItemPrefab => itemPrefab;

    /// <summary> ������ ���� (get) </summary>
    public ItemType Type
    {
        get => _ItemType;
        set => _ItemType = value;
    }
}

//������Ʈ â���� ���� ���� ����
//�⺻ ���ϸ�:ItemList
//�����ϴ� ��: ��Ŭ�� > Create > Scriptable Object > ItemList
[CreateAssetMenu(fileName = "ItemList", menuName = "Scriptable Object/ItemList")]
public class ScriptableItem : ScriptableObject
{
    /// <summary> ���� ������ ����Ʈ </summary>
    [SerializeField] private List<ItemBase> weaponList;
    /// <summary> �Ҹ�ǰ ������ ����Ʈ </summary>
    [SerializeField] private List<ItemBase> consumableList;
    /// <summary> ��� ������ ����Ʈ </summary>
    [SerializeField] private List<ItemBase> ingredientList;
    /// <summary> ��Ÿ ������ ����Ʈ </summary>
    [SerializeField] private List<ItemBase> etcList;

    /// <summary> ������ ����Ʈ ���� </summary>
    private List<List<ItemBase>> _itemList = null;

    private int ItemListIdx;

    /// <summary> ������ ����Ʈ ���� (get) </summary>
    public List<List<ItemBase>> ItemList
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
            }
            return _itemList;
        }
    }

    /// <summary> ������ ����Ʈ </summary>
    public List<ItemBase> GetItemList(ItemType type)
    {
        return ItemList[(int)type];
    }

    /// <summary> ������ ����Ʈ ������ �ش� ������ ����Ʈ�� �߰� </summary>
    private void SetItemTypeAndRegisterToList(List<ItemBase> items, ItemType type)
    {
        SetItemType(items, type);
        _itemList.Add(items);
    }

    /// <summary> �ش� ������ ����Ʈ�� ��� ������ ������ �޾ƿ� type���� ���� </summary>
    private void SetItemType(List<ItemBase> items, ItemType type)
    {
        for (var i = 0; i < items.Count; i++)
        {
            items[i].Type = type;
        }
    }
}