using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class PlayerState
{
    public int PlayerHp;
    public bool ondamage;
    public bool PlayerDie;


    public PlayerState(int hp, bool damagedelay,bool playerDie)
    {
        PlayerHp = hp;
        ondamage = damagedelay;
        PlayerDie = playerDie;
    }
}

public class PlayerManager : MonoBehaviour
{
    #region instance

    private static PlayerManager instance = null;

    public static PlayerManager GetInstance()
    {
        if (instance == null)
        {
            GameObject go = new GameObject("@PlayerManager");
            instance = go.AddComponent<PlayerManager>();

            DontDestroyOnLoad(go);
        }
        return instance;

    }
    #endregion

    public PlayerState Newplayer = new PlayerState(100, true, false);
    [SerializeField] Image demegeImg;
    public PlayerJump player;
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerJump>();
        Newplayer = new PlayerState(100, true, false);
    }
    public void Damage(int demege)
    {
        if (Newplayer.ondamage == true)
        {
            Newplayer.ondamage = false;
            Newplayer.PlayerHp -= demege;
            demegeImgfade();
            Invoke("DamageTrue", 1f);
        }
        else return;
    }
    public void healhp(int heal)
    {
        Newplayer.PlayerHp += heal;
    }

        void DamageTrue()
    {
        Newplayer.ondamage = true;

    }
    public void demegeImgfade()
    {
        demegeImg.DOFade(0.6f, 0.5f);
        demegeImg.DOFade(0, 0.5f).SetDelay(0.5f);

    }
    public void itemPickUpSound(AudioSource playersfx)
    {
        AudioManager.GetInstance().PlayerSfxPlay(playersfx, 3);
    }

}
