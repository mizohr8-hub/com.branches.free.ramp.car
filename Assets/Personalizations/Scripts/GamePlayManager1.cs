using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using RGSK;
using GoogleMobileAds.Samples;

[System.Serializable]
public class PropsPositions
{
    public Transform startPos;
    public Transform endPos;
}

public class GamePlayManager1 : MonoBehaviour {
    public static GamePlayManager1 instance;

    public GameObject nextBtn;
    public GameObject restartBtn;
    public GameObject nosImg;

    public GameObject revivePanel, pausePanel, failPanel;
    public float isTime = 10; 
    public Text timerTxt, levelNumTxt;
    public bool isGameOver, isScoreAdded;
    public GameObject[] starPng;
    private int currency;
    public Text coinsText;
    public GameObject[] levels;
    public GameObject[] checkPointContainers;
    public GameObject[] spawnPointContainers;
    public GameObject[] paths;
    public Transform[] triggerPos;
    public Text starsCollected, coinsEarned;
    public bool levelCompleted = false;
    public Image nosFillImage, speedFillImage;
    public GameObject gasBtn;


    public PropsPositions[] levelsPositions;
    public GameObject startPoint;
    public GameObject endPoint;

    public GameObject LoadingPanel;


    
    public void OnPressNos()
    {
        gasBtn.GetComponent<RCC_UIController>().pressing = true;
    }

    public void OnReleaseNos()
    {
        gasBtn.GetComponent<RCC_UIController>().pressing = false;
    }

