using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GetTreasure : MonoBehaviour
{
    ScenesManager scenesManager;
    public XRGrabInteractable grabInteractable;

    void Start()
    {
        scenesManager = ScenesManager.GetInstance();
        grabInteractable.enabled = false;
    }

    public void Win()
    {
        grabInteractable.enabled = true;
    }

    public void GrabTreasure()
    {
        Debug.Log($"Ending 씬으로 넘어감");
        Invoke("ToED", 2f);
    }

    public void ToED()
    {
        scenesManager.ChangeScene(Scene.Ending);
    }
}
