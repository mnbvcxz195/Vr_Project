using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NextStage : MonoBehaviour
{
    [SerializeField] GameObject pl;
    public Image fade;

    void Update()
    {
        NextStage2();
    }
    void NextStage2()
    {
        float aaa = Vector3.Distance(pl.transform.position, transform.position);
        if(aaa < 2)
        {
            FadeIn();
            Invoke("nextInvoke", 2);
        }
    }
    void nextInvoke()
    {
        ScenesManager.GetInstance().ChangeScene(Scene.Stage2);
    }
    void FadeIn()
    {
        fade.DOFade(1, 2f).SetDelay(1f);
    }

}
