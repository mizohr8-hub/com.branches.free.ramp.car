using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushButtonScript : MonoBehaviour {
	private Vector3 startPos,endPos;
	public Animator TaskAnim;
	// Use this for initialization
	void Start () {
		startPos = transform.position;
		endPos = startPos + new Vector3 (0, -0.2f, 0);

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			PushEnable ();
		}
		
	}


	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.name == "Ball") {
			PushDisable ();
			Invoke ("PushEnable", 2f);
		}
	}





	void PushEnable ()
	{
		GetComponent<Collider> ().enabled = true;
		transform.position = startPos; 
		TaskAnim.SetTrigger ("UpTrigger");
	}
	void PushDisable ()
	{
		GetComponent<Collider> ().enabled = false;
		transform.position = endPos; 
		TaskAnim.SetTrigger ("DownTrigger");
	}
}
