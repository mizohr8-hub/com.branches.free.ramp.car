using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWorldMissions : MonoBehaviour
{
    public GameObject Timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "RacingMission")
        {
            if (OpenWorldManager.Instance)
            {
                for (int i = 0; i < OpenWorldManager.Instance.RacingMissionPanelButtons.Length; i++)
                {
                    OpenWorldManager.Instance.RacingMissionPanelButtons[i].SetActive(false);
                }
                OpenWorldManager.Instance.RacingMissionPanelButtons[0].SetActive(true);
                OpenWorldManager.Instance.RacingMissionPanel.SetActive(true);
            }
        }

        if (other.gameObject.tag == "OWMissionCompleted")
        {
            if (OpenWorldManager.Instance)
            {
                PlayerPrefs.SetInt("Currency", PlayerPrefs.GetInt("Currency") + 1000);
                OpenWorldManager.Instance.LevelCompletePanel.SetActive(true);
             
                Timer.SetActive(false);





            }
        }

        if (other.gameObject.tag == "RooftopFall")
        {
            Rigidbody carRigidbody = GetComponent<Rigidbody>();

            if (carRigidbody != null)
            {

                carRigidbody.velocity = Vector3.zero;
            }
            if (OpenWorldManager.Instance)
            {
                OpenWorldManager.Instance.RespawnRootop();
            }
        }

        if (other.gameObject.tag == "RampMission")
        {
            Rigidbody carRigidbody = GetComponent<Rigidbody>();

            if (carRigidbody != null)
            {

                carRigidbody.velocity = Vector3.zero;
            }
            if (OpenWorldManager.Instance)
            {
                OpenWorldManager.Instance.MissionPanelText.text = "A rooftop ramps mission is an exciting adventure where participants navigate their way across rooftops, overcoming obstacles and challenges along the way. This mission typically involves agility, quick thinking, and precise movements as individuals traverse the rooftops, utilizing ramps and other structures to reach their destination. Are you prepared to take on this exhilarating challenge?".ToString();
                for (int i = 0; i < OpenWorldManager.Instance.RacingMissionPanelButtons.Length; i++)
                {
                    OpenWorldManager.Instance.RacingMissionPanelButtons[i].SetActive(false);
                }
                OpenWorldManager.Instance.RacingMissionPanelButtons[1].SetActive(true);
                OpenWorldManager.Instance.RacingMissionPanel.SetActive(true);
            }
        }
        if (other.gameObject.tag == "TreasureMission")
        {
            Rigidbody carRigidbody = GetComponent<Rigidbody>();

            if (carRigidbody != null)
            {

                carRigidbody.velocity = Vector3.zero;
            }
            if (OpenWorldManager.Instance)
            {
                OpenWorldManager.Instance.MissionPanelText.text = "The Hunt 15 Treasure Mission is an exhilarating quest where participants embark on a journey to locate and secure 15 hidden treasures. Are you ready to embark on this exciting treasure hunt?".ToString();
                for (int i = 0; i < OpenWorldManager.Instance.RacingMissionPanelButtons.Length; i++)
                {
                    OpenWorldManager.Instance.RacingMissionPanelButtons[i].SetActive(false);
                }
                OpenWorldManager.Instance.RacingMissionPanelButtons[2].SetActive(true);
                OpenWorldManager.Instance.RacingMissionPanel.SetActive(true);
            }
        }
        if (other.gameObject.tag == "SecondRampMission.")
        {
            Rigidbody carRigidbody = GetComponent<Rigidbody>();

            if (carRigidbody != null)
            {

                carRigidbody.velocity = Vector3.zero;
            }
            if (OpenWorldManager.Instance)
            {
                OpenWorldManager.Instance.MissionPanelText.text = "The Ramp Mission Challenge is an adrenaline-pumping adventure where participants tackle a series of ramps and obstacles in various settings. From skate parks to urban landscapes, participant showcase their skills in navigating ramps with precision and flair. It's a thrilling test of agility, balance, and creativity as individuals aim to conquer each ramp and complete the challenge. Are you prepared to take on this high-flying mission?".ToString();
                for (int i = 0; i < OpenWorldManager.Instance.RacingMissionPanelButtons.Length; i++)
                {
                    OpenWorldManager.Instance.RacingMissionPanelButtons[i].SetActive(false);
                }
                OpenWorldManager.Instance.RacingMissionPanelButtons[3].SetActive(true);
                OpenWorldManager.Instance.RacingMissionPanel.SetActive(true);
            }
        }
        if (other.gameObject.tag == "CheckpointMission")
        {
            Rigidbody carRigidbody = GetComponent<Rigidbody>();

            if (carRigidbody != null)
            {
                
                carRigidbody.velocity = Vector3.zero;
            }
            if (OpenWorldManager.Instance)
            {

                OpenWorldManager.Instance.MissionPanelText.text = " Collect the 5 Checkpoint Challenge. Race against time to gather checkpoints scattered across the area. It's a test of speed and strategy. Ready to take it on?".ToString();
                for (int i = 0; i < OpenWorldManager.Instance.RacingMissionPanelButtons.Length; i++)
                {
                    OpenWorldManager.Instance.RacingMissionPanelButtons[i].SetActive(false);
                }
                OpenWorldManager.Instance.RacingMissionPanelButtons[4].SetActive(true);
                OpenWorldManager.Instance.RacingMissionPanel.SetActive(true);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "RacingMission")
        {
            if (OpenWorldManager.Instance)
            {
                OpenWorldManager.Instance.ActivePlayer.GetComponent<Rigidbody>().drag = 2;
            }
        }
        if (other.gameObject.tag == "RampMission")
        {
            if (OpenWorldManager.Instance)
            {
                OpenWorldManager.Instance.ActivePlayer.GetComponent<Rigidbody>().drag = 2;
            }
        }
        if (other.gameObject.tag == "TreasureMission")
        {
            if (OpenWorldManager.Instance)
            {
                OpenWorldManager.Instance.ActivePlayer.GetComponent<Rigidbody>().drag = 2;
            }
        }
        if (other.gameObject.tag == "SecondRampMission")
        {
            if (OpenWorldManager.Instance)
            {
                OpenWorldManager.Instance.ActivePlayer.GetComponent<Rigidbody>().drag = 2;
            }
        }
        if (other.gameObject.tag == "TreasureMission")
        {
            if (OpenWorldManager.Instance)
            {
                OpenWorldManager.Instance.ActivePlayer.GetComponent<Rigidbody>().drag = 2;
            }
        }
        if (other.gameObject.tag == "CheckpointMission")
        {
            if (OpenWorldManager.Instance)
            {
                OpenWorldManager.Instance.ActivePlayer.GetComponent<Rigidbody>().drag = 2;
            }
        }
        Invoke(nameof(RemoveDrag), 2f);
    }
    public void RemoveDrag()
    {
        Time.timeScale = 1;
        if (OpenWorldManager.Instance)
        {
            OpenWorldManager.Instance.ActivePlayer.GetComponent<Rigidbody>().drag = 0.01f;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Time.timeScale = 1;
        if (OpenWorldManager.Instance)
        {
            OpenWorldManager.Instance.ActivePlayer.GetComponent<Rigidbody>().drag = 0.01f;
        }
        if (other.gameObject.tag == "RacingMission")
        {
            if (OpenWorldManager.Instance)
            {
                OpenWorldManager.Instance.RacingMissionPanel.SetActive(false);
            }
        }
        if (other.gameObject.tag == "CheckpointMission")
        {
            if (OpenWorldManager.Instance)
            {
                OpenWorldManager.Instance.RacingMissionPanel.SetActive(false);
            }
        }
        if (other.gameObject.tag == "RampMission")
        {
            if (OpenWorldManager.Instance)
            {
                OpenWorldManager.Instance.RacingMissionPanel.SetActive(false);
            }
        }
        if (other.gameObject.tag == "TreasureMission")
        {
            if (OpenWorldManager.Instance)
            {
                OpenWorldManager.Instance.RacingMissionPanel.SetActive(false);
            }
        }
        if (other.gameObject.tag == "SecondRampMission")
        {
            if (OpenWorldManager.Instance)
            {
                OpenWorldManager.Instance.RacingMissionPanel.SetActive(false);
            }
        }
        if (other.gameObject.tag == "TreasureMission")
        {
            if (OpenWorldManager.Instance)
            {
                OpenWorldManager.Instance.RacingMissionPanel.SetActive(false);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
