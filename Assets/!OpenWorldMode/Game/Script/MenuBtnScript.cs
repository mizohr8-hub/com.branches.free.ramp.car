using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuBtnScript : MonoBehaviour
{
	private int soundHitCount;
	public Image SoundBtnImg;
	public Sprite soundOn, SoundOff;
	//	public string moreUrl;
	public enum Stores
	{
		Google,
Amazon,
Samsung,
Yandex}

	;

	public Stores StoreAccounts;
	//	public RectTransform PlayBtn;
	// Use this for initialization
	void Start ()
	{
//		PlayerPrefs.SetInt ("SoundStatus", 0);
		soundHitCount = PlayerPrefs.GetInt ("SoundStatus");
//		if (PlayerPrefs.GetInt ("SoundStatus") == 0) {
//			soundHitCount = 1;
//		} else {
//			soundHitCount = 0;
//		}
//		print("soundHitCount-----"+soundHitCount);
		//StoreAccounts=Stores.Google;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (PlayerPrefs.GetInt ("SoundStatus") == 0) {
			SoundBtnImg.sprite = soundOn;

		} else {
			SoundBtnImg.sprite = SoundOff;
		}

//		PlayBtn.localScale = new Vector3(Mathf.PingPong(0.1f,PlayBtn.localScale.x+0.2f),Mathf.PingPong(0.1f,PlayBtn.localScale.y+0.2f),1);
		if (Input.GetKey (KeyCode.Escape)) {
			onExitClick ();
		}
	}

	public void onPlayClick ()
	{
		// Call Your Ads Here::::::::::::::::::::::::::::::::::::::::::::::::::::::::
		if (PlayerPrefs.GetInt ("IsFirstTime") == 0) {
			PlayerPrefs.SetInt ("IsFirstTime", 1);
			SceneManager.LoadScene ("Help");
		} else {
			SceneManager.LoadScene ("Game");
		}
	}

	public void onSoundClick ()
	{
		soundHitCount++;
		if (soundHitCount % 2 == 0) {
			PlayerPrefs.SetInt ("SoundStatus", 0);
			//SoundBtnImg.sprite = soundOn;

		} else {
			PlayerPrefs.SetInt ("SoundStatus", 1);
			//SoundBtnImg.sprite = SoundOff;
		}
	}

	public void onMoreClick ()
	{
//		Application.OpenURL (moreUrl);
		if (StoreAccounts == Stores.Google) {
			Application.OpenURL ("https://play.google.com/store/apps/dev?id=5581886918361803159&hl=en");
		}
		if (StoreAccounts == Stores.Samsung) {
			Application.OpenURL ("samsungapps://ProductDetail/");
		}
		if (StoreAccounts == Stores.Amazon) {
			Application.OpenURL ("amzn://apps/android?");
		}
		if (StoreAccounts == Stores.Yandex) {
			Application.OpenURL ("yastore://details?");
		}
	}

	public void onExitClick ()
	{
		Application.Quit ();
	}


}
