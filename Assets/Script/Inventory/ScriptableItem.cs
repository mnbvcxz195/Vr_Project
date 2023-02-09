using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템 유형
/// </summary>
public enum ItemType
{
    Weapon,           //무기 Type
    Consumable,       //소모품 Type(포션 등)
    Ingredient,       //재료 Type
    ETC,              //기타 Type(제작된 아이템이나 보물 등)
}

/// <summary>
/// 아이템 데이터
/// </summary>
[Serializable]
public class ItemBase
{
    [SerializeField] private int itemIdx;            //아이템 idx
    [SerializeField] private string itemName;        //아이템 이름
    [SerializeField] private Sprite itemImage;       //아이템 Sprite
    [SerializeField] private GameObject itemPrefab;  //아이템 Prefab

    private ItemType _ItemType;                      //아이템 유형

    public int ItemIdx => itemIdx;                   //아이템 idx get
    public string ItemName => itemName;              //아이템 이름 get
    public Sprite ItemImage => itemImage;            //아이템 Sprite get
    public GameObject ItemPrefab => itemPrefab;      //아이템 Prefab get

    public ItemType Type                             //아이템 유형 get set
    {
        get => _ItemType;
        set => _ItemType = value;
    }
}

//프로젝트 창에서 파일 생성 가능
//기본이름:ItemList
//우클릭 > Create > Scriptable Object > ItemList
[CreateAssetMenu(fileName = "ItemList", menuName = "Scriptable Object/ItemList")]
public class ScriptableItem : ScriptableObject
{
    [SerializeField] private List<ItemBase> weaponList;      //무기 리스트
    [SerializeField] private List<ItemBase> consumableList;  //소모품 리스트
    [SerializeField] private List<ItemBase> ingredientList;  //재료 리스트
    [SerializeField] private List<ItemBase> etcList;         //기타 리스트

    private List<List<ItemBase>> _itemList = null;           //아이템 리스트 묶음

    private int ItemListIdx;

    public List<List<ItemBase>> ItemList                     //아이템 리스트 묶음 get
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
                //아이템 리스트 묶음에 (무기, 소모품, 재료, 기타 리스트 추가)
            }
            return _itemList;
        }
    }

    public void SetItemList(int idx)     //아이템 리스트 Setter
    {
        ItemListIdx = idx;
    }

    public List<ItemBase> GetItemList()  //아이템 리스트 Getter
    {
        return ItemList[ItemListIdx];
    }

    /// <summary>
    /// 아이템 리스트 묶음에 해당 아이템 리스트를 추가
    /// </summary>
    /// <param name="items"></param>
    /// <param name="type"></param>
    private void SetItemTypeAndRegisterToList(List<ItemBase> items, ItemType type)
    {
        SetItemType(items, type);
        _itemList.Add(items);
    }

    /// <summary>
    /// 해당 아이템 리스트의 모든 아이템 유형을 받아온 type으로 고정
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