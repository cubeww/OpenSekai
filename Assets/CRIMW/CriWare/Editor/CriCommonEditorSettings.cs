/****************************************************************************
 *
 * Copyright (c) 2023 CRI Middleware Co., Ltd.
 *
 ****************************************************************************/


using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace CriWare.Editor
{
	public class CriCommonEditorSettingsProvider : SettingsProvider
	{
		static readonly string settingPath = "Project/CRIWARE/Editor/Common";

		public CriCommonEditorSettingsProvider(string path, SettingsScope scope) : base(path, scope) { }

		public override void OnGUI(string searchContext)
		{
			EditorGUI.indentLevel++;
			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Common Editor Settings for CRIWARE", EditorStyles.boldLabel);
			EditorGUI.indentLevel++;
			EditorGUILayout.Space();
			CriCommonEditorSettings.Instance.EditorInstance.OnInspectorGUI();
			EditorGUI.indentLevel -= 2;
		}

		[SettingsProvider]
		static SettingsProvider Create()
		{
			var provider = new CriCommonEditorSettingsProvider(settingPath, SettingsScope.Project);
			return provider;
		}
	} //class CriCommonEditorSettingsProvider

	public class CriCommonEditorSettings : ScriptableObject
	{
		static readonly string SettingsDirPath = "Assets/CriData/Settings";
		static CriCommonEditorSettings instance = null;

		private UnityEditor.Editor editorInstance = null;
		internal UnityEditor.Editor EditorInstance
		{
			get
			{
				if (editorInstance == null)
				{
					editorInstance = UnityEditor.Editor.CreateEditor(this);
				}
				return editorInstance;
			}
		}

		private bool hasSettingsChanged = false;
		internal void SetChangeFlag() { hasSettingsChanged = true; }
		internal bool GetChangeStatusOnce()
		{
			bool currentChangeStatus = hasSettingsChanged;
			hasSettingsChanged = false;
			return currentChangeStatus;
		}

		[SerializeField]
		private bool tryFsSceneSettings = false;
		internal bool TryFsSceneSettings { get { return tryFsSceneSettings; } }

		[SerializeField]
		private bool enableStaticFieldReload = true;
		internal bool EnableStaticFieldReload { get { return enableStaticFieldReload; } set { enableStaticFieldReload = value; } }

		[SerializeField]
		private CriFsConfig fsConfig = new CriFsConfig();
		internal CriFsConfig FsConfig { get { return fsConfig; } }

		internal static CriCommonEditorSettings Instance
		{
			get
			{
				if (instance == null)
				{
					var guids = AssetDatabase.FindAssets("t:" + typeof(CriCommonEditorSettings).Name);
					if (guids.Length <= 0)
					{
						if (!System.IO.Directory.Exists(SettingsDirPath))
						{
							System.IO.Directory.CreateDirectory(SettingsDirPath);
						}
						instance = CreateInstance<CriCommonEditorSettings>();
						AssetDatabase.CreateAsset(instance, System.IO.Path.Combine(SettingsDirPath, typeof(CriCommonEditorSettings).Name + ".asset"));
						AssetDatabase.Refresh();
					}
					else
					{
						var assetPath = AssetDatabase.GUIDToAssetPath(guids[0]);
						if (guids.Length > 1)
						{
							Debug.LogWarning("[CRIWARE] Multiple setting files founded. Using " + assetPath);
						}
						instance = AssetDatabase.LoadAssetAtPath<CriCommonEditorSettings>(assetPath);
					}
				}
				return instance;
			}
		}

		internal void ResetFsConfig()
		{
			tryFsSceneSettings = false;
			fsConfig = new CriFsConfig();
		}
	} //class CriCommonEditorSettings

	[CustomEditor(typeof(CriCommonEditorSettings))]
	public class CriCommonEditorSettingsEditor : UnityEditor.Editor
	{
		private SerializedProperty enableStaticFieldReloadProp;
		private SerializedProperty tryFsSceneSettingsProp;
		private List<SerializedProperty> fsConfigProps = new List<SerializedProperty>();

		private void OnEnable()
		{
			enableStaticFieldReloadProp = serializedObject.FindProperty("enableStaticFieldReload");

			tryFsSceneSettingsProp = serializedObject.FindProperty("tryFsSceneSettings");
			fsConfigProps.Add(serializedObject.FindProperty("fsConfig.numberOfLoaders"));
			fsConfigProps.Add(serializedObject.FindProperty("fsConfig.numberOfBinders"));
			fsConfigProps.Add(serializedObject.FindProperty("fsConfig.numberOfInstallers"));
			fsConfigProps.Add(serializedObject.FindProperty("fsConfig.installBufferSize"));
			fsConfigProps.Add(serializedObject.FindProperty("fsConfig.maxPath"));
			fsConfigProps.Add(serializedObject.FindProperty("fsConfig.userAgentString"));
			fsConfigProps.Add(serializedObject.FindProperty("fsConfig.minimizeFileDescriptorUsage"));
			fsConfigProps.Add(serializedObject.FindProperty("fsConfig.enableCrcCheck"));
			fsConfigProps.Add(serializedObject.FindProperty("fsConfig.androidDeviceReadBitrate"));
		}

		public override void OnInspectorGUI()
		{
			const float LABEL_WIDTH = 250;
			float prevLabelWidth;

			serializedObject.Update();
			prevLabelWidth = EditorGUIUtility.labelWidth;
			EditorGUIUtility.labelWidth = LABEL_WIDTH;

#if UNITY_2019_3_OR_NEWER
			EditorGUILayout.PropertyField(enableStaticFieldReloadProp);

			if (!enableStaticFieldReloadProp.boolValue && EditorSettings.enterPlayModeOptionsEnabled)
			{
				EditorGUILayout.HelpBox("Enable this option to reload static fields in CRIWARE runtime when reload domain is disabled.", MessageType.Info);
			}

			if (serializedObject.hasModifiedProperties)
			{
				if (enableStaticFieldReloadProp.serializedObject.hasModifiedProperties)
				{
					EditorUtility.RequestScriptReload();
				}
				(target as CriCommonEditorSettings).SetChangeFlag();
			}
#endif

			EditorGUI.indentLevel--;
			EditorGUILayout.Space();
			EditorGUILayout.LabelField("File System Settings for CRIWARE", EditorStyles.boldLabel);
			EditorGUI.indentLevel++;

			EditorGUILayout.PropertyField(tryFsSceneSettingsProp);
			if (tryFsSceneSettingsProp.boolValue)
			{
				EditorGUILayout.HelpBox("Will use project settings if CriLibraryInitializer does not exist in the scene.", MessageType.Info);
			}

#if UNITY_2019_3_OR_NEWER
			foreach (SerializedProperty fsProp in fsConfigProps)
			{
				EditorGUILayout.PropertyField(fsProp);
			}
#endif

			if (serializedObject.hasModifiedProperties)
			{
				(target as CriCommonEditorSettings).SetChangeFlag();
			}
			serializedObject.ApplyModifiedProperties();

			if (GUILayout.Button("Reset File System Settings to Default"))
			{
				(target as CriCommonEditorSettings).ResetFsConfig();
				(target as CriCommonEditorSettings).SetChangeFlag();
			}
		}
	} //class CriCommonEditorSettingsEditor

} //namespace CriWare.Editor


