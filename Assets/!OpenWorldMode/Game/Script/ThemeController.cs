using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeController : MonoBehaviour
{
	public Renderer BGRenderer;
	public Texture TBlue, TGreen, TPink, TPurple, TYellow, TBlack;
	public static ThemeController InstanceTheme;
	// Use this for initialization
	void Start ()
	{
		InstanceTheme = this;
	}

	public void setTheme (int LevelNo)
	{
		print ("LevelNo   " + LevelNo);
		if (LevelNo > 0 && LevelNo <= 10) {
			BGRenderer.material.SetTexture ("_MainTex", TBlue);
		} else if (LevelNo > 10 && LevelNo <= 20) {
			BGRenderer.material.SetTexture ("_MainTex", TGreen);
		} else if (LevelNo > 20 && LevelNo <= 30) {
			BGRenderer.material.SetTexture ("_MainTex", TPink);
		} else if (LevelNo > 30 && LevelNo <= 40) {
			BGRenderer.material.SetTexture ("_MainTex", TPurple);
		} else if (LevelNo > 40 && LevelNo <= 50) {
			BGRenderer.material.SetTexture ("_MainTex", TYellow);
		} else if (LevelNo > 50 && LevelNo <= 60) {
			BGRenderer.material.SetTexture ("_MainTex", TBlack);
		} else {
			BGRenderer.material.SetTexture ("_MainTex", TBlack);
		}
	}

}
