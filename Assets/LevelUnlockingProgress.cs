using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUnlockingProgress : MonoBehaviour
{
    // Start is called before the first frame update
    public string levelPref;
    public string progressPref;
    void Start()
    {
        if (PlayerPrefs.GetInt(levelPref) >= PlayerPrefs.GetInt(progressPref))
        {


            PlayerPrefs.SetInt(progressPref, PlayerPrefs.GetInt(progressPref)+1);
            //Debug.LogError("offfflocckparking" + unlocklevelsParkingMode);
        }

        //if (PlayerPrefs.GetInt("ParkingModeLevel") >= PlayerPrefs.GetInt("unlockParkingModeLevel"))
        //{


        //    PlayerPrefs.SetInt("unlockParkingModeLevel", PlayerPrefs.GetInt("ParkingModeLevel"));
        //    //Debug.LogError("offfflocckparking" + unlocklevelsParkingMode);
        //}

        //if (PlayerPrefs.GetInt("OFFRoadMode")>= PlayerPrefs.GetInt("unlockOFFRoadMode"))
        //      {


        //	PlayerPrefs.SetInt("unlockOFFRoadMode", PlayerPrefs.GetInt("OFFRoadMode"));
        //	//Debug.LogError("offfflocckroad" + unlocklevelsOFFRoad);
        //	unlocklevelsOFFRoad = PlayerPrefs.GetInt("unlockOFFRoadMode");
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
