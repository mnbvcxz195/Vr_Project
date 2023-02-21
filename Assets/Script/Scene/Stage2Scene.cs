using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2Scene : MonoBehaviour
{
    [SerializeField] AudioSource bgm;
    void Start()
    {
        AudioManager.GetInstance().BgmPlay(bgm, 2);
    }

}
