using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camON : MonoBehaviour {
    public GameObject cam;
	// Use this for initialization
	void OnEnable () {
        cam.GetComponent<MenuCamera>().enabled = true;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
