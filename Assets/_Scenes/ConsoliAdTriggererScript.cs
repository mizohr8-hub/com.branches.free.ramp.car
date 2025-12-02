using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Samples;


public class ConsoliAdTriggererScript : MonoBehaviour
{

    public int adID = 0;

    public bool isShowBanner = false;

    void OnEnable()
    {
        print("On ENable");
        if (isShowBanner) //Show Game Banner!
        {
            print("In BAnner");
            TriggerBanneWithID(adID);
        }
        else //show Interstitial
        {
            TriggerAdWithID(adID);
        }
    }
    
    public void TriggerAdWithID(int _adID)
    {
        if (PlayerPrefs.GetInt("RemoveAds") == 0)
        {
            if (AdsManager.Instance)
            {
                AdsManager.Instance.ShowInterstitial();
            }
        }
    }


    public void TriggerBanneWithID(int _adID)
    {
        if (PlayerPrefs.GetInt("RemoveAds") == 0)
        {
            if (AdsManager.Instance)
            {
                AdsManager.Instance.ShowAdaptiveBanner();
            }
        }
    }

}
