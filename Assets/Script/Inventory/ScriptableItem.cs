using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary> ������ ���� </summary>
public enum ItemType
{
    #region ������ ����: ����
    /// <summary> ������ ����: ���� </summary>
    #endregion
    Weapon,

    #region ������ ����: �Ҹ�ǰ
    /// <summary> ������ ����: �Ҹ�ǰ </summary>
    #endregion
    Consumable,

    #region ������ ����: ���
    /// <summary> ������ ����: ��� </summary>
    #endregion
    Ingredient,

    #region ������ ����: ��Ÿ
    /// <summary> ������ ����: ��Ÿ </summary>
    #endregion
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

    private ItemType _ItemType;    //������ ����

    #region ������ Idx (get)
    /// <summary> ������ Idx (get) </summary>
    #endregion
    public int ItemIdx => itemIdx;

    #region ������ �̸� (get)
    /// <summary> ������ �̸� (get) </summary>
    #endregion
    public string ItemName => itemName;

    #region ������ �̹��� (get)
    /// <summary> ������ �̹��� (get) </summary>
    #endregion
    public Sprite ItemImage => itemImage;

    #region ������ ������ (get)
    /// <summary> ������ ������ (get) </summary>
    #endregion
    public GameObject ItemPrefab => itemPrefab;

    #region ������ ���� (get)
    /// <summary> ������ ���� (get) </summary>
    #endregion
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
                                                             #region ���� ������ ����Ʈ
                                                             /// <summary> ���� ������ ����Ʈ </summary>
                                                             #endregion
    [SerializeField] private List<ItemBase> weaponList;
                                                             #region �Ҹ�ǰ ������ ����Ʈ
                                                             /// <summary> �Ҹ�ǰ ������ ����Ʈ </summary>
                                                             #endregion
    [SerializeField] private List<ItemBase> consumableList;
                                                             #region ��� ������ ����Ʈ
                                                             /// <summary> ��� ������ ����Ʈ </summary>
                                                             #endregion
    [SerializeField] private List<ItemBase> ingredientList;
                                                             #region ��Ÿ ������ ����Ʈ
                                                             /// <summary> ��Ÿ ������ ����Ʈ </summary>
                                                             #endregion
    [SerializeField] private List<ItemBase> etcList;

    #region ������ ����Ʈ ����
    /// <summary> ������ ����Ʈ ���� </summary>
    #endregion
    private List<List<ItemBase>> _itemList = null;

    private int ItemListIdx;

    #region ������ ����Ʈ ���� (get)
    /// <summary> ������ ����Ʈ ���� (get) </summary>
    #endregion
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