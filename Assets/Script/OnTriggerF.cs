using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerF : MonoBehaviour
{
    ScenesManager scenesManager;

    private void Start()
    {
        scenesManager = ScenesManager.GetInstance();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log($"Ʃ�丮���� ����Ǿ����ϴ�.");
            Invoke("ToStage1", 5f);
        }
    }

    public void ToStage1()
    {
        scenesManager.ChangeScene(Scene.Stage1);
    }
}
