using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanScript : MonoBehaviour
{
	private GameObject Ball;
	private float offset, FanLeftX, FanRightX;
	private Vector3 FanInitPos;
	private float rotationsPerMinute=1000.0f;
	public GameObject FanAirPart;
	// Use this for initialization
	void Start ()
	{
		offset = 1f;
		FanInitPos = transform.position;
		FanLeftX = FanInitPos.x - offset;
		FanRightX = FanInitPos.x+offset/2;

	}
	void Update()
	{
		transform.Rotate(0,0,6.0f*rotationsPerMinute*Time.deltaTime);
		FanAirPart.transform.position = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if (ObjectThrowScript.instanceObjThrow.ballObj != null) {
			Ball = ObjectThrowScript.instanceObjThrow.ballObj;
			if (Ball.transform.position.x > FanLeftX && Ball.transform.transform.position.x < FanRightX && Ball.transform.position.y > FanInitPos.y) {
				StartCoroutine (FanEffectr (Ball));
//			gameObject.GetComponent<Rigidbody> ().AddForce (new Vector3 (0,Random.Range(0.4f,0.8f), 0), ForceMode.Impulse);
				print ("Inside Fan.................................");
			}
		}

	}

	IEnumerator FanEffectr ( GameObject _Ball)
	{
		yield return new WaitForSeconds (0f);
		if (_Ball != null) {
//			_Ball.gameObject.GetComponent<Rigidbody> ().AddForce (new Vector3 (0, Random.Range (0.6f, 0.8f), 0), ForceMode.Impulse);
			_Ball.gameObject.GetComponent<Rigidbody> ().velocity=_Ball.gameObject.GetComponent<Rigidbody> ().velocity+ new Vector3 (0, Random.Range (1f, 1.5f),0);
		}
		yield return new WaitForSeconds (0.1f);
		if (_Ball != null) {
//			_Ball.gameObject.GetComponent<Rigidbody> ().AddForce (new Vector3 (0, -0.2f, 0), ForceMode.Impulse);
			_Ball.gameObject.GetComponent<Rigidbody> ().velocity= _Ball.gameObject.GetComponent<Rigidbody> ().velocity+new Vector3 (0, -0.5f, 0);
		}
	}
}
