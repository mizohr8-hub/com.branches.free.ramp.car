using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayAdsHandler : MonoBehaviour
{
    public static GamePlayAdsHandler adsHandler;
    private void Awake()
    {
        if (adsHandler==null)
        {
            adsHandler = this;
        }
    }

    private void Start()
    {
       
        if (PlayerPrefs.GetInt("SoundSwitchState") == 0)
        {
            AudioListener.volume = 1;
        }
        else
        {
            AudioListener.volume = 0;
        }
    }
    public void CallingBannerInvoke()
    {
             
    }

    void CallBanner()
    {
        
    }

    public void CancelBanner()
    {
            
    }
}
