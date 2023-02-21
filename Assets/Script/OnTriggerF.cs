using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerF : MonoBehaviour
{
    ScenesManager scenesManager;
    public GameObject bossRoom1;
    public GameObject bossRoom2;

    private void Start()
    {
        scenesManager = ScenesManager.GetInstance();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log($"튜토리얼이 종료되었습니다.");
            Invoke("ToStage1", 1f);
        }
    }

    public void ToStage1()
    {
        bossRoom1.SetActive(false);
        bossRoom2.SetActive(true);
        //scenesManager.ChangeScene(Scene.Stage1);
    }
}
