using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBlinkingScript : MonoBehaviour {
	bool isReady;
	// Use this for initialization
	void Start () {
		isReady = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (isReady) {
			isReady = false;
			StartCoroutine (spriteBlink ());
		}
	}
	IEnumerator spriteBlink()
	{
		yield return new WaitForSeconds (0.05f);
		GetComponent<SpriteRenderer> ().enabled = false;
		yield return new WaitForSeconds (0.05f);
		GetComponent<SpriteRenderer> ().enabled = true;
		isReady = true;

	}
}
