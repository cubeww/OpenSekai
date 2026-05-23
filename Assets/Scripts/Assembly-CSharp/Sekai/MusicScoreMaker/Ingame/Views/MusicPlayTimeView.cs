using Sekai.MusicScoreMaker.Ingame.Events;
using Sekai.MusicScoreMaker.Ingame.Models;
using Sekai.MusicScoreMaker.Ingame.Utilities;
using Sekai.UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Sekai.MusicScoreMaker.Ingame.Views
{
	public class MusicPlayTimeView : MonoBehaviour
	{
		[SerializeField]
		[FormerlySerializedAs("_lineObject")]
		private RectTransform _focusTickObj;

		[SerializeField]
		private RectTransform _bpmEventSettingModeObject;

		[SerializeField]
		private RectTransform _highSpeedEventSettingModeObject;

		[SerializeField]
		private RectTransform _timeSignatureEventSettingModeObject;

		[SerializeField]
		private CustomButton _eventSettingModeButton;

		[SerializeField]
		private CustomTextMesh _eventSettingModeText;

		private RectTransform _rectTransform;

		private bool _isEventSettingMode;

		private MusicScoreEventType? _currentEventSettingType;

		private static readonly GetCurrentMusicScoreScaleEvent _getCurrentMusicScoreScaleEvent;

		public void Setup()
		{
			if (_rectTransform == null)
			{
				_rectTransform = GetComponent<RectTransform>();
			}
			HideAllEventSettingModeObjects();
			MusicScoreMakerEventDispatcher dispatcher = MusicScoreMakerEventDispatcher.Instance;
			dispatcher.Register<UpdateMusicPlayTimePreviewEvent>(UpdateMusicPlayTimePreview);
			dispatcher.Register<HideMusicPlayTimePreviewEvent>(HideMusicPlayTimePreview);
		}

		public void Dispose()
		{
			if (MusicScoreMakerEventDispatcher.ExistsInstance)
			{
				MusicScoreMakerEventDispatcher dispatcher = MusicScoreMakerEventDispatcher.Instance;
				dispatcher.Remove<UpdateMusicPlayTimePreviewEvent>(UpdateMusicPlayTimePreview);
				dispatcher.Remove<HideMusicPlayTimePreviewEvent>(HideMusicPlayTimePreview);
			}
		}

		private void UpdateMusicPlayTimePreview(UpdateMusicPlayTimePreviewEvent arg)
		{
			if (_isEventSettingMode)
			{
				UpdateMusicPlayTimePreviewInEventSettingMode(arg);
				return;
			}
			UpdateMusicPlayTimePreviewInNormalMode(arg);
		}

		private void UpdateMusicPlayTimePreviewInNormalMode(UpdateMusicPlayTimePreviewEvent arg)
		{
			if (arg == null || _rectTransform == null || _focusTickObj == null)
			{
				SetFocusTickActive(false);
				return;
			}

			float scale = MusicScoreMakerUtility.GetCurrentMusicScoreScale();
			long focusTicks = MusicScoreMakerUtility.GetFocusTicks();
			double showTicksRange = scale * MusicScoreMakerUtility.TICKS_PER_BAR;
			double startTicks = focusTicks - showTicksRange * MusicScoreMakerSettingsManager.ShowFocusTicksRate;
			double targetTicks = arg.Ticks;
			if (targetTicks < startTicks || targetTicks > startTicks + showTicksRange)
			{
				SetFocusTickActive(false);
				return;
			}

			float y = CalculatePositionYFromTicksDouble(arg.Ticks, startTicks, showTicksRange, _rectTransform.rect.height);
			_focusTickObj.anchoredPosition = new Vector2(0f, y);
			SetFocusTickActive(true);
		}

		private void UpdateMusicPlayTimePreviewInEventSettingMode(UpdateMusicPlayTimePreviewEvent arg)
		{
			UpdateEventSettingModeObjectPosition();
		}

		private static float CalculatePositionYFromTicksDouble(long ticks, double startTicksD, double showTicksRangeD, float height)
		{
			if (showTicksRangeD <= 0d)
			{
				return 0f;
			}
			return (float)((ticks - startTicksD) / showTicksRangeD * height);
		}

		private void HideMusicPlayTimePreview(HideMusicPlayTimePreviewEvent obj)
		{
			SetFocusTickActive(false);
		}

		public void SetFocusTickActive(bool active)
		{
			if (_focusTickObj != null)
			{
				_focusTickObj.gameObject.SetActive(active);
			}
		}

		public void ShowEventSettingModeObject(MusicScoreEventType eventType)
		{
			_isEventSettingMode = true;
			_currentEventSettingType = eventType;
			SetFocusTickActive(false);
			HideAllEventSettingModeObjects();

			RectTransform targetObject = GetEventSettingModeObject(eventType);
			if (targetObject != null)
			{
				targetObject.gameObject.SetActive(true);
				UpdateEventSettingModeObjectPosition(targetObject);
			}
			ShowEventSettingModeButton(eventType);
			SetEventSettingModeText(eventType);
		}

		public void HideEventSettingModeObject()
		{
			_isEventSettingMode = false;
			_currentEventSettingType = null;
			HideAllEventSettingModeObjects();
		}

		private void HideAllEventSettingModeObjects()
		{
			if (_bpmEventSettingModeObject != null)
			{
				_bpmEventSettingModeObject.gameObject.SetActive(false);
			}
			if (_highSpeedEventSettingModeObject != null)
			{
				_highSpeedEventSettingModeObject.gameObject.SetActive(false);
			}
			if (_timeSignatureEventSettingModeObject != null)
			{
				_timeSignatureEventSettingModeObject.gameObject.SetActive(false);
			}
			HideAllEventSettingModeButtons();
		}

		private void ShowEventSettingModeButton(MusicScoreEventType eventType)
		{
			if (_eventSettingModeButton != null)
			{
				_eventSettingModeButton.gameObject.SetActive(true);
			}
		}

		private void HideAllEventSettingModeButtons()
		{
			if (_eventSettingModeButton != null)
			{
				_eventSettingModeButton.gameObject.SetActive(false);
			}
		}

		private void SetEventSettingModeText(MusicScoreEventType eventType)
		{
			if (_eventSettingModeText == null)
			{
				return;
			}

			switch (eventType)
			{
			case MusicScoreEventType.BPM:
				_eventSettingModeText.SetWordingText("WORD_CHANGE_BPM");
				break;
			case MusicScoreEventType.HighSpeed:
				_eventSettingModeText.SetWordingText("WORD_CHANGE_HIGH_SPEED");
				break;
			case MusicScoreEventType.TimeSignature:
				_eventSettingModeText.SetWordingText("WORD_CHANGE_TIME_SIGNATURE");
				break;
			default:
				_eventSettingModeText.SetText(string.Empty);
				break;
			}
		}

		private RectTransform GetEventSettingModeObject(MusicScoreEventType eventType)
		{
			switch (eventType)
			{
			case MusicScoreEventType.BPM:
				return _bpmEventSettingModeObject;
			case MusicScoreEventType.HighSpeed:
				return _highSpeedEventSettingModeObject;
			case MusicScoreEventType.TimeSignature:
				return _timeSignatureEventSettingModeObject;
			default:
				return null;
			}
		}

		private long GetSnappedTicksForEventSettingMode(MusicScoreEventType? eventType, long focusTicks)
		{
			if (eventType == MusicScoreEventType.TimeSignature)
			{
				MusicScoreMakerData musicScoreMakerData = MusicScoreMakerUtility.GetMusicScoreMakerData();
				if (focusTicks == 0L)
				{
					return MusicScoreMakerUtility.CalculateTicksFromBarAndProgress(0, 0f, musicScoreMakerData?.MusicScoreEventDataList);
				}
				long snappedTicks = MusicScoreMakerUtility.SnapToBarStartOrTimeSignature(focusTicks, musicScoreMakerData?.MusicScoreEventDataList);
				return snappedTicks <= MusicScoreMakerUtility.GetMusicScoreTicksMax() ? snappedTicks : MusicScoreMakerUtility.SnapToBarStart(focusTicks);
			}
			return MusicScoreMakerUtility.CalculateSnapQuantizedTicks(0L, focusTicks);
		}

		private void UpdateEventSettingModeObjectPosition(RectTransform targetObject)
		{
			if (targetObject == null || _rectTransform == null)
			{
				return;
			}

			long focusTicks = MusicScoreMakerUtility.GetFocusTicks();
			long snappedTicks = GetSnappedTicksForEventSettingMode(_currentEventSettingType, focusTicks);
			float scale = MusicScoreMakerUtility.GetCurrentMusicScoreScale();
			double showTicksRange = scale * MusicScoreMakerUtility.TICKS_PER_BAR;
			double startTicks = focusTicks - showTicksRange * MusicScoreMakerSettingsManager.ShowFocusTicksRate;
			float height = _rectTransform.rect.height;
			float y = CalculatePositionYFromTicksDouble(snappedTicks, startTicks, showTicksRange, height);
			if (_currentEventSettingType == MusicScoreEventType.TimeSignature)
			{
				y = Mathf.Clamp(y, 0f, height);
			}
			targetObject.anchoredPosition = new Vector2(0f, y);
		}

		private void UpdateEventSettingModeObjectPosition()
		{
			if (_currentEventSettingType.HasValue)
			{
				UpdateEventSettingModeObjectPosition(GetEventSettingModeObject(_currentEventSettingType.Value));
			}
		}

		public long GetEventSettingModeSnappedTicks(MusicScoreEventType eventType)
		{
			RectTransform targetObject = GetEventSettingModeObject(eventType);
			if (targetObject == null || !targetObject.gameObject.activeSelf)
			{
				return 0L;
			}
			return GetSnappedTicksForEventSettingMode(eventType, MusicScoreMakerUtility.GetFocusTicks());
		}

		public MusicPlayTimeView()
		{
		}

		static MusicPlayTimeView()
		{
			_getCurrentMusicScoreScaleEvent = new GetCurrentMusicScoreScaleEvent();
		}
	}
}
