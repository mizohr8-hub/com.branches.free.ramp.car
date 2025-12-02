using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RGSK;
using UnityEngine.SceneManagement;

public class TournamentManager : MonoBehaviour
{

    public static TournamentManager Instance;

    [Header("References")]
    public RGSK.RaceManager RaceManager;
    public RGSK.RaceUI RaceUIManager;
    public RGSK.SoundManager RGSKSoundManager;

    [Header("PlayerDetails")]
    public GameObject[] PlayerPrefabs;

    [Header("int")]
    public int Stage = 0;
    public int PlayerRank;

    [Header("Bools")]
    public bool QuaterFinal;
    public bool SemiFinal;
    public bool Final;

    [Header("UI Screens")]
    public GameObject[] TournamnetMatchinngScreens;
    public GameObject CongratulationsPanel;
    public GameObject FailPanel;
    public GameObject RCCControllers;

    [Header("UI Buttons")]
    public GameObject ContinueButton;

    [Header("Levels")]
    public GameObject[] Levels;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        if (PlayerPrefs.GetInt("PlayingTournament") == 1)
        {
            RaceManager.totalLaps = 1;
            RaceManager.totalRacers = 2;
            Levels[0].SetActive(true);
        }
        else
        {
            if (PlayerPrefs.GetInt("PlayingRacing") == 1)
            {
                for (int i = 0; i < Levels.Length; i++)
                {
                    Levels[i].SetActive(false);
                }

                //Assign Racers and Lavels
                int R = PlayerPrefs.GetInt("RacingModeLevel") ;
                if (R > 4 || R <= 1)
                {
                    R = 1;
                    R++;
                }
                RaceManager.totalRacers = R;
                if (PlayerPrefs.GetInt("PlayingRacing") != 1 && PlayerPrefs.GetInt("PlayingRacing") != 1)
                {
                    RaceManager.totalLaps = R;
                }
                else
                {
                    RaceManager.totalLaps = 1;
                }
                Levels[PlayerPrefs.GetInt("RacingModeLevel")].SetActive(true);
                Debug.LogError("r = " + R);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {

        //Matching Screens Working
        if (PlayerPrefs.GetInt("TournamentStage") == 0)
        {
            StartCoroutine(nameof(StartTournament));
        }
    }

    IEnumerator StartTournament()
    {
        if (PlayerPrefs.GetInt("TournamentStage") == 0)
        {
            TournamnetMatchinngScreens[0].SetActive(true);
            yield return new WaitForSeconds(12f);
        }
        if (PlayerPrefs.GetInt("TournamentStage") == 1)
        {
            yield return new WaitForSecondsRealtime(8f);
            RaceUIManager.Restart();
        }
        if (PlayerPrefs.GetInt("TournamentStage") == 2)
        {
            yield return new WaitForSecondsRealtime(3f);
            RaceUIManager.Restart();
        }
        for (int i = 0; i < TournamnetMatchinngScreens.Length; i++)
        {
            TournamnetMatchinngScreens[i].SetActive(false);
        }

    }

    public void SetRank(int Rank)
    {
        PlayerRank = Rank;
    }

    public void PlayNext()
    {
        Time.timeScale = 1;
        if (PlayerPrefs.GetInt("PlayingOpenWorld") == 1)
        {
            SceneManager.LoadScene("OpenWorld");
        }
        else
        {
            if (PlayerPrefs.GetInt("PlayingTournament") == 1)
        {
                if (PlayerRank == 1)
                {
                    PlayerPrefs.SetInt("TournamentStage", PlayerPrefs.GetInt("TournamentStage") + 1);
                    if (PlayerPrefs.GetInt("TournamentStage") <= 1)
                    {
                        TournamnetMatchinngScreens[1].SetActive(true);
                        SemiFinal = true;
                        StartCoroutine(nameof(StartTournament));
                    }
                    if (PlayerPrefs.GetInt("TournamentStage") == 2)
                    {
                        TournamnetMatchinngScreens[2].SetActive(true);
                        Final = true;
                        StartCoroutine(nameof(StartTournament));
                    }
                    if (PlayerPrefs.GetInt("TournamentStage") == 3)
                    {
                        RaceUIManager.raceCompletePanel.SetActive(false);
                        CongratulationsPanel.SetActive(true);
                    }
                }
                else
                {
                    FailPanel.SetActive(true);
                }
        }
        else
        {
                PlayerPrefs.SetInt("RacingModeLevel", PlayerPrefs.GetInt("RacingModeLevel") + 1);
                RaceUIManager.Restart();
                if (PlayerPrefs.GetInt("PlayingRacing") != 1 && PlayerPrefs.GetInt("PlayingRacing") != 1)
                {
                    ContinueButton.SetActive(false);
                }
            }
        }
    }

    private void OnDisable()
    {
    }
    // Update is called once per frame
    void Update()
    {

    }
}
