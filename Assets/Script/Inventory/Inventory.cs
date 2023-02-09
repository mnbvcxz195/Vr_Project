using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

#region Inven: 내 아이템 목록 (테스트용)
/// <summary> 내 아이템 목록 class </summary>
[Serializable]
public class Inven
{
    [SerializeField] public int itemCount;
    [SerializeField] public ItemBase Item;
}
#endregion

public class Inventory : MonoBehaviour
{
    #region 인벤토리 활성화 여부
    /// <summary> 인벤토리 활성화 여부 </summary>
    #endregion
    private bool inventoryActivated;

    #region 인벤토리 오브젝트
    /// <summary> 인벤토리 오브젝트 </summary>
    #endregion
    [SerializeField] private Transform slotParent;

    #region 인벤토리 슬롯
    /// <summary> 인벤토리 슬롯 </summary>
    #endregion
    [SerializeField] private Slot[] slots;

    #region 내 아이템 목록 (테스트용)
    /// <summary> 내 아이템 목록 </summary>
    #endregion
    [SerializeField] private Inven[] myItem;


    private void OnValidate()
    {
        slots = slotParent.GetComponentsInChildren<Slot>();
    }

    void Awake()
    {
        myItem = new Inven[slots.Length];

        for (int i = 0; i < slots.Length; i++)
        {
            int idx = i;
            slots[i].btnUsed.onClick.AddListener(() => { UseItem(idx); });
        }

        TestList();
    }

    private void Update()
    {
        OpenInventory();
    }

    #region 테스트용
    /// <summary> 인스펙터에 슬롯의 아이템 목록 표시 </summary>
    public void TestList()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            int idx = i;
            myItem[i].Item = slots[idx].Item;
            myItem[i].itemCount = slots[idx].ItemCount;
        }
    }
    #endregion

    /// <summary> 인벤토리 오픈 관리 </summary>
    private void OpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!inventoryActivated)
            {
                slotParent.gameObject.SetActive(true);
                inventoryActivated = true;
            }
            else
            {
                slotParent.gameObject.SetActive(false);
                inventoryActivated = false;
            }
        }

    }

    /// <summary> 아이템 획득 </summary>
    public void AcquireItem(ItemBase item, int _count = 1)
    {
        if (item.Type == ItemType.Ingredient || item.Type == ItemType.Consumable)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].Item != null)
                {
                    if (slots[i].Item.ItemName == item.ItemName)
                    {
                        slots[i].SetItemCount(_count); //얻은 템이 중복템일 경우 아이템 개수 +1
                        return;                        //중복템은 슬롯에 추가 X
                    }
                }
            }
        }

        int num = 0;
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].Item == null)                 //슬롯에 자리 남아있으면 얻은 아이템 추가
            {
                slots[i].AddItem(item, _count);
                return;
            }
            else
                num++;
        }
        if (slots.Length <= num)
            Debug.Log($"꽉참");
    }

    /// <summary> 아이템 사용 </summary>
    public void UseItem(int idx)
    {
        if (slots[idx].Item != null)
        {
            Debug.Log($"아이템 사용");
            slots[idx].SetItemCount(-1);
            TestList();
        }
        else
            Debug.Log($"텅");
    }
}