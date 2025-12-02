using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RGSK;
public class RankChecker : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Opponenet") || (other.gameObject.CompareTag("Player")))
        {
            print(PlayerPrefs.GetString("PlayerName") + " Player > Opponents " + RaceManager.instance.racerName);
        }
    }
}
