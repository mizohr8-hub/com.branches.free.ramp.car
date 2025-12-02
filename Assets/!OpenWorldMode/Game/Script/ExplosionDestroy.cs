using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        Destroy(GetComponent<CircleCollider2D>(), 0.1f);
        Destroy(GetComponent<PointEffector2D>(), 0.1f);
        Destroy(gameObject, 3f);
//        Destroy(gameObject,0.1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
