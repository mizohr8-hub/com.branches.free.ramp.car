using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUnlock : MonoBehaviour {

	public int previousLevel;
	public Sprite locked, unlockedHolder;
	public bool lvl1;
	Text text;
	Image pinImage/* , pinImage */;
	Button button;

	
	void Update()
	{
		pinImage = this.transform.GetChild(0).GetComponent<Image>();
		// pinImage= this.transform.GetChild(0).GetComponent<Image>();		
		button = this.GetComponent<Button>();
		text = this.GetComponentInChildren<Text>();
		text.transform.SetParent(pinImage.transform);
		if(PlayerPrefs.GetInt(previousLevel.ToString()) == 1 || lvl1)
		{
			pinImage.sprite = unlockedHolder;
			pinImage.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
			text.enabled = true;
			text.text = (previousLevel + 1).ToString();	
			button.interactable = true;
		}	
		else
		{
			pinImage.sprite = locked;
			pinImage.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
			text.enabled = false;
			button.interactable = false;
		}
		if(Input.GetKeyDown(KeyCode.D))
		{
			PlayerPrefs.DeleteAll();
		}
	}
	
}
