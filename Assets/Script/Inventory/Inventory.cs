using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    #region 인벤토리 활성화 여부
    /// <summary> 인벤토리 활성화 여부 </summary>
    #endregion
    public bool inventoryActivated;

    #region 인벤토리 매니저
    /// <summary> 인벤토리 매니저 </summary>
    #endregion
    private InventoryManager _inventoryManager;
    
    #region 인벤토리 오브젝트
    /// <summary> 인벤토리 오브젝트 </summary>
    #endregion
    [SerializeField] private Transform slotParent;
    [SerializeField] private Transform workroomParent;

    #region 인벤토리 슬롯
    /// <summary> 인벤토리 슬롯 (배열) </summary>
    #endregion
    [SerializeField] private UISlot[] slots;

    private InputDevice targetDevice;

    private int changeSlotIdx;
    public int ChangeIdx => changeSlotIdx;

    [SerializeField] private Transform inventoryParent;
    [SerializeField] private Transform UIPos;
    [SerializeField] private Transform UIRot;
    public UITurotialGuide guide;

    public XRRayInteractor ryInteractor;
    public XRDirectInteractor drInteractor;

    void Awake()
    {
        _inventoryManager = InventoryManager.GetInstance();
        _inventoryManager.OnItemAddHandler += ItemAdd;
        
        slots = slotParent.GetComponentsInChildren<UISlot>();
    }
    private void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics rightControllerCharacteristics =
            InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devices);

        if (devices.Count > 0)
            targetDevice = devices[0];
    }

    private void Update()
    {
        OpenInventory();
    }

    /// <summary> 인벤토리 오픈 관리 </summary>
    private void OpenInventory()
    {
        targetDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondaryButtonValue);

        if (secondaryButtonValue)
        {
            if (!inventoryActivated)
            {
                if (_inventoryManager.is1st != true)
                {
                    guide.OnTrigger(1);
                    _inventoryManager.is1st = true;
                }
                inventoryParent.position = UIPos.position;
                inventoryParent.rotation = UIRot.rotation;
                slotParent.gameObject.SetActive(true);
                workroomParent.gameObject.SetActive(true);
                drInteractor.gameObject.SetActive(false);
                ryInteractor.gameObject.SetActive(true);
                inventoryActivated = true;
            }
            else
            {
                slotParent.gameObject.SetActive(false);
                workroomParent.gameObject.SetActive(false);
                ryInteractor.gameObject.SetActive(false);
                drInteractor.gameObject.SetActive(true);
                inventoryActivated = false;
            }
        }

    }

    void ItemAdd(Item item)
    {
        var slot = slots[item.itemPosition];
        
        if(slot.Item == null)
            slot.AddItem(item.item, item.itemCount);
        else
            slot.SetItemCount(item.itemCount);
    }

    public int ChangeSlotIdx()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].dropItem)
            {
                changeSlotIdx = i;
                slots[i].dropItem = false;
            }
        }

        return changeSlotIdx;
    }

    public void EquipItem()
    {
        if(!_inventoryManager.Equip)
            return;

        if (_inventoryManager._preIdx < 10)
            slots[_inventoryManager.Items[_inventoryManager.preUse][_inventoryManager._preIdx].itemPosition].SetEquipColor(1);
        if (_inventoryManager._curIdx < 10)
            slots[_inventoryManager.Items[_inventoryManager.curUse][_inventoryManager._curIdx].itemPosition].SetEquipColor(0.5f);
    }

    public void Throwaway()
    {
        slots[_inventoryManager.Items[_inventoryManager.curUse][_inventoryManager._curIdx].itemPosition].SetEquipColor(1f);

        _inventoryManager.Items[_inventoryManager.curUse][_inventoryManager._curIdx].use = false;
        _inventoryManager._curIdx = 10;
        _inventoryManager.curEquip = true;
        Debug.Log($"{_inventoryManager._curIdx}");
    }

    public void SpendSelectItem(int idx, int count)
    {
        slots[idx].SetItemCount(count);
    }
}