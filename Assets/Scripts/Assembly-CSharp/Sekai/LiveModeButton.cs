using System.Collections.Generic;
using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	public class LiveModeButton : MonoBehaviour
	{
		[SerializeField]
		private CustomButton changeButton;

		[SerializeField]
		private CustomTextMesh modeText;

		private int index;

		private LiveSettingData settings;

		private bool isUpdated;

		private bool selectableSettingMode;

		private MasterMusicAll musicAll;

		private List<LiveSettingData.LiveModeType> modes;

		private void Awake()
		{
			if (changeButton != null)
			{
				changeButton.onClick.AddListener(UpdateMode);
			}
		}

		public void SetupLiveMode(MasterMusicAll musicAll, bool isCheckParameter = false)
		{
			InitializeLiveMode(musicAll, isCheckParameter);
		}

		public void Setup(MasterMusicAll musicAll, bool isMVOnly)
		{
			InitializeMode(musicAll, isMVOnly);
		}

		private void InitializeLiveMode(MasterMusicAll nextMusicAll, bool isCheckParameter)
		{
			InitializeMode(nextMusicAll, false);
		}

		private void InitializeMode(MasterMusicAll musicAll, bool isMvOnly)
		{
			this.musicAll = musicAll;
			selectableSettingMode = true;
			modes = CreateSelectableModes(musicAll, isMvOnly);
			LiveSettingData.LiveModeType initialMode = GetPriorityLiveModeType(musicAll?.music);
			index = Mathf.Max(0, modes.IndexOf(initialMode));
			RefreshChangeButtonEnabled();
			RefreshText();
		}

		private LiveSettingData.LiveModeType GetPriorityLiveModeType(MasterMusic masterMusic)
		{
			if (settings != null && modes != null && modes.Contains(settings.LiveMode))
			{
				return settings.LiveMode;
			}
			return modes != null && modes.Count > 0 ? modes[0] : LiveSettingData.LiveModeType.Low;
		}

		private void UpdateMode()
		{
			if (modes == null || modes.Count == 0)
			{
				return;
			}
			index = (index + 1) % modes.Count;
			isUpdated = true;
			RefreshText();
		}

		public void SelectLiveMode(LiveSettingData.LiveModeType liveModeType)
		{
			if (modes == null || modes.Count == 0)
			{
				modes = new List<LiveSettingData.LiveModeType> { liveModeType };
			}
			int found = modes.IndexOf(liveModeType);
			if (found < 0)
			{
				modes.Add(liveModeType);
				found = modes.Count - 1;
			}
			index = found;
			isUpdated = true;
			RefreshText();
		}

		public void SetupFixedModes(params LiveSettingData.LiveModeType[] selectableModes)
		{
			selectableSettingMode = true;
			modes = new List<LiveSettingData.LiveModeType>();
			if (selectableModes != null)
			{
				for (int i = 0; i < selectableModes.Length; i++)
				{
					LiveSettingData.LiveModeType mode = selectableModes[i];
					if (!modes.Contains(mode))
					{
						modes.Add(mode);
					}
				}
			}
			if (modes.Count == 0)
			{
				modes.Add(LiveSettingData.LiveModeType.Low);
			}
			index = 0;
			isUpdated = false;
			RefreshChangeButtonEnabled();
			RefreshText();
		}

		public void SetChangeButtonEnabled(bool enabled)
		{
			selectableSettingMode = enabled;
			if (changeButton != null)
			{
				changeButton.enabled = enabled;
			}
		}

		public void RefreshChangeButtonEnabled()
		{
			if (changeButton != null)
			{
				changeButton.enabled = modes != null && modes.Count > 1;
			}
		}

		public LiveSettingData.LiveModeType SaveChanges()
		{
			LiveSettingData.LiveModeType selected = Get();
			if (settings != null)
			{
				settings.LiveMode = selected;
			}
			isUpdated = false;
			return selected;
		}

		public LiveSettingData.LiveModeType Get()
		{
			if (modes == null || modes.Count == 0)
			{
				return LiveSettingData.LiveModeType.Low;
			}
			return modes[Mathf.Clamp(index, 0, modes.Count - 1)];
		}

		public LiveModeButton()
		{
			modes = new List<LiveSettingData.LiveModeType>();
			selectableSettingMode = true;
		}

		private static List<LiveSettingData.LiveModeType> CreateSelectableModes(MasterMusicAll musicAll, bool isMvOnly)
		{
			List<LiveSettingData.LiveModeType> result = new List<LiveSettingData.LiveModeType>();
			LiveSettingData liveSetting = LiveSettingData.LoadFromStorage();
			string[] categories = musicAll?.music?.categories;
			bool hasMv = categories == null || categories.Length == 0 || System.Array.Exists(categories, x => x == "mv");
			bool has2d = categories != null && System.Array.Exists(categories, x => x == "mv_2d");
			bool hasOriginal = categories != null && System.Array.Exists(categories, x => x == "original");
			if (hasMv)
			{
				result.Add(liveSetting != null && liveSetting.QualityType != MVQualityType.Default
					? LiveSettingData.LiveModeType.High3D
					: LiveSettingData.LiveModeType.Default3D);
			}
			if (has2d)
			{
				result.Add(LiveSettingData.LiveModeType.Mode2D);
			}
			if (!isMvOnly)
			{
				result.Add(LiveSettingData.LiveModeType.Low);
			}
			if (hasOriginal)
			{
				result.Add(LiveSettingData.LiveModeType.OriginalMV);
			}
			if (result.Count == 0)
			{
				result.Add(LiveSettingData.LiveModeType.Low);
			}
			return result;
		}

		private void RefreshText()
		{
			if (modeText != null)
			{
				modeText.SetWordingText(LiveSettingData.GetLiveModeTextKey(Get()));
			}
		}
	}
}
