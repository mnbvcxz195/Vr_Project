using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;
using DG.Tweening;



public class GetTreasure : MonoBehaviour
{
    ScenesManager scenesManager;
    public XRGrabInteractable grabInteractable;
    public AnubisAni anubisAni;
    [SerializeField] Image fade;
    void Start()
    {
        scenesManager = ScenesManager.GetInstance();
        grabInteractable.enabled = false;
    }
    private void Update()
    {
        Isclear();
    }
    public void Win()
    {
        grabInteractable.enabled = true;
    }

    public void GrabTreasure()
    {
        Debug.Log($"Ending 씬으로 넘어감");
        fade.DOFade(1, 1.5f);
        Invoke("ToED", 2f);
    }

    public void ToED()
    {
        scenesManager.ChangeScene(Scene.Ending);
    }
    void Isclear()
    {
        if (MonsterManager.GetInstance().battle == false && anubisAni.isDie == true)
        {
            Win();
        }
        else
            return;
    }
}
