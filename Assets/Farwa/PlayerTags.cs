using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Samples;

public class PlayerTags : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag=="GameOver")
        {
            GameManagerOFFRoad.instance.BGM.Pause();
            GameManagerOFFRoad.instance.Clips.PlayOneShot(GameManagerOFFRoad.instance.buzzarClip);
            Invoke(nameof(GameFailPanel), 0.5f);
            //gameObject.transform.position = GameManagerOFFRoad.instance.playerStartingPoint[PlayerPrefs.GetInt("OFFRoadMode")].transform.position;
            //gameObject.transform.rotation = GameManagerOFFRoad.instance.playerStartingPoint[PlayerPrefs.GetInt("OFFRoadMode")].transform.rotation;
        }
       
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "ParkingFinish")
        {
            // Debug.LogError("bbbbbbbbbbb");
           
            GameManagerOFFRoad.instance.Clips.PlayOneShot(GameManagerOFFRoad.instance.levelCompleteClip);
            GameManagerOFFRoad.instance.levelCompleteImage.SetActive(true);
            GameManagerOFFRoad.instance.Confetti1.Play();
            GameManagerOFFRoad.instance.Confetti2.Play();
            GetComponent<RCC_CarControllerV3>().KillEngine();
            GetComponent<Rigidbody>().isKinematic = true;
            
        }
    }

    public void GameFailPanel()
    {
        if (PlayerPrefs.GetInt("RemoveAds") != 1)
        {
            if (AdsManager.Instance)
                AdsManager.Instance.HideBanner();//farwa  ADS Nov
        }
        GameManagerOFFRoad.instance.gameFailPanel.SetActive(true);
        GameAnalyticsSDK.GameAnalytics.NewProgressionEvent(GameAnalyticsSDK.GAProgressionStatus.Start, "OFFRoadMode Level " + PlayerPrefs.GetInt("OFFRoadMode") + " Fail");//farwa//farwa GA Nov
    }
}
