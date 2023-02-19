using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemEffect
{
    public string itemName;
    public string effectPart;
    public int effectValue;
}

public class ItemEffectDatabase : MonoBehaviour
{
    [SerializeField]
    private ItemEffect[] itemEffect;
    [SerializeField] private Transform objPos;

    private const string HP = "HP";

    public void UseItemEffect(ItemType type, string itemName)
    {
        if (type == ItemType.Weapon)
        {
            GameObject obj = Resources.Load<GameObject>($"Prefabs/{itemName}");
            Instantiate(obj);
            obj.transform.position = objPos.position;
            Debug.Log($"{obj.transform.position} | {objPos.position}");
        }
        else if (type == ItemType.ETC)
        {
            GameObject obj = Resources.Load<GameObject>($"Prefabs/{itemName}");
            Instantiate(obj);
            obj.transform.position = objPos.position;
            Debug.Log($"{obj.transform.position} | {objPos.position}");
            //기타 아이템 장착
        }
        //else if (type == ItemType.Consumable)
        //{
        //    //아이템 사용
        //    for (int i = 0; i < itemEffect.Length; i++)
        //    {
        //        if (itemEffect[i].itemName == itemName)
        //        {
        //            switch (itemEffect[i].effectPart)
        //            {
        //                case HP:
        //                    PlayerManager.GetInstance().healhp(itemEffect[i].effectValue);
        //                    Debug.Log($"{itemEffect[i].effectPart} +{itemEffect[i].effectValue}");
        //                    break;
        //            }
        //            Debug.Log($"{itemName}을 사용했습니다.");
        //            return;
        //        }
        //    }
        //}
        else
            Debug.Log($"장비 / 기타 아이템이 아님");
    }

    public void UseConsumable(ItemType type, string itemName)
    {
        //아이템 사용
        for (int i = 0; i < itemEffect.Length; i++)
        {
            if (itemEffect[i].itemName == itemName)
            {
                switch (itemEffect[i].effectPart)
                {
                    case HP:
                        PlayerManager.GetInstance().healhp(itemEffect[i].effectValue);
                        Debug.Log($"{itemEffect[i].effectPart} +{itemEffect[i].effectValue}");
                        break;
                }
                Debug.Log($"{itemName}을 사용했습니다.");
                return;
            }
        }
    }


    //ItemEffectDatabase itemEffectDatabase;
    //itemEffectDatabase.UseItemEffect(itemType, _item.ItemName);
}
