using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;
#if UNITY_EDITOR
using UnityPlayerSettings = UnityEditor.PlayerSettings;
#endif




using System;

namespace MiF_Plugin.Setting
{
#if UNITY_EDITOR
    public class MiF_Settings : MonoBehaviour {
	public		const	string		kMacroInitAds					= "INIT_MiF";

//        public static readonly BuildTargetGroup[]	buildTargetGroups		= new BuildTargetGroup[]{
//		BuildTargetGroup.Android,
//#if UNITY_5 || UNITY_6 || UNITY_7
//		BuildTargetGroup.iOS,
//#else
//		BuildTargetGroup.iOS, 
//#endif
//#if UNITY_5 || UNITY_6 || UNITY_7
//		BuildTargetGroup.WSA, 
//#else
//		BuildTargetGroup.Metro,
//		BuildTargetGroup.WP8,
//#endif
//#if !(UNITY_5_0 || UNITY_5_1 || UNITY_5_2 || UNITY_5_3_0) && (UNITY_5 || UNITY_6 || UNITY_7)
//		BuildTargetGroup.tvOS,
//#endif
//		BuildTargetGroup.Standalone

//	};

//        private static 	readonly char[] 	defineSeperators 				= new char[] {
//		';',
//		',',
//		' '
//	};

//	public static void directiveHandler(bool val)
//	{

//		foreach (BuildTargetGroup _curBuildTargetGroup in buildTargetGroups) {
//			string[] _curDefineSymbols	= UnityPlayerSettings.GetScriptingDefineSymbolsForGroup (_curBuildTargetGroup).Split (defineSeperators, StringSplitOptions.RemoveEmptyEntries);
//			List<string>	_newDefineSymbols	= new List<string> (_curDefineSymbols);


//			AddOrRemoveFeatureDefineSymbol (_newDefineSymbols, val, kMacroInitAds);


//			// Now save these changes
//			UnityPlayerSettings.SetScriptingDefineSymbolsForGroup (_curBuildTargetGroup, string.Join (";", _newDefineSymbols.ToArray ()));
//		}
//	}


//	private static void AddOrRemoveFeatureDefineSymbol (List<string> _defineSymbols, bool _usesFeature, string _featureSymbol)
//	{
//		if (_usesFeature)
//		{
//			if (!_defineSymbols.Contains(_featureSymbol))
//				_defineSymbols.Add(_featureSymbol);
//		}
//		else
//		{

//			if (_defineSymbols.Contains(_featureSymbol))
//				_defineSymbols.Remove(_featureSymbol);
//		}
//	}
}
#endif
};
