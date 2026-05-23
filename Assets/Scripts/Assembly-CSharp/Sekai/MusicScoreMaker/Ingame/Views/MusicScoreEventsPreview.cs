using System.Collections.Generic;
using Sekai.MusicScoreMaker.Ingame.Models;
using Sekai.MusicScoreMaker.Ingame.Utilities;
using UnityEngine;
using UnityEngine.Serialization;

namespace Sekai.MusicScoreMaker.Ingame.Views
{
	public class MusicScoreEventsPreview : MonoBehaviour
	{
		private const float DefaultBpm = 120f;

		private const string DefaultTimeSignatureText = "4/4";

		[SerializeField]
		[FormerlySerializedAs("musicEventBalloonPreviewPrefab")]
		private MusicEventBalloonPreview _musicEventBalloonPreviewPrefab;

		private Dictionary<long, MusicEventBalloonPreview> _balloonDict;

		private Dictionary<long, RectTransform> _balloonRectCache;

		private Dictionary<int, MusicEventBalloonPreview> _eventIdToBalloonMap;

		private Dictionary<MusicEventBalloonPreview, long> _balloonToTicksMap;

		private Dictionary<long, List<MusicScoreEventData>> _ticksGroupsCache;

		private Dictionary<MusicScoreEventType, (int id, string value)> _eventDataMapCache;

		private Dictionary<float, string> _eventValueStringCache;

		private HashSet<long> _existingTicksSetCache;

		private List<long> _ticksToRemoveCache;

		private MusicEventBalloonPreview _focusTicksBalloon;

		private RectTransform _focusTicksBalloonRectTransform;

		private bool _focusTicksBalloonPositionCacheValid;

		private long _cachedFocusTicksForBalloonPosition;

		private long _cachedStartTicksForBalloonPosition;

		private long _cachedEndTicksForBalloonPosition;

		private Vector2 _cachedParentSizeForBalloonPosition;

		private RectTransform _rectTransform;

		public RectTransform RectTransform
		{
			get
			{
				return _rectTransform;
			}
		}

		public Dictionary<long, MusicEventBalloonPreview> BalloonDict
		{
			get
			{
				return _balloonDict;
			}
		}

		public void Setup()
		{
			if (_rectTransform == null)
			{
				_rectTransform = GetComponent<RectTransform>();
			}
			CreateFocusTicksBalloon();
		}

		public void Dispose()
		{
			DestroyBalloonInstances();
			_balloonDict.Clear();
			_balloonRectCache.Clear();
			_eventIdToBalloonMap.Clear();
			_balloonToTicksMap.Clear();
			if (_focusTicksBalloon != null)
			{
				_focusTicksBalloon.Dispose();
				Destroy(_focusTicksBalloon.gameObject);
				_focusTicksBalloon = null;
				_focusTicksBalloonRectTransform = null;
			}
		}

