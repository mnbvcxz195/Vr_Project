using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    void Start()
    {
        Debug.Log($"����");
        InventoryManager.GetInstance().itemEffectDatabase = GameObject.FindWithTag("Database").GetComponent<ItemEffectDatabase>();
    }
}
