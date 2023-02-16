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

    private const string HP = "HP";

    public void UseItemEffect(ItemType type, string itemName)
    {
        if (type == ItemType.Weapon)
        {
            //���� ����
        }
        else if (type == ItemType.ETC)
        {
            //��Ÿ ������ ����
        }
        else if (type == ItemType.Consumable)
        {
            //������ ���
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
                    Debug.Log($"{itemName}�� ����߽��ϴ�.");
                    return;
                }
            }
        }
        else
            Debug.Log($"Database�� ��ġ�ϴ� �������� �����ϴ�.");
    }

    public void UseConsumable(ItemType type, string itemName)
    {
        //������ ���
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
                Debug.Log($"{itemName}�� ����߽��ϴ�.");
                return;
            }
        }
    }


    //ItemEffectDatabase itemEffectDatabase;
    //itemEffectDatabase.UseItemEffect(itemType, _item.ItemName);
}
