using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPin : MonoBehaviour {

	public Sprite[] tins;

	void Start () {
		
		this.GetComponent<SpriteRenderer>().sprite = tins[Random.Range(0,tins.Length)];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
