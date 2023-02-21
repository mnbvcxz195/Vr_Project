using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAni : MonoBehaviour
{
    public Transform ItemPosition;
    Vector3 itemPo;
    Vector3 anivector = new Vector3 (0,0.3f,0);
    Vector3 UPpo = new Vector3(0, 0.5f, 0);
    private void Awake()
    {
        //ItemPosition.transform.position = gameObject.transform.position;

    }
    void Start()
    {
        ItemPosition = gameObject.GetComponent<Transform>();
        //ItemPosition.transform.position += UPpo;
        StartCoroutine(itmeani());
    }

    void Update()
    {
        //gameObject.transform.position = ItemPosition.transform.position;
    }

    IEnumerator itmeani()
    {
        while(ItemPosition != null)
        {
            yield return new WaitForSeconds(0.001f);
            ItemPosition.transform.localEulerAngles += anivector;
        }

    }
}
