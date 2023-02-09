using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class ObjectItem : MonoBehaviour
{
    public ScriptableItem scriptableItem;

    public int listIdx;
    public int itemIdx;

    ItemBase item;
    Inventory inventory;
    Button btnTestItem;

    void Start()
    {
        inventory = GameObject.FindWithTag("Inventory").GetComponent<Inventory>();
        btnTestItem = GetComponent<Button>();

        scriptableItem.SetItemList(listIdx);           //아이템 리스트 지정
        item = scriptableItem.GetItemList()[itemIdx];  //아이템 지정

        btnTestItem.GetComponent<Image>().sprite = item.ItemImage;

        btnTestItem.onClick.AddListener(() => { inventory.AcquireItem(item); });
    }

    public ItemBase ClickItem()
    {
        return item;
    }
}