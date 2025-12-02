//using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GoogleMobileAds.Samples;

public class RewardedVideoSetup : MonoBehaviour
{
    public static RewardedVideoSetup Instance;
    [SerializeField] int Index = 0;


    public CharacterSelection character;

    public bool isRewardDouble = false;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    #region Give Reward

    public void RewardAfterAd()
    {
        if (Index == 0)
        {
            character.WatchVidToUnlock();
        }
        else if (Index == 1)
        {
            PlayerPrefs.SetInt("currentCash", PlayerPrefs.GetInt("currentCash") + 1000);
            
        }
    }
    #endregion

    public void ShowRewardedVideo()
    {
        if (AdsManager.Instance)
            AdsManager.Instance.Rewarded.ShowAdOne(this);
    }
    public void Show_RewardedInterstitial_Video()
    {
        if (AdsManager.Instance)
            AdsManager.Instance.Rewarded.ShowAdOne(this);

    }
  
    }