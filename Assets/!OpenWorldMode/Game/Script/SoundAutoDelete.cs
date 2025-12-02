using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAutoDelete : MonoBehaviour {
	private float soundClipLength;
	// Use this for initialization
	void Start () {
		soundClipLength=GetComponent<AudioSource> ().clip.length;
		Invoke ("DestroySelf", soundClipLength);
	}
	
	public void DestroySelf()
	{
		Destroy (gameObject);
	}
}
