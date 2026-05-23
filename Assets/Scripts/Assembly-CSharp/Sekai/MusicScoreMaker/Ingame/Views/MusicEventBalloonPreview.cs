using Sekai.MusicScoreMaker.Ingame.Input;
using Sekai.MusicScoreMaker.Ingame.Events;
using Sekai.MusicScoreMaker.Ingame.Models;
using Sekai.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sekai.MusicScoreMaker.Ingame.Views
{
	public class MusicEventBalloonPreview : MonoBehaviour
	{
		[SerializeField]
		private GameObject _bpmObject;

		[SerializeField]
		private RectTransform _bpmRectTransform;

		[SerializeField]
		private CustomTextMesh _bpmText;

		[SerializeField]
		private GameObject _highSpeedObject;

		[SerializeField]
		private RectTransform _highSpeedRectTransform;

		[SerializeField]
		private CustomTextMesh _highSpeedText;

		[SerializeField]
		private GameObject _timeSignatureObject;

		[SerializeField]
		private RectTransform _timeSignatureRectTransform;

		[SerializeField]
		private CustomTextMesh _timeSignatureText;

		[SerializeField]
		private GameObject _timeSignatureErrorObject;

		[SerializeField]
		private GameObject _separatorTimeSignatureAndBpm;

		[SerializeField]
		private GameObject _separatorBpmAndHighSpeed;

		[SerializeField]
		private ToolInputHandler _toolInputHandler;

		private Canvas _canvas;

		private int _bpmId;

		private int _highSpeedId;

		private int _timeSignatureId;

		public void Setup()
		{
			if (_toolInputHandler != null)
			{
				_toolInputHandler.RemoveAllAndAddListener(OnBalloonClick);
			}
		}

		public void Dispose()
		{
			if (_toolInputHandler != null)
			{
				_toolInputHandler.RemoveAllListeners();
			}
		}

		private void OnBalloonClick(PointerEventData eventData)
		{
			int eventId = GetEventIdByCurrentSettingMode();
			if (eventId < 0)
			{
				return;
			}
			MusicScoreMakerEventDispatcher.Instance.Publish(new OnClickMusicScoreEventDataEvent
			{
				Id = eventId
			});
		}

		private int GetEventIdByCurrentSettingMode()
		{
			MusicScoreEventType? eventType = MusicScoreMakerEventDispatcher.Instance.PublishFirst<GetSelectedEventSettingModeTypeEvent, MusicScoreEventType?>(new GetSelectedEventSettingModeTypeEvent());
			if (!eventType.HasValue)
			{
				return -1;
			}
			switch (eventType.Value)
			{
			case MusicScoreEventType.BPM:
				return _bpmId;
			case MusicScoreEventType.HighSpeed:
				return _highSpeedId;
			case MusicScoreEventType.TimeSignature:
				return _timeSignatureId;
			default:
				return -1;
			}
		}

		public void SetEventActive(MusicScoreEventType eventType, bool active, int id = -1, string showValue = "")
		{
			showValue ??= string.Empty;
			switch (eventType)
			{
			case MusicScoreEventType.BPM:
				SetSegment(_bpmObject, _bpmText, active, id, showValue, ref _bpmId);
				break;
			case MusicScoreEventType.HighSpeed:
				// The original client branch disables this segment in the score maker preview.
				// Keep the hidden state here until the high-speed event UI is restored in full.
				if (_highSpeedObject != null)
				{
					_highSpeedObject.SetActive(false);
				}
				_highSpeedId = id;
				break;
			case MusicScoreEventType.TimeSignature:
				SetSegment(_timeSignatureObject, _timeSignatureText, active, id, showValue + WordingManager.Get("WORD_BAR"), ref _timeSignatureId);
				break;
			}
			UpdateSeparatorVisibility();
		}

		private void UpdateSeparatorVisibility()
		{
			if (_separatorTimeSignatureAndBpm != null)
			{
				_separatorTimeSignatureAndBpm.SetActive(IsActive(_timeSignatureObject) && IsActive(_bpmObject));
			}
			if (_separatorBpmAndHighSpeed != null)
			{
				_separatorBpmAndHighSpeed.SetActive(IsActive(_bpmObject) && IsActive(_highSpeedObject));
			}
		}

		public void SetTimeSignatureErrorActive(bool hasError)
		{
			if (_timeSignatureErrorObject != null)
			{
				_timeSignatureErrorObject.SetActive(hasError);
			}
			if (_timeSignatureText != null)
			{
				_timeSignatureText.color = hasError ? Sekai.ColorUtility.FONT_COLOR_WHITE : Sekai.ColorUtility.COLOR_BASE_DBL;
			}
		}

		private static void SetSegment(GameObject targetObject, CustomTextMesh text, bool active, int id, string showValue, ref int targetId)
		{
			if (targetObject != null)
			{
				if (targetObject.activeSelf != active)
				{
					targetObject.SetActive(active);
				}
				else
				{
					targetObject.SetActive(active);
				}
			}
			if (text != null)
			{
				text.SetText(showValue);
			}
			targetId = id;
		}

		private static bool IsActive(GameObject gameObject)
		{
			return gameObject != null && gameObject.activeSelf;
		}

		public MusicEventBalloonPreview()
		{
		}
	}
}
