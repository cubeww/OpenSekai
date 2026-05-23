using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sekai.MusicScoreMaker.Ingame.Events;
using Sekai.MusicScoreMaker.Ingame.Models;
using Sekai.MusicScoreMaker.Ingame.Utilities;
using Sekai.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Sekai.MusicScoreMaker.Ingame.Views
{
	public class BarLinePreview : MonoBehaviour
	{
		private class TimeSignatureInfo
		{
			public int Numerator
			{
				[CompilerGenerated]
				get
				{
					return _numerator;
				}
				[CompilerGenerated]
				private set
				{
					_numerator = value;
				}
			}

			public int Denominator
			{
				[CompilerGenerated]
				get
				{
					return _denominator;
				}
				[CompilerGenerated]
				private set
				{
					_denominator = value;
				}
			}

			private int _numerator;

			private int _denominator;

			public TimeSignatureInfo(int numerator, int denominator)
			{
				Numerator = Mathf.Max(1, numerator);
				Denominator = Mathf.Max(1, denominator);
			}

			public bool IsCompoundTimeSignature()
			{
				return Numerator >= 6 && Numerator % 3 == 0 && Denominator >= 8;
			}

			public int GetBaseUnit()
			{
				return IsCompoundTimeSignature() ? 3 : 1;
			}

			public int GetGroupCount()
			{
				return IsCompoundTimeSignature() ? Numerator / 3 : Numerator;
			}
		}

		private const int MAX_QUANTIZE_LINES_PER_VIEW = 48;

		private const int MAX_BAR_LINES_PER_VIEW = 30;

		private const int MAX_BEAT_LINES_PER_VIEW = 48;

		private const int INVALID_BAR_NUMBER = -1;

		[SerializeField]
		private RectTransform _barLinePrefab;

		[SerializeField]
		private RectTransform _beatLinePrefab;

		[SerializeField]
		private RectTransform _quantizeLinePrefab;

		[SerializeField]
		private CustomTextMesh _barNumberPrefab;

		private List<RectTransform> _barLinePool;

		private List<RectTransform> _beatLinePool;

		private List<RectTransform> _quantizeLinePool;

		private List<CustomTextMesh> _barNumberPool;

		private RectTransform _rectTransform;

		private Dictionary<(int, int), TimeSignatureInfo> _timeSignatureCache;

		private Dictionary<long, TimeSignatureInfo> _timeSignatureByTicksCache;

		private List<long> _sortedTimeSignatureTicksCache;

		public void Setup()
		{
			if (_rectTransform == null)
			{
				_rectTransform = GetComponent<RectTransform>();
			}
			MusicScoreMakerEventDispatcher.Instance.Register<UpdateBarLinePreviewEvent>(OnUpdateBarLinePreview);
		}

		private void OnDestroy()
		{
			if (MusicScoreMakerEventDispatcher.ExistsInstance)
			{
				MusicScoreMakerEventDispatcher.Instance.Remove<UpdateBarLinePreviewEvent>(OnUpdateBarLinePreview);
			}
		}

		private void OnUpdateBarLinePreview(UpdateBarLinePreviewEvent arg)
		{
			float scale = MusicScoreMakerUtility.GetCurrentMusicScoreScale();
			long startTicks = MusicScoreMakerUtility.GetCurrentMusicScoreStartTicks();
			MusicScoreMakerData data = MusicScoreMakerUtility.GetMusicScoreMakerData();
			if (data == null)
			{
				return;
			}
			long endTicks = startTicks + (long)(scale * MusicScoreMakerUtility.TICKS_PER_BAR);
			UpdateView(scale, startTicks, endTicks, data.MusicScoreEventDataList);
		}

		public void UpdateView(float scale, long startTicks, long endTicks, List<MusicScoreEventData> musicScoreEventDataArray, bool showQuantizeLines = true, bool showBarNumbers = true)
		{
			if (scale <= 0f)
			{
				return;
			}
			if (_rectTransform == null)
			{
				Setup();
			}
			if (_rectTransform == null)
			{
				return;
			}
			DisplayBarAndBeatLines(startTicks, endTicks, _rectTransform.rect.height, musicScoreEventDataArray, showQuantizeLines, showBarNumbers);
		}

		private void DisplayBarAndBeatLines(long startTicks, long endTicks, float height, List<MusicScoreEventData> musicScoreEventDataArray, bool showQuantizeLines = true, bool showBarNumbers = true)
		{
			bool showQuantize = showQuantizeLines && !CheckSkipQuantizeLines(startTicks, endTicks);
			Dictionary<long, TimeSignatureInfo> timeSignatureByTicks = BuildTimeSignatureMap(musicScoreEventDataArray);
			_sortedTimeSignatureTicksCache.Clear();
			foreach (long ticks in timeSignatureByTicks.Keys)
			{
				_sortedTimeSignatureTicksCache.Add(ticks);
			}
			_sortedTimeSignatureTicksCache.Sort();
			GetInitialBarState(startTicks, _sortedTimeSignatureTicksCache, timeSignatureByTicks, out TimeSignatureInfo currentTimeSignature, out long currentBarStartTicks, out int barNumber);
			bool skipBeatLines = CheckSkipBeatLines(startTicks, endTicks, currentTimeSignature, _sortedTimeSignatureTicksCache, timeSignatureByTicks);
			int barDisplayInterval = CalculateBarDisplayInterval(startTicks, endTicks, currentTimeSignature, _sortedTimeSignatureTicksCache, timeSignatureByTicks);
			int barLineIndex = 0;
			int beatLineIndex = 0;
			int quantizeLineIndex = 0;
			long barStartTicks = currentBarStartTicks;
			while (barStartTicks <= endTicks)
			{
				if (timeSignatureByTicks.TryGetValue(barStartTicks, out TimeSignatureInfo changedTimeSignature))
				{
					currentTimeSignature = changedTimeSignature;
				}
				float positionY = CalculatePositionYFromTicks(barStartTicks, startTicks, endTicks, height);
				bool shouldDisplayBar = barDisplayInterval < 2 || barNumber % barDisplayInterval == 0;
				if (MusicScoreMakerSettingsManager.ShowBarLines && shouldDisplayBar)
				{
					barLineIndex = DisplayBarLine(barLineIndex, positionY, 1f, showBarNumbers ? barNumber : INVALID_BAR_NUMBER);
				}
				if (!skipBeatLines)
				{
					beatLineIndex = DisplayBeatLinesForBar(barStartTicks, startTicks, endTicks, height, currentTimeSignature, barLineIndex, beatLineIndex);
				}
				if (showQuantize)
				{
					quantizeLineIndex = DisplayQuantizeLinesForBar(barStartTicks, startTicks, endTicks, height, currentTimeSignature, quantizeLineIndex);
				}
				long nextBarStartTicks = barStartTicks + GetBarLengthTicks(currentTimeSignature);
				foreach (long timeSignatureTicks in _sortedTimeSignatureTicksCache)
				{
					if (timeSignatureTicks > barStartTicks && timeSignatureTicks < nextBarStartTicks)
					{
						nextBarStartTicks = timeSignatureTicks;
						if (timeSignatureByTicks.TryGetValue(timeSignatureTicks, out TimeSignatureInfo innerChange))
						{
							currentTimeSignature = innerChange;
						}
						break;
					}
				}
				if (nextBarStartTicks <= barStartTicks)
				{
					break;
				}
				barStartTicks = nextBarStartTicks;
				barNumber++;
			}
			HideUnusedLines(barLineIndex, beatLineIndex, quantizeLineIndex);
		}

		private static bool CheckSkipQuantizeLines(long startTicks, long endTicks)
		{
			if (!MusicScoreMakerSettingsManager.ShowQuantizeLines)
			{
				return false;
			}
			long quantizeTicks = MusicScoreMakerUtility.GetQuantizeTicks();
			return quantizeTicks >= 1 && (endTicks - startTicks) / quantizeTicks > MAX_QUANTIZE_LINES_PER_VIEW;
		}

		private static int EstimateBeatLinesPerBar(TimeSignatureInfo timeSignature)
		{
			if (timeSignature == null)
			{
				return 0;
			}
			int result = timeSignature.IsCompoundTimeSignature() ? timeSignature.Numerator + timeSignature.Numerator / 3 - 2 : timeSignature.Numerator - 1;
			if (timeSignature.Denominator >= 5)
			{
				int subdivision = (timeSignature.Denominator >> 2) - 1;
				result += subdivision + subdivision * timeSignature.Numerator;
			}
			return Mathf.Max(0, result);
		}

		private bool CheckSkipBeatLines(long startTicks, long endTicks, TimeSignatureInfo initialTimeSignature, List<long> sortedTimeSignatureTicks, Dictionary<long, TimeSignatureInfo> timeSignatureByTicks)
		{
			if (endTicks - startTicks < 1L || initialTimeSignature == null)
			{
				return false;
			}
			float maxDensity = EstimateBeatLinesPerBar(initialTimeSignature) / Mathf.Max(1f, GetBarLengthTicks(initialTimeSignature));
			foreach (long ticks in sortedTimeSignatureTicks)
			{
				if (ticks > endTicks)
				{
					break;
				}
				if (ticks > startTicks && timeSignatureByTicks.TryGetValue(ticks, out TimeSignatureInfo timeSignature))
				{
					maxDensity = Mathf.Max(maxDensity, EstimateBeatLinesPerBar(timeSignature) / Mathf.Max(1f, GetBarLengthTicks(timeSignature)));
				}
			}
			return maxDensity * (endTicks - startTicks) > MAX_BEAT_LINES_PER_VIEW;
		}

		private int CalculateBarDisplayInterval(long startTicks, long endTicks, TimeSignatureInfo initialTimeSignature, List<long> sortedTimeSignatureTicks, Dictionary<long, TimeSignatureInfo> timeSignatureByTicks)
		{
			if (endTicks - startTicks < 1L || initialTimeSignature == null)
			{
				return 1;
			}
			long minBarLength = GetBarLengthTicks(initialTimeSignature);
			if (minBarLength < 1L)
			{
				minBarLength = 1L;
			}
			foreach (long ticks in sortedTimeSignatureTicks)
			{
				if (ticks > endTicks)
				{
					break;
				}
				if (ticks > startTicks && timeSignatureByTicks.TryGetValue(ticks, out TimeSignatureInfo timeSignature))
				{
					long barLength = GetBarLengthTicks(timeSignature);
					if (barLength < 1L)
					{
						barLength = 1L;
					}
					if (barLength < minBarLength)
					{
						minBarLength = barLength;
					}
				}
			}
			int estimatedBarCount = (int)((endTicks - startTicks) / minBarLength) + 1;
			if (estimatedBarCount < MAX_BAR_LINES_PER_VIEW + 1)
			{
				return 1;
			}
			int interval = 1;
			while (estimatedBarCount / interval > MAX_BAR_LINES_PER_VIEW)
			{
				interval *= 2;
			}
			return interval;
		}

		private Dictionary<long, TimeSignatureInfo> BuildTimeSignatureMap(List<MusicScoreEventData> musicScoreEventDataArray)
		{
			_timeSignatureByTicksCache.Clear();
			if (musicScoreEventDataArray == null)
			{
				return _timeSignatureByTicksCache;
			}
			foreach (MusicScoreEventData eventData in musicScoreEventDataArray)
			{
				if (eventData == null || eventData.eventType != MusicScoreEventType.TimeSignature)
				{
					continue;
				}
				(int numerator, int denominator) signature = MusicScoreMakerUtility.GetTimeSignatureFromChangeValue(eventData.changeValue);
				var key = (Mathf.Max(1, signature.numerator), Mathf.Max(1, signature.denominator));
				if (!_timeSignatureCache.TryGetValue(key, out TimeSignatureInfo info))
				{
					info = new TimeSignatureInfo(key.Item1, key.Item2);
					_timeSignatureCache[key] = info;
				}
				_timeSignatureByTicksCache[eventData.ticks] = info;
			}
			return _timeSignatureByTicksCache;
		}

		private void GetInitialBarState(long startTicks, List<long> sortedTimeSignatureTicks, Dictionary<long, TimeSignatureInfo> timeSignatureByTicks, out TimeSignatureInfo currentTimeSignature, out long currentBarStartTicks, out int barNumber)
		{
			currentTimeSignature = new TimeSignatureInfo(4, 4);
			currentBarStartTicks = 0L;
			barNumber = 0;
			long segmentStartTicks = 0L;
			if (sortedTimeSignatureTicks != null)
			{
				foreach (long changeTicks in sortedTimeSignatureTicks)
				{
					if (changeTicks > startTicks)
					{
						break;
					}
					long barLength = GetBarLengthTicks(currentTimeSignature);
					if (barLength >= 1L)
					{
						barNumber += (int)((changeTicks - segmentStartTicks) / barLength);
						currentBarStartTicks = changeTicks;
					}
					if (timeSignatureByTicks != null && timeSignatureByTicks.TryGetValue(changeTicks, out TimeSignatureInfo changedSignature))
					{
						currentTimeSignature = changedSignature;
					}
					segmentStartTicks = changeTicks;
				}
			}
			long currentBarLength = GetBarLengthTicks(currentTimeSignature);
			if (currentBarLength >= 1L)
			{
				long offsetBars = (startTicks - segmentStartTicks) / currentBarLength;
				barNumber += (int)offsetBars;
				currentBarStartTicks = segmentStartTicks + offsetBars * currentBarLength;
			}
		}

		private int DisplayBeatLinesForBar(long barStartTicks, long startTicks, long endTicks, float height, TimeSignatureInfo timeSignature, int barLineIndex, int beatLineIndex, bool skipAdditionalBarLines = false)
		{
			if (!MusicScoreMakerSettingsManager.ShowBeatLines || timeSignature == null)
			{
				return beatLineIndex;
			}
			long barLength = GetBarLengthTicks(timeSignature);
			if (barLength <= 0L)
			{
				return beatLineIndex;
			}
			float alpha = Mathf.Max(Mathf.Min(4f / timeSignature.Denominator * 0.5f, 0.5f), 0.2f);
			for (int i = 1; i < timeSignature.Numerator; i++)
			{
				long ticks = barStartTicks + (long)(i / (float)timeSignature.Numerator * barLength);
				if (ticks >= startTicks && ticks <= endTicks)
				{
					beatLineIndex = DisplayBeatLine(beatLineIndex, CalculatePositionYFromTicks(ticks, startTicks, endTicks, height), alpha);
				}
			}
			if (timeSignature.IsCompoundTimeSignature())
			{
				float groupAlpha = Mathf.Max(Mathf.Min(4f / timeSignature.Denominator * 0.7f, 0.7f), 0.4f);
				int groupCount = timeSignature.GetGroupCount();
				for (int group = 2; group <= groupCount; group++)
				{
					long ticks = barStartTicks + (long)(timeSignature.GetBaseUnit() * (group - 1) / (float)timeSignature.Numerator * barLength);
					if (ticks >= startTicks && ticks <= endTicks)
					{
						beatLineIndex = DisplayBeatLine(beatLineIndex, CalculatePositionYFromTicks(ticks, startTicks, endTicks, height), groupAlpha);
					}
				}
			}
			return beatLineIndex;
		}

		private int DisplayQuantizeLinesForBar(long barStartTicks, long startTicks, long endTicks, float height, TimeSignatureInfo timeSignature, int quantizeLineIndex)
		{
			if (!MusicScoreMakerSettingsManager.ShowQuantizeLines || timeSignature == null || quantizeLineIndex > MAX_QUANTIZE_LINES_PER_VIEW - 1)
			{
				return quantizeLineIndex;
			}
			long barLength = GetBarLengthTicks(timeSignature);
			if (barLength <= 0L)
			{
				return quantizeLineIndex;
			}
			long quantizeTicks = MusicScoreMakerUtility.GetQuantizeTicks();
			if (quantizeTicks <= 0L)
			{
				quantizeTicks = MusicScoreMakerUtility.TICKS_PER_BAR;
			}
			long endBarTicks = barStartTicks + barLength;
			for (long ticks = barStartTicks; ticks < endBarTicks; ticks += quantizeTicks)
			{
				if (ticks == barStartTicks || ticks < startTicks || ticks > endTicks)
				{
					continue;
				}
				bool nearBeatLine = false;
				long beatTolerance = quantizeTicks / 4L;
				for (int beat = 1; beat < timeSignature.Numerator; beat++)
				{
					long beatTicks = barStartTicks + (long)(beat / (float)timeSignature.Numerator * barLength);
					if (Mathf.Abs((float)(ticks - beatTicks)) < beatTolerance)
					{
						nearBeatLine = true;
						break;
					}
				}
				if (nearBeatLine)
				{
					continue;
				}
				quantizeLineIndex = DisplayQuantizeLine(quantizeLineIndex, CalculatePositionYFromTicks(ticks, startTicks, endTicks, height), 0.15f);
				if (quantizeLineIndex > MAX_QUANTIZE_LINES_PER_VIEW - 1)
				{
					break;
				}
			}
			return quantizeLineIndex;
		}

		private void HideUnusedLines(int barLineCount, int beatLineCount, int quantizeLineCount)
		{
			SetPoolActiveRange(_barLinePool, barLineCount);
			SetPoolActiveRange(_beatLinePool, beatLineCount);
			SetPoolActiveRange(_quantizeLinePool, quantizeLineCount);
			for (int i = barLineCount; i < _barNumberPool.Count; i++)
			{
				if (_barNumberPool[i] != null)
				{
					_barNumberPool[i].gameObject.SetActive(false);
				}
			}
		}

		private int DisplayBarLine(int index, float positionY, float alpha, int barNumber = -1)
		{
			RectTransform line = GetOrCreateRect(_barLinePool, _barLinePrefab, index);
			if (line == null)
			{
				return index;
			}
			SetLine(line, line.anchoredPosition.x, positionY, alpha);
			SetBarNumber(index, positionY, barNumber);
			return index + 1;
		}

		private void SetBarNumber(int index, float positionY, int barNumber)
		{
			if (barNumber != INVALID_BAR_NUMBER && MusicScoreMakerSettingsManager.ShowBarLines)
			{
				CustomTextMesh text = GetOrCreateBarNumber(index);
				if (text == null)
				{
					return;
				}
				RectTransform rectTransform = text.rectTransform;
				Vector2 position = rectTransform.anchoredPosition;
				position.y = positionY;
				rectTransform.anchoredPosition = position;
				string barNumberText = (barNumber + 1).ToString();
				if (text.text != barNumberText)
				{
					text.SetText(barNumberText);
				}
				text.gameObject.SetActive(true);
			}
			else if (index < _barNumberPool.Count && _barNumberPool[index] != null)
			{
				_barNumberPool[index].gameObject.SetActive(false);
			}
		}

		private int DisplayBeatLine(int index, float positionY, float alpha)
		{
			RectTransform line = GetOrCreateRect(_beatLinePool, _beatLinePrefab, index);
			if (line == null)
			{
				return index;
			}
			SetLine(line, 0f, positionY, alpha);
			return index + 1;
		}

		private int DisplayQuantizeLine(int index, float positionY, float alpha)
		{
			RectTransform line = GetOrCreateRect(_quantizeLinePool, _quantizeLinePrefab, index);
			if (line == null)
			{
				return index;
			}
			SetLine(line, 0f, positionY, alpha);
			return index + 1;
		}

		private float CalculatePositionYFromTicks(long ticks, long startTicks, long endTicks, float height)
		{
			return MusicScoreMakerUtility.CalcNormalizedPositionFromTicks(startTicks, endTicks, ticks) * height;
		}

		private long GetBarLengthTicks(TimeSignatureInfo timeSignature)
		{
			if (timeSignature == null)
			{
				return MusicScoreMakerUtility.TICKS_PER_BAR;
			}
			return (long)(MusicScoreMakerUtility.TICKS_PER_BAR * timeSignature.Numerator / (float)timeSignature.Denominator);
		}

		public BarLinePreview()
		{
			_barLinePool = new List<RectTransform>();
			_beatLinePool = new List<RectTransform>();
			_quantizeLinePool = new List<RectTransform>();
			_barNumberPool = new List<CustomTextMesh>();
			_timeSignatureCache = new Dictionary<(int, int), TimeSignatureInfo>();
			_timeSignatureByTicksCache = new Dictionary<long, TimeSignatureInfo>();
			_sortedTimeSignatureTicksCache = new List<long>();
		}

		private RectTransform GetOrCreateRect(List<RectTransform> pool, RectTransform prefab, int index)
		{
			if (pool == null || prefab == null || index < 0)
			{
				return null;
			}
			while (pool.Count <= index)
			{
				pool.Add(Instantiate(prefab, transform));
			}
			return pool[index];
		}

		private CustomTextMesh GetOrCreateBarNumber(int index)
		{
			if (_barNumberPrefab == null || index < 0)
			{
				return null;
			}
			while (_barNumberPool.Count <= index)
			{
				_barNumberPool.Add(Instantiate(_barNumberPrefab, transform));
			}
			return _barNumberPool[index];
		}

		private static void SetLine(RectTransform line, float x, float y, float alpha)
		{
			Vector2 position = line.anchoredPosition;
			position.x = x;
			position.y = y;
			line.anchoredPosition = position;
			Image image = line.GetComponent<Image>();
			if (image != null)
			{
				Color color = image.color;
				color.a = alpha;
				image.color = color;
			}
			line.gameObject.SetActive(true);
		}

		private static void SetPoolActiveRange(List<RectTransform> pool, int activeCount)
		{
			if (pool == null)
			{
				return;
			}
			for (int i = activeCount; i < pool.Count; i++)
			{
				if (pool[i] != null)
				{
					pool[i].gameObject.SetActive(false);
				}
			}
		}
	}
}
