#pragma warning disable 649

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Watermelon;

public class GameUIController : UICanvasAbstract
{
    [SerializeField] RectTransform safeAreaPanel;

    [Header("Buttons")]
    [SerializeField] Button replayButton;
    [SerializeField] Button skipButton;

    [Header("Coins")]
    [SerializeField] Text levelText;
    [SerializeField] Text coinsAmountText;

    [Header("Dev Panel")]
    [SerializeField] RectTransform devPanel;

    [Header("Powers")]
    public Button hammer;
    public Button missile;
    public Button roller;
    public Button npcOff;
    public Button obstaclesOff;

    public Animator[] powerBtnAnimator;
    public GameObject[] handPointer;
    public GameObject powersUnlockBlackTint;
    public Text powerUnlockBlackTintText;
    public GameObject doNotHitNPC;
    public GameObject bossLevelEnter;
    public GameObject challengeLevelEntry;
    public Text bossLevelMoveLimit;

    public Text hammerUse;
    public Text missileUse;
    public Text rollerUse;
    public Text npcUse;
    public Text obstacleUse;

    public GameObject Tutorial;

    [Header("LevelEnd")]
    public GameObject levelFail;
    public GameObject levelComplete;
    public GameObject confetti;
    public GameObject levelPause;
    public GameObject levelLoad;
    public GameObject rateUs;
    public Image loadBar;
    public GameObject skipLevelAd;
    public Text levelRewardCoins, totalGameCoins, treasureChestCoins, treasureChestCoinsGreen;

    [Header("RewardVidoItems")]
    public GameObject PowerGainOptions;
    public GameObject RevivalPanel;
    public GameObject MoreTurnsPanel;
    new void Awake()
    {
        base.Awake();

        safeAreaPanel.anchoredPosition = Vector3.down * UIController.SafeAreaTopOffset;
    } 

    public override void Show(){}
    public override void Hide(){}

    public void AllowLevelFail()
    {
        LevelController.TrueLevelFail();
    }
    public void ReplayButton()
    {

        if (!UITouchHandler.CanReplay) return;

        GameControllerParkingJam.ReplayLevel();

        SetReplayButtonVisibility(false);

        GameAudioController.PlayButtonAudio();
    }

    public void AddMoreTurns()
    {
        PlayerPrefs.SetInt("BossLevelTurnLimit", PlayerPrefs.GetInt("BossLevelTurnLimit") + 5);
        UIController.GameUI.bossLevelMoveLimit.text = "MOVES LEFT: " + PlayerPrefs.GetInt("BossLevelTurnLimit");
    }
    public void ReviveLevel()
    {
        LevelObjectsSpawner.RemoveNPCHitCar();
    }

