using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GoogleMobileAds.Samples;


public class GameManagerOFFRoad : MonoBehaviour
{
    public static GameManagerOFFRoad instance;

    public Transform PlayerGameObject;
    public GameObject[] playerCarsOffRoad;
    public Transform[] playerStartingPoint;
    public GameObject gameWinPanel;
    public Text levelNo;
    public GameObject levelCompleteImage;
   
    public GameObject nextBtnForFinalLevel;
    public GameObject nextBtnForNewMode;
    public GameObject gameFailPanel;
    public AudioSource Clips;
    public AudioClip buzzarClip;
    public AudioClip levelCompleteClip;
    public ParticleSystem Confetti1;
    public ParticleSystem Confetti2;
    public AudioSource BGM;

    public GameObject giftPanelOffRoad;

    public GameObject[] lights;

    public GameObject loadingScreenOFFR;

    private void Awake()
    {
        Invoke(nameof(AwakeInit), 0.1f);
    }
     public void AwakeInit()
    {
        playerCarsOffRoad[PlayerPrefs.GetInt("OurSelectedCar")].SetActive(true);
        BGM.Play();
        levelNo.text = ("Level NO: " + (PlayerPrefs.GetInt("OFFRoadMode") + 1).ToString());
        instance = this;

        playerCarsOffRoad[PlayerPrefs.GetInt("OurSelectedCar")].transform.position = playerStartingPoint[PlayerPrefs.GetInt("OFFRoadMode")].transform.position;
        playerCarsOffRoad[PlayerPrefs.GetInt("OurSelectedCar")].transform.rotation = playerStartingPoint[PlayerPrefs.GetInt("OFFRoadMode")].transform.rotation;
        //PlayerGameObject.transform.position = playerStartingPoint[PlayerPrefs.GetInt("OFFRoadMode")].transform.position;
        //PlayerGameObject.transform.rotation = playerStartingPoint[PlayerPrefs.GetInt("OFFRoadMode")].transform.rotation;

        if (PlayerPrefs.GetInt("RemoveAds") != 1)
        {
            if (AdsManager.Instance)
                AdsManager.Instance.HideBanner();
            if (AdsManager.Instance)
                AdsManager.Instance.ShowAdaptiveBanner();//farwa  ADS Nov
        }

        PlayerPrefs.SetInt("FirstAdSkip", 1);
        GameAnalyticsSDK.GameAnalytics.NewProgressionEvent(GameAnalyticsSDK.GAProgressionStatus.Start, "OFFRoadMode Level " + PlayerPrefs.GetInt("OFFRoadMode") + " Start");//farwa//farwa GA Nov
    }


    public void GameWin()
    {
        if (PlayerPrefs.GetInt("RemoveAds") != 1)
        {
            if (AdsManager.Instance)
                AdsManager.Instance.HideBanner();//farwa  ADS Nov
        }

        
        gameWinPanel.SetActive(true);
        

    }

    public void NextBtn()
    {

        PlayerPrefs.SetInt("OFFRoadMode", PlayerPrefs.GetInt("OFFRoadMode") + 1);
        if (PlayerPrefs.GetInt("RemoveAds") != 1)
        {
            if (AdsManager.Instance)
                AdsManager.Instance.HideBanner();
            if (AdsManager.Instance)
                AdsManager.Instance.ShowInterstitial();//farwa  ADS Nov
        }
        Time.timeScale = 1f;
        //SceneManager.LoadScene("OffRoad Mode");
        StartCoroutine(LoadScene1("OffRoad Mode"));

    }
    public void NextBtnForOtherMode()
    {
        PlayerPrefs.SetInt("ParkingModeLevel",1);
        //SceneManager.LoadScene("Parking Mode");
        StartCoroutine(LoadScene1("Parking Mode"));
    }
    public void RestartBtn()
    {
        //if (PlayerPrefs.GetInt("RemoveAds") != 1)
        //{
        //    if (AdsManager.Instance)
        //        AdsManager.Instance.DestroyBannerAd();
        //    if (AdsManager.Instance)
        //        AdsManager.Instance.ShowInterstitial();//farwa  ADS Nov
        //}
        Time.timeScale = 1f;
      //  SceneManager.LoadScene("OffRoad Mode");
        StartCoroutine(LoadScene1("OffRoad Mode"));
    }

    public void RestartBtnOnLevelComplete()
    {
        if (PlayerPrefs.GetInt("RemoveAds") != 1)
        {
            if (AdsManager.Instance)
                AdsManager.Instance.HideBanner();
            if (AdsManager.Instance)
                AdsManager.Instance.ShowInterstitial();//farwa  ADS Nov
        }
        Time.timeScale = 1f;
       // PlayerPrefs.SetInt("OFFRoadMode",PlayerPrefs.GetInt("OFFRoadMode")-1);
        //SceneManager.LoadScene("OffRoad Mode");
        StartCoroutine(LoadScene1("OffRoad Mode"));
    }
    public void MainMeun()
    {
        if (PlayerPrefs.GetInt("RemoveAds") != 1)
        {
            //if (AdsManager.Instance)
            //    AdsManager.Instance.DestroyBannerAd();
            //if (AdsManager.Instance)
            //    AdsManager.Instance.ShowInterstitial();
        }
        Time.timeScale = 1f;
        //SceneManager.LoadScene("MainMenu");
        StartCoroutine(LoadScene1("MainMenu"));
    }

    public void MainMenuLevelComplete()
    {
        if (PlayerPrefs.GetInt("RemoveAds") != 1)
        {
            if (AdsManager.Instance)
                AdsManager.Instance.HideBanner();
            if (AdsManager.Instance)
                AdsManager.Instance.ShowInterstitial();
        }
        Time.timeScale = 1f;
        //SceneManager.LoadScene("MainMenu");
        StartCoroutine(LoadScene1("MainMenu"));
    }
    public void Pause()
    {
        BGM.Pause();
        Time.timeScale = 0f;
       
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        BGM.Play();
    }
    

    public void TurnOnLights()
    {
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].SetActive(true);
        }
    }

    public void TurnOffLights()
    {
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].SetActive(false);
        }
    }

    IEnumerator LoadScene1(string S)
    {
        loadingScreenOFFR.SetActive(true);
        yield return new WaitForSecondsRealtime(3f);
        SceneManager.LoadScene(S);

        StopCoroutine(LoadScene1(S));
    }
}
