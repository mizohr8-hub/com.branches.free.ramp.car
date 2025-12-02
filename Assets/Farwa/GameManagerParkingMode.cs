using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GoogleMobileAds.Samples;

public class GameManagerParkingMode : MonoBehaviour
{
    public static GameManagerParkingMode instance;

    public Transform PlayerGameObject;
    public GameObject[] playerCars, carLights;
    public Transform[] playerStartingPoint;
    public Text levelNo;
    public GameObject levelCompleteImage;
    public GameObject giftPanel;
    public GameObject gameWinPanel;
    public GameObject nextBtnForFinalLevel;
    public GameObject nextBtnForNewMode;
    public GameObject gameFailPanel;
    public AudioSource Clips;
    public AudioClip buzzarClip;
    public AudioClip levelCompleteClip;
    public ParticleSystem Confetti1;
    public ParticleSystem Confetti2;
    public AudioSource BGM;
    public GameObject loadingScreen;
    private void Awake()
    {
        Invoke(nameof(AwakeInit), 0.1f);
    }
    
    public void AwakeInit()
    {
        playerCars[PlayerPrefs.GetInt("SelectedPlayer")].SetActive(true);/*PlayerPrefs.GetInt("OurSelectedCar")*/
        BGM.Play();
        levelNo.text = ("Level NO: " + (PlayerPrefs.GetInt("ParkingModeLevel") + 1).ToString());
        instance = this;

        // PlayerGameObject.transform.position = playerStartingPoint[PlayerPrefs.GetInt("ParkingModeLevel")].transform.position;
        // PlayerGameObject.transform.rotation = playerStartingPoint[PlayerPrefs.GetInt("ParkingModeLevel")].transform.rotation;
        playerCars[0].transform.position = playerStartingPoint[PlayerPrefs.GetInt("ParkingModeLevel")].transform.position; /*PlayerPrefs.GetInt("OurSelectedCar")*/
        playerCars[0].transform.rotation = playerStartingPoint[PlayerPrefs.GetInt("ParkingModeLevel")].transform.rotation;

        if (PlayerPrefs.GetInt("RemoveAds") != 1)
        {
            if (AdsManager.Instance)
                AdsManager.Instance.HideBanner();
            if (AdsManager.Instance)
                AdsManager.Instance.ShowAdaptiveBanner();//farwa  ADS Nov
        }

        PlayerPrefs.SetInt("FirstAdSkip", 1);
        GameAnalyticsSDK.GameAnalytics.NewProgressionEvent(GameAnalyticsSDK.GAProgressionStatus.Start, "ParkingMode Level " + PlayerPrefs.GetInt("ParkingModeLevel") + " Start");//farwa//farwa GA Nov
    }
    public void NextBtn()
    {
        PlayerPrefs.SetInt("ParkingModeLevel", PlayerPrefs.GetInt("ParkingModeLevel") + 1);

        if (PlayerPrefs.GetInt("RemoveAds") != 1)
        {
            if (AdsManager.Instance)
                AdsManager.Instance.HideBanner();
            if (AdsManager.Instance)
                AdsManager.Instance.ShowInterstitial();//farwa  ADS Nov
        }

       // SceneManager.LoadScene("Parking Mode");
        StartCoroutine(LoadScene("Parking Mode"));
    }
    public void NextBtnForOtherMode()
    {
        PlayerPrefs.SetInt("OFFRoadMode", 1);
        StartCoroutine(LoadScene("OffRoad Mode"));
        //SceneManager.LoadScene("OffRoad Mode");
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
        //SceneManager.LoadScene("Parking Mode");
        StartCoroutine(LoadScene("Parking Mode"));
    }

    public void RestartBtnOnComplete()
    {
        if (PlayerPrefs.GetInt("RemoveAds") != 1)
        {
            if (AdsManager.Instance)
                AdsManager.Instance.HideBanner();
            if (AdsManager.Instance)
                AdsManager.Instance.ShowInterstitial();//farwa  ADS Nov
        }
        Time.timeScale = 1f;
       // SceneManager.LoadScene("Parking Mode");
        StartCoroutine(LoadScene("Parking Mode"));
    }
    public void MainMeun()
    {
        //if (PlayerPrefs.GetInt("RemoveAds") != 1)
        //{
        //    if (AdsManager.Instance)
        //        AdsManager.Instance.DestroyBannerAd();
        //    if (AdsManager.Instance)
        //        AdsManager.Instance.ShowInterstitial();//farwa  ADS Nov
        //}
        Time.timeScale = 1f;
        //SceneManager.LoadScene("MainMenu");
        StartCoroutine(LoadScene("MainMenu"));
    }
    public void MainMeunONLevelComplete()
    {
        if (PlayerPrefs.GetInt("RemoveAds") != 1)
        {
            if (AdsManager.Instance)
                AdsManager.Instance.HideBanner();
            if (AdsManager.Instance)
                AdsManager.Instance.ShowInterstitial();//farwa  ADS Nov
        }
        Time.timeScale = 1f;
        // PlayerPrefs.SetInt("ParkingModeLevel", PlayerPrefs.GetInt("ParkingModeLevel") + 1);

        StartCoroutine(LoadScene("MainMenu"));
       // SceneManager.LoadScene("MainMenu");
    }
    public void Pause()
    {
        Time.timeScale = 0f;
        BGM.Pause();
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        BGM.Play();
    }

    public void LightsOn()
    {
        for (int i = 0; i < carLights.Length; i++)
        {
            carLights[i].SetActive(true);
        }
    }

    public void LightsOff()
    {
        for (int i = 0; i < carLights.Length; i++)
        {
            carLights[i].SetActive(false);
        }
    }

    IEnumerator LoadScene(string S)
    {
        loadingScreen.SetActive(true);
        yield return new WaitForSecondsRealtime(3f);
        SceneManager.LoadScene(S);

        StopCoroutine(LoadScene(S));
    }
}
