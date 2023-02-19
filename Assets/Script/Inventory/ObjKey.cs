using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjKey : MonoBehaviour
{
    public ScriptableItem scriptableItem;
    #region ������ ����
    /// <summary> ������ ���� </summary>
    #endregion
    [SerializeField] private ItemType type;
    [SerializeField] private int itemIdx;

    ItemBase item;
    InventoryManager inventoryManager;
    public UITurotialGuide guide;

    void Awake()
    {
        item = scriptableItem.GetItemList(type)[itemIdx];  //������ ����
        inventoryManager = InventoryManager.GetInstance();
        guide = GameObject.FindWithTag("guide").GetComponent<UITurotialGuide>();

    }

    /// <summary> ������ ���� </summary>
    public ItemBase ClickItem()
    {
        return item;
    }

    /// <summary> ������ ȹ�� </summary>
    public void PickUp()
    {
        inventoryManager.keyCount += 1;
        if (inventoryManager.keyCount <= 1)
        {
            inventoryManager.AcquireItem(item);
            gameObject.SetActive(false);
            guide.OnTrigger(4);
        }
        else
        {
            inventoryManager.AcquireItem(item);
            gameObject.SetActive(false);
            guide.OnTrigger(5);
        }
    }
}
