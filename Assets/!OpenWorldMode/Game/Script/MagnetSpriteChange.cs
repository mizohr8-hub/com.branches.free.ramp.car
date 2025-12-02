using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetSpriteChange : MonoBehaviour {

	public Sprite on,off;
	public ParticleSystem[] particles;
	
	void Start()
	{
		particles[0].Stop();
		particles[1].Stop();

	}
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			this.GetComponentInParent<SpriteRenderer>().sprite = on;
			StartCoroutine(particlesPlay());

		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			this.GetComponentInParent<SpriteRenderer>().sprite = off;
		}
	}

	IEnumerator particlesPlay()
	{
		if(!particles[0].isPlaying)
		{
			particles[0].Play();
		}
		if(!particles[1].isPlaying)
		{
			yield return new WaitForSeconds(Random.Range(0,0.2f));
			particles[1].Play();
		}
	}


}
