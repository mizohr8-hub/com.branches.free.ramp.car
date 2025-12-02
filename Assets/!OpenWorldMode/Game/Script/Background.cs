using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Background : MonoBehaviour {

	
	public Sprite[] bgs;
	SpriteRenderer sr;

	void Start()
	{
		sr = this.GetComponent<SpriteRenderer>();

		if(SceneManager.GetActiveScene().buildIndex >=2 && SceneManager.GetActiveScene().buildIndex <32)
		{
			sr.sprite = bgs[0];
		}
		else if(SceneManager.GetActiveScene().buildIndex >=32 && SceneManager.GetActiveScene().buildIndex <62)
		{
			sr.sprite = bgs[1];
		}
		else if(SceneManager.GetActiveScene().buildIndex >=62 && SceneManager.GetActiveScene().buildIndex <92)
		{
			sr.sprite = bgs[2];
		}
		else if(SceneManager.GetActiveScene().buildIndex >=92 && SceneManager.GetActiveScene().buildIndex <122)
		{
			sr.sprite = bgs[3];
		}
		else if(SceneManager.GetActiveScene().buildIndex >=122 && SceneManager.GetActiveScene().buildIndex <=151)
		{
			sr.sprite = bgs[4];
		}
	}
}
