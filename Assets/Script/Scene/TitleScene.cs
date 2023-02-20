using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : MonoBehaviour
{
    [SerializeField] AudioSource bgm;
    void Start()
    {
        AudioManager.GetInstance().BgmPlay(bgm, 0);
    }

}
