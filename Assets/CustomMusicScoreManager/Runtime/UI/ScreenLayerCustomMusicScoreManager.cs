using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Cysharp.Threading.Tasks;
using Sekai.Live;
using Sekai.MusicScoreMaker.Common;
using Sekai.MusicScoreMaker.Ingame.Models;
using Sekai.MusicScoreMaker.Ingame.Presenters;
using SFB;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sekai.CustomMusicScoreManager
{
	public sealed class ScreenLayerCustomMusicScoreManager : ScreenLayer
	{
		private const string BaseFontEbPath = "font/FOT-RodinNTLGPro-EB SDF_Base";

		private const string DynamicFontEbPath = "font/FOT-RodinNTLGPro-EB SDF_Dynamic";

		private const string BaseFontDbPath = "font/FOT-RodinNTLGPro-DB SDF_Base";

		private const string DynamicFontDbPath = "font/FOT-RodinNTLGPro-DB SDF_Dynamic";

		private const int ManifestFieldColumnCount = 4;

		private const float ManifestFieldMinWidth = 210f;

		private const float ActionButtonWidth = 176f;

		private const float ActionButtonHeight = 44f;

		private static readonly string[] DifficultyTypes =
		{
			"easy",
			"normal",
			"hard",
			"expert",
			"master",
			"append"
		};

		private static TMP_FontAsset _baseFontEB;

		private static TMP_FontAsset _baseFontDB;

		private static bool _fontAssetSetup;

		private readonly List<RowView> _rows = new List<RowView>();
		private RectTransform _listContent;
		private RectTransform _manifestFieldGrid;
		private GridLayoutGroup _manifestFieldLayout;
		private TextMeshProUGUI _emptyText;
		private TextMeshProUGUI _statusText;
		private TextMeshProUGUI _detailTitle;
		private TextMeshProUGUI _detailMeta;
		private TextMeshProUGUI _detailStatus;
		private Image _jacketImage;
		private Button _editButton;
		private Button _playButton;
		private Button _autoButton;
		private Button _duplicateButton;
		private Button _deleteButton;
		private Button _exportButton;
		private Button _saveManifestButton;
		private Button _audioSelectButton;
		private Button _jacketSelectButton;
		private Button _scoreSelectButton;
		private RectTransform _settingsOverlay;
		private TMP_InputField _settingLiveBgmInput;
		private TMP_InputField _settingLiveSeInput;
		private TMP_InputField _settingNoteSpeedInput;
		private TMP_InputField _settingTimingAdjustInput;
		private TMP_InputField _titleInput;
		private TMP_InputField _scoreTitleInput;
		private TMP_InputField _userInput;
		private TMP_InputField _composerInput;
		private TMP_InputField _lyricistInput;
		private TMP_InputField _arrangerInput;
		private TMP_InputField _singerInput;
		private TMP_InputField _descriptionInput;
		private RectTransform _difficultyFieldRect;
		private TextMeshProUGUI _difficultyLabel;
		private Button _difficultyButton;
		private int _difficultyIndex = 4;
		private TMP_InputField _levelInput;
		private TMP_InputField _durationInput;
		private TMP_InputField _fillerInput;
		private TMP_InputField _audioInput;
		private TMP_InputField _jacketInput;
		private TMP_InputField _scoreInput;
		private IReadOnlyList<CustomMusicScoreManagerItem> _items = Array.Empty<CustomMusicScoreManagerItem>();
		private CustomMusicScoreManagerItem _selected;
		private Sprite _jacketSprite;

		protected override void Awake()
		{
			base.Awake();
			BuildView();
		}

		protected override void OnBoot(BootArgBase bootData)
		{
			RefreshList();
		}

		protected override void OnScreenStart()
		{
			RefreshList();
		}

		private void LateUpdate()
		{
			UpdateManifestFieldLayout();
		}

		protected override void OnExited()
		{
			ClearLoadedJacket();
			base.OnExited();
		}

		private void BuildView()
		{
			RectTransform root = GetComponent<RectTransform>() ?? gameObject.AddComponent<RectTransform>();
			Stretch(root);

			Image background = gameObject.GetComponent<Image>() ?? gameObject.AddComponent<Image>();
			background.color = new Color32(21, 25, 31, 255);

			RectTransform topBar = CreatePanel("TopBar", root, new Color32(31, 37, 45, 255));
			SetStretchTop(topBar, 0f, 0f, 0f, 92f);

			TextMeshProUGUI title = CreateText("Title", topBar, "Open Sekai 0.5.0", 34, FontStyles.Bold, TextAlignmentOptions.Left);
			SetAnchor(title.rectTransform, new Vector2(0f, 0f), new Vector2(0f, 1f), new Vector2(0f, 0.5f), new Vector2(32f, 0f), new Vector2(520f, 0f));

			RectTransform toolbar = CreateRect("Toolbar", topBar);
			SetAnchor(toolbar, new Vector2(1f, 0f), new Vector2(1f, 1f), new Vector2(1f, 0.5f), new Vector2(-32f, 0f), new Vector2(900f, 0f));
			HorizontalLayoutGroup toolbarLayout = toolbar.gameObject.AddComponent<HorizontalLayoutGroup>();
			toolbarLayout.childAlignment = TextAnchor.MiddleRight;
			toolbarLayout.childControlWidth = false;
			toolbarLayout.childControlHeight = false;
			toolbarLayout.spacing = 12f;
			CreateButton("SettingsButton", toolbar, "Settings", OpenSettings, 128f, 48f);
			CreateButton("RefreshButton", toolbar, "Refresh", RefreshList, 128f, 48f);
			CreateButton("NewButton", toolbar, "New", CreatePackage, 112f, 48f);
			CreateButton("ImportButton", toolbar, "Import", ImportPackage, 128f, 48f);

			RectTransform body = CreateRect("Body", root);
			SetStretchOffsets(body, 24f, 24f, 24f, 116f);

			RectTransform listPanel = CreatePanel("ListPanel", body, new Color32(26, 31, 38, 255));
			SetAnchor(listPanel, new Vector2(0f, 0f), new Vector2(0f, 1f), new Vector2(0f, 0.5f), new Vector2(24f, 0f), new Vector2(520f, 0f));

			RectTransform listHeader = CreateRect("ListHeader", listPanel);
			SetStretchTop(listHeader, 16f, 18f, 16f, 42f);
			TextMeshProUGUI listTitle = CreateText("ListTitle", listHeader, "Local Packages", 22, FontStyles.Bold, TextAlignmentOptions.Left);
			Stretch(listTitle.rectTransform);

			ScrollRect scrollRect = CreateScrollRect("PackageScroll", listPanel, out _listContent);
			SetStretchOffsets(scrollRect.GetComponent<RectTransform>(), 14f, 14f, 14f, 76f);

			_emptyText = CreateText("EmptyText", listPanel, "No local package", 22, FontStyles.Normal, TextAlignmentOptions.Center);
			SetStretchOffsets(_emptyText.rectTransform, 24f, 90f, 24f, 90f);

			RectTransform detailPanel = CreatePanel("DetailPanel", body, new Color32(28, 34, 42, 255));
			SetStretchOffsets(detailPanel, 568f, 0f, 0f, 0f);

			_jacketImage = CreateImage("Jacket", detailPanel, new Color32(42, 49, 58, 255));
			SetAnchor(_jacketImage.rectTransform, new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(24f, -24f), new Vector2(160f, 160f));
			_jacketImage.preserveAspect = true;

			_detailTitle = CreateText("DetailTitle", detailPanel, "Select a package", 32, FontStyles.Bold, TextAlignmentOptions.Left);
			SetStretchTop(_detailTitle.rectTransform, 208f, 28f, 28f, 44f);

			_detailMeta = CreateText("DetailMeta", detailPanel, string.Empty, 20, FontStyles.Normal, TextAlignmentOptions.Left);
			SetStretchTop(_detailMeta.rectTransform, 208f, 78f, 28f, 76f);

			_detailStatus = CreateText("DetailStatus", detailPanel, string.Empty, 20, FontStyles.Bold, TextAlignmentOptions.Left);
			SetStretchTop(_detailStatus.rectTransform, 208f, 158f, 28f, 32f);

			ScrollRect actionScroll = CreateHorizontalScrollRect("ActionButtons", detailPanel, out RectTransform actionButtons);
			SetStretchTop(actionScroll.GetComponent<RectTransform>(), 24f, 214f, 24f, 56f);
			_editButton = CreateButton("EditButton", actionButtons, "Edit", OpenEditor, ActionButtonWidth, ActionButtonHeight);
			_playButton = CreateButton("PlayButton", actionButtons, "Play", PlaySelected, ActionButtonWidth, ActionButtonHeight);
			_autoButton = CreateButton("AutoButton", actionButtons, "Auto", AutoPlaySelected, ActionButtonWidth, ActionButtonHeight);
			_duplicateButton = CreateButton("DuplicateButton", actionButtons, "Duplicate", DuplicateSelected, ActionButtonWidth, ActionButtonHeight);
			_exportButton = CreateButton("ExportButton", actionButtons, "Export Zip", ExportSelected, ActionButtonWidth, ActionButtonHeight);
			_deleteButton = CreateButton("DeleteButton", actionButtons, "Delete", DeleteSelected, ActionButtonWidth, ActionButtonHeight, new Color32(110, 49, 57, 255));

			ScrollRect formScroll = CreateMaskedScrollRect("ManifestFormScroll", detailPanel, out RectTransform form);
			SetStretchOffsets(formScroll.GetComponent<RectTransform>(), 24f, 78f, 24f, 290f);
			VerticalLayoutGroup formLayout = form.gameObject.AddComponent<VerticalLayoutGroup>();
			formLayout.spacing = 12f;
			formLayout.padding = new RectOffset(16, 16, 16, 16);
			formLayout.childControlWidth = true;
			formLayout.childControlHeight = false;
			ContentSizeFitter formFitter = form.gameObject.AddComponent<ContentSizeFitter>();
			formFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

			_manifestFieldGrid = CreateRect("FieldGrid", form);
			_manifestFieldLayout = _manifestFieldGrid.gameObject.AddComponent<GridLayoutGroup>();
			_manifestFieldLayout.cellSize = new Vector2(ManifestFieldMinWidth, 70f);
			_manifestFieldLayout.spacing = new Vector2(14f, 10f);
			_manifestFieldLayout.startAxis = GridLayoutGroup.Axis.Horizontal;
			_manifestFieldLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
			_manifestFieldLayout.constraintCount = ManifestFieldColumnCount;
			ContentSizeFitter fieldFitter = _manifestFieldGrid.gameObject.AddComponent<ContentSizeFitter>();
			fieldFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

			_titleInput = CreateInputField(_manifestFieldGrid, "Title", "title");
			_scoreTitleInput = CreateInputField(_manifestFieldGrid, "Score Title", "scoreTitle");
			_userInput = CreateInputField(_manifestFieldGrid, "Author", "userName");
			_composerInput = CreateInputField(_manifestFieldGrid, "Composer", "composer");
			_lyricistInput = CreateInputField(_manifestFieldGrid, "Lyricist", "lyricist");
			_arrangerInput = CreateInputField(_manifestFieldGrid, "Arranger", "arranger");
			_singerInput = CreateInputField(_manifestFieldGrid, "Singer", "singer");
			_descriptionInput = CreateInputField(_manifestFieldGrid, "Description", "description");
			_difficultyButton = CreateDifficultySelector(_manifestFieldGrid);
			_levelInput = CreateInputField(_manifestFieldGrid, "Level", "playLevel");
			_durationInput = CreateInputField(_manifestFieldGrid, "Duration", "secForMusicScoreMaker");
			_fillerInput = CreateInputField(_manifestFieldGrid, "Filler Sec", "fillerSec");
			_audioInput = CreateInputField(_manifestFieldGrid, "Audio", "audioFileName", ReplaceSelectedAudio, out _audioSelectButton);
			_jacketInput = CreateInputField(_manifestFieldGrid, "Jacket (740x740)", "jacketFileName", ReplaceSelectedJacket, out _jacketSelectButton);
			_scoreInput = CreateInputField(_manifestFieldGrid, "Score", "scoreFileName", ReplaceSelectedScore, out _scoreSelectButton);
			RectTransform saveRow = CreateRect("SaveManifestRow", form);
			LayoutElement saveRowLayout = saveRow.gameObject.AddComponent<LayoutElement>();
			saveRowLayout.preferredHeight = 44f;
			saveRowLayout.minHeight = 44f;
			HorizontalLayoutGroup saveRowGroup = saveRow.gameObject.AddComponent<HorizontalLayoutGroup>();
			saveRowGroup.childControlWidth = false;
			saveRowGroup.childControlHeight = false;
			saveRowGroup.childAlignment = TextAnchor.MiddleLeft;
			_saveManifestButton = CreateButton("SaveManifestButton", saveRow, "Save Manifest", SaveSelectedManifest, 176f, 44f);

			RectTransform bottom = CreateRect("BottomBar", detailPanel);
			SetStretchBottom(bottom, 28f, 26f, 28f, 34f);
			_statusText = CreateText("StatusText", bottom, string.Empty, 18, FontStyles.Normal, TextAlignmentOptions.Left);
			Stretch(_statusText.rectTransform);

			BuildSettingsOverlay(root);
			UpdateSelection(null);
		}

		private void BuildSettingsOverlay(RectTransform root)
		{
			_settingsOverlay = CreatePanel("SettingsOverlay", root, new Color32(0, 0, 0, 176));
			Stretch(_settingsOverlay);
			_settingsOverlay.gameObject.SetActive(false);

			RectTransform dialog = CreatePanel("SettingsDialog", _settingsOverlay, new Color32(31, 37, 45, 255));
			SetAnchor(dialog, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), Vector2.zero, new Vector2(640f, 560f));

			VerticalLayoutGroup dialogLayout = dialog.gameObject.AddComponent<VerticalLayoutGroup>();
			dialogLayout.padding = new RectOffset(24, 24, 24, 24);
			dialogLayout.spacing = 12f;
			dialogLayout.childControlWidth = true;
			dialogLayout.childControlHeight = false;
			dialogLayout.childForceExpandWidth = true;
			dialogLayout.childForceExpandHeight = false;

			TextMeshProUGUI title = CreateText("Title", dialog, "Settings", 30, FontStyles.Bold, TextAlignmentOptions.Left);
			LayoutElement titleLayout = title.gameObject.AddComponent<LayoutElement>();
			titleLayout.preferredHeight = 38f;

			_settingLiveBgmInput = CreateInputField(dialog, "LiveVolume.Bgm", "0.0 - 1.0");
			_settingLiveSeInput = CreateInputField(dialog, "LiveVolume.Se", "0.0 - 1.0");
			_settingNoteSpeedInput = CreateInputField(dialog, "LiveSettingData.NoteSpeed", "1.0 - 12.0");
			_settingTimingAdjustInput = CreateInputField(dialog, "LiveSettingData.TimingAdjustData", "-20.0 - 20.0");

			RectTransform buttonRow = CreateRect("ButtonRow", dialog);
			LayoutElement buttonRowLayout = buttonRow.gameObject.AddComponent<LayoutElement>();
			buttonRowLayout.preferredHeight = 48f;
			buttonRowLayout.minHeight = 48f;
			HorizontalLayoutGroup buttonRowGroup = buttonRow.gameObject.AddComponent<HorizontalLayoutGroup>();
			buttonRowGroup.spacing = 12f;
			buttonRowGroup.childAlignment = TextAnchor.MiddleRight;
			buttonRowGroup.childControlWidth = false;
			buttonRowGroup.childControlHeight = false;
			buttonRowGroup.childForceExpandWidth = false;
			buttonRowGroup.childForceExpandHeight = false;

			CreateButton("CancelButton", buttonRow, "Cancel", CloseSettings, 128f, 44f);
			CreateButton("SaveButton", buttonRow, "Save", SaveSettings, 128f, 44f);
		}

		private void OpenSettings()
		{
			ApplicationLocalSettings localSettings = ApplicationLocalSettings.LoadFromStorage();
			ApplicationLocalSettings.VolumeSettings liveVolume = localSettings.LiveVolume ?? localSettings.SetupLiveVolume();
			LiveSettingData liveSettingData = LiveSettingData.LoadFromStorage();

			_settingLiveBgmInput.SetTextWithoutNotify(FormatSettingValue(liveVolume.Bgm));
			_settingLiveSeInput.SetTextWithoutNotify(FormatSettingValue(liveVolume.Se));
			_settingNoteSpeedInput.SetTextWithoutNotify(FormatSettingValue(liveSettingData.NoteSpeed));
			_settingTimingAdjustInput.SetTextWithoutNotify(FormatSettingValue(liveSettingData.TimingAdjustData));
			_settingsOverlay.gameObject.SetActive(true);
			_settingsOverlay.SetAsLastSibling();
		}

		private void CloseSettings()
		{
			if (_settingsOverlay != null)
			{
				_settingsOverlay.gameObject.SetActive(false);
			}
		}

		private void SaveSettings()
		{
			ApplicationLocalSettings localSettings = ApplicationLocalSettings.LoadFromStorage();
			ApplicationLocalSettings.VolumeSettings liveVolume = localSettings.LiveVolume ?? localSettings.SetupLiveVolume();
			LiveSettingData liveSettingData = LiveSettingData.LoadFromStorage();

			liveVolume.Bgm = ParseClampedSetting(_settingLiveBgmInput.text, 0f, 1f, liveVolume.Bgm);
			liveVolume.Se = ParseClampedSetting(_settingLiveSeInput.text, 0f, 1f, liveVolume.Se);
			liveSettingData.NoteSpeed = ParseClampedSetting(_settingNoteSpeedInput.text, LiveConfig.MinNoteSpeed, LiveConfig.MaxNoteSpeed, liveSettingData.NoteSpeed);
			liveSettingData.TimingAdjustData = ParseClampedSetting(
				_settingTimingAdjustInput.text,
				LiveConfig.MinNoteTiming,
				LiveConfig.MaxNoteTiming,
				liveSettingData.TimingAdjustData);

			ApplicationLocalSettings.SaveToStorage(localSettings);
			LiveSettingData.SaveToStorage(liveSettingData);
			SoundManager.Instance.SetupVolume(1f, liveVolume.Bgm, liveVolume.Se, liveVolume.Voice);

			_settingLiveBgmInput.SetTextWithoutNotify(FormatSettingValue(liveVolume.Bgm));
			_settingLiveSeInput.SetTextWithoutNotify(FormatSettingValue(liveVolume.Se));
			_settingNoteSpeedInput.SetTextWithoutNotify(FormatSettingValue(liveSettingData.NoteSpeed));
			_settingTimingAdjustInput.SetTextWithoutNotify(FormatSettingValue(liveSettingData.TimingAdjustData));
			CloseSettings();
			SetStatus("Saved settings.");
		}

		private static float ParseClampedSetting(string text, float min, float max, float fallback)
		{
			if (!float.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out float value))
			{
				value = fallback;
			}
			return Mathf.Clamp(value, min, max);
		}

		private static string FormatSettingValue(float value)
		{
			return value.ToString("0.###", CultureInfo.InvariantCulture);
		}

		private void UpdateManifestFieldLayout()
		{
			if (_manifestFieldGrid == null || _manifestFieldLayout == null)
			{
				return;
			}

			float availableWidth = _manifestFieldGrid.rect.width;
			if (availableWidth <= 0f)
			{
				return;
			}

			float spacingWidth = _manifestFieldLayout.spacing.x * (ManifestFieldColumnCount - 1);
			float cellWidth = Mathf.Floor((availableWidth - spacingWidth) / ManifestFieldColumnCount);
			cellWidth = Mathf.Max(ManifestFieldMinWidth, cellWidth);
			Vector2 cellSize = _manifestFieldLayout.cellSize;
			if (Mathf.Abs(cellSize.x - cellWidth) <= 0.5f)
			{
				return;
			}

			_manifestFieldLayout.cellSize = new Vector2(cellWidth, cellSize.y);
			LayoutRebuilder.MarkLayoutForRebuild(_manifestFieldGrid);
		}

		private void RefreshList()
		{
			_items = CustomMusicScoreManagerService.LoadItems();
			foreach (RowView row in _rows)
			{
				if (row.Root != null)
				{
					Destroy(row.Root);
				}
			}
			_rows.Clear();

			foreach (CustomMusicScoreManagerItem item in _items)
			{
				RowView row = CreateRow(_listContent, item);
				_rows.Add(row);
			}

			_emptyText.gameObject.SetActive(_items.Count == 0);
			CustomMusicScoreManagerItem selected = null;
			if (_selected != null)
			{
				string selectedPath = _selected.Package.RootDirectory;
				for (int i = 0; i < _items.Count; i++)
				{
					if (string.Equals(_items[i].Package.RootDirectory, selectedPath, StringComparison.OrdinalIgnoreCase))
					{
						selected = _items[i];
						break;
					}
				}
			}
			UpdateSelection(selected ?? (_items.Count > 0 ? _items[0] : null));
			SetStatus("Loaded " + _items.Count.ToString(CultureInfo.InvariantCulture) + " package(s).");
		}

		private RowView CreateRow(RectTransform parent, CustomMusicScoreManagerItem item)
		{
			GameObject root = new GameObject("PackageCell", typeof(RectTransform), typeof(Image), typeof(Button), typeof(LayoutElement));
			root.transform.SetParent(parent, false);
			RectTransform rect = root.GetComponent<RectTransform>();
			rect.sizeDelta = new Vector2(0f, 92f);
			LayoutElement layout = root.GetComponent<LayoutElement>();
			layout.preferredHeight = 92f;
			layout.minHeight = 92f;

			Image image = root.GetComponent<Image>();
			image.color = new Color32(39, 46, 55, 255);
			Button button = root.GetComponent<Button>();
			button.targetGraphic = image;
			button.onClick.AddListener(() => UpdateSelection(item));

			TextMeshProUGUI title = CreateText("Title", rect, item.Package.Manifest.scoreTitle, 22, FontStyles.Bold, TextAlignmentOptions.Left);
			SetAnchor(title.rectTransform, new Vector2(0f, 1f), new Vector2(1f, 1f), new Vector2(0f, 1f), new Vector2(18f, -12f), new Vector2(-36f, 30f));

			string metaText = item.Package.Manifest.title;
			if (!string.IsNullOrEmpty(item.Package.Manifest.userName))
			{
				metaText += "  " + item.Package.Manifest.userName;
			}
			if (!string.IsNullOrEmpty(item.Package.Manifest.musicDifficultyType))
			{
				metaText += "  " + item.Package.Manifest.musicDifficultyType.ToUpperInvariant();
			}
			TextMeshProUGUI meta = CreateText("Meta", rect, metaText, 17, FontStyles.Normal, TextAlignmentOptions.Left);
			SetAnchor(meta.rectTransform, new Vector2(0f, 0f), new Vector2(1f, 0f), new Vector2(0f, 0f), new Vector2(18f, 12f), new Vector2(-180f, 26f));

			TextMeshProUGUI status = CreateText("Status", rect, item.StatusText, 17, FontStyles.Bold, TextAlignmentOptions.Right);
			SetAnchor(status.rectTransform, new Vector2(1f, 0f), new Vector2(1f, 0f), new Vector2(1f, 0f), new Vector2(-18f, 12f), new Vector2(150f, 26f));
			status.color = item.HasAudio && item.HasScore ? new Color32(126, 221, 166, 255) : new Color32(255, 184, 100, 255);

			return new RowView(root, image, item);
		}

		private void UpdateSelection(CustomMusicScoreManagerItem item)
		{
			_selected = item;
			foreach (RowView row in _rows)
			{
				row.Background.color = row.Item == item ? new Color32(52, 91, 112, 255) : new Color32(39, 46, 55, 255);
			}

			bool hasSelection = item != null;
			_editButton.interactable = hasSelection && item.IsReadyForEdit;
			_playButton.interactable = hasSelection && item.HasScore && item.HasAudio;
			_autoButton.interactable = hasSelection && item.HasScore && item.HasAudio;
			_duplicateButton.interactable = hasSelection;
			_deleteButton.interactable = hasSelection;
			_exportButton.interactable = hasSelection;
			_saveManifestButton.interactable = hasSelection;
			_audioSelectButton.interactable = hasSelection;
			_jacketSelectButton.interactable = hasSelection;
			_scoreSelectButton.interactable = hasSelection;
			SetFormInteractable(hasSelection);

			if (!hasSelection)
			{
				_detailTitle.text = "Select a package";
				_detailMeta.text = string.Empty;
				_detailStatus.text = string.Empty;
				ClearLoadedJacket();
				ClearForm();
				return;
			}

			CustomMusicScoreManifest manifest = item.Package.Manifest;
			_detailTitle.text = manifest.scoreTitle;
			_detailMeta.text = string.Format(
				CultureInfo.InvariantCulture,
				"title: {0}\nid: {1}\npath: {2}\nupdated: {3:yyyy-MM-dd HH:mm}",
				manifest.title,
				manifest.id,
				item.Package.RootDirectory,
				item.LastWriteTime);
			_detailStatus.text = item.StatusText;
			_detailStatus.color = item.HasAudio && item.HasScore ? new Color32(126, 221, 166, 255) : new Color32(255, 184, 100, 255);
			LoadJacket(item.Package.JacketPath);
			LoadForm(manifest);
		}

		private void CreatePackage()
		{
			CustomMusicScorePackage package = CustomMusicScoreManagerService.CreateNewPackage();
			_selected = package == null ? null : new CustomMusicScoreManagerItem(
				package,
				DateTime.Now,
				true,
				File.Exists(package.ScorePath),
				File.Exists(package.AudioPath),
				File.Exists(package.JacketPath));
			RefreshList();
			SetStatus("Created package.");
		}

		private void OpenEditor()
		{
			if (_selected?.Package == null)
			{
				return;
			}

			CustomMusicScorePackage package = CustomMusicScoreStorage.LoadPackage(_selected.Package.RootDirectory);
			if (package == null)
			{
				SetStatus("Package could not be loaded.");
				RefreshList();
				return;
			}

			ScreenLayerMusicScoreMaker.BootArg bootArg = new ScreenLayerMusicScoreMaker.BootArg
			{
				musicId = package.MusicId,
				difficulty = package.Manifest.musicDifficultyType,
				vocalId = 0,
				baseMusicDifficultyId = -1,
				MusicScoreMakerData = package.LoadScore(),
				LastSavedDataHash = null,
				CurrentMusicScoreScale = 1f,
				FromScreenType = MenuScreenType.MusicScoreMakerTop,
				CustomMusicScorePackage = package
			};

			ScreenManager.Instance?.PushUIScreen(MenuScreenType.MusicScoreMaker, bootArg, false);
		}

		private void PlaySelected()
		{
			PlaySelectedAsync(false).Forget();
		}

		private void AutoPlaySelected()
		{
			PlaySelectedAsync(true).Forget();
		}

		private async UniTask PlaySelectedAsync(bool isAuto)
		{
			if (_selected?.Package == null)
			{
				return;
			}

			CustomMusicScorePackage package = SaveSelectedManifestFromForm(refreshList: false);
			package ??= CustomMusicScoreStorage.LoadPackage(_selected.Package.RootDirectory);
			if (package == null)
			{
				SetStatus("Package could not be loaded.");
				RefreshList();
				return;
			}

			if (!File.Exists(package.ScorePath))
			{
				SetStatus("Score file not found.");
				return;
			}
			if (!File.Exists(package.AudioPath))
			{
				SetStatus("Audio file not found.");
				return;
			}

			MusicScoreMakerData scoreData = package.LoadScore();
			if (scoreData == null)
			{
				SetStatus("Score file could not be loaded.");
				return;
			}
			if (!HasPlayableNotes(scoreData))
			{
				SetStatus("No notes to play.");
				return;
			}

			SetStatus("Loading audio...");
			bool audioReady = await package.RegisterAudioAsync(this.GetCancellationTokenOnDestroy());
			if (!audioReady)
			{
				SetStatus("Audio file could not be loaded.");
				return;
			}

			FreeLiveBootData bootData = CreateDirectPlayBootData(package, scoreData, isAuto);
			if (bootData == null)
			{
				SetStatus("Live boot data could not be created.");
				return;
			}

			UserDataManager.Instance.FreeLiveBootData = bootData;
			Sekai.Core.EntryPoint.PlayMode = Sekai.Core.PlayMode.SoloLive;
			LiveTransitioner.SafeForceFinish(null);
			ScreenManager.Instance?.PushUIScreen(MenuScreenType.LiveLoading, false);
			SetStatus(isAuto ? "Starting auto live..." : "Starting live...");
		}

		private static bool HasPlayableNotes(MusicScoreMakerData data)
		{
			return data?.NoteList != null && data.NoteList.Exists(note => note != null);
		}

		private FreeLiveBootData CreateDirectPlayBootData(CustomMusicScorePackage package, MusicScoreMakerData scoreData, bool isAuto)
		{
			if (package == null || scoreData == null)
			{
				return null;
			}

			LiveBundleBuildData liveBundleBuildData = Resources.Load<LiveBundleBuildData>(LiveConfig.ConfigBundleNamePath);
			MusicScore musicScore = scoreData.ToMusicScore(liveBundleBuildData);
			int deckId = UserDataManager.Instance.SelectedDeckId;
			MasterMusicDifficulty difficulty = CreateDirectPlayDifficulty(package, scoreData);
			string difficultyString = difficulty?.musicDifficulty ?? "master";
			MusicCategory musicCategory = MusicCategory.original;

			// Matches the original manager play route: normal FreeLive, not editor test play.
			FreeLiveBootData bootData = new FreeLiveBootData(
				package.MusicId,
				difficultyString,
				0,
				deckId,
				LivePlayMode.Free,
				LiveMusicData.CollaborationModeState.Off,
				musicCategory);

			bootData.LiveEventData = new LiveEventData(Array.Empty<IngameLotterySkill>(), Array.Empty<IngameComboCutin>(), deckId, isAuto);
			bootData.LiveSettingData = LiveSettingData.LoadFromStorage();
			bootData.MVQualityType = bootData.LiveSettingData?.QualityType ?? Sekai.MVQualityType.Default;
			bootData.MusicCategory = musicCategory;
			bootData.IsAuto = isAuto;
			bootData.IsCustomMusicScore = true;
			bootData.IsOfficialMusicScore = false;
			bootData.ReturnScreenType = MenuScreenType.MusicScoreMakerTop;
			bootData.canSkipDisplayMusicInfo = false;
			bootData.ReleaseTransitionBeforeMusicStart = true;
			bootData.CustomMusicScoreId = package.Manifest.id;
			bootData.CustomMusicScorePath = package.RootDirectory;
			bootData.CustomMusicScoreTitle = package.Manifest.scoreTitle;
			bootData.CustomMusicScoreAuthorName = package.Manifest.userName;

			if (bootData.MusicData != null)
			{
				bootData.MusicData.Music = CreateDirectPlayMusic(package);
				bootData.MusicData.Difficulty = difficulty;
				bootData.MusicData.Vocal = CreateDirectPlayVocal(package);
				bootData.MusicData.Score = new MasterPlayLevelScore
				{
					liveType = LiveType.solo.ToString(),
					playLevel = package.Manifest.playLevel
				};
				bootData.MusicData.IsTestPlay = false;
				bootData.MusicData.IsUseCustomScore = true;
				bootData.MusicData.CustomPlayLevel = package.Manifest.playLevel;
				bootData.MusicData.MusicScore = musicScore;
				bootData.MusicData.StartMusicTimeMs = 0L;
				bootData.MusicData.PlayStartEffectEnabled = true;
			}

			return bootData;
		}

		private static MasterMusicDifficulty CreateDirectPlayDifficulty(CustomMusicScorePackage package, MusicScoreMakerData scoreData)
		{
			return new MasterMusicDifficulty
			{
				id = MasterMusicDifficulty.INVALID_ID,
				musicId = package.MusicId,
				musicDifficulty = NormalizeDifficulty(package.Manifest.musicDifficultyType),
				playLevel = package.Manifest.playLevel,
				totalNoteCount = scoreData?.NoteList?.Count ?? 0
			};
		}

		private static MasterMusic CreateDirectPlayMusic(CustomMusicScorePackage package)
		{
			return new MasterMusic
			{
				id = package.MusicId,
				title = package.Manifest.title,
				lyricist = package.Manifest.lyricist,
				composer = package.Manifest.composer,
				arranger = package.Manifest.arranger,
				assetbundleName = package.AudioCueName,
				fillerSec = package.Manifest.fillerSec,
				secForMusicScoreMaker = package.MusicDurationSec,
				isAvailableForMusicScoreMaker = true
			};
		}

		private static MasterMusicVocal CreateDirectPlayVocal(CustomMusicScorePackage package)
		{
			return new MasterMusicVocal
			{
				id = 0,
				musicId = package.MusicId,
				musicVocalType = MusicVocalType.original_song.ToString(),
				caption = package.Manifest.singer,
				assetbundleName = package.AudioCueName
			};
		}

		private static string NormalizeDifficulty(string difficulty)
		{
			return string.IsNullOrWhiteSpace(difficulty) || string.Equals(difficulty, "none", StringComparison.OrdinalIgnoreCase)
				? "master"
				: difficulty.ToLowerInvariant();
		}

		private void DuplicateSelected()
		{
			if (_selected == null)
			{
				return;
			}

			CustomMusicScorePackage package = CustomMusicScoreManagerService.DuplicatePackage(_selected.Package);
			_selected = package == null ? null : new CustomMusicScoreManagerItem(package, DateTime.Now, true, File.Exists(package.ScorePath), File.Exists(package.AudioPath), File.Exists(package.JacketPath));
			RefreshList();
			SetStatus("Duplicated package.");
		}

		private void DeleteSelected()
		{
			if (_selected == null)
			{
				return;
			}

			string title = _selected.Package.Manifest.scoreTitle;
			CustomMusicScoreManagerService.DeletePackage(_selected.Package);
			_selected = null;
			RefreshList();
			SetStatus("Deleted " + title + ".");
		}

		private void ExportSelected()
		{
			if (_selected == null)
			{
				return;
			}

			string destination = null;
#if UNITY_EDITOR || UNITY_STANDALONE
			string defaultName = _selected.Package.Manifest.scoreTitle + "_" + _selected.Package.Manifest.id;
			destination = SaveStandaloneFile("Export Custom Music Score", CustomMusicScoreStorage.RootDirectory, defaultName, "zip");
			if (string.IsNullOrEmpty(destination))
			{
				return;
			}
#endif
			string path = CustomMusicScoreManagerService.ExportZip(_selected.Package, destination);
			SetStatus(string.IsNullOrEmpty(path) ? "Export failed." : "Exported: " + path);
		}

		private void ImportPackage()
		{
#if UNITY_EDITOR || UNITY_STANDALONE
			CustomMusicScorePackage package = null;
			string path = PickStandaloneFile(
				"Import Custom Music Score Zip",
				string.Empty,
				new ExtensionFilter("Custom Music Score Zip", "zip"));
			if (!string.IsNullOrEmpty(path))
			{
				package = CustomMusicScoreManagerService.ImportZip(path);
			}
			else
			{
				string folder = PickStandaloneFolder("Import Custom Music Score Folder", string.Empty);
				if (!string.IsNullOrEmpty(folder))
				{
					package = CustomMusicScoreManagerService.ImportFolder(folder);
				}
			}
			ApplyImportedPackage(package);
#elif UNITY_ANDROID || UNITY_IOS
			PickNativeFile(
				"Import Custom Music Score Zip",
				"Import canceled or failed.",
				path => ApplyImportedPackage(CustomMusicScoreManagerService.ImportZip(path)),
				"zip");
			return;
#else
			SetStatus("Runtime import uses Native File Picker on Android/iOS. Copy packages manually on this platform.");
			return;
#endif
		}

		private void ApplyImportedPackage(CustomMusicScorePackage package)
		{
			if (package != null)
			{
				_selected = new CustomMusicScoreManagerItem(package, DateTime.Now, true, File.Exists(package.ScorePath), File.Exists(package.AudioPath), File.Exists(package.JacketPath));
				RefreshList();
				SetStatus("Imported package.");
			}
			else
			{
				SetStatus("Import canceled or failed.");
			}
		}

		private void ReplaceSelectedAudio()
		{
			if (_selected?.Package == null)
			{
				return;
			}

#if UNITY_EDITOR || UNITY_STANDALONE
			string path = PickStandaloneFile(
				"Replace Audio File",
				string.Empty,
				new ExtensionFilter("Audio Files", "ogg", "mp3", "wav"));
			if (string.IsNullOrEmpty(path))
			{
				SetStatus("Audio replacement canceled.");
				return;
			}

			ReplaceSelectedFile(path, CustomMusicScoreManagerService.ReplaceAudioFile, "Replaced audio");
#elif UNITY_ANDROID || UNITY_IOS
			PickNativeFile(
				"Replace Audio File",
				"Audio replacement canceled.",
				path => ReplaceSelectedFile(path, CustomMusicScoreManagerService.ReplaceAudioFile, "Replaced audio"),
				"ogg",
				"mp3",
				"wav");
#else
			SetStatus("Runtime file selection uses Native File Picker on Android/iOS. Copy the audio file manually on this platform.");
#endif
		}

		private void ReplaceSelectedJacket()
		{
			if (_selected?.Package == null)
			{
				return;
			}

#if UNITY_EDITOR || UNITY_STANDALONE
			string path = PickStandaloneFile(
				"Replace Jacket File",
				string.Empty,
				new ExtensionFilter("Image Files", "png", "jpg", "jpeg"));
			if (string.IsNullOrEmpty(path))
			{
				SetStatus("Jacket replacement canceled.");
				return;
			}

			ReplaceSelectedFile(path, CustomMusicScoreManagerService.ReplaceJacketFile, "Replaced jacket");
#elif UNITY_ANDROID || UNITY_IOS
			PickNativeFile(
				"Replace Jacket File",
				"Jacket replacement canceled.",
				path => ReplaceSelectedFile(path, CustomMusicScoreManagerService.ReplaceJacketFile, "Replaced jacket"),
				"png",
				"jpg",
				"jpeg");
#else
			SetStatus("Runtime file selection uses Native File Picker on Android/iOS. Copy the jacket file manually on this platform.");
#endif
		}

		private void ReplaceSelectedScore()
		{
			if (_selected?.Package == null)
			{
				return;
			}

#if UNITY_EDITOR || UNITY_STANDALONE
			string path = PickStandaloneFile(
				"Replace Score File",
				string.Empty,
				new ExtensionFilter("Score Files", "json", "txt"));
			if (string.IsNullOrEmpty(path))
			{
				SetStatus("Score replacement canceled.");
				return;
			}

			ReplaceSelectedFile(path, CustomMusicScoreManagerService.ReplaceScoreFile, "Replaced score");
#elif UNITY_ANDROID || UNITY_IOS
			PickNativeFile(
				"Replace Score File",
				"Score replacement canceled.",
				path => ReplaceSelectedFile(path, CustomMusicScoreManagerService.ReplaceScoreFile, "Replaced score"),
				"json",
				"txt");
#else
			SetStatus("Runtime file selection uses Native File Picker on Android/iOS. Copy the score file manually on this platform.");
#endif
		}

