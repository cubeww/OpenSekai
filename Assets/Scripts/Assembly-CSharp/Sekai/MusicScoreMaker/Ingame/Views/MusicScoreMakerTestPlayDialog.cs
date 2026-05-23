using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using Sekai.MusicScoreMaker.Ingame.Utilities;
using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.Ingame.Views
{
	public class MusicScoreMakerTestPlayDialog : Common2ButtonDialog
	{
		[SerializeField]
		private CustomToggle _enableSetStartMusicTimeMsToggle;

		[SerializeField]
		private CustomToggle _autoPlayToggle;

		[SerializeField]
		private LiveModeButton _liveModeButton;

		private bool _isMidStartMvForced;

		private LiveSettingData.LiveModeType _savedLiveModeTypeBeforeForce;

		public void Setup(int musicId)
		{
			if (_enableSetStartMusicTimeMsToggle != null)
			{
				_enableSetStartMusicTimeMsToggle.onValueChanged.RemoveAllListeners();
				_enableSetStartMusicTimeMsToggle.isOn = MusicScoreMakerSettingsManager.SetStartMusicTimeMsEnabled;
				_enableSetStartMusicTimeMsToggle.onValueChanged.AddListener(value => MusicScoreMakerSettingsManager.SetStartMusicTimeMsEnabled = value);
				_enableSetStartMusicTimeMsToggle.onValueChanged.AddListener(OnMidStartToggleChanged);
			}
			if (_autoPlayToggle != null)
			{
				_autoPlayToggle.onValueChanged.RemoveAllListeners();
				_autoPlayToggle.isOn = MusicScoreMakerSettingsManager.AutoPlayEnabled;
				_autoPlayToggle.onValueChanged.AddListener(value => MusicScoreMakerSettingsManager.AutoPlayEnabled = value);
			}
			if (_liveModeButton != null)
			{
				MasterMusicAll musicAll = LoadMasterMusicAll(musicId);
				// TODO(original): remove reflection fallback after MasterDataManager is copied.
				_liveModeButton.SetupLiveMode(musicAll, true);
				if (MusicScoreMakerSettingsManager.TryGetTestPlayLiveModeType(out LiveSettingData.LiveModeType liveModeType))
				{
					_liveModeButton.SelectLiveMode(liveModeType);
				}
				if (_enableSetStartMusicTimeMsToggle != null && _enableSetStartMusicTimeMsToggle.isOn)
				{
					ForceToLowMvMode();
				}
				else
				{
					_isMidStartMvForced = false;
					_liveModeButton.RefreshChangeButtonEnabled();
				}
			}
		}

		public LiveSettingData.LiveModeType GetSelectedLiveModeType()
		{
			return _liveModeButton != null ? _liveModeButton.Get() : LiveSettingData.LiveModeType.Low;
		}

		public MusicCategory GetSelectedMusicCategory()
		{
			return MusicScoreMakerUtility.ConvertLiveModeTypeToMusicCategory(GetSelectedLiveModeType());
		}

		private void OnMidStartToggleChanged(bool isOn)
		{
			if (isOn)
			{
				ForceToLowMvMode();
			}
			else
			{
				RestoreMvMode();
			}
		}

		private void ForceToLowMvMode()
		{
			if (_liveModeButton == null || _isMidStartMvForced)
			{
				return;
			}
			_savedLiveModeTypeBeforeForce = _liveModeButton.Get();
			_isMidStartMvForced = true;
			_liveModeButton.SelectLiveMode(LiveSettingData.LiveModeType.Low);
			_liveModeButton.SetChangeButtonEnabled(false);
		}

		private void RestoreMvMode()
		{
			if (_liveModeButton == null)
			{
				return;
			}
			if (_isMidStartMvForced)
			{
				_liveModeButton.SelectLiveMode(_savedLiveModeTypeBeforeForce);
			}
			_isMidStartMvForced = false;
			_liveModeButton.RefreshChangeButtonEnabled();
		}

		protected override void OnClickOK()
		{
			ApplyData();
			base.OnClickOK();
		}

		private void ApplyData()
		{
			if (_enableSetStartMusicTimeMsToggle != null)
			{
				MusicScoreMakerSettingsManager.SetStartMusicTimeMsEnabled = _enableSetStartMusicTimeMsToggle.isOn;
			}
			if (_autoPlayToggle != null)
			{
				MusicScoreMakerSettingsManager.AutoPlayEnabled = _autoPlayToggle.isOn;
			}
			if (_liveModeButton != null && !_isMidStartMvForced)
			{
				MusicScoreMakerSettingsManager.SetTestPlayLiveModeType(_liveModeButton.Get());
			}
			MusicScoreMakerSettingsManager.SaveSettingData();
		}

		public MusicScoreMakerTestPlayDialog()
		{
		}

		private static MasterMusicAll LoadMasterMusicAll(int musicId)
		{
			Type managerType = AppDomain.CurrentDomain.GetAssemblies()
				.Select(assembly => assembly.GetType("Sekai.MasterDataManager"))
				.FirstOrDefault(type => type != null);
			if (managerType == null)
			{
				return null;
			}
			object instance = managerType.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)?.GetValue(null);
			MethodInfo getMusic = managerType.GetMethod("GetMasterMusicAll", BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
			if (getMusic != null)
			{
				return getMusic.Invoke(getMusic.IsStatic ? null : instance, new object[] { musicId }) as MasterMusicAll;
			}
			MethodInfo getMap = managerType.GetMethod("GetMasterMusicAllMap", BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
			object map = getMap?.Invoke(getMap.IsStatic ? null : instance, null);
			if (map is IDictionary dictionary)
			{
				return dictionary.Values.OfType<MasterMusicAll>().FirstOrDefault(m => m?.music != null && m.music.id == musicId);
			}
			return null;
		}
	}
}
