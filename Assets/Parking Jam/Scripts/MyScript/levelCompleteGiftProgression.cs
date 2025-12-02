using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Watermelon;

public class levelCompleteGiftProgression : MonoBehaviour
{
    [Header("Treasure")]
    public GameObject TreasureChestWork;
    public Image treasureProgressBar;
    public Text targetChestLevel;
    public GameObject treasureChestUnlockedPanel;
    public Text currentCoins;
    public AudioSource fillSound;

    private void Start()
    {
        if (/*!GameControllerParkingJam.IsBossLevel && */PlayerPrefs.GetInt("IsChallenges") == 0)
        {
            if (GameControllerParkingJam.CurrentLevelId < 1000)
            {
                StartCoroutine(TeasureChestGifting());
            }
        }
        else
        {
            TreasureChestWork.SetActive(false);

        }
    }

    IEnumerator TeasureChestGifting()
    {
        float diff = 0;
        if (GameControllerParkingJam.CurrentLevelId<5)
        {
            diff = 5f;
        }
        else if (GameControllerParkingJam.CurrentLevelId >= 5 && GameControllerParkingJam.CurrentLevelId<=995)
        {
            diff = 10f;
        }
        else
        {
            diff = 5f;
        }
        
        int diff1 = (GameControllerParkingJam.CurrentLevelId - PlayerPrefs.GetInt("PreviousChestLevel"));
        float temp22 = diff1 / diff;
        float temp33 = (diff1+1)/diff;
        targetChestLevel.text = diff1+"/"+diff;
        treasureProgressBar.fillAmount = temp22;
        fillSound.Play();
        while (true)
        {
            treasureProgressBar.fillAmount += 0.005f;
            yield return new WaitForSecondsRealtime(0.025f);
            if (treasureProgressBar.fillAmount>=temp33)
            {
                break;
            }
        }
        treasureProgressBar.fillAmount = temp33;
        targetChestLevel.text = (diff1+1) + "/" + diff;
        if (treasureProgressBar.fillAmount >= 1)
        {
            PlayerPrefs.SetInt("PreviousChestLevel", (GameControllerParkingJam.ActualLevelId + 1));
            yield return new WaitForSecondsRealtime(3.5f);
            GameControllerParkingJam.CollectCoins(250);
            treasureChestUnlockedPanel.SetActive(true);            
            currentCoins.text = GameControllerParkingJam.CoinsCount.ToString();
            UIController.GameUI.levelRewardCoins.text = (250 + PlayerPrefs.GetInt("ParkingJamLevelReward")).ToString();
            UIController.GameUI.totalGameCoins.text = GameControllerParkingJam.CoinsCount.ToString();
        }
    }

    public void DelayOff(int time)
    {
        Invoke(nameof(TreasureOff), time);
    }

    void TreasureOff()
    {
        treasureChestUnlockedPanel.SetActive(false);
    }
}
