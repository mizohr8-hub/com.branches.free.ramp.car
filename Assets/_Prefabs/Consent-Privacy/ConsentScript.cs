
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConsentScript : MonoBehaviour
{
	public int nextSceneIndex = 1;

	public string PrivacyPolicyLink;
	public string TermConditionsLink;
    [SerializeField]
	private GameObject dialog;
    public GameObject loadingBar, loadingPanel;
    public bool devMode = false;

    void onConsoliAdsInitialization()
    {
        //consola
        //ConsoliAds.Instance.LoadInterstitial(); 
        //ConsoliAds.Instance.LoadRewarded(); 
    }
    private void Awake()
    {
        
    }

    public void Start ()
	{

        //consola
        //ConsoliAds.onConsoliAdsInitializationSuccess += onConsoliAdsInitialization;
        //ConsoliAds.Instance.initialize("8fa4640c1e57b77368da851dcebb09af", devMode, true, Platform.Google);
        GameAnalyticsSDK.GameAnalytics.Initialize();//HaiderGA

        if (PlayerPrefs.GetInt("IsFirst") == 0)
            dialog.SetActive(true);
        else
            No();
        Debug.Log("Player prefs Valuse: " + PlayerPrefs.GetInt("IsFirst"));

    }


	public void openPrivacyPolicy ()
	{
		Application.OpenURL (PrivacyPolicyLink);
	}

	public void openTermPolicy ()
	{
		Application.OpenURL (TermConditionsLink);
	}


	public void Yes ()
	{
        Debug.Log("Yes");
		PlayerPrefs.SetInt ("IsFirst", 1);
        //Application.LoadLevel (nextSceneIndex);

        //StartCoroutine(LoadScene("LiveMenu"));
		dialog.SetActive (false);
        loadingPanel.SetActive(true);
        Invoke("LoadScene", 9f);
    }

	public void No ()
	{
        Debug.Log("No");
		dialog.SetActive (false);
        loadingPanel.SetActive (true);
        Invoke("LoadScene", 9f);
        //Application.LoadLevel (nextSceneIndex);
       
	}

    void LoadScene()
    {
        Application.LoadLevel(nextSceneIndex);
    }


    private AsyncOperation async;
    IEnumerator LoadScene(string levelName)
    {
        
        async = SceneManager.LoadSceneAsync(levelName);

        while (!async.isDone)
        {
            if (loadingBar) loadingBar.GetComponent<Image>().fillAmount = async.progress;

            yield return null;
        }
        //print(async.progress + "Fill Amount"); //mjr
    }
}