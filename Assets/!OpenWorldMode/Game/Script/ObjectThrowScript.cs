using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrickGameManager;

public class ObjectThrowScript : MonoBehaviour
{
	public GameObject ballPrefab;
	public LineRenderer pathLine;
	private Vector3 mMouseDownPos, mMouseUpPos, mMouseDragPos, ballDirection;
	private float ballDistance, trajectoryDist;
	private float speed;
	[HideInInspector]
	public GameObject ballObj;
	private bool isShowTrajectory, isMouseUp;
	public LineRenderer trailLine;
	private Vector3 tailLineEndPos;
	public GameObject ballShadowPatch;
	private GameObject shadowBall;
	private float velocityFactor;
//	public static bool timeToOpenBox;
	public static ObjectThrowScript instanceObjThrow;
	// Use this for initialization
	void Start ()
	{
		pathLine = GetComponent<LineRenderer> ();
		isShowTrajectory = false;
		trailLine.enabled = false;
		speed = 1f;
		velocityFactor = 1.2f;
		instanceObjThrow = this;
//		Physics.gravity = new Vector3 (0, -8.8f, 0);
	}
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
			//GameManagerBouncy.instance.CurrentGameState = GameState.PlayState;

			if (ballObj != null) {
				Destroy (ballObj);
			}
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (GetComponent<Collider> ().Raycast (ray, out hit, 100.0F)) {
				GameManagerBouncy.instance.CurrentGameState = GameState.PlayState;
				mMouseDownPos = Input.mousePosition;
				ballObj = Instantiate (ballPrefab, GetWorldPosition (mMouseDownPos), Quaternion.identity);
				ballObj.name = "Ball";
				//ballObj.transform.parent = transform;
				mMouseDownPos = Input.mousePosition;
				mMouseDownPos.z = 0;
				isShowTrajectory = true;
				if (shadowBall == null) {
					shadowBall = Instantiate (ballShadowPatch, GetWorldPosition (Input.mousePosition), Quaternion.identity);
					shadowBall.transform.parent = transform;
				} else {
					shadowBall.transform.position = GetWorldPosition (Input.mousePosition);
				}
			}


		}

		if (Input.GetMouseButtonUp (0)) {
			mMouseUpPos = Input.mousePosition;
			mMouseUpPos.z = 0;
			ballDirection =GetWorldPosition( mMouseDownPos) - GetWorldPosition(mMouseUpPos);
			ballDirection.Normalize ();
			isMouseUp = true;
			SetTrajectoryLineRenderesActive (false);
			isShowTrajectory = false;
			trailLine.enabled = false;
			resetLineRenderer (GetComponent<LineRenderer> ());


		}

		if (Input.GetMouseButton (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (GetComponent<Collider> ().Raycast (ray, out hit, 100.0F)) {
				SetTrajectoryLineRenderesActive (true);
				mMouseDragPos = Input.mousePosition;
				//ballDistance = Vector3.Distance (mMouseDownPos, mMouseDragPos);
				trajectoryDist = Vector3.Distance (GetWorldPosition (mMouseDownPos), GetWorldPosition (mMouseDragPos));
			}

		}
		if (Input.GetMouseButton (0)) {
			if (isShowTrajectory && GetWorldPosition (mMouseDownPos) != GetWorldPosition (Input.mousePosition)) {
				trailLine.enabled = true;
				trailLine.SetPosition (0, ballObj.transform.position);
				DisplayTrajectoryLineRenderer (trajectoryDist);
				var AngleFromBallObj = AngleInRad (ballObj.transform.position, GetWorldPosition (Input.mousePosition));
				var tailLineEndPos = new Vector3 (Mathf.Cos (AngleFromBallObj) + ballObj.transform.position.x, Mathf.Sin (AngleFromBallObj) + ballObj.transform.position.y, 0);
				trailLine.SetPosition (1, tailLineEndPos);
				//UpdateTrajectory (trajectoryDist);
			}
		}

		colliderStatus (ScrollViewController.islevelOpen);
	}


	void FixedUpdate ()
	{
		if (isMouseUp) {
			isMouseUp = false;
			if (ballObj != null) {
				Vector3 velocity=(GetWorldPosition (mMouseDownPos) - GetWorldPosition (Input.mousePosition))*velocityFactor;
				ballObj.GetComponent<Rigidbody> ().useGravity = true;
//				ballObj.GetComponent<Rigidbody> ().AddForce (ballDirection * speed * trajectoryDist * Time.fixedDeltaTime, ForceMode.Impulse);
				ballObj.GetComponent<Rigidbody>().velocity=velocity * speed * trajectoryDist;
			}
		}
	}

	/// <summary>
	/// Get the world position.
	/// </summary>
	private Vector3 GetWorldPosition (Vector3 inputPosition)
	{
		Vector3 worldPosition = Camera.main.ScreenToWorldPoint (inputPosition);
		worldPosition.z = transform.position.z;
		return worldPosition;
	}


	/// <summary>
	/// Display the trajectory line renderer.
	/// </summary>
	void DisplayTrajectoryLineRenderer (float distance)
	{
		SetTrajectoryLineRenderesActive (true);
		Vector3 v2 = (GetWorldPosition (mMouseDownPos) - GetWorldPosition (Input.mousePosition))*velocityFactor;
		int segmentCount = 5;
		float segmentScale = 2;
		Vector2[] segments = new Vector2[segmentCount];
		if (ballObj != null) {
			segments [0] = ballObj.transform.position;
		}
		Vector2 segVelocity = new Vector2 (v2.x, v2.y) * distance;
//		Vector2 segVelocity = new Vector2 (v2.x, v2.y)*distance *6;
//		print("segment velocity::  "+segVelocity);

		float angle = Vector2.Angle (segVelocity, new Vector2 (1, 0));
		float time = segmentScale / segVelocity.magnitude;
		for (int i = 1; i < segmentCount; i++) {
			float time2 = i * Time.fixedDeltaTime * 5;
			segments [i] = segments [0] + segVelocity * time2 +0.25f* Physics2D.gravity * Mathf.Pow (time2, 2);
//			segments [i] = segments [0] + segVelocity * time2 +0.25f* Physics2D.gravity * Mathf.Pow (time2, 2);
		}

		pathLine.SetVertexCount (segmentCount);
		for (int i = 0; i < segmentCount; i++)
			pathLine.SetPosition (i, segments [i]);
	}

	void SetTrajectoryLineRenderesActive (bool active)
	{
		pathLine.enabled = active;
	}

	public static float AngleInDeg (Vector3 vec1, Vector3 vec2)
	{
		return AngleInRad (vec1, vec2) * 180 / Mathf.PI;
	}

	public static float AngleInRad (Vector3 vec1, Vector3 vec2)
	{
		return Mathf.Atan2 (vec2.y - vec1.y, vec2.x - vec1.x);
	}

	private void resetLineRenderer (LineRenderer lr)
	{
		lr.positionCount = 0;
	}
	public void colliderStatus( bool isActive)
	{
			GetComponent<Collider> ().enabled = !isActive;
	}
		
}