    public void OurPowerButtonVisibility()
    {
        if (GameControllerParkingJam.CurrentLevelId == 0)
        {
            Tutorial.SetActive(true);
        }

        if (PlayerPrefs.GetInt("IsChallenges") == 1)
        {
            skipLevelAd.SetActive(false);
        }

        if (GameControllerParkingJam.CurrentLevelId == 10 && PlayerPrefs.GetInt("IsChallenges") == 0)
        {
            PlayerPrefs.SetInt("GamePowerLock", 1); //obstacle
            if (GameControllerParkingJam.IsBossLevel)
            {
                powersUnlockBlackTint.SetActive(true);
                powerBtnAnimator[0].enabled = true;
                handPointer[0].SetActive(true);
                powerUnlockBlackTintText.text = "Use Obstacle Disable To Turn off Environment Obstacles For 5 Seconds";
            }
            PlayerPrefs.SetInt("ObstaclePowerUnlocked", 1);

        }
        if (GameControllerParkingJam.CurrentLevelId == 15 && PlayerPrefs.GetInt("IsChallenges") == 0)
        {
            doNotHitNPC.SetActive(true);
        }
        if (GameControllerParkingJam.CurrentLevelId == 18 && PlayerPrefs.GetInt("IsChallenges") == 0)
        {
            PlayerPrefs.SetInt("GamePowerLock", 2); //npc
            
            powersUnlockBlackTint.SetActive(true);
            powerUnlockBlackTintText.text = "Use Civilian Disable To Turn off All Moving Characters For 5 Seconds";
            powerBtnAnimator[1].enabled = true;
            handPointer[1].SetActive(true);
            
            PlayerPrefs.SetInt("NPCPowerUnlocked", 1);
        }
        if (GameControllerParkingJam.CurrentLevelId == 28 && PlayerPrefs.GetInt("IsChallenges") == 0)
        {
            PlayerPrefs.SetInt("GamePowerLock", 3); //missile
            
            powersUnlockBlackTint.SetActive(true);
            powerBtnAnimator[2].enabled = true;
            handPointer[2].SetActive(true);
            powerUnlockBlackTintText.text = "Use Missile Shoot to Destroy a Vehicle and Everything in it's Lane";
            
            PlayerPrefs.SetInt("MissilePowerUnlocked", 1);
        }
        if (GameControllerParkingJam.CurrentLevelId == 40 && PlayerPrefs.GetInt("IsChallenges") == 0)
        {
            PlayerPrefs.SetInt("GamePowerLock", 4); //hammer
            if (GameControllerParkingJam.IsBossLevel)
            {
                powersUnlockBlackTint.SetActive(true);
                powerUnlockBlackTintText.text = "Use Hammer Power to and Select  Vehicle to Destroy";
                powerBtnAnimator[3].enabled = true;
                handPointer[3].SetActive(true);
            }
            PlayerPrefs.SetInt("HammerPowerUnlocked", 1);
        }
        if (GameControllerParkingJam.CurrentLevelId == 70 && PlayerPrefs.GetInt("IsChallenges") == 0)
        {
            PlayerPrefs.SetInt("GamePowerLock", 5); //roller
            if (GameControllerParkingJam.IsBossLevel)
            {
                powerBtnAnimator[4].enabled = true;
                handPointer[4].SetActive(true);
                powersUnlockBlackTint.SetActive(true);
                powerUnlockBlackTintText.text = "Use Roller Power to Destroy All Vehicles and Obstacles in a Lane";
            }

            PlayerPrefs.SetInt("RollerPowerUnlocked", 1);
        }


        if (PlayerPrefs.GetInt("HammerPowerUnlocked")==1 && PlayerPrefs.GetInt("IsChallenges") == 0)
        {
            hammer.gameObject.SetActive(true);
        }
        if (PlayerPrefs.GetInt("MissilePowerUnlocked") == 1 && PlayerPrefs.GetInt("IsChallenges") == 0)
        {
            missile.gameObject.SetActive(true);
        }
        if (PlayerPrefs.GetInt("RollerPowerUnlocked") == 1 && PlayerPrefs.GetInt("IsChallenges") == 0)
        {
            roller.gameObject.SetActive(true);
        }
        if (PlayerPrefs.GetInt("NPCPowerUnlocked") == 1 && LevelController.CurrentLevel.hasNpcs)
        {
            npcOff.gameObject.SetActive(true);
        }
        if (PlayerPrefs.GetInt("ObstaclePowerUnlocked") == 1 && PlayerPrefs.GetInt("IsChallenges") == 0)
        {
            obstaclesOff.gameObject.SetActive(true);
        }

        PowersEnabled();
    }

    public void PowersDisabled()
    {
        hammer.interactable = false;
        missile.interactable = false;
        roller.interactable = false;
        npcOff.interactable = false;
        obstaclesOff.interactable = false;
                              
        hammerUse.text = PlayerPrefs.GetInt("PowersHammer").ToString();
        missileUse.text = PlayerPrefs.GetInt("PowersMissile").ToString();
        rollerUse.text = PlayerPrefs.GetInt("PowersRoller").ToString();
        npcUse.text = PlayerPrefs.GetInt("PowersNPC").ToString();
        obstacleUse.text = PlayerPrefs.GetInt("PowersObstacles").ToString();
    }
    
    public void PowersEnabled()
    {
        //if (PlayerPrefs.GetInt("PowersHammer") > 0)
        //{
            hammer.interactable = true;
            hammerUse.text = PlayerPrefs.GetInt("PowersHammer").ToString();
        //}
        //if (PlayerPrefs.GetInt("PowersMissile") > 0)
        //{
            missile.interactable = true;
            missileUse.text = PlayerPrefs.GetInt("PowersMissile").ToString();
        //}
        //if (PlayerPrefs.GetInt("PowersRoller") > 0)
        //{
            roller.interactable = true;
            rollerUse.text = PlayerPrefs.GetInt("PowersRoller").ToString();
        //}
        //if (PlayerPrefs.GetInt("PowersNPC") > 0)
        //{
            npcOff.interactable = true;
            npcUse.text = PlayerPrefs.GetInt("PowersNPC").ToString();
        //}
        //if (PlayerPrefs.GetInt("PowersObstacles") > 0)
        //{
            obstaclesOff.interactable = true;
            obstacleUse.text = PlayerPrefs.GetInt("PowersObstacles").ToString();
        //}
    }
    public void SetCoinsAmount(int coinsAmount)
    {
        coinsAmountText.text = coinsAmount.ToString();
    }

