using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{

	Rigidbody myRigidbody;
	Vector3 oldVel;
	private GameObject Absorver, Reflector;
	public GameObject StepSound,BoxHitSound,WinSound, MagicEnterSound;
	//Vector3 prevConPoint;

	void Start ()
	{
		myRigidbody = GetComponent<Rigidbody> ();
//		GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
//		GetComponent<Renderer>().material.color=Color.red;
		Absorver = GameObject.Find ("Absorver");
		Reflector = GameObject.Find ("Reflector");
		myRigidbody.interpolation = RigidbodyInterpolation.Interpolate;
		//prevConPoint = transform.position;
	}

	void FixedUpdate ()
	{
		oldVel = myRigidbody.velocity;
	}

	void OnCollisionEnter (Collision col)
	{
		if (col.gameObject.tag != "WinObj" && col.gameObject.tag!="MagicObj") {
			ContactPoint cp = col.contacts [0];
			//float dist = Vector3.Distance (prevConPoint, cp.point);
			// calculate with Vector3.Reflect
			myRigidbody.velocity = Vector3.Reflect (oldVel, cp.normal);
			// bumper effect to speed up ball
			myRigidbody.velocity += cp.normal * (-0.3f);

//			Instantiate (StepSound, Vector3.zero, Quaternion.identity);
			SoundInst(StepSound);
		} 
		else if (col.gameObject.tag=="MagicObj") {
			//col.gameObject.GetComponentInChildren<ParticleSystem> ().Play ();
//			Instantiate (MagicEnterSound, Vector3.zero, Quaternion.identity);
			SoundInst(MagicEnterSound);
			ContactPoint cp = col.contacts [0];
			ParticlePlayer (col.transform.GetChild (0).gameObject, cp.point);

			transform.position = Reflector.transform.position-(Absorver.transform.position-cp.point);

			myRigidbody.velocity = Vector3.Reflect (oldVel, cp.normal);
			ParticlePlayer (Reflector.transform.GetChild (0).gameObject, transform.position);
			// bumper effect to speed up ball
			myRigidbody.velocity += cp.normal * (-0.5f);
		}
		else {
			GameController.GControlManager.OnLevelCompletion ();
//			Instantiate (WinSound, Vector3.zero, Quaternion.identity);
			SoundInst(WinSound);
			//BoxScript.isBoxCloseReady = true;
			Destroy (this.gameObject);
		}


		if (col.gameObject.name == "Box" || col.gameObject.name == "CapRight" || col.gameObject.name == "CapLeft") {
			//Instantiate (BoxHitSound, Vector3.zero, Quaternion.identity);
			SoundInst(BoxHitSound);
		}
	}

	void OnBecameInvisible() {
		Destroy(gameObject);
	}

	public void ParticlePlayer(GameObject go,Vector3 pos)
	{
		
		go.transform.position = pos;
		go.GetComponent<ParticleSystem> ().Play ();
	}

	public void SoundInst(GameObject soundObj)
	{
		if (PlayerPrefs.GetInt ("SoundStatus") == 0) {
			Instantiate (soundObj, Vector3.zero, Quaternion.identity);
		}
	}

}