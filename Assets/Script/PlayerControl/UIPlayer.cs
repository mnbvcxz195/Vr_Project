using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class UIPlayer : MonoBehaviour
{
    [SerializeField] Slider hpPlayer;
    [SerializeField] Slider hpMonster;

    [SerializeField] Image demegeImg;

    void Start()
    {
        hpPlayer.value = PlayerManager.GetInstance().Newplayer.PlayerHp;
        hpMonster.value = MonsterManager.GetInstance().Newmonster.MonsterHp;

    }

    void Update()
    {
        RefreshHP();
        hpMonsterHideOn();
    }
    void RefreshHP()
    {
        hpPlayer.value = PlayerManager.GetInstance().Newplayer.PlayerHp;
        hpMonster.value = MonsterManager.GetInstance().Newmonster.MonsterHp;
    }
    void hpMonsterHideOn()
    {
        GameObject go = hpMonster.gameObject;
        MonsterManager.GetInstance().MonsterHpBar(go);
    }

}