    public void LevelFailed()
    {
        GamePlayAdsHandler.adsHandler.CancelBanner();
        UITouchHandler.Enabled = false;
        GamePlayAudioSources.instance.GameAmbience.volume = 0;
       
        levelFail.SetActive(true);
    }

    public void LevelComplete(int coins)
    {
        UITouchHandler.Enabled = false;
        GamePlayAudioSources.instance.GameAmbience.volume = 0;

        PlayerPrefs.SetInt("ParkingJamLevelReward", coins);
        GameControllerParkingJam.CollectCoins(coins);
        levelRewardCoins.text = coins.ToString();
        totalGameCoins.text = GameControllerParkingJam.CoinsCount.ToString();
        GamePlayAdsHandler.adsHandler.CancelBanner();
        if (PlayerPrefs.GetInt("RateUsPref")==0 && (PlayerPrefs.GetInt("settings_CurrentLevelID")== 2 || PlayerPrefs.GetInt("settings_CurrentLevelID")==5))
        {
            rateUs.SetActive(true);
        }
        else
        {
            levelComplete.SetActive(true);
        }

       
    }

    public void DoubleLevelReward()
    {
        int doubleReward = PlayerPrefs.GetInt("ParkingJamLevelReward");
        GameControllerParkingJam.CollectCoins(doubleReward);
        levelRewardCoins.text = (2 * doubleReward).ToString();
        totalGameCoins.text = GameControllerParkingJam.CoinsCount.ToString();
    }

    public void SkipLevelForVideo()
    {        
        if (PlayerPrefs.GetInt("IsChallenges") == 0)
        {
            //environment set if level skip
            if (GameControllerParkingJam.CurrentLevelId == 38)
            {
                PlayerPrefs.SetInt("TucTucSelectedEnv", 1);
            }
            else if (GameControllerParkingJam.CurrentLevelId == 78)
            {
                PlayerPrefs.SetInt("TucTucSelectedEnv", 2);
            }
            else if (GameControllerParkingJam.CurrentLevelId == 118)
            {
                PlayerPrefs.SetInt("TucTucSelectedEnv", 3);
            }
            else if (GameControllerParkingJam.CurrentLevelId == 158)
            {
                PlayerPrefs.SetInt("TucTucSelectedEnv", 4);
            }
            else if (GameControllerParkingJam.CurrentLevelId == 198)
            {
                PlayerPrefs.SetInt("TucTucSelectedEnv", 5);
            }
            else if (GameControllerParkingJam.CurrentLevelId == 238)
            {
                PlayerPrefs.SetInt("TucTucSelectedEnv", 6);
            }
            else if (GameControllerParkingJam.CurrentLevelId == 278)
            {
                PlayerPrefs.SetInt("TucTucSelectedEnv", 7);
            }
            else if (GameControllerParkingJam.CurrentLevelId == 318)
            {
                PlayerPrefs.SetInt("TucTucSelectedEnv", 8);
            }

            //treasure chest set
            if (GameControllerParkingJam.CurrentLevelId==4)
            {
                PlayerPrefs.SetInt("PreviousChestLevel", (GameControllerParkingJam.CurrentLevelId + 1));
            }
            else if (GameControllerParkingJam.CurrentLevelId > 4 && GameControllerParkingJam.CurrentLevelId < 994)
            {
                if ((GameControllerParkingJam.CurrentLevelId+1)%10==5)
                {
                    PlayerPrefs.SetInt("PreviousChestLevel", (GameControllerParkingJam.CurrentLevelId + 1));
                }
            }
            else if(GameControllerParkingJam.CurrentLevelId==998)
            {
                PlayerPrefs.SetInt("PreviousChestLevel", (GameControllerParkingJam.CurrentLevelId + 1));
            }
        }
        GameControllerParkingJam.NextLevelBtnClick(1);
    }
    public void SetLevelText(int level)
    {
        if (PlayerPrefs.GetInt("IsChallenges") == 1)
        {
            levelText.text = "Challenge " + (PlayerPrefs.GetInt("ChallengeNumber")+1);
        }
        if ((level != 0 && level % 10 == 0) && PlayerPrefs.GetInt("BossLevelTime" + level) == 0)
        {
            levelText.text = "BOSS LEVEL";
        }
        else
        {
            levelText.text = "LEVEL " + (level + 1);
        }
    }

    private void Update()
    {
            levelText.text = "LEVEL " + (PlayerPrefs.GetInt("ParkingLevelNumber") + 1);
    }
    public void SkipButton()
    {
        GameAudioController.PlayButtonAudio();

        //AdsManager.ShowRewardBasedVideo((hasWatched) =>  //bilal.ads
        //{
        //    if (hasWatched)
        //    {
        //        GameControllerParkingJam.TurnsAfterRewardVideo = 0;
        //        GameControllerParkingJam.SkipLevel();
        //    }
        //});

    }

