using Sekai.MusicScoreMaker.Ingame.Events;
using Sekai.MusicScoreMaker.Ingame.Utilities;
using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.Ingame.Views
{
	public class ToolButton : MonoBehaviour
	{
		[SerializeField]
		private MusicScoreMakerUtility.ToolType _toolType;

		[SerializeField]
		private CustomButton _button;

		[SerializeField]
		private GameObject _selectedIndicator;

		private void Awake()
		{
			Setup(MusicScoreMakerUtility.GetSelectedToolType());
		}

		private void OnDestroy()
		{
			Dispose();
		}

		private void Setup(MusicScoreMakerUtility.ToolType selectedToolType)
		{
			if (_selectedIndicator != null)
			{
				_selectedIndicator.SetActive(_toolType == selectedToolType);
			}
			if (_button != null)
			{
				_button.onClick.RemoveAllListeners();
				_button.onClick.AddListener(OnClick);
			}
			SetupEventDispatcher();
		}

		private void Dispose()
		{
			if (_button != null)
			{
				_button.onClick.RemoveListener(OnClick);
			}
			DisposeEventDispatcher();
		}

		private void SetupEventDispatcher()
		{
			MusicScoreMakerEventDispatcher.Instance.Register<OnToolIconClickEvent>(OnSetSelectedToolTypeEvent);
		}

		private void DisposeEventDispatcher()
		{
			MusicScoreMakerEventDispatcher.Instance.Remove<OnToolIconClickEvent>(OnSetSelectedToolTypeEvent);
		}

		private void OnSetSelectedToolTypeEvent(OnToolIconClickEvent obj)
		{
			if (_selectedIndicator == null)
			{
				return;
			}
			bool selected;
			switch (_toolType)
			{
				case MusicScoreMakerUtility.ToolType.EditRestrict:
					selected = MusicScoreMakerUtility.IsEditRestricted();
					break;
				case MusicScoreMakerUtility.ToolType.AreaSelect:
					selected = MusicScoreMakerUtility.IsAreaSelectMode();
					break;
				case MusicScoreMakerUtility.ToolType.Remove:
					selected = MusicScoreMakerUtility.IsRemoveMode();
					break;
				default:
					selected = !MusicScoreMakerUtility.IsRemoveMode() && MusicScoreMakerUtility.GetSelectedToolType() == _toolType;
					break;
			}
			_selectedIndicator.SetActive(selected);
		}

		private void OnClick()
		{
			PublishEvent();
		}

		private void PublishEvent()
		{
			MusicScoreMakerEventDispatcher.Instance.Publish(new OnToolIconClickEvent
			{
				ToolType = _toolType
			});
		}

		public ToolButton()
		{
		}
	}
}
