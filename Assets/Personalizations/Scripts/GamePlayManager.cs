using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using RGSK;
using GoogleMobileAds.Samples;

[System.Serializable]
public class PropPositions
{
    public Transform startPos;
    public Transform endPos;
}
public class GamePlayManager : MonoBehaviour
{
    public static GamePlayManager instance;
    public GameObject nosImg;
    public GameObject levelCompletePanel, failPanel;
    public GameObject[] levels;
    public GameObject[] checkPointContainers;
    public GameObject[] spawnPointContainers;
    public GameObject[] paths;
    public GameObject revivePanel;//, pausePanel;
    public Text timerTxt, coinsText;
    public Text starsCollected, coinsEarned;
    public bool levelCompleted = false;
    private bool isGameOver;
    private int currency;
    public float isTime = 10;
    public GameObject[] starPng;
    private bool isScoreAdded = false;
    public Image nosFillImage, speedFillImage;
    public GameObject gasBtn;




    public PropPositions[] levelsPositions;
    public GameObject startPoint;
    public GameObject endPoint;




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
        if(instance == null)
        {
        instance = this;
        }
        if (AdsManager.Instance)
        {
            AdsManager.Instance.ShowAdaptiveBanner();
        }
        RaceManager.instance.totalRacers = (PlayerPrefs.GetInt("Difficulty"));
        RaceManager.instance.totalLaps = 1;

        foreach (var item in starPng)
        {
            item.SetActive(false);
        }

        foreach (var item in levels)
        {
            //item.SetActive(false);
        }

        levelCompletePanel.SetActive(true);

        //levels[PlayerPrefs.GetInt("9UY67Difficulty") - 1].SetActive(true);
        //RaceManager.instance.pathContainer = paths[PlayerPrefs.GetInt("Difficulty") - 1].transform;
        //RaceManager.instance.spawnpointContainer = spawnPointContainers[PlayerPrefs.GetInt("Difficulty") - 1].transform;
        //RaceManager.instance.checkpointContainer = checkPointContainers[PlayerPrefs.GetInt("Difficulty") - 1].transform;

        //Instantiate(startPoint, levelsPositions[PlayerPrefs.GetInt("Difficulty") - 1].startPos.position, levelsPositions[PlayerPrefs.GetInt("Difficulty") - 1].startPos.rotation);
        //Instantiate(endPoint, levelsPositions[PlayerPrefs.GetInt("Difficulty") - 1].endPos.position, levelsPositions[PlayerPrefs.GetInt("Difficulty") - 1].endPos.rotation);
    }

    public int health = 0;

    private void OnCollisionEnter(Collision collision)
    {
        print(collision.gameObject.name);
        if (collision.gameObject.tag == "Player" && !isGameOver)
        {
            health++;
            if (health >= 2)
            {
                isGameOver = true;
                print("GameOver");
                revivePanel.SetActive(true);
            }
            else
            {
                PlayerControl.instance.Respawn();
            }

        }
        if (collision.gameObject.tag == "Opponent")
        {
            print("AI Fell down:  " + collision.gameObject.name);
            RGSK.OpponentControl.instance.RespawnRacer();
            // Destroy(collision.gameObject);
        }
    }

    private void Start()
    {
        //ConsoliAds.Instance.LoadRewarded(0);

        //consola
        //ConsoliAds.onRewardedVideoAdCompletedEvent += Respawn;
        //Firebase.Analytics.FirebaseAnalytics.LogEvent("Multiplayer mode started");
        GameAnalyticsSDK.GameAnalytics.NewProgressionEvent(GameAnalyticsSDK.GAProgressionStatus.Start, "Multiplayer mode started");//HAiderGA
    }

    private void Update()
    {
        if (BehaviourSetting.instance.isLevelCompleted)
        {
            levelCompletePanel.SetActive(true);
        }

        currency = PlayerPrefs.GetInt("Currency");
        coinsText.text = currency.ToString();
        if (starsCollected)
        {
            starsCollected.text = BehaviourSetting.instance.a.ToString();

        }
        if (isGameOver)
        {
            Timer();
        }

        if (levelCompleted && !isScoreAdded)
        {


            isScoreAdded = true;
            if (PlayerPrefs.GetInt("Difficulty") == 1)
            {
                PlayerData.AddCurrency(3000);
                StartCoroutine(AddCoin(3000));
                //coinsEarned.text = 3000.ToString();*/
            }
            else if (PlayerPrefs.GetInt("Difficulty") == 2)
            {
                PlayerData.AddCurrency(5000);
                StartCoroutine(AddCoin(5000));
                //coinsEarned.text = 5000.ToString();
            }
            else if (PlayerPrefs.GetInt("Difficulty") == 3)
            {
                PlayerData.AddCurrency(8000);
                StartCoroutine(AddCoin(8000));
                //coinsEarned.text = 8000.ToString();
            }
            
        }
    }

    int showing = 0;
    IEnumerator AddCoin(int coins)
    {
        yield return new WaitForSeconds(1f);
        BehaviourSetting.instance.bSource.PlayOneShot(BehaviourSetting.instance.sounds[0]);
        for (int i = 0; i < (coins / 100); i++)
        {
            //yield return new WaitForSeconds(1);
            yield return new WaitForSeconds(0.015f);
            showing += 100;
            coinsEarned.text = showing.ToString();
            
        }
    }

    void Timer()
    {
        isTime -= Time.deltaTime;
        timerTxt.text = isTime.ToString("f0");
        if (isTime <= 0)
        {
            RaceUI.instance.failRacePanel.SetActive(true);
           
            failPanel.SetActive(true);
            BehaviourSetting.instance.bSource.PlayOneShot(BehaviourSetting.instance.sounds[5]);
        }
    }

    public void SkipButton()
    {
        isTime = 0;
        RaceUI.instance.failRacePanel.SetActive(true);
        failPanel.SetActive(true);
        BehaviourSetting.instance.bSource.PlayOneShot(BehaviourSetting.instance.sounds[5]);
    }

    public void Respawn()
    {
        PlayerControl.instance.Respawn();
    }
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
        if (PlayerPrefs.GetInt("Currency") >= 100)
        {
            int a = PlayerPrefs.GetInt("Currency");
            a -= 100;
            PlayerPrefs.SetInt("Currency", a);
            PlayerControl.instance.Respawn();
            revivePanel.SetActive(false);
            isGameOver = false;
            isTime = 5;
        }
    }
}
