using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

	public float startAfter = 1f;

	
	void Start()
	{
		
		StartCoroutine(StartAnimation());
	}

	IEnumerator StartAnimation()
	{
		yield return new WaitForSeconds(startAfter);
		
		this.GetComponent<Animator>().Play("MovingPlatform");
	}
}