		public void UpdateView(MusicScoreMakerData MusicScoreMakerData, long startTicks, long endTicks)
		{
			if (MusicScoreMakerData?.MusicScoreEventDataList == null)
			{
				HideFocusTicksBalloon();
				return;
			}
			ClearReverseLookupMaps();
			long focusTicks = MusicScoreMakerUtility.GetFocusTicks();
			Dictionary<long, List<MusicScoreEventData>> groupedEvents = GroupEventsByTicksAndGetFocusEventValues(MusicScoreMakerData.MusicScoreEventDataList, focusTicks, out float focusBpm, out string focusTimeSignatureText);
			Vector2 parentSize = GetParentSize();
			_existingTicksSetCache.Clear();
			foreach (KeyValuePair<long, List<MusicScoreEventData>> pair in groupedEvents)
			{
				if (pair.Value != null && pair.Value.Count > 0)
				{
					_existingTicksSetCache.Add(pair.Key);
				}
			}
			_ticksToRemoveCache.Clear();
			foreach (KeyValuePair<long, MusicEventBalloonPreview> pair in _balloonDict)
			{
				if (pair.Value == null || !_existingTicksSetCache.Contains(pair.Key))
				{
					if (pair.Value != null)
					{
						Destroy(pair.Value.gameObject);
					}
					_ticksToRemoveCache.Add(pair.Key);
				}
			}
			foreach (long ticks in _ticksToRemoveCache)
			{
				_balloonDict.Remove(ticks);
				_balloonRectCache.Remove(ticks);
			}
			foreach (KeyValuePair<long, List<MusicScoreEventData>> pair in groupedEvents)
			{
				if (pair.Value == null || pair.Value.Count == 0)
				{
					continue;
				}
				if (pair.Key < startTicks || pair.Key > endTicks)
				{
					if (_balloonDict.TryGetValue(pair.Key, out MusicEventBalloonPreview preview) && preview != null)
					{
						preview.gameObject.SetActive(false);
					}
					continue;
				}
				UpdateBalloonForTicks(pair.Key, pair.Value, startTicks, endTicks, parentSize, MusicScoreMakerData);
			}
			UpdateFocusTicksBalloon(focusTicks, focusBpm, focusTimeSignatureText, startTicks, endTicks, parentSize);
		}

		private void UpdateFocusTicksBalloon(long focusTicks, float focusBpm, string focusTimeSignatureText, long startTicks, long endTicks, Vector2 parentSize)
		{
			if (focusTicks < startTicks || focusTicks > endTicks)
			{
				HideFocusTicksBalloon();
				return;
			}
			if (_focusTicksBalloon == null)
			{
				return;
			}
			_focusTicksBalloon.gameObject.SetActive(true);
			_focusTicksBalloon.SetEventActive(MusicScoreEventType.BPM, true, -1, GetCachedFloatString(focusBpm));
			_focusTicksBalloon.SetEventActive(MusicScoreEventType.HighSpeed, false, -1, string.Empty);
			_focusTicksBalloon.SetEventActive(MusicScoreEventType.TimeSignature, true, -1, focusTimeSignatureText);
			UpdateFocusTicksBalloonPosition(focusTicks, startTicks, endTicks, parentSize);
		}

		private void CreateFocusTicksBalloon()
		{
			if (_focusTicksBalloon == null && _musicEventBalloonPreviewPrefab != null)
			{
				_focusTicksBalloon = Instantiate(_musicEventBalloonPreviewPrefab, transform);
				_focusTicksBalloon.name = "FocusTicksBalloon";
				_focusTicksBalloon.Setup();
				_focusTicksBalloonRectTransform = _focusTicksBalloon.GetComponent<RectTransform>();
				_focusTicksBalloonPositionCacheValid = false;
				_focusTicksBalloon.transform.SetAsLastSibling();
			}
			if (_focusTicksBalloonRectTransform == null && _focusTicksBalloon != null)
			{
				_focusTicksBalloonRectTransform = _focusTicksBalloon.GetComponent<RectTransform>();
			}
			if (_focusTicksBalloon != null)
			{
				_focusTicksBalloon.gameObject.SetActive(false);
			}
		}

		private void UpdateFocusTicksBalloonPosition(long focusTicks, long startTicks, long endTicks, Vector2 parentSize)
		{
			if (_focusTicksBalloonRectTransform == null)
			{
				return;
			}
			if (_focusTicksBalloonPositionCacheValid && _cachedFocusTicksForBalloonPosition == focusTicks && _cachedStartTicksForBalloonPosition == startTicks && _cachedEndTicksForBalloonPosition == endTicks && (_cachedParentSizeForBalloonPosition - parentSize).sqrMagnitude < 1E-10f)
			{
				return;
			}
			Vector2 position = _focusTicksBalloonRectTransform.anchoredPosition;
			float rate = MusicScoreMakerSettingsManager.ShowFocusTicksRate;
			position.y = parentSize.y * rate - parentSize.y * 0.5f;
			_focusTicksBalloonRectTransform.anchoredPosition = position;
			_cachedFocusTicksForBalloonPosition = focusTicks;
			_cachedStartTicksForBalloonPosition = startTicks;
			_cachedEndTicksForBalloonPosition = endTicks;
			_cachedParentSizeForBalloonPosition = parentSize;
			_focusTicksBalloonPositionCacheValid = true;
		}

