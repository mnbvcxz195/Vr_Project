using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class PlayerStatetest
{
    public int PlayerHp;
    public bool ondemege;

    public PlayerStatetest(int hp, bool damegedelay)
    {
        PlayerHp = hp;
        ondemege = damegedelay;

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

    public PlayerStatetest Newplayer = new PlayerStatetest(100, true);
    [SerializeField] Image demegeImg;

    public void Demege(int demege)
    {
        if (Newplayer.ondemege == true)
        {
            Newplayer.ondemege = false;
            Newplayer.PlayerHp -= demege;
            demegeImgfade();
            Invoke("test", 1f);
        }
        else return;
    }
    public void healhp(int heal)
    {
        Newplayer.PlayerHp += heal;
    }

        void test()
    {
        Newplayer.ondemege = true;

    }
    public void demegeImgfade()
    {
        demegeImg.DOFade(0.6f, 0.5f);
        demegeImg.DOFade(0, 0.5f).SetDelay(0.5f);

    }

}
