using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelENVSetParkingMode : MonoBehaviour
{
    private void Awake()
    {
        
        transform.GetChild(PlayerPrefs.GetInt("ParkingModeLevel")).gameObject.SetActive(true);
    }
}
