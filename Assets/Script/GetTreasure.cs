using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GetTreasure : MonoBehaviour
{
    ScenesManager scenesManager;
    public XRGrabInteractable grabInteractable;
    public AnubisAni anubisAni;
    void Start()
    {
        scenesManager = ScenesManager.GetInstance();
        grabInteractable.enabled = false;
    }
    private void Update()
    {
       if (MonsterManager.GetInstance().battle = false&& anubisAni.isDie == true)
        {
            Win();
        }
    }
    public void Win()
    {
        grabInteractable.enabled = true;
    }

    public void GrabTreasure()
    {
        Debug.Log($"Ending ������ �Ѿ");
        Invoke("ToED", 2f);
    }

    public void ToED()
    {
        scenesManager.ChangeScene(Scene.Ending);
    }
}
