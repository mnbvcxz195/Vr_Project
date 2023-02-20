using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjItem : MonoBehaviour
{
    public ScriptableItem scriptableItem;
    #region 아이템 유형
    /// <summary> 아이템 유형 </summary>
    #endregion
    [SerializeField] private ItemType type;
    [SerializeField] private int itemIdx;
    Inventory _Inventory;

    ItemBase item;

    void Awake()
    {
        //inventory = GameObject.FindWithTag("Inventory").GetComponent<Inventory>();

        item = scriptableItem.GetItemList(type)[itemIdx];  //아이템 지정
        _Inventory = GameObject.FindWithTag("Inventory").GetComponent<Inventory>();

    }

    /// <summary> 아이템 정보 </summary>
    public ItemBase ClickItem()
    {
        return item;
    }

    /// <summary> 아이템 획득 </summary>
    public void PickUp()
    {
        InventoryManager.GetInstance().AcquireItem(item);
        gameObject.SetActive(false);
    }
    public void Throwaway()
    {
        Debug.Log($"버림");
        _Inventory.Throwaway();
        Invoke("DestroyGo", 5f);
    }
    public void DestroyGo()
    {
        Destroy(this.gameObject);
    }
}
