using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjKey : MonoBehaviour
{
    public ScriptableItem scriptableItem;
    #region 아이템 유형
    /// <summary> 아이템 유형 </summary>
    #endregion
    [SerializeField] private ItemType type;
    [SerializeField] private int itemIdx;

    ItemBase item;
    InventoryManager inventoryManager;
    public UITurotialGuide guide;

    void Awake()
    {
        item = scriptableItem.GetItemList(type)[itemIdx];  //아이템 지정
        inventoryManager = InventoryManager.GetInstance();
        guide = GameObject.FindWithTag("guide").GetComponent<UITurotialGuide>();

    }

    /// <summary> 아이템 정보 </summary>
    public ItemBase ClickItem()
    {
        return item;
    }

    /// <summary> 아이템 획득 </summary>
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
