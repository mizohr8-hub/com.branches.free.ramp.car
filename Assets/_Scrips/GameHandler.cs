using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Samples;


public class GameHandler : MonoBehaviour
{
    public static GameHandler instance;
    public GameObject MainScreen;
    public GameObject rccCanvas;
    public GameObject pausePanel;
    public GameObject gasPadel;
    public GameObject nos;
    public GameObject carSelectionPanel;
    public GameObject levelSelectionPanel;
    public GameObject MainMenu;
    public GameObject[] carSpawn;
    public Transform carPosition;
    public RCC_Camera cam;
    public float duration;
    public GameObject car;
    public GameObject rccCam;
    public GameObject rgskCam;

    public int totalCoins;
    public GameObject[] coinsImg;
    public GameObject flipCoin;


    public GameObject[] levels;
    public GameObject[] cars;
    public GameObject[] trynowBtn;
    public GameObject nextLevel;
    public GameObject[] locklvl;
    public GameObject[] selectlvl;
    public GameObject gasBtn,flag;
    public RCC_CarControllerV3 currentCar;
    public RCC_CarControllerV3 currentPlayer;
    public AudioClip pin,boxCrash,elephant,crowd;
    public AudioSource gameSfx;
    //public AudioSource cashSound;
    public AudioClip cash;
    public AudioClip carHorn;
    public GameObject nosImage;
    public GameObject particle;
    public GameObject laodingPanel,nosParticle;
    public Slider needle;
    public GameObject notEnough;
    public GameObject tutor, welcome, engineUp, boostUp, bonusUp, levelUp, carUp, videoUp, setUp;
    public GameObject  welcomeDes, engineUpDes, boostUpDes, bonusUpDes, levelUpDes, carUpDes, videoUpDes, setUpDes;
    public GameObject engineOff, boostOff, bonusOff, levelOff, carOff, videoOff;
    public AudioClip click;
     public AudioClip flagSound;
    public bool gameStarted = false;

    public Text timerText;
    public float time = 600;
    public float engineTorque;
    public float speed;

    private bool isfive, isFour, isThree, isTwo, isOne;
    public bool timesUp;


    public int currentLevelID = 0;
    public int currentCarID = 0;


    public void Awake()
    {
        if (PlayerPrefs.GetInt("RemoveAds") == 0)
        {
            if (AdsManager.Instance)
            {
                AdsManager.Instance.ShowAdaptiveBanner();
            }
        }
        AudioListener.volume = 1;
        Screen.orientation = ScreenOrientation.Portrait;
        instance = this;

        //PlayerPrefs.SetInt("Currency", 500000);
        unlockLevels();
        ActivateLevels();
        FirstTime();
        //ActivateCars();
        nosSlider.maxValue = time;
        nosSlider.value = time;

        //consola
    }
    public void playclick()
    {
        gameSfx.PlayOneShot(click);
    }
  
    public void FirstTime()
    {
        if(PlayerPrefs.GetInt("FirstTimeTutorial") == 0)
        {
            //PlayerPrefs.SetInt("FirstTimeTutorial");
            tutor.SetActive(true);
            MainMenu.SetActive(false);
        }
        else
        {
            //PlayerPrefs.SetInt("FirstTimeTutorial");
            tutor.SetActive(false);
            MainMenu.SetActive(true);
        }
    }
    public void OnWelcome()
    {
        playclick();
        welcome.SetActive(false);
        welcomeDes.SetActive(false);
        engineUp.SetActive(true);
        engineOff.SetActive(true);
        engineUpDes.SetActive(true);
    }
    public void OnEngine()
    {
        playclick();
        engineUp.SetActive(false);
        engineUpDes.SetActive(false);
        boostUp.SetActive(true);
        boostOff.SetActive(true);
        boostUpDes.SetActive(true);
    } 
    public void OnBoost()
    {
        playclick();


        boostUp.SetActive(false);
        boostUpDes.SetActive(false);
        bonusUp.SetActive(true);
        bonusOff.SetActive(true);
        bonusUpDes.SetActive(true);
    } 
    public void OnBonus()
    {
        playclick();
        bonusUp.SetActive(false);
        bonusUpDes.SetActive(false);
        levelUp.SetActive(true);
        levelOff.SetActive(true);
        levelUpDes.SetActive(true);
    }
    public void OnLevel()
    {
        playclick();
        levelUp.SetActive(false);
        levelUpDes.SetActive(false);
        carUp.SetActive(true);
        carOff.SetActive(true);
        carUpDes.SetActive(true);
    } 
    public void OnCar()
    {
        playclick();
        carUp.SetActive(false);
        carUpDes.SetActive(false);
        videoUp.SetActive(true);
        videoOff.SetActive(true);
        videoUpDes.SetActive(true);
    }
    public void OnVideo()
    {
        playclick();
        videoUp.SetActive(false);
        videoUpDes.SetActive(false);
        setUp.SetActive(true);
        setUpDes.SetActive(true);
    } 
    public void OnSetUp()
    {
        playclick();
        setUp.SetActive(false);
        setUpDes.SetActive(false);
        tutor.SetActive(false);
        MainMenu.SetActive(true);
        PlayerPrefs.SetInt("FirstTimeTutorial", 1);
    }


