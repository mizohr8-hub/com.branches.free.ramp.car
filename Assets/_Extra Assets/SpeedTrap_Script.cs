using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RGSK;
using UnityEngine.UI;
public class SpeedTrap_Script : MonoBehaviour
{
    public static SpeedTrap_Script instance;
 
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        gameObject.GetComponent<Checkpoint>().checkpointType = Checkpoint.CheckpointType.Speedtrap;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player" && RaceManager.instance._raceType== RaceManager.RaceType.SpeedTrap)
        {
            Debug.LogError("Triggered object : " + other.name);
           // RaceUiPopUp_Script.instance.BestTimeText_.text ="Best? " +RCC_SceneManager.Instance.activePlayerVehicle.GetComponent<Statistics>().currentLapTime;//jjtech
            RaceUI.instance.numberoftracks++;
            //if (RaceUiPopUp_Script.instance && (other.GetComponent<StoreReference_Script>()))
            //{
            //    RaceUiPopUp_Script.instance.UpdateSpeed(other.GetComponent<StoreReference_Script>().Speedmter.GetComponent<Text>().text.ToString()); //rEMEBER tHIS

            //}
           
        }
    
    }
}
