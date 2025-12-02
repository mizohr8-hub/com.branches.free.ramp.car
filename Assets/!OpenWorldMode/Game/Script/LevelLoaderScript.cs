using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrickGameManager;
using UnityEngine.UI;


public class LevelLoaderScript : MonoBehaviour {
	public Animator ScrollLevel;
	private int intLevelValue;
	private Button btn;
	// Use this for initialization
	void Start () {
		intLevelValue=int.Parse (name) - 1;
		btn = GetComponent<Button> ();
		GetComponentInChildren<Text> ().text =name;
	}
	
	// Update is called once per frame
	void Update () {
		if ( intLevelValue<=PlayerPrefs.GetInt ("levelNos")) {
			btn.interactable = true;
		} else {
			btn.interactable = false;
		}
	}
	public void OnLevelBtnClick()
	{
		GameManagerBouncy.instance.CurrentLevel = intLevelValue;
		GameController.GControlManager.SetUpLevel ();
		ScrollLevel.SetTrigger ("CloseTrigger");
		ScrollViewController.islevelOpen = false;
	}
}
