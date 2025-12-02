using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Samples;

public class HideBanner1 : MonoBehaviour
{
    // Start is called before the first frame update
    public bool showAd = false;
    private void OnEnable()
    {
        if (AdsManager.Instance)
        {
            if (PlayerPrefs.GetInt("RemoveAds")==1 && showAd)
            {
                AdsManager.Instance.ShowInterstitial();
            }

            AdsManager.Instance.HideBanner();
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
