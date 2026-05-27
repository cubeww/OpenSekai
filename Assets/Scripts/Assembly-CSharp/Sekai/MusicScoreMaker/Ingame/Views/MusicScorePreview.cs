using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Sekai.MusicScoreMaker.Ingame.Events;
using Sekai.MusicScoreMaker.Ingame.Input;
using Sekai.MusicScoreMaker.Ingame.Models;
using Sekai.MusicScoreMaker.Ingame.Utilities;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Sekai.MusicScoreMaker.Ingame.Views
{
	public class MusicScorePreview : MonoBehaviour
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CDelayedUpdateAsync_003Ed__31 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MusicScorePreview _003C_003E4__this;

			private UniTask.Awaiter _003C_003Eu__1;

			private void MoveNext()
			{
				try
				{
					_003C_003E4__this?._PerformDelayedUpdateFromGeneratedStateMachine();
					_003C_003Et__builder.SetResult();
				}
				catch (Exception exception)
				{
					_003C_003Et__builder.SetException(exception);
				}
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
				_003C_003Et__builder.SetStateMachine(stateMachine);
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		private static readonly IsEditRestrictedEvent IsEditRestrictedEventCache;

		[FormerlySerializedAs("_toolInputButton")]
		[SerializeField]
		private ToolInputHandler toolInputHandler;

		[SerializeField]
		private NotesPreview _notesView;

		[SerializeField]
		private MusicScoreEventsPreview _musicScoreEventsView;

		[SerializeField]
		private MusicScoreMinimapView _minimapView;

		[SerializeField]
		private BarLinePreview _barLineView;

		[SerializeField]
		private LaneLinePreview _laneLineView;

		[SerializeField]
		private RectTransform _rectTransform;

		[SerializeField]
		private InvalidPlacementMarkersView _invalidPlacementMarkersView;

		private RectTransform _cachedNoteInstanceRect;

		private RectTransform _cachedBalloonInstanceRect;

		private SubWindowSlideAnimationController[] _cachedSubWindows;

		private bool _updateRequested;

		private bool _refreshRequested;

		private CancellationTokenSource _cancellationTokenSource;

		public RectTransform NotesViewRectTransform
		{
			get
			{
				return _notesView != null ? _notesView.GetComponent<RectTransform>() : null;
			}
		}

		public RectTransform EventsViewRectTransform
		{
			get
			{
				return _musicScoreEventsView != null ? _musicScoreEventsView.GetComponent<RectTransform>() : null;
			}
		}

		public RectTransform RectTransform
		{
			get
			{
				return _rectTransform;
			}
		}

		public NotesPreview NotesPreview
		{
			get
			{
				return _notesView;
			}
		}

		public MusicScoreEventsPreview MusicScoreEventsPreview
		{
			get
			{
				return _musicScoreEventsView;
			}
		}

		public void SetHideNoInGameNotes(bool hide)
		{
			if (_notesView != null)
			{
				_notesView.SetHideNoInGameSprite(hide);
			}
		}

		public void Setup(int notePoolCount = 50, int linePoolCount = 10)
		{
			_notesView.Setup(notePoolCount, linePoolCount);
			_musicScoreEventsView.Setup();
			if (_minimapView != null)
			{
				_minimapView.Setup();
			}
			_barLineView.Setup();
			_laneLineView.Setup();
			SetupEventDispatcher();
			toolInputHandler.RemoveAllAndAddListener(OnClick, null, OnDrag, OnPointerDown, OnPointerUp, OnPinch);
			UpdateSubWindowCache();
		}

		public void Dispose()
		{
			DisposeEventDispatcher();
			_notesView.Dispose();
			_musicScoreEventsView.Dispose();
			if (_minimapView != null)
			{
				_minimapView.Dispose();
			}
			toolInputHandler.RemoveAllListeners();
			if (_invalidPlacementMarkersView != null)
			{
				_invalidPlacementMarkersView.Dispose();
				_invalidPlacementMarkersView = null;
			}
			SafeDisposeCancellation();
			_updateRequested = false;
			_refreshRequested = false;
			_cachedNoteInstanceRect = null;
			_cachedBalloonInstanceRect = null;
			_cachedSubWindows = null;
		}

		public void SetupEventDispatcher()
		{
			MusicScoreMakerEventDispatcher dispatcher = MusicScoreMakerEventDispatcher.Instance;
			dispatcher.Register<UpdateMusicScoreEvent>(UpdateMusicScoreEvent);
			dispatcher.Register<RefreshMusicScoreEvent>(RefreshMusicScoreEvent);
			dispatcher.Register<RefreshNoteDrawingOrderEvent>(RefreshNoteDrawingOrderEvent);
		}

		public void DisposeEventDispatcher()
		{
			if (!MusicScoreMakerEventDispatcher.ExistsInstance)
			{
				return;
			}
			MusicScoreMakerEventDispatcher dispatcher = MusicScoreMakerEventDispatcher.Instance;
			dispatcher.Remove<UpdateMusicScoreEvent>(UpdateMusicScoreEvent);
			dispatcher.Remove<RefreshMusicScoreEvent>(RefreshMusicScoreEvent);
			dispatcher.Remove<RefreshNoteDrawingOrderEvent>(RefreshNoteDrawingOrderEvent);
		}

		private void UpdateMusicScoreEvent(UpdateMusicScoreEvent @event)
		{
			if (_updateRequested)
			{
				return;
			}

			_updateRequested = true;
			DelayedUpdateAsync().Forget();
		}

		private UniTask DelayedUpdateAsync()
		{
			return DelayedUpdateAsyncCore();
		}

		private async UniTask DelayedUpdateAsyncCore()
		{
			_cancellationTokenSource ??= new CancellationTokenSource();
			try
			{
				await UniTask.WaitForEndOfFrame(_cancellationTokenSource.Token);
			}
			catch (OperationCanceledException)
			{
				return;
			}

			_updateRequested = false;
			PerformActualUpdate();
		}

		private void _PerformDelayedUpdateFromGeneratedStateMachine()
		{
			_updateRequested = false;
			PerformActualUpdate();
		}

		private void PerformActualUpdate()
		{
			MusicScoreMakerData musicScore = MusicScoreMakerUtility.GetMusicScoreMakerData();
			if (musicScore == null)
			{
				return;
			}

			bool isEditRestricted = MusicScoreMakerEventDispatcher.Instance.PublishFirst<IsEditRestrictedEvent, bool>(IsEditRestrictedEventCache);
			if (_notesView != null)
			{
				_notesView.SetNotesRaycastEnabled(!isEditRestricted);
			}

			UpdateInvalidPlacementIfNeeded(musicScore);
			UpdateInvalidTimeSignaturePlacementIfNeeded(musicScore);
			CalculateVisibleTicksRange(out long startTicks, out long endTicks);

			float currentMusicScoreScale = MusicScoreMakerUtility.GetCurrentMusicScoreScale();
			long focusTicks = MusicScoreMakerUtility.GetFocusTicks();
			MusicScoreMakerUtility.BeginFloatTicksScope(startTicks, endTicks, currentMusicScoreScale, focusTicks);
			try
			{
				PerformRefreshIfRequested(musicScore);
				musicScore.EnsureNoteListTicksOrderIfNeeded();
				bool isDrawingOrderDirty = musicScore.ConsumeDrawingOrderDirty();
				_notesView.UpdateView(musicScore, startTicks, endTicks, currentMusicScoreScale);
				_musicScoreEventsView.UpdateView(musicScore, startTicks, endTicks);
				if (_minimapView != null)
				{
					_minimapView.UpdateView();
				}
				UpdateView(currentMusicScoreScale, startTicks, endTicks, musicScore);
				UpdateInvalidPlacementMarkersIfNeeded(musicScore, startTicks, endTicks);
				PublishInvalidPlacementCount(musicScore);
				PublishSelectedObjectEditUIViewEvent(musicScore, startTicks, endTicks);
				if (isDrawingOrderDirty)
				{
					_notesView.MarkSiblingIndicesDirty();
				}
				_notesView.RefreshNoteDrawingOrder();
			}
			finally
			{
				MusicScoreMakerUtility.EndFloatTicksScope();
			}
		}

		private static void CalculateVisibleTicksRange(out long startTicks, out long endTicks)
		{
			startTicks = MusicScoreMakerUtility.GetCurrentMusicScoreStartTicks();
			endTicks = startTicks + MusicScoreMakerUtility.GetShowTicksRange();
		}

		private static void UpdateInvalidPlacementIfNeeded(MusicScoreMakerData musicScore)
		{
			if (!MusicScoreMakerSettingsManager.EnableInvalidPlacementCheck)
			{
				return;
			}

			IEnumerable<long> recentlyEditedTicks = MusicScoreMakerUtility.GetRecentlyEditedTicks();
			if (recentlyEditedTicks != null)
			{
				musicScore.RecalculateInvalidPlacementsForTicks(recentlyEditedTicks);
			}
		}

		private static void UpdateInvalidTimeSignaturePlacementIfNeeded(MusicScoreMakerData musicScore)
		{
			if (MusicScoreMakerSettingsManager.EnableInvalidPlacementCheck)
			{
				musicScore.RecalculateInvalidTimeSignaturePlacements();
			}
		}

		private void PerformRefreshIfRequested(MusicScoreMakerData musicScore)
		{
			if (!_refreshRequested)
			{
				return;
			}

			_refreshRequested = false;
			_notesView.Refresh();
			_musicScoreEventsView.Refresh();
			_cachedNoteInstanceRect = null;
			_cachedBalloonInstanceRect = null;
			if (MusicScoreMakerSettingsManager.EnableInvalidPlacementCheck)
			{
				musicScore.RecalculateInvalidPlacementsForTicks(null);
			}
		}

		private void UpdateInvalidPlacementMarkersIfNeeded(MusicScoreMakerData musicScore, long startTicks, long endTicks)
		{
			if (_invalidPlacementMarkersView != null)
			{
				_invalidPlacementMarkersView.UpdateMarkers(musicScore, startTicks, endTicks);
			}
		}

		private void PublishSelectedObjectEditUIViewEvent(MusicScoreMakerData musicScore, long startTicks, long endTicks)
		{
			HashSet<int> selectedNoteTargetIdSet = musicScore.SelectedNoteTargetIdSet;
			HashSet<int> selectedEventTargetIdSet = musicScore.SelectedEventTargetIdSet;
			bool hasSelectedNotes = selectedNoteTargetIdSet != null && selectedNoteTargetIdSet.Count > 0;
			bool hasSelectedEvents = selectedEventTargetIdSet != null && selectedEventTargetIdSet.Count > 0;
			Vector2? anchoredPosition = null;
			Vector2? sizeDelta = null;
			if (hasSelectedNotes || hasSelectedEvents)
			{
				CalculateSelectedObjectsBounds(musicScore, startTicks, endTicks, out anchoredPosition, out sizeDelta);
			}
			bool canFlipSelectedNotes = hasSelectedNotes && selectedNoteTargetIdSet.Count > 1;
			MusicScoreMakerEventDispatcher.Instance.Publish(new ShowSelectedObjectEditUIViewEvent
			{
				isShow = hasSelectedNotes || hasSelectedEvents,
				isMirror = canFlipSelectedNotes,
				isInvert = canFlipSelectedNotes,
				isChange = GetIsShowChange(musicScore, hasSelectedNotes),
				isCopy = GetIsShowCopy(musicScore, hasSelectedNotes, hasSelectedEvents),
				isDelete = hasSelectedNotes || hasSelectedEvents,
				isLeftExpand = GetIsShowExpand(musicScore),
				isRightExpand = GetIsShowExpand(musicScore),
				isSelectAllConnectedNotes = GetIsShowSelectAllConnectedNotes(musicScore, hasSelectedNotes, hasSelectedEvents),
				anchoredPosition = anchoredPosition,
				sizeDelta = sizeDelta,
				coordinateSpaceTransform = _notesView != null ? _notesView.RectTransform : null,
				selectedTargetOperation = musicScore.SelectedTargetOperation
			});
		}

		private static bool GetIsShowExpand(MusicScoreMakerData musicScore)
		{
			HashSet<int> selectedNoteTargetIdSet = musicScore?.SelectedNoteTargetIdSet;
			if (selectedNoteTargetIdSet == null || selectedNoteTargetIdSet.Count != 1)
			{
				return false;
			}
			foreach (int noteId in selectedNoteTargetIdSet)
			{
				MusicScoreNoteBase note = musicScore.FindNote(noteId);
				return note != null && !note.isSkip;
			}
			return false;
		}

		private static bool GetIsShowChange(MusicScoreMakerData musicScore, bool hasSelectedNotes)
		{
			if (!hasSelectedNotes || musicScore == null)
			{
				return false;
			}
			List<MusicScoreNoteBase> selectedNotes = new List<MusicScoreNoteBase>();
			foreach (int noteId in musicScore.SelectedNoteTargetIdSet)
			{
				MusicScoreNoteBase note = musicScore.FindNote(noteId);
				if (note != null)
				{
					selectedNotes.Add(note);
				}
			}
			if (selectedNotes.Count == 0)
			{
				return false;
			}
			NoteGroupType groupType;
			return MusicScoreMakerUtility.IsSameNoteGroup(selectedNotes, musicScore.GetNoteIdCacheOrRebuild(), out groupType);
		}

		private static bool GetIsShowCopy(MusicScoreMakerData musicScore, bool hasSelectedNotes, bool hasSelectedEvents)
		{
			return hasSelectedEvents || (hasSelectedNotes && MusicScoreMakerUtility.CanCopySelectedNotes(musicScore.SelectedNoteIdList, musicScore.GetNoteIdCacheOrRebuild()));
		}

		private static bool GetIsShowSelectAllConnectedNotes(MusicScoreMakerData musicScore, bool hasSelectedNotes, bool hasSelectedEvents)
		{
			return hasSelectedNotes && MusicScoreMakerUtility.HasPartiallySelectedConnectedNotes(musicScore.SelectedNoteIdList, musicScore.GetNoteIdCacheOrRebuild());
		}

		private void CalculateSelectedObjectsBounds(MusicScoreMakerData musicScore, long startTicks, long endTicks, out Vector2? anchoredPosition, out Vector2? sizeDelta)
		{
			anchoredPosition = null;
			sizeDelta = null;
			if (musicScore == null || _notesView == null || _notesView.RectTransform == null)
			{
				return;
			}

			bool hasBounds = false;
			float minX = float.PositiveInfinity;
			float minY = float.PositiveInfinity;
			float maxX = float.NegativeInfinity;
			float maxY = float.NegativeInfinity;

			Rect notesRect = _notesView.RectTransform.rect;
			Vector2 notesSize = notesRect.size;
			float noteHalfHeight = GetSelectedNoteHalfHeight();
			HashSet<int> selectedNoteTargetIdSet = musicScore.SelectedNoteTargetIdSet;
			if (selectedNoteTargetIdSet != null && selectedNoteTargetIdSet.Count > 0)
			{
				foreach (int noteId in selectedNoteTargetIdSet)
				{
					MusicScoreNoteBase note = musicScore.FindNote(noteId);
					if (note == null)
					{
						continue;
					}

					long ticks;
					float laneStart;
					float laneEnd;
					if (note.isSkip)
					{
						Dictionary<int, MusicScoreNoteBase> noteIdCache = musicScore.GetNoteIdCacheOrRebuild();
						MusicScoreNoteBase prevNote = note.FindPrevNote(noteIdCache, true);
						MusicScoreNoteBase nextNote = note.FindNextNote(noteIdCache, true);
						if (prevNote == null || nextNote == null)
						{
							continue;
						}
						(ticks, laneStart, laneEnd) = MusicScoreMakerUtility.CalcSkipNoteLane(note, prevNote, nextNote, musicScore);
						float centerLane = (laneStart + laneEnd) * 0.5f;
						laneStart = centerLane;
						laneEnd = centerLane;
					}
					else
					{
						ticks = note.ticks;
						int noteLaneStart = note.laneStart;
						int noteLaneEnd = note.laneEnd;
						MusicScoreMakerUtility.CalcNoteOperation(musicScore, ref ticks, ref noteLaneStart, ref noteLaneEnd, note);
						laneStart = noteLaneStart;
						laneEnd = noteLaneEnd;
					}

					(float centerX, float width) = MusicScoreMakerUtility.CalcPreviewCenterXAndWidth(laneStart, laneEnd, notesRect.width, Vector2.zero);
					float centerY = MusicScoreMakerUtility.CalcPreviewPositionYFromTicks(startTicks, endTicks, notesSize, Vector2.zero, ticks);
					EncapsulateBounds(ref hasBounds, ref minX, ref minY, ref maxX, ref maxY, centerX, centerY, width * 0.5f, noteHalfHeight);
				}
			}

			HashSet<int> selectedEventTargetIdSet = musicScore.SelectedEventTargetIdSet;
			if (selectedEventTargetIdSet != null && selectedEventTargetIdSet.Count > 0 && _musicScoreEventsView != null && _musicScoreEventsView.RectTransform != null)
			{
				Vector2 eventSize = _musicScoreEventsView.RectTransform.rect.size;
				Dictionary<int, MusicScoreEventData> eventIdCache = musicScore.GetEventIdCacheOrRebuild();
				foreach (int eventId in selectedEventTargetIdSet)
				{
					if (!eventIdCache.TryGetValue(eventId, out MusicScoreEventData eventData) || eventData == null)
					{
						continue;
					}

					long ticks = MusicScoreMakerUtility.CalcEventOperation(musicScore, eventData.ticks, eventData.id);
					float centerX = MusicScoreMakerUtility.CalcMusicScoreEventPreviewPositionX(eventData, eventSize);
					float centerY = MusicScoreMakerUtility.CalcPreviewPositionYFromTicks(startTicks, endTicks, eventSize, Vector2.zero, ticks);
					float halfWidth = 20f;
					float halfHeight = 10f;
					RectTransform balloonRect = _musicScoreEventsView.FindBalloonRectTransformFromId(eventData.id);
					if (balloonRect != null)
					{
						Rect rect = balloonRect.rect;
						halfWidth = rect.width * 0.5f;
						halfHeight = rect.height * 0.5f;
					}
					EncapsulateBounds(ref hasBounds, ref minX, ref minY, ref maxX, ref maxY, centerX, centerY, halfWidth, halfHeight);
				}
			}

			if (!hasBounds)
			{
				return;
			}

			anchoredPosition = new Vector2((minX + maxX) * 0.5f, (minY + maxY) * 0.5f);
			sizeDelta = new Vector2(maxX - minX, maxY - minY);
		}

		private float GetSelectedNoteHalfHeight()
		{
			if (_cachedNoteInstanceRect == null && _notesView != null)
			{
				_cachedNoteInstanceRect = _notesView.GetNoteInstanceRectTransform();
			}
			return _cachedNoteInstanceRect != null ? _cachedNoteInstanceRect.rect.height * 0.5f : 15f;
		}

		private static void EncapsulateBounds(ref bool hasBounds, ref float minX, ref float minY, ref float maxX, ref float maxY, float centerX, float centerY, float halfWidth, float halfHeight)
		{
			float left = centerX - halfWidth;
			float right = centerX + halfWidth;
			float bottom = centerY - halfHeight;
			float top = centerY + halfHeight;
			if (!hasBounds)
			{
				minX = left;
				minY = bottom;
				maxX = right;
				maxY = top;
				hasBounds = true;
				return;
			}
			minX = Mathf.Min(minX, left);
			minY = Mathf.Min(minY, bottom);
			maxX = Mathf.Max(maxX, right);
			maxY = Mathf.Max(maxY, top);
		}

		public void UpdateView(float currentMusicScoreScale, long startTicks, long endTicks, MusicScoreMakerData musicScore)
		{
			if (musicScore == null)
			{
				return;
			}

			_barLineView.UpdateView(currentMusicScoreScale, startTicks, endTicks, musicScore.MusicScoreEventDataList, true, true);
			_laneLineView.UpdateView(1f, 0f);
		}

		private void RefreshMusicScoreEvent(RefreshMusicScoreEvent obj)
		{
			_refreshRequested = true;
			UpdateMusicScoreEvent(null);
		}

		private void RefreshNoteDrawingOrderEvent(RefreshNoteDrawingOrderEvent obj)
		{
			if (_notesView == null)
			{
				return;
			}

			_notesView.MarkSiblingIndicesDirty();
			_notesView.RefreshNoteDrawingOrder();
		}

		private void OnClick(PointerEventData eventData)
		{
			MusicScoreMakerEventDispatcher.Instance.Publish(new OnMusicScorePreviewClickEvent
			{
				EventData = eventData
			});
		}

		private void OnPointerDown(PointerEventData eventData)
		{
			MusicScoreMakerEventDispatcher.Instance.Publish(new OnMusicScorePreviewPointerDownEvent());
		}

		private void OnPointerUp(PointerEventData eventData, bool isLongPress, bool isDragging)
		{
			MusicScoreMakerEventDispatcher.Instance.Publish(new OnMusicScorePreviewPointerUpEvent
			{
				EventData = eventData,
				IsDragging = isDragging,
				IsLongPress = isLongPress
			});
		}

		private void OnDrag(PointerEventData eventData)
		{
			MusicScoreMakerEventDispatcher.Instance.Publish(new OnMusicScorePreviewDragEvent
			{
				EventData = eventData
			});
		}

		private void OnPinch(float pinchDelta)
		{
			if (IsSelectedObjectExpandInputDragging() || IsDialogOrSubWindowActive())
			{
				return;
			}

			if (pinchDelta <= 0f)
			{
				MusicScoreMakerEventDispatcher.Instance.Publish(new ZoomInTimelineEvent());
			}
			else
			{
				MusicScoreMakerEventDispatcher.Instance.Publish(new ZoomOutTimelineEvent());
			}
		}

		private bool IsDialogOrSubWindowActive()
		{
			if (_cachedSubWindows == null)
			{
				return false;
			}

			foreach (SubWindowSlideAnimationController subWindow in _cachedSubWindows)
			{
				if (subWindow == null)
				{
					_cachedSubWindows = null;
					return false;
				}
				if (subWindow.gameObject.activeSelf)
				{
					return true;
				}
			}

			return false;
		}

		private void PublishInvalidPlacementCount(MusicScoreMakerData musicScore)
		{
			if (musicScore == null)
			{
				return;
			}

			MusicScoreMakerEventDispatcher.Instance.Publish(new ValidateViewEvent
			{
				InvalidPlacementCount = musicScore.InvalidPlacements?.Count ?? 0
			});
		}

		private void UpdateSubWindowCache()
		{
			_cachedSubWindows = FindObjectsOfType<SubWindowSlideAnimationController>(true);
		}

		private void InvalidateSubWindowCache()
		{
			_cachedSubWindows = null;
		}

		private static bool IsSelectedObjectExpandInputDragging()
		{
			try
			{
				return SelectedObjectEditUIView.IsExpandInputDragging();
			}
			catch (NullReferenceException)
			{
				return false;
			}
		}

		private void SafeDisposeCancellation()
		{
			if (_cancellationTokenSource == null)
			{
				return;
			}

			_cancellationTokenSource.Cancel();
			_cancellationTokenSource.Dispose();
			_cancellationTokenSource = null;
		}

		public MusicScorePreview()
		{
		}

		static MusicScorePreview()
		{
			IsEditRestrictedEventCache = new IsEditRestrictedEvent();
		}
	}
}
