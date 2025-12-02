using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public GameObject ball;
	// public int noOfBalls = 3;
	public float maxStretch;
	public float minStretch = 0.7f;
	public float releaseTime = 0.15f;
	public float minVelocity = 0.5f;
	public float startingWidth, endingWidth;
	public LineRenderer lr1;
	public LineRenderer lr2;
	public Transform patch;
	Vector2 midPos;
	SpringJoint2D spring;
	bool isPressed, newBallCheck, freeBallCheck, outside, minStretchAchieved;
	Rigidbody2D rb;
	Vector2 initialPos;
	GameManagerNew gm;
	TrailRenderer tr;

	void Start()
	{
		spring = this.GetComponent<SpringJoint2D>();
		rb = this.GetComponent<Rigidbody2D>();
		midPos = (lr1.transform.position + lr2.transform.position)/2;
		initialPos = transform.position;
		gm = GameObject.FindGameObjectWithTag("GameManagerBouncy").GetComponent<GameManagerNew>();
		tr = this.GetComponent<TrailRenderer>();
		tr.enabled = false;
	}

	void Update()
	{
		LineSetup();

		if(isPressed)
		{
			Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			if(Vector2.Distance(mousePos, midPos) > maxStretch)
			{
				rb.position = midPos + (mousePos - midPos).normalized * maxStretch;
				// Debug.Log("OutOfBounds");
			}
			else
			{
				rb.position = mousePos;
			}
			if(Vector2.Distance(mousePos, midPos) > minStretch)
			{
				minStretchAchieved = true;
			}
			else
			{
				minStretchAchieved = false;
			}

			patch.GetComponent<Rigidbody2D>().position = rb.position;			
		}
		if(newBallCheck)
		{
			BallCheck();
		}
	}

	void LineSetup()
	{
		lr1.SetPosition(0, lr1.transform.position);
		lr1.SetPosition(1, patch.position);
		lr1.startWidth = startingWidth;
		lr1.endWidth = endingWidth;
		lr2.SetPosition(0, lr2.transform.position);
		lr2.SetPosition(1, patch.position);
		lr2.startWidth = startingWidth;
		lr2.endWidth = endingWidth;
		lr1.useWorldSpace = true;
		lr2.useWorldSpace = true;
	}

	void OnMouseDown()
	{		
		isPressed = true;
		rb.isKinematic = true;
		patch.GetComponent<SpringJoint2D>().enabled = false;
	}

	void OnMouseUp()
	{	
		rb.isKinematic = false;
		isPressed = false;
		patch.GetComponent<SpringJoint2D>().enabled = true;
		if(minStretchAchieved)
		{	
			freeBallCheck = true;
			tr.enabled = true;			
			StartCoroutine(Release());
		}	
			
	}

	IEnumerator Release()
	{
		yield return new WaitForSeconds(releaseTime);
		spring.enabled = false;
		newBallCheck = true;
		Debug.Log("Released");	

		yield return new WaitForSeconds(1f);
		patch.GetComponent<BoxCollider2D>().enabled = true;			
	}

	void BallCheck()
	{
		// Debug.Log(rb.velocity.magnitude);
		
		if(rb.velocity.magnitude < minVelocity || outside)
		{	
			// Debug.Log("DragChange");
			// rb.angularDrag = 4f;
			// if(rb.velocity.magnitude == 0)
			{
				newBallCheck = false;
				if(gm.noOfBalls >1)
				{
					GameObject newBall = Instantiate(ball, initialPos, Quaternion.identity);
					newBall.GetComponent<SpringJoint2D>().enabled = true;
					newBall.transform.parent = this.transform.parent;
					newBall.GetComponent<PlayerController>().freeBallCheck = false;
					newBall.GetComponent<TrailRenderer>().enabled = false;
					newBall.transform.localScale = this.transform.localScale;
					Destroy(this.GetComponent<PlayerController>());
					Debug.Log("ThisMethodWorks");
					gm.noOfBalls--;
				}
				else
				{
					Debug.Log("BallOver");	// if ball over and pins left
				}

				if(outside)
				{
					Destroy(this.gameObject);
				}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.name == "Patch" && freeBallCheck)
		{
			newBallCheck = false;
			spring.enabled = true;
			transform.position = initialPos;
			patch.GetComponent<BoxCollider2D>().enabled = false;
			Debug.Log("FreeBall");
		}		
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.gameObject.name == "Down")
		{
			outside = true;
			patch.GetComponent<BoxCollider2D>().enabled = false;
		}
	}

	


	
	
}
