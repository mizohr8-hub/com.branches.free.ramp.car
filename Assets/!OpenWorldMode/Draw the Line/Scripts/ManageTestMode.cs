using DrawDotGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManageTestMode : MonoBehaviour
{
    //back drops
    //public List<GameObject> obstacles;
    public List<Sprite> backDropSprites;
    public GameObject backDropObject;
    public static int backDropNumber;
    //level info
    public static int levelNumber = 1;
    public static int levelCount;
    public List<GameObject> levels;
    public Sprite locked, unlocked, selected;
    //Car Objects
    public GameObject carPrefab;
    public GameObject carPlaceHolder;

    //Level Text
    public GameObject LevelSelection;
    public Text levelText;
    //Instance 
    public static ManageTestMode Instance;

    public SoundManager soundManager;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

    }
    public void Start()
    {
       
        Debug.Log("baka, value for FromLevelSelection is" + PlayerPrefs.GetInt("FromLevelSelection"));

        int num = PlayerPrefs.GetInt("BackDrop");
        if (levelNumber % 5 == 0 || num % 5 == 0)
        {
            if (backDropNumber >= backDropSprites.Count - 1)
            {
                backDropNumber = 0;
                PlayerPrefs.SetInt("BackDrop", 0);
            }
            else
            {
                backDropNumber++;
                PlayerPrefs.SetInt("BackDrop", backDropNumber);

            }
            backDropObject.GetComponent<SpriteRenderer>().sprite = backDropSprites[backDropNumber];

        }
        else
        {
            backDropNumber = PlayerPrefs.GetInt("BackDrop");
            backDropObject.GetComponent<SpriteRenderer>().sprite = backDropSprites[backDropNumber];
        }




        Debug.Log("baka, level number  " + levelNumber);
        if (PlayerPrefs.GetInt("FromLevelSelection") == 1)
        {
            int lvlfromSelection = PlayerPrefs.GetInt("LevelFromSelection");
            Debug.Log("Baka,value of level from selection is " + lvlfromSelection);
            if (lvlfromSelection > 0)
            {
                levelText.text = "Level " + lvlfromSelection.ToString();
                LevelSelection.SetActive(false);
                Instantiate(Resources.Load("Level" + lvlfromSelection) as GameObject);
                Instantiate(carPrefab, carPlaceHolder.transform.position, Quaternion.identity);
            }
            //else if (lvlfromSelection == 1)
            //{
            //    levelText.text = "Level " + lvlfromSelection.ToString();
            //    LevelSelection.SetActive(false);
            //    Instantiate(Resources.Load("Level" + lvlfromSelection) as GameObject);
            //    Instantiate(carPrefab, carPlaceHolder.transform.position, Quaternion.identity);
            //}
            else
            {
               
              //SoundManager.Instance.PlaySound(SoundManager.Instance.background, true);
                LevelSelection.SetActive(true);
                //LevelSelection.SetActive(false);
                //Instantiate(Resources.Load("Level" + levelNumber) as GameObject);
                //Instantiate(carPrefab, carPlaceHolder.transform.position, Quaternion.identity);
            }
        }
        else if (PlayerPrefs.GetInt("FromLevelSelection") == 0)
        {
           //SoundManager.Instance.PlaySound(SoundManager.Instance.background, true);
          
            LevelSelection.SetActive(true);

        }
        else
        {
            levelText.text = "Level " + levelNumber.ToString();

            LevelSelection.SetActive(false);
            Instantiate(Resources.Load("Level" + levelNumber) as GameObject);
            Instantiate(carPrefab, carPlaceHolder.transform.position, Quaternion.identity);
        }


        for (int i = 0; i < levels.Count; i++)
        {
            if (i <= PlayerPrefs.GetInt("LevelFromSelection"))
            {
                Image component = levels[i].GetComponent<Image>();
                component.sprite = unlocked;
            }
            else
            {
                Image component = levels[i].GetComponent<Image>();
                component.sprite = locked;
            }

            if (i == PlayerPrefs.GetInt("LevelFromSelection"))
            {
                Image component = levels[i].GetComponent<Image>();
                component.sprite = selected;
            }
        }
      SoundManager.Instance.PlaySound(SoundManager.Instance.background, true);
    }

    public void Home()
    {
        
        SceneManager.LoadScene("MainMenu");
        SoundManager.Instance.StopMusic();


    }
    public void OnWin()
    {

    }

    public void OnRestart()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.click, true);
        levelNumber--;
    }
    public void LoadNextLevel()
    {
        int lvl = PlayerPrefs.GetInt("LevelFromSelection");
        SoundManager.Instance.PlaySound(SoundManager.Instance.click, true);
        //Destroy(obstacles[(levelNumber - 1) % obstacles.Count]);
        if (levelNumber >= 50)
        {
            Debug.Log("in if");
            PlayerPrefs.SetInt("FromLevelSelection", 1);
            levelNumber = 1;
        }


        if (lvl >= 50)
        {
            Debug.Log("in set level selection");

            PlayerPrefs.SetInt("FromLevelSelection", 1);
        }
        else
        {
            Debug.Log("in else");
            levelNumber = PlayerPrefs.GetInt("LevelFromSelection");
            levelNumber++;
            PlayerPrefs.SetInt("LevelFromSelection", levelNumber);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LevelCounter()
    {
        // Specify the folder name (without "Resources/")
        string folderName = "Levels";

        // Load all objects in the specified folder
        Object[] levelObjects = Resources.LoadAll(folderName, typeof(GameObject));

        // Count the number of objects
        levelCount = levelObjects.Length;
        Debug.Log("Total number of Level objects: " + levelCount);
    }


    public void PlayLevelNumber(int lvlNumber)
    {
        //SoundManager.Instance.SetSoundOn(false);
        //Invoke(nameof(ToggleBgmMusic), 1f);
        SoundManager.Instance.PlaySound(SoundManager.Instance.click, true);
        Debug.Log("baka, level number here is " + lvlNumber);
        PlayerPrefs.SetInt("LevelFromSelection", lvlNumber);
        LevelSelection.SetActive(false);
        PlayerPrefs.SetInt("FromLevelSelection", 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
        
    }
    
    void ToggleBgmMusic()
    {
       SoundManager.Instance.SetSoundOn(true);
      SoundManager.Instance.PlaySound(SoundManager.Instance.click, true);
    }
    public void GoHome()
    {
        Time.timeScale = 1.0f;
        
        //SoundManager.Instance.PlaySound(SoundManager.Instance.click, true);
        PlayerPrefs.SetInt("FromLevelSelection", 0);
        //PlayerPrefs.SetInt("LevelFromSelection", 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    //private void OnDisable()
    //{
    //    PlayerPrefs.SetInt("FromLevelSelection", 1);
    //    PlayerPrefs.SetInt("LevelFromSelection", 0);
    //}
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("FromLevelSelection", 1);
        //PlayerPrefs.SetInt("LevelFromSelection", 0);
        // SoundManager.Instance.PlaySound(SoundManager.Instance.background, false);
    }

    
}
