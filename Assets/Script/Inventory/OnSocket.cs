using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OnSocket : MonoBehaviour
{
    public XRSocketInteractor interactor;
    public Animator stoneAmim;
    string keyName;

    void Start()
    {
        keyName = "�̻��Ѽ���1";
    }

    public void OpenStone()
    {
        if ($"{interactor.GetOldestInteractableSelected()}" == $"{keyName} (UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable)" || $"{interactor.GetOldestInteractableSelected()}" == $"{keyName}(Clone) (UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable)")
        {
            Debug.Log($"����");
            stoneAmim.SetTrigger("DownT");
        }
        else
            Debug.Log($"Ʋ��");
    }
}
