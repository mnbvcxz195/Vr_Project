using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.XR.Interaction.Toolkit;



public class UIPlayer : MonoBehaviour
{
    [SerializeField] Slider hpPlayer;
    [SerializeField] Slider hpMonster;

    [SerializeField] Image hpPlayerfill;
    [SerializeField] Image hpMonsterfill;

    [SerializeField] Canvas Uidie;

    [SerializeField] Image demegeImg;
    public XRRayInteractor ryInteractor;
    public XRDirectInteractor drInteractor;


    void Start()
    {
        hpPlayer.value = PlayerManager.GetInstance().Newplayer.PlayerHp;
        hpMonster.value = MonsterManager.GetInstance().Newmonster.MonsterHp;
        hpMonster.gameObject.SetActive(false);
        if (PlayerManager.GetInstance().Newplayer.PlayerHp > 0)
        {
            hpPlayerfill.gameObject.SetActive(true);
            hpMonster.gameObject.SetActive(false);
            PlayerManager.GetInstance().Newplayer.PlayerDie = false;
            Uidie.gameObject.SetActive(false);
        }
    }
        void Update()
        {
            RefreshHP();
            hpMonsterHideOn();
        }
        void RefreshHP()
        {
            hpPlayer.value = PlayerManager.GetInstance().Newplayer.PlayerHp;
            Debug.Log("ÇÃ·¹ÀÌ¾î ¹ë·ù ³Ñ¾î¿È");
            hpMonster.value = MonsterManager.GetInstance().Newmonster.MonsterHp;
            Debug.Log("¸ó½ºÅÍ ¹ë·ù ³Ñ¾î¿È");

            if (MonsterManager.GetInstance().battle == true)
            {
                hpMonster.gameObject.SetActive(true);
            }
            if (PlayerManager.GetInstance().Newplayer.PlayerHp <= 0)
            {
                hpPlayerfill.gameObject.SetActive(false);
                PlayerManager.GetInstance().Newplayer.PlayerDie = true;
                Uidie.gameObject.SetActive(true);
                if (PlayerManager.GetInstance().Newplayer.PlayerDie == true)
                {
                    ryInteractor.gameObject.SetActive(true);
                    drInteractor.gameObject.SetActive(false);
                }
            }

        }
        void hpMonsterHideOn()
        {
            GameObject go = hpMonster.gameObject;
            MonsterManager.GetInstance().MonsterHpBar(go);
        }
    
}

