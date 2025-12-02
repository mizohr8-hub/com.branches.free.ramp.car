using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    public GameObject LoadingScreen;
    public GameObject[] Object;
    public GameObject[] Locks;
    public Button[] characterButton;
    public GameObject selectedButton, selectButton, vidBtn, lockingCondition;
    public Text lockingCondText, watchVidText;

    int tempInt;

    private void OnEnable()
    {

        ObjectClick(PlayerPrefs.GetInt("SelectedBall"));

        PlayerPrefs.SetInt("WheelLockMrJ0",1);

        if (PlayerPrefs.GetInt(10.ToString()) == 1)
        {
            PlayerPrefs.SetInt("WheelLockMrJ1", 1);
        }
        else if (PlayerPrefs.GetInt(20.ToString()) == 1)
        {
            PlayerPrefs.SetInt("WheelLockMrJ3", 1);
        }
        else if (PlayerPrefs.GetInt(30.ToString()) == 1)
        {
            PlayerPrefs.SetInt("WheelLockMrJ5", 1);
        }
        else if (PlayerPrefs.GetInt(40.ToString()) == 1)
        {
            PlayerPrefs.SetInt("WheelLockMrJ6", 1);
        }
        else if (PlayerPrefs.GetInt(50.ToString()) == 1)
        {
            PlayerPrefs.SetInt("WheelLockMrJ8", 1);
        }

        for (int i = 0; i < Locks.Length; i++)
        {
            if (PlayerPrefs.GetInt("WheelLockMrJ"+i)==1)
            {
                Locks[i].SetActive(false);
                characterButton[i].enabled = true;
            }
        }

       
        //for (int i = 0; i < Locks.Length; i++)
        //{
        //    Locks[PlayerPrefs.GetInt("SelectedBall")].SetActive(false);
        //}
    }

    public void LockClick(int Lock)
    {
        if (Lock == 2 || Lock == 4 || Lock == 7)
        {
            for (int i = 0; i < Object.Length; i++)
            {
                Object[i].SetActive(false);
            }
            Object[Lock].SetActive(true);

            vidBtn.SetActive(true);
            selectedButton.SetActive(false);
            selectButton.SetActive(false);
            lockingCondition.SetActive(false);

            if (Lock == 2)
            {
                watchVidText.text = PlayerPrefs.GetInt("CharacterVid2").ToString() + "/3";
            }
            else if (Lock == 4)
            {
                watchVidText.text = PlayerPrefs.GetInt("CharacterVid4").ToString() + "/3";
            }
            else if (Lock == 7)
            {
                watchVidText.text = PlayerPrefs.GetInt("CharacterVid7").ToString() + "/3";
            }

            tempInt = Lock;
        }
        else
        {
            for (int i = 0; i < Object.Length; i++)
            {
                Object[i].SetActive(false);
            }
            Object[Lock].SetActive(true);

            vidBtn.SetActive(false);
            selectedButton.SetActive(false);
            selectButton.SetActive(false);
            lockingCondition.SetActive(true);

            if (Lock == 1)
            {
                lockingCondText.text = "Unlock After 10 Levels";
            }
            else if (Lock == 3)
            {
                lockingCondText.text = "Unlock After 20 Levels";
            }
            else if (Lock == 5)
            {
                lockingCondText.text = "Unlock After 30 Levels";
            }
            else if (Lock == 6)
            {
                lockingCondText.text = "Unlock After 40 Levels";
            }
            else if (Lock == 8)
            {
                lockingCondText.text = "Unlock After 50 Levels";
            }
            tempInt = Lock;
        }
    }
        public void ObjectClick(int Object1)
    {
        for (int i = 0; i < Object.Length; i++)
        {
            Object[i].SetActive(false);
        }
        Object[Object1].SetActive(true);

        vidBtn.SetActive(false);
        
        lockingCondition.SetActive(false);

        if (PlayerPrefs.GetInt("SelectedBall")==Object1)
        {
            selectedButton.SetActive(true);
            selectButton.SetActive(false);
        }
        else if (true)
        {
            selectedButton.SetActive(false);
            selectButton.SetActive(true);
        }

        tempInt = Object1;
    }

    public void SelectThisCharacter()
    {
        PlayerPrefs.SetInt("SelectedBall", tempInt);
        selectedButton.SetActive(true);
        selectButton.SetActive(false);
    }

    IEnumerator Loading(string SceneName)
    {
        LoadingScreen.SetActive(true);
        yield return new WaitForSeconds(8f);
        SceneManager.LoadScene(SceneName);
    }
    public void Home()
    {
        StartCoroutine(Loading("MainMenu1"));
    }
    public void WatchVidToUnlock()
    {
        if (tempInt == 2)
        {
            PlayerPrefs.SetInt("CharacterVid2", PlayerPrefs.GetInt("CharacterVid2")+1);
            if (PlayerPrefs.GetInt("CharacterVid2") == 3)
            {
                PlayerPrefs.SetInt("WheelLockMrJ2", 1);
                Locks[2].SetActive(false);

                vidBtn.SetActive(false);

                lockingCondition.SetActive(false);
                selectedButton.SetActive(false);
                selectButton.SetActive(true);

                characterButton[2].enabled = true;
            }
            else
            {
                watchVidText.text = PlayerPrefs.GetInt("CharacterVid2").ToString() + "/3";
            }
        }
        else if (tempInt == 4)
        {
            PlayerPrefs.SetInt("CharacterVid4", PlayerPrefs.GetInt("CharacterVid4") + 1);
            if (PlayerPrefs.GetInt("CharacterVid4") == 3)
            {
                PlayerPrefs.SetInt("WheelLockMrJ4", 1);
                Locks[4].SetActive(false);
                vidBtn.SetActive(false);

                lockingCondition.SetActive(false);
                selectedButton.SetActive(false);
                selectButton.SetActive(true);

                characterButton[4].enabled = true;

            }
            else
            {
                watchVidText.text = PlayerPrefs.GetInt("CharacterVid4").ToString() + "/3";
            }
        }
        else if (tempInt == 7)
        {
            PlayerPrefs.SetInt("CharacterVid7", PlayerPrefs.GetInt("CharacterVid7") + 1);
            if (PlayerPrefs.GetInt("CharacterVid7") == 3)
            {
                PlayerPrefs.SetInt("WheelLockMrJ7", 1);
                Locks[7].SetActive(false);

                vidBtn.SetActive(false);

                lockingCondition.SetActive(false);
                selectedButton.SetActive(false);
                selectButton.SetActive(true);

                characterButton[7].enabled = true;
            }
            else
            {
                watchVidText.text = PlayerPrefs.GetInt("CharacterVid7").ToString() + "/3";
            }
        }
    }
}
