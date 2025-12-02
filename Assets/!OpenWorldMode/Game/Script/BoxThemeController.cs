using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrickGameManager;

public class BoxThemeController : MonoBehaviour {

	public Texture TBlue, TGreen, TPink, TPurple, TYellow, TBlack;
	public Texture TBlueCap, TGreenCap, TPinkCap, TPurpleCap, TYellowCap, TBlackCap;
	public static BoxThemeController instance;
	// Use this for initialization
	void Start ()
	{
		instance = this;
	}

	public void setboxTheme (int LevelNo,Renderer _Renderer)
	{
		if (LevelNo >= 0 && LevelNo < 10) {
			_Renderer.material.SetTexture ("_MainTex", TBlue);
		} else if (LevelNo >= 10 && LevelNo < 20) {
			_Renderer.material.SetTexture ("_MainTex", TGreen);
		} else if (LevelNo >= 20 && LevelNo < 30) {
			_Renderer.material.SetTexture ("_MainTex", TPink);
		} else if (LevelNo >= 30 && LevelNo < 40) {
			_Renderer.material.SetTexture ("_MainTex", TPurple);
		} else if (LevelNo >= 40 && LevelNo < 50) {
			_Renderer.material.SetTexture ("_MainTex", TYellow);
		} else if (LevelNo >= 50 && LevelNo < 60) {
			_Renderer.material.SetTexture ("_MainTex", TBlack);
		} else {
			_Renderer.material.SetTexture ("_MainTex", TBlack);
		}
	}
	public void setboxCapTheme (int LevelNo,Renderer _Renderer)
	{
		if (LevelNo >= 0 && LevelNo < 10) {
			_Renderer.material.SetTexture ("_MainTex", TBlueCap);
		} else if (LevelNo >= 10 && LevelNo < 20) {
			_Renderer.material.SetTexture ("_MainTex", TGreenCap);
		} else if (LevelNo >= 20 && LevelNo < 30) {
			_Renderer.material.SetTexture ("_MainTex", TPinkCap);
		} else if (LevelNo >= 30 && LevelNo < 40) {
			_Renderer.material.SetTexture ("_MainTex", TPurpleCap);
		} else if (LevelNo >= 40 && LevelNo < 50) {
			_Renderer.material.SetTexture ("_MainTex", TYellowCap);
		} else if (LevelNo >= 50 && LevelNo < 60) {
			_Renderer.material.SetTexture ("_MainTex", TBlackCap);
		} else {
			_Renderer.material.SetTexture ("_MainTex", TBlack);
		}
	}

}