    public GameObject[] tryNow;
    public int videoId;
    private void OnEnable()
    {
        //consola
        //ConsoliAds.onRewardedVideoAdCompletedEvent += GetCoins;
        //ConsoliAds.onRewardedVideoAdCompletedEvent += GetReward;
    }
    private void OnDisable()
    {
        //consola
        //ConsoliAds.onRewardedVideoAdCompletedEvent -= GetCoins;
        //ConsoliAds.onRewardedVideoAdCompletedEvent -= GetReward;
    }

    public void GetCoins()
    {
        //int currency = PlayerPrefs.GetInt("Currency");
        //currency = currency + 1000;
        //PlayerPrefs.SetInt("Currency", currency);
        //print("Video completed");

        tryNow[videoId].SetActive(false);
        print("Try Button Off" + videoId + "Deactivated Game Handler");
    }
    public void GetReward()
    {
        int currency = PlayerPrefs.GetInt("Currency");
        currency = currency + 1000;
        PlayerPrefs.SetInt("Currency", currency);
        print("Video completed Game Handler");


    }

    public void ShowVideo(int id)
    {
        videoId = id;
        //consola
        //if (ConsoliAds.Instance.IsRewardedVideoAvailable(0))
        //{
        //    ConsoliAds.Instance.ShowRewardedVideo(0);
        //}
        //else
        //{
        //    //noVideoAvailablePanel.SetActive(true);
        //    print("Video not available Game Handler");
        //}
    }
    public void ShowVideo()
    {

        //consola
        //if (ConsoliAds.Instance.IsRewardedVideoAvailable(0))
        //{
        //    ConsoliAds.Instance.ShowRewardedVideo(0);
        //}
        //else
        //{
        //    //noVideoAvailablePanel.SetActive(true);
        //    print("Video not available Game Handler");
        //}
    }

