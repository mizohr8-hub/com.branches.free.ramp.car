using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {
    public Vector3 rotDirection;
    public int speed;
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(rotDirection * speed * Time.deltaTime);
	}
}
