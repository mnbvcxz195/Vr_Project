using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    private ItemBase _item;                            //ȹ���� ������
    private int _itemCount;                             //ȹ���� ������ ����
    [SerializeField] private GameObject goItemCount;
    [SerializeField] private Text txtItemCount;

    /// <summary> �κ��丮 ������ ������ �̹��� </summary>
    [SerializeField] private Image imgItem;

    /// <summary> ������ ��� ��ư </summary>
    public Button btnUsed;

    /// <summary> ȹ���� ������ (get) </summary>
    public ItemBase Item
    {
        get => _item;
        set => _item = value;
    }

    /// <summary> ȹ���� ������ ���� (get) </summary>
    public int ItemCount
    {
        get => _itemCount;
        set => _itemCount = value;
    }

    /// <summary> ���Կ� ������ �߰� </summary>
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

    /// <summary> ������ ���� ���� </summary>
    public void SetItemCount(int _count)
    {
        ItemCount += _count;
        txtItemCount.text = $"{ItemCount}";

        if (ItemCount <= 0)
            ClearSlot();
    }

    /// <summary> ���� ���� </summary>
    public void ClearSlot()
    {
        ItemCount = 0;
        Item = null;
        imgItem.color = new Color(1, 1, 1, 0);
        goItemCount.SetActive(false);
        Debug.Log($"��");
    }
}