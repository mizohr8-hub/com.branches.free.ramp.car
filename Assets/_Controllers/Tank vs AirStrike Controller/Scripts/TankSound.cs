using UnityEngine;
using System.Collections;

public class TankSound : MonoBehaviour {

	// Use this for initialization
	public Rigidbody rb;
	public float minPitch;
	public float maxPitch;

	public float minVelocity;
	public float maxVelocity;


	private AudioSource myAudio;


	void Start () {
		myAudio = this.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
			myAudio.pitch = minPitch + (rb.velocity.magnitude - minVelocity) * (maxPitch - minPitch) / (maxVelocity - minVelocity);
	}
}
