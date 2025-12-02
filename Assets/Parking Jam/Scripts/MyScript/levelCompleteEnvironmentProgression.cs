using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Watermelon;

public class levelCompleteEnvironmentProgression : MonoBehaviour
{
    [Header("Power")]
    public GameObject environmentWork;
    public Image environmentLockingProgressBar;
    public Text environmentLevelTarget;
    public GameObject environmentUnlockedPanel;
    public Image environmentImg;
    public Sprite[] environmentSprites;
    public int[] environmentTagretLevels;
    public string[] environmentUnlockedText;
    public AudioSource fillSound3;
    void Start()
    {
        if (/*!GameControllerParkingJam.IsBossLevel &&*/ PlayerPrefs.GetInt("IsChallenges") == 0)
        {
            if (GameControllerParkingJam.CurrentLevelId < 320)
            {
                StartCoroutine(EnvironmentUnlocking());
            }
            else
            {
                environmentWork.SetActive(false);
            }
        }
        else
        {
            environmentWork.SetActive(false);

        }
    }

    IEnumerator EnvironmentUnlocking()
    {
        environmentLevelTarget.text = "UNLOCK AFTER LEVEL " + (environmentTagretLevels[PlayerPrefs.GetInt("GameEnvironmentLock")]
            );
        
        int diff = 0;
        float diff1 = 0;
        if (PlayerPrefs.GetInt("GameEnvironmentLock") == 0)
        {
            diff = GameControllerParkingJam.CurrentLevelId;
            diff1 = environmentTagretLevels[PlayerPrefs.GetInt("GameEnvironmentLock")];
        }
        if (PlayerPrefs.GetInt("GameEnvironmentLock") == 1)
        {
            diff = GameControllerParkingJam.CurrentLevelId - 39;
            diff1 = 40f;
        }if (PlayerPrefs.GetInt("GameEnvironmentLock") == 2)
        {
            diff = GameControllerParkingJam.CurrentLevelId - 79;
            diff1 = 40f;
        }if (PlayerPrefs.GetInt("GameEnvironmentLock") == 3)
        {
            diff = GameControllerParkingJam.CurrentLevelId - 119;
            diff1 = 40f;
        }
        if (PlayerPrefs.GetInt("GameEnvironmentLock") == 4)
        {
            diff = GameControllerParkingJam.CurrentLevelId - 159;
            diff1 = 40f;
        }
        if (PlayerPrefs.GetInt("GameEnvironmentLock") == 5)
        {
            diff = GameControllerParkingJam.CurrentLevelId- 199;
            diff1 = 40f;
        }
        if (PlayerPrefs.GetInt("GameEnvironmentLock") == 6)
        {
            diff = GameControllerParkingJam.CurrentLevelId - 239;
            diff1 = 40f;
        }
        if (PlayerPrefs.GetInt("GameEnvironmentLock") == 7)
        {
            diff = GameControllerParkingJam.CurrentLevelId - 279;
            diff1 = 40f;
        }
        float temp22 = diff / diff1;
        float temp33 = (diff+1)/ diff1;
        environmentImg.sprite = environmentSprites[PlayerPrefs.GetInt("GameEnvironmentLock")];
        environmentLockingProgressBar.fillAmount = temp22;
        yield return new WaitForSecondsRealtime(3.5f);
        fillSound3.Play();
        while (true)
        {
            environmentLockingProgressBar.fillAmount += 0.005f;
            yield return new WaitForSecondsRealtime(0.025f);
            if (environmentLockingProgressBar.fillAmount >= temp33)
            {
                break;
            }
        }
        environmentLockingProgressBar.fillAmount = temp33;
        if (environmentLockingProgressBar.fillAmount >= 1)
        {
            yield return new WaitForSecondsRealtime(1f);
            environmentWork.SetActive(false);
            environmentUnlockedPanel.SetActive(true);
            environmentUnlockedPanel.GetComponent<Image>().sprite = environmentSprites[PlayerPrefs.GetInt("GameEnvironmentLock")]; ;
            environmentUnlockedPanel.GetComponentInChildren<Text>().text = environmentUnlockedText[PlayerPrefs.GetInt("GameEnvironmentLock")];
            if (PlayerPrefs.GetInt("GameEnvironmentLock") == 0)
            {
                PlayerPrefs.SetInt("TucTucSelectedEnv",1);
            }
            else if (PlayerPrefs.GetInt("GameEnvironmentLock") == 1)
            {
                PlayerPrefs.SetInt("TucTucSelectedEnv", 2);
            }
            else if (PlayerPrefs.GetInt("GameEnvironmentLock") == 2)
            {
                PlayerPrefs.SetInt("TucTucSelectedEnv", 3);
            }
            else if (PlayerPrefs.GetInt("GameEnvironmentLock") == 3)
            {
                PlayerPrefs.SetInt("TucTucSelectedEnv", 4);
            }
            else if (PlayerPrefs.GetInt("GameEnvironmentLock") == 4)
            {
                PlayerPrefs.SetInt("TucTucSelectedEnv", 5);
            }
            else if (PlayerPrefs.GetInt("GameEnvironmentLock") == 5)
            {
                PlayerPrefs.SetInt("TucTucSelectedEnv", 6);
            }
            else if (PlayerPrefs.GetInt("GameEnvironmentLock") == 6)
            {
                PlayerPrefs.SetInt("TucTucSelectedEnv", 7);
            }
            else if (PlayerPrefs.GetInt("GameEnvironmentLock") == 7)
            {
                PlayerPrefs.SetInt("TucTucSelectedEnv", 8);
            }
        }
    }

}
