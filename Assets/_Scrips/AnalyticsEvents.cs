using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyticsEvents : MonoBehaviour
{
    public bool isMultiplayer = false;
    public string msg;
    private void OnEnable()
    {
        if (!isMultiplayer)
        {
            //Firebase.Analytics.FirebaseAnalytics.LogEvent("Level No " + PlayerPrefs.GetInt("LevelNum") + msg);
            GameAnalyticsSDK.GameAnalytics.NewProgressionEvent(GameAnalyticsSDK.GAProgressionStatus.Start, "Level No " + PlayerPrefs.GetInt("LevelNum") + msg);//HAiderGA
        }
        else
        {
            //Firebase.Analytics.FirebaseAnalytics.LogEvent("Multiplayer mode" + msg);
            GameAnalyticsSDK.GameAnalytics.NewProgressionEvent(GameAnalyticsSDK.GAProgressionStatus.Start, "Multiplayer mode" + msg);//HAiderGA
        }
    }
}
