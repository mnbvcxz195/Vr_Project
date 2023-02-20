using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIDie : MonoBehaviour
{
    public Button restart;
    [SerializeField] AudioSource Diesound;
    void Start()
    {
        AudioManager.GetInstance().BgmPlay(Diesound, 5);
        restart = GetComponentInChildren<Button>();
        restart.onClick.AddListener(SceneReLoad);
    }

    void SceneReLoad()
    {
        ScenesManager.GetInstance().SceneReLoad();
        Debug.Log("11");
    }
}