#if UNITY_EDITOR || UNITY_STANDALONE
		private static string PickStandaloneFile(string title, string directory, params ExtensionFilter[] filters)
		{
			string[] paths = StandaloneFileBrowser.OpenFilePanel(title, directory ?? string.Empty, filters, false);
			return paths != null && paths.Length > 0 ? paths[0] : string.Empty;
		}

		private static string PickStandaloneFolder(string title, string directory)
		{
			string[] paths = StandaloneFileBrowser.OpenFolderPanel(title, directory ?? string.Empty, false);
			return paths != null && paths.Length > 0 ? paths[0] : string.Empty;
		}

		private static string SaveStandaloneFile(string title, string directory, string defaultName, string extension)
		{
			return StandaloneFileBrowser.SaveFilePanel(title, directory ?? string.Empty, defaultName ?? string.Empty, extension);
		}
#endif

#if UNITY_ANDROID || UNITY_IOS
		private void PickNativeFile(string title, string cancelStatus, Action<string> onPicked, params string[] extensions)
		{
			if (NativeFilePicker.IsFilePickerBusy())
			{
				SetStatus("File picker is already open.");
				return;
			}

			SetStatus(title + "...");
			NativeFilePicker.PickFile(path =>
			{
				if (this == null)
				{
					return;
				}

				if (string.IsNullOrEmpty(path))
				{
					SetStatus(cancelStatus);
					return;
				}

				onPicked?.Invoke(path);
			}, CreateNativeFileTypes(extensions));
		}

		private static string[] CreateNativeFileTypes(params string[] extensions)
		{
			if (extensions == null || extensions.Length == 0)
			{
				return Array.Empty<string>();
			}

			List<string> fileTypes = new List<string>();
			for (int i = 0; i < extensions.Length; i++)
			{
				string extension = extensions[i];
				if (string.IsNullOrWhiteSpace(extension))
				{
					continue;
				}

				extension = extension.Trim().TrimStart('.');
				string fileType = NativeFilePicker.ConvertExtensionToFileType(extension);
				if (!string.IsNullOrEmpty(fileType) && !fileTypes.Contains(fileType))
				{
					fileTypes.Add(fileType);
				}
			}

			return fileTypes.ToArray();
		}
