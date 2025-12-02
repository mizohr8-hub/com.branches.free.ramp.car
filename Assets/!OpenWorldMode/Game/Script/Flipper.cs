using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flipper : MonoBehaviour {

	public Rigidbody2D flipper;
	public float power = 50f; 

	void OnMouseDown()
	{
		flipper.AddForce(Vector3.up*power);
		this.GetComponent<AudioSource>().Play();
	}
}