    void ActivateLevels()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].SetActive(false);
        }

        levels[PlayerPrefs.GetInt("Currentlvl")].SetActive(true);
        //levels[PlayerPrefs.GetInt("CurrentLevelJump")].SetActive(true);
    }

    public void SelecLevel(int id)
    {
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].SetActive(false);
        }
        //PlayerPrefs.SetInt("Currentlvl", id);
        levels[id].SetActive(true);
        PlayerPrefs.SetInt("CurrentLevelSelected", id);
        levelSelectionPanel.SetActive(false);
        MainMenu.SetActive(true);
    }


    //void ActivateCars()
    //{
    //    for (int i = 0; i < cars.Length; i++)
    //    {
    //        cars[i].SetActive(false);
    //    }

    //    cars[currentCarID].SetActive(true);
    //    //cars[PlayerPrefs.GetInt("CurrentCarJump")].SetActive(true);
    //}

    public GameObject currentPlayerCar;
    public void Spawn()
    {
        currentPlayerCar = Instantiate(carSpawn[PlayerPrefs.GetInt("CurrentCarJump")], carPosition.position, carPosition.rotation);
    }

    public void SpawnCar(int id)
    {
        if (currentPlayerCar)
        {

            Destroy(currentPlayerCar);

            currentPlayerCar = null;
        }
        PlayerPrefs.SetInt("CurrentCarJump", id);
        Spawn();
        carSelectionPanel.SetActive(false);
        MainMenu.SetActive(true);
    }



    public void Start()
    {
        Spawn();
        Invoke("FindPlayer", 1f);
        //  Invoke("setCam", duration);
        Time.timeScale = 1f;

        StartCoundownTimer();

       // PlayerPrefs.SetInt("Currency", 10000);
        UpdateValues();
        lockImgOff();
        //Firebase.Analytics.FirebaseAnalytics.LogEvent("Ramp Car Jumping started ");
        GameAnalyticsSDK.GameAnalytics.NewProgressionEvent(GameAnalyticsSDK.GAProgressionStatus.Start, "Ramp Car Jumping started ");//HAiderGA
    }
    public void FindPlayer()
    {
        car = GameObject.FindGameObjectWithTag("Player");
    }


    void StartCoundownTimer()
    {
        if (timerText != null)
        {
            //time = 1200;
            timerText.text = "Time Left: 20:00:000";
            InvokeRepeating("UpdateTimer", 0.0f, 0.01667f);
        }
    }

    public Text distanceTextGame, distanceText, highDistance;
    public Text coinsText;
    public Text bonusText;

    public void GameOver()
    {
        StartCoroutine(GameEnded());
      
    }
    public Text bonus;
    IEnumerator GameEnded()
    {
        yield return new WaitForSeconds(2f);
        currentPlayerCar.GetComponent<Rigidbody>().isKinematic = true;
        yield return new WaitForSeconds(0.5f);
        currentPlayerCar.GetComponent<Rigidbody>().isKinematic = false;
        flagAnim();
        SetReward();
        yield return new WaitForSeconds(4f);

        //bonus.text = totalCoins.ToString();
        bonus.text = (((PlayerPrefs.GetInt("UpdatingBonus")) +1 + 0.1) * totalCoins).ToString();

        gameOver.SetActive(true);
        rccCanvas.SetActive(false);
        //car.SetActive(false);
        //AudioListener.volume = 0;
        Time.timeScale = 0f;
        car.SetActive(false);

        yield return new WaitForSeconds(2f);
        StartCoroutine(AddCoin(2000));

    }
   
    bool temp = false;
     void flagAnim()
    {
        if (!temp)
        {
            PlayerPrefs.SetInt("Currency", PlayerPrefs.GetInt("Currency") + 2000);
            temp = true;
            gameSfx.PlayOneShot(flagSound);
            Instantiate(flag, currentPlayerCar.transform.position + new Vector3(-3f, 0, 0), Quaternion.identity);
            Instantiate(particle, currentPlayerCar.transform.position, Quaternion.identity);
            PlayerPrefs.SetInt("Currency", PlayerPrefs.GetInt("Currency") + totalCoins);
        }
    }
    void SetReward()
    {
        if (currentPlayer.GetComponent<TotalDistanceCalculator>().distance > PlayerPrefs.GetFloat("TotalDistance"))
        {
            PlayerPrefs.SetFloat("TotalDistance", currentPlayer.GetComponent<TotalDistanceCalculator>().distance);
        }
        distanceText.text = RCC_SceneManager.Instance.activePlayerVehicle.GetComponent<TotalDistanceCalculator>().distance.ToString("F0");
        highDistance.text = PlayerPrefs.GetFloat("TotalDistance").ToString("F0");
    }


    public void onNextLvl()
    {
        if (PlayerPrefs.GetInt("RemoveAds") == 0)
        {
            if (AdsManager.Instance)
            {
                AdsManager.Instance.ShowInterstitial();
            }
        }        //if (PlayerPrefs.GetInt("RemoveAds") == 0)
        AudioListener.volume = 1;
        playclick();
        laodingPanel.SetActive(true);
        if (PlayerPrefs.GetInt("Currentlvl") < 3)
        {
            PlayerPrefs.SetInt("Currentlvl", PlayerPrefs.GetInt("Currentlvl") + 1);
        }
        else
        {

            PlayerPrefs.SetInt("Currentlvl", 3);
        }
        gameOver.SetActive(false);
        SceneManager.LoadScene("RampCarJump");
        //{

        //    //consola
        //    //if (PlayerPrefs.GetInt("RemoveAds") == 0)
        //    //{
        //    //    AdsManagerNew._Instance.ShowInterstitial();

        //    //}
        //}
        //if (PlayerPrefs.GetInt("Currentlvl") > 0)
        //{

        //for (int i = 0; i < locklvl.Length; i++)
        //{
        //    locklvl[i].SetActive(false);
        //} 

        //for (int i = 0; i < selectlvl.Length; i++)
        //{
        //    selectlvl[i].SetActive(true);
        //}

        //}
    }

    public void lockImgOff()
    {
        if (PlayerPrefs.GetInt("Currentlvl") > 0)
        {

            //for (int i = 0; i < locklvl.Length; i++)
            //{
            //    locklvl[i].SetActive(false);
            //}

            for (int i = 0; i < PlayerPrefs.GetInt("Currentlvl")+1 ; i++)
            {
                locklvl[i].SetActive(false);
                selectlvl[i].SetActive(true);
            }

        }
    }
    public void unlockLevels()
    {
        if (PlayerPrefs.GetInt("UnlockEnv") == 1)
        { 

            for (int i = 0; i < PlayerPrefs.GetInt("Currentlvl") + 4; i++)
            {
                locklvl[i].SetActive(false);
                selectlvl[i].SetActive(true);
            }

        }
    }

    public void lvlInapp()
    {
        PlayerPrefs.SetInt("UnlockEnv", 1);
    }



    public Slider nosSlider;

    void UpdateTimer()
    {
        if (timerText != null && !timesUp)
        {
            time -= Time.deltaTime;
            string minutes = Mathf.Floor(time / 60).ToString("00");
            string seconds = (time % 60).ToString("00");
            //string fraction = ((time * 100) % 100).ToString("000");
            nosSlider.value = time;

            if (time <= 0)
            {
                print("timesUp ");
                timesUp = true;

                nos.GetComponent<RCC_UIController>().enabled = false;
                currentPlayer.isNos = false;
                //currentPlayer.GetComponent<TotalDistanceCalculator>().record = false;

                //PlayerPrefs.SetFloat("TotalDistanceBefore", ;

                //if (currentPlayer.GetComponent<TotalDistanceCalculator>().distance > PlayerPrefs.GetFloat("TotalDistance"))
                //{
                //    PlayerPrefs.SetFloat("TotalDistance", currentPlayer.GetComponent<TotalDistanceCalculator>().distance);
                //}

                gasBtn.GetComponent<RCC_UIController>().pressing = false;

                //TDMCompletaionScreen.SetActive(true);
                Invoke("GameOver", 10f);
            }
            else
            {
                if (time < 5 && !isfive)
                {
                    isfive = true;
                    //print("Tinggggg");
                    //SoundHandlerScript.Insatnce.TriggerTimerSFX();
                }
                else if (time < 4 && !isFour)
                {
                    isFour = true;
                    //SoundHandlerScript.Insatnce.TriggerTimerSFX();
                    //print("Tinggggg");
                }
                else if (time < 3 && !isThree)
                {
                    isThree = true;
                    //SoundHandlerScript.Insatnce.TriggerTimerSFX();
                    //print("Tinggggg");
                }
                else if (time < 2 && !isTwo)
                {
                    isTwo = true;
                    //SoundHandlerScript.Insatnce.TriggerTimerSFX();
                    //print("Tinggggg");
                }
                else if (time < 1 && !isOne)
                {
                    isOne = true;
                    //SoundHandlerScript.Insatnce.TriggerTimerSFX();
                    //print("Tinggggg");
                }

                timerText.text = minutes + ":" + seconds;
            }
            //timerText.text = "Time Left: " + minutes + ":" + seconds + ":" + fraction;
        }
    }






    public void clickTap()
    {
        //MainScreen.SetActive(false);
        playclick();
        MainMenu.SetActive(false);
        cam.TPSDistance = 10.8f;
        cam.TPSHeight = 5.5f;
        rccCanvas.SetActive(true);
    }
    public void OnPressNos()
    {
        timesUp = false;
        if (time > 0)
        {
            currentPlayer = RCC_SceneManager.Instance.activePlayerVehicle.GetComponent<RCC_CarControllerV3>();
            if (!currentPlayer.RearLeftWheelCollider.wheelCollider.isGrounded
                    && !currentPlayer.RearRightWheelCollider.wheelCollider.isGrounded
                    && !currentPlayer.FrontLeftWheelCollider.wheelCollider.isGrounded
                    && !currentPlayer.FrontRightWheelCollider.wheelCollider.isGrounded)
            {
                currentPlayer.isNos = true;
                gasBtn.GetComponent<RCC_UIController>().pressing = true;
            }

        }
        else
        {
            nos.GetComponent<RCC_UIController>().enabled = false;
        }
    }

    public void OnReleaseNos()
    {
        timesUp = true;
        currentPlayer = RCC_SceneManager.Instance.activePlayerVehicle.GetComponent<RCC_CarControllerV3>();
        gasBtn.GetComponent<RCC_UIController>().pressing = false;
        currentPlayer.isNos = false;
    }
    public void onPause()
    {
        if (PlayerPrefs.GetInt("RemoveAds") == 0)
        {
            if (AdsManager.Instance)
            {
                AdsManager.Instance.ShowInterstitial();
            }
        }        //if (PlayerPrefs.GetInt("RemoveAds") == 0)
        playclick();
        rccCanvas.SetActive(false);
        pausePanel.SetActive(true);
       // MainScreen.SetActive(true);
        Time.timeScale = 0f;
        AudioListener.volume = 0f;
    }
    public void onResume()
    {
        playclick();
        AudioListener.volume = 1f;
        pausePanel.SetActive(false);
       // MainScreen.SetActive(false);
        rccCanvas.SetActive(true);
        Time.timeScale = 1f;
    }
    public void onHome()
    {
        playclick();
        laodingPanel.SetActive(true);
        SceneManager.LoadScene("RampCarJump");
        MainMenu.SetActive(true);
    }

   

    public void onCarSelect()
    {
        if (PlayerPrefs.GetInt("RemoveAds") == 0)
        {
            if (AdsManager.Instance)
            {
                AdsManager.Instance.ShowInterstitial();
            }
        }     
        playclick();
        MainMenu.SetActive(false);
        carSelectionPanel.SetActive(true);

    }

    public void Restart()
    {
        playclick();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void onBack()
    {
        playclick();
        carSelectionPanel.SetActive(false);    
        levelSelectionPanel.SetActive(false);
        MainMenu.SetActive(true);

    }
    public void onMainMenu()
    {
        playclick();
        SceneManager.LoadScene("MainMenu");
    }
    public void onLevelScreen()
    {
        if (PlayerPrefs.GetInt("RemoveAds") == 0)
        {
            if (AdsManager.Instance)
            {
                AdsManager.Instance.ShowInterstitial();
            }
        }
        playclick();
        MainMenu.SetActive(false);
        levelSelectionPanel.SetActive(true);

    }
    //public void setCam()
    //{
    //    cam.TPSDistance = 10.8f;
    //    cam.TPSHeight = 8.2f;
    //}


    public int updateCoinsPrice, updateEngineTorque, nosPrice;
    public Text updateCoinsPriceText, updateEngineTorqueText, nosPriceText;
    public Text updateCoinsLeveText, updateEngineTorqueLeveText, nosPriceLeveText;
    public GameObject gameOver;

    void UpdateValues()
    {
        updateCoinsPrice = 100 + ((PlayerPrefs.GetInt("UpdatingBonus")) * 200);
        updateCoinsLeveText.text = "Level: " + (PlayerPrefs.GetInt("UpdatingBonus"));
        updateCoinsPriceText.text = updateCoinsPrice.ToString();

        updateEngineTorque = 100 + (PlayerPrefs.GetInt("EngineTorque") * 200);
        updateEngineTorqueText.text = updateEngineTorque.ToString();
        updateEngineTorqueLeveText.text = "Level: " + (PlayerPrefs.GetInt("EngineTorque"));

        nosPrice = 100 + (PlayerPrefs.GetInt("UpdaingNos") * 200);
        nosPriceText.text = nosPrice.ToString();
        nosPriceLeveText.text = "Level: " + (PlayerPrefs.GetInt("UpdaingNos"));
    }

    public void UpdateReward()
    {
        playCash();

        updateCoinsPrice = 100 + ((PlayerPrefs.GetInt("UpdatingBonus")) * 200);
        if (PlayerPrefs.GetInt("Currency") > updateCoinsPrice)
        {
            PlayerPrefs.SetInt("UpdatingBonus", PlayerPrefs.GetInt("UpdatingBonus") + 1);
            updateCoinsPrice = 100 + ((PlayerPrefs.GetInt("UpdatingBonus")) * 200);
            updateCoinsPriceText.text = updateCoinsPrice.ToString();
            updateCoinsLeveText.text = "Level: " + (PlayerPrefs.GetInt("UpdatingBonus"));
            PlayerPrefs.SetInt("Currency", PlayerPrefs.GetInt("Currency") - updateCoinsPrice);
            print("UpdatingCoins" + PlayerPrefs.GetInt("UpdatingCoins"));
            print("Currency" + PlayerPrefs.GetInt("Currency"));

        }
        else
        {
            print("Not enough Coins");
            notEnough.SetActive(false);
            notEnough.SetActive(true);
        }
    }



    public int showing;
    public Text coinsEarned;


    IEnumerator AddCoin(int coins)
    {
        print("Running");
        yield return new WaitForSeconds(1);
        BehaviourSetting.instance.bSource.PlayOneShot(BehaviourSetting.instance.sounds[0]);
        for (int i = 0; i < (coins / 50); i++)
        {
            yield return new WaitForSeconds(0.01f);
            showing += 50;
            coinsEarned.text = showing.ToString();
        }
    }




    public void UpdateTorque()
    {
        playCash();

        updateCoinsPrice = 100 + ((PlayerPrefs.GetInt("EngineTorque")) * 200);
        if (PlayerPrefs.GetInt("Currency") > updateCoinsPrice)
        {
            PlayerPrefs.SetInt("EngineTorque", PlayerPrefs.GetInt("EngineTorque") + 1);
            updateEngineTorque = 100 + ((PlayerPrefs.GetInt("EngineTorque") + 1) * 200);
            updateEngineTorqueText.text = updateEngineTorque.ToString();
            updateEngineTorqueLeveText.text = "Level: " + (PlayerPrefs.GetInt("EngineTorque"));
            PlayerPrefs.SetInt("Currency", PlayerPrefs.GetInt("Currency") - updateEngineTorque);
            print("EngineTorque" + PlayerPrefs.GetInt("EngineTorque"));
            print("Currency" + PlayerPrefs.GetInt("Currency"));
        }
        else
        {
            print("Not enough Coins");
            notEnough.SetActive(false);
            notEnough.SetActive(true);
        }
    }

    
    public void UpdateNOS()
    {
        playCash();

        updateCoinsPrice = 100 + ((PlayerPrefs.GetInt("UpdaingNos")) * 200);
        if (PlayerPrefs.GetInt("Currency") > updateCoinsPrice)
        {
            PlayerPrefs.SetInt("UpdaingNos", PlayerPrefs.GetInt("UpdaingNos") + 1);
            nosPrice = 100 + (PlayerPrefs.GetInt("UpdaingNos") * 200);
            nosPriceText.text = nosPrice.ToString();
            nosPriceLeveText.text = "Level: " + (PlayerPrefs.GetInt("UpdaingNos"));
            PlayerPrefs.SetInt("Currency", PlayerPrefs.GetInt("Currency") - nosPrice);
            print("UpdaingNos" + PlayerPrefs.GetInt("UpdaingNos"));
            print("Currency" + PlayerPrefs.GetInt("Currency"));
        }
        else
        {
            print("Not enough Coins");
            notEnough.SetActive(false);
            notEnough.SetActive(true);
        }
    }
     public void playCash()
    {
           gameSfx.PlayOneShot(cash);
    }
   
    public void onCarPurchase()
    {
       
    }

}
