using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RGSK;

public class DifficultyLevel : MonoBehaviour {
    private int currency;
    public GameObject[] difficultyModes;
    public GameObject[] highlighterImages;
    public GameObject playBtn, noCashPanel;


    private void OnEnable()
    {
        PlayerPrefs.SetInt("Currency", PlayerPrefs.GetInt("Currency") + 100000);

        currency = PlayerPrefs.GetInt("Currency");
        foreach (var item in highlighterImages)
        {
            item.SetActive(false);
        }
    }
    private void Update()
    {
        MenuManager.instance.playerCurrency.text = currency.ToString();
    }

    void ButtonSFX()
    {
        if (SoundManager.instance) SoundManager.instance.PlaySound("Button", true);
    }
    public void EasyMode()
    {
        if (currency >= 1000)
        {
            currency -= 1000;
            PlayerPrefs.SetInt("Currency", currency);
            PlayerPrefs.SetInt("Difficulty", 1);
            foreach (var item in highlighterImages)
            {
                item.SetActive(false);
            }
            highlighterImages[0].SetActive(true);
            ButtonSFX();
            playBtn.SetActive(true);
        }
        else
        {
            noCashPanel.SetActive(true);
        }
    }
    public void MediumMode()
    {
        if (currency >= 2500)
        {
            currency -= 2500;
            PlayerPrefs.SetInt("Currency", currency);
            PlayerPrefs.SetInt("Difficulty", 2);
            foreach (var item in highlighterImages)
            {
                item.SetActive(false);
            }
            highlighterImages[1].SetActive(true);
            ButtonSFX();
            playBtn.SetActive(true);
        }
        else
        {
            noCashPanel.SetActive(true);
        }
    }
    public void HardMode()
    {
        if (currency >= 4000)
        {
            currency -= 4000;
            PlayerPrefs.SetInt("Currency", currency);
            PlayerPrefs.SetInt("Difficulty", 3);
            foreach (var item in highlighterImages)
            {
                item.SetActive(false);
            }
            highlighterImages[2].SetActive(true);
            ButtonSFX();
            playBtn.SetActive(true);
        }
        else
        {
            noCashPanel.SetActive(true);
        }
    }

}
