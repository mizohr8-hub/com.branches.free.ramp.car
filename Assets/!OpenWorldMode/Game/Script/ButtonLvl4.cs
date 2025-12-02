using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLvl4 : MonoBehaviour {


	public Sprite off;
	public GameObject target;
	// public Vector3 targetScale;
	public bool pressed, animStart, objDisappear, animStop, cannonStart, fanStart, fanStop, objAppear;
	public float timeToDisappear = 0.5f;
	AudioSource aS;	
	void Start()
	{
		if(animStart)
		{
			target.GetComponent<Animator>().enabled=false;
		}
		else if(cannonStart)
		{
			target.GetComponent<Cannon>().enabled = false;
		}
		else if(fanStart)
		{
			target.GetComponentInChildren<ParticleSystem>().Stop();
			target.GetComponentInChildren<Animator>().enabled = false;
			target.GetComponentInChildren<AreaEffector2D>().enabled = false;
		}
		else if(objAppear)
		{
			target.SetActive(false);
		}
		aS = this.GetComponent<AudioSource>();
	}
	void Update()
	{
		/* if(pressed && !animStart && objDisappear)
		{
			targetColor = Color.Lerp(targetColor, Color.clear, timeToDisappear);
			target.GetComponent<SpriteRenderer>().color = targetColor;
			if(targetColor == Color.clear)
			{
				Destroy(target.gameObject);
			}
		} */
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.collider.CompareTag("Player"))
		{
			
			this.GetComponent<SpriteRenderer>().sprite = off;
			pressed = true;
			// this.transform.localScale = targetScale;
			this.GetComponent<BoxCollider2D>().enabled = false;
			if(animStart)
			{
				if(!target.GetComponent<Animator>().enabled)
				{
					target.GetComponent<Animator>().enabled=true;
				}
				else
				{
					target.GetComponent<Animator>().Play("MovingPlatform");
				}

			}
			else if(objDisappear)
			{
				Destroy(target.gameObject);
			}
			else if(animStop)
			{
				if(target.GetComponent<Animator>())
				{
					Destroy(target.GetComponent<Animator>());
				}
				else
				Destroy(target.GetComponent<TriangleRotator>());
			}
			else if(cannonStart)
			{
				target.GetComponent<Cannon>().enabled = true;
			}
			else if(fanStart)
			{
				target.GetComponentInChildren<ParticleSystem>().Play();
				target.GetComponentInChildren<Animator>().enabled = true;
				target.GetComponentInChildren<AreaEffector2D>().enabled = true;
			}
			else if(fanStop)
			{
				target.GetComponentInChildren<ParticleSystem>().Stop();
				target.GetComponentInChildren<Animator>().enabled = false;
				target.GetComponentInChildren<AreaEffector2D>().enabled = false;			
			}
			else if(objAppear)
		{
			target.SetActive(true);
		}
			aS.Play();
		}
	}


}
