using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using UnityEngine.Analytics;
using GoogleMobileAds.Samples;

public class ButtonCanvas : MonoBehaviour {


	public GameObject winPanel, losePanel, LoadingScreen;
	public Animator infoPanel;
	public Text ballsText, pinsText;
	public Animator[] stars;
	public string[] winSlogans;
	public string[] loseSlogans;
	public Text winText, loseText, levelNumber;
	public GameObject freeball;
	GameManagerNew gm;
	public GameObject sling;
	
	
	void OnEnable()
	{
		Analytics.CustomEvent("Level" + SceneManager.GetActiveScene().name);	
	}

	void Start () 
	{
		this.GetComponent<Canvas>().worldCamera = Camera.main;
		Time.timeScale = 1;
		gm = GameObject.FindGameObjectWithTag("GameManagerBouncy").GetComponent<GameManagerNew>();
		infoPanel.SetTrigger("Down");
		loseText.text = loseSlogans[Random.Range(0,loseSlogans.Length)];
		winText.text = winSlogans[Random.Range(0,winSlogans.Length)];
		levelNumber.text = "Level " + SceneManager.GetActiveScene().name;
	}
	
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			LevelSelection();
		}
		if(infoPanel.GetCurrentAnimatorStateInfo(0).IsName("UIInfoPanelDown") 
			&& Input.GetMouseButtonDown(0))
		{
			infoPanel.SetTrigger("Up");
		}
		ballsText.text = "X   " + gm.noOfBalls.ToString();
		pinsText.text = "X   " + gm.noOfPins.ToString();

	}


	public void Restart()
	{
		GameManagerNew.Instance.DisableObjects();
		if (AdsManager.Instance)
		{
			AdsManager.Instance.ShowInterstitial();
		}
		StartCoroutine(LoadingRestart());
	}
	public void LevelSelection()
	{
        GameManagerNew.Instance.DisableObjects();
        if (AdsManager.Instance)
        {
            AdsManager.Instance.ShowInterstitial();
        }
        StartCoroutine(LoadingHome());
	}
	public void Next()
	{
        GameManagerNew.Instance.DisableObjects();
        if (AdsManager.Instance)
        {
            AdsManager.Instance.ShowInterstitial();
        }
        StartCoroutine(LoadingNext());
	}
	IEnumerator LoadingNext()
	{
		LoadingScreen.SetActive(true);
        losePanel.SetActive(false);

        yield return new WaitForSecondsRealtime(8f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    IEnumerator LoadingRestart()
    {
		
        LoadingScreen.SetActive(true);
		
        losePanel.SetActive(false);

        yield return new WaitForSecondsRealtime(8f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    IEnumerator LoadingHome()
    {
        LoadingScreen.SetActive(true);
		
		losePanel.SetActive(false);
        yield return new WaitForSecondsRealtime(8f);
        PlayerPrefs.SetInt("LevelNumber", SceneManager.GetActiveScene().buildIndex - 2);
        SceneManager.LoadScene("MainMenu1");
    }



    public void ShowPlayerInfo()
	{
		if(infoPanel.GetCurrentAnimatorStateInfo(0).IsName("UIInfoPanelUp"))
		{
			infoPanel.SetTrigger("Down");
		}		
	}
	public IEnumerator ShowFreeBall()
	{
		freeball.SetActive(true);
		yield return new WaitForSeconds(1.1f);
		freeball.SetActive(false);
	}
	public void Win()
	{
		if(!losePanel.activeSelf)
		{
			winPanel.SetActive(true);
			if(PlayerPrefs.GetInt("Stars")==3)
			{
				stars[0].enabled = true;
				stars[1].enabled = true;
				stars[2].enabled = true;
			}
			else if(PlayerPrefs.GetInt("Stars")==2)
			{
				stars[0].enabled = true;
				stars[1].enabled = true;
			}
			else if(PlayerPrefs.GetInt("Stars")==1)
			{
				stars[0].enabled = true;				
			}	
			
			PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);
		}
	}
	public void Fail()
	{
		if(!winPanel.activeSelf)
		{
			losePanel.SetActive(true);
			




		}
	}

	public void Forward()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
	}
	public void Backward()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
	}

	
}
