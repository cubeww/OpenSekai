/****************************************************************************
 *
 * Copyright (c) 2018 CRI Middleware Co., Ltd.
 *
 ****************************************************************************/

using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System;
using System.IO;
#if (UNITY_5 || UNITY_2017_1_OR_NEWER) && (UNITY_IOS || UNITY_TVOS)
using UnityEditor.iOS.Xcode;
#endif

namespace CriWare {

public partial class CriWareBuildPostprocessor : ScriptableObject
{
	private static string prefsFilePath;
	public bool iosAddDependencyFrameworks  = true;
	public bool iosReorderLibraryLinkingsForVp9 = true;

	[MenuItem("GameObject/CRIWARE/Create CriWareBuildPostprocessorPrefs.asset")]
	public static void Create()
	{
		CriWareBuildPostprocessor instance = LoadExistingAsset();
		if (instance) {
			Debug.LogError("[CRIWARE] Preferences file of CriWareBuildPostprocessor already exists.");
			Selection.activeObject = instance;
			return;
		}

		var scobj = ScriptableObject.CreateInstance<CriWareBuildPostprocessor>();
		if (scobj == null) {
			Debug.Log("[CRIWARE] Failed to create CriWareBuildPostprocessor");
			return;
		}

		var script = MonoScript.FromScriptableObject(scobj);
		prefsFilePath = Path.Combine(Path.GetDirectoryName(AssetDatabase.GetAssetPath(script)), "CriWareBuildPostprocessorPrefs.asset");

		AssetDatabase.CreateAsset(scobj, prefsFilePath);

		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();

		Debug.Log("[CRIWARE] Created the preferences file of CriWareBuildPostprocessor. (" + prefsFilePath + ")");

		Selection.activeObject = scobj;
	}

	private static CriWareBuildPostprocessor LoadExistingAsset()
	{
		string postProcessorPath = "";
		string[] searchResult = AssetDatabase.FindAssets("t:CriWareBuildPostprocessor");
		if (searchResult.Length == 0) {
			return null;
		}
		postProcessorPath = AssetDatabase.GUIDToAssetPath(searchResult[0]);
		var instance = (CriWareBuildPostprocessor)AssetDatabase.LoadAssetAtPath(postProcessorPath, typeof(CriWareBuildPostprocessor));

		return instance;
	}

	[PostProcessScene]
	public static void OnPostProcessScene() {
		CheckGraphicsApiProcess ();
	}


	[PostProcessBuild]
	public static void OnPostprocessBuild(BuildTarget build_target, string path)
	{
		CriWareBuildPostprocessor instance = LoadExistingAsset();
		if (instance == null) {
			instance = ScriptableObject.CreateInstance<CriWareBuildPostprocessor>();
			Debug.Log(
				"[CRIWARE] Run CriWareBuildPostprocessor.OnPostprocessBuild with default preferences.\n"
				+ "If you want to change the preferences, please create the preferences file by 'GameObject/CRIWARE/Create CriWareBuildPostprocessorPrefs.asset' menu."
				);
		} else {
			Debug.Log(
				"[CRIWARE] Run CriWareBuildPostprocessor.OnPostprocessBuild with default preferences.\n"
				+ "If you want to change the preferences, please edit the preferences file (" + prefsFilePath + ")"
				);
		}
		PostProcessForPlatform(instance, build_target, path);
	}

	static partial void PostProcessForPlatform(CriWareBuildPostprocessor instance, BuildTarget buildTarget, string path);

	private static string GetAssetPath(string assetName, string substring)
	{
		string[] pathStock = AssetDatabase.FindAssets(assetName);
		if (pathStock == null) {
			return null;
		}
		foreach (string path in pathStock) {
			string strPath = AssetDatabase.GUIDToAssetPath(path);
			if (strPath.Contains(substring)) {
				return strPath;
			}
		}
		return null;
	}

	private static bool CheckGraphicsApiProcess() {
		return true;
	}
}

} //namespace CriWare