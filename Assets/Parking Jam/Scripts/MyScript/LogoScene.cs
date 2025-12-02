using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogoScene : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject LogoScreen, LoadScreen;
    public Image fillBar;

    void Start()
    {
        
        Time.timeScale = 1;
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSecondsRealtime(2f);
        LogoScreen.SetActive(false);
        LoadScreen.SetActive(true);
        while (true)
        {
            fillBar.fillAmount +=0.025f;
            yield return new WaitForSecondsRealtime(0.025f); ;
            if (fillBar.fillAmount >= 1f)
            {
                break;
            }

        }
        SceneManager.LoadSceneAsync("MainMenu");
    }
    // Update is called once per frame
  
}
