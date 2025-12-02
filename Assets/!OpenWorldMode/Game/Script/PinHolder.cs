using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinHolder : MonoBehaviour {

	public GameObject particles; 
	AudioSource aS;
	Rigidbody2D rb;
	public float minVelocity = 1.5f;
	public bool moving, circular;
	void Start () 
	{
		rb = this.GetComponent<Rigidbody2D>();
		aS = this.GetComponentInChildren<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

		void OnCollisionEnter2D(Collision2D other)
	{
		if (other.collider.CompareTag("Player"))
		{
			// rb.isKinematic = false;
			if(this.GetComponent<FixedJoint2D>())
			{
				Destroy(this.GetComponent<FixedJoint2D>());
				ContactPoint2D contact = other.contacts[0];
				Instantiate(particles, contact.point, Quaternion.identity);
				aS.Play();
			}
			if(this.transform.childCount > 1)
			{
				this.transform.GetChild(0).GetComponent<Pins>().ColorChangeHingedParent();
				this.transform.DetachChildren();
			}		
			Movement();
			
			// Debug.Log("Hit");
			// rb.AddTorque(30f);
		}
		if(other.relativeVelocity.magnitude > minVelocity)
		{
			if(this.GetComponent<FixedJoint2D>())
			{
				Destroy(this.GetComponent<FixedJoint2D>());
				ContactPoint2D contact = other.contacts[0];
				Instantiate(particles, contact.point, Quaternion.identity);	
				aS.Play();
			}
			if(this.transform.childCount > 1)
			{
				this.transform.GetChild(0).GetComponent<Pins>().ColorChangeHingedParent();
				this.transform.DetachChildren();
			}
			Movement();
					
		}
	}

	void Movement()
	{
		if(moving)
		{
			Destroy(this.GetComponent<Animator>());
		}
		else if(circular)
		{
			Destroy(this.GetComponent<CircularMotion>());
		}
	}
}
