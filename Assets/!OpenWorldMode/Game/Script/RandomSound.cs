using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSound : MonoBehaviour {

	public AudioClip[] fizz;
	AudioSource aS;

	
	void Start()
	{
		aS = this.GetComponent<AudioSource>();
		aS.clip = fizz[Random.Range(0,fizz.Length)];
		aS.Play();
	}
}
