using GoogleMobileAds.Samples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerNew : MonoBehaviour {


	public static GameManagerNew Instance;

	public MMCanvas Canvas;

	public float popupDelay = 2f;
	public int noOfBalls = 3;
	public int noOfPins =0;
	public GameObject confetti;
	float timer = 0f;
	// bool checkPins = true;
	public bool gameOver, win, stop, mainMenu;
	ButtonCanvas buttonCanvas;

	public GameObject sling;





	private void Awake()
    {
        if(Instance == null)
		{
			Instance = this;
		}
		

	}
    void Start()
	{
		if(!mainMenu)
		{
			buttonCanvas = GameObject.FindGameObjectWithTag("ButtonCanvas").GetComponent<ButtonCanvas>();
		}

		if (AdsManager.Instance)
		{
			AdsManager.Instance.HideAdaptiveBanner();
		}

		Screen.orientation = ScreenOrientation.Portrait;
	}
	public void Home()
    {

		StartCoroutine(Canvas.Loading("MainMenu"));
		
	}

	void Update()
	{
		if(!stop)
		{
			timer += Time.deltaTime;
			if(timer>= 1f)
			{
				CheckingPins();
			}			
		}
		//if(noOfPins==0 && noOfBalls>0 && !mainMenu)
		//{
		//	GameObject.FindGameObjectWithTag("Player").layer = 2;
		//}
	}

	public void AddPins()
	{
		noOfPins++;
	}
	public void MinusPins()
	{
		noOfPins--;
	}	

	public void DisableObjects()
	{
		confetti.SetActive(false);
		GameObject Player = GameObject.FindGameObjectWithTag("Player");
		if (Player != null)
		{
			if (Player.transform.parent)
				Player.transform.parent.gameObject.SetActive(false);
			Player.SetActive(false);
		}
    }



    void CheckingPins()
	{			
		if(noOfPins <=0)
		{
			// win = true;	
			stop = true;
			// CheckStatus();
			PlayerPrefs.SetInt("Stars", noOfBalls);
			Debug.Log("Stars- " + noOfBalls);			
		}	
		CheckStatus();		
	}

	IEnumerator OnLose()
	{
		yield return new WaitForSeconds(popupDelay);
		buttonCanvas.Fail();
		sling.SetActive(false);
		

		Debug.Log("GameOver");
	}

	IEnumerator OnWin()
	{

		yield return new WaitForSeconds(popupDelay);
		if(!mainMenu && !gameOver)		
		{
			buttonCanvas.Win();
			sling.SetActive(false);
			Instantiate(confetti);
			Debug.Log("YouWin!");
			
		}
		
	}

	public void CheckStatus()
	{
		if(noOfBalls <= 0 && noOfPins>0 || gameOver)
		{
			StartCoroutine(OnLose());
			
		}
		else if(noOfPins<=0 && noOfBalls>=0)
		{
			StartCoroutine(OnWin());
			
		}
	}

	

}
