using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerClose : MonoBehaviour
{
    public Animator stoneAmimS2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            stoneAmimS2.SetTrigger("CloseT");
        }
    }
}
