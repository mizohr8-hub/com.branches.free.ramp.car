using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideDestroy : MonoBehaviour {

	AudioSource aS;
	void Start () 
	{
		aS = this.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			if(other.GetComponent<PlayerController1New>())
			{
				if(!other.GetComponent<PlayerController1New>().inBubble)
				{
					
					aS.Play();
					Destroy(other.gameObject, 1.5f);
				}
			}
			else
			{
				Destroy(other.gameObject, 1.5f);
			}
		}
		else if (!other.gameObject.CompareTag("Bubble"))
		{
			Destroy(other.gameObject, 1.5f);
		}	
		Debug.Log(other.gameObject.name);	
	}
}
