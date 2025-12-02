using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Watermelon;

public class levelCompletePowerProgression : MonoBehaviour
{
    [Header("Power")]
    public GameObject powerWork;
    public Image powerLockingProgressBar;
    public Text powerLevelTarget;
    public GameObject powerUnlockedPanel;
    public Image powerImg;
    public Sprite[] powerSprites;
    public int[] powerTagretLevels;
    public string[] UnlockedStatments;
    public AudioSource fillSound2;

    private void Start()
    {
        if (/*!GameControllerParkingJam.IsBossLevel &&*/ PlayerPrefs.GetInt("IsChallenges") == 0)
        {
            if (GameControllerParkingJam.CurrentLevelId < 70)
            {
                StartCoroutine(PowerUnlocking());
            }
            else
            {
                powerWork.SetActive(false);
            }
        }
        else
        {
            powerWork.SetActive(false);

        }
    }

    IEnumerator PowerUnlocking()
    {
        powerLevelTarget.text = "UNLOCK AFTER LEVEL " + (powerTagretLevels[PlayerPrefs.GetInt("GamePowerLock")]);
        int diff = 0;
        float diff1 = 0;
        if (PlayerPrefs.GetInt("GamePowerLock") == 0)
        {
            diff = GameControllerParkingJam.CurrentLevelId;
            diff1 = powerTagretLevels[PlayerPrefs.GetInt("GamePowerLock")];
        }
        if (PlayerPrefs.GetInt("GamePowerLock") == 1)
        {
            diff = GameControllerParkingJam.CurrentLevelId - 10;
            diff1 = 8f;
        }
        if (PlayerPrefs.GetInt("GamePowerLock") == 2)
        {
            diff = GameControllerParkingJam.CurrentLevelId - 18;
            diff1 = 10f;
        }
        if (PlayerPrefs.GetInt("GamePowerLock") == 3)
        {
            diff = GameControllerParkingJam.CurrentLevelId - 28;
            diff1 = 12f;
        }
        if (PlayerPrefs.GetInt("GamePowerLock") == 4)
        {
            diff = GameControllerParkingJam.CurrentLevelId - 40;
            diff1 = 30f;
        }
        float temp22 = diff / diff1;
        float temp33 = (diff + 1)/ diff1;
        powerImg.sprite = powerSprites[PlayerPrefs.GetInt("GamePowerLock")];
        powerLockingProgressBar.fillAmount = temp22;
        yield return new WaitForSecondsRealtime(2f);
        fillSound2.Play();
        while (true)
        {
            powerLockingProgressBar.fillAmount += 0.005f;
            yield return new WaitForSecondsRealtime(0.025f);
            if (powerLockingProgressBar.fillAmount >= temp33)
            {
                break;
            }
        }
        powerLockingProgressBar.fillAmount = temp33;
        if (powerLockingProgressBar.fillAmount >= 1)
        {
            yield return new WaitForSecondsRealtime(1f);
            powerWork.SetActive(false);
            powerUnlockedPanel.SetActive(true);
            powerUnlockedPanel.GetComponent<Image>().sprite = powerSprites[PlayerPrefs.GetInt("GamePowerLock")];
            powerUnlockedPanel.GetComponentInChildren<Text>().text = UnlockedStatments[PlayerPrefs.GetInt("GamePowerLock")];
            if (PlayerPrefs.GetInt("GamePowerLock")==0 && PlayerPrefs.GetInt("GamePowerIsLock1")==0)
            {
                PlayerPrefs.SetInt("PowersObstacles", PlayerPrefs.GetInt("PowersObstacles") + 3);
                PlayerPrefs.SetInt("GamePowerIsLock1", 1);
            }
            else if (PlayerPrefs.GetInt("GamePowerLock") == 1 && PlayerPrefs.GetInt("GamePowerIsLock2") == 0)
            {
                PlayerPrefs.SetInt("PowersNPC", PlayerPrefs.GetInt("PowersNPC")+3);
                PlayerPrefs.SetInt("GamePowerIsLock2",1);
            }
            else if (PlayerPrefs.GetInt("GamePowerLock") == 2 && PlayerPrefs.GetInt("GamePowerIsLock3") == 0)
            {
                PlayerPrefs.SetInt("PowersMissile", PlayerPrefs.GetInt("PowersMissile")+ 3);
                PlayerPrefs.SetInt("GamePowerIsLock3",1);
            }
            else if (PlayerPrefs.GetInt("GamePowerLock") == 3 && PlayerPrefs.GetInt("GamePowerIsLock4") == 0)
            {
                PlayerPrefs.SetInt("PowersHammer", PlayerPrefs.GetInt("PowersHammer")+3);
                PlayerPrefs.SetInt("GamePowerIsLock4",1);
            }
            else if (PlayerPrefs.GetInt("GamePowerLock") == 4 && PlayerPrefs.GetInt("GamePowerIsLock5") == 0)
            {
                PlayerPrefs.SetInt("PowersRoller", PlayerPrefs.GetInt("PowersRoller")+3);
                PlayerPrefs.SetInt("GamePowerIsLock5",1);
            }
            // PlayerPrefs.SetInt("GamePowerLock", PlayerPrefs.GetInt("GamePowerLock")+1);

        }
    }
}
