using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrickGameManager;

public class BoxScript : MonoBehaviour
{
	public Light signalLight;
	public static bool isBoxCloseReady;
	public static bool isBoxOpenReady;
	public ParticleSystem blastPart;
	public GameObject smileFace, cryingFace, idleFace;
	// Use this for initialization
	void Start ()
	{
		BoxThemeController.instance.setboxTheme (GameManagerBouncy.instance.CurrentLevel, transform.GetChild (1).GetComponent<Renderer> ());
		BoxThemeController.instance.setboxCapTheme (GameManagerBouncy.instance.CurrentLevel, transform.GetChild (2).GetComponent<Renderer> ());
		BoxThemeController.instance.setboxCapTheme (GameManagerBouncy.instance.CurrentLevel, transform.GetChild (3).GetComponent<Renderer> ());

	}
	
	// Update is called once per frame
	void Update ()
	{

		if (GameManagerBouncy.instance.CurrentGameState == GameState.CompletedState && isBoxCloseReady) {
			isBoxCloseReady = false;
			GetComponent<Animator> ().SetTrigger ("CloseTrigger");
			signalLight.color = Color.green;
			blastPart.Play ();
			smileFace.SetActive (true);
			cryingFace.SetActive (false);
			idleFace.SetActive (false);
		}
		if (GameManagerBouncy.instance.CurrentGameState == GameState.PlayState && isBoxOpenReady) {
			isBoxOpenReady = false;
			GetComponent<Animator> ().SetTrigger ("OpenTrigger");
			signalLight.color = Color.red;
			smileFace.SetActive (false);
			cryingFace.SetActive (false);
			idleFace.SetActive (true);
		}
	}
}