#endif

		private void ReplaceSelectedFile(
			string sourcePath,
			Func<CustomMusicScorePackage, string, CustomMusicScorePackage> replaceFile,
			string successStatus)
		{
			try
			{
				CustomMusicScorePackage package = replaceFile(_selected.Package, sourcePath);
				if (package == null)
				{
					SetStatus("Replacement failed.");
					return;
				}

				_selected = new CustomMusicScoreManagerItem(
					package,
					DateTime.Now,
					File.Exists(package.ManifestPath),
					File.Exists(package.ScorePath),
					File.Exists(package.AudioPath),
					File.Exists(package.JacketPath));
				RefreshList();
				SetStatus(successStatus + ": " + Path.GetFileName(sourcePath));
			}
			catch (Exception ex)
			{
				SetStatus(ex.Message);
			}
		}

		private void SaveSelectedManifest()
		{
			CustomMusicScorePackage savedPackage = SaveSelectedManifestFromForm(refreshList: true);
			SetStatus(savedPackage != null ? "Saved manifest." : "Save manifest failed.");
		}

		private CustomMusicScorePackage SaveSelectedManifestFromForm(bool refreshList)
		{
			if (_selected?.Package == null)
			{
				return null;
			}

			CustomMusicScoreManifest manifest = _selected.Package.Manifest;
			manifest.title = _titleInput.text;
			manifest.scoreTitle = _scoreTitleInput.text;
			manifest.userName = _userInput.text;
			manifest.composer = _composerInput.text;
			manifest.lyricist = _lyricistInput.text;
			manifest.arranger = _arrangerInput.text;
			manifest.singer = _singerInput.text;
			manifest.description = _descriptionInput.text;
			manifest.musicDifficultyType = GetSelectedDifficultyType();
			manifest.audioFileName = Path.GetFileName(_audioInput.text);
			manifest.jacketFileName = Path.GetFileName(_jacketInput.text);
			manifest.scoreFileName = Path.GetFileName(_scoreInput.text);
			if (int.TryParse(_levelInput.text, NumberStyles.Integer, CultureInfo.InvariantCulture, out int level))
			{
				manifest.playLevel = level;
			}
			if (int.TryParse(_durationInput.text, NumberStyles.Integer, CultureInfo.InvariantCulture, out int duration))
			{
				manifest.secForMusicScoreMaker = duration;
			}
			if (float.TryParse(_fillerInput.text, NumberStyles.Float, CultureInfo.InvariantCulture, out float filler))
			{
				manifest.fillerSec = filler;
			}

			CustomMusicScorePackage savedPackage = CustomMusicScoreManagerService.SaveManifest(_selected.Package, manifest);
			if (savedPackage != null)
			{
				_selected = new CustomMusicScoreManagerItem(
					savedPackage,
					DateTime.Now,
					File.Exists(savedPackage.ManifestPath),
					File.Exists(savedPackage.ScorePath),
					File.Exists(savedPackage.AudioPath),
					File.Exists(savedPackage.JacketPath));
			}
			if (refreshList)
			{
				RefreshList();
			}
			return savedPackage;
		}

		private void LoadForm(CustomMusicScoreManifest manifest)
		{
			_titleInput.SetTextWithoutNotify(manifest.title);
			_scoreTitleInput.SetTextWithoutNotify(manifest.scoreTitle);
			_userInput.SetTextWithoutNotify(manifest.userName);
			_composerInput.SetTextWithoutNotify(manifest.composer);
			_lyricistInput.SetTextWithoutNotify(manifest.lyricist);
			_arrangerInput.SetTextWithoutNotify(manifest.arranger);
			_singerInput.SetTextWithoutNotify(manifest.singer);
			_descriptionInput.SetTextWithoutNotify(manifest.description);
			SetDifficultyDropdownValue(manifest.musicDifficultyType);
			_levelInput.SetTextWithoutNotify(manifest.playLevel.ToString(CultureInfo.InvariantCulture));
			_durationInput.SetTextWithoutNotify(manifest.secForMusicScoreMaker.ToString(CultureInfo.InvariantCulture));
			_fillerInput.SetTextWithoutNotify(manifest.fillerSec.ToString(CultureInfo.InvariantCulture));
			_audioInput.SetTextWithoutNotify(manifest.audioFileName);
			_jacketInput.SetTextWithoutNotify(manifest.jacketFileName);
			_scoreInput.SetTextWithoutNotify(manifest.scoreFileName);
		}

		private void ClearForm()
		{
			TMP_InputField[] inputs =
			{
				_titleInput, _scoreTitleInput, _userInput, _composerInput, _lyricistInput, _arrangerInput, _singerInput, _descriptionInput, _levelInput,
				_durationInput, _fillerInput, _audioInput, _jacketInput, _scoreInput
			};
			foreach (TMP_InputField input in inputs)
			{
				input.SetTextWithoutNotify(string.Empty);
			}
			SetDifficultyDropdownValue("master");
		}

		private void SetFormInteractable(bool interactable)
		{
			TMP_InputField[] inputs =
			{
				_titleInput, _scoreTitleInput, _userInput, _composerInput, _lyricistInput, _arrangerInput, _singerInput, _descriptionInput, _levelInput,
				_durationInput, _fillerInput, _audioInput, _jacketInput, _scoreInput
			};
			foreach (TMP_InputField input in inputs)
			{
				input.interactable = interactable;
			}
			_difficultyButton.interactable = interactable;
		}

		private string GetSelectedDifficultyType()
		{
			int index = _difficultyIndex;
			if (index < 0 || index >= DifficultyTypes.Length)
			{
				return "master";
			}
			return DifficultyTypes[index];
		}

		private void SetDifficultyDropdownValue(string difficultyType)
		{
			int index = GetDifficultyIndex(difficultyType);
			_difficultyIndex = index;
			if (_difficultyLabel != null)
			{
				_difficultyLabel.text = DifficultyTypes[index].ToUpperInvariant();
			}
		}

		private static int GetDifficultyIndex(string difficultyType)
		{
			for (int i = 0; i < DifficultyTypes.Length; i++)
			{
				if (string.Equals(DifficultyTypes[i], difficultyType, StringComparison.OrdinalIgnoreCase))
				{
					return i;
				}
			}
			return 4;
		}

		private void LoadJacket(string path)
		{
			ClearLoadedJacket();
			if (string.IsNullOrEmpty(path) || !File.Exists(path))
			{
				_jacketImage.sprite = null;
				_jacketImage.color = new Color32(42, 49, 58, 255);
				return;
			}

			byte[] bytes = File.ReadAllBytes(path);
			Texture2D texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
			if (!texture.LoadImage(bytes))
			{
				Destroy(texture);
				return;
			}

			_jacketSprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100f);
			_jacketImage.sprite = _jacketSprite;
			_jacketImage.color = Color.white;
		}

		private void ClearLoadedJacket()
		{
			if (_jacketSprite == null)
			{
				return;
			}

			Texture texture = _jacketSprite.texture;
			Destroy(_jacketSprite);
			if (texture != null)
			{
				Destroy(texture);
			}
			_jacketSprite = null;
			if (_jacketImage != null)
			{
				_jacketImage.sprite = null;
			}
		}

		private void SetStatus(string message)
		{
			if (_statusText != null)
			{
				_statusText.text = message ?? string.Empty;
			}
		}

		private static TMP_InputField CreateInputField(Transform parent, string label, string placeholder)
		{
			Button actionButton;
			return CreateInputField(parent, label, placeholder, null, out actionButton);
		}

		private Button CreateDifficultySelector(Transform parent)
		{
			GameObject root = new GameObject("DifficultyInput", typeof(RectTransform));
			root.transform.SetParent(parent, false);
			LayoutElement layoutElement = root.AddComponent<LayoutElement>();
			layoutElement.preferredHeight = 72f;
			layoutElement.minHeight = 72f;

			VerticalLayoutGroup vertical = root.AddComponent<VerticalLayoutGroup>();
			vertical.spacing = 6f;
			vertical.childControlWidth = true;
			vertical.childControlHeight = false;

			TextMeshProUGUI labelText = CreateText("Label", root.transform, "Difficulty", 16, FontStyles.Bold, TextAlignmentOptions.Left);
			labelText.rectTransform.sizeDelta = new Vector2(0f, 20f);

			GameObject fieldObject = new GameObject("Field", typeof(RectTransform), typeof(Image), typeof(Button));
			fieldObject.transform.SetParent(root.transform, false);
			_difficultyFieldRect = fieldObject.GetComponent<RectTransform>();
			_difficultyFieldRect.sizeDelta = new Vector2(0f, 42f);
			Image fieldImage = fieldObject.GetComponent<Image>();
			fieldImage.color = new Color32(22, 26, 32, 255);

			Button button = fieldObject.GetComponent<Button>();
			button.targetGraphic = fieldImage;
			button.transition = Selectable.Transition.ColorTint;
			ColorBlock colors = button.colors;
			colors.normalColor = Color.white;
			colors.highlightedColor = new Color32(220, 238, 255, 255);
			colors.pressedColor = new Color32(180, 213, 242, 255);
			colors.disabledColor = new Color32(120, 126, 132, 120);
			button.colors = colors;
			button.onClick.AddListener(CycleDifficulty);

			_difficultyLabel = CreateText("Label", _difficultyFieldRect, string.Empty, 18, FontStyles.Normal, TextAlignmentOptions.Left);
			_difficultyLabel.raycastTarget = false;
			SetStretchOffsets(_difficultyLabel.rectTransform, 12f, 0f, 88f, 0f);

			TextMeshProUGUI hint = CreateText("Hint", _difficultyFieldRect, "Cycle", 13, FontStyles.Bold, TextAlignmentOptions.Center);
			hint.raycastTarget = false;
			SetAnchor(hint.rectTransform, new Vector2(1f, 0f), new Vector2(1f, 1f), new Vector2(1f, 0.5f), new Vector2(-8f, 0f), new Vector2(70f, 0f));
			SetDifficultyDropdownValue("master");
			return button;
		}

		private void CycleDifficulty()
		{
			_difficultyIndex = (_difficultyIndex + 1) % DifficultyTypes.Length;
			SetDifficultyDropdownValue(DifficultyTypes[_difficultyIndex]);
		}

		private static TMP_InputField CreateInputField(
			Transform parent,
			string label,
			string placeholder,
			UnityEngine.Events.UnityAction onClick,
			out Button actionButton)
		{
			actionButton = null;
			GameObject root = new GameObject(label + "Input", typeof(RectTransform));
			root.transform.SetParent(parent, false);
			LayoutElement layoutElement = root.AddComponent<LayoutElement>();
			layoutElement.preferredHeight = 72f;
			layoutElement.minHeight = 72f;
			VerticalLayoutGroup vertical = root.AddComponent<VerticalLayoutGroup>();
			vertical.spacing = 6f;
			vertical.childControlWidth = true;
			vertical.childControlHeight = false;

			TextMeshProUGUI labelText = CreateText("Label", root.transform, label, 16, FontStyles.Bold, TextAlignmentOptions.Left);
			labelText.rectTransform.sizeDelta = new Vector2(0f, 20f);

			GameObject fieldObject = new GameObject("Field", typeof(RectTransform), typeof(Image), typeof(TMP_InputField));
			fieldObject.transform.SetParent(root.transform, false);
			RectTransform fieldRect = fieldObject.GetComponent<RectTransform>();
			fieldRect.sizeDelta = new Vector2(0f, 42f);
			fieldObject.GetComponent<Image>().color = new Color32(22, 26, 32, 255);

			RectTransform textArea = CreateRect("Text Area", fieldRect);
			textArea.gameObject.AddComponent<RectMask2D>();
			SetStretchOffsets(textArea, 8f, 4f, onClick == null ? 8f : 82f, 4f);

			TextMeshProUGUI text = CreateText("Text", textArea, string.Empty, 18, FontStyles.Normal, TextAlignmentOptions.Left);
			SetStretchOffsets(text.rectTransform, 4f, 0f, 4f, 0f);
			text.raycastTarget = false;

			TextMeshProUGUI placeholderText = CreateText("Placeholder", textArea, placeholder, 18, FontStyles.Normal, TextAlignmentOptions.Left);
			SetStretchOffsets(placeholderText.rectTransform, 4f, 0f, 4f, 0f);
			placeholderText.color = new Color32(136, 146, 158, 180);
			placeholderText.raycastTarget = false;

			if (onClick != null)
			{
				GameObject actionObject = new GameObject("SelectButton", typeof(RectTransform), typeof(Image), typeof(Button));
				actionObject.transform.SetParent(fieldRect, false);
				RectTransform actionRect = actionObject.GetComponent<RectTransform>();
				SetAnchor(actionRect, new Vector2(1f, 0f), new Vector2(1f, 1f), new Vector2(1f, 0.5f), new Vector2(-4f, 0f), new Vector2(70f, -8f));

				Image actionImage = actionObject.GetComponent<Image>();
				actionImage.color = new Color32(62, 78, 92, 255);
				actionButton = actionObject.GetComponent<Button>();
				actionButton.targetGraphic = actionImage;
				actionButton.transition = Selectable.Transition.ColorTint;
				ColorBlock colors = actionButton.colors;
				colors.normalColor = Color.white;
				colors.highlightedColor = new Color32(220, 238, 255, 255);
				colors.pressedColor = new Color32(180, 213, 242, 255);
				colors.disabledColor = new Color32(120, 126, 132, 120);
				actionButton.colors = colors;
				actionButton.onClick.AddListener(onClick);

				TextMeshProUGUI actionLabel = CreateText("Label", actionRect, "Select", 14, FontStyles.Bold, TextAlignmentOptions.Center);
				actionLabel.enableWordWrapping = false;
				actionLabel.overflowMode = TextOverflowModes.Ellipsis;
				Stretch(actionLabel.rectTransform);
			}

			TMP_InputField input = fieldObject.GetComponent<TMP_InputField>();
			input.textViewport = textArea;
			input.textComponent = text;
			input.placeholder = placeholderText;
			input.targetGraphic = fieldObject.GetComponent<Image>();
			return input;
		}

		private static Button CreateFormButton(Transform parent, string name, string label, UnityEngine.Events.UnityAction onClick)
		{
			GameObject root = new GameObject(name + "Cell", typeof(RectTransform), typeof(LayoutElement));
			root.transform.SetParent(parent, false);
			LayoutElement layoutElement = root.GetComponent<LayoutElement>();
			layoutElement.preferredHeight = 72f;
			layoutElement.minHeight = 72f;

			GameObject buttonObject = new GameObject(name, typeof(RectTransform), typeof(Image), typeof(Button));
			buttonObject.transform.SetParent(root.transform, false);
			RectTransform buttonRect = buttonObject.GetComponent<RectTransform>();
			SetStretchOffsets(buttonRect, 0f, 8f, 0f, 8f);

			Image image = buttonObject.GetComponent<Image>();
			image.color = new Color32(54, 67, 80, 255);
			Button button = buttonObject.GetComponent<Button>();
			button.targetGraphic = image;
			button.transition = Selectable.Transition.ColorTint;
			ColorBlock colors = button.colors;
			colors.normalColor = Color.white;
			colors.highlightedColor = new Color32(220, 238, 255, 255);
			colors.pressedColor = new Color32(180, 213, 242, 255);
			colors.disabledColor = new Color32(120, 126, 132, 120);
			button.colors = colors;
			button.onClick.AddListener(onClick);

			TextMeshProUGUI text = CreateText("Label", buttonRect, label, 18, FontStyles.Bold, TextAlignmentOptions.Center);
			text.enableWordWrapping = false;
			text.overflowMode = TextOverflowModes.Ellipsis;
			Stretch(text.rectTransform);
			return button;
		}

		private static Button CreateButton(string name, Transform parent, string label, UnityEngine.Events.UnityAction onClick, float width, float height, Color32? color = null)
		{
			GameObject root = new GameObject(name, typeof(RectTransform), typeof(Image), typeof(Button), typeof(LayoutElement));
			root.transform.SetParent(parent, false);
			RectTransform rect = root.GetComponent<RectTransform>();
			rect.sizeDelta = new Vector2(width, height);
			LayoutElement layout = root.GetComponent<LayoutElement>();
			layout.preferredWidth = width;
			layout.preferredHeight = height;

			Image image = root.GetComponent<Image>();
			image.color = color ?? new Color32(54, 67, 80, 255);
			Button button = root.GetComponent<Button>();
			button.targetGraphic = image;
			button.transition = Selectable.Transition.ColorTint;
			ColorBlock colors = button.colors;
			colors.normalColor = Color.white;
			colors.highlightedColor = new Color32(220, 238, 255, 255);
			colors.pressedColor = new Color32(180, 213, 242, 255);
			colors.disabledColor = new Color32(120, 126, 132, 120);
			button.colors = colors;
			button.onClick.AddListener(onClick);

			TextMeshProUGUI text = CreateText("Label", rect, label, 18, FontStyles.Bold, TextAlignmentOptions.Center);
			text.enableWordWrapping = false;
			text.overflowMode = TextOverflowModes.Ellipsis;
			Stretch(text.rectTransform);
			return button;
		}

		private static ScrollRect CreateHorizontalScrollRect(string name, Transform parent, out RectTransform content)
		{
			GameObject root = new GameObject(name, typeof(RectTransform), typeof(ScrollRect));
			root.transform.SetParent(parent, false);
			ScrollRect scrollRect = root.GetComponent<ScrollRect>();
			scrollRect.horizontal = true;
			scrollRect.vertical = false;
			scrollRect.movementType = ScrollRect.MovementType.Clamped;

			RectTransform viewport = CreateRect("Viewport", root.transform);
			viewport.gameObject.AddComponent<RectMask2D>();
			Stretch(viewport);

			content = CreateRect("Content", viewport);
			content.anchorMin = new Vector2(0f, 0f);
			content.anchorMax = new Vector2(0f, 1f);
			content.pivot = new Vector2(0f, 0.5f);
			content.anchoredPosition = Vector2.zero;
			content.sizeDelta = Vector2.zero;

			HorizontalLayoutGroup layout = content.gameObject.AddComponent<HorizontalLayoutGroup>();
			layout.spacing = 10f;
			layout.padding = new RectOffset(0, 0, 6, 6);
			layout.childControlWidth = false;
			layout.childControlHeight = false;
			layout.childAlignment = TextAnchor.MiddleLeft;

			ContentSizeFitter fitter = content.gameObject.AddComponent<ContentSizeFitter>();
			fitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
			scrollRect.viewport = viewport;
			scrollRect.content = content;
			return scrollRect;
		}

		private static ScrollRect CreateScrollRect(string name, Transform parent, out RectTransform content)
		{
			GameObject root = new GameObject(name, typeof(RectTransform), typeof(ScrollRect));
			root.transform.SetParent(parent, false);
			ScrollRect scrollRect = root.GetComponent<ScrollRect>();

			Image viewportImage = CreateImage("Viewport", root.transform, new Color32(20, 24, 30, 255));
			Mask mask = viewportImage.gameObject.AddComponent<Mask>();
			mask.showMaskGraphic = false;
			RectTransform viewport = viewportImage.rectTransform;
			Stretch(viewport);

			content = CreateRect("Content", viewport);
			content.anchorMin = new Vector2(0f, 1f);
			content.anchorMax = new Vector2(1f, 1f);
			content.pivot = new Vector2(0.5f, 1f);
			content.anchoredPosition = Vector2.zero;
			content.sizeDelta = Vector2.zero;
			VerticalLayoutGroup layout = content.gameObject.AddComponent<VerticalLayoutGroup>();
			layout.spacing = 10f;
			layout.padding = new RectOffset(8, 8, 8, 8);
			layout.childControlWidth = true;
			layout.childControlHeight = false;
			ContentSizeFitter fitter = content.gameObject.AddComponent<ContentSizeFitter>();
			fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

			scrollRect.viewport = viewport;
			scrollRect.content = content;
			scrollRect.horizontal = false;
			scrollRect.vertical = true;
			scrollRect.movementType = ScrollRect.MovementType.Clamped;
			return scrollRect;
		}

		private static ScrollRect CreateMaskedScrollRect(string name, Transform parent, out RectTransform content)
		{
			GameObject root = new GameObject(name, typeof(RectTransform), typeof(Image), typeof(ScrollRect));
			root.transform.SetParent(parent, false);
			root.GetComponent<Image>().color = new Color32(24, 29, 36, 255);
			ScrollRect scrollRect = root.GetComponent<ScrollRect>();

			RectTransform viewport = CreateRect("Viewport", root.transform);
			viewport.gameObject.AddComponent<RectMask2D>();
			Stretch(viewport);

			content = CreateRect("Content", viewport);
			content.anchorMin = new Vector2(0f, 1f);
			content.anchorMax = new Vector2(1f, 1f);
			content.pivot = new Vector2(0.5f, 1f);
			content.anchoredPosition = Vector2.zero;
			content.sizeDelta = new Vector2(0f, 430f);

			scrollRect.viewport = viewport;
			scrollRect.content = content;
			scrollRect.horizontal = false;
			scrollRect.vertical = true;
			scrollRect.movementType = ScrollRect.MovementType.Clamped;
			return scrollRect;
		}

		private static RectTransform CreatePanel(string name, Transform parent, Color32 color)
		{
			Image image = CreateImage(name, parent, color);
			return image.rectTransform;
		}

		private static Image CreateImage(string name, Transform parent, Color32 color)
		{
			GameObject go = new GameObject(name, typeof(RectTransform), typeof(Image));
			go.transform.SetParent(parent, false);
			Image image = go.GetComponent<Image>();
			image.color = color;
			return image;
		}

		private static TextMeshProUGUI CreateText(string name, Transform parent, string text, float fontSize, FontStyles style, TextAlignmentOptions alignment)
		{
			GameObject go = new GameObject(name, typeof(RectTransform), typeof(TextMeshProUGUI));
			go.transform.SetParent(parent, false);
			TextMeshProUGUI tmp = go.GetComponent<TextMeshProUGUI>();
			TMP_FontAsset fontAsset = GetOriginalFontAsset(style);
			if (fontAsset != null)
			{
				tmp.font = fontAsset;
			}
			tmp.text = text;
			tmp.fontSize = fontSize;
			tmp.fontStyle = style;
			tmp.alignment = alignment;
			tmp.color = new Color32(238, 243, 247, 255);
			tmp.enableWordWrapping = false;
			tmp.overflowMode = TextOverflowModes.Ellipsis;
			return tmp;
		}

		private static TMP_FontAsset GetOriginalFontAsset(FontStyles style)
		{
			SetupOriginalFontAssets();
			return (style & FontStyles.Bold) != 0 ? _baseFontEB : _baseFontDB;
		}

		private static void SetupOriginalFontAssets()
		{
			if (_fontAssetSetup)
			{
				return;
			}

			_baseFontEB = Resources.Load<TMP_FontAsset>(BaseFontEbPath);
			_baseFontDB = Resources.Load<TMP_FontAsset>(BaseFontDbPath);
			TMP_FontAsset dynamicFontEB = Resources.Load<TMP_FontAsset>(DynamicFontEbPath);
			TMP_FontAsset dynamicFontDB = Resources.Load<TMP_FontAsset>(DynamicFontDbPath);
			AddFallbackFontAsset(_baseFontEB, dynamicFontEB);
			AddFallbackFontAsset(_baseFontDB, dynamicFontDB);
			_fontAssetSetup = true;
		}

		private static void AddFallbackFontAsset(TMP_FontAsset fontAsset, TMP_FontAsset fallbackFontAsset)
		{
			if (fontAsset == null || fallbackFontAsset == null)
			{
				return;
			}

			if (fontAsset.fallbackFontAssetTable == null)
			{
				fontAsset.fallbackFontAssetTable = new List<TMP_FontAsset>();
			}

			if (!fontAsset.fallbackFontAssetTable.Contains(fallbackFontAsset))
			{
				fontAsset.fallbackFontAssetTable.Add(fallbackFontAsset);
			}
		}

		private static RectTransform CreateRect(string name, Transform parent)
		{
			GameObject go = new GameObject(name, typeof(RectTransform));
			go.transform.SetParent(parent, false);
			return go.GetComponent<RectTransform>();
		}

		private static void Stretch(RectTransform rect)
		{
			SetAnchor(rect, Vector2.zero, Vector2.one, new Vector2(0.5f, 0.5f), Vector2.zero, Vector2.zero);
		}

		private static void SetStretchOffsets(RectTransform rect, float left, float bottom, float right, float top)
		{
			rect.anchorMin = Vector2.zero;
			rect.anchorMax = Vector2.one;
			rect.pivot = new Vector2(0.5f, 0.5f);
			rect.offsetMin = new Vector2(left, bottom);
			rect.offsetMax = new Vector2(-right, -top);
		}

		private static void SetStretchTop(RectTransform rect, float left, float top, float right, float height)
		{
			rect.anchorMin = new Vector2(0f, 1f);
			rect.anchorMax = new Vector2(1f, 1f);
			rect.pivot = new Vector2(0.5f, 1f);
			rect.offsetMin = new Vector2(left, -top - height);
			rect.offsetMax = new Vector2(-right, -top);
		}

		private static void SetStretchBottom(RectTransform rect, float left, float bottom, float right, float height)
		{
			rect.anchorMin = new Vector2(0f, 0f);
			rect.anchorMax = new Vector2(1f, 0f);
			rect.pivot = new Vector2(0.5f, 0f);
			rect.offsetMin = new Vector2(left, bottom);
			rect.offsetMax = new Vector2(-right, bottom + height);
		}

		private static void SetAnchor(RectTransform rect, Vector2 anchorMin, Vector2 anchorMax, Vector2 pivot, Vector2 anchoredPosition, Vector2 sizeDelta)
		{
			rect.anchorMin = anchorMin;
			rect.anchorMax = anchorMax;
			rect.pivot = pivot;
			rect.anchoredPosition = anchoredPosition;
			rect.sizeDelta = sizeDelta;
		}

		private sealed class RowView
		{
			public GameObject Root { get; }

			public Image Background { get; }

			public CustomMusicScoreManagerItem Item { get; }

			public RowView(GameObject root, Image background, CustomMusicScoreManagerItem item)
			{
				Root = root;
				Background = background;
				Item = item;
			}
		}
	}
}
