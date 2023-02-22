using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OnSocketS1 : MonoBehaviour
{
    public XRSocketInteractor interactor;
    public Animator stoneAmimL;
    public Animator stoneAmimR;
    string keyName;

    void Start()
    {
        keyName = "�̻��Ѽ���2";
    }

    public void OpenStone()
    {
        if ($"{interactor.GetOldestInteractableSelected()}" == $"{keyName} (UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable)" || $"{interactor.GetOldestInteractableSelected()}" == $"{keyName}(Clone) (UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable)")
        {
            Debug.Log($"����");
            stoneAmimL.SetTrigger("OpenT");
            stoneAmimR.SetTrigger("OpenT");
        }
        else
            Debug.Log($"Ʋ��");
    }
}