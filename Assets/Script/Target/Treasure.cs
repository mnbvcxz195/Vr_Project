using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    [SerializeField] AudioSource Bgm;
    [SerializeField] GameObject Pl;
    public AnubisAni anubisAni;

    void Start()
    {
        
    }

    void Update()
    {
        //float distance = Vector3.Distance(Pl.transform.position, transform.position);
        //if(distance < 1)
        //{
        //    MonsterManager.GetInstance().battle = true;
        //    AudioManager.GetInstance().BgmPlay(Bgm, 3);
        //    Debug.Log($"{MonsterManager.GetInstance().Newmonster.MonsterHp}");
        //    gameObject.SetActive(false);

        //}

        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    MonsterManager.GetInstance().battle = true;
        //    AudioManager.GetInstance().BgmPlay(Bgm, 3);
        //    Debug.Log($"{MonsterManager.GetInstance().Newmonster.MonsterHp}");
        //    gameObject.SetActive(false);

       // }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            MonsterManager.GetInstance().battle = true;
            if (MonsterManager.GetInstance().battle == true)
            {
                MonsterManager.GetInstance().Newmonster.MonsterHp = 100;
                anubisAni.atr.SetBool("IsBattle", true);
                Debug.Log($"{MonsterManager.GetInstance().battle}");
                AudioManager.GetInstance().BgmPlay(Bgm, 3);
                Debug.Log($"{MonsterManager.GetInstance().Newmonster.MonsterHp}");
                gameObject.SetActive(false);

            }
        }
    }
    public void GrapTreasure()
    {
        MonsterManager.GetInstance().battle = false;
        AudioManager.GetInstance().BgmPlay(Bgm, 3);
        Debug.Log($"{MonsterManager.GetInstance().Newmonster.MonsterHp}");
        gameObject.SetActive(false);
    }

}
