using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

	public GameObject particles;
	GameManagerNew gm;
	public float minVelocity;
	public bool throughRelativeVelocity = true;

	void Start()
	{
		gm =GameObject.FindGameObjectWithTag("GameManagerBouncy").GetComponent<GameManagerNew>();
	}
	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.collider.CompareTag("Player"))
		{
			gm.gameOver = true;
			gm.CheckStatus();
			// Debug.Log("BombOver");
			Instantiate(particles, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
		if(other.relativeVelocity.magnitude > minVelocity && throughRelativeVelocity)
		{
			gm.gameOver = true;
			gm.CheckStatus();
			Debug.Log("BombThroughVelocity");
			Instantiate(particles, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
		Debug.Log(other.gameObject.name);
	}
}
