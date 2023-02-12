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

    public Item() {}
    
    public Item(int itemPosition, int itemCount, ItemBase item)
    {
        this.itemPosition = itemPosition;
        this.itemCount = itemCount;
        this.item = item;
    }
    
    /// <summary> 아이템 수량 관리 </summary>
    public int SetItemCount(int _count)
    {
        itemCount += _count;

        return itemCount;
    }
}
