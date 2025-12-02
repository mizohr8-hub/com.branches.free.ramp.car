using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrickGameManager;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

	public GameObject[] levels;
	public GameObject playerController;
	//	private int currentLevel;
	private GameObject currentLevelGameObject;

	public static GameController GControlManager;
	//	public GameObject uiPanel;
	//	public Text levelNumberText;

	//	public GameObject levelNoText;
	//	public GameObject LevelStartUpSound;
	//	public Button NextBtn, BackBtn;
	//store boundary world positions

	// Use this for initialization
	void Start ()
	{
		PlayerPrefs.SetInt ("levelNos",0);


		if (PlayerPrefs.GetInt ("levelNos") >= levels.Length) {
			PlayerPrefs.SetInt ("levelNos", levels.Length - 1);
		}


		GameManagerBouncy.instance.CurrentLevel = PlayerPrefs.GetInt ("levelNos");
		SetUpLevel ();
		GControlManager = this;
	}

	/// <summary>
	/// Sets up level.
	/// </summary>
	 public void SetUpLevel ()
	{
		
		StartCoroutine (SetUpGame (0));
	}

	IEnumerator SetUpGame (float waitTime)
	{
		GameManagerBouncy.instance.CurrentGameState = GameState.PlayState;
		yield return new WaitForSeconds (waitTime);
		Destroy (currentLevelGameObject);
		currentLevelGameObject = (GameObject)GameObject.Instantiate (levels [GameManagerBouncy.instance.CurrentLevel]);
		ThemeController.InstanceTheme.setTheme (GameManagerBouncy.instance.CurrentLevel+1);
	//	AdMobManager._AdMobInstance.loadInterstitial ();
	}


	/// <summary>
	/// Raises the level completion event.
	/// </summary>
	public void OnLevelCompletion ()
	{

		GameManagerBouncy.instance.CurrentGameState = GameState.CompletedState;
//		onNextLevel ();
	}

	public void onNextLevel ()
	{
	//	AdMobManager._AdMobInstance.showInterstitial ();
		GameManagerBouncy.instance.CurrentLevel++;
		//For Lock 
		if (GameManagerBouncy.instance.CurrentLevel > PlayerPrefs.GetInt ("levelNos")) {
			PlayerPrefs.SetInt ("levelNos", GameManagerBouncy.instance.CurrentLevel);

		}
		// End
		if (GameManagerBouncy.instance.CurrentLevel >= levels.Length) {
			GameManagerBouncy.instance.CurrentLevel = 0;

		}

		SetUpLevel ();
	}

	public void onPreviousLevel ()
	{
		GameManagerBouncy.instance.CurrentLevel--;
		if (GameManagerBouncy.instance.CurrentLevel < 0)
			GameManagerBouncy.instance.CurrentLevel = 0;

		SetUpLevel ();
	}

	public void onNextClick ()
	{
		if (GameManagerBouncy.instance.CurrentLevel < PlayerPrefs.GetInt ("levelNos")) {  //For Lock
			GameManagerBouncy.instance.CurrentLevel++;
			if (GameManagerBouncy.instance.CurrentLevel >= levels.Length)
				GameManagerBouncy.instance.CurrentLevel = 0;

			SetUpLevel ();
		}
	}
// End


	public void loadAdOnGameSetup ()
	{

	}



	
}
