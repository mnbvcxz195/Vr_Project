using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjItem : MonoBehaviour
{
    public ScriptableItem scriptableItem;
    #region ������ ����
    /// <summary> ������ ���� </summary>
    #endregion
    [SerializeField] private ItemType type;
    [SerializeField] private int itemIdx;

    ItemBase item;
    Inventory inventory;

    void Awake()
    {
        inventory = GameObject.FindWithTag("Inventory").GetComponent<Inventory>();

        item = scriptableItem.GetItemList(type)[itemIdx];  //������ ����

    }

    /// <summary> ������ ���� </summary>
    public ItemBase ClickItem()
    {
        return item;
    }

    /// <summary> ������ ȹ�� </summary>
    public void PickUp()
    {
        inventory.AcquireItem(item);
        gameObject.SetActive(false);
        inventory.TestList();
    }
}
