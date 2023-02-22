using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStage : MonoBehaviour
{
    [SerializeField] GameObject pl;
    void Start()
    {
        
    }

    void Update()
    {
        NextStage2();
    }
    void NextStage2()
    {
        float aaa = Vector3.Distance(pl.transform.position, transform.position);
        if(aaa < 2)
        {
            ScenesManager.GetInstance().ChangeScene(Scene.Stage2);
        }
        Debug.Log($"{aaa}");
    }
}
