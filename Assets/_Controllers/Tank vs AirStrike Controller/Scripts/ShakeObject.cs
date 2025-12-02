using UnityEngine;
using System.Collections;

public class ShakeObject : MonoBehaviour {

	// Use this for initialization
	[HideInInspector]
	public float shakeTimer;
	[HideInInspector]
	public float shakeAmount;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (shakeTimer >= 0) {
			Vector2 shakePosition = Random.insideUnitCircle * shakeAmount;
			transform.position = new Vector3 (transform.position.x + shakePosition.x,transform.position.y + shakePosition.y,transform.position.z);
			shakeTimer -= Time.deltaTime;
		}
	}



	public void ShakeCamera (float shakePower, float shakeDuration) {
		shakeAmount = shakePower;
		shakeTimer = shakeDuration;
	}


}
