using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    private ItemBase _item;                            //È¹µæÇÑ ¾ÆÀÌÅÛ
    private int itemCount;                             //È¹µæÇÑ ¾ÆÀÌÅÛ °³¼ö
    [SerializeField] private GameObject goItemCount;
    [SerializeField] private Text txtItemCount;

    [SerializeField] private Image imgItem;            //È¹µæÇÑ ¾ÆÀÌÅÛÀÇ ÀÌ¹ÌÁö

    public Button btnUsed;

    public int ItemCount                               //itemCount get set
    {
        get => itemCount;
        set => itemCount = value;
    }

    public ItemBase Item => _item;

    public void AddItem(ItemBase item, int _count = 1)
    {
        _item = item;
        ItemCount = _count;

        imgItem.sprite = _item.ItemImage;
        imgItem.color = new Color(1, 1, 1, 1);

        if (_item.Type == ItemType.Ingredient || _item.Type == ItemType.Consumable)
        {
            goItemCount.SetActive(true);
            txtItemCount.text = $"{itemCount}";
        }
        else
        
        {
            goItemCount.SetActive(false);
        }
    }

    public void SetItemCount(int _count)
    {
        itemCount += _count;
        txtItemCount.text = $"{itemCount}";

        if (itemCount <= 0)
            ClearSlot();
    }

    public void ClearSlot()
    {
        itemCount = 0;
        _item = null;
        imgItem.color = new Color(1, 1, 1, 0);
        goItemCount.SetActive(false);
        Debug.Log($"ÅÖ");
    }
}