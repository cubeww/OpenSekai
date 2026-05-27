using Sekai.Live;
using Sekai.MusicScoreMaker.Ingame.Events;
using Sekai.MusicScoreMaker.Ingame.Models;
using Sekai.MusicScoreMaker.Ingame.Utilities;
using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.Ingame.Views
{
	public class CriticalFilterToggleView : MonoBehaviour
	{
		[SerializeField]
		private CustomToggle _toggle;

		public void SetActive(bool isActive)
		{
			gameObject.SetActive(isActive);
		}

		private void Awake()
		{
			Setup();
		}

		private void OnDestroy()
		{
			Dispose();
		}

		public void Setup()
		{
			bool isEnabled = MusicScoreMakerEventDispatcher.Instance.PublishFirst<IsCriticalFilterEnabledEvent, bool>(new IsCriticalFilterEnabledEvent());
			if (_toggle != null)
			{
				_toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
				_toggle.SetIsOnWithoutNotify(isEnabled);
				_toggle.onValueChanged.AddListener(OnToggleValueChanged);
			}
			SetupEventDispatcher();
			gameObject.SetActive(false);
		}

		public void Dispose()
		{
			if (_toggle != null)
			{
				_toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
			}
			DisposeEventDispatcher();
		}

		private void SetupEventDispatcher()
		{
			MusicScoreMakerEventDispatcher dispatcher = MusicScoreMakerEventDispatcher.Instance;
			dispatcher.Remove<SwitchCriticalFilterEvent>(OnCriticalFilterChanged);
			dispatcher.Remove<ShowNoteChangeButtonsEvent>(OnShowNoteChangeButtons);
			dispatcher.Register<SwitchCriticalFilterEvent>(OnCriticalFilterChanged);
			dispatcher.Register<ShowNoteChangeButtonsEvent>(OnShowNoteChangeButtons);
		}

		private void DisposeEventDispatcher()
		{
			if (!MusicScoreMakerEventDispatcher.ExistsInstance)
			{
				return;
			}
			MusicScoreMakerEventDispatcher dispatcher = MusicScoreMakerEventDispatcher.Instance;
			dispatcher.Remove<SwitchCriticalFilterEvent>(OnCriticalFilterChanged);
			dispatcher.Remove<ShowNoteChangeButtonsEvent>(OnShowNoteChangeButtons);
		}

		private void OnToggleValueChanged(bool isOn)
		{
			bool current = MusicScoreMakerEventDispatcher.Instance.PublishFirst<IsCriticalFilterEnabledEvent, bool>(new IsCriticalFilterEnabledEvent());
			if (current != isOn)
			{
				MusicScoreMakerEventDispatcher.Instance.Publish(new SwitchCriticalFilterEvent());
			}
		}

		private void OnCriticalFilterChanged(SwitchCriticalFilterEvent obj)
		{
			if (_toggle == null)
			{
				return;
			}

			bool isEnabled = MusicScoreMakerEventDispatcher.Instance.PublishFirst<IsCriticalFilterEnabledEvent, bool>(new IsCriticalFilterEnabledEvent());
			if (_toggle.isOn != isEnabled)
			{
				_toggle.SetIsOnWithoutNotify(isEnabled);
			}
		}

		private void OnShowNoteChangeButtons(ShowNoteChangeButtonsEvent eventData)
		{
			if (eventData == null || eventData.GroupType == NoteGroupType.Unknown || eventData.GroupType == NoteGroupType.CriticalLongEnd)
			{
				gameObject.SetActive(false);
				return;
			}

			gameObject.SetActive(true);
			bool targetState = DetermineInitialCriticalState(eventData.SelectedNotes);
			bool currentState = MusicScoreMakerEventDispatcher.Instance.PublishFirst<IsCriticalFilterEnabledEvent, bool>(new IsCriticalFilterEnabledEvent());
			if (targetState == currentState)
			{
				if (_toggle != null)
				{
					_toggle.SetIsOnWithoutNotify(targetState);
				}
			}
			else
			{
				MusicScoreMakerEventDispatcher.Instance.Publish(new SwitchCriticalFilterEvent());
			}
		}

		private bool DetermineInitialCriticalState(MusicScoreNoteBase[] selectedNotes)
		{
			if (selectedNotes == null || selectedNotes.Length == 0)
			{
				return false;
			}

			for (int i = 0; i < selectedNotes.Length; i++)
			{
				if (selectedNotes[i] == null || selectedNotes[i].type != NoteType.Critical)
				{
					return false;
				}
			}
			return true;
		}

		public CriticalFilterToggleView()
		{
		}
	}
}
