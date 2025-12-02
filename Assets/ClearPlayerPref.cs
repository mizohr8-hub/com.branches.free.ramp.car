using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearPlayerPref : MonoBehaviour
{

    private void OnEnable()
    {
        PlayerPrefs.SetInt("PlayingOpenWorld", 0);
        PlayerPrefs.SetInt("Modeis", 9999999);
        PlayerPrefs.SetInt("PlayingRacing", 9999999);
        PlayerPrefs.SetInt("TournamentStage", 10);
        PlayerPrefs.SetInt("PlayingTournament", 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
