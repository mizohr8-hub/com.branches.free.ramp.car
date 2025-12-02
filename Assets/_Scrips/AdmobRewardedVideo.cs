using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Samples;
using System.Collections;
public class AdmobRewardedVideo : MonoBehaviour
{
    [SerializeField] int Index = 0;
    [SerializeField] Text coinsText;

    


    public bool isRewardDouble = false;
    public GameObject RewardPanel;




    #region Give Reward

    public void RewardAfterAd()
    {
        if (Index == 0) //rewarded Video
        {

        }
        else if (Index == 1)
        {
            UIController.GameUI.DoubleLevelReward();
        }
        else if (Index == 2)
        {
            UIController.GameUI.SkipLevelForVideo();
        }
        else if (Index == 3)
        {
            UIController.GameUI.DoubleTreasure();
        }
        else if (Index == 4) //get power
        {

            if (PlayerPrefs.GetInt("BuyOrVidForPower") == 0)//obstacles
            {
                PlayerPrefs.SetInt("PowersObstacles", PlayerPrefs.GetInt("PowersObstacles") + 1);
            }
            else if (PlayerPrefs.GetInt("BuyOrVidForPower") == 1)//npc
            {
                PlayerPrefs.SetInt("PowersNPC", PlayerPrefs.GetInt("PowersNPC") + 1);
            }
            else if (PlayerPrefs.GetInt("BuyOrVidForPower") == 2)//missile
            {
                PlayerPrefs.SetInt("PowersMissile", PlayerPrefs.GetInt("PowersMissile") + 1);
            }
            else if (PlayerPrefs.GetInt("BuyOrVidForPower") == 3)//hammer
            {
                PlayerPrefs.SetInt("PowersHammer", PlayerPrefs.GetInt("PowersHammer") + 1);
            }
            else if (PlayerPrefs.GetInt("BuyOrVidForPower") == 4) //roller
            {
                PlayerPrefs.SetInt("PowersRoller", PlayerPrefs.GetInt("PowersRoller") + 1);
            }
            UIController.GameUI.PowersEnabled();
            UIController.GameUI.PowerGainOptions.SetActive(false);
        }
        else if (Index == 5)//revive from npc hit
        {
            UIController.GameUI.ReviveLevel();
            UIController.GameUI.RevivalPanel.SetActive(false);
            GamePlayAdsHandler.adsHandler.CallingBannerInvoke();
        }
        else if (Index == 6)
        {
            UIController.GameUI.AddMoreTurns();
            UIController.GameUI.MoreTurnsPanel.SetActive(false);
            GamePlayAdsHandler.adsHandler.CallingBannerInvoke();
        }
        else if (Index == 7)
        {
            //dailyRewards.Claim_ButtonDouble();
        }

    }



    #endregion

    public void ShowRewardedVideo()
    {
        if (AdsManager.Instance)
            AdsManager.Instance.Rewarded.ShowAd(this);
    }
    public void Show_RewardedInterstitial_Video()
    {
        if (AdsManager.Instance)
            AdsManager.Instance.RewardedInterstitial.ShowAd(this);
    }


}