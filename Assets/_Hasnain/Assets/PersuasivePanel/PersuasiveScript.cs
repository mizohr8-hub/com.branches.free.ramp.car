using UnityEngine;
using System.Collections;

public class PersuasiveScript : MonoBehaviour
{
    public static PersuasiveScript instance;
    public GameObject persuasiveCanvas;
    public GameObject[] persuasivePanels;
    public GameObject[] closeButtons;
    private void Awake()
    {
       
        if (instance == null)
        {
            instance = this;
        }
    }
    public void ShowUnlockAll()
    {
        if (PlayerPrefs.GetInt("UnlockAllGame") == 0)
        {
            if (PlayerPrefs.GetInt("LevelNum") >= 5)
            {
                int rand = Random.Range(0, 30);
                if (rand <= 15 || (rand % 2 == 0))
                {
                    persuasiveCanvas.SetActive(true);
                    persuasivePanels[0].SetActive(true);
                    StartCoroutine(ShowCloseButton(0));
                    vehicles.SetActive(false);
                }
            }
        }
    }
    public void ShowUnlockLevels()
    {
        if (PlayerPrefs.GetInt("UnlockAllGame") == 0 && PlayerPrefs.GetInt("UnlockAllLevels") == 0)
        {
            if (PlayerPrefs.GetInt("LevelNum") >= 5)
            {
                int rand = Random.Range(0, 30);
                if (rand <= 15 || (rand % 2 == 0))
                {
                    persuasiveCanvas.SetActive(true);
                    persuasivePanels[1].SetActive(true);
                    StartCoroutine(ShowCloseButton(1));
                }
            }
        }
    }
    public void ShowRemoveAds()
    {
        if (PlayerPrefs.GetInt("UnlockAllGame") == 0 && PlayerPrefs.GetInt("RemoveAds") == 0)
        {
            if (PlayerPrefs.GetInt("LevelNum") >= 5)
            {
                int rand = Random.Range(0, 30);
                if (rand <= 15 || (rand % 2 == 0))
                {
                    persuasiveCanvas.SetActive(true);
                    persuasivePanels[2].SetActive(true);
                    StartCoroutine(ShowCloseButton(2));
                }
            }
        }
    }
    public ColorPicker colorPicker;
    public GameObject customizeYourCar, vehicles;
    public void ShowUnlockCars()
    {
        if (PlayerPrefs.GetInt("UnlockAllGame") == 0 && PlayerPrefs.GetInt("UnlockAllCars") == 0)
        {
            if (PlayerPrefs.GetInt("LevelNum") >= 5)
            {
                persuasiveCanvas.SetActive(true);
                persuasivePanels[3].SetActive(true);
                colorPicker.enabled = false;
                customizeYourCar.SetActive(false);
            }
        }
    }
    public void ClosePanel(int no)
    {
        persuasivePanels[no].SetActive(false);
        persuasiveCanvas.SetActive(false);
        if (no == 3)
        {
            colorPicker.enabled = true;
            customizeYourCar.SetActive(true);
        }
        else if (no == 0)
        {
            vehicles.SetActive(true);
        }
    }

    IEnumerator ShowCloseButton(int no)
    {
        yield return new WaitForSecondsRealtime(1);
        closeButtons[no].SetActive(true);
    }

}
