using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallThrowScript : MonoBehaviour
{
	public GameObject ballPrefab;
	public LineRenderer pathLine;
	Vector3 mMouseDownPos,mMouseUpPos,mMouseDragPos;
	public float speed = .1f;
	private GameObject ballObj;
	// Use this for initialization
	void Start ()
	{
		pathLine = GetComponent<LineRenderer> ();
	}

	// Update is called once per frame
	void Update ()
	{
	}

	void OnMouseDown ()
	{
		mMouseDownPos = Input.mousePosition;
		ballObj = Instantiate (ballPrefab, GetWorldPosition (mMouseDownPos), Quaternion.identity);
		Debug.Log ("the mouse down pos is " + mMouseDownPos.y.ToString ());
		mMouseDownPos = Input.mousePosition;
		Debug.Log ("the mouse down pos is " + mMouseDownPos.z.ToString ());
		mMouseDownPos.z = 0;
	}

	void OnMouseUp ()
	{
		mMouseUpPos = Input.mousePosition;
		mMouseUpPos.z = 0;
		var direction = mMouseDownPos - mMouseUpPos;
		var distance = Vector3.Distance (mMouseDownPos, mMouseUpPos);
		direction.Normalize ();
		ballObj.GetComponent<Rigidbody> ().useGravity = true;
		ballObj.GetComponent<Rigidbody> ().AddForce (direction * speed * distance, ForceMode.Impulse);
		Debug.Log ("the mouse up pos is " + mMouseUpPos.ToString ());
		SetTrajectoryLineRenderesActive(false);
	}

	void OnMouseDrag ()
	{
		mMouseDragPos = Input.mousePosition;
		var dist = Vector3.Distance (GetWorldPosition(mMouseDownPos), GetWorldPosition(mMouseDragPos));
		DisplayTrajectoryLineRenderer (dist);
		print ("Distance     " + dist);
	}


	private Vector3 GetWorldPosition (Vector3 inputPosition)
	{
		Vector3 worldPosition = Camera.main.ScreenToWorldPoint (inputPosition);
		worldPosition.z = transform.position.z;
		return worldPosition;
	}



	void DisplayTrajectoryLineRenderer(float distance)
	{
		SetTrajectoryLineRenderesActive(true);
		Vector3 v2 =GetWorldPosition( mMouseDownPos) -GetWorldPosition( mMouseDragPos);
		int segmentCount = 5;
		float segmentScale = 2;
		Vector2[] segments = new Vector2[segmentCount];

		// The first line point is wherever the player's cannon, etc is
		segments[0] =ballObj.transform.position;

		// The initial velocity
		Vector2 segVelocity = new Vector2(v2.x, v2.y) * 10 * distance;

		float angle = Vector2.Angle(segVelocity, new Vector2(1, 0));
		float time = segmentScale / segVelocity.magnitude;
		for (int i = 1; i < segmentCount; i++)
		{
			float time2 = i * Time.fixedDeltaTime * 5;
			segments[i] = segments[0] + segVelocity * time2 + 0.5f * Physics2D.gravity * Mathf.Pow(time2, 2);
		}

		pathLine.SetVertexCount(segmentCount);
		for (int i = 0; i < segmentCount; i++)
			pathLine.SetPosition(i, segments[i]);
	}

	void SetTrajectoryLineRenderesActive( bool active)
	{
		pathLine.enabled = active;
	}




}