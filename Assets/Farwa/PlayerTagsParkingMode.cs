using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Samples;
using UnityEngine;

public class PlayerTagsParkingMode : MonoBehaviour
{
    bool notFinished = true;
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ParkingFinish" && notFinished)
        {
            //Debug.LogError("bbbbbbbbbbb");
            notFinished = false;
           
            GameManagerParkingMode.instance.BGM.Pause();
            GameManagerParkingMode.instance.Clips.PlayOneShot(GameManagerParkingMode.instance.levelCompleteClip);
            GameManagerParkingMode.instance.levelCompleteImage.SetActive(true);

            if (PlayerPrefs.GetInt("ParkingModeLevel")==4 && PlayerPrefs.GetInt("Mission2Done") == 0)
            {
                PlayerPrefs.SetInt("Mission2Done", 1); //complete 5 levels of parking mode
            }


            if (PlayerPrefs.GetInt("ParkingModeLevel") == 14 && PlayerPrefs.GetInt("Mission4Done") == 0)
            {
                PlayerPrefs.SetInt("Mission4Done", 1); //complete 15 levels of parking mode
            }

            GameManagerParkingMode.instance.Confetti1.Play();
            GameManagerParkingMode.instance.Confetti2.Play();

            //if (PlayerPrefs.GetInt("unlockParkingModeLevel") <= PlayerPrefs.GetInt("ParkingModeLevel"))
            //{
            //    Invoke(nameof(GiftPlayer), 4f);
            //}
            //else
            //{
                Invoke(nameof(GameWinPanel), 4f);
            //}
            GetComponent<RCC_CarControllerV3>().KillEngine();
            GetComponent<Rigidbody>().isKinematic = true;

        }
        if (other.tag == "Enemy")
        {
            //Debug.LogError("conee");
            GameManagerParkingMode.instance.BGM.Pause();
            GameManagerParkingMode.instance.Clips.PlayOneShot(GameManagerParkingMode.instance.buzzarClip);
            Invoke(nameof(GameFailPanel), 1.5f);
            GetComponent<RCC_CarControllerV3>().KillEngine();
            GetComponent<Rigidbody>().isKinematic = true;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
         
            GameManagerParkingMode.instance.Clips.PlayOneShot(GameManagerParkingMode.instance.levelCompleteClip);

            GameManagerParkingMode.instance.BGM.Pause();
            Invoke(nameof(GameWinPanel), 4f);
            GetComponent<RCC_CarControllerV3>().KillEngine();
            GetComponent<Rigidbody>().isKinematic = true;
        }
        if (collision.gameObject.tag == "Enemy")
        {
            //Debug.LogError("conee");
            GameManagerParkingMode.instance.Clips.PlayOneShot(GameManagerParkingMode.instance.buzzarClip);
            GameManagerParkingMode.instance.BGM.Pause();
            Invoke(nameof(GameFailPanel), 1.5f);
            GetComponent<RCC_CarControllerV3>().KillEngine();
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }
        
    public void GiftPlayer()
    {
        if (PlayerPrefs.GetInt("ParkingModeLevel")>=6)
        {
            GameManagerParkingMode.instance.levelCompleteImage.SetActive(false);
            GameManagerParkingMode.instance.gameWinPanel.SetActive(true);
            if (PlayerPrefs.GetInt("RemoveAds") != 1)
            {
                if (AdsManager.Instance)
                    AdsManager.Instance.HideBanner();//farwa  ADS Nov
            }
            if (PlayerPrefs.GetInt("ParkingModeLevel") >= 49)
            {
                GameManagerParkingMode.instance.nextBtnForFinalLevel.SetActive(false);
                GameManagerParkingMode.instance.nextBtnForNewMode.SetActive(true);
            }
            GameAnalyticsSDK.GameAnalytics.NewProgressionEvent(GameAnalyticsSDK.GAProgressionStatus.Start, "ParkingMode Level " + PlayerPrefs.GetInt("ParkingModeLevel") + " Won");//farwa//farwa GA Nov
        }
        else
        {
            GameManagerParkingMode.instance.levelCompleteImage.SetActive(false);
            if (PlayerPrefs.GetInt("RemoveAds") != 1)
            {
                if (AdsManager.Instance)
                    AdsManager.Instance.HideBanner();//farwa  ADS Nov
            }
            GameManagerParkingMode.instance.giftPanel.SetActive(true);
        }
    }
    
    public void GameWinPanel()
    {
       
        GameManagerParkingMode.instance.levelCompleteImage.SetActive(false);
        GameManagerParkingMode.instance.gameWinPanel.SetActive(true);
        if (PlayerPrefs.GetInt("RemoveAds") != 1)
        {
            if (AdsManager.Instance)
                AdsManager.Instance.HideBanner();//farwa  ADS Nov
        }
        if (PlayerPrefs.GetInt("ParkingModeLevel")>=49)
        {
            GameManagerParkingMode.instance.nextBtnForFinalLevel.SetActive(false);
            GameManagerParkingMode.instance.nextBtnForNewMode.SetActive(true);
        }
        GameAnalyticsSDK.GameAnalytics.NewProgressionEvent(GameAnalyticsSDK.GAProgressionStatus.Start, "ParkingMode Level " + PlayerPrefs.GetInt("ParkingModeLevel") + " Won");//farwa//farwa GA Nov
    }

    public void OnCollisionEnter(Collider col)
    {
        if (col.CompareTag("Enemy"))
        {
            //Debug.LogError("coneeee");
            Invoke(nameof(GameFailPanel), 2f);

        }
    }
    public void GameFailPanel()
    {
        if (PlayerPrefs.GetInt("RemoveAds") != 1)
        {
            if (AdsManager.Instance)
                AdsManager.Instance.HideBanner();//farwa  ADS Nov
        }
            GameManagerParkingMode.instance.gameFailPanel.SetActive(true);
        GameAnalyticsSDK.GameAnalytics.NewProgressionEvent(GameAnalyticsSDK.GAProgressionStatus.Start, "ParkingMode Level " + PlayerPrefs.GetInt("ParkingModeLevel") + " Fail");//farwa//farwa GA Nov
    }


}
