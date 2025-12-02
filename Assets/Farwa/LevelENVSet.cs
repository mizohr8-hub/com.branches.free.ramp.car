using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelENVSet : MonoBehaviour
{
    private void Awake()
    {
        transform.GetChild(PlayerPrefs.GetInt("OFFRoadMode")).gameObject.SetActive(true);
    }
}
