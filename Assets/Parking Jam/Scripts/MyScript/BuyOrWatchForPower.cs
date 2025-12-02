using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watermelon;

public class BuyOrWatchForPower : MonoBehaviour
{
    public GameObject NotENoughCoins;

    private void OnEnable()
    {
        GamePlayAdsHandler.adsHandler.CancelBanner();
    }
    public void BuyPowerUp()
    {
        if (PrefsSettings.GetInt(PrefsSettings.Key.CoinsCountID) >= 1000)
        {
            PrefsSettings.SetInt(PrefsSettings.Key.CoinsCountID, PrefsSettings.GetInt(PrefsSettings.Key.CoinsCountID)-1000);

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
            NotENoughCoins.SetActive(false);
            UIController.GameUI.PowersEnabled();
            UIController.GameUI.PowerGainOptions.SetActive(false);
        }
        else
        {
            NotENoughCoins.SetActive(true);
        }
    }

    public void Later()
    {
        PlayerPrefs.SetInt("BuyOrVidForPower", 0);
        NotENoughCoins.SetActive(false);
        UIController.GameUI.PowerGainOptions.SetActive(false);       
    }
    private void OnDisable()
    {
        NotENoughCoins.SetActive(false);
        GamePlayAdsHandler.adsHandler.CallingBannerInvoke();
    }
}
