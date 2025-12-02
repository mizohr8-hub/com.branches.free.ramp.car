using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pins : MonoBehaviour {

	public float minForce;
	Color hitColor = Color.red;
	public GameObject particles;
	SpriteRenderer sr;
	public bool hinged, chained, teleport, movingPlatform;
	HingeJoint2D hingeJointHolder;
	GameManagerNew gm;
	bool checkGm = true;
	Vector3 teleportA;
	public Vector3 teleportB;
	int teleportPos;
	public float teleportInterval = 2f;
	public GameObject teleportParticles;
	float fadeSpeed = 8f;
	public float fadeAfter = 3f;
	bool dead = false;
	AudioSource aS;
	// public Sprite crushedCup;
	// public GameObject pinImpact;
	public AudioClip[] shatter;

	void Start () 
	{
		sr = this.GetComponent<SpriteRenderer>();	
		hingeJointHolder = this.GetComponent<HingeJoint2D>();
		gm = GameObject.FindGameObjectWithTag("GameManagerBouncy").GetComponent<GameManagerNew>();
		gm.AddPins();
		if(teleport)
		{
			teleportA = transform.localPosition;
			teleportPos = 0;
			InvokeRepeating("Teleport", teleportInterval, teleportInterval);			
		}
		aS = this.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
	{	
		if(sr.color == hitColor && checkGm)
		{
			checkGm = false;
			teleport = false;
			gm.MinusPins();						
		}
		if(sr.color == hitColor)
		{
			dead = true;
			StartCoroutine(FadeAway());
		}
		
	}

	void Teleport()
	{
		Instantiate(teleportParticles,teleportA, Quaternion.identity);
		Instantiate(teleportParticles,teleportB, Quaternion.identity);
		if(teleportPos == 0)
		{
			Debug.Log("Teleporting");	
			transform.localPosition = teleportB;
			teleportPos =1;
		}
		else if(teleportPos == 1)
		{
			Debug.Log("Teleporting");	
			transform.localPosition = teleportA;
			teleportPos =0;
		}	
		
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.collider.CompareTag ("Player") && !dead)
		{
			if(hinged && hingeJointHolder)
			{
				ColorChangeHingedParent();		
			}			
			else if(chained)
			{
				 ColorChange();
				 Destroy(this.GetComponent<HingeJoint2D>());
			}
			else if(movingPlatform)
			{
				 ColorChange();
				 this.transform.SetParent(null);
			}
			else
			{
				ColorChange();
			}
			ContactPoint2D contact = other.contacts[0];
			Instantiate(particles, contact.point, Quaternion.identity);
			
			aS.clip = shatter[Random.Range(0,shatter.Length)];
			aS.Play();
		}

		if(other.relativeVelocity.magnitude >= minForce && !dead)
		{
			if(hinged && hingeJointHolder)
			{
				ColorChangeHingedParent();		
			}
			else if(chained)
			{
				 ColorChange();
				 Destroy(this.GetComponent<HingeJoint2D>());
			}
			else if(movingPlatform)
			{
				 ColorChange();
				 this.transform.SetParent(null);
			}
			else
			{
				ColorChange();
			}
			ContactPoint2D contact = other.contacts[0];
			Instantiate(particles, contact.point, Quaternion.identity);
			
			aS.clip = shatter[Random.Range(0,shatter.Length)];
			aS.Play();
		}
		// Debug.Log(other.relativeVelocity.magnitude);
	}

	public void ColorChange()
	{
		sr.color = hitColor;		
		// Instantiate(pinImpact, transform.position, Quaternion.identity);
		// Instantiate(shatterPieces, transform.position, Quaternion.identity);
	}

	public void ColorChangeHingedParent()
	{
		sr.color = hitColor;
		// Instantiate(pinImpact, transform.position, Quaternion.identity);
		// Instantiate(shatterPieces, transform.position, Quaternion.identity);
		// hingeJointHolder.connectedBody.isKinematic = false;
		if(this.transform.parent)
		{
			if(this.transform.parent.childCount>1)
			{
				Destroy(this.transform.parent.GetChild(1).gameObject);
			}
			Destroy(this.transform.parent.GetComponent<FixedJoint2D>());
		}
		ParentMoving();
		this.transform.SetParent(null);
		Destroy(this.GetComponent<HingeJoint2D>());	
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.name=="Down" && !dead)
		{
			ColorChange();			
		}
	}

	IEnumerator FadeAway()
	{
		yield return new WaitForSeconds(fadeAfter);
		
		Color fade = Color.Lerp(sr.color, Color.clear, fadeSpeed*Time.deltaTime);
		sr.color = fade;
		if(sr.color == Color.clear)
		{
			Destroy(gameObject);
		}
		// Debug.Log("Fading");
	}

	void ParentMoving()
	{
		if(this.GetComponentInParent<Animator>())
		{
			Destroy(this.GetComponentInParent<Animator>());
		}
		else if(this.GetComponentInParent<CircularMotion>())
		{
			Destroy(this.GetComponentInParent<CircularMotion>());
		}
	}
}
