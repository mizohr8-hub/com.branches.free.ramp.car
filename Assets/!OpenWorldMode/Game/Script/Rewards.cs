using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class Rewards : MonoBehaviour {
	public static Rewards _reward;
	public string adID;
	/* void Awake()
	{
		{
			if (_reward) {
				DestroyImmediate (gameObject);
			} else {
				DontDestroyOnLoad (gameObject);
				_reward = this;
			}
		}
        Advertisement.Initialize (adID, true);
	}

	public void ShowAd(string zone = "")
	{
		#if UNITY_EDITOR
		StartCoroutine(WaitForAd ());
		#endif

		if (string.Equals (zone, ""))
			zone = null;

		ShowOptions options = new ShowOptions ();
		options.resultCallback = AdCallbackhandler;

		if (Advertisement.IsReady (zone))
			Advertisement.Show (zone, options);
	}

	void AdCallbackhandler (ShowResult result)
	{
		switch(result)
		{
            case ShowResult.Finished:
                Debug.Log("Ad Finished. Rewarding player...");         // ADD REWARD
			
                if (PlayerPrefs.GetInt("VideoForPlane") == 1)
                {
                    PlayerPrefs.SetInt("PlanePowerUp", PlayerPrefs.GetInt("PlanePowerUp") + 1);
                    PlayerPrefs.SetInt("VideoForPlane", 0);
                }
                    

			break;
		case ShowResult.Skipped:
			Debug.Log ("Ad skipped. Son, I am dissapointed in you");
			break;
		case ShowResult.Failed:
			Debug.Log("I swear this has never happened to me before");
			break;
		}
	}

	IEnumerator WaitForAd()
	{
		float currentTimeScale = Time.timeScale;
		Time.timeScale = 0f;
		yield return null;

		while (Advertisement.isShowing)
			yield return null;

		Time.timeScale = currentTimeScale;
	} */

}
