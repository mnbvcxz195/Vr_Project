using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class Stage1Scene : MonoBehaviour
{
    [SerializeField] AudioSource bgm;
    public Image fade;
    public NextStage nex;
    public PlayerJump pl;

    void Start()
    {
        pl = PlayerManager.GetInstance().player;
        pl.gameObject.transform.position = new Vector3(-7, 2, -62);
        PlayerManager.GetInstance().Newplayer.PlayerHp = 100;
        AudioManager.GetInstance().BgmPlay(bgm, 2);
        Fadeout();
    }
    void Fadeout()
    {
        fade.DOFade(0, 2f).SetDelay(1f);
    }

}
