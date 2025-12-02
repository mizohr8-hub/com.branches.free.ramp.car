using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrickGameManager;

public class BallThemeController : MonoBehaviour
{
	private Renderer _Renderer;
	public Texture TBlue, TGreen, TPink, TPurple, TYellow, TBlack;
	//	public static ThemeController InstanceTheme;
	// Use this for initialization
	void Start ()
	{
		_Renderer = GetComponent<Renderer> ();
		setTheme (GameManagerBouncy.instance.CurrentLevel + 1);
//		InstanceTheme = this;
	}

	public void setTheme (int LevelNo)
	{
		if (LevelNo > 0 && LevelNo <= 10) {
			_Renderer.material.SetTexture ("_MainTex", TBlue);
		} else if (LevelNo > 10 && LevelNo <= 20) {
			_Renderer.material.SetTexture ("_MainTex", TGreen);
		} else if (LevelNo > 20 && LevelNo <= 30) {
			_Renderer.material.SetTexture ("_MainTex", TPink);
		} else if (LevelNo > 30 && LevelNo <= 40) {
			_Renderer.material.SetTexture ("_MainTex", TPurple);
		} else if (LevelNo > 40 && LevelNo <= 50) {
			_Renderer.material.SetTexture ("_MainTex", TYellow);
		} else if (LevelNo > 50 && LevelNo <= 60) {
			_Renderer.material.SetTexture ("_MainTex", TBlack);
		} else {
			_Renderer.material.SetTexture ("_MainTex", TBlack);
		}
	}

}
