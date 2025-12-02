using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelctionCanvas : MonoBehaviour {

	public RectTransform levelContainer;
	public GameObject LoadingScreen;
	public float difference = 1518f;
	public float initialValue = 13922f;
	// public float value = 327;
	// public float initialValue = 10074;

	void Start () 
	{
		
		// float val = PlayerPrefs.GetInt("LevelNumber");
		
		ParticularLevel();			
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			SceneManager.LoadScene("MainMenu1");
		}
	}

	public void GoToLevel (string levelNumber)
	{		
		StartCoroutine(Loading(levelNumber));
	}

	public void UnlockAll()
	{
		for (int i = 0; i < 250; i++)
		{
			PlayerPrefs.SetInt(i.ToString(),1);
		}
		Debug.Log("AllUnlocked");
	}

	void ParticularLevel()
	{
		if(PlayerPrefs.GetInt("LevelNumber") >0 && PlayerPrefs.GetInt("LevelNumber") <13)
		{
			levelContainer.anchoredPosition = new Vector2
										(levelContainer.anchoredPosition.x, initialValue) ;
		}
		else if(PlayerPrefs.GetInt("LevelNumber") >12 && PlayerPrefs.GetInt("LevelNumber") <25)
		{
			levelContainer.anchoredPosition = new Vector2
										(levelContainer.anchoredPosition.x, initialValue-difference*1) ;
		}
		else if(PlayerPrefs.GetInt("LevelNumber") >24 && PlayerPrefs.GetInt("LevelNumber") <37)
		{
			levelContainer.anchoredPosition = new Vector2
										(levelContainer.anchoredPosition.x, initialValue-difference*2) ;
		}
		else if(PlayerPrefs.GetInt("LevelNumber") >36 && PlayerPrefs.GetInt("LevelNumber") <49)
		{
			levelContainer.anchoredPosition = new Vector2
										(levelContainer.anchoredPosition.x, initialValue-difference*3) ;
		}
		else if(PlayerPrefs.GetInt("LevelNumber") >48 && PlayerPrefs.GetInt("LevelNumber") <61)
		{
			levelContainer.anchoredPosition = new Vector2
										(levelContainer.anchoredPosition.x, initialValue-difference*4) ;
		}
		else if(PlayerPrefs.GetInt("LevelNumber") >60 && PlayerPrefs.GetInt("LevelNumber") <73)
		{
			levelContainer.anchoredPosition = new Vector2
										(levelContainer.anchoredPosition.x, initialValue-difference*5) ;
		}
		else if(PlayerPrefs.GetInt("LevelNumber") >72 && PlayerPrefs.GetInt("LevelNumber") <85)
		{
			levelContainer.anchoredPosition = new Vector2
										(levelContainer.anchoredPosition.x, initialValue-difference*6) ;
		}
		else if(PlayerPrefs.GetInt("LevelNumber") >84 && PlayerPrefs.GetInt("LevelNumber") <97)
		{
			levelContainer.anchoredPosition = new Vector2
										(levelContainer.anchoredPosition.x, initialValue-difference*7) ;
		}
		else if(PlayerPrefs.GetInt("LevelNumber") >96 && PlayerPrefs.GetInt("LevelNumber") <109)
		{
			levelContainer.anchoredPosition = new Vector2
										(levelContainer.anchoredPosition.x, initialValue-difference*8) ;
		}
		else if(PlayerPrefs.GetInt("LevelNumber") >108 && PlayerPrefs.GetInt("LevelNumber") <121)
		{
			levelContainer.anchoredPosition = new Vector2
										(levelContainer.anchoredPosition.x,initialValue-difference*9) ;
		}
		else if(PlayerPrefs.GetInt("LevelNumber") >120 && PlayerPrefs.GetInt("LevelNumber") <133)
		{
			levelContainer.anchoredPosition = new Vector2
										(levelContainer.anchoredPosition.x, initialValue-difference*10) ;
		}
		else if(PlayerPrefs.GetInt("LevelNumber") >132 && PlayerPrefs.GetInt("LevelNumber") <145)
		{
			levelContainer.anchoredPosition = new Vector2
										(levelContainer.anchoredPosition.x, initialValue-difference*11) ;
		}
		else if(PlayerPrefs.GetInt("LevelNumber") >144 && PlayerPrefs.GetInt("LevelNumber") <157)
		{
			levelContainer.anchoredPosition = new Vector2
										(levelContainer.anchoredPosition.x, initialValue-difference*12) ;
		}
		else if(PlayerPrefs.GetInt("LevelNumber") >156 && PlayerPrefs.GetInt("LevelNumber") <169)
		{
			levelContainer.anchoredPosition = new Vector2
										(levelContainer.anchoredPosition.x, initialValue-difference*13) ;
		}
		else if(PlayerPrefs.GetInt("LevelNumber") >168 && PlayerPrefs.GetInt("LevelNumber") <181)
		{
			levelContainer.anchoredPosition = new Vector2
										(levelContainer.anchoredPosition.x, initialValue-difference*14) ;
		}
		else if(PlayerPrefs.GetInt("LevelNumber") >180 && PlayerPrefs.GetInt("LevelNumber") <193)
		{
			levelContainer.anchoredPosition = new Vector2
										(levelContainer.anchoredPosition.x, initialValue-difference*15) ;
		}
		else if(PlayerPrefs.GetInt("LevelNumber") >192 && PlayerPrefs.GetInt("LevelNumber") <205)
		{
			levelContainer.anchoredPosition = new Vector2
										(levelContainer.anchoredPosition.x, initialValue-difference*16) ;
		}
		else if(PlayerPrefs.GetInt("LevelNumber") >204 && PlayerPrefs.GetInt("LevelNumber") <217)
		{
			levelContainer.anchoredPosition = new Vector2
										(levelContainer.anchoredPosition.x, initialValue-difference*17) ;
		}
		else if(PlayerPrefs.GetInt("LevelNumber") >216 && PlayerPrefs.GetInt("LevelNumber") <229)
		{
			levelContainer.anchoredPosition = new Vector2
										(levelContainer.anchoredPosition.x, initialValue-difference*18) ;
		}
		else if(PlayerPrefs.GetInt("LevelNumber") >228 && PlayerPrefs.GetInt("LevelNumber") <241)
		{
			levelContainer.anchoredPosition = new Vector2
										(levelContainer.anchoredPosition.x, initialValue-difference*19) ;
		}
		else if(PlayerPrefs.GetInt("LevelNumber") >240 && PlayerPrefs.GetInt("LevelNumber") <251)
		{
			levelContainer.anchoredPosition = new Vector2
										(levelContainer.anchoredPosition.x, initialValue-difference*20) ;
		}
		/* else if(PlayerPrefs.GetInt("LevelNumber") >252 && PlayerPrefs.GetInt("LevelNumber") <563)
		{
			levelContainer.anchoredPosition = new Vector2
										(levelContainer.anchoredPosition.x, initialValue-difference*21) ;
		}
		else if(PlayerPrefs.GetInt("LevelNumber") >264 && PlayerPrefs.GetInt("LevelNumber") <185)
		{
			levelContainer.anchoredPosition = new Vector2
										(levelContainer.anchoredPosition.x, initialValue-difference*22) ;
		} */

		PlayerPrefs.SetInt("LevelNumber", 0);
	}

    IEnumerator Loading(string SceneName)
    {
        LoadingScreen.SetActive(true);
        yield return new WaitForSeconds(8f);
        SceneManager.LoadScene(SceneName);
    }
    public void Home()
    {
        StartCoroutine(Loading("MainMenu1"));
    }
}
