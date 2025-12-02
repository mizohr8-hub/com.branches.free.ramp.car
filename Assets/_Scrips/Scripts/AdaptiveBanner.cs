using UnityEngine;
using System;
using GoogleMobileAds.Api;
using System.Collections.Generic;

public class AdaptiveBanner : MonoBehaviour
{
    private BannerView _bannerView;

    // Use this for initialization
    void Start()
    {
        // Set your test devices.
        // https://developers.google.com/admob/unity/test-ads
        RequestConfiguration requestConfiguration = new RequestConfiguration
        {
            TestDeviceIds = new List<string>
            {
                AdRequest.TestDeviceSimulator,
                // Add your test device IDs (replace with your own device IDs).
                #if UNITY_IPHONE
                "96e23e80653bb28980d3f40beb58915c"
                #elif UNITY_ANDROID
                "75EF8D155528C04DACBBA6F36F433035"
                #endif
            }
        };
        MobileAds.SetRequestConfiguration(requestConfiguration);

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize((InitializationStatus status) =>
        {
            RequestBanner();
        });
    }

    public void OnGUI()
    {
        GUI.skin.label.fontSize = 60;
        Rect textOutputRect = new Rect(
          0.15f * Screen.width,
          0.25f * Screen.height,
          0.7f * Screen.width,
          0.3f * Screen.height);
        //GUI.Label(textOutputRect, "Adaptive Banner Example");
    }

    private void RequestBanner()
    {
        // These ad units are configured to always serve test ads.
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
            string adUnitId = "ca-app-pub-6208957461273961/5678218575";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3790687197150862/9282608041";
#else
            string adUnitId = "unexpected_platform";
#endif

        // Clean up banner ad before creating a new one.
        if (_bannerView != null)
        {
            _bannerView.Destroy();
        }

        AdSize adaptiveSize =
                AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);

        _bannerView = new BannerView(adUnitId, adaptiveSize, AdPosition.Top);

        // Register for ad events.
        _bannerView.OnBannerAdLoaded += OnBannerAdLoaded;
        _bannerView.OnBannerAdLoadFailed += OnBannerAdLoadFailed;

        AdRequest adRequest = new AdRequest();

        // Load a banner ad.
        _bannerView.LoadAd(adRequest);
        _bannerView.Hide();

    }

    public void ShowAd()
    {
        if (_bannerView != null)
        {
            _bannerView.Hide();

            Debug.Log("Showing banner view.");
            _bannerView.Show();
        }
        else
        {
            RequestBanner();
        }
    }


    public void HideAd()
    {
        if (_bannerView != null)
        {
            Debug.Log("Hiding banner view.");
            _bannerView.Hide();
        }
    }

    private void OnBannerAdLoaded()
    {
        
    }

    #region Banner callback handlers

    private void OnBannerAdLoaded(object sender, EventArgs args)
    {
        Debug.Log("Banner view loaded an ad with response : "
                 + _bannerView.GetResponseInfo());
        Debug.LogErrorFormat("Ad Height: {0}, width: {1}",
                _bannerView.GetHeightInPixels(),
                _bannerView.GetWidthInPixels());
    }

    private void OnBannerAdLoadFailed(LoadAdError error)
    {
        Debug.LogError("Banner view failed to load an ad with error : "
                + error);
    }

    #endregion
}