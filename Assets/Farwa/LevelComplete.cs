using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Samples;

public class LevelComplete : MonoBehaviour
{
    bool stillPlaying = true;
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player" && stillPlaying)
        {
            //other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            // other.gameObject.GetComponent<RCC_CarControllerV3>().KillEngine();
            stillPlaying = false;
           
            GameManagerOFFRoad.instance.levelCompleteImage.SetActive(true);
            if (PlayerPrefs.GetInt("unlockOFFRoadMode") <= PlayerPrefs.GetInt("OFFRoadMode"))
            {
                Invoke(nameof(GiftPlayer), 3f);
            }
            else
            {
                Invoke(nameof(GameWinWait), 3f);
            }


        }
    }

    public void GiftPlayer()
    {
            GameWinWait();
        //if (PlayerPrefs.GetInt("OFFRoadMode") >= 6)
        //{
        //}
        //else
        //{
        //    GameManagerOFFRoad.instance.levelCompleteImage.SetActive(false);
        //    GameManagerOFFRoad.instance.giftPanelOffRoad.SetActive(true);

        //    if (AdsManager.Instance)
        //    {
        //        AdsManager.Instance.HideBanner();
        //    }

        //}
    }
    public void GameWinWait()
    {
        if (PlayerPrefs.GetInt("OFFRoadMode") == 2 && PlayerPrefs.GetInt("Mission1Done") == 0)
        {
            PlayerPrefs.SetInt("Mission1Done", 1); //compplete 3 levels of off road
        }

        if (PlayerPrefs.GetInt("OFFRoadMode") == 29 && PlayerPrefs.GetInt("Mission5Done") == 0)
        {
            PlayerPrefs.SetInt("Mission5Done", 1); //compplete all levels of off road
        }

        GameAnalyticsSDK.GameAnalytics.NewProgressionEvent(GameAnalyticsSDK.GAProgressionStatus.Start, "OFFRoadMode Level " + PlayerPrefs.GetInt("OFFRoadMode") + " Won");//farwa//farwa GA Nov
        
        if (PlayerPrefs.GetInt("RemoveAds") != 1)
        {
            if (AdsManager.Instance)
                AdsManager.Instance.HideBanner();//farwa  ADS Nov
        }

        

        GameManagerOFFRoad.instance.BGM.Pause();
        GameManagerOFFRoad.instance.levelCompleteImage.SetActive(false);
        GameManagerOFFRoad.instance.GameWin();
        if (PlayerPrefs.GetInt("OFFRoadMode") >= 49)
        {
            GameManagerOFFRoad.instance.nextBtnForFinalLevel.SetActive(false);
            GameManagerOFFRoad.instance.nextBtnForNewMode.SetActive(true);
        }
    }
}
