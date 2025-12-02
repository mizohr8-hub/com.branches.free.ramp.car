using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour {
    public GameObject rccCanvas;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (RGSK.RaceManager.instance.isCanvas)
            rccCanvas.SetActive(true);
        else
            rccCanvas.SetActive(false);
	}
    
}
