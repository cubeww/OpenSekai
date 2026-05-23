using System;
using System.Globalization;
using Sekai.MusicScoreMaker.Ingame.Events;
using Sekai.MusicScoreMaker.Ingame.Utilities;
using Sekai.UI;
using TMPro;
using UnityEngine;

namespace Sekai.MusicScoreMaker.Ingame.Views
{
	public class ZoomScaleInputView : MonoBehaviour
	{
		private const string WORD_MULTIPLY = "×";

		private const string SCALE_DISPLAY_FORMAT = "{0:0}<size=20>×</size>";

		private const float DISPLAY_MULTIPLIER = 100f;

		[SerializeField]
		private CustomInputFieldTextMesh _scaleInputField;

		[SerializeField]
		private CustomButton _zoomInButton;

		[SerializeField]
		private CustomButton _zoomOutButton;

		private static readonly GetCurrentMusicScoreScaleEvent _getCurrentMusicScoreScaleEvent;

		private void OnDestroy()
		{
			Dispose();
		}

		public void Setup()
		{
			InitializeInputField();
			SetupEventDispatcher();
			UpdateScaleDisplay();
			UpdateZoomButtonsState();
		}

		private void InitializeInputField()
		{
			if (_scaleInputField == null)
			{
				return;
			}

			_scaleInputField.contentType = TMP_InputField.ContentType.IntegerNumber;
			_scaleInputField.lineType = TMP_InputField.LineType.SingleLine;
			_scaleInputField.inputType = TMP_InputField.InputType.Standard;
			_scaleInputField.keyboardType = TouchScreenKeyboardType.NumberPad;
			_scaleInputField.characterValidation = TMP_InputField.CharacterValidation.Integer;
			_scaleInputField.characterLimit = 5;
			_scaleInputField.onEndEdit.RemoveListener(OnScaleInputEndEdit);
			_scaleInputField.onSelect.RemoveListener(OnScaleInputSelected);
			_scaleInputField.onDeselect.RemoveListener(OnScaleInputDeselected);
			_scaleInputField.onEndEdit.AddListener(OnScaleInputEndEdit);
			_scaleInputField.onSelect.AddListener(OnScaleInputSelected);
			_scaleInputField.onDeselect.AddListener(OnScaleInputDeselected);
		}

		private void SetupEventDispatcher()
		{
			MusicScoreMakerEventDispatcher dispatcher = MusicScoreMakerEventDispatcher.Instance;
			dispatcher.Remove<ZoomTimelineScaleChangedEvent>(OnZoomTimelineScaleChanged);
			dispatcher.Register<ZoomTimelineScaleChangedEvent>(OnZoomTimelineScaleChanged);
			if (_zoomInButton != null)
			{
				_zoomInButton.onClick.RemoveListener(PublishZoomIn);
				_zoomInButton.onClick.AddListener(PublishZoomIn);
			}
			if (_zoomOutButton != null)
			{
				_zoomOutButton.onClick.RemoveListener(PublishZoomOut);
				_zoomOutButton.onClick.AddListener(PublishZoomOut);
			}
		}

		public void Dispose()
		{
			if (_scaleInputField != null)
			{
				_scaleInputField.onEndEdit.RemoveListener(OnScaleInputEndEdit);
				_scaleInputField.onSelect.RemoveListener(OnScaleInputSelected);
				_scaleInputField.onDeselect.RemoveListener(OnScaleInputDeselected);
			}
			if (_zoomInButton != null)
			{
				_zoomInButton.onClick.RemoveListener(PublishZoomIn);
			}
			if (_zoomOutButton != null)
			{
				_zoomOutButton.onClick.RemoveListener(PublishZoomOut);
			}
			DisposeEventDispatcher();
		}

		private void DisposeEventDispatcher()
		{
			MusicScoreMakerEventDispatcher.Instance.Remove<ZoomTimelineScaleChangedEvent>(OnZoomTimelineScaleChanged);
		}

		private int ConvertScaleToDisplayValue(float scale)
		{
			return (int)Math.Round(scale * DISPLAY_MULTIPLIER, MidpointRounding.ToEven);
		}

		private float ConvertDisplayValueToScale(int displayValue)
		{
			return Mathf.Clamp(displayValue / DISPLAY_MULTIPLIER, MusicScoreMakerSettingsManager.ZoomTimelineScaleMin, MusicScoreMakerSettingsManager.ZoomTimelineScaleMax);
		}

		private void UpdateScaleDisplay()
		{
			if (_scaleInputField == null)
			{
				return;
			}

			float scale = MusicScoreMakerEventDispatcher.Instance.PublishFirst<GetCurrentMusicScoreScaleEvent, float>(_getCurrentMusicScoreScaleEvent);
			if (scale <= 0f)
			{
				scale = 1f;
			}
			_scaleInputField.SetTextWithoutNotify(string.Format(CultureInfo.InvariantCulture, SCALE_DISPLAY_FORMAT, ConvertScaleToDisplayValue(scale)));
		}

		private void OnScaleInputSelected(string text)
		{
			if (_scaleInputField == null)
			{
				return;
			}
			float scale = MusicScoreMakerEventDispatcher.Instance.PublishFirst<GetCurrentMusicScoreScaleEvent, float>(_getCurrentMusicScoreScaleEvent);
			if (scale <= 0f)
			{
				scale = 1f;
			}
			_scaleInputField.SetTextWithoutNotify(ConvertScaleToDisplayValue(scale).ToString(CultureInfo.InvariantCulture));
		}

		private void OnScaleInputDeselected(string text)
		{
			UpdateScaleDisplay();
		}

		private void OnScaleInputEndEdit(string text)
		{
			if (int.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out int displayValue))
			{
				MusicScoreMakerEventDispatcher.Instance.Publish(new SetZoomTimelineScaleEvent
				{
					Scale = ConvertDisplayValueToScale(displayValue)
				});
			}
			else
			{
				UpdateScaleDisplay();
			}
		}

		private void OnZoomTimelineScaleChanged(ZoomTimelineScaleChangedEvent evt)
		{
			UpdateScaleDisplay();
			UpdateZoomButtonsState();
		}

		private void UpdateZoomButtonsState()
		{
			float scale = MusicScoreMakerEventDispatcher.Instance.PublishFirst<GetCurrentMusicScoreScaleEvent, float>(_getCurrentMusicScoreScaleEvent);
			if (scale <= 0f)
			{
				scale = 1f;
			}
			if (_zoomOutButton != null)
			{
				_zoomOutButton.enabled = scale > MusicScoreMakerSettingsManager.ZoomTimelineScaleMin;
			}
			if (_zoomInButton != null)
			{
				_zoomInButton.enabled = scale < MusicScoreMakerSettingsManager.ZoomTimelineScaleMax;
			}
		}

		private void PublishZoomIn()
		{
			MusicScoreMakerEventDispatcher.Instance.Publish(new ZoomInTimelineEvent());
		}

		private void PublishZoomOut()
		{
			MusicScoreMakerEventDispatcher.Instance.Publish(new ZoomOutTimelineEvent());
		}

		public ZoomScaleInputView()
		{
		}

		static ZoomScaleInputView()
		{
			_getCurrentMusicScoreScaleEvent = new GetCurrentMusicScoreScaleEvent();
		}
	}
}
