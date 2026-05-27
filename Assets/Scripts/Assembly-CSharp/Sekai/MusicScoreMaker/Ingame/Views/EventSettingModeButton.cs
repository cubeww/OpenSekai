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

		public void Setup()
		{
			SetupEventDispatcher();
			UpdateSelectedState();
		}

		public void Dispose()
		{
			DisposeEventDispatcher();
		}

		private void SetupEventDispatcher()
		{
			MusicScoreMakerEventDispatcher dispatcher = MusicScoreMakerEventDispatcher.Instance;
			dispatcher.Remove<UpdateButtonSelectionStateEvent>(OnUpdateButtonSelectionState);
			dispatcher.Register<UpdateButtonSelectionStateEvent>(OnUpdateButtonSelectionState);
		}

		private void DisposeEventDispatcher()
		{
			if (!MusicScoreMakerEventDispatcher.ExistsInstance)
			{
				return;
			}
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
