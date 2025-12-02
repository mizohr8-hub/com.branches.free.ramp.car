using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour {


	Rigidbody2D otherRb;
	float initialSize;
	AudioSource aS;
	public AudioClip bubbleIn, bubbleBurst;

	void Start()
	{
		aS = this.GetComponent<AudioSource>();
	}
	
		void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Player"))
		{		
			if(!other.GetComponent<PlayerController1New>().inBubble)
			{
				otherRb = other.GetComponent<Rigidbody2D>();
				otherRb.GetComponent<PlayerController1New>().inBubble = true;
				other.transform.position = this.transform.position;
				this.transform.SetParent(other.transform);			
				otherRb.GetComponent<PlayerController1New>().inBubble = true;
				initialSize = otherRb.GetComponent<CircleCollider2D>().radius;
				otherRb.GetComponent<CircleCollider2D>().radius = this.GetComponent<CircleCollider2D>().radius;
				otherRb.velocity = otherRb.velocity/2;
				otherRb.gravityScale = -0.5f;	
				aS.clip = bubbleIn;
				aS.Play();
			}		
		}
	}

	
	void OnMouseDown()
	{
		otherRb.GetComponent<PlayerController1New>().inBubble = false;
		otherRb.GetComponent<CircleCollider2D>().radius = initialSize;
		otherRb.gravityScale = 1;
		AudioSource ao = new GameObject("Audio").AddComponent<AudioSource>();
		ao.clip = bubbleBurst;
		ao.Play();
		Destroy(ao.gameObject,bubbleBurst.length);		
		Destroy(gameObject);
	}
}
