using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 아이템 유형 </summary>
public enum ItemType
{
    /// <summary> 아이템 유형: 무기 </summary>
    Weapon,
    /// <summary> 아이템 유형: 소모품 </summary>
    Consumable,
    /// <summary> 아이템 유형: 재료 </summary>
    Ingredient,
    /// <summary> 아이템 유형: 기타 </summary>
    ETC,
}

/// <summary> 아이템 데이터 </summary>
[Serializable]
public class ItemBase
{
    [SerializeField] private int itemIdx;            //아이템 idx
    [SerializeField] private string itemName;        //아이템 이름
    [SerializeField] private Sprite itemImage;       //아이템 Sprite
    [SerializeField] private GameObject itemPrefab;  //아이템 Prefab

    private ItemType _ItemType;                      //아이템 유형


    /// <summary> 아이템 Idx (get) </summary>
    public int ItemIdx => itemIdx;

    /// <summary> 아이템 이름 (get) </summary>
    public string ItemName => itemName;

    /// <summary> 아이템 이미지 (get) </summary>
    public Sprite ItemImage => itemImage;

    /// <summary> 아이템 프리팹 (get) </summary>
    public GameObject ItemPrefab => itemPrefab;

    /// <summary> 아이템 유형 (get) </summary>
    public ItemType Type
    {
        get => _ItemType;
        set => _ItemType = value;
    }
}

//프로젝트 창에서 파일 생성 가능
//기본 파일명:ItemList
//생성하는 법: 우클릭 > Create > Scriptable Object > ItemList
[CreateAssetMenu(fileName = "ItemList", menuName = "Scriptable Object/ItemList")]
public class ScriptableItem : ScriptableObject
{
    /// <summary> 무기 아이템 리스트 </summary>
    [SerializeField] private List<ItemBase> weaponList;
    /// <summary> 소모품 아이템 리스트 </summary>
    [SerializeField] private List<ItemBase> consumableList;
    /// <summary> 재료 아이템 리스트 </summary>
    [SerializeField] private List<ItemBase> ingredientList;
    /// <summary> 기타 아이템 리스트 </summary>
    [SerializeField] private List<ItemBase> etcList;

    /// <summary> 아이템 리스트 묶음 </summary>
    private List<List<ItemBase>> _itemList = null;

    private int ItemListIdx;

    /// <summary> 아이템 리스트 묶음 (get) </summary>
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

    /// <summary> 아이템 리스트 </summary>
    public List<ItemBase> GetItemList(ItemType type)
    {
        return ItemList[(int)type];
    }

    /// <summary> 아이템 리스트 묶음에 해당 아이템 리스트를 추가 </summary>
    private void SetItemTypeAndRegisterToList(List<ItemBase> items, ItemType type)
    {
        SetItemType(items, type);
        _itemList.Add(items);
    }

    /// <summary> 해당 아이템 리스트의 모든 아이템 유형을 받아온 type으로 고정 </summary>
    private void SetItemType(List<ItemBase> items, ItemType type)
    {
        for (var i = 0; i < items.Count; i++)
        {
            items[i].Type = type;
        }
    }
}