using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController1New : MonoBehaviour {

	public GameObject ball;
	public float radius1, radius2;
	// public CircleCollider2D triggerColl;
	// public int noOfBalls = 3;
	public float maxStretch;
	public float minStretch = 0.7f;
	public float releaseTime = 0.15f;
	public float minVelocity = 0.5f;
	public float ballCheckDelay = 2f;
	public float startingWidth, endingWidth;
	public LineRenderer lr1;
	public LineRenderer lr2;
	public Transform patch;
	Vector2 midPos;
	SpringJoint2D spring;
	bool isPressed, newBallCheck, freeBallCheck, outside, checkingVel;
	[HideInInspector]
	public bool minStretchAchieved, inBubble/* , noBallAtStop */;
	Rigidbody2D rb;
	Vector2 initialPos;
	GameManagerNew gm;
	TrailRenderer tr;
	TrajectoryNew2 trajectory;
	ButtonCanvas buttonCanvas;
	AudioSource aS;
	public AudioClip stretch, release;
	void Start()
	{
		// triggerColl.radius = 0.8f;
		spring = this.GetComponent<SpringJoint2D>();
		rb = this.GetComponent<Rigidbody2D>();
		midPos = (lr1.transform.position + lr2.transform.position)/2;
		initialPos = transform.position;
		gm = GameObject.FindGameObjectWithTag("GameManagerBouncy").GetComponent<GameManagerNew>();
		tr = this.GetComponent<TrailRenderer>();
		tr.enabled = false;
		trajectory = this.GetComponent<TrajectoryNew2>();
		aS = this.GetComponent<AudioSource>();
		if(SceneManager.GetActiveScene().name != "MainMenu1")
		{
			buttonCanvas = GameObject.FindGameObjectWithTag("ButtonCanvas").GetComponent<ButtonCanvas>();
		}		
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
				// Debug.Log("Following");
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
		
		
		if(!inBubble && newBallCheck)			
		{
			if(rb.velocity.magnitude < minVelocity && checkingVel)
			{				
				checkingVel = false;
				StartCoroutine(BallCheck());			
			}
			else if (rb.velocity.magnitude > minVelocity && !outside)
			{
				checkingVel = true;
			}
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
		spring.enabled = false;
		patch.GetComponent<SpringJoint2D>().enabled = false;
		aS.clip = stretch;
		aS.Play();
	}

	void OnMouseUp()
	{	
		// this.GetComponent<CircleCollider2D>().radius =radius2;
		
		rb.isKinematic = false;
		isPressed = false;
		patch.GetComponent<SpringJoint2D>().enabled = true;
		if(minStretchAchieved)
		{	
			// triggerColl.radius = 0.15f;
			freeBallCheck = true;
			tr.enabled = true;			
			StartCoroutine(Release());
			aS.clip = release;
			aS.Play();
		}
		else
		{
			spring.enabled = true;
			trajectory.isBallThrown = false;
			// transform.position = initialPos;
		}
			
	}

	IEnumerator Release()
	{
		this.gameObject.layer = 2;
		yield return new WaitForSeconds(releaseTime);
		spring.enabled = false;
		newBallCheck = true;
		// Debug.Log("Released");	
		yield return new WaitForSeconds(0.5f);
		patch.GetComponent<BoxCollider2D>().enabled = true;			
	}

	IEnumerator BallCheck()
	{
		// Debug.Log(rb.velocity.magnitude);
		if(!outside)
		{
			yield return new WaitForSeconds(ballCheckDelay);
		}
		
		if(!checkingVel || outside)
		{				
			newBallCheck = false;
			outside = false;
			if(gm.noOfBalls >1)
			{
				
				this.gameObject.layer = 0;
				GameObject newBall = Instantiate(ball, initialPos, Quaternion.identity);
				newBall.GetComponent<SpringJoint2D>().enabled = true;
				newBall.transform.parent = this.transform.parent;
				newBall.GetComponent<PlayerController1New>().freeBallCheck = false;
				newBall.GetComponent<TrailRenderer>().enabled = false;
				newBall.transform.localScale = this.transform.localScale;
				// newBall.GetComponent<PlayerController1New>().triggerColl.radius = 0.8f;
				
				Destroy(this.GetComponent<PlayerController1New>());				
				// Animate the fade out
				
				gm.noOfBalls--;
				buttonCanvas.ShowPlayerInfo();
				Debug.Log("BallsDeducting");
				Destroy(gameObject);
				Debug.Log("BallDestroyed");
				
			}
			else
			{
				gm.noOfBalls--;
				Debug.Log("BallOver");	// if ball over and pins left
				// gm.CheckStatus();
				Destroy(gameObject,1f);					
			}			
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.name == "Patch" && freeBallCheck && !inBubble)
		{
			// triggerColl.radius = 0.8f;
			this.gameObject.layer = 0;
			trajectory.InstantiateTrajectoryPoints();
			trajectory.isBallThrown =false;
			newBallCheck = false;
			spring.enabled = true;
			tr.enabled = false;
			transform.position = initialPos;
			patch.GetComponent<BoxCollider2D>().enabled = false;
			patch.GetComponent<SpringJoint2D>().enabled = true;
			if(!gm.mainMenu)
			{
				buttonCanvas.StartCoroutine(buttonCanvas.ShowFreeBall());
			}
			Debug.Log("FreeBall");
			// gm.noOfBalls++;
		}	
		if( other.gameObject.CompareTag("Laser"))
		{
			this.gameObject.layer = 0;
			outside = true;
			StartCoroutine(BallCheck());
			patch.GetComponent<BoxCollider2D>().enabled = false;
			Debug.Log("Laser");
		}	

	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Outside") && !inBubble)
		{
			this.gameObject.layer = 0;
			outside = true;
			if (gameObject.activeInHierarchy)
			{
				StartCoroutine(BallCheck());
			}
			patch.GetComponent<BoxCollider2D>().enabled = false;
			Debug.Log("Outside");
			
		}
	}

	int magnet=0;
	void OnCollisionStay2D(Collision2D other)
	{
		if(other.collider.CompareTag("Magnet"))
		{
			magnet++;
			if(magnet==70)
			{
				this.gameObject.layer = 0;
				outside = true;
				StartCoroutine(BallCheck());
				patch.GetComponent<BoxCollider2D>().enabled = false;				
				Debug.Log("Magnet");
				magnet =0;
			}
		}
	}

	


	
	
}
