using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateUsScript : MonoBehaviour
{
    // Start is called before the first frame update

    public void RateUsStar()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.mizo.car.tycoon.game");
        PlayerPrefs.SetInt("RateUsPref", 1);
    }
    
}
