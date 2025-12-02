using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class mainMenu : MonoBehaviour {
    public GameObject RemoveAdsBtn,UnockLevelsBtn,UnlockCarsBTn;
    public GameObject noVideoAvailablePanel;
    public static mainMenu instance;
    private AsyncOperation async;
    public Image loadingBar;


    private void Awake()
    {
        instance = this;
    }

    public void Home()
    {
        StartCoroutine(LoadScene("LiveMenu"));
    }
    IEnumerator LoadScene(string levelName)
    {
        async = SceneManager.LoadSceneAsync(levelName);
        while (!async.isDone)
        {
            if (loadingBar)
            {
                Debug.Log("the progress " + async.progress);
                loadingBar.fillAmount = async.progress;
            }

            yield return null;
        }

    }

    private void OnEnable()
    {
        //consola
        //ConsoliAds.onRewardedVideoAdCompletedEvent += GetCoins;
    }
    private void OnDisable()
    {
        //consola
        //ConsoliAds.onRewardedVideoAdCompletedEvent -= GetCoins;
    }
    // Use this for initialization
    void Start () 
    {

        Screen.orientation = ScreenOrientation.Landscape;

        if (PlayerPrefs.GetInt("RemoveAds")==1)
        {
            RemoveAdsBtn.SetActive(false);
        }
        if(PlayerPrefs.GetInt("UnlockAllLevels")==1)
        {
            UnockLevelsBtn.SetActive(false);
        }
        if(PlayerPrefs.GetInt("UnlockAllCars")==1)
        {
            UnlockCarsBTn.SetActive(false);
        }
        //consola
        //ConsoliAds.Instance.LoadRewarded();

        //ConsoliAds.onRewardedVideoAdCompletedEvent += GetCoins;
    }
	
    public void ShowVideo()
    {
        //consola
        //if (ConsoliAds.Instance.IsRewardedVideoAvailable(0))
        //{
        //    ConsoliAds.Instance.ShowRewardedVideo(0);
        //}
        //else
        //{
        //    noVideoAvailablePanel.SetActive(true);
        //}
    }



    void GetCoins()
    {
       int currency= PlayerPrefs.GetInt("Currency");
        currency = currency + 1000;
        PlayerPrefs.SetInt("Currency", currency);

    }

    public void RateUs()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.branches.car.stunt.games");
      //  ConsoliAds.Instance.RateUsURL();
    }
    public void MoreGames()
    {
        Application.OpenURL("https://play.google.com/store/apps/dev?id=4641174712214040615");
        //ConsoliAds.Instance.MoreFunURL();
    }
    public void RemoveAds()
    {
        //GSF_InAppController.Instance.BuyInAppProduct(0);
        PlayerPrefs.SetInt("RemoveAds", 1);
       
        Invoke("InvokDelay", 0.5f);
        //RobotSelection.instance.UnlockAllRobots();
    }
    void InvokDelay()
    {
        mainMenu.instance.RemoveAdsBtn.SetActive(false);
    }
    void InvokDelay2()
    {
        mainMenu.instance.UnockLevelsBtn.SetActive(false);
    }


    public void UnlockAllGame()
    {
        PlayerPrefs.SetInt("UnlockAllGame", 1);
        //RemoveAds
        PlayerPrefs.SetInt("RemoveAds", 1);
        Invoke("InvokDelay", 0.5f);
        //Levels
        PlayerPrefs.SetInt("UnlockAllLevels", 1);
        PlayerPrefs.SetInt("LevelNum", 20);
        Invoke("InvokDelay2", 0.5f);
        //Cars
        RGSK.MenuManager.instance.ClickUnlockBtn();

    }


    public void UnlockAllLevels()
    {
        //GSF_InAppController.Instance.BuyInAppProduct(1);
        PlayerPrefs.SetInt("UnlockAllLevels", 1);
        PlayerPrefs.SetInt("LevelNum", 20);
        Invoke("InvokDelay2", 0.5f);
        //Preferences.Instance.UnlockLevel = 13;
        //Preferences.Instance.LevelMissionUnlock = 25;
    }
}