    private void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
        }
        if (AdsManager.Instance)
        {
            AdsManager.Instance.ShowAdaptiveBanner();
        }
        levelNumTxt.text = PlayerPrefs.GetInt("LevelNum").ToString();
        if (PlayerPrefs.GetInt("PlayingOpenWorld") == 1)
        {
            levelNumTxt.transform.parent.gameObject.SetActive(false);
        }
        currency = PlayerPrefs.GetInt("Currency");
        coinsText.text = currency.ToString();


        foreach (var item in starPng)
        {
            item.SetActive(false);
        }

        foreach (var item in levels)
        {
            item.SetActive(false);
        }

        if (PlayerPrefs.GetInt("LevelNum") >= 12)
        {
            nextBtn.SetActive(false);
            restartBtn.transform.position = nextBtn.transform.position;
        }

        levels[PlayerPrefs.GetInt("LevelNum") - 1].SetActive(true);
        RaceManager.instance.pathContainer = paths[PlayerPrefs.GetInt("LevelNum") - 1].transform;
        transform.position = triggerPos[PlayerPrefs.GetInt("LevelNum") - 1].transform.position;
        RaceManager.instance.spawnpointContainer = spawnPointContainers[PlayerPrefs.GetInt("LevelNum") - 1].transform;
        RaceManager.instance.checkpointContainer = checkPointContainers[PlayerPrefs.GetInt("LevelNum") - 1].transform;
        print(PlayerPrefs.GetInt("LevelNum") + "  : Level");
        Instantiate(startPoint, levelsPositions[PlayerPrefs.GetInt("LevelNum") - 1].startPos.position, levelsPositions[PlayerPrefs.GetInt("LevelNum") - 1].startPos.rotation);
        //startPoint.transform.position = levelsPositions[PlayerPrefs.GetInt("LevelNum") - 1].startPos.position;
        //startPoint.transform.rotation = levelsPositions[PlayerPrefs.GetInt("LevelNum") - 1].startPos.rotation;
        Instantiate(endPoint, levelsPositions[PlayerPrefs.GetInt("LevelNum") - 1].endPos.position, levelsPositions[PlayerPrefs.GetInt("LevelNum") - 1].endPos.rotation);

        //endPoint.transform.position = levelsPositions[PlayerPrefs.GetInt("LevelNum") - 1].endPos.position;
        //endPoint.transform.rotation = levelsPositions[PlayerPrefs.GetInt("LevelNum") - 1].endPos.rotation;
        RaceManager.instance.totalLaps = 1;
    }
    private void Start()
    {
        Time.timeScale = 1;
        AudioListener.volume = 1;
        Screen.orientation = ScreenOrientation.Landscape;

        //consola
        //ConsoliAds.Instance.LoadRewarded();


        // ConsoliAds.onRewardedVideoAdCompletedEvent += Respawn;
        //Firebase.Analytics.FirebaseAnalytics.LogEvent("Challenge Mode Level No " + PlayerPrefs.GetInt("LevelNum") + " started");
        //GameAnalyticsSDK.GameAnalytics.NewProgressionEvent(GameAnalyticsSDK.GAProgressionStatus.Start, "Challenge Mode Level No " + PlayerPrefs.GetInt("LevelNum") + " started");//HAiderGA
    }

    public Animation[] anims;



    public void Next()
    {
        Debug.LogError("Next() function called");
      //  SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if (PlayerPrefs.GetInt("PlayingOpenWorld", 0) == 1)
        {
            Debug.LogError("Loading Open World");

            if (LoadingPanel != null) LoadingPanel.SetActive(true);
            if (restartBtn != null) restartBtn.SetActive(false);

            SceneManager.LoadScene("OpenWorld");
        }
        else
        {
            Debug.LogError("Loading Next Level");

            int currentLevel = PlayerPrefs.GetInt("LevelNum", 1);
            int unlockedLevels = PlayerPrefs.GetInt("UnlockLevels", 1);

            // 🛑 Check if RaceManager is null before accessing it
            if (RaceManager.instance == null)
            {
                Debug.LogError("RaceManager instance is NULL. Make sure it is initialized.");
                return;
            }

            // ✅ Check if the player has completed all laps
            if (currentLevel >= RaceManager.instance.totalLaps)
            {
                Debug.LogError("Race finished, moving to next level.");

                PlayerPrefs.SetInt("LevelNum", currentLevel + 1);

                if (unlockedLevels < currentLevel + 1)
                {
                    PlayerPrefs.SetInt("UnlockLevels", currentLevel + 1);
                }

                if (LoadingPanel != null)
                {
                    Debug.LogError("Showing Loading Panel");
                    LoadingPanel.SetActive(true);
                }
                else
                {
                    Debug.LogWarning("LoadingPanel is NULL, check in Unity Inspector.");
                }

                Invoke(nameof(ShowAD), 2f);
                Invoke(nameof(Restart), 7f);
            }
            else
            {
                Debug.LogWarning("Next() called, but race is not finished yet.");
            }
        }
    }


    public void ShowAD()
    {
        if (PlayerPrefs.GetInt("RemoveAds") == 0)
        {
            if (AdsManager.Instance)
            {
                AdsManager.Instance.ShowInterstitial();
            }
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Update()
    {
        
        if (starsCollected )
        {
           if(starsCollected.text != null) starsCollected.text = BehaviourSetting.instance.a.ToString();
            
        }
        if (isGameOver)
        {
            Timer();
        }
        if (levelCompleted && !isScoreAdded)
        {
            isScoreAdded = true;
            levelCompleted = false;
            
           
            StartCoroutine(AddCoin(3000 + (BehaviourSetting.instance.a * 100)));
            PlayerData.AddCurrency(3000 + (BehaviourSetting.instance.a * 100));
        }
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            Debug.Log("TimeScale was 0, resetting to 1.");
        }
    }

    int showing = 0;
    IEnumerator AddCoin(int coins)
    {
        yield return new WaitForSeconds(1);
        BehaviourSetting.instance.bSource.PlayOneShot(BehaviourSetting.instance.sounds[0]);
        for (int i = 0; i < (coins / 50); i++)
        {
            yield return new WaitForSeconds(0.01f);
            showing += 50;
            coinsEarned.text = showing.ToString();
        }
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }
    bool isSoundPlayed;
    void Timer()
    {
        isTime -= Time.deltaTime;
        timerTxt.text = isTime.ToString("f0");
        if (isTime <= 0)
        {
            RaceUI.instance.failRacePanel.SetActive(true);
            if (!isSoundPlayed)
            {
                isSoundPlayed = true;
                failPanel.SetActive(true);
                BehaviourSetting.instance.bSource.PlayOneShot(BehaviourSetting.instance.sounds[5]);
            }

        } 
    }

    public void SkipButton()
    {
        isTime = 0;
        RaceUI.instance.failRacePanel.SetActive(true);
        if (!isSoundPlayed)
        {
            isSoundPlayed = true;
            failPanel.SetActive(true);
            BehaviourSetting.instance.bSource.PlayOneShot(BehaviourSetting.instance.sounds[5]);
        }
    }

    public int health = 0;

    //private void OnCollisionEnter(Collision collision)
    //{
    //    health++;
    //    print(collision.gameObject.name);
    //    if (health >= 2) 
    //    {
    //        if (collision.gameObject.tag == "Player" && !isGameOver)
    //        {
    //            isGameOver = true;
    //            print("GameOver");
    //            revivePanel.SetActive(true);

    //        }
    //    }

    //    else
    //    {
    //       // PlayerControl.instance.Respawn();
    //        PlayerControl.instance.RespawnNew();
    //    }
    //    //if (collision.gameObject.tag == "Opponent")
    //    //{
    //    //    print("AI Fell down:  " + collision.gameObject.name);
    //    //    RGSK.OpponentControl.instance.RespawnRacer();
    //    //   // Destroy(collision.gameObject);
    //    //}
    //}


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            health++;
            print(other.gameObject.name);
            if (health >= 2)
            {
                if (other.gameObject.tag == "Player" && !isGameOver)
                {
                    isGameOver = true;
                    print("GameOver");
                    //revivePanel.SetActive(true);

                }
            }

            else
            {
                 //PlayerControl.instance.Respawn();
                PlayerControl.instance.RespawnNew();
            }
            //if (collision.gameObject.tag == "Opponent")
            //{
            //    print("AI Fell down:  " + collision.gameObject.name);
            //    RGSK.OpponentControl.instance.RespawnRacer();
            //   // Destroy(collision.gameObject);
            //}
        }
    }
    public void Respawn()
    {
        PlayerControl.instance.RespawnNew();
    }
    //public void Respawn()
    //{
    //    PlayerControl.instance.Respawn();
    //}
    public GameObject noVideoPanel;
    public void ShowVideo()
    {
        //consola
        //if (AdsManager.Instance)
        //{
        //    AdsManager.Instance.ShowRewardVideo(gameObject);
        //}
        //else
        //{
        //    noVideoPanel.SetActive(true);
        //}
    }
    public void ReviveWithCoins()
    {
        Debug.Log("ReviveBtn Working");
        if (PlayerPrefs.GetInt("Currency") >= 100)
        {
            int a = PlayerPrefs.GetInt("Currency");
            a-= 100;
            PlayerPrefs.SetInt("Currency",a);
            PlayerControl.instance.Respawn();
            revivePanel.SetActive(false);
            isGameOver = false;
            isTime = 5;
            coinsText.text = currency.ToString();
        }
    }
   
}
