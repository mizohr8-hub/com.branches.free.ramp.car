using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpriteRandom : MonoBehaviour {


	public Sprite[] balls;

	void Start () 
	{
		this.GetComponent<SpriteRenderer>().sprite = balls[Random.Range(0,balls.Length)];	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
