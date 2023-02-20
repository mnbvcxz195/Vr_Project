using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class UIPlayer : MonoBehaviour
{
    [SerializeField] Slider hpPlayer;
    [SerializeField] Slider hpMonster;

    [SerializeField] Image hpPlayerfill;
    [SerializeField] Image hpMonsterfill;

    [SerializeField] Canvas Uidie;

    [SerializeField] Image demegeImg;

    void Start()
    {
        hpPlayer.value = PlayerManager.GetInstance().Newplayer.PlayerHp;
        hpMonster.value = MonsterManager.GetInstance().Newmonster.MonsterHp;
        //1
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
        if(hpPlayer.value <= 0)
        {
            hpPlayerfill.gameObject.SetActive(false);
            Uidie.gameObject.SetActive(true);
        }
        if (hpMonster.value <= 0)
        {
            hpPlayerfill.gameObject.SetActive(false);
        }

    }
    void hpMonsterHideOn()
    {
        GameObject go = hpMonster.gameObject;
        MonsterManager.GetInstance().MonsterHpBar(go);
    }

}
