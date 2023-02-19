using System.Collections;
using System.Collections.Generic;
using Unity.XR.Oculus;
using UnityEngine;
using UnityEngine.UI;

public class Workroom : MonoBehaviour
{
    [SerializeField] private Transform slotParent;
    [SerializeField] private UICreateSlot[] uICreateSlot;
    private InventoryManager _inventoryManager;
    [SerializeField] private Button btnCreate;
    Inventory inventory;
    public UITurotialGuide guide;

    void Start()
    {
        _inventoryManager = InventoryManager.GetInstance();
        inventory = GameObject.FindWithTag("Inventory").GetComponent<Inventory>();
        uICreateSlot = slotParent.GetComponentsInChildren<UICreateSlot>();
        btnCreate.onClick.AddListener(CreateItem);
    }

    public void CreateItem()
    {
        if (uICreateSlot[0]._item != null && uICreateSlot[1]._item != null)
        {
            _inventoryManager.MixItem(uICreateSlot[0]._item.Type, uICreateSlot[0]._item.ItemIdx, uICreateSlot[1]._item.Type, uICreateSlot[1]._item.ItemIdx);
            uICreateSlot[0].ClearSelected();
            uICreateSlot[1].ClearSelected();
            if (_inventoryManager.mix)
            {
                inventory.SpendSelectItem(_inventoryManager.mixItem1, _inventoryManager.mixItemCount1);
                inventory.SpendSelectItem(_inventoryManager.mixItem2, _inventoryManager.mixItemCount2);
                _inventoryManager.mix = false;
            }

            if (!_inventoryManager.key)
            {
                if (_inventoryManager.Items[ItemType.ETC].ContainsKey(0))
                {
                    guide.OnTrigger(6);
                    _inventoryManager.key = true;
                }
            }
        }
        else
            Debug.Log($"재료를 선택하세요.");
    }
}
