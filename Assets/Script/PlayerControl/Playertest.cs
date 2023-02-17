using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Playertest : MonoBehaviour
{
    public Rigidbody rigid;
    public GameObject Ground;
    public int JumpPower;
    public int MoveSpeed;
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        Move();
    }
    void Move ()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
 
        transform.Translate((new Vector3 (h, 0, v) * MoveSpeed) * Time.deltaTime);
    }
 
}

