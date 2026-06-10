/****************************************************************************
 *
 * Copyright (c) 2017 CRI Middleware Co., Ltd.
 *
 ****************************************************************************/

#if UNITY_EDITOR && UNITY_5_6_OR_NEWER

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.Build;
using System.Runtime.CompilerServices;
using UnityEngine.Assertions;

namespace CriWare {

public class CriWareBuildPreprocessor : ScriptableObject {
	public bool muteOtherAudio = true;

	public static CriWareBuildPreprocessor LoadExistingAsset() {
		string preProcessorPath = "";
		string[] searchResult = AssetDatabase.FindAssets("t:CriWareBuildPreprocessor");
		if (searchResult.Length == 0) {
			return null;
		}
		preProcessorPath = AssetDatabase.GUIDToAssetPath(searchResult[0]);
		var instance = (CriWareBuildPreprocessor)AssetDatabase.LoadAssetAtPath(preProcessorPath, typeof(CriWareBuildPreprocessor));
		return instance;
	}
}

public partial class CriWareBuildPreprocessExcecutor :
#if UNITY_2018_1_OR_NEWER
	IPreprocessBuildWithReport
#else
	IPreprocessBuild
#endif
{
	private static string prefsFilePath;

	public int callbackOrder { get { return 0; } }

	[MenuItem("GameObject/CRIWARE/Create CriWareBuildPreprocessorPrefs.asset")]
	public static void Create()
	{
		CriWareBuildPreprocessor instance = CriWareBuildPreprocessor.LoadExistingAsset();
		if (instance) {
			Debug.LogError("[CRIWARE] Preferences file of CriWareBuildPreprocessor already exists.");
			Selection.activeObject = instance;
			return;
		}

		var scobj = ScriptableObject.CreateInstance<CriWareBuildPreprocessor>();
		if (scobj == null) {
			Debug.Log("[CRIWARE] Failed to create CriWareBuildPreprocessor asset");
			return;
		}

		var script = MonoScript.FromScriptableObject(scobj);
		prefsFilePath = Path.Combine(Path.GetDirectoryName(AssetDatabase.GetAssetPath(script)), "CriWareBuildPreprocessorPrefs.asset");

		AssetDatabase.CreateAsset(scobj, prefsFilePath);

		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();

		Debug.Log("[CRIWARE] Created the preferences file of CriWareBuildPreprocessor. (" + prefsFilePath + ")");

		Selection.activeObject = scobj;
	}

#if UNITY_2018_1_OR_NEWER
	public void OnPreprocessBuild(UnityEditor.Build.Reporting.BuildReport report)
	{
		OnPreprocessBuild(report.summary.platform, report.summary.outputPath);
	}
#endif

	public void OnPreprocessBuild(BuildTarget buildTarget, string path)
	{
		CriWareBuildPreprocessor instance = CriWareBuildPreprocessor.LoadExistingAsset();
		if (instance == null) {
			instance = ScriptableObject.CreateInstance<CriWareBuildPreprocessor>();
			Debug.Log(
				"[CRIWARE] Run CriWareBuildPreprocessor.OnPreprocessBuild with default preferences.\n"
				+ "If you want to change the preferences, please create the preferences file by 'GameObject/CRIWARE/Create CriWareBuildPreprocessorPrefs.asset' menu."
			);
		} else {
			Debug.Log(
				"[CRIWARE] CriWareBuildPreprocessor preferences file has been loaded.\n"
				+ "If you want to change the preferences, please edit the preferences file (" + prefsFilePath + ")"
			);
		}

		if (instance.muteOtherAudio == true) {
			ModifyAudioManager(buildTarget, path);
		}

		PreProcessForPlatform(instance, buildTarget, path);
	}

	private static void ModifyAudioManager(BuildTarget build_target, string path)
	{
		string audioManagerPath = "ProjectSettings/AudioManager.asset";
		SerializedObject audioManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath(audioManagerPath)[0]);
		SerializedProperty propertyDisableUnityAudio = audioManager.FindProperty("m_DisableAudio");

		if (build_target == BuildTarget.Android) {
			if (PlayerSettings.muteOtherAudioSources == true) {
				propertyDisableUnityAudio.boolValue = false;
			}
		} else {
			propertyDisableUnityAudio.boolValue = true;
		}

		audioManager.ApplyModifiedProperties();
	}

	internal static List<CriWareVersionWindow.PluginInfo> GetPluginInfoList() {
		CriWareVersionWindow _criWareVersionWindow = ScriptableObject.CreateInstance("CriWareVersionWindow") as CriWareVersionWindow;
		_criWareVersionWindow.Reload(CriWareVersionWindow.PluginType.CRIWARE, true);
		var pluginInfoList = _criWareVersionWindow.PluginInfos;
		return pluginInfoList;
	}

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

	static partial void PreProcessForPlatform(CriWareBuildPreprocessor instance, BuildTarget buildTarget, string path);

}


} //namespace CriWare

#endif

/* end of file */