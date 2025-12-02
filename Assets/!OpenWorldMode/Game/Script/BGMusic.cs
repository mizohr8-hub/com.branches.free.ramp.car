using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusic : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		GameObject[] bg = GameObject.FindGameObjectsWithTag("BGMusic");
		if(bg.Length >1)
		{
			Destroy(bg[1].gameObject);
		}
		DontDestroyOnLoad(this.gameObject);	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
