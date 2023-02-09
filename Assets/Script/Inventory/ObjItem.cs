using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjItem : MonoBehaviour
{
    public ScriptableItem scriptableItem;
    public int listIdx;
    public int itemIdx;

    ItemBase item;
    Inventory inventory;

    void Awake()
    {
        inventory = GameObject.FindWithTag("Inventory").GetComponent<Inventory>();

        scriptableItem.SetItemList(listIdx);           //아이템 리스트 지정
        item = scriptableItem.GetItemList()[itemIdx];  //아이템 지정

    }

    public ItemBase ClickItem()
    {
        return item;
    }

    public void PickUp()
    {
        inventory.AcquireItem(item);
        gameObject.SetActive(false);
    }
}
