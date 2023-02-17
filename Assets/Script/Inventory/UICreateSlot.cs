using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UICreateSlot : MonoBehaviour, IPointerClickHandler, IDropHandler
{
    public ItemBase _item;   //재료 슬롯 아이템

    /// <summary> Workroom 재료 슬롯의 아이템 이미지 </summary>
    [SerializeField] private Image imgItem;

    // Start is called before the first frame update
    void Start()
    {
        _item = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            ClearSelected();
            imgItem.color = new Color(1, 1, 1, 0);
            Debug.Log($"재료 선택 취소");
        }
    }

    public void ClearSelected()
    {
        _item = null;
        imgItem.color = new Color(1, 1, 1, 0);
    }

    public void OnDrop(PointerEventData eventData)
    {
        SelectItem();
    }

    public void SelectItem()
    {
        _item = DragSlot.instance.dragSlot.Item;
        imgItem.sprite = _item.ItemImage;
        imgItem.color = new Color(1, 1, 1, 1);
        Debug.Log($"재료 선택: {_item.ItemName}");
    }
}
