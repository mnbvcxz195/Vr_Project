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

        scriptableItem.SetItemList(listIdx);           //������ ����Ʈ ����
        item = scriptableItem.GetItemList()[itemIdx];  //������ ����

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
