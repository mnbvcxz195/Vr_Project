using System;
using UnityEngine;

#region Item : 내 아이템
/// <summary> 내 아이템 </summary>
#endregion
[Serializable]
public class Item
{
    [SerializeField] public int itemPosition;
    [SerializeField] public int itemCount;
    [SerializeField] public ItemBase item;
    [SerializeField] public bool use;

    public Item() {}
    
    public Item(int itemPosition, int itemCount, ItemBase item, bool use)
    {
        this.itemPosition = itemPosition;
        this.itemCount = itemCount;
        this.item = item;
        this.use = use;
    }
    
    /// <summary> 아이템 수량 관리 </summary>
    public int SetItemCount(int _count)
    {
        itemCount += _count;

        return itemCount;
    }
}
