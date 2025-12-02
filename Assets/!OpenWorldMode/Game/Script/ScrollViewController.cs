using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TrickGameManager;

public class ScrollViewController : MonoBehaviour {
	public Button LvlOpenBtn, LvlCloseBtn;
	public Animator levelScrollView;
	public static bool islevelOpen;
	public RectTransform ContentRect;
	public float startContentRectPosX;

	// Use this for initialization
	void Start () {
		startContentRectPosX=ContentRect.localPosition.x;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Escape)) {
			onHomeClick ();
		}
	}
	public void onLevelOpen()
	{
		setLevelScrollPos (GameManagerBouncy.instance.CurrentLevel, 1280);
		levelScrollView.SetTrigger ("OpenTrigger");
		islevelOpen = true;
	}
	public void onLevelClose()
	{
		levelScrollView.SetTrigger ("CloseTrigger");
		islevelOpen = false;
	}
	public void onHomeClick()
	{
		// Call Your Ads Here::::::::::::::::::::::::::::::::::::::::::::::::::::::::
		SceneManager.LoadScene ("Menu");
	}
	public void setLevelScrollPos (int LevelNo,int offset)
	{
		ContentRect.localPosition = new Vector3 ((startContentRectPosX-((LevelNo/10)*offset)), ContentRect.localPosition.y, ContentRect.localPosition.z);
	}
}
