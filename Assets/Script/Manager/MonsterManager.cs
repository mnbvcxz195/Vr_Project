using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anubis
{
    public int MonsterHp;
    public bool ondamage;

    public Anubis(int hp, bool damagedelay)
    {
        MonsterHp = hp;
        ondamage = damagedelay;

    }

}
public class MonsterManager : MonoBehaviour
{
    #region Singletone

    private static MonsterManager instance = null;

    public static MonsterManager GetInstance()
    {
        if (instance == null)
        {
            GameObject go = new GameObject("@MonsterManager");
            instance = go.AddComponent<MonsterManager>();

            DontDestroyOnLoad(go);
        }
        return instance;

    }
    #endregion

    public bool battle = false;
    public Anubis Newmonster = new Anubis(100, true);
    private void Start()
    {
        Newmonster = new Anubis(100, true);
    }
    public void MonsterDamage(int demege)
    {
        if (Newmonster.ondamage == true)
        {
            Newmonster.ondamage = false;
            Newmonster.MonsterHp -= demege;
            Invoke("DamageTrue", 1f);
        }
        else return;
    }
    void DamageTrue()
    {
        Newmonster.ondamage = true;
    }
    public void MonsterHpBar(GameObject go)
    {
        if(battle)
        go.gameObject.SetActive(true);
    }

    public void MonsterDie(AudioSource oo)
    {
        AudioManager.GetInstance().BgmPlay(oo, 2);
    }
}
