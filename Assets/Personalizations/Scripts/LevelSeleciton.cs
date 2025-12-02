using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSeleciton : MonoBehaviour {
   // public Text scoreTxt;
    
    public GameObject[] highlighterImages;
    public GameObject[] locked;
    public GameObject[] levels;
    public GameObject[] unlocked;
    

    private void OnEnable()
    {
        //PlayerPrefs.DeleteAll();
        foreach (var item in highlighterImages)
        {
            item.SetActive(false);
        }
        foreach (var item in levels)
        {
            item.GetComponent<Button>().interactable = false;
        }
        if (PlayerPrefs.GetInt("LevelNum") <= 0)
        {
            PlayerPrefs.SetInt("LevelNum", 1);
        }

        for (int i =0; i < PlayerPrefs.GetInt("UnlockLevels"); i++)
        {
            Debug.Log("levelNum :"+i);
            levels[i].GetComponent<Button>().interactable = true;
            //locked[i].SetActive(false);
            //unlocked[i].SetActive(true);
        }
        for (int i =1; i < PlayerPrefs.GetInt("UnlockLevels"); i++)
        {
            Debug.Log("levelNum :"+i);
          
            locked[i-1].SetActive(false);
            unlocked[i-1].SetActive(true);
         
        }

        if (PlayerPrefs.HasKey("UnlockLevels"))
        {
            int a = PlayerPrefs.GetInt("UnlockLevels");
            highlighterImages[a-1].SetActive(true);
        }
        else
        {
            highlighterImages[0].SetActive(true);
        }
    }
    
    public void SelectLevel(int levelID)
    {
        PlaySound();

        PlayerPrefs.SetInt("LevelNum", levelID);
        if (levelID > PlayerPrefs.GetInt("UnlockLevels"))
        {
            PlayerPrefs.SetInt("UnlockLevels", levelID);
        }
        foreach (var item in highlighterImages)
        {
            item.SetActive(false);
        }

        highlighterImages[levelID-1].SetActive(true);
        print("LevelNum= " + levelID);
      
    }

    public void PlaySound()
    {
        RGSK.SoundManager.instance.PlaySound("Button", true);
    }

    public void Next()
    {
        PlaySound();
    }
    public void UnlockAllLevels()
    {
        //unlock all levels via iap //mjr

        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].GetComponent<Button>().interactable = true;
            PlayerPrefs.SetInt("LevelNum", 20);
            locked[i - 1].SetActive(false);
            unlocked[i - 1].SetActive(true);
        }
    }

}