		private void HideFocusTicksBalloon()
		{
			if (_focusTicksBalloon != null && _focusTicksBalloon.gameObject.activeSelf)
			{
				_focusTicksBalloon.gameObject.SetActive(false);
			}
			_focusTicksBalloonPositionCacheValid = false;
		}

		private Dictionary<long, List<MusicScoreEventData>> GroupEventsByTicksAndGetFocusEventValues(List<MusicScoreEventData> eventDataList, long focusTicks, out float bpm, out string timeSignatureText)
		{
			bpm = DefaultBpm;
			timeSignatureText = DefaultTimeSignatureText;
			if (eventDataList == null)
			{
				return _ticksGroupsCache;
			}
			foreach (List<MusicScoreEventData> group in _ticksGroupsCache.Values)
			{
				group.Clear();
			}
			long latestBpmTicks = long.MinValue;
			long latestTimeSignatureTicks = long.MinValue;
			foreach (MusicScoreEventData eventData in eventDataList)
			{
				if (eventData == null)
				{
					continue;
				}
				if (!_ticksGroupsCache.TryGetValue(eventData.ticks, out List<MusicScoreEventData> group))
				{
					group = new List<MusicScoreEventData>();
					_ticksGroupsCache[eventData.ticks] = group;
				}
				group.Add(eventData);
				if (eventData.ticks > focusTicks)
				{
					continue;
				}
				if (eventData.eventType == MusicScoreEventType.BPM && eventData.ticks >= latestBpmTicks)
				{
					bpm = ExtractFloatFromChangeValue(eventData.changeValue, DefaultBpm);
					latestBpmTicks = eventData.ticks;
				}
				else if (eventData.eventType == MusicScoreEventType.TimeSignature && eventData.ticks >= latestTimeSignatureTicks)
				{
					timeSignatureText = ExtractTimeSignatureText(eventData.changeValue);
					latestTimeSignatureTicks = eventData.ticks;
				}
			}
			return _ticksGroupsCache;
		}

		private static float ExtractFloatFromChangeValue(object changeValue, float fallback)
		{
			switch (changeValue)
			{
			case float floatValue:
				return floatValue;
			case double doubleValue:
				return (float)doubleValue;
			default:
				return fallback;
			}
		}

		private static string ExtractTimeSignatureText(object changeValue)
		{
			switch (changeValue)
			{
			case string text when !string.IsNullOrEmpty(text):
				return text;
			case float floatValue:
				return MusicScoreMakerUtility.FormatTimeSignatureText(floatValue);
			case double doubleValue:
				return MusicScoreMakerUtility.FormatTimeSignatureText((float)doubleValue);
			default:
				return DefaultTimeSignatureText;
			}
		}

		private string GetCachedFloatString(float floatValue)
		{
			if (!_eventValueStringCache.TryGetValue(floatValue, out string value))
			{
				value = floatValue.ToString();
				_eventValueStringCache[floatValue] = value;
			}
			return value;
		}

		private void ClearReverseLookupMaps()
		{
			_eventIdToBalloonMap.Clear();
			_balloonToTicksMap.Clear();
		}

		private Vector2 GetParentSize()
		{
			if (_rectTransform == null)
			{
				_rectTransform = GetComponent<RectTransform>();
			}
			return _rectTransform != null ? _rectTransform.rect.size : Vector2.zero;
		}

		private void UpdateBalloonForTicks(long ticks, List<MusicScoreEventData> events, long startTicks, long endTicks, Vector2 parentSize, MusicScoreMakerData MusicScoreMakerData)
		{
			MusicEventBalloonPreview balloon = GetOrCreateBalloon(ticks);
			if (balloon == null)
			{
				return;
			}
			balloon.gameObject.SetActive(true);
			_balloonToTicksMap[balloon] = ticks;
			UpdateBalloonPosition(balloon, ticks, startTicks, endTicks, parentSize);
			UpdateBalloonEvents(balloon, events, MusicScoreMakerData, ticks);
		}

