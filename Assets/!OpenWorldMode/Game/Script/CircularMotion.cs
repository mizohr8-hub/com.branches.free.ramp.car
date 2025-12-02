using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularMotion : MonoBehaviour {


	public float speed, width, height;
	public float delaytimer = 0f;
	float x,y,z = 0;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		delaytimer += Time.deltaTime*speed;
		x = Mathf.Cos(delaytimer) * width;
		y = Mathf.Sin(delaytimer)*height;
		
		transform.localPosition = new Vector3(x,y,z);
	}
}
