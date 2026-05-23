using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sekai.Live;
using Sekai.MusicScoreMaker.Ingame.Events;
using Sekai.MusicScoreMaker.Ingame.Models;
using Sekai.MusicScoreMaker.Ingame.Utilities;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Sekai.MusicScoreMaker.Ingame.Views
{
	public class NoteChangeButtonView : MonoBehaviour
	{
		private class ButtonInfo
		{
			public GameObject GameObject;

			public NoteVariation Variation;

			public Text Text;

			public SelectedNoteDataButton SelectedNoteDataButton
			{
				[CompilerGenerated]
				get;
				[CompilerGenerated]
				set;
			}

			public ButtonInfo()
			{
			}
		}

		[FormerlySerializedAs("buttonPrefab")]
		[SerializeField]
		private SelectedNoteDataButton _buttonPrefab;

		[FormerlySerializedAs("buttonParent")]
		[SerializeField]
		private Transform _buttonParent;

		private readonly Dictionary<NoteGroupType, List<ButtonInfo>> _cachedButtons;

		private NoteGroupType _currentGroupType;

		private bool _isInitialized;

		private void OnDestroy()
		{
			Dispose();
		}

		public void Initialize()
		{
			if (_isInitialized)
			{
				return;
			}

			gameObject.SetActive(true);
			PreGenerateAllButtons();
			SetupEventDispatcher();
			_isInitialized = true;
		}

		private void Dispose()
		{
			if (!_isInitialized)
			{
				return;
			}

			DisposeEventDispatcher();
			ClearAllButtons();
			_isInitialized = false;
		}

		private void PreGenerateAllButtons()
		{
			if (_buttonPrefab == null || _buttonParent == null)
			{
				return;
			}

			ClearAllButtons();
			NoteGroupType[] groupTypes =
			{
				NoteGroupType.Single,
				NoteGroupType.LongStart,
				NoteGroupType.Guide,
				NoteGroupType.LongEnd,
				NoteGroupType.CriticalLongEnd,
				NoteGroupType.Connection
			};

			foreach (NoteGroupType groupType in groupTypes)
			{
				List<ButtonInfo> buttonInfos = new List<ButtonInfo>();
				List<NoteVariation> variations = NoteGroupUtility.GetAvailableVariations(groupType, 0, 0, 0);
				for (int i = 0; i < variations.Count; i++)
				{
					ButtonInfo buttonInfo = CreateButtonInfo(variations[i], groupType);
					if (buttonInfo == null)
					{
						continue;
					}
					buttonInfo.GameObject.SetActive(false);
					buttonInfos.Add(buttonInfo);
				}
				_cachedButtons[groupType] = buttonInfos;
			}
		}

		private ButtonInfo CreateButtonInfo(NoteVariation variation, NoteGroupType groupType)
		{
			if (_buttonPrefab == null || _buttonParent == null || variation == null)
			{
				return null;
			}

			SelectedNoteDataButton selectedNoteDataButton = Instantiate(_buttonPrefab, _buttonParent);
			if (selectedNoteDataButton == null)
			{
				return null;
			}

			selectedNoteDataButton.SetData(SelectedNoteDataButton.ButtonType.Change, variation.Category, variation.Direction, variation.Type, variation.LineType, variation.IsSkip, groupType);
			return new ButtonInfo
			{
				GameObject = selectedNoteDataButton.gameObject,
				Variation = variation,
				Text = selectedNoteDataButton.GetComponentInChildren<Text>(true),
				SelectedNoteDataButton = selectedNoteDataButton
			};
		}

		private void SetupEventDispatcher()
		{
			MusicScoreMakerEventDispatcher dispatcher = MusicScoreMakerEventDispatcher.Instance;
			dispatcher.Register<ShowNoteChangeButtonsEvent>(OnShowNoteChangeButtons);
			dispatcher.Register<ShowClipboardCacheListEvent>(OnShowClipboardCacheList);
			dispatcher.Register<SwitchCriticalFilterEvent>(OnCriticalFilterChanged);
		}

		private void DisposeEventDispatcher()
		{
			MusicScoreMakerEventDispatcher dispatcher = MusicScoreMakerEventDispatcher.Instance;
			dispatcher.Remove<ShowNoteChangeButtonsEvent>(OnShowNoteChangeButtons);
			dispatcher.Remove<ShowClipboardCacheListEvent>(OnShowClipboardCacheList);
			dispatcher.Remove<SwitchCriticalFilterEvent>(OnCriticalFilterChanged);
		}

		private void OnShowNoteChangeButtons(ShowNoteChangeButtonsEvent eventData)
		{
			if (eventData == null)
			{
				return;
			}

			SetSelectNote(eventData.SelectedNotes);
			ShowButtonsForGroupType(eventData.GroupType);
		}

		private void SetSelectNote(MusicScoreNoteBase[] eventDataSelectedNotes)
		{
			foreach (List<ButtonInfo> buttonInfos in _cachedButtons.Values)
			{
				for (int i = 0; i < buttonInfos.Count; i++)
				{
					SelectedNoteDataButton button = buttonInfos[i].SelectedNoteDataButton;
					if (button == null)
					{
						continue;
					}

					bool isSelected = false;
					if (eventDataSelectedNotes != null)
					{
						for (int j = 0; j < eventDataSelectedNotes.Length; j++)
						{
							if (button.IsSameNote(eventDataSelectedNotes[j]))
							{
								isSelected = true;
								break;
							}
						}
					}
					button.SetSelected(isSelected);
				}
			}
		}

		private void OnShowClipboardCacheList(ShowClipboardCacheListEvent eventData)
		{
			HideCurrentButtons();
		}

		private void ShowButtonsForGroupType(NoteGroupType groupType)
		{
			HideCurrentButtons();
			_currentGroupType = groupType;
			if (!_cachedButtons.TryGetValue(groupType, out List<ButtonInfo> buttonInfos))
			{
				return;
			}

			for (int i = 0; i < buttonInfos.Count; i++)
			{
				ButtonInfo buttonInfo = buttonInfos[i];
				if (buttonInfo.GameObject != null)
				{
					buttonInfo.GameObject.SetActive(ShouldShowButton(buttonInfo.Variation, groupType));
				}
			}
		}

		private void OnCriticalFilterChanged(SwitchCriticalFilterEvent eventData)
		{
			ShowButtonsForGroupType(_currentGroupType);
		}

		private bool ShouldShowButton(NoteVariation variation, NoteGroupType groupType)
		{
			if (variation == null)
			{
				return false;
			}
			if (groupType == NoteGroupType.CriticalLongEnd)
			{
				return variation.Type == NoteType.Critical;
			}

			bool isCriticalFilterEnabled = MusicScoreMakerEventDispatcher.Instance.PublishFirst<IsCriticalFilterEnabledEvent, bool>(new IsCriticalFilterEnabledEvent());
			return isCriticalFilterEnabled ? variation.Type == NoteType.Critical : variation.Type != NoteType.Critical;
		}

		private void HideCurrentButtons()
		{
			if (!_cachedButtons.TryGetValue(_currentGroupType, out List<ButtonInfo> buttonInfos))
			{
				return;
			}
			for (int i = 0; i < buttonInfos.Count; i++)
			{
				if (buttonInfos[i].GameObject != null)
				{
					buttonInfos[i].GameObject.SetActive(false);
				}
			}
		}

		private void ClearAllButtons()
		{
			foreach (List<ButtonInfo> buttonInfos in _cachedButtons.Values)
			{
				for (int i = 0; i < buttonInfos.Count; i++)
				{
					if (buttonInfos[i].GameObject != null)
					{
						Destroy(buttonInfos[i].GameObject);
					}
				}
			}
			_cachedButtons.Clear();
		}

		public NoteChangeButtonView()
		{
			_cachedButtons = new Dictionary<NoteGroupType, List<ButtonInfo>>();
		}
	}
}
