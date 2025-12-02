using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TrickGameManager;

public class UIManagerBumper : MonoBehaviour
{
	public Button nextBtn;
	public GameObject compBanner;
	private Animator compBannerAnim;
	public Text quotsText;
	// Use this for initialization
	void Start ()
	{
		nextBtn.interactable = false;
//		compBanner.transform.localPosition = new Vector3 (0, 450, 0);
		compBannerAnim=compBanner.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if ((GameManagerBouncy.instance.CurrentGameState == GameState.CompletedState) && !nextBtn.IsInteractable ()) {
//			compBanner.transform.localPosition = new Vector3 (0, 310, 0);
			compBannerAnim.SetTrigger("OpenTrigger");
			nextBtn.interactable = true;
			BoxScript.isBoxCloseReady = true;
			quotsText.text = RandomQuots.instance.quotsStr ();

		} else if ((GameManagerBouncy.instance.CurrentGameState == GameState.PlayState) && nextBtn.IsInteractable ()) {
			nextBtn.interactable = false;
//			compBanner.transform.localPosition = new Vector3 (0, 450, 0);
			compBannerAnim.SetTrigger("CloseTrigger");
			BoxScript.isBoxOpenReady = true;
		}
	}
}
