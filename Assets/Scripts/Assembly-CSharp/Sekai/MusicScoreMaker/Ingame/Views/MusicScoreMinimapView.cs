using System.Collections.Generic;
using Sekai.Live;
using Sekai.MusicScoreMaker.Ingame.Events;
using Sekai.MusicScoreMaker.Ingame.Models;
using Sekai.MusicScoreMaker.Ingame.Utilities;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Sekai.MusicScoreMaker.Ingame.Views
{
	public class MusicScoreMinimapView : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IDragHandler
	{
		[SerializeField]
		private RawImage _rawImage;

		[SerializeField]
		private Material _minimapMaterial;

		[SerializeField]
		private RectTransform _previewRectTransform;

		[SerializeField]
		private Sprite _viewportFrameSprite;

		private const int TEXTURE_WIDTH = 72;

		private const int LANE_PIXEL_WIDTH = 6;

		private const int PIXELS_PER_BEAT = 2;

		private const int MAX_TEXTURE_HEIGHT = 4096;

		private const int MIN_TEXTURE_HEIGHT = 64;

		private const long TICKS_PER_PIXEL = 240L;

		private const int NOTE_HEIGHT_PIXELS = 2;

		private const int DISPLAY_RANGE_BARS = 30;

		private static readonly Color32 ColorNoteNormal;

		private static readonly Color32 ColorNoteNormalCritical;

		private static readonly Color32 ColorNoteFlick;

		private static readonly Color32 ColorNoteFlickCritical;

		private static readonly Color32 ColorNoteLong;

		private static readonly Color32 ColorNoteLongCritical;

		private static readonly Color32 ColorNoteGuide;

		private static readonly Color32 ColorNoteGuideCritical;

		private static readonly Color32 ColorLongBand;

		private static readonly Color32 ColorLongBandCritical;

		private static readonly Color32 ColorBarLine;

		private static readonly Color32 ColorBackground;

		private static readonly Color ViewportFrameColor;

		private const float VIEWPORT_FRAME_MARGIN = 6f;

		private Texture2D _minimapTexture;

		private Color32[] _pixelBuffer;

		private Material _materialInstance;

		private Image _viewportFrameImage;

		private RectTransform _viewportFrameRect;

		private readonly Vector3[] _previewCorners;

		private readonly Vector3[] _parentCorners;

		private float _cachedViewportStart;

		private float _cachedViewportEnd;

		private bool _isDirty;

		private int _currentTextureHeight;

		private long _cachedMaxTicks;

		private bool _isSetup;

		private long _displayStartTicks;

		private long _displayEndTicks;

		private long _displayRangeTicks;

		private long _lastRebuildStartTicks;

		private long _cachedDataFingerprint;

		private static readonly IsMusicPlayingEvent IsMusicPlayingEventCache;

		private static readonly GetMusicScoreMakerDataEvent GetMusicScoreMakerDataEventCache;

		private static readonly SetFocusTicksEvent SetFocusTicksEventCache;

		public void Setup()
		{
			if (_rawImage == null || _minimapMaterial == null)
			{
				return;
			}
			_materialInstance = new Material(_minimapMaterial);
			_rawImage.material = _materialInstance;
			CreateViewportFrame();
			MusicScoreMakerEventDispatcher.Instance.Register<InvalidateMaxFocusableTicksCacheEvent>(OnMaxTicksCacheInvalidated);
			_isDirty = true;
			_isSetup = true;
		}

		public void Dispose()
		{
			_isSetup = false;
			if (MusicScoreMakerEventDispatcher.ExistsInstance)
			{
				MusicScoreMakerEventDispatcher.Instance.Remove<InvalidateMaxFocusableTicksCacheEvent>(OnMaxTicksCacheInvalidated);
			}
			if (_rawImage != null)
			{
				_rawImage.texture = null;
				_rawImage.material = null;
			}
			if (_minimapTexture != null)
			{
				Destroy(_minimapTexture);
				_minimapTexture = null;
			}
			if (_materialInstance != null)
			{
				Destroy(_materialInstance);
				_materialInstance = null;
			}
			if (_viewportFrameImage != null)
			{
				Destroy(_viewportFrameImage.gameObject);
				_viewportFrameImage = null;
				_viewportFrameRect = null;
			}
			_pixelBuffer = null;
		}

		public void UpdateView()
		{
			if (!_isSetup)
			{
				return;
			}
			UpdateDisplayRange();
			MusicScoreMakerData data = MusicScoreMakerEventDispatcher.Instance.PublishFirst<GetMusicScoreMakerDataEvent, MusicScoreMakerData>(GetMusicScoreMakerDataEventCache);
			if (data != null)
			{
				long fingerprint = ComputeDataFingerprint(data);
				if (fingerprint != _cachedDataFingerprint)
				{
					_isDirty = true;
					_cachedDataFingerprint = fingerprint;
				}
			}
			if (!_isDirty && _lastRebuildStartTicks >= 0L && _displayStartTicks != _lastRebuildStartTicks)
			{
				_isDirty = true;
			}
			if (_isDirty && RebuildTexture(data))
			{
				_isDirty = false;
			}
			UpdateViewportIndicator();
		}

		private void LateUpdate()
		{
			UpdateView();
		}

		private void UpdateDisplayRange()
		{
			long oldStartTicks = _displayStartTicks;
			long oldEndTicks = _displayEndTicks;
			long oldRangeTicks = _displayRangeTicks;
			long oldMaxTicks = _cachedMaxTicks;

			long maxTicks = MusicScoreMakerUtility.GetMusicScoreTicksMax();
			long focusTicks = MusicScoreMakerUtility.GetFocusTicks();
			long displayWindowTicks = DISPLAY_RANGE_BARS * MusicScoreMakerUtility.TICKS_PER_BAR;

			_displayRangeTicks = displayWindowTicks;
			_cachedMaxTicks = maxTicks;

			long focusOffsetTicks = maxTicks < 1L
				? 0L
				: (long)(focusTicks / (float)maxTicks * displayWindowTicks);
			_displayStartTicks = focusTicks - focusOffsetTicks;
			_displayEndTicks = _displayStartTicks + displayWindowTicks;

			if (_displayStartTicks < 0L)
			{
				_displayStartTicks = 0L;
				_displayEndTicks = displayWindowTicks;
			}

			if (maxTicks >= 1L && _displayEndTicks > maxTicks)
			{
				_displayStartTicks = maxTicks <= displayWindowTicks - 1L ? 0L : maxTicks - displayWindowTicks;
				_displayEndTicks = maxTicks;
			}

			if (maxTicks >= 1L && maxTicks < displayWindowTicks)
			{
				_displayStartTicks = 0L;
				_displayEndTicks = maxTicks;
				_displayRangeTicks = maxTicks;
			}

			if (oldStartTicks != _displayStartTicks
				|| oldEndTicks != _displayEndTicks
				|| oldRangeTicks != _displayRangeTicks
				|| oldMaxTicks != _cachedMaxTicks)
			{
				_isDirty = true;
			}
		}

		private void OnMaxTicksCacheInvalidated(InvalidateMaxFocusableTicksCacheEvent evt)
		{
			_isDirty = true;
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			HandlePointerInput(eventData);
		}

		public void OnDrag(PointerEventData eventData)
		{
			HandlePointerInput(eventData);
		}

		private bool RebuildTexture(MusicScoreMakerData data)
		{
			if (data == null || _displayRangeTicks < 1L)
			{
				return false;
			}
			int textureHeight = CalculateTextureHeight(_displayRangeTicks);
			if (_minimapTexture == null || _currentTextureHeight != textureHeight)
			{
				CreateOrResizeTexture(textureHeight);
			}
			_lastRebuildStartTicks = _displayStartTicks;
			ClearPixelBuffer();
			DrawBarLines(data.MusicScoreEventDataList, textureHeight);
			Dictionary<int, MusicScoreNoteBase> noteIdCache = data.GetNoteIdCacheOrRebuild();
			if (data.NoteList != null)
			{
				for (int i = 0; i < data.NoteList.Count; i++)
				{
					MusicScoreNoteBase note = data.NoteList[i];
					if (note == null)
					{
						continue;
					}
					if (note.nextConnectionId != -1)
					{
						DrawLongNoteBand(note, noteIdCache, textureHeight);
					}
					DrawNote(note, textureHeight);
				}
			}
			_minimapTexture.SetPixels32(_pixelBuffer);
			_minimapTexture.Apply(false);
			return true;
		}

		private static int CalculateTextureHeight(long rangeTicks)
		{
			return Mathf.Clamp((int)(rangeTicks / TICKS_PER_PIXEL), MIN_TEXTURE_HEIGHT, MAX_TEXTURE_HEIGHT);
		}

		private void CreateOrResizeTexture(int textureHeight)
		{
			if (_minimapTexture != null)
			{
				Destroy(_minimapTexture);
			}
			_currentTextureHeight = textureHeight;
			_minimapTexture = new Texture2D(TEXTURE_WIDTH, textureHeight, TextureFormat.RGBA32, false);
			_minimapTexture.wrapMode = TextureWrapMode.Clamp;
			_minimapTexture.filterMode = FilterMode.Point;
			_pixelBuffer = new Color32[TEXTURE_WIDTH * textureHeight];
			if (_rawImage != null)
			{
				_rawImage.texture = _minimapTexture;
			}
		}

		private void ClearPixelBuffer()
		{
			if (_pixelBuffer == null)
			{
				return;
			}
			for (int i = 0; i < _pixelBuffer.Length; i++)
			{
				_pixelBuffer[i] = ColorBackground;
			}
		}

		private void DrawNote(MusicScoreNoteBase note, int textureHeight)
		{
			if (note == null || note.isSkip || IsHiddenMinimapCategory(note.category))
			{
				return;
			}
			if (note.ticks < _displayStartTicks || note.ticks > _displayEndTicks)
			{
				return;
			}
			int y = Mathf.Clamp(TicksToPixelY(note.ticks, textureHeight), 0, Mathf.Max(0, textureHeight - NOTE_HEIGHT_PIXELS));
			int xStart = Mathf.Clamp(note.laneStart * LANE_PIXEL_WIDTH, 0, TEXTURE_WIDTH);
			int xEnd = Mathf.Clamp(note.laneEnd * LANE_PIXEL_WIDTH + LANE_PIXEL_WIDTH, 0, TEXTURE_WIDTH);
			Color32 color = GetNoteColor(note.category, note.type);
			for (int dy = 0; dy < NOTE_HEIGHT_PIXELS; dy++)
			{
				int py = y + dy;
				if (py < 0 || py >= textureHeight)
				{
					continue;
				}
				int index = py * TEXTURE_WIDTH + xStart;
				for (int x = xStart; x < xEnd; x++)
				{
					_pixelBuffer[index++] = color;
				}
			}
		}

		private void DrawLongNoteBand(MusicScoreNoteBase note, Dictionary<int, MusicScoreNoteBase> noteIdCache, int textureHeight)
		{
			if (note == null || noteIdCache == null || !noteIdCache.TryGetValue(note.nextConnectionId, out MusicScoreNoteBase next) || next == null)
			{
				return;
			}
			long startTicks = System.Math.Max(_displayStartTicks, note.ticks);
			long endTicks = System.Math.Min(_displayEndTicks, next.ticks);
			if (endTicks < _displayStartTicks || startTicks > _displayEndTicks || endTicks <= startTicks)
			{
				return;
			}
			int yStart = Mathf.Clamp(TicksToPixelY(startTicks, textureHeight), 0, textureHeight - 1);
			int yEnd = Mathf.Clamp(TicksToPixelY(endTicks, textureHeight), 0, textureHeight - 1);
			if (yEnd < yStart)
			{
				(yStart, yEnd) = (yEnd, yStart);
			}
			Color32 color = note.type == NoteType.Critical ? ColorLongBandCritical : ColorLongBand;
			for (int y = yStart; y <= yEnd; y++)
			{
				float rate = endTicks == startTicks ? 0f : (PixelYToTicks(y, textureHeight) - note.ticks) / (float)(next.ticks - note.ticks);
				rate = Mathf.Clamp01(ApplyEasing(rate, note.noteLineType));
				int laneStart = Mathf.RoundToInt(Mathf.Lerp(note.laneStart, next.laneStart, rate));
				int laneEnd = Mathf.RoundToInt(Mathf.Lerp(note.laneEnd, next.laneEnd, rate));
				int xStart = Mathf.Clamp(laneStart * LANE_PIXEL_WIDTH, 0, TEXTURE_WIDTH);
				int xEnd = Mathf.Clamp(laneEnd * LANE_PIXEL_WIDTH + LANE_PIXEL_WIDTH, 0, TEXTURE_WIDTH);
				int index = y * TEXTURE_WIDTH + xStart;
				for (int x = xStart; x < xEnd; x++)
				{
					if (index >= 0 && index < _pixelBuffer.Length)
					{
						_pixelBuffer[index] = color;
					}
					index++;
				}
			}
		}

		private void DrawBarLines(List<MusicScoreEventData> eventList, int textureHeight)
		{
			int numerator = 4;
			int denominator = 4;
			long segmentStart = 0L;
			if (eventList == null || eventList.Count == 0)
			{
				DrawBarLinesInRange(0L, _displayEndTicks, numerator, denominator, textureHeight);
				return;
			}
			List<MusicScoreEventData> timeSignatures = new List<MusicScoreEventData>();
			for (int i = 0; i < eventList.Count; i++)
			{
				MusicScoreEventData evt = eventList[i];
				if (evt != null && evt.eventType == MusicScoreEventType.TimeSignature)
				{
					timeSignatures.Add(evt);
				}
			}
			timeSignatures.Sort((a, b) => a.ticks.CompareTo(b.ticks));
			for (int i = 0; i < timeSignatures.Count; i++)
			{
				MusicScoreEventData evt = timeSignatures[i];
				if (evt.ticks > segmentStart)
				{
					DrawBarLinesInRange(segmentStart, evt.ticks, numerator, denominator, textureHeight);
				}
				(numerator, denominator) = MusicScoreMakerUtility.GetTimeSignatureFromChangeValue(evt.changeValue);
				segmentStart = evt.ticks;
			}
			DrawBarLinesInRange(segmentStart, _displayEndTicks, numerator, denominator, textureHeight);
		}

		private void DrawBarLinesInRange(long startTicks, long endTicks, int numerator, int denominator, int textureHeight)
		{
			if (denominator < 1)
			{
				return;
			}
			long barTicks = MusicScoreMakerUtility.TICKS_PER_BAR * numerator / denominator;
			if (barTicks < 1L)
			{
				return;
			}
			long ticks = startTicks;
			if (_displayStartTicks > ticks)
			{
				ticks += ((_displayStartTicks - ticks) / barTicks) * barTicks;
			}
			while (ticks <= endTicks && ticks <= _displayEndTicks)
			{
				int y = TicksToPixelY(ticks, textureHeight);
				if (y >= 0 && y < textureHeight)
				{
					int index = y * TEXTURE_WIDTH;
					for (int x = 0; x < TEXTURE_WIDTH; x++)
					{
						if (_pixelBuffer[index + x].a == 0)
						{
							_pixelBuffer[index + x] = ColorBarLine;
						}
					}
				}
				ticks += barTicks;
			}
		}

		private int TicksToPixelY(long ticks, int textureHeight)
		{
			return _displayRangeTicks < 1L ? 0 : (int)((ticks - _displayStartTicks) * textureHeight / _displayRangeTicks);
		}

		private long PixelYToTicks(int pixelY, int textureHeight)
		{
			return textureHeight < 1 ? _displayStartTicks : _displayStartTicks + _displayRangeTicks * pixelY / textureHeight;
		}

		private static long ComputeDataFingerprint(MusicScoreMakerData data)
		{
			if (data == null)
			{
				return 0L;
			}
			unchecked
			{
				long hash = 17L;
				if (data.NoteList != null)
				{
					hash = hash * 31L + data.NoteList.Count;
					for (int i = 0; i < data.NoteList.Count; i++)
					{
						MusicScoreNoteBase note = data.NoteList[i];
						if (note == null)
						{
							continue;
						}
						hash = hash * 31L + note.id;
						hash = hash * 31L + note.ticks;
						hash = hash * 31L + note.laneStart;
						hash = hash * 31L + note.laneEnd;
						hash = hash * 31L + (int)note.category;
						hash = hash * 31L + (int)note.type;
						hash = hash * 31L + note.nextConnectionId;
					}
				}
				if (data.MusicScoreEventDataList != null)
				{
					hash = hash * 31L + data.MusicScoreEventDataList.Count;
					for (int i = 0; i < data.MusicScoreEventDataList.Count; i++)
					{
						MusicScoreEventData evt = data.MusicScoreEventDataList[i];
						if (evt == null)
						{
							continue;
						}
						hash = hash * 31L + evt.id;
						hash = hash * 31L + evt.ticks;
						hash = hash * 31L + (int)evt.eventType;
						hash = hash * 31L + (evt.changeValue?.GetHashCode() ?? 0);
					}
				}
				return hash;
			}
		}

		private static float ApplyEasing(float t, NoteLineType lineType)
		{
			if (lineType == NoteLineType.EaseOut)
			{
				return 1f - (1f - t) * (1f - t);
			}
			if (lineType == NoteLineType.EaseIn)
			{
				return t * t;
			}
			return t;
		}

		private static Color32 GetNoteColor(NoteCategory category, NoteType type)
		{
			switch (category)
			{
			case NoteCategory.Long:
			case NoteCategory.Connection:
			case NoteCategory.Friction:
			case NoteCategory.FrictionHide:
			case NoteCategory.FrictionLong:
			case NoteCategory.FrictionHideLong:
				return type == NoteType.Critical ? ColorNoteLongCritical : ColorNoteLong;
			case NoteCategory.Flick:
			case NoteCategory.FrictionFlick:
				return type == NoteType.Critical ? ColorNoteFlickCritical : ColorNoteFlick;
			case NoteCategory.Guide:
			case NoteCategory.GuideEnd:
			case NoteCategory.GuideHidden:
				return type == NoteType.Critical ? ColorNoteGuideCritical : ColorNoteGuide;
			default:
				return type == NoteType.Critical ? ColorNoteNormalCritical : ColorNoteNormal;
			}
		}

		private void UpdateViewportIndicator()
		{
			if (_viewportFrameRect == null || _displayRangeTicks < 1L)
			{
				return;
			}

			long currentStartTicks = MusicScoreMakerUtility.GetCurrentMusicScoreStartTicks();
			long showTicksRange = MusicScoreMakerUtility.GetShowTicksRange();
			long clippedStartOffsetTicks = 0L;
			long clippedEndOffsetTicks = 0L;

			RectTransform parentRectTransform = _previewRectTransform != null
				? _previewRectTransform.parent as RectTransform
				: null;
			if (_previewRectTransform != null && parentRectTransform != null)
			{
				_previewRectTransform.GetWorldCorners(_previewCorners);
				parentRectTransform.GetWorldCorners(_parentCorners);

				float previewBottom = _previewCorners[0].y;
				float previewTop = _previewCorners[1].y;
				float previewHeight = previewTop - previewBottom;
				if (previewHeight > 0f)
				{
					float clippedBottom = Mathf.Max(_parentCorners[0].y - previewBottom, 0f);
					float clippedTop = Mathf.Max(previewTop - _parentCorners[1].y, 0f);
					clippedStartOffsetTicks = (long)(clippedBottom * showTicksRange / previewHeight);
					clippedEndOffsetTicks = (long)(clippedTop * showTicksRange / previewHeight);
				}
			}

			float viewportStart = Mathf.Max((clippedStartOffsetTicks + currentStartTicks - _displayStartTicks) / (float)_displayRangeTicks, 0f);
			float viewportEnd = Mathf.Min((currentStartTicks + showTicksRange - clippedEndOffsetTicks - _displayStartTicks) / (float)_displayRangeTicks, 1f);
			if (Mathf.Approximately(_cachedViewportStart, viewportStart) && Mathf.Approximately(_cachedViewportEnd, viewportEnd))
			{
				return;
			}

			_viewportFrameRect.anchorMin = new Vector2(0f, viewportStart);
			_viewportFrameRect.anchorMax = new Vector2(1f, viewportEnd);
			_cachedViewportStart = viewportStart;
			_cachedViewportEnd = viewportEnd;
		}

		private void CreateViewportFrame()
		{
			if (_viewportFrameSprite == null || _rawImage == null)
			{
				return;
			}
			GameObject frame = new GameObject("ViewportFrame");
			frame.transform.SetParent(_rawImage.transform, false);
			_viewportFrameImage = frame.AddComponent<Image>();
			_viewportFrameImage.sprite = _viewportFrameSprite;
			_viewportFrameImage.type = Image.Type.Sliced;
			_viewportFrameImage.color = ViewportFrameColor;
			_viewportFrameImage.raycastTarget = false;
			_viewportFrameRect = frame.GetComponent<RectTransform>();
			_viewportFrameRect.offsetMin = new Vector2(-VIEWPORT_FRAME_MARGIN, -VIEWPORT_FRAME_MARGIN);
			_viewportFrameRect.offsetMax = new Vector2(VIEWPORT_FRAME_MARGIN, VIEWPORT_FRAME_MARGIN);
		}

		private void HandlePointerInput(PointerEventData eventData)
		{
			if (eventData == null || _rawImage == null || _currentTextureHeight <= 0)
			{
				return;
			}
			if (MusicScoreMakerEventDispatcher.Instance.PublishFirst<IsMusicPlayingEvent, bool>(IsMusicPlayingEventCache))
			{
				return;
			}
			RectTransform rect = _rawImage.rectTransform;
			if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, eventData.position, eventData.pressEventCamera, out Vector2 localPoint))
			{
				return;
			}
			Rect rawRect = rect.rect;
			float normalizedY = rawRect.height > 0f
				? Mathf.Clamp01((localPoint.y - rawRect.yMin) / rawRect.height)
				: 0f;
			long ticks = _displayStartTicks + (long)(normalizedY * _displayRangeTicks);
			SetFocusTicksEventCache.Ticks = System.Math.Min(System.Math.Max(ticks, 0L), System.Math.Max(_cachedMaxTicks, 0L));
			MusicScoreMakerEventDispatcher.Instance.Publish(SetFocusTicksEventCache);
		}

		private static bool IsHiddenMinimapCategory(NoteCategory category)
		{
			return category == NoteCategory.Connection
				|| category == NoteCategory.GuideHidden
				|| category == NoteCategory.FrictionHide
				|| category == NoteCategory.FrictionHideLong
				|| category == NoteCategory.Hidden;
		}

		public MusicScoreMinimapView()
		{
			_previewCorners = new Vector3[4];
			_parentCorners = new Vector3[4];
			_cachedViewportStart = -1f;
			_cachedViewportEnd = -1f;
			_lastRebuildStartTicks = -1L;
		}

		static MusicScoreMinimapView()
		{
			ColorNoteNormal = new Color32(89, 214, 255, 255);
			ColorNoteNormalCritical = new Color32(255, 231, 92, 255);
			ColorNoteFlick = new Color32(255, 119, 210, 255);
			ColorNoteFlickCritical = new Color32(255, 231, 92, 255);
			ColorNoteLong = new Color32(97, 232, 148, 255);
			ColorNoteLongCritical = new Color32(255, 231, 92, 255);
			ColorNoteGuide = new Color32(150, 170, 255, 255);
			ColorNoteGuideCritical = new Color32(255, 231, 92, 255);
			ColorLongBand = new Color32(72, 184, 128, 180);
			ColorLongBandCritical = new Color32(255, 208, 76, 180);
			ColorBarLine = new Color32(255, 255, 255, 90);
			ColorBackground = new Color32(0, 0, 0, 0);
			ViewportFrameColor = new Color(1f, 1f, 1f, 0.85f);
			IsMusicPlayingEventCache = new IsMusicPlayingEvent();
			GetMusicScoreMakerDataEventCache = new GetMusicScoreMakerDataEvent();
			SetFocusTicksEventCache = new SetFocusTicksEvent();
		}
	}
}
