using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RGSK;
public class LevelData_Script : MonoBehaviour
{
    public Transform LevelPath, LevelCheckPoints;
    private void Awake()
    {
        RaceManager.instance.pathContainer = LevelPath;
        RaceManager.instance.checkpointContainer = LevelCheckPoints;
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
