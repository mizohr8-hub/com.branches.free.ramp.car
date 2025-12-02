using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour {

public float magnitude = 2500f;
AudioSource aS;
	void Start ()
	{
		aS = this.GetComponent<AudioSource>();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	
	void OnCollisionEnter2D(Collision2D other)
	{
		this.GetComponent<Animator>().Play("BounceObj");
		if(other.collider.CompareTag("Player"))
		{
			 var force = transform.position - other.transform.position;
 
			force.Normalize ();
			other.collider.GetComponent<Rigidbody2D> ().AddForce (-force * magnitude);
			aS.Play();
		}
	}
}