		private MusicEventBalloonPreview GetOrCreateBalloon(long ticks)
		{
			if (_balloonDict.TryGetValue(ticks, out MusicEventBalloonPreview balloon) && balloon != null)
			{
				return balloon;
			}
			if (_musicEventBalloonPreviewPrefab == null)
			{
				return null;
			}
			balloon = Instantiate(_musicEventBalloonPreviewPrefab, transform);
			balloon.Setup();
			_balloonDict[ticks] = balloon;
			_balloonRectCache[ticks] = balloon.GetComponent<RectTransform>();
			return balloon;
		}

		private void UpdateBalloonPosition(MusicEventBalloonPreview musicEventBalloon, long ticks, long startTicks, long endTicks, Vector2 parentSize)
		{
			if (musicEventBalloon == null)
			{
				return;
			}
			if (!_balloonRectCache.TryGetValue(ticks, out RectTransform rectTransform) || rectTransform == null)
			{
				rectTransform = musicEventBalloon.GetComponent<RectTransform>();
				_balloonRectCache[ticks] = rectTransform;
			}
			if (rectTransform == null)
			{
				return;
			}
			Vector2 position = rectTransform.anchoredPosition;
			position.y = MusicScoreMakerUtility.CalcPreviewPositionYFromTicks(startTicks, endTicks, parentSize, Vector2.zero, ticks);
			rectTransform.anchoredPosition = position;
		}

		private void UpdateBalloonEvents(MusicEventBalloonPreview musicEventBalloon, List<MusicScoreEventData> events, MusicScoreMakerData MusicScoreMakerData, long ticks)
		{
			_eventDataMapCache.Clear();
			if (events != null)
			{
				foreach (MusicScoreEventData eventData in events)
				{
					if (eventData == null)
					{
						continue;
					}
					_eventDataMapCache[eventData.eventType] = (eventData.id, GetMusicScoreEventDisplayValue(eventData, MusicScoreMakerData));
					_eventIdToBalloonMap[eventData.id] = musicEventBalloon;
				}
			}
			UpdateEventActiveState(musicEventBalloon, MusicScoreEventType.BPM, _eventDataMapCache);
			UpdateEventActiveState(musicEventBalloon, MusicScoreEventType.HighSpeed, _eventDataMapCache);
			UpdateEventActiveState(musicEventBalloon, MusicScoreEventType.TimeSignature, _eventDataMapCache);
			UpdateTimeSignatureErrorState(musicEventBalloon, events, MusicScoreMakerData, ticks);
		}

		private void UpdateTimeSignatureErrorState(MusicEventBalloonPreview musicEventBalloon, List<MusicScoreEventData> events, MusicScoreMakerData MusicScoreMakerData, long ticks)
		{
			bool hasError = false;
			if (MusicScoreMakerData?.InvalidPlacements != null && events != null)
			{
				MusicScoreEventData timeSignatureEvent = events.Find(eventData => eventData != null && eventData.eventType == MusicScoreEventType.TimeSignature);
				if (timeSignatureEvent != null)
				{
					foreach (InvalidPlacementInfo placement in MusicScoreMakerData.InvalidPlacements)
					{
						if (placement != null && placement.Type == InvalidPlacementType.TimeSignatureOffset && placement.Ids != null && placement.Ids.Count > 0 && placement.Ids[0] == timeSignatureEvent.id)
						{
							hasError = true;
							break;
						}
					}
				}
			}
			if (musicEventBalloon != null)
			{
				musicEventBalloon.SetTimeSignatureErrorActive(hasError);
			}
		}

