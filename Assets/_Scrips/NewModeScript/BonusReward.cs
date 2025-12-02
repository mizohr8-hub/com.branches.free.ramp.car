using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class BonusReward : MonoBehaviour
{
    public Text t;
    //public GameObject img, img2;
    public Button btn;
    public Image carImg;
    public Sprite[] carSprites;
    public GameObject[] tryNow;
    public GameObject unlocked, loadingScreen;
    public Image loadingBar;
    public int timeToClaim = 1440; // minutes in a day
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckReward();
    }

    bool temp = false;

    void CheckReward()
    {
        DateTime currDate = System.DateTime.Now;

        if (PlayerPrefs.GetString("LastClaimed") == "")
        {
            PlayerPrefs.SetString("LastClaimed", currDate.ToString());
        }




        string stringLastDate = PlayerPrefs.GetString("LastClaimed");
        DateTime lastDate = Convert.ToDateTime(stringLastDate);
        DateTime nextDate = lastDate.AddMinutes(timeToClaim); //24*60


        TimeSpan difference = nextDate.Subtract(currDate);
        for (int i = 0; i < PlayerPrefs.GetInt("TotalCar"); i++)
        {
            tryNow[i].SetActive(false);
        }

        if (difference.TotalMinutes <= 0)
        {
            //img.SetActive(false);
            //img2.SetActive(false);
            //btn.interactable = true;
            if (!temp)
            {
                temp = true;
                unlocked.SetActive(true);
                carImg.sprite = carSprites[PlayerPrefs.GetInt("TotalCar")];
                PlayerPrefs.SetInt("TotalCar", PlayerPrefs.GetInt("TotalCar") + 1);
                for (int i = 0; i < PlayerPrefs.GetInt("TotalCar"); i++)
                {
                    tryNow[i].SetActive(false);
                }
                UpdateTime();
            }
            //if (PlayerPrefs.GetInt("1stTime")!= 3)
            //{
            //    unlocked.SetActive(true);
            //}
            //else
            //{
            //    unlocked.SetActive(false);
            //}
        }
        else
        {
            //btn.interactable = false;
            t.text = Mathf.Abs(difference.Hours) + ":"  + Mathf.Abs(difference.Minutes) + ":" + Mathf.Abs(difference.Seconds);
         

        }

    }


    void UpdateTime()
    {

        DateTime currDate = System.DateTime.Now;
        PlayerPrefs.SetString("LastClaimed", currDate.ToString());
    }

    public void ClaimPrize()
    {
        PlayerPrefs.SetInt("1stTime", 2);
        this.gameObject.GetComponent<Button>().interactable = false;
        DateTime currDate = System.DateTime.Now;
        PlayerPrefs.SetString("LastClaimed", currDate.ToString());
        print("Claimed");

    }

    public void Close()
    {
        PlayerPrefs.SetInt("1stTime", 3);
        unlocked.SetActive(false);
        unlocked.GetComponent<CanvasGroup>().alpha = 0;
    }
    public void LoadBeatEmUpcene(string name)
    {
        PlayerPrefs.SetInt("1stTime", 3);
        StartCoroutine(LoadLevel(name));
        //Firebase.Analytics.FirebaseAnalytics.LogEvent("Beat em up Mode Clicked");
        GameAnalyticsSDK.GameAnalytics.NewProgressionEvent(GameAnalyticsSDK.GAProgressionStatus.Start, "Beat em up Mode Clicked");//HAiderGA
    }

    AsyncOperation asyncLoadLevel;
    public IEnumerator LoadLevel(string sceneName)
    {
        loadingScreen.SetActive(true);
        asyncLoadLevel = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        while (!asyncLoadLevel.isDone)
        {
            loadingBar.fillAmount = asyncLoadLevel.progress / 0.9f;
            yield return null;
        }
    }
}
