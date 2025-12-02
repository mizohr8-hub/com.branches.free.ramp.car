using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour {

	public float liftime;
	
	void Start () 
	{
		Destroy(gameObject,liftime);	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
