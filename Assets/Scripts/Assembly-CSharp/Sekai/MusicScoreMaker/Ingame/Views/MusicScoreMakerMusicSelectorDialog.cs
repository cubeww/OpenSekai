using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sekai.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Sekai.MusicScoreMaker.Ingame.Views
{
	public class MusicScoreMakerMusicSelectorDialog : Common2ButtonDialog
	{
		[SerializeField]
		[FormerlySerializedAs("musicDropdown")]
		private CustomDropdown _musicDropdown;

		[SerializeField]
		[FormerlySerializedAs("difficultyDropdown")]
		private CustomDropdown _difficultyDropdown;

		[FormerlySerializedAs("vocalDropdown")]
		[SerializeField]
		private CustomDropdown _vocalDropdown;

		[SerializeField]
		private CustomButton _selectScoreButton;

		[SerializeField]
		private CustomButton _selectAcbButton;

		[SerializeField]
		private CustomInputFieldTextMesh _fillerTimeInputField;

		private List<MasterMusicAll> _musicList;

		public float FillerSec
		{
			get
			{
				return _fillerTimeInputField != null && float.TryParse(_fillerTimeInputField.text, out float result) ? result : 0f;
			}
		}

		public void Setup(int modelMusicId, string modelDifficulty, int modelVocalId)
		{
			_musicList = LoadMusicListFromMasterDataManager();
			if (_musicDropdown != null)
			{
				_musicDropdown.onValueChanged.RemoveListener(OnChangeMusic);
				_musicDropdown.onValueChanged.AddListener(OnChangeMusic);
			}
			if (_musicList.Count > 0)
			{
				_musicList = _musicList.OrderByDescending(m => m?.id ?? 0).ToList();
				if (_musicDropdown != null)
				{
					_musicDropdown.options = _musicList.Select(CreateMusicOption).ToList();
				}
				int musicIndex = Math.Max(0, _musicList.FindIndex(m => m?.music != null && m.music.id == modelMusicId));
				if (_musicDropdown != null)
				{
					_musicDropdown.SetValueWithoutNotify(musicIndex);
				}
				MasterMusicAll music = GetMusicAt(musicIndex);
				int vocalIndex = music?.musicVocals == null ? 0 : Math.Max(0, Array.FindIndex(music.musicVocals, v => v != null && v.id == modelVocalId));
				int difficultyIndex = music?.musicDifficulties == null ? 0 : Math.Max(0, Array.FindIndex(music.musicDifficulties, d => string.Equals(d?.musicDifficulty, modelDifficulty, StringComparison.OrdinalIgnoreCase)));
				OnChangeMusic(musicIndex, vocalIndex, difficultyIndex);
			}
			else
			{
				// TODO(original): remove this fallback once MasterDataManager is restored/copied.
				OnChangeMusic(_musicDropdown != null ? _musicDropdown.value : 0);
			}
			SwitchUIVisibility(false);
		}

		private void SwitchUIVisibility(bool value)
		{
			SetParentActive(_musicDropdown, !value);
			SetParentActive(_difficultyDropdown, !value);
			SetParentActive(_vocalDropdown, !value);
			if (_selectScoreButton != null)
			{
				_selectScoreButton.gameObject.SetActive(value);
			}
			if (_selectAcbButton != null)
			{
				_selectAcbButton.gameObject.SetActive(value);
			}
			if (_fillerTimeInputField != null)
			{
				_fillerTimeInputField.gameObject.SetActive(value);
			}
		}

		private void OnChangeMusic(int musicIndex)
		{
			OnChangeMusic(musicIndex, _vocalDropdown != null ? _vocalDropdown.value : 0, _difficultyDropdown != null ? _difficultyDropdown.value : 0);
		}

		private void OnChangeMusic(int musicIndex, int selectedVocalValue, int selectedDifficultyValue)
		{
			MasterMusicAll music = GetMusicAt(musicIndex);
			if (music == null)
			{
				return;
			}
			if (_vocalDropdown != null)
			{
				_vocalDropdown.options = (music.musicVocals ?? Array.Empty<MasterMusicVocal>()).Select(CreateVocalOption).ToList();
				_vocalDropdown.SetValueWithoutNotify(ClampOptionIndex(_vocalDropdown, selectedVocalValue));
			}
			if (_difficultyDropdown != null)
			{
				_difficultyDropdown.options = (music.musicDifficulties ?? Array.Empty<MasterMusicDifficulty>()).Select(CreateDifficultyOption).ToList();
				_difficultyDropdown.SetValueWithoutNotify(ClampOptionIndex(_difficultyDropdown, selectedDifficultyValue));
			}
		}

		public int GetSelectedMusicId()
		{
			MasterMusicAll music = GetMusicAt(_musicDropdown != null ? _musicDropdown.value : 0);
			if (music?.music != null)
			{
				return music.music.id;
			}
			Debug.LogError("Failed to resolve selected music id.");
			return 0;
		}

		public string GetSelectedDifficulty()
		{
			if (_difficultyDropdown != null && _difficultyDropdown.Contains(_difficultyDropdown.value))
			{
				return _difficultyDropdown.options[_difficultyDropdown.value].text.ToLowerInvariant();
			}
			Debug.LogError("Failed to resolve selected difficulty.");
			return string.Empty;
		}

		public int GetSelectedVocalId()
		{
			MasterMusicAll music = GetMusicAt(_musicDropdown != null ? _musicDropdown.value : 0);
			int vocalIndex = _vocalDropdown != null ? _vocalDropdown.value : 0;
			if (music?.musicVocals != null && vocalIndex >= 0 && vocalIndex < music.musicVocals.Length && music.musicVocals[vocalIndex] != null)
			{
				return music.musicVocals[vocalIndex].id;
			}
			Debug.LogError("Failed to resolve selected vocal id.");
			return 0;
		}

		public MusicScoreMakerMusicSelectorDialog()
		{
			_musicList = new List<MasterMusicAll>();
		}

		private static void SetParentActive(Component component, bool active)
		{
			if (component != null && component.transform.parent != null)
			{
				component.transform.parent.gameObject.SetActive(active);
			}
		}

		private MasterMusicAll GetMusicAt(int index)
		{
			return _musicList != null && index >= 0 && index < _musicList.Count ? _musicList[index] : null;
		}

		private static int ClampOptionIndex(TMP_Dropdown dropdown, int index)
		{
			if (dropdown == null || dropdown.options == null || dropdown.options.Count == 0)
			{
				return 0;
			}
			return Mathf.Clamp(index, 0, dropdown.options.Count - 1);
		}

		private static TMP_Dropdown.OptionData CreateMusicOption(MasterMusicAll musicAll)
		{
			MasterMusic music = musicAll?.music;
			return new TMP_Dropdown.OptionData(music == null ? string.Empty : string.Format("{0}: {1}", music.id, music.title));
		}

		private static TMP_Dropdown.OptionData CreateVocalOption(MasterMusicVocal vocal)
		{
			return new TMP_Dropdown.OptionData(vocal == null ? string.Empty : string.Concat(vocal.assetbundleName, " ", vocal.caption));
		}

		private static TMP_Dropdown.OptionData CreateDifficultyOption(MasterMusicDifficulty difficulty)
		{
			return new TMP_Dropdown.OptionData((difficulty?.musicDifficulty ?? string.Empty).ToUpperInvariant());
		}

		private static List<MasterMusicAll> LoadMusicListFromMasterDataManager()
		{
			Type managerType = AppDomain.CurrentDomain.GetAssemblies()
				.Select(assembly => assembly.GetType("Sekai.MasterDataManager"))
				.FirstOrDefault(type => type != null);
			if (managerType == null)
			{
				return new List<MasterMusicAll>();
			}
			object instance = managerType.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)?.GetValue(null);
			MethodInfo getMap = managerType.GetMethod("GetMasterMusicAllMap", BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
			object map = getMap?.Invoke(getMap.IsStatic ? null : instance, null);
			if (map is IDictionary dictionary)
			{
				return dictionary.Values.OfType<MasterMusicAll>().ToList();
			}
			if (map is IEnumerable enumerable)
			{
				List<MasterMusicAll> result = new List<MasterMusicAll>();
				foreach (object item in enumerable)
				{
					if (item is MasterMusicAll music)
					{
						result.Add(music);
						continue;
					}
					object value = item?.GetType().GetProperty("Value")?.GetValue(item);
					if (value is MasterMusicAll musicValue)
					{
						result.Add(musicValue);
					}
				}
				return result;
			}
			return new List<MasterMusicAll>();
		}
	}
}
