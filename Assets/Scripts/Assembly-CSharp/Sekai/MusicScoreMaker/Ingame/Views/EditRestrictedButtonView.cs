using Sekai.MusicScoreMaker.Ingame.Events;
using Sekai.MusicScoreMaker.Ingame.Utilities;
using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.Ingame.Views
{
	public class EditRestrictedButtonView : MonoBehaviour
	{
		[SerializeField]
		private CustomButton _button;

		[SerializeField]
		private GameObject _restrictedObject;

		[SerializeField]
		private GameObject _unrestrictedObject;

		private bool _isRestricted;

		private void Awake()
		{
			Setup();
		}

		private void Setup()
		{
			if (_button != null)
			{
				_button.onClick.AddListener(OnButtonClicked);
			}
			SetupEventDispatcher();
			_isRestricted = MusicScoreMakerUtility.IsEditRestricted();
			UpdateDisplay();
			UpdateInteractable();
		}

		private void OnDestroy()
		{
			Dispose();
		}

		private void SetupEventDispatcher()
		{
			MusicScoreMakerEventDispatcher.Instance.Register<UpdateButtonSelectionStateEvent>(OnUpdateButtonSelectionState);
		}

		private void DisposeEventDispatcher()
		{
			MusicScoreMakerEventDispatcher.Instance.Remove<UpdateButtonSelectionStateEvent>(OnUpdateButtonSelectionState);
		}

		private void Dispose()
		{
			if (_button != null)
			{
				_button.onClick.RemoveListener(OnButtonClicked);
			}
			DisposeEventDispatcher();
		}

		private void OnUpdateButtonSelectionState(UpdateButtonSelectionStateEvent obj)
		{
			UpdateInteractable();
		}

		private void UpdateInteractable()
		{
			if (_button == null)
			{
				return;
			}
			bool isEventSettingMode = MusicScoreMakerEventDispatcher.Instance.PublishFirst<GetIsEventSettingModeEvent, bool>(new GetIsEventSettingModeEvent());
			_button.enabled = !isEventSettingMode;
		}

		private void OnButtonClicked()
		{
			_isRestricted = !_isRestricted;
			MusicScoreMakerEventDispatcher.Instance.Publish(new ToggleEditRestrictedEvent
			{
				IsRestricted = _isRestricted
			});
			UpdateDisplay();
		}

		private void UpdateDisplay()
		{
			if (_restrictedObject != null)
			{
				_restrictedObject.SetActive(_isRestricted);
			}
			if (_unrestrictedObject != null)
			{
				_unrestrictedObject.SetActive(!_isRestricted);
			}
		}

		public EditRestrictedButtonView()
		{
		}
	}
}
