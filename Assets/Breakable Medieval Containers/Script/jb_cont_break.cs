using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jb_cont_break : MonoBehaviour
{

	public Transform Fragments;
	public float Spread, Force;
	[SerializeField] GameObject Item;

    private void Awake()
    {
		Item.gameObject.SetActive(false);
	}
    public void GrapoBj () 
    {
		Item.gameObject.SetActive(true);
		Destroy(gameObject,0.5f);
		Instantiate(Fragments, transform.position, transform.rotation);
		Fragments.localScale = transform.localScale;
		Vector3 breakPosition = transform.position;
		Collider[] fragments = Physics.OverlapSphere(breakPosition, Spread);

		foreach (Collider fragment in fragments)
		{
			if (fragment.attachedRigidbody)
			{
				fragment.attachedRigidbody.AddExplosionForce(Force, breakPosition, Spread);
			}
		}
	}
}