		private void UpdateEventActiveState(MusicEventBalloonPreview musicEventBalloon, MusicScoreEventType eventType, Dictionary<MusicScoreEventType, (int id, string value)> eventDataMap)
		{
			if (musicEventBalloon == null)
			{
				return;
			}
			if (eventDataMap != null && eventDataMap.TryGetValue(eventType, out (int id, string value) eventData))
			{
				musicEventBalloon.SetEventActive(eventType, true, eventData.id, eventData.value);
			}
			else
			{
				musicEventBalloon.SetEventActive(eventType, false, -1, string.Empty);
			}
		}

		private string GetMusicScoreEventDisplayValue(MusicScoreEventData eventData, MusicScoreMakerData MusicScoreMakerData)
		{
			if (eventData?.changeValue == null || MusicScoreMakerData == null)
			{
				return string.Empty;
			}
			if (eventData.eventType == MusicScoreEventType.TimeSignature)
			{
				return ExtractTimeSignatureText(eventData.changeValue);
			}
			switch (eventData.changeValue)
			{
			case float floatValue:
				return GetCachedFloatString(floatValue);
			case double doubleValue:
				return GetCachedFloatString((float)doubleValue);
			default:
				return eventData.changeValue.ToString();
			}
		}

		public void Refresh()
		{
			DestroyBalloonInstances();
			_balloonDict.Clear();
			_balloonRectCache.Clear();
			_eventIdToBalloonMap.Clear();
			_balloonToTicksMap.Clear();
			_ticksGroupsCache.Clear();
			_eventDataMapCache.Clear();
			_eventValueStringCache.Clear();
			_focusTicksBalloonPositionCacheValid = false;
		}

		public RectTransform GetBalloonInstanceRectTransform()
		{
			foreach (RectTransform rectTransform in _balloonRectCache.Values)
			{
				if (rectTransform != null)
				{
					return rectTransform;
				}
			}
			foreach (KeyValuePair<long, MusicEventBalloonPreview> pair in _balloonDict)
			{
				if (pair.Value == null)
				{
					continue;
				}
				RectTransform rectTransform = pair.Value.GetComponent<RectTransform>();
				if (rectTransform != null)
				{
					_balloonRectCache[pair.Key] = rectTransform;
					return rectTransform;
				}
			}
			return null;
		}

		public RectTransform FindBalloonRectTransformFromId(int eventDataID)
		{
			if (!_eventIdToBalloonMap.TryGetValue(eventDataID, out MusicEventBalloonPreview balloon) || balloon == null)
			{
				return null;
			}
			if (_balloonToTicksMap.TryGetValue(balloon, out long ticks) && _balloonRectCache.TryGetValue(ticks, out RectTransform cachedRectTransform))
			{
				return cachedRectTransform;
			}
			RectTransform rectTransform = balloon.GetComponent<RectTransform>();
			if (rectTransform != null && _balloonToTicksMap.TryGetValue(balloon, out ticks))
			{
				_balloonRectCache[ticks] = rectTransform;
			}
			return rectTransform;
		}

		public MusicScoreEventsPreview()
		{
			_balloonDict = new Dictionary<long, MusicEventBalloonPreview>();
			_balloonRectCache = new Dictionary<long, RectTransform>();
			_eventIdToBalloonMap = new Dictionary<int, MusicEventBalloonPreview>();
			_balloonToTicksMap = new Dictionary<MusicEventBalloonPreview, long>();
			_ticksGroupsCache = new Dictionary<long, List<MusicScoreEventData>>();
			_eventDataMapCache = new Dictionary<MusicScoreEventType, (int id, string value)>();
			_eventValueStringCache = new Dictionary<float, string>();
			_existingTicksSetCache = new HashSet<long>();
			_ticksToRemoveCache = new List<long>();
		}

		private void DestroyBalloonInstances()
		{
			foreach (MusicEventBalloonPreview balloon in _balloonDict.Values)
			{
				if (balloon != null)
				{
					balloon.Dispose();
					Destroy(balloon.gameObject);
				}
			}
		}
	}
}
