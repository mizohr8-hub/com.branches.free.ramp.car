using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ModeSelection : MonoBehaviour {

    public GameObject[] highlighterImages;
    public GameObject modeSelectionPanel, LevelSelectionPanel, difficultyPanel;

    private void OnEnable()
    {
       
         foreach (var item in highlighterImages)
         {
            item.SetActive(false);
         }

       // highlighterImages[highlighterImages.Length-1].SetActive(false);
    }

    public void SelectMode(int modeID)
    {
        foreach (var item in highlighterImages)
        {
            item.SetActive(false);
        }
        highlighterImages[modeID - 1].SetActive(true);
        PlayerPrefs.SetInt("Mode", modeID);
    }
    public void NextMode()
    {
        int a = PlayerPrefs.GetInt("Mode");
        switch (a)
        {
            case 1:
                LevelSelectionPanel.SetActive(true);
                PlayerPrefs.SetInt("SelectedMode", 1);
                modeSelectionPanel.SetActive(false);
                break;
            case 2:
                PlayerPrefs.SetInt("SelectedMode",2);
                // SceneManager.LoadScene("MultiplayerMode");
                difficultyPanel.SetActive(true);
                modeSelectionPanel.SetActive(false);
                break;
            default:
                break;
        }
    }
}
