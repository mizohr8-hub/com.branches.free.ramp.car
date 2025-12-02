#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

using System;
#if UNITY_EDITOR
using UnityPlayerSettings = UnityEditor.PlayerSettings;
#endif
using System.Collections.Generic;
using MiF_Plugin.Setting;

#if UNITY_EDITOR
public class MizoStudio : EditorWindow {



	string adsInitString="Init_Ads";
	public bool showBtn;
	[MenuItem("Mizo Studio/Project Settings")]
	public static void ShowMizoWindow()
	{
		var window = GetWindow <MizoStudio> ();
		window.Show ();

		#if INIT_MiF
		#endif


	}

	private Texture2D m_Logo = null;


	void OnEnable()
	{
		m_Logo = (Texture2D)Resources.Load("MiF_Title",typeof(Texture2D));
	}

	public void OnGUI()
	{
		DefineGUIStyle ("Third Party FrameWorks");

		var guiTitleStyle = new GUIStyle(GUI.skin.label);
		guiTitleStyle.normal.textColor = Color.white;
		guiTitleStyle.fontSize = 12;
		guiTitleStyle.alignment = TextAnchor.MiddleLeft;

		GUILayout.Label ("Initialize The Module", guiTitleStyle);

		showBtn = EditorGUILayout.Toggle("Initialization", showBtn);
		if (showBtn) {
			
			GUILayout.Label ("Enable Ads iAPs Analytics", guiTitleStyle);
			var initializeButton = GUILayout.Button ("Initialized Third Party Plugins");
			if (initializeButton) {
				//MiF_Settings.directiveHandler (true);	
				EditorUtility.DisplayDialog("Mizo Studio", "Third Party Plugins Initialized", "Done");

			}
			GUILayout.Label ("Disable All", guiTitleStyle);
			var disableButton = GUILayout.Button ("Disable Third Party Plugins");
			if (disableButton) {
				//MiF_Settings.directiveHandler (false);	
				EditorUtility.DisplayDialog("Mizo Studio", "Third Party Plugins Disbaled", "OK");

			}

		} 

	}


	

	public  void DefineGUIStyle(string module) { 

		GUIStyle myStyle = GUI.skin.GetStyle("box");
		myStyle.padding = new RectOffset(15, 10, 8, 8);

		var guiTitleStyle = new GUIStyle(GUI.skin.label);
		guiTitleStyle.normal.textColor = Color.white;
		guiTitleStyle.fontSize = 14;
		guiTitleStyle.alignment = TextAnchor.MiddleCenter;


		var guiMessageStyle = new GUIStyle(GUI.skin.label);
		guiMessageStyle.wordWrap = true;
		guiMessageStyle.fontSize = 15;
		guiMessageStyle.fontStyle = FontStyle.Bold;
		guiMessageStyle.normal.textColor = Color.white;
		guiMessageStyle.alignment = TextAnchor.MiddleCenter;

		var guiMessageStyle1 = new GUIStyle(GUI.skin.label);
		guiMessageStyle1.wordWrap = true;
		guiMessageStyle1.fontSize = 9;
		guiMessageStyle1.normal.textColor = Color.grey;
		guiMessageStyle1.alignment = TextAnchor.MiddleCenter;

		EditorGUILayout.Space();
		EditorGUILayout.BeginVertical("box");
		GUILayout.Box(m_Logo, guiTitleStyle);
		EditorGUILayout.LabelField("Mizo Studio Architecture v" + "1.0", guiMessageStyle);
		EditorGUILayout.LabelField("Module : "+module, guiMessageStyle1);
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.Space();
	}
}
#endif