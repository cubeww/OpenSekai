using System.Collections.Generic;
using Sekai.MusicScoreMaker.Ingame.Models;
using Sekai.MusicScoreMaker.Ingame.Utilities;
using UnityEngine;

namespace Sekai.MusicScoreMaker.Ingame.Views
{
	public class InvalidPlacementMarkersView : MonoBehaviour
	{
		[SerializeField]
		private GameObject _prefab;

		[SerializeField]
		private float _height;

		private readonly List<RectTransform> _instances;

		private RectTransform _container;

		private bool _disposed;

		private Vector2 _cachedPosition;

		private Vector2 _cachedSize;

		private long _lastStartTicks;

		private long _lastEndTicks;

		private int _lastInvalidPlacementCount;

		private int _lastVisiblePlacementsHash;

		private void ResetCache()
		{
			_lastStartTicks = long.MinValue;
			_lastEndTicks = long.MinValue;
			_lastInvalidPlacementCount = -1;
			_lastVisiblePlacementsHash = -1;
		}

		private bool ShouldSkipUpdate(long startTicks, long endTicks, int count, int visiblePlacementsHash)
		{
			return _lastStartTicks == startTicks && _lastEndTicks == endTicks && _lastInvalidPlacementCount == count && _lastVisiblePlacementsHash == visiblePlacementsHash;
		}

		private void UpdateCache(long startTicks, long endTicks, int count, int visiblePlacementsHash)
		{
			_lastStartTicks = startTicks;
			_lastEndTicks = endTicks;
			_lastInvalidPlacementCount = count;
			_lastVisiblePlacementsHash = visiblePlacementsHash;
		}

		public void Setup()
		{
			if (_disposed)
			{
				_disposed = false;
			}
			_container = transform as RectTransform;
			ResetCache();
			SetActiveRange(0);
			ResetCache();
		}

		public void Setup(RectTransform container, GameObject prefab, float height)
		{
			_container = container;
			_prefab = prefab;
			_height = height;
			if (_disposed)
			{
				_disposed = false;
			}
			ResetCache();
			SetActiveRange(0);
			ResetCache();
		}

		public void UpdateMarkers(MusicScoreMakerData musicScore, long startTicks, long endTicks)
		{
			if (_disposed)
			{
				return;
			}
			if (musicScore?.InvalidPlacements == null || _prefab == null)
			{
				SetActiveRange(0);
				return;
			}
			int visibleHash = CalculateVisibleHash(musicScore.InvalidPlacements, startTicks, endTicks);
			int count = musicScore.InvalidPlacements.Count;
			if (ShouldSkipUpdate(startTicks, endTicks, count, visibleHash))
			{
				return;
			}
			UpdateCache(startTicks, endTicks, count, visibleHash);
			RectTransform parent = _container != null ? _container : transform as RectTransform;
			if (parent == null)
			{
				SetActiveRange(0);
				return;
			}
			Rect parentRect = parent.rect;
			Vector2 parentPosition = parent.anchoredPosition;
			int index = 0;
			foreach (InvalidPlacementInfo info in musicScore.InvalidPlacements)
			{
				if (info == null || info.Type == InvalidPlacementType.ComboCountUnderflow || info.Type == InvalidPlacementType.ComboCountOverflow || info.Type == InvalidPlacementType.TimeSignatureOffset)
				{
					continue;
				}
				long visibleStart = info.Ticks;
				long visibleEnd = IsRangeType(info.Type) ? info.EndTicks : info.Ticks;
				if (visibleStart > endTicks || visibleEnd < startTicks)
				{
					continue;
				}
				RectTransform marker = GetOrCreate(index, parent);
				if (marker == null)
				{
					continue;
				}
				Vector2 position;
				Vector2 size;
				if (IsRangeType(info.Type))
				{
					(float centerX, float width) = MusicScoreMakerUtility.CalcPreviewCenterXAndWidth(0, MusicScoreMakerModel.LaneCountMinus1, parentRect.width);
					float startY = MusicScoreMakerUtility.CalcPreviewPositionYFromTicks(startTicks, endTicks, parentRect.size, parentPosition, visibleStart);
					float endY = MusicScoreMakerUtility.CalcPreviewPositionYFromTicks(startTicks, endTicks, parentRect.size, parentPosition, visibleEnd);
					position = new Vector2(centerX, (startY + endY) * 0.5f);
					size = new Vector2(width, Mathf.Max(Mathf.Abs(endY - startY), _height));
				}
				else
				{
					(float centerX, float width) = MusicScoreMakerUtility.CalcPreviewCenterXAndWidth(info.OverlapLaneStart, info.OverlapLaneEnd, parentRect.width);
					float y = MusicScoreMakerUtility.CalcPreviewPositionYFromTicks(startTicks, endTicks, parentRect.size, parentPosition, visibleStart);
					position = new Vector2(centerX, y);
					size = new Vector2(width, _height);
				}
				_cachedPosition = position;
				_cachedSize = size;
				marker.anchoredPosition = position;
				marker.sizeDelta = size;
				index++;
			}
			SetActiveRange(index);
		}

