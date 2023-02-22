using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class OnTriggerF : MonoBehaviour
{
    ScenesManager scenesManager;
    public GameObject bossRoom1;
    public GameObject bossRoom2;
    public AudioSource breakSound;
    public Image fadein;

    public GameObject PL;
    public GameObject IV;

    private void Start()
    {
        scenesManager = ScenesManager.GetInstance();
        DontDestroyOnLoad(PL);
        DontDestroyOnLoad(IV);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            fadein.DOFade(1, 4f);
            AudioManager.GetInstance().SfxPlay(breakSound, 2);
            Debug.Log($"튜토리얼이 종료되었습니다.");
            Invoke("ToStage1", 1f);
            Invoke("goScene1", 8f);
            FadeIn();
        }
    }

    public void ToStage1()
    {
        bossRoom1.SetActive(false);
        bossRoom2.SetActive(true);
    }
    void goScene1()
    {
        scenesManager.ChangeScene(Scene.Stage1);
        Debug.Log($"이동");
    }
    void FadeIn()
    {
        fadein.DOFade(1, 2f).SetDelay(1f);
    }

}
