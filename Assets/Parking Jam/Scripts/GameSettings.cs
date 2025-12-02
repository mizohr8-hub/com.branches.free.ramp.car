using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject SoundButton;
    public GameObject vibratorButton;

    public Sprite[] switchState;

    public Button low, medium, high;
    public Sprite[] _low, _medium, _high;
    void Start()
    {
        SoundButton.GetComponent<Image>().sprite = switchState[PlayerPrefs.GetInt("SoundSwitchState")];
        vibratorButton.GetComponent<Image>().sprite = switchState[PlayerPrefs.GetInt("VibrationSwitchState")];
        QualitySetting(PlayerPrefs.GetInt("GameQuality"));
    }

    public void SoundState()
    {
        if (PlayerPrefs.GetInt("SoundSwitchState") == 0)
        {
            AudioListener.volume = 0;
            PlayerPrefs.SetInt("SoundSwitchState", 1);
            SoundButton.GetComponent<Image>().sprite = switchState[PlayerPrefs.GetInt("SoundSwitchState")];
        }
        else
        {
            AudioListener.volume = 1;
            PlayerPrefs.SetInt("SoundSwitchState", 0);
            SoundButton.GetComponent<Image>().sprite = switchState[PlayerPrefs.GetInt("SoundSwitchState")];
        }
        
    }
    
    public void VibrationState()
    {
        if (PlayerPrefs.GetInt("VibrationSwitchState") == 0)
        {
            PlayerPrefs.SetInt("VibrationSwitchState", 1);
            vibratorButton.GetComponent<Image>().sprite = switchState[PlayerPrefs.GetInt("VibrationSwitchState")];
        }
        else
        {
            PlayerPrefs.SetInt("VibrationSwitchState", 0);
            vibratorButton.GetComponent<Image>().sprite = switchState[PlayerPrefs.GetInt("VibrationSwitchState")];
        }
       
    }

    public void QualitySetting(int q)
    {
        if (q==0)
        {
            high.GetComponent<Image>().sprite = _high[1];
            medium.GetComponent<Image>().sprite = _medium[0];
            low.GetComponent<Image>().sprite = _low[0];
            PlayerPrefs.SetInt("GameQuality", 0);
        }
        else if (q==1)
        {
            high.GetComponent<Image>().sprite = _high[0];
            medium.GetComponent<Image>().sprite = _medium[1];
            low.GetComponent<Image>().sprite = _low[0];
            PlayerPrefs.SetInt("GameQuality", 1);
        }
        else if (q==2)
        {
            high.GetComponent<Image>().sprite = _high[0];
            medium.GetComponent<Image>().sprite = _medium[0];
            low.GetComponent<Image>().sprite = _low[1];
            PlayerPrefs.SetInt("GameQuality", 2);
        }
    }

}
