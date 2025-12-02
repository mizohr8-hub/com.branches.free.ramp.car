using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanRotationScript : MonoBehaviour {
	private float rotationsPerMinute=500.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0,0,6.0f*rotationsPerMinute*Time.deltaTime);
	}
}