		public void Clear()
		{
			if (_disposed)
			{
				return;
			}
			SetActiveRange(0);
			ResetCache();
		}

		public void Dispose()
		{
			if (_disposed)
			{
				return;
			}
			foreach (RectTransform instance in _instances)
			{
				if (instance != null)
				{
					Destroy(instance.gameObject);
				}
			}
			_instances.Clear();
			_container = null;
			_prefab = null;
			ResetCache();
			_disposed = true;
		}

		private void OnDestroy()
		{
			Dispose();
		}

		private RectTransform GetOrCreate(int index, RectTransform parent)
		{
			if (index < _instances.Count && _instances[index] != null)
			{
				if (!_instances[index].gameObject.activeSelf)
				{
					_instances[index].gameObject.SetActive(true);
				}
				return _instances[index];
			}
			if (_prefab == null || parent == null)
			{
				return null;
			}
			GameObject instance = Instantiate(_prefab, parent);
			RectTransform rectTransform = instance.transform as RectTransform;
			if (rectTransform == null)
			{
				rectTransform = instance.AddComponent<RectTransform>();
				rectTransform.SetParent(parent, false);
			}
			if (index < _instances.Count)
			{
				_instances[index] = rectTransform;
			}
			else
			{
				_instances.Add(rectTransform);
			}
			return rectTransform;
		}

		private void SetActiveRange(int activeCount)
		{
			for (int i = 0; i < _instances.Count; i++)
			{
				RectTransform instance = _instances[i];
				if (instance == null)
				{
					continue;
				}
				bool shouldBeActive = i < activeCount;
				if (instance.gameObject.activeSelf != shouldBeActive)
				{
					instance.gameObject.SetActive(shouldBeActive);
				}
			}
		}

		private static bool IsRangeType(InvalidPlacementType type)
		{
			return type == InvalidPlacementType.JudgmentNoteGap || type == InvalidPlacementType.NoteDensityOverflow || type == InvalidPlacementType.LongNoteMeshOverflow || type == InvalidPlacementType.GuideMeshOverflow;
		}

		public InvalidPlacementMarkersView()
		{
			_height = 6f;
			_instances = new List<RectTransform>(64);
			ResetCache();
		}

		private static int CalculateVisibleHash(List<InvalidPlacementInfo> placements, long startTicks, long endTicks)
		{
			int hash = 0;
			foreach (InvalidPlacementInfo info in placements)
			{
				if (info == null)
				{
					continue;
				}
				long visibleStart = info.Ticks;
				long visibleEnd = IsRangeType(info.Type) ? info.EndTicks : info.Ticks;
				if (visibleStart > endTicks || visibleEnd < startTicks)
				{
					continue;
				}
				int itemHash = 31 * (info.OverlapLaneStart + 31 * (int)(visibleStart & int.MaxValue)) + info.OverlapLaneEnd;
				if (IsRangeType(info.Type))
				{
					itemHash = ((int)(visibleEnd & int.MaxValue) ^ (int)(visibleStart & int.MaxValue)) + 53 * itemHash;
				}
				hash = itemHash + 397 * hash;
			}
			return hash;
		}
	}
}
