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
    Inventory _Inventory;
    public PlayerJump _player;
    ItemBase item;

    void Awake()
    {

        item = scriptableItem.GetItemList(type)[itemIdx];  //������ ����
        _Inventory = GameObject.FindWithTag("Inventory").GetComponent<Inventory>();
        //_player = GameObject.FindWithTag("Player").GetComponent<PlayerJump>();
    }
    private void OnEnable()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<PlayerJump>();

    }

    /// <summary> ������ ���� </summary>
    public ItemBase ClickItem()
    {
        return item;
    }

    /// <summary> ������ ȹ�� </summary>
    public void PickUp()
    {
        PlayerManager.GetInstance().itemPickUpSound(_player.PlayerSfx);
        InventoryManager.GetInstance().AcquireItem(item);
        gameObject.SetActive(false);
    }
    public void Throwaway()
    {
        Debug.Log($"����");
        _Inventory.Throwaway();
        Invoke("DestroyGo", 5f);
    }
    public void DestroyGo()
    {
        Destroy(this.gameObject);
    }
}
