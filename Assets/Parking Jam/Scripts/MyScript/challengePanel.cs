using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Watermelon;

public class challengePanel : MonoBehaviour
{
    [Header("Challenges")]
    public GameObject LoadScreen;
    public GameObject Challenges;
    public Image loadfillBar;

    public Button[] challengesBtn;
    public Sprite challengeUnlocked, challenegPlayed;
    public int[] challengeEntryFee;
    public Text notEnoughCoins;

    public Text[] gameCoins;
    void Start()
    {
        gameCoins.ForEach((Text txt) => txt.text = PrefsSettings.GetInt(PrefsSettings.Key.CoinsCountID).ToString());
    }

    IEnumerator LoadingScene(string name)
    {
        GamePlayAdsHandler.adsHandler.CancelBanner();
       // Challenges.SetActive(false);
        LoadScreen.SetActive(true);   
        while (true)
        {
            loadfillBar.fillAmount += 0.025f; //0.025f
            yield return new WaitForSecondsRealtime(0.025f); //.025f;
            if (loadfillBar.fillAmount >= 1f)
            {
                break;
            }
        }
        SceneManager.LoadSceneAsync(name);

    }

    //challenges
    public void OpenChallenges()
    {
        if (PlayerPrefs.GetInt("settings_CurrentLevelID") > 9) //c1
        {
            PlayerPrefs.SetInt("ChallengesLock", 1);
        }
        if (PlayerPrefs.GetInt("settings_CurrentLevelID") > 19)//c2
        {
            PlayerPrefs.SetInt("ChallengesLock", 2);
        }
        if (PlayerPrefs.GetInt("settings_CurrentLevelID") > 29)//c3
        {
            PlayerPrefs.SetInt("ChallengesLock", 3);
        }
        if (PlayerPrefs.GetInt("settings_CurrentLevelID") > 39)//c4
        {
            PlayerPrefs.SetInt("ChallengesLock", 4);
        }
        if (PlayerPrefs.GetInt("settings_CurrentLevelID") > 49)//c5
        {
            PlayerPrefs.SetInt("ChallengesLock", 5);
        }
        if (PlayerPrefs.GetInt("settings_CurrentLevelID") > 59)//c6
        {
            PlayerPrefs.SetInt("ChallengesLock", 6);
        }
        if (PlayerPrefs.GetInt("settings_CurrentLevelID") > 69)//c7
        {
            PlayerPrefs.SetInt("ChallengesLock", 7);
        }
        if (PlayerPrefs.GetInt("settings_CurrentLevelID") > 79)//c8
        {
            PlayerPrefs.SetInt("ChallengesLock", 8);
        }
        if (PlayerPrefs.GetInt("settings_CurrentLevelID") > 89)//c9
        {
            PlayerPrefs.SetInt("ChallengesLock", 9);
        }
        if (PlayerPrefs.GetInt("settings_CurrentLevelID") > 99)//c10
        {
            PlayerPrefs.SetInt("ChallengesLock", 10);
        }
        if (PlayerPrefs.GetInt("settings_CurrentLevelID") > 109)//c11
        {
            PlayerPrefs.SetInt("ChallengesLock", 11);
        }
        if (PlayerPrefs.GetInt("settings_CurrentLevelID") > 119)//c12
        {
            PlayerPrefs.SetInt("ChallengesLock", 12);
        }
        if (PlayerPrefs.GetInt("settings_CurrentLevelID") > 129)//c13
        {
            PlayerPrefs.SetInt("ChallengesLock", 13);
        }
        if (PlayerPrefs.GetInt("settings_CurrentLevelID") > 139)//c14
        {
            PlayerPrefs.SetInt("ChallengesLock", 14);
        }
        if (PlayerPrefs.GetInt("settings_CurrentLevelID") > 149)//c15
        {
            PlayerPrefs.SetInt("ChallengesLock", 15);
        }

        Challenges.SetActive(true);

        for (int i = 0; i < PlayerPrefs.GetInt("ChallengesLock"); i++)
        {
            challengesBtn[i].enabled = true;
            challengesBtn[i].transform.GetChild(0).gameObject.SetActive(false);
            challengesBtn[i].transform.GetChild(1).gameObject.SetActive(false);
            if (PlayerPrefs.GetInt("Challenge" + i + "Completed") == 1)
            {
                challengesBtn[i].transform.GetChild(2).gameObject.SetActive(true);
                challengesBtn[i].transform.GetChild(3).gameObject.SetActive(true);
                challengesBtn[i].transform.GetChild(4).gameObject.SetActive(true);
                challengesBtn[i].transform.GetChild(5).gameObject.SetActive(true);
                challengesBtn[i].transform.GetChild(3).gameObject.GetComponent<Text>().text = "PLAY AGAIN";
                challengesBtn[i].GetComponent<Image>().sprite = challenegPlayed;
            }
            else
            {
                challengesBtn[i].transform.GetChild(2).gameObject.SetActive(true);
                challengesBtn[i].transform.GetChild(3).gameObject.SetActive(true);
                challengesBtn[i].transform.GetChild(4).gameObject.SetActive(true);
                challengesBtn[i].transform.GetChild(5).gameObject.SetActive(true);
                challengesBtn[i].GetComponent<Image>().sprite = challengeUnlocked;
            }
            int y = i;
            challengesBtn[i].onClick.AddListener(delegate { EnterChallenge(y); });
        }
    }


    public void EnterChallenge(int challengeID)
    {
        
        if (challengeEntryFee[challengeID] <= PrefsSettings.GetInt(PrefsSettings.Key.CoinsCountID))
        {
            PrefsSettings.SetInt(PrefsSettings.Key.CoinsCountID, PrefsSettings.GetInt(PrefsSettings.Key.CoinsCountID) - challengeEntryFee[challengeID]);
            PlayerPrefs.SetInt("IsChallenges", 1);
            PlayerPrefs.SetInt("ChallengeNumber", challengeID);
            gameCoins.ForEach((Text txt) => txt.text = PrefsSettings.GetInt(PrefsSettings.Key.CoinsCountID).ToString());
            
            StartCoroutine(LoadingScene("Game"));
        }
        else
        {
            notEnoughCoins.gameObject.SetActive(true);
        }
    }
}
