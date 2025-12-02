using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {

	public GameObject cannon;
	// public GameObject baseStand;
	public GameObject ball;
	public Transform spawnPoint;
	public float interval = 0.3f;
	public float power = 2f;
	public bool left, right;
	// public float angle = 45f;
	AudioSource aS;

	
	void Start()
	{
		aS = this.GetComponent<AudioSource>();
		InvokeRepeating("Shoot", interval,interval);
		if(right)
		{
			cannon.GetComponent<Animator>().Play("CannonDown2Up");
		}
		else if (left)
		{
			cannon.GetComponent<Animator>().Play("CannonUp2Down");
		}
	}

	void Shoot()
	{
		GameObject ballSpawn= Instantiate(ball, spawnPoint.position	, Quaternion.identity);
		// ballSpawn.GetComponent<Rigidbody2D>().mass = 0.5f;
		if(left)
		{
			ballSpawn.GetComponent<Rigidbody2D>().AddForce(-spawnPoint.right*power);
		}
		else if(right)
		{
			ballSpawn.GetComponent<Rigidbody2D>().AddForce(spawnPoint.right*power);
		}

		aS.Play();		

	}

	
}