    public void FirstLevelButton()
    {
        if (GameControllerParkingJam.WinStage) return;
        GameControllerParkingJam.FirstLevelDev();
    }

    public void NextLevelButton()
    {
        if (GameControllerParkingJam.WinStage) return;
        //GameControllerParkingJam.SkipLevel();
        LevelController.DestroyLevel();
        GameControllerParkingJam.NextLevel(false);
    }

    public void PreviousLevel()
    {
        if (GameControllerParkingJam.WinStage) return;
        GameControllerParkingJam.PrevLevelDev();
    }

    public void HideButton()
    {
        devPanel.gameObject.SetActive(false);
    }

    public void SetReplayButtonVisibility(bool isShown)
    {
        if (isShown)
        {
            replayButton.image.rectTransform.DOAnchoredPosition(Vector3.up * 470f, 0.5f).SetEasing(Ease.Type.QuadOut);
        } else
        {
            replayButton.image.rectTransform.DOAnchoredPosition(new Vector2(200, 470), 0.5f).SetEasing(Ease.Type.QuadOut);
        }
    }

    public void SetSkipButtonVisibility(bool isShown)
    {
        if (isShown)
        {
            skipButton.image.rectTransform.DOAnchoredPosition(Vector3.up * 470f, 0.5f).SetEasing(Ease.Type.QuadOut);
        }
        else
        {
            skipButton.image.rectTransform.DOAnchoredPosition(new Vector2(-200, 470), 0.5f).SetEasing(Ease.Type.QuadOut);
        }
    }

    public void ShopButton()
    {
        GameAudioController.PlayButtonAudio();

        StoreUIController.OpenStore();
    }

    //new progression events
    public void RestartLevel()
    {
        
        levelFail.SetActive(false);
        levelComplete.SetActive(false);
        levelPause.SetActive(false);
        levelLoad.SetActive(true);       
        StartCoroutine(Loading("ParkingJamGame"));
    }

    public void Home()
    {
        
        levelFail.SetActive(false);
        levelComplete.SetActive(false);
        levelPause.SetActive(false);
        levelLoad.SetActive(true);      
        StartCoroutine(Loading("ParkingJamMainMenu"));
    }

    public void NextBtnClick()
    {
        PlayerPrefs.SetInt("ParkingLevelNumber", PlayerPrefs.GetInt("ParkingLevelNumber") + 1);
        GameControllerParkingJam.NextLevelBtnClick();
    }

    public void Resume()
    {
        AudioListener.volume = 1;
        GamePlayAdsHandler.adsHandler.CallingBannerInvoke();
        levelPause.SetActive(false);
        UITouchHandler.Enabled = true;
    }

    public void PauseGame()
    {
        AudioListener.volume = 0;
        GamePlayAdsHandler.adsHandler.CancelBanner();
        levelPause.SetActive(true);
        UITouchHandler.Enabled = false;
        
    }
    public void NextLevelLoad(int ads=0)
    {
        if (ads==0)
        {


           
        }
        levelFail.SetActive(false);
        levelComplete.SetActive(false);
        levelPause.SetActive(false);
        levelLoad.SetActive(true);        
        StartCoroutine(Loading("ParkingJamGame"));
    }
    IEnumerator Loading(string sceneName)
    {
        AudioListener.volume = 1;
        if (PlayerPrefs.GetInt("RemoveAds") == 0)
        {
            yield return new WaitForSecondsRealtime(3f);
        }
        
        Canvas.gameObject.SetActive(false);
        while (true)
        {
            loadBar.fillAmount += 0.025f;
            yield return new WaitForSecondsRealtime(0.025f);
            if (loadBar.fillAmount >= 1f)
            {
                break;
            }
        }
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void CollecTreasure()
    {
       
        treasureChestCoins.text = GameControllerParkingJam.CoinsCount.ToString();
        //Invoke(nameof(NextBtnClick),2f);
    }

    public void DoubleTreasure()
    {
        GameControllerParkingJam.CollectCoins(250);
        levelRewardCoins.text = (PlayerPrefs.GetInt("ParkingJamLevelReward") + 500).ToString();
        totalGameCoins.text = GameControllerParkingJam.CoinsCount.ToString();
        treasureChestCoins.text = GameControllerParkingJam.CoinsCount.ToString();
        treasureChestCoinsGreen.text = "500 coins";
        //Invoke(nameof(NextBtnClick), 2f);
    }
}
