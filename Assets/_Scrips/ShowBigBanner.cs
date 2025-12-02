using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Samples;

public class ShowBigBanner : MonoBehaviour
{

    public bool BannerHide;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        if (BannerHide)
        {
            if (AdsManager.Instance)
            {
                AdsManager.Instance.HideAdaptiveBanner();
            }
        }
        else
        {
            if (AdsManager.Instance)
            {
                AdsManager.Instance.ShowBigBanner();
            }
        }
    }
    private void OnDisable()
    {
        if (AdsManager.Instance)
        {
            AdsManager.Instance.HideBigBanner();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
