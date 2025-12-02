using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Samples;

public class MMCanvas : MonoBehaviour 
{

	public GameObject LoadingScreen;

	void Start () 
	{
		PlayerPrefs.SetInt("Banner",0);
	}
	
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(PlayerPrefs.GetInt("Banner") == 0)
			{
				PlayerPrefs.SetInt("Banner",1);
			}
			else if(PlayerPrefs.GetInt("Banner") == 1)
			{
				Application.Quit();
			}
		}
	}

	public void Home()
    {
		LoadingScreen.SetActive(true);
		SceneManager.LoadScene("MainMenu");
    }
	public IEnumerator Loading(string SceneName)
	{
		LoadingScreen.SetActive(true);
		yield return new WaitForSeconds(8f);
        SceneManager.LoadScene(SceneName);
    }

    public void LevelSelection()
	{
		//SceneManager.LoadScene("LevelSelection");
		if (AdsManager.Instance)
		{
			AdsManager.Instance.ShowInterstitial();
		}
		StartCoroutine(Loading("LevelSelection"));
	}
    public void CharacterSelection()
    {
        //SceneManager.LoadScene("CharacterSelection");
        if (AdsManager.Instance)
        {
            AdsManager.Instance.ShowInterstitial();
        }
        StartCoroutine(Loading("CharacterSelection"));
    }

    public void Banner()
	{		
		
	}
	public void Close()
	{
		
	}
	
}
