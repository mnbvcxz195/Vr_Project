using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary> 아이템 유형 </summary>
public enum ItemType
{
    #region 아이템 유형: 무기
    /// <summary> 아이템 유형: 무기 </summary>
    #endregion
    Weapon,

    #region 아이템 유형: 소모품
    /// <summary> 아이템 유형: 소모품 </summary>
    #endregion
    Consumable,

    #region 아이템 유형: 재료
    /// <summary> 아이템 유형: 재료 </summary>
    #endregion
    Ingredient,

    #region 아이템 유형: 기타
    /// <summary> 아이템 유형: 기타 </summary>
    #endregion
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

    private ItemType _ItemType;    //아이템 유형

    #region 아이템 Idx (get)
    /// <summary> 아이템 Idx (get) </summary>
    #endregion
    public int ItemIdx => itemIdx;

    #region 아이템 이름 (get)
    /// <summary> 아이템 이름 (get) </summary>
    #endregion
    public string ItemName => itemName;

    #region 아이템 이미지 (get)
    /// <summary> 아이템 이미지 (get) </summary>
    #endregion
    public Sprite ItemImage => itemImage;

    #region 아이템 프리팹 (get)
    /// <summary> 아이템 프리팹 (get) </summary>
    #endregion
    public GameObject ItemPrefab => itemPrefab;

    #region 아이템 유형 (get)
    /// <summary> 아이템 유형 (get) </summary>
    #endregion
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
                                                             #region 무기 아이템 리스트
                                                             /// <summary> 무기 아이템 리스트 </summary>
                                                             #endregion
    [SerializeField] private List<ItemBase> weaponList;
                                                             #region 소모품 아이템 리스트
                                                             /// <summary> 소모품 아이템 리스트 </summary>
                                                             #endregion
    [SerializeField] private List<ItemBase> consumableList;
                                                             #region 재료 아이템 리스트
                                                             /// <summary> 재료 아이템 리스트 </summary>
                                                             #endregion
    [SerializeField] private List<ItemBase> ingredientList;
                                                             #region 기타 아이템 리스트
                                                             /// <summary> 기타 아이템 리스트 </summary>
                                                             #endregion
    [SerializeField] private List<ItemBase> etcList;

    #region 아이템 리스트 묶음
    /// <summary> 아이템 리스트 묶음 </summary>
    #endregion
    private List<List<ItemBase>> _itemList = null;

    private int ItemListIdx;

    #region 아이템 리스트 묶음 (get)
    /// <summary> 아이템 리스트 묶음 (get) </summary>
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