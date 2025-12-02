using RGSK;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Samples;
using UnityEngine.SceneManagement;

public class OpenWorldManager : MonoBehaviour
{
    public static OpenWorldManager Instance;

    [Header("Player Details")]
    public GameObject ActivePlayer;
    public GameObject[] Player;
    public string ResourceFolder = "PlayerVehicles/"; //the name of the folder within the Resources folder where your vehicles are stored.

    [Header("Panels")]
    public Text loadingText;
    public Text MissionPanelText;
    public Text CoinsBarText;
    public GameObject[] Respawnbtn;
    public GameObject[] TreasuresIcons;
    public GameObject CoinsBar;
    public GameObject TimeBar;
    public GameObject loadingPanel;
    public GameObject RacingMissionPanel;
    public GameObject LevelCompletePanel;
    public GameObject MissionFailPanel;
    public GameObject ObjectivePanel;
    public GameObject FadeOutPanel;
    public GameObject RampMissionPickups;
    public GameObject[] RacingMissionPanelButtons;

    [Header("PlayerSpawning Point")]
    public Transform PlayerSpawningPoint;
    public string stringValue; // Get the string value from PlayerPrefs
    int intValue;

    [Header("Levels")]
    public GameObject[] Treasures;
    public GameObject AllMissions;
    public GameObject checkpointRamp;
    public GameObject[] RampTracks;
    public Transform[] RampTracksSpawningPoints;
    public Transform RampMissionSpawningPoint;
    public Transform CheckpointSpawningPoint;
    public Transform TreasureMissionSpawningPoint;
    public GameObject timer;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        stringValue = PlayerPrefs.GetString("PlayerVehicle");
        if (AdsManager.Instance)
        {
            AdsManager.Instance.ShowAdaptiveBanner();
        }

        if (int.TryParse(stringValue, out intValue)) // Try parsing the string to an int
        {
            PlayerPrefs.SetInt("PlayerVehicleNo", intValue); // Store the int value in PlayerPrefs
        }
        else
        {
            Debug.LogError("Failed to parse string to int!"); // Handle the case where parsing fails
        }

        if (PlayerPrefs.GetInt("LevelNum") > 1)
        {
            RampMissionPickups.SetActive(true);
        }
        else
        {
            RampMissionPickups.SetActive(false);
        }

        //ObjectivePanel
        if (PlayerPrefs.GetInt("OnlyOnce") == 0)
        {
            ObjectivePanel.SetActive(true);
            PlayerPrefs.SetInt("OnlyOnce", 1);
        }
        else
        {
            ObjectivePanel.SetActive(false);
        }

        Invoke(nameof(TrunOffPanel), 3f);

        //Spawn Player
        Debug.Log("Loading Player Vehicle : " + PlayerPrefs.GetInt("SelectedPlayer"));
        ActivePlayer =  Instantiate(Player[PlayerPrefs.GetInt("SelectedPlayer")], PlayerSpawningPoint.position, PlayerSpawningPoint.rotation);
    }

    public void TrunOffPanel()
    {
        FadeOutPanel.SetActive(false);
    }

    public void StartMission(int MissionNumber)
    {
        FallenNumber = 0;
        PlayerPrefs.SetInt("CoinsPoints", 0);
        RacingMissionPanel.SetActive(false);
        AllMissions.SetActive(false);
        TimeBar.SetActive(true);
        this.GetComponent<AudioSource>().enabled = false;
        if (MissionNumber == 1)
        {
            loadingPanel.SetActive(true);
            PlayerPrefs.SetInt("PlayingRacing", 1);
            PlayerPrefs.SetInt("TournamentStage", 10);
            PlayerPrefs.SetInt("PlayingTournament", 0);
            StartCoroutine(LoadingScenes("RacingMode"));
        }
        if (MissionNumber == 2)
        {
            Respawnbtn[0].SetActive(false);
            Respawnbtn[1].SetActive(true);
            RampMission();
        }
        if (MissionNumber == 3)
        {
            TreasureHunt();
        }
        if(MissionNumber == 4)
        {
            loadingPanel.SetActive(true);
            PlayerPrefs.SetInt("Modeis",0);
            StartCoroutine(LoadingScenes("MyCarRaceSolo"));
        }
        if (MissionNumber == 5)
        {
            CheckpointMission();
        }
    }

    IEnumerator LoadingScenes(string name)
    {
        loadingText.text = "Initializing...";
        yield return new WaitForSeconds(2f);
        loadingText.text = "Loading tracks...";
        yield return new WaitForSeconds(1f);
        loadingText.text = "Preparing your car...";
        yield return new WaitForSeconds(1f);
        loadingText.text = "Loading...";
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(name);
    }
    IEnumerator LoadingOWMission()
    {
        loadingText.text = "Initializing...";
        yield return new WaitForSeconds(2f);
        loadingText.text = "Loading tracks...";
        yield return new WaitForSeconds(1f);
        loadingText.text = "Preparing your car...";
        yield return new WaitForSeconds(1f);
        loadingText.text = "Loading...";
        ActivePlayer.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        yield return new WaitForSeconds(5f);
        loadingPanel.SetActive(false);
    }
    int FallenNumber = 0;
    public void RespawnRootop()
    {
        FallenNumber++;
        if (FallenNumber >= 2)
        {
            Restart();
        }
        else
        {
            loadingPanel.SetActive(true);
            ActivePlayer.transform.position = RampMissionSpawningPoint.position;
            ActivePlayer.transform.rotation = RampMissionSpawningPoint.rotation;
            ActivePlayer.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            StartCoroutine(LoadingOWMission());
        }
    }

    public void RespawnDefault()
    {
        /*loadingPanel.SetActive(true);
        ActivePlayer.transform.position = PlayerSpawningPoint.position;
        ActivePlayer.transform.rotation = PlayerSpawningPoint.rotation;
        ActivePlayer.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        StartCoroutine(LoadingOWMission());
        timer.SetActive(false);
        CoinsBar.SetActive(false);*/
        loadingPanel.SetActive(true);
        SceneManager.LoadSceneAsync(8);



    }

    public void CheckpointMission()
    {
        loadingPanel.SetActive(true);
        checkpointRamp.SetActive(true);
        CoinsBar.SetActive(true);
        TreasuresIcons[5].SetActive(true);
        ActivePlayer.transform.position = CheckpointSpawningPoint.position;
        ActivePlayer.transform.rotation = CheckpointSpawningPoint.rotation;
        ActivePlayer.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        StartCoroutine(LoadingOWMission());
    }
    public void RampMission()
    {
        loadingPanel.SetActive(true);
        int e = Random.Range(0, 4);
        RampTracks[e].SetActive(true);
        RampMissionSpawningPoint = RampTracksSpawningPoints[e];
        ActivePlayer.transform.position = RampMissionSpawningPoint.position;
        ActivePlayer.transform.rotation = RampMissionSpawningPoint.rotation;
        ActivePlayer.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        StartCoroutine(LoadingOWMission());
    }
    public int p;

    public void TreasureHunt()
    {

        PlayerPrefs.SetInt("CoinsPoints", 0);

        CoinsBar.SetActive(true);
        loadingPanel.SetActive(true);
        p = Random.Range(0, 4);
        Treasures[p].SetActive(true);
        TreasuresIcons[p].SetActive(true);
        ActivePlayer.transform.position = TreasureMissionSpawningPoint.position;
        ActivePlayer.transform.rotation = TreasureMissionSpawningPoint.rotation;
        ActivePlayer.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        StartCoroutine(LoadingOWMission());
    }
    public void Home()
    {
        if (AdsManager.Instance)
        {
            AdsManager.Instance.ShowInterstitial();
        }

        loadingPanel.SetActive(true);
        StartCoroutine(LoadingScenes("MainMenu"));
    }
    public void Restart()
    {
        if (AdsManager.Instance)
        {
            AdsManager.Instance.ShowInterstitial();
        }
        loadingPanel.SetActive(true);
        StartCoroutine(LoadingScenes("OpenWorld"));
    }

    public void LevelFail()
    {
        MissionFailPanel.SetActive(true);
    }

    public void Replay()
    {
        loadingPanel.SetActive(true);
    }
    public void Continue()
    {
        if (AdsManager.Instance)
        {
            AdsManager.Instance.ShowInterstitial();
        }
        for (int i = 0; i < RampTracks.Length; i++)
        {
            RampTracks[i].SetActive(false);
        }
        Respawnbtn[1].SetActive(false);
        Respawnbtn[0].SetActive(true);
        LevelCompletePanel.SetActive(false);
        CoinsBar.SetActive(false);
        for (int i = 0; i < Treasures.Length; i++)
        {
            Treasures[i].SetActive(false);
        }
        this.GetComponent<AudioSource>().enabled = true;
        AllMissions.SetActive(true);      
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         CoinsBarText.text = PlayerPrefs.GetInt("CoinsPoints").ToString();
    }
}
