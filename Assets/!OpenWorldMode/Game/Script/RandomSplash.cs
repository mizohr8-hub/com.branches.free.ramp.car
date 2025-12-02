using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSplash : MonoBehaviour {


	public Sprite[] splashes;

	void Start () 
	{
		this.GetComponent<SpriteRenderer>().sprite = splashes[Random.Range(0, splashes.Length)];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
