using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region instance

    private static GameManager instance = null;

    public static GameManager GetInstance()
    {
        if (instance == null)
        {
            GameObject go = new GameObject("@GameManager");
            instance = go.AddComponent<GameManager>();

            DontDestroyOnLoad(go);
        }
        return instance;

    }
    #endregion

    public PlayerState Newplayer = new PlayerState(100, true);

    public void Damege(int demege)
    {
        if (Newplayer.ondamage == true)
        {
            Newplayer.ondamage = false;
            Newplayer.PlayerHp -= demege;
            Invoke("Damegedelay", 1f);
        }
        else return;
    }
    void Damegedelay()
    {
        Newplayer.ondamage = true;

    }

    public List<trap> trapList = new List<trap>();

    public void CheckTrap()
    {
        Debug.Log("1");

        var traps = FindObjectsOfType<trap>();
        for (int i = 0; i < traps.Length; i++)
        {
            trapList.Add(traps[i]);
        }
    }
}
