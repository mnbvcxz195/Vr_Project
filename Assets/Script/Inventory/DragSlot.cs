using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragSlot : MonoBehaviour
{
    static public DragSlot instance;
    public UISlot dragSlot;

    [SerializeField] private Image imgItem;

    void Start()
    {
        instance = this;
    }

    public void DragSetImage(Image _itemImage)
    {
        imgItem.sprite = _itemImage.sprite;
        SetColor(0.5f);
    }

    public void SetColor(float a)
    {
        imgItem.color = new Color(1, 1, 1, a);
    }
}
