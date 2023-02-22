using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2Scene : MonoBehaviour
{
    [SerializeField] AudioSource bgm;
    void Start()
    {
        PlayerManager.GetInstance().Newplayer.PlayerHp = 100;
        MonsterManager.GetInstance().battle = false;
        MonsterManager.GetInstance().Newmonster.MonsterHp = 100;
        AudioManager.GetInstance().BgmPlay(bgm, 2);

    }

}
