using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void GrapTreasure()
    {
        MonsterManager.GetInstance().battle = true;
        Debug.Log($"{MonsterManager.GetInstance().Newmonster.MonsterHp}");
        gameObject.SetActive(false);
    }
}
