using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2Scene : MonoBehaviour
{
    [SerializeField] AudioSource bgm;
    public PlayerJump pl;

    void Start()
    {
        pl.gameObject.transform.position = new Vector3(0, -0.57f, -23);

        PlayerManager.GetInstance().Newplayer.PlayerHp = 100;
        MonsterManager.GetInstance().battle = false;
        MonsterManager.GetInstance().Newmonster.MonsterHp = 100;
        AudioManager.GetInstance().BgmPlay(bgm, 2);

    }

}
