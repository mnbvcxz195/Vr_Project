using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class UIPlayer : MonoBehaviour
{
    [SerializeField] Slider hpPlayer;
    [SerializeField] Image demegeImg;

    void Start()
    {
        hpPlayer.value = PlayerManager.GetInstance().Newplayer.PlayerHp;
    }

    void Update()
    {
        RefreshHP();
    }
    void RefreshHP()
    {
        hpPlayer.value = PlayerManager.GetInstance().Newplayer.PlayerHp;
    }
}
