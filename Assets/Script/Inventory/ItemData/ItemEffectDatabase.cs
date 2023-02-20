using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static UnityEditor.PlayerSettings;

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

    //XRRayInteractor ryInteractor;
    public XRDirectInteractor drInteractor;
    public XRGrabInteractable grabInteractable;
    GameObject objWeapon;
    GameObject objETC;

    private void Start()
    {
        //ryInteractor = GameObject.FindWithTag("Player").GetComponentsInChildren<XRRayInteractor>()[1];
        drInteractor = GameObject.FindWithTag("Player").GetComponentsInChildren<XRDirectInteractor>()[1];
    }

    [System.Obsolete]
    public void UseItemEffect(ItemType type, string itemName)
    {
        if (type == ItemType.Weapon)
        {
            if (objWeapon != null)
                Destroy(objWeapon);

            GameObject obj = Resources.Load<GameObject>($"Prefabs/{itemName}");
            objWeapon = Instantiate(obj);
            grabInteractable = objWeapon.GetComponent<XRGrabInteractable>();
            Debug.Log($"{objWeapon}");
            //ryInteractor.interactionManager.SelectEnter(ryInteractor, grabInteractable);

            drInteractor.interactionManager.SelectEnter(drInteractor, grabInteractable);
        }

        else if (type == ItemType.ETC)
        {
            if (objWeapon != null)
                Destroy(objWeapon);

            GameObject obj = Resources.Load<GameObject>($"Prefabs/{itemName}");
            objETC = Instantiate(obj);
            grabInteractable = objETC.GetComponent<XRGrabInteractable>();
            Debug.Log($"{objETC}");
            //ryInteractor.interactionManager.SelectEnter(ryInteractor, grabInteractable);
            drInteractor.interactionManager.SelectEnter(drInteractor, grabInteractable);
            Debug.Log($"dd");
        }
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
}
