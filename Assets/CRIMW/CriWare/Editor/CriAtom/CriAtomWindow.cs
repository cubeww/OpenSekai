/****************************************************************************
 *
 * Copyright (c) 2011 CRI Middleware Co., Ltd.
 *
 ****************************************************************************/

#if UNITY_2017 && UNITY_EDITOR_WIN
#define OPENFOLDERPANEL_IS_BROKEN
#endif

using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CriWare {

public sealed class CriAtomWindow : EditorWindow
{
	#region Variables
	private int selectedAcfId = 0;
	private int selectedDspBusSettingId = 0;
	private int selectedCueSheetId = 0;
	private int selectedCueInfoIndex = 0;
	private string lastLoadedAcbGuid = null;
	private string lastLoadedAcfGuid = null;
	private bool isCueSheetListInitiated = false;
	private int lastPreviewCueSheetId = -1;
	private Vector2 scrollPosCueSheetList;
	private Vector2 scrollPosCueList;
	private Vector2 scrollPosWindow;
	private bool useCopyAssetsFromCriAtomCraft = false;
	private string[] popupAcfList = null;
	private Color currentGuiColor;
	private float cuesheetListWidth = 200;
	private float cueIdListWidth = 70;
	private bool isResizingLeftSplitter = false;
	private bool isResizingRightSplitter = false;
	private Vector2 lastMousePosLeftSplitter = new Vector2();
	private Vector2 lastMousePosRightSplitter = new Vector2();
	private string strSearchCueInSheet = "";
	private string strSearchCue = "";

	private GameObject targetObject = null;
	private CriAtomSource targetAtomSource = null;
	private CriWare.Editor.CriAtomEditorUtilities.PreviewPlayer previewPlayer = null;
	private CriAtomExAcb previewAcb = null;

	
	[SerializeField] private CriAtomWindowInfo projInfo = new CriAtomWindowInfo();
	[SerializeField] private CriAtomWindowPrefs criAtomWindowPrefs = null;
	[SerializeField] private GUIStyle toolBarButtonStyle = null;
	#endregion

	#region Functions

	[MenuItem("Window/CRIWARE/Atom Browser", false, 100)]
	static void OpenWindow()
	{
		EditorWindow.GetWindow<CriAtomWindow>(false, "Atom Browser");
	}

	private void OnEnable() {
		criAtomWindowPrefs = CriAtomWindowPrefs.Load();


		if (Selection.gameObjects.Length > 0) {
			targetObject = Selection.gameObjects[0];
			targetAtomSource = targetObject.GetComponent<CriAtomSource>();
		}

		ReloadAcbInfo();
	}

	private void OnDisable() {
		if (previewAcb != null) {
			previewAcb.Dispose();
			previewAcb = null;
		}
		lastLoadedAcbGuid = null;
		if (previewPlayer != null) {
			previewPlayer.Dispose();
			previewPlayer = null;
		}
	}

	private void OnSelectionChange() {
		if (Selection.gameObjects.Length > 0) {
			targetObject = Selection.gameObjects[0];
			targetAtomSource = targetObject.GetComponent<CriAtomSource>();
		}

		Repaint();
	}

	private void OnLostFocus() {
		StopPreview();
		if (previewAcb != null) {
			previewAcb.Dispose();
			previewAcb = null;
		}
		lastLoadedAcbGuid = null;
	}

	private void OnGUI() {
		if (toolBarButtonStyle == null) {
			toolBarButtonStyle = new GUIStyle(EditorStyles.toolbarButton);
			toolBarButtonStyle.alignment = TextAnchor.MiddleLeft;
		}

		currentGuiColor = GUI.color;
		this.scrollPosWindow = GUILayout.BeginScrollView(this.scrollPosWindow);
		{
			GUISearchAndReload();
			GUIACFSettings();
			GUICueList();
			EditorGUILayout.Space();
			GUIImportAssetsFromAtomCraft();
			EditorGUILayout.Space();
		}
		GUILayout.EndScrollView();
		GUI.color = currentGuiColor;
	}

	private void ReloadAcbInfo() {
		var loadPath = criAtomWindowPrefs.searchPath;

		if (string.IsNullOrEmpty(loadPath) || Directory.Exists(loadPath) == false) {
			Debug.LogWarning("[CRIWARE] Search path is not valid.");
			return;
		}

		if (loadPath.Contains(Application.dataPath) == false) {
			Debug.LogWarning("[CRIWARE] Search path is not in the current project.");
		}

		TryInitializePlugin();
		projInfo.GetAcfInfoList(true, loadPath);
		projInfo.GetAcbInfoList(true, loadPath);
		selectedAcfId = 0;
		selectedDspBusSettingId = 0;
		selectedCueSheetId = 0;
		selectedCueInfoIndex = 0;
		ResetDataRecord();
		GUI.FocusControl(null);
	}

	private void ResetDataRecord() {
		lastLoadedAcfGuid = null;
		lastLoadedAcbGuid = null;
	}

	private void PlayPreview(CriAtomExAcb acb, string cuename) {
		if (previewPlayer == null) {
			previewPlayer = new CriWare.Editor.CriAtomEditorUtilities.PreviewPlayer();
		}
		previewPlayer.Play(acb, cuename);
	}

	private void StopPreview(bool withoutRelease = true) {
		if (previewPlayer != null) {
			previewPlayer.Stop(withoutRelease);
		}
	}

	private void TryInitializePlugin() {
		if (CriAtomPlugin.IsLibraryInitialized() == false) {
			previewAcb = null;
		}
		if (CriWare.Editor.CriAtomEditorUtilities.InitializeLibrary()) {
			ResetDataRecord();
		}
	}

	private void GUISearchAndReload()
	{
		EditorGUILayout.BeginHorizontal();
		{
			EditorGUI.BeginChangeCheck();
			criAtomWindowPrefs.searchPath = EditorGUILayout.TextField("Path to Search", criAtomWindowPrefs.searchPath);
#if !OPENFOLDERPANEL_IS_BROKEN
			if (GUILayout.Button("...", EditorStyles.miniButton, GUILayout.Width(50)))
			{
				string pathTemp = EditorUtility.OpenFolderPanel("Select folder to search", criAtomWindowPrefs.searchPath, "");
				if (string.IsNullOrEmpty(pathTemp) == false) {
					if (pathTemp.Contains(Application.dataPath)) {
						criAtomWindowPrefs.searchPath = pathTemp;
						ReloadAcbInfo();
					} else {
						Debug.LogWarning("[CRIWARE] Selected folder should be inside this project. Search path will not be changed.");
					}
				}
			}
#endif
			if (GUILayout.Button("Reload Info", EditorStyles.miniButton, GUILayout.Width(100))) {
				ReloadAcbInfo();
			}
			if (GUILayout.Button("Reset Path", EditorStyles.miniButton, GUILayout.Width(100))) {
				criAtomWindowPrefs.searchPath = Application.streamingAssetsPath;
				ReloadAcbInfo();
			}
			if (EditorGUI.EndChangeCheck()) {
				criAtomWindowPrefs.Save();
			}
		}
		EditorGUILayout.EndHorizontal();
	}

	private void GUIACFSettings()
	{
		var acfInfo = projInfo.GetAcfInfoList(false, criAtomWindowPrefs.searchPath);
		popupAcfList = new string[acfInfo.Count];
		for (int i = 0; i < acfInfo.Count; i++) {
			popupAcfList[i] = acfInfo[i].filePath;
		}
		if (acfInfo.Count > 0) {
			selectedAcfId = EditorGUILayout.Popup("ACF Files", selectedAcfId, popupAcfList);
			selectedDspBusSettingId = EditorGUILayout.Popup("DSPBusSettings", selectedDspBusSettingId, acfInfo[selectedAcfId].dspBusSettingsList.ToArray());
		} else {
			EditorGUILayout.LabelField("ACF Files", "No ACF has been found");
			EditorGUILayout.LabelField("DSPBusSettings", "-");
		}
	}

	private void GUICueList()
	{
		const int cCueListItemHeight = 18;
		var acfInfoList = projInfo.GetAcfInfoList(false, criAtomWindowPrefs.searchPath);
		var acbInfoList = projInfo.GetAcbInfoList(false, criAtomWindowPrefs.searchPath, this.strSearchCueInSheet);
		bool isAcfInfoAvailable = (selectedAcfId < acfInfoList.Count) && (selectedDspBusSettingId < acfInfoList[selectedAcfId].dspBusSettingsList.Count);
		bool isCueSheetAvailable = false;
		Rect splitterRect;

		if (isCueSheetListInitiated == true) {
			lastPreviewCueSheetId = this.selectedCueSheetId;
		} else {
			isCueSheetListInitiated = true;
		}

		if (this.selectedCueSheetId >= acbInfoList.Count) { this.selectedCueSheetId = 0; }

		EditorGUILayout.BeginHorizontal();

		/* cuesheet list */
		using (var cuesheetListScope = new EditorGUILayout.VerticalScope("CN Box", GUILayout.Width(cuesheetListWidth))) {
			using (var horizontalScope = new EditorGUILayout.HorizontalScope()) {
				if (GUILayout.Button("Cue Sheet", toolBarButtonStyle)) {
					projInfo.SortCueSheet();
					this.selectedCueSheetId = 0;
				}
			}
			using (var cueListTitleScope = new EditorGUILayout.HorizontalScope()) {
				EditorGUI.BeginChangeCheck();
				GUILayout.Label("Contains", GUILayout.Width(55));
				this.strSearchCueInSheet = EditorGUILayout.TextField(this.strSearchCueInSheet);
				if (EditorGUI.EndChangeCheck()) {
					this.strSearchCue = this.strSearchCueInSheet;
				}
			}
			using (var cuesheetScrollViewScope = new EditorGUILayout.ScrollViewScope(scrollPosCueSheetList)) {
				scrollPosCueSheetList = cuesheetScrollViewScope.scrollPosition;
				int listLengthInView = (int)position.height / cCueListItemHeight;
				int idFirstItemInView = Mathf.Clamp((int)scrollPosCueSheetList.y / cCueListItemHeight, 0, Mathf.Max(0, acbInfoList.Count - listLengthInView));

				GUILayout.Space(idFirstItemInView * cCueListItemHeight);
				for (int i = idFirstItemInView; i < Mathf.Min(idFirstItemInView + listLengthInView, acbInfoList.Count); i++) {
					GUI.color = (this.lastPreviewCueSheetId == i) ? Color.yellow : currentGuiColor;
					EditorGUILayout.BeginHorizontal();
					if (GUILayout.Button(acbInfoList[i].name, EditorStyles.label)) {
						this.selectedCueSheetId = i;
					}
					EditorGUILayout.EndHorizontal();
				}
				GUILayout.Space(Mathf.Max(0, acbInfoList.Count - idFirstItemInView - listLengthInView) * cCueListItemHeight);
				GUI.color = currentGuiColor;
			}
		}
		
		splitterRect = EditorGUILayout.GetControlRect(false, 2, "CN Box", GUILayout.Width(2), GUILayout.ExpandHeight(true));
		cuesheetListWidth += GUILayoutResizeHandler(splitterRect, true, ref lastMousePosLeftSplitter, ref isResizingLeftSplitter).x;

		if (this.selectedCueSheetId != lastPreviewCueSheetId) {
			this.selectedCueInfoIndex = 0;
		}

		isCueSheetAvailable = (acbInfoList.Count > this.selectedCueSheetId);

		using (var cueListAndInfoScope = new EditorGUILayout.VerticalScope("CN Box")) {
			bool playButtonPushed = false;
			bool isCueAvailable = false;
			CriAtomWindowInfo.CueInfo selectedCueInfo = null;

			/* list title */
			using (var cueListTitleScope = new EditorGUILayout.HorizontalScope()) {
				if (GUILayout.Button("Cue Name", toolBarButtonStyle)) {
					if (isCueSheetAvailable) {
						acbInfoList[selectedCueSheetId].SortCueInfo(CriAtomWindowInfo.CueSortType.Name);
						this.selectedCueInfoIndex = 0;
					}
				}
				splitterRect = EditorGUILayout.GetControlRect(false, 20, toolBarButtonStyle, GUILayout.Width(2));
				cueIdListWidth -= GUILayoutResizeHandler(splitterRect, true, ref lastMousePosRightSplitter, ref isResizingRightSplitter).x;
				if (GUILayout.Button("Cue ID", toolBarButtonStyle, GUILayout.Width(cueIdListWidth + 20))) {
					if (isCueSheetAvailable) {
						acbInfoList[selectedCueSheetId].SortCueInfo(CriAtomWindowInfo.CueSortType.Id);
						this.selectedCueInfoIndex = 0;
					}
				}
			}

			using (var cueListTitleScope = new EditorGUILayout.HorizontalScope()) {
				GUILayout.Label("Filter", GUILayout.Width(32));
				this.strSearchCue = EditorGUILayout.TextField(this.strSearchCue);
			}

			/* cue list */
			using (var cueListScope = new EditorGUILayout.ScrollViewScope(scrollPosCueList, GUILayout.ExpandHeight(true))) {
				scrollPosCueList = cueListScope.scrollPosition;
				if (isCueSheetAvailable) {
					var acbInfo = acbInfoList[this.selectedCueSheetId];
					var currentList = acbInfo.GetFilteredCueInfoList(this.strSearchCue, criAtomWindowPrefs.showPrivateCue);

					if (this.selectedCueInfoIndex >= currentList.Count) { this.selectedCueInfoIndex = 0; }

					if (currentList.Count <= 0) {
						EditorGUILayout.HelpBox("Nothing to be shown here.\nTry push the \"Reload Info\" button to refresh the list\nor change the keyword for the filter.", MessageType.Info);
					} else {
						int listLengthInView = (int)position.height / cCueListItemHeight;
						int idFirstCueInView = Mathf.Clamp((int)scrollPosCueList.y / cCueListItemHeight, 0, Mathf.Max(0, currentList.Count - listLengthInView));

						GUILayout.Space(idFirstCueInView * cCueListItemHeight);
						for (int i = idFirstCueInView; i < Mathf.Min(idFirstCueInView + listLengthInView, currentList.Count); ++i) {
							GUI.color = (this.selectedCueInfoIndex == i) ? Color.yellow : currentGuiColor;
							using (var cueItemScope = new EditorGUILayout.HorizontalScope(GUILayout.Height(cCueListItemHeight))) {
								if (GUILayout.Button(EditorGUIUtility.IconContent("Animation.Play"), EditorStyles.miniButton, GUILayout.Width(30))) {
									this.selectedCueInfoIndex = i;
									playButtonPushed = true;
								}
								if (GUILayout.Button(currentList[i].name + " (" + (currentList[i].length < 0 ? "loop" : Ms2String(currentList[i].length)) + ")", EditorStyles.label)) {
									if (selectedCueInfoIndex != i) {
										StopPreview();
									}
									this.selectedCueInfoIndex = i;
								}
								GUILayout.Label(currentList[i].id.ToString(), GUILayout.Width(cueIdListWidth));
							}
						}
						GUILayout.Space(Mathf.Max(0, currentList.Count - idFirstCueInView - listLengthInView) * cCueListItemHeight);
						GUI.color = currentGuiColor;
					}

					isCueAvailable = currentList.Count > this.selectedCueInfoIndex;
					if (isCueAvailable) {
						selectedCueInfo = currentList[selectedCueInfoIndex];
					}
				} else {
					EditorGUILayout.HelpBox("No cue sheet is found.\nPlease check the search path. Press \"Reload Info\" button to refresh the list.", MessageType.Info);
				}
			}

			EditorGUILayout.BeginHorizontal(EditorStyles.toolbar,GUILayout.ExpandWidth(true));
			{
				{
					var tempToggleVal = GUILayout.Toggle(criAtomWindowPrefs.showPrivateCue, "Show Private Cue", GUILayout.Width(120));
					if (tempToggleVal != criAtomWindowPrefs.showPrivateCue) {
						/* Clear cue selection when change setting */
						this.selectedCueInfoIndex = 0;
						criAtomWindowPrefs.Save();
					}
					criAtomWindowPrefs.showPrivateCue = tempToggleVal;
				}
				if (GUILayout.Button("Stop All", EditorStyles.toolbarButton, GUILayout.Width(100))) {
					StopPreview();
				}
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.Space();

			EditorGUILayout.BeginHorizontal("AppToolbar");
			{
				GUILayout.Label("Cue Information");
			}
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.LabelField("Cue ID", (isCueSheetAvailable && isCueAvailable) ? selectedCueInfo.id.ToString() : "N/A");
			string cueName = "N/A";
			string userData = "";
			if (isCueSheetAvailable && isCueAvailable) {
				cueName = selectedCueInfo.name;
				userData = selectedCueInfo.comment;
			}
			GUILayout.BeginHorizontal();
			GUILayout.Label("Cue Name", GUILayout.Width(145));
			GUILayout.Label(cueName, GUILayout.ExpandWidth(false));
			if (GUILayout.Button("Copy", GUILayout.Width(100))) {
				GUIUtility.systemCopyBuffer = cueName;
			}
			GUILayout.EndHorizontal();
			EditorGUILayout.LabelField("User Data", userData, EditorStyles.wordWrappedLabel, GUILayout.Height(28));

			if (GUI.changed && isCueSheetAvailable) {
				CriAtomWindowInfo.AcfInfo acfInfo = null;
				var acbInfo = acbInfoList[selectedCueSheetId];
				if (isAcfInfoAvailable) {
					acfInfo = acfInfoList[selectedAcfId];
				}

				if (playButtonPushed && isCueAvailable) {
					StopPreview();

					TryInitializePlugin();
					if (isAcfInfoAvailable == false) {
						CriAtomEx.UnregisterAcf();
						lastLoadedAcfGuid = null;
						selectedDspBusSettingId = 0;
					} else if (lastLoadedAcfGuid != acfInfo.assetGuid) {
						CriAtomEx.UnregisterAcf();
						CriAtomEx.RegisterAcf(null, Path.Combine(CriWare.Common.streamingAssetsPath, acfInfo.filePath));
						lastLoadedAcfGuid = acfInfo.assetGuid;
						selectedDspBusSettingId = 0;
					}
					if (lastLoadedAcbGuid != acbInfo.assetGuid) {
						if (previewAcb != null) {
							previewAcb.Dispose();
						}
						previewAcb = CriWare.Editor.CriAtomEditorUtilities.LoadAcbFile(null, acbInfo.acbPath, acbInfo.awbPath);
						lastLoadedAcbGuid = acbInfo.assetGuid;
					}
					if (previewAcb != null) {
						CriAtomEx.DetachDspBusSetting();
						if (isAcfInfoAvailable) {
							CriAtomEx.AttachDspBusSetting(acfInfo.dspBusSettingsList[selectedDspBusSettingId]);
						}
						PlayPreview(previewAcb, selectedCueInfo.name);
					}
				}
			}

			/* edit buttons */
			GUIEdit();
		} /* cueListAndInfoScope */

		EditorGUILayout.EndHorizontal();
	}

	private void GUIEdit()
	{
		EditorGUILayout.BeginHorizontal();
		{
			if (GUILayout.Button("Create GameObject", GUILayout.Height(22))) {
				this.CreateAtomSourceGameObject(true);
			}

			if (this.targetObject == null) {
				GUI.enabled = false;
			}
			if (GUILayout.Button("Add Component", GUILayout.Height(22))) {
				if (targetObject != null) {
					if (targetAtomSource != null) {
						if (EditorUtility.DisplayDialog("Duplicate Component", "CriAtomSource Component already exists. \nDo you really want to add more?", "Yes", "Cancel")) {
							this.CreateAtomSourceGameObject(false);
						}
					} else {
						this.CreateAtomSourceGameObject(false);
					}
				}
			}

			if (this.targetAtomSource == null) {
				GUI.enabled = false;
			}
			if (GUILayout.Button("Update Component", GUILayout.Height(22))) {
				if (targetAtomSource != null) {
					var acbInfoList = projInfo.GetAcbInfoList(false, criAtomWindowPrefs.searchPath);
					if (acbInfoList.Count > this.selectedCueSheetId) {
						var acbInfo = acbInfoList[this.selectedCueSheetId];
						var cueInfo = acbInfo.cueInfoList[this.selectedCueInfoIndex];
						Undo.RegisterCompleteObjectUndo(targetAtomSource, "AtomSource Update");
						targetAtomSource.cueSheet = acbInfo.name;
						targetAtomSource.cueName = cueInfo.name;
						Selection.activeGameObject = targetObject;
					}
				}
			}
			GUI.enabled = true;
		}
		EditorGUILayout.EndHorizontal();
	}

	private void GUIImportAssetsFromAtomCraft()
	{
		if (criAtomWindowPrefs == null) {
			criAtomWindowPrefs = CriAtomWindowPrefs.Load();
		}

		useCopyAssetsFromCriAtomCraft = CriFoldout(useCopyAssetsFromCriAtomCraft, "Import Assets from Atom Craft Project");
		if (useCopyAssetsFromCriAtomCraft) {
			EditorGUI.indentLevel++;

			GUILayout.BeginHorizontal();
			{
				if (criAtomWindowPrefs != null) {
					criAtomWindowPrefs.outputAssetsRoot = EditorGUILayout.TextField("Import From:", criAtomWindowPrefs.outputAssetsRoot);
				}

#if !OPENFOLDERPANEL_IS_BROKEN
				if (GUILayout.Button("...", EditorStyles.miniButton, GUILayout.Width(50))) {
					string tmpPath = "";
					string errorMsg;
					tmpPath = EditorUtility.OpenFolderPanel("Select CRI Atom Craft output Assets Folder", criAtomWindowPrefs.outputAssetsRoot, "");
					if (CheckPathIsAtomCraftAssetRoot(tmpPath, out errorMsg)) {
						criAtomWindowPrefs.outputAssetsRoot = tmpPath;
						criAtomWindowPrefs.Save();
					} else {
						Debug.LogWarning(errorMsg);
					}
				}
#endif
			}
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			{
				GUI.color = Color.yellow;
				EditorGUILayout.PrefixLabel(" ");
				if (GUILayout.Button("Import Assets", GUILayout.Width(160), GUILayout.Height(40))) {
					string errorMsg;
					if (CheckPathIsAtomCraftAssetRoot(criAtomWindowPrefs.outputAssetsRoot, out errorMsg) == false) {
						Debug.LogWarning(errorMsg);
					} else {
						try {
							CopyDirectory(criAtomWindowPrefs.outputAssetsRoot, Application.dataPath);
							Debug.Log("[CRIWARE] Assets successfully imported from " + criAtomWindowPrefs.outputAssetsRoot);
						} catch (Exception ex) {
							Debug.LogError(ex.Message);
							Debug.LogError("[CRIWARE] Failed to import assets from " + criAtomWindowPrefs.outputAssetsRoot);
						}
						AssetDatabase.Refresh();

						ReloadAcbInfo();
					}
				}
				GUI.color = Color.white;
				EditorGUILayout.HelpBox("Copy \"Assets\" folder (created in CRI Atom Craft) to the \"StreamingAssets\" folder.\nThe folder can be created by building with the \"With Unity Assets\" flag in CRI Atom Craft.", MessageType.Info);
			}
			GUILayout.EndHorizontal();

			EditorGUI.indentLevel--;
		}
	}

	private bool CheckPathIsAtomCraftAssetRoot(string outString, out string errorMsg)
	{
		errorMsg = "";
		try {
			if (string.IsNullOrEmpty(outString) == false && Path.GetFileName(outString) == "Assets") {
				if (Directory.Exists(outString) == false) {
					errorMsg = "[CRIWARE] Path to the assets does not exist: " + outString;
					return false;
				}
				foreach (string dir in Directory.GetDirectories(outString)) {
					if (Path.GetFileName(dir) == "Editor") {
						errorMsg = "[CRIWARE] Please choose the Assets folder in your <color=yellow>Atom Craft project</color> instead of the one in your Unity project. Import path will not be changed.";
						return false;
					}
				}
				return true;
			} else {
				errorMsg = "[CRIWARE] Unmatched folder name. Please set the right import path (\"Assets\" folder).";
				return false;
			}
		} catch (Exception ex) {
			errorMsg = "[CRIWARE] I/O Error: " + ex.Message;
			return false;
		}
	}

	private static void CopyDirectory(string sourceDirName, string destDirName)
	{
		try {
			if (Directory.Exists(destDirName) == false) {
				Directory.CreateDirectory(destDirName);
				File.SetAttributes(destDirName, File.GetAttributes(sourceDirName));
			}

			foreach (var file in Directory.GetFiles(sourceDirName)) {
				string ext = Path.GetExtension(file.Replace("\\", "/"));
				if (ext == ".acf" || ext == ".acb" || ext == ".awb") {
					string targetPath = Path.Combine(destDirName, Path.GetFileName(file));
					Debug.Log(String.Format("[CRIWARE] Copying \"{0}\" to \"{1}\"", file, targetPath));
					File.Copy(file, targetPath, true);
				}
			}

			foreach (var dir in Directory.GetDirectories(sourceDirName)) {
				/* sub directories */
				CopyDirectory(dir, Path.Combine(destDirName, Path.GetFileName(dir)));
			}
		} catch {
			throw;
		}
	}

	private void CreateAtomSourceGameObject(bool createGameObjectFlag)
	{
		if (Selection.gameObjects.Length == 0) {
			createGameObjectFlag = true;
		}
		var acbInfoList = projInfo.GetAcbInfoList(false, criAtomWindowPrefs.searchPath);
		if (acbInfoList.Count > this.selectedCueSheetId) {
			GameObject go = null;
			if (createGameObjectFlag) {
				go = new GameObject(acbInfoList[this.selectedCueSheetId].cueInfoList[this.selectedCueInfoIndex].name + "(CriAtomSource)");
				if (Selection.gameObjects.Length > 0) {
					go.transform.parent = Selection.gameObjects[0].transform;
				}
				Undo.RegisterCreatedObjectUndo(go, "Create AtomSource GameObject");
			} else {
				go = Selection.gameObjects[0];
			}
			var acbInfo = acbInfoList[this.selectedCueSheetId];
			CriAtom atom = null;
#if UNITY_2023_1_OR_NEWER
			atom = GameObject.FindAnyObjectByType<CriAtom>();
#else
			atom = GameObject.FindObjectOfType<CriAtom>();
#endif
			if (atom == null) {
				var criObject = GameObject.Find("/CRIWARE");
				if (criObject == null) {
					criObject = new GameObject("CRIWARE");
					Undo.RegisterCreatedObjectUndo(criObject, "Add CRIWARE GameObject");
				}
				atom = Undo.AddComponent<CriAtom>(criObject);
				var acfList = projInfo.GetAcfInfoList(false, criAtomWindowPrefs.searchPath);
				if (acfList.Count > selectedAcfId) {
					atom.acfFile = acfList[selectedAcfId].filePath;
				}
			}
			CriAtomCueSheet cueSheet = atom.GetCueSheetInternal(acbInfo.name);
			if (cueSheet == null) {
				cueSheet = atom.AddCueSheetInternal(null, acbInfo.acbPath, acbInfo.awbPath, null);
			}
			CriAtomSource newCriAtomSource = Undo.AddComponent<CriAtomSource>(go);
			newCriAtomSource.cueSheet = cueSheet.name;
			newCriAtomSource.cueName = acbInfo.cueInfoList[this.selectedCueInfoIndex].name;
			Selection.activeObject = go;
		}
	}

	private Vector2 GUILayoutResizeHandler(Rect rect, bool isVertical, ref Vector2 lastMousePos, ref bool isResizing) {
		EditorGUIUtility.AddCursorRect(rect, isVertical ? MouseCursor.SplitResizeLeftRight : MouseCursor.SplitResizeUpDown);

		if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition)) {
			lastMousePos = Event.current.mousePosition;
			isResizing = true;
		}

		if (isResizing) {
			var mousePos = Event.current.mousePosition;
			GUI.DrawTexture(new Rect(isVertical ? new Vector2(mousePos.x, rect.y) : new Vector2(rect.x, mousePos.y), rect.size), EditorGUIUtility.whiteTexture);
			Repaint();
			if (new Rect(Vector2.zero, this.position.size).Contains(mousePos) == false) {
				isResizing = false;
			}
		}

		if (Event.current.type == EventType.MouseUp) {
			if (isResizing) {
				isResizing = false;
				return Event.current.mousePosition - lastMousePos;
			}
			isResizing = false;
		}

		return Vector2.zero;
	}

	private string Ms2String(long timeMs) {
		long hh, mm, ss, ms;
		hh = timeMs / 3600000;
		mm = (timeMs % 3600000) / 60000;
		ss = (timeMs % 60000) / 1000;
		ms = timeMs % 1000;
		string res = ((hh > 0) ? hh.ToString("00") + ":" : "") + ((mm > 0) ? mm.ToString("00") + ":" : "") + ss.ToString("00") + ":" + ms.ToString("000");
		return res;
	}

	private bool CriFoldout(bool foldout, string content) {
#if UNITY_5_5_OR_NEWER
		return EditorGUILayout.Foldout(foldout, content, true);
#else
        return EditorGUILayout.Foldout(foldout, content);
#endif
	}
#endregion
}

} //namespace CriWare
/* end of file */
