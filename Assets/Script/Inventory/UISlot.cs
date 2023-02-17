using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;

public class UISlot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    private ItemBase _item;   //ȹ���� ������
    private int _itemCount;   //ȹ���� ������ ����
    [SerializeField] private GameObject goItemCount;
    [SerializeField] private Text txtItemCount;
    public bool dropItem = false;

    #region �κ��丮 ������ ������ �̹���
    /// <summary> �κ��丮 ������ ������ �̹��� </summary>
    #endregion
    [SerializeField] private Image imgItem;

    #region ȹ���� ������ (get)
    /// <summary> ȹ���� ������ (get) </summary>
    #endregion
    public ItemBase Item
    {
        get => _item;
        set => _item = value;
    }

    #region ȹ���� ������ ���� (get)
    /// <summary> ȹ���� ������ ���� (get) </summary>
    #endregion
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
        ItemCount = _count;
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
    }

    public void SetEquipColor(float color)
    {
        imgItem.color = new Color(color, color, color, 1);
    }

    private InventoryManager _inventoryManager;
    Inventory inventory;

    void Awake()
    {
        _inventoryManager = InventoryManager.GetInstance();
        inventory = GameObject.FindWithTag("Inventory").GetComponent<Inventory>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (Item == null)
            {
                Debug.Log($"�� ����");
                return;
            }

            ItemType itemType = Item.Type;
            int itemIdx = Item.ItemIdx;
            _inventoryManager.UseItem(itemType, itemIdx);

            SetItemCount(_inventoryManager.CurSlot());
            inventory.EquipItem();
        }
    }

    /// <summary> �巡�� ���� </summary>
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Item == null)
            return;

        DragSlot.instance.dragSlot = this;
        DragSlot.instance.DragSetImage(imgItem);
        DragSlot.instance.transform.position = eventData.position;
        
        _inventoryManager.DragItem(Item.Type, Item.ItemIdx);
    }

    /// <summary> �巡�� �� </summary>
    public void OnDrag(PointerEventData eventData)
    {
        if (Item != null)
            DragSlot.instance.transform.position = eventData.position;

    }

    /// <summary> �巡�� �� </summary>
    public void OnEndDrag(PointerEventData eventData)
    {
        DragSlot.instance.SetColor(0);
        DragSlot.instance.dragSlot = null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (DragSlot.instance.dragSlot != null)
        {
            Debug.Log($"���");
            ChangeSlot();
        }
    }

    private void ChangeSlot()
    {
        ItemBase _tempItem = _item;
        int _tempItemCount = _itemCount;
        dropItem = true;

        if(_item != null)
            _inventoryManager.TempItem(_tempItem.Type, _tempItem.ItemIdx);

        AddItem(DragSlot.instance.dragSlot.Item, DragSlot.instance.dragSlot.ItemCount);
        _inventoryManager.DropItem(inventory.ChangeSlotIdx());

        if (_tempItem != null)
        {
            DragSlot.instance.dragSlot.AddItem(_tempItem, _tempItemCount);
            _inventoryManager.ChanheTempItem();
        }
        else
            DragSlot.instance.dragSlot.ClearSlot();
    }
}