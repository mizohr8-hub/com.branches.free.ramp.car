using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleRotator : MonoBehaviour {


	public float speed;
	Vector3 rotateAround = new Vector3(0,0,1);
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Rotate(rotateAround * speed*Time.deltaTime);
	}
	
}
