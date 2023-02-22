using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingScene : MonoBehaviour
{
    [SerializeField] AudioSource bgm;
    void Start()
    {
        AudioManager.GetInstance().BgmPlay(bgm, 4);

    }

}
