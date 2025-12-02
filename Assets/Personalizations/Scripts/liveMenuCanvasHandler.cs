using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class liveMenuCanvasHandler : MonoBehaviour {

    private AsyncOperation async;

    public Image loadingBar;
    public Text loadingText;
    public GameObject bar;


    private void Awake()
    {
        Screen.orientation = ScreenOrientation.Landscape;
    }
    public void Play()
    {
        PlayerPrefs.SetInt("FirstTimeLoadings", 1);
        StartCoroutine(LoadGame());
    }
	
    IEnumerator LoadGame()
    {
        bar.GetComponent<Animator>().enabled = true;
        loadingText.text = "Initializing assets...";
        yield return new WaitForSeconds(2f);
        loadingText.text = "Preparing car...";
        yield return new WaitForSeconds(1f);
        loadingText.text = "Loading assets...";
        yield return new WaitForSeconds(1f);
        loadingText.text = "Loading...";
        yield return new WaitForSeconds(4f);
        StartCoroutine(LoadScene("MainMenu"));
    }
    public void Exit()
    {
        Application.Quit();
    }

    IEnumerator LoadScene(string levelName)
    {
        async = SceneManager.LoadSceneAsync(levelName);
        while (!async.isDone)
        {
            if (loadingBar)
            {
                //Debug.Log("the progress " + async.progress);
                loadingBar.fillAmount = async.progress;
            }
           
            yield return null;
        }
       
    }
}
