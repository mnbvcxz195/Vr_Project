using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestPickUpObj : MonoBehaviour
{
    public ObjItem objItem;
    public Button btnPickUp;
    public Image imgPickUp;
    void Start()
    {
        imgPickUp.sprite = objItem.ClickItem().ItemImage;
        btnPickUp.onClick.AddListener(objItem.PickUp);
    }
}
