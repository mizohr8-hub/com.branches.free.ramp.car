using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

	public GameObject A, B;
	public float disabledTime = 0.5f;
	public bool teleportOthers;
	void Start () 
	{

	}
	
	
	void Update () 
	{

	}

	public void TeleportAToB(GameObject other)
	{
		other.GetComponent<Rigidbody2D>().velocity = other.GetComponent<Rigidbody2D>().velocity.magnitude* B.transform.up;
		other.transform.position = B.transform.position;
		StartCoroutine(CollisionOnOff(other));
	}
	public void TeleportBToA(GameObject other)
	{
		other.GetComponent<Rigidbody2D>().velocity = other.GetComponent<Rigidbody2D>().velocity.magnitude* A.transform.up;
		other.transform.position = A.transform.position;
		StartCoroutine(CollisionOnOff(other));
	}

	IEnumerator CollisionOnOff(GameObject playerBall)
	{
		A.GetComponent<CapsuleCollider2D>().enabled = false;
		B.GetComponent<CapsuleCollider2D>().enabled = false;
		if(playerBall.GetComponent<TrailRenderer>())
		{
			playerBall.GetComponent<TrailRenderer>().enabled = false;
		}
		yield return new WaitForSeconds(disabledTime);
		A.GetComponent<CapsuleCollider2D>().enabled = true;
		B.GetComponent<CapsuleCollider2D>().enabled = true;
		if(playerBall.GetComponent<TrailRenderer>())
		{
			playerBall.GetComponent<TrailRenderer>().enabled = true;
		}
	}

	
}
