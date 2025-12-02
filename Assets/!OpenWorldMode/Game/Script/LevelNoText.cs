using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrickGameManager;

public class LevelNoText : MonoBehaviour {

	// Use this for initialization
	void Start () {
//		GetComponent<Renderer> ().sortingOrder = 10;
//		GetComponent<Renderer> ().sortingLayerID = 10;
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<TextMesh> ().text = (GameManagerBouncy.instance.CurrentLevel+1).ToString();
	}
}
