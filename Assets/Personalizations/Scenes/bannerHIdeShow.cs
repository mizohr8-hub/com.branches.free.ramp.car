using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Samples;

public class bannerHIdeShow : MonoBehaviour {

    private void OnEnable()
    {
        if (PlayerPrefs.GetInt("RemoveAds") == 0)
        {
            if (AdsManager.Instance)
            {
              // AdsManager.Instance.HideBanner();
            }
        }
    }
    private void OnDisable()
    {
        if (PlayerPrefs.GetInt("RemoveAds") == 0)
        {
            if (AdsManager.Instance)
            {
               // AdsManager.Instance.ShowBigBanner();
            }
        } 
    }

    private void OnDestroy()
    {
        if (PlayerPrefs.GetInt("RemoveAds") == 0)
        {
            if (AdsManager.Instance)
            {
               // AdsManager.Instance.ShowBigBanner();
            }
        }
    }
}
