using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OnSocket : MonoBehaviour
{
    public XRSocketInteractor interactor;
    public Animator stoneAmim;
    string keyName;
    public AudioSource breakSound;

    void Start()
    {
        keyName = "이상한석상1";
    }

    public void OpenStone()
    {
        if ($"{interactor.GetOldestInteractableSelected()}" == $"{keyName} (UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable)" || $"{interactor.GetOldestInteractableSelected()}" == $"{keyName}(Clone) (UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable)")
        {
            Debug.Log($"맞음");
            stoneAmim.SetTrigger("DownT");
            AudioManager.GetInstance().SfxPlay(breakSound, 1);

        }
        else
            Debug.Log($"틀림");
    }
}
