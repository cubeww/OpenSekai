using Sekai.MusicScoreMaker.Ingame.Events;
using Sekai.MusicScoreMaker.Ingame.Models;
using UnityEngine;

namespace Sekai.MusicScoreMaker.Ingame.Views
{
	public class EventSettingModeButton : MonoBehaviour
	{
		[SerializeField]
		private MusicScoreEventType _eventType;

		[SerializeField]
		private GameObject _selectedIndicator;

		private void Awake()
		{
			Setup();
		}

		private void OnDestroy()
		{
			Dispose();
		}

		private void Setup()
		{
			SetupEventDispatcher();
			UpdateSelectedState();
		}

		private void Dispose()
		{
			DisposeEventDispatcher();
		}

		private void SetupEventDispatcher()
		{
			MusicScoreMakerEventDispatcher.Instance.Register<UpdateButtonSelectionStateEvent>(OnUpdateButtonSelectionState);
		}

		private void DisposeEventDispatcher()
		{
			MusicScoreMakerEventDispatcher.Instance.Remove<UpdateButtonSelectionStateEvent>(OnUpdateButtonSelectionState);
		}

		private void OnUpdateButtonSelectionState(UpdateButtonSelectionStateEvent obj)
		{
			UpdateSelectedState();
		}

		private void UpdateSelectedState()
		{
			if (_selectedIndicator == null)
			{
				return;
			}
			MusicScoreEventType? selectedType = MusicScoreMakerEventDispatcher.Instance.PublishFirst<GetSelectedEventSettingModeTypeEvent, MusicScoreEventType?>(new GetSelectedEventSettingModeTypeEvent());
			_selectedIndicator.SetActive(selectedType.HasValue && selectedType.Value == _eventType);
		}

		public EventSettingModeButton()
		{
		}
	}
}
