using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    [SerializeField] AudioSource Bgm;
    [SerializeField] GameObject Pl;


    void Start()
    {
        
    }

    void Update()
    {
        float distance = Vector3.Distance(Pl.transform.position, transform.position);
        if(distance < 1)
        {
            MonsterManager.GetInstance().battle = true;
            AudioManager.GetInstance().BgmPlay(Bgm, 3);
            Debug.Log($"{MonsterManager.GetInstance().Newmonster.MonsterHp}");
            gameObject.SetActive(false);

        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            MonsterManager.GetInstance().battle = true;
            AudioManager.GetInstance().BgmPlay(Bgm, 3);
            Debug.Log($"{MonsterManager.GetInstance().Newmonster.MonsterHp}");
            gameObject.SetActive(false);

        }
    }
    public void GrapTreasure()
    {
        MonsterManager.GetInstance().battle = true;
        AudioManager.GetInstance().BgmPlay(Bgm, 3);
        Debug.Log($"{MonsterManager.GetInstance().Newmonster.MonsterHp}");
        gameObject.SetActive(false);
    }

}
