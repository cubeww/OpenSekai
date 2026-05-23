using Sekai.MusicScoreMaker.Ingame.Events;
using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.Ingame.Views
{
	public class DispatcherEventBaseButton : MonoBehaviour
	{
		[SerializeField]
		private string _eventClassName;

		[SerializeField]
		private string _holdRepeatEventClassName;

		[SerializeField]
		private string _dragEventClassName;

		[SerializeField]
		private CustomButton _button;

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
			if (_button == null)
			{
				_button = GetComponent<CustomButton>();
			}
			if (_button == null)
			{
				return;
			}
			_button.onClick.RemoveAllListeners();
			if (!string.IsNullOrEmpty(_eventClassName))
			{
				_button.onClick.AddListener(OnClick);
			}
			_button.OnHoldRepeatEvent = string.IsNullOrEmpty(_holdRepeatEventClassName) ? null : PublishHoldRepeatEvent;
		}

		private void Dispose()
		{
			if (_button == null)
			{
				return;
			}
			_button.onClick.RemoveListener(OnClick);
			if (_button.OnHoldRepeatEvent == PublishHoldRepeatEvent)
			{
				_button.OnHoldRepeatEvent = null;
			}
		}

		private void OnClick()
		{
			PublishEvent();
		}

		private void PublishEvent()
		{
			if (!string.IsNullOrEmpty(_eventClassName))
			{
				MusicScoreMakerEventDispatcher.Instance.Publish(_eventClassName);
			}
		}

		private void PublishHoldRepeatEvent()
		{
			if (!string.IsNullOrEmpty(_holdRepeatEventClassName))
			{
				MusicScoreMakerEventDispatcher.Instance.Publish(_holdRepeatEventClassName);
			}
		}

		public void SetActive(bool value)
		{
			gameObject.SetActive(value);
		}

		public DispatcherEventBaseButton()
		{
		}
	}
}
