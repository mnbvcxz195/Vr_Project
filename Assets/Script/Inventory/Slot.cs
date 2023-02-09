using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    private ItemBase _item;                            //획득한 아이템
    private int _itemCount;                             //획득한 아이템 개수
    [SerializeField] private GameObject goItemCount;
    [SerializeField] private Text txtItemCount;

    /// <summary> 인벤토리 슬롯의 아이템 이미지 </summary>
    [SerializeField] private Image imgItem;

    /// <summary> 아이템 사용 버튼 </summary>
    public Button btnUsed;

    /// <summary> 획득한 아이템 (get) </summary>
    public ItemBase Item
    {
        get => _item;
        set => _item = value;
    }

    /// <summary> 획득한 아이템 개수 (get) </summary>
    public int ItemCount
    {
        get => _itemCount;
        set => _itemCount = value;
    }

    /// <summary> 슬롯에 아이템 추가 </summary>
    public void AddItem(ItemBase item, int _count = 1)
    {
        Item = item;
        ItemCount = _count;

        imgItem.sprite = Item.ItemImage;
        imgItem.color = new Color(1, 1, 1, 1);

        if (Item.Type == ItemType.Ingredient || Item.Type == ItemType.Consumable)
        {
            goItemCount.SetActive(true);
            txtItemCount.text = $"{ItemCount}";
        }
        else
        
        {
            goItemCount.SetActive(false);
        }
    }

    /// <summary> 아이템 수량 관리 </summary>
    public void SetItemCount(int _count)
    {
        ItemCount += _count;
        txtItemCount.text = $"{ItemCount}";

        if (ItemCount <= 0)
            ClearSlot();
    }

    /// <summary> 슬롯 비우기 </summary>
    public void ClearSlot()
    {
        ItemCount = 0;
        Item = null;
        imgItem.color = new Color(1, 1, 1, 0);
        goItemCount.SetActive(false);
        Debug.Log($"텅");
    }
}