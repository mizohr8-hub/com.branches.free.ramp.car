using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TDMTimeHandlerScript : MonoBehaviour
{
    public Text timerText;
    public float time = 600;

    private bool isfive,isFour,isThree,isTwo,isOne;
    public bool timesUp;

    public GameObject TDMCompletaionScreen;
    

    void Start()
    {
        StartCoundownTimer();
    }

    void StartCoundownTimer()
    {
        if (timerText != null)
        {
            //time = 1200;
            timerText.text = "Time Left: 20:00:000";
            InvokeRepeating("UpdateTimer", 0.0f, 0.01667f);
        }
    }

    void UpdateTimer()
    {
        if (timerText != null && !timesUp)
        {
            time -= Time.deltaTime;
            string minutes = Mathf.Floor(time / 60).ToString("00");
            string seconds = (time % 60).ToString("00");
            //string fraction = ((time * 100) % 100).ToString("000");
            
            
            if (time<=0)
            {
                print("timesUp ");
                timesUp = true;
                TDMCompletaionScreen.SetActive(true);
            }
            else
            {
                if (time<5 && !isfive)
                {
                    isfive = true;
                    //print("Tinggggg");
                    //SoundHandlerScript.Insatnce.TriggerTimerSFX();
                }
                else if (time < 4 && !isFour)
                {
                    isFour = true;
                    //SoundHandlerScript.Insatnce.TriggerTimerSFX();
                    //print("Tinggggg");
                }
                else if (time < 3 && !isThree)
                {
                    isThree = true;
                    //SoundHandlerScript.Insatnce.TriggerTimerSFX();
                    //print("Tinggggg");
                }
                else if (time < 2 && !isTwo)
                {
                    isTwo = true;
                    //SoundHandlerScript.Insatnce.TriggerTimerSFX();
                    //print("Tinggggg");
                }
                else if (time < 1 && !isOne)
                {
                    isOne = true;
                    //SoundHandlerScript.Insatnce.TriggerTimerSFX();
                    //print("Tinggggg");
                }

                timerText.text = minutes + ":" + seconds;
            }
            //timerText.text = "Time Left: " + minutes + ":" + seconds + ":" + fraction;
        }
    }

}
