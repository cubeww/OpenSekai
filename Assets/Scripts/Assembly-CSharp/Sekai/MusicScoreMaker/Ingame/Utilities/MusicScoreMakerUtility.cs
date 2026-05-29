using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using Sekai.Live;
using Sekai.MusicScoreMaker.Ingame.Events;
using Sekai.MusicScoreMaker.Ingame.Models;
using Sekai.MusicScoreMaker.Ingame.Views;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sekai.MusicScoreMaker.Ingame.Utilities
{
	public static class MusicScoreMakerUtility
	{
		public enum ToolType
		{
			None = 0,
			Undo = 15,
			Redo = 16,
			Remove = 17,
			AreaRemove = 18,
			AreaSelect = 19,
			TestPlay = 20,
			MusicSelector = 21,
			EditRestrict = 22
		}

		[StructLayout((LayoutKind)0, Size = 1)]
		private struct CompareByTicks : IComparer<(long, float)>
		{
			public int Compare((long, float) a, (long, float) b)
			{
				return a.Item1.CompareTo(b.Item1);
			}
		}

		public const float SELECTED_COLOR_BRIGHTNESS = 1.2f;

		public const long MinTickInterval = 1L;

		private const float DEFAULT_TIME_SIGNATURE = 4f;

		private const float DEFAULT_BPM = 120f;

		private const float DEFAULT_SPEED_RATIO = 1f;

		private const float DEFAULT_SE_VOLUME = 1f;

		private const float SECONDS_PER_MINUTES = 60f;

		private const float PREVIEW_EVENT_BPM_RATE = 0f;

		private const float PREVIEW_EVENT_HIGHSPEED_RATE = 0.25f;

		private const float PREVIEW_EVENT_TIMESIGNATURE_RATE = 0.5f;

		private const float PREVIEW_EVENT_SEVOLUME_RATE = 0.75f;

		private const float PREVIEW_EVENT_DEFAULT_RATE = 1f;

		public const long TICKS_PER_BAR = 1920L;

		internal const long TICKS_PER_BEAT = 480L;

		private static readonly GetCurrentMusicScoreScaleEvent _getCurrentMusicScoreScaleEvent;

		private static readonly GetMusicScoreTicksMaxEvent _getMusicScoreTicksMaxEvent;

		private static readonly GetFocusTicksEvent _getFocusTicksEvent;

		private static readonly GetCurrentMusicScoreStartTicksEvent _getCurrentMusicScoreStartTicksEvent;

		private static bool _floatTicksScopeActive;

		private static double _floatStartTicks;

		private static double _floatShowTicksRange;

		private static long _floatScopeStartTicksL;

		private static long _floatScopeEndTicksL;

		private static readonly HashSet<long> _recentlyEditedTicks;

		public static string FormatTimeSignatureText(float timeSignature)
		{
			if (Mathf.Approximately(timeSignature, 2f))
			{
				return "2/4";
			}
			if (Mathf.Approximately(timeSignature, 3f))
			{
				return "3/4";
			}
			if (Mathf.Approximately(timeSignature, 4f))
			{
				return "4/4";
			}
			if (Mathf.Approximately(timeSignature, 5f))
			{
				return "5/4";
			}
			if (Mathf.Approximately(timeSignature, 6f))
			{
				return "6/4";
			}
			if (Mathf.Approximately(timeSignature, 7f))
			{
				return "7/4";
			}
			if (Mathf.Approximately(timeSignature, 8f))
			{
				return "8/4";
			}
			return ConvertFloatToTimeSignatureString(timeSignature);
		}

		private static string ConvertFloatToTimeSignatureString(float timeSignature)
		{
			if (timeSignature <= 0f)
			{
				return "4/4";
			}
			int[] denominators = { 4, 8, 16, 2, 1, 32 };
			foreach (int denominator in denominators)
			{
				float numerator = denominator * timeSignature * 0.25f;
				int roundedNumerator = (int)Math.Round(numerator);
				if (roundedNumerator >= 1 && Math.Abs(numerator - roundedNumerator) < 0.001f)
				{
					return $"{roundedNumerator}/{denominator}";
				}
			}
			return "4/4";
		}

		public static (float, float) CalcPreviewCenterXAndWidth(int laneStart, int laneEnd, float parentWidth)
		{
			int clampedLaneStart = ClampLaneStart(laneStart);
			int clampedLaneEnd = ClampLaneEnd(laneEnd);
			float widthRate = (clampedLaneEnd - clampedLaneStart + 1) / (float)MusicScoreMakerModel.LaneCount;
			float centerRate = clampedLaneStart / (float)MusicScoreMakerModel.LaneCount + widthRate * 0.5f - 0.5f;
			return (centerRate * parentWidth, widthRate * parentWidth);
		}

		public static (float, float) CalcPreviewCenterXAndWidth(float laneStart, float laneEnd, float parentWidth, Vector2 parentPosition)
		{
			float clampedLaneStart = ClampLaneStart(laneStart);
			float clampedLaneEnd = ClampLaneEnd(laneEnd);
			float widthRate = (clampedLaneEnd - clampedLaneStart + 1f) / MusicScoreMakerModel.LaneCount;
			float centerRate = clampedLaneStart / MusicScoreMakerModel.LaneCount + widthRate * 0.5f - 0.5f;
			return (parentPosition.x + centerRate * parentWidth, widthRate * parentWidth);
		}

		public static float GetCurrentMusicScoreScale()
		{
			return PublishFirstOrDefault<GetCurrentMusicScoreScaleEvent, float>(_getCurrentMusicScoreScaleEvent, 1f);
		}

		public static long GetMusicScoreTicksMax()
		{
			long ticksMax = PublishFirstOrDefault<GetMusicScoreTicksMaxEvent, long>(_getMusicScoreTicksMaxEvent, 0L);
			if (ticksMax > 0L)
			{
				return ticksMax;
			}
			return GetMusicScoreMakerData()?.MusicScoreTicksMax ?? 0L;
		}

		public static long ClampTicksToValidRange(long ticks)
		{
			if (ticks < 0L)
			{
				return 0L;
			}
			return Math.Min(ticks, Math.Max(GetMusicScoreTicksMax(), 0L));
		}

		public static void SetFocusTicks(long value)
		{
			Publish(new SetFocusTicksEvent
			{
				Ticks = value
			});
		}

		public static long GetFocusTicks()
		{
			return PublishFirstOrDefault<GetFocusTicksEvent, long>(_getFocusTicksEvent, 0L);
		}

		public static long GetCurrentMusicScoreStartTicks()
		{
			return PublishFirstOrDefault<GetCurrentMusicScoreStartTicksEvent, long>(_getCurrentMusicScoreStartTicksEvent, 0L);
		}

		public static long GetShowTicksRange()
		{
			float range = GetCurrentMusicScoreScale() * TICKS_PER_BAR;
			return float.IsPositiveInfinity(range) ? long.MinValue : (long)range;
		}

		public static void BeginFloatTicksScope(long startTicksL, long endTicksL, float scale, long focusTicks)
		{
			double showTicksRange = scale * TICKS_PER_BAR;
			double startTicks = focusTicks - showTicksRange * MusicScoreMakerSettingsManager.ShowFocusTicksRate;
			bool useWholeRange = Math.Abs(startTicks - startTicksL) > 1.000001d;
			_floatStartTicks = useWholeRange ? startTicksL : startTicks;
			_floatShowTicksRange = useWholeRange ? endTicksL - startTicksL : showTicksRange;
			_floatScopeStartTicksL = startTicksL;
			_floatScopeEndTicksL = endTicksL;
			_floatTicksScopeActive = true;
		}

		public static void EndFloatTicksScope()
		{
			_floatTicksScopeActive = false;
		}

		public static float CalcNormalizedPositionFromTicks(long startTicks, long endTicks, long ticks)
		{
			if (_floatTicksScopeActive && _floatScopeStartTicksL == startTicks && _floatScopeEndTicksL == endTicks && _floatShowTicksRange > 0d)
			{
				return (float)((ticks - _floatStartTicks) / _floatShowTicksRange);
			}
			if (endTicks - startTicks < 1L)
			{
				return 0f;
			}
			return (ticks - startTicks) / (float)(endTicks - startTicks);
		}

		public static MusicScoreMakerData GetMusicScoreMakerData()
		{
			try
			{
				if (MusicScoreMakerEventDispatcher.ExistsInstance)
				{
					return MusicScoreMakerEventDispatcher.Instance.PublishFirst<GetMusicScoreMakerDataEvent, MusicScoreMakerData>(new GetMusicScoreMakerDataEvent());
				}
			}
			catch
			{
				// The dispatcher singleton is still being restored; callers treat null as unavailable.
			}
			return null;
		}

		public static bool IsAreaSelectMode()
		{
			return PublishFirstOrDefault<IsAreaSelectModeEvent, bool>(new IsAreaSelectModeEvent(), false);
		}

		public static bool IsRemoveMode()
		{
			return PublishFirstOrDefault<IsRemoveModeEvent, bool>(new IsRemoveModeEvent(), false);
		}

		public static bool IsMusicPlaying()
		{
			return PublishFirstOrDefault<IsMusicPlayingEvent, bool>(new IsMusicPlayingEvent(), false);
		}

		public static long CreateEventPreviewDragEvent(Vector2 pressPosition, Vector2 position, RectTransform parentRectTransform)
		{
			long previewStartTicks = GetPreviewStartTicks();
			long previewEndTicks = GetPreviewEndTicks();
			return CalcMoveTicksByDeltaY(position.y - pressPosition.y, parentRectTransform.rect.height, previewStartTicks, previewEndTicks, false);
		}

		public static int CalcMoveLane(float deltaX, float rectWidth)
		{
			if (Mathf.Approximately(rectWidth, 0f))
			{
				return 0;
			}
			float deltaLane = deltaX / rectWidth * MusicScoreMakerModel.LaneCountMinus1;
			if (float.IsPositiveInfinity(deltaLane) || float.IsNegativeInfinity(deltaLane) || float.IsNaN(deltaLane))
			{
				return 0;
			}
			return (int)Math.Round(deltaLane);
		}

		public static SelectedTargetOperation CreateSelectedOperation(SelectedTargetOperation.NoteTapPosition noteTapPosition = SelectedTargetOperation.NoteTapPosition.none, int deltaLane = 0, long deltaTicks = 0L)
		{
			return new SelectedTargetOperation
			{
				noteTapPosition = noteTapPosition,
				deltaLane = deltaLane,
				deltaTicks = deltaTicks
			};
		}

		public static int ClampLaneStart(int laneStart)
		{
			return Math.Min(Math.Max(laneStart, 0), MusicScoreMakerModel.LaneCountMinus1);
		}

		public static float ClampLaneStart(float laneStart)
		{
			return Mathf.Min(Mathf.Max(laneStart, 0f), MusicScoreMakerModel.LaneCountMinus1);
		}

		public static int ClampLaneStart(int laneStart, int laneEnd)
		{
			return Math.Min(Math.Max(laneStart, 0), laneEnd);
		}

		public static int ClampLaneEnd(int laneEnd)
		{
			return Math.Min(Math.Max(laneEnd, 0), MusicScoreMakerModel.LaneCountMinus1);
		}

		public static float ClampLaneEnd(float laneEnd)
		{
			return Mathf.Min(Mathf.Max(laneEnd, 0f), MusicScoreMakerModel.LaneCountMinus1);
		}

		public static int ClampLaneEnd(int laneEnd, int laneStart)
		{
			return Math.Min(Math.Max(laneEnd, laneStart), MusicScoreMakerModel.LaneCountMinus1);
		}

		public static long CalcEventOperation(MusicScoreMakerData MusicScoreMakerData, long ticks, int id)
		{
			if (MusicScoreMakerData == null)
			{
				throw new ArgumentNullException(nameof(MusicScoreMakerData));
			}
			bool selected = MusicScoreMakerData.SelectedEventIdList.Contains(id) || MusicScoreMakerData.SelectedTemporaryEventIdList.Contains(id);
			if (!selected || MusicScoreMakerData.SelectedTargetOperation == null)
			{
				return ticks;
			}
			return ClampTicksToValidRange(CalculateSnapQuantizedTicks(MusicScoreMakerData.SelectedTargetOperation.deltaTicks, ticks));
		}

		public static (long, float, float) CalcSkipNoteLane(MusicScoreNoteBase targetNote, MusicScoreNoteBase prevNote, MusicScoreNoteBase nextNote, MusicScoreMakerData MusicScoreMakerData)
		{
			if (targetNote == null || prevNote == null || nextNote == null)
			{
				throw new ArgumentNullException();
			}
			long prevTicks = prevNote.ticks;
			int prevLaneStart = prevNote.laneStart;
			int prevLaneEnd = prevNote.laneEnd;
			CalcNoteOperation(MusicScoreMakerData, ref prevTicks, ref prevLaneStart, ref prevLaneEnd, prevNote);
			long nextTicks = nextNote.ticks;
			int nextLaneStart = nextNote.laneStart;
			int nextLaneEnd = nextNote.laneEnd;
			CalcNoteOperation(MusicScoreMakerData, ref nextTicks, ref nextLaneStart, ref nextLaneEnd, nextNote);
			long targetTicks = targetNote.ticks;
			if (MusicScoreMakerData.SelectedNoteTargetIdSet.Contains(targetNote.id))
			{
				ApplyNoteTicksDeltaWithConnectionConstraint(MusicScoreMakerData, ref targetTicks, targetNote, MusicScoreMakerData.SelectedTargetOperation);
			}
			if (nextTicks == prevTicks)
			{
				return (targetTicks, prevNote.laneStart, prevNote.laneEnd);
			}
			float rate = (targetTicks - prevTicks) / (float)(nextTicks - prevTicks);
			rate = ApplyLineEase(rate, prevNote.noteLineType);
			return (
				targetTicks,
				prevLaneStart + rate * (nextLaneStart - prevLaneStart),
				prevLaneEnd + rate * (nextLaneEnd - prevLaneEnd));
		}

		public static (float, float) CalcSkipNoteLane(NoteBase noteBase)
		{
			if (noteBase?.ParentNote is not NoteBase parentNote || parentNote.NoteList == null)
			{
				throw new ArgumentNullException(nameof(noteBase));
			}
			List<NoteBase> noteList = parentNote.NoteList;
			int index = noteList.FindIndex(note => ReferenceEquals(note, noteBase));
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(noteBase));
			}
			NoteBase prevNote = null;
			for (int i = index - 1; i >= 0; i--)
			{
				NoteBase candidate = noteList[i];
				if (candidate != null && !candidate.IsSkip && candidate.Category != NoteCategory.Combo)
				{
					prevNote = candidate;
					break;
				}
			}
			NoteBase nextNote = null;
			for (int i = index + 1; i < noteList.Count; i++)
			{
				NoteBase candidate = noteList[i];
				if (candidate != null && !candidate.IsSkip && candidate.Category != NoteCategory.Combo)
				{
					nextNote = candidate;
					break;
				}
			}
			if (prevNote == null || nextNote == null)
			{
				throw new ArgumentOutOfRangeException(nameof(noteBase));
			}
			float prevPosition = prevNote.MusicScoreInfo.bar + prevNote.MusicScoreInfo.barProgress;
			float nextPosition = nextNote.MusicScoreInfo.bar + nextNote.MusicScoreInfo.barProgress;
			float targetPosition = noteBase.MusicScoreInfo.bar + noteBase.MusicScoreInfo.barProgress;
			float rate = Mathf.Approximately(nextPosition, prevPosition) ? 0f : (targetPosition - prevPosition) / (nextPosition - prevPosition);
			rate = ApplyLineEase(rate, prevNote.LineType);
			return (
				prevNote.LaneStart + rate * (nextNote.LaneStart - prevNote.LaneStart),
				prevNote.LaneEnd + rate * (nextNote.LaneEnd - prevNote.LaneEnd));
		}

		public static (int, int) CalcSkipNoteLaneForMusicScoreNoteBase(MusicScoreNoteBase note, Dictionary<int, MusicScoreNoteBase> noteIdCache, List<MusicScoreNoteBase> connectedNotesBuffer)
		{
			if (note == null || noteIdCache == null || connectedNotesBuffer == null)
			{
				throw new ArgumentNullException();
			}
			note.FindConnectedNotes(noteIdCache, connectedNotesBuffer);
			int index = connectedNotesBuffer.FindIndex(connectedNote => connectedNote != null && connectedNote.id == note.id);
			if (index < 0)
			{
				return (note.laneStart, note.laneEnd);
			}
			MusicScoreNoteBase prevNote = null;
			for (int i = index - 1; i >= 0; i--)
			{
				MusicScoreNoteBase candidate = connectedNotesBuffer[i];
				if (candidate != null && !candidate.isSkip && candidate.category != NoteCategory.Combo)
				{
					prevNote = candidate;
					break;
				}
			}
			MusicScoreNoteBase nextNote = null;
			for (int i = index + 1; i < connectedNotesBuffer.Count; i++)
			{
				MusicScoreNoteBase candidate = connectedNotesBuffer[i];
				if (candidate != null && !candidate.isSkip && candidate.category != NoteCategory.Combo)
				{
					nextNote = candidate;
					break;
				}
			}
			if (prevNote == null || nextNote == null || nextNote.ticks == prevNote.ticks)
			{
				return (note.laneStart, note.laneEnd);
			}
			float rate = (note.ticks - prevNote.ticks) / (float)(nextNote.ticks - prevNote.ticks);
			rate = ApplyLineEase(rate, prevNote.noteLineType);
			int laneStart = ClampLaneStart(Mathf.RoundToInt(prevNote.laneStart + rate * (nextNote.laneStart - prevNote.laneStart)));
			int laneEnd = ClampLaneEnd(Mathf.RoundToInt(prevNote.laneEnd + rate * (nextNote.laneEnd - prevNote.laneEnd)));
			return (laneStart, laneEnd);
		}

		public static long CalculateTicksFromBarAndProgress(int bar, float barProgress, MusicScoreEventData[] eventDatas)
		{
			return CalculateTicksFromBarAndProgressInternal(bar, barProgress, eventDatas);
		}

		public static long CalculateTicksFromBarAndProgress(int bar, float barProgress, List<MusicScoreEventData> eventDataList)
		{
			return CalculateTicksFromBarAndProgressInternal(bar, barProgress, eventDataList?.ToArray());
		}

		private static long CalculateTicksFromBarAndProgressInternal(int bar, float barProgress, MusicScoreEventData[] eventDatas)
		{
			return CalculateTicksFromBarAndProgress(bar, barProgress, ConvertMusicScoreInfo(eventDatas == null ? null : new List<MusicScoreEventData>(eventDatas)));
		}

		public static long CalculateTicksFromBarAndProgress(int bar, float barProgress, MusicScoreInfo[] infos)
		{
			return CalculateTicksFromBarAndProgressSorted(bar, barProgress, CreateSortedMusicScoreInfoList(infos));
		}

		public static long CalculateTicksFromBarAndProgressSorted(int bar, float barProgress, List<MusicScoreInfo> sortedInfos)
		{
			if (sortedInfos == null || sortedInfos.Count == 0)
			{
				return 1920L * bar + (long)Math.Round(barProgress * 1920f);
			}
			long ticks = 0L;
			int currentBar = 0;
			float currentBarLength = TICKS_PER_BAR;
			for (int i = 0; i < sortedInfos.Count; i++)
			{
				MusicScoreInfo info = sortedInfos[i];
				int nextBar = Math.Min(bar, info.bar);
				if (nextBar - currentBar >= 1)
				{
					ticks += (long)(currentBarLength * (nextBar - currentBar));
					currentBar = nextBar;
				}
				if (info.bar > bar || (info.bar == bar && info.barProgress > barProgress))
				{
					break;
				}
				currentBarLength = info.timeSignature * 0.25f * TICKS_PER_BAR;
			}
			if (bar - currentBar >= 1)
			{
				ticks += (long)(currentBarLength * (bar - currentBar));
			}
			return ticks + (long)Math.Round(currentBarLength * barProgress);
		}

		public static List<MusicScoreInfo> CreateSortedMusicScoreInfoList(MusicScoreInfo[] infos)
		{
			List<MusicScoreInfo> result = infos == null ? new List<MusicScoreInfo>() : new List<MusicScoreInfo>(infos);
			result.Sort((a, b) =>
			{
				int compare = a.bar.CompareTo(b.bar);
				return compare != 0 ? compare : a.barProgress.CompareTo(b.barProgress);
			});
			return result;
		}

		public static long GetQuantizeTicks()
		{
			return PublishFirstOrDefault<GetQuantizeTicksEvent, long>(new GetQuantizeTicksEvent(), 0L);
		}

		public static long CalculateQuantizeTicks(int division)
		{
			return division < 1 ? TICKS_PER_BAR : TICKS_PER_BAR / division;
		}

		public static long GetPreviewStartTicks()
		{
			return GetCurrentMusicScoreStartTicks();
		}

		public static long GetPreviewEndTicks()
		{
			return GetCurrentMusicScoreStartTicks() + GetShowTicksRange();
		}

		public static (int, long) CreateNotePreviewDragEventWithTicks(Vector2 pressPosition, Vector2 position, SelectedTargetOperation.NoteTapPosition tapPosition, RectTransform parentRectTransform, long startTicks, long endTicks, PointerEventData pointerEventData)
		{
			if (tapPosition == SelectedTargetOperation.NoteTapPosition.none)
			{
				return (0, 0L);
			}
			Vector2 pressLocalPoint = CalcLocalPoint(pointerEventData, pressPosition, parentRectTransform);
			Vector2 currentLocalPoint = CalcLocalPoint(pointerEventData, position, parentRectTransform);
			int deltaLane = CalcMoveLane(currentLocalPoint.x - pressLocalPoint.x, parentRectTransform.rect.width);
			if (tapPosition == SelectedTargetOperation.NoteTapPosition.left || tapPosition == SelectedTargetOperation.NoteTapPosition.right)
			{
				return (deltaLane, 0L);
			}
			long deltaTicks = CalcMoveTicksByDeltaY(currentLocalPoint.y - pressLocalPoint.y, parentRectTransform.rect.height, startTicks, endTicks, false);
			return (deltaLane, deltaTicks);
		}

		public static void CalcNoteOperation(MusicScoreMakerData MusicScoreMakerData, ref long ticks, ref int laneStart, ref int laneEnd, MusicScoreNoteBase musicScoreNoteBase)
		{
			if (MusicScoreMakerData == null || musicScoreNoteBase == null || MusicScoreMakerData.SelectedNoteTargetIdSet == null)
			{
				return;
			}
			if (!MusicScoreMakerData.SelectedNoteTargetIdSet.Contains(musicScoreNoteBase.id))
			{
				return;
			}

			SelectedTargetOperation leftExpandOperation = MusicScoreMakerData.LeftExpandOperation;
			SelectedTargetOperation rightExpandOperation = MusicScoreMakerData.RightExpandOperation;
			if (leftExpandOperation != null && rightExpandOperation != null)
			{
				laneStart = ClampLaneStart(laneStart + leftExpandOperation.deltaLane, laneEnd);
				laneEnd = ClampLaneEnd(laneEnd + rightExpandOperation.deltaLane, laneStart);
				return;
			}

			SelectedTargetOperation selectedTargetOperation = MusicScoreMakerData.SelectedTargetOperation;
			if (selectedTargetOperation == null)
			{
				return;
			}

			switch (selectedTargetOperation.noteTapPosition)
			{
			case SelectedTargetOperation.NoteTapPosition.none:
			case SelectedTargetOperation.NoteTapPosition.center:
				laneStart = ClampLaneStart(laneStart + selectedTargetOperation.deltaLane);
				laneEnd = ClampLaneEnd(laneEnd + selectedTargetOperation.deltaLane);
				ApplyNoteTicksDeltaWithConnectionConstraint(MusicScoreMakerData, ref ticks, musicScoreNoteBase, selectedTargetOperation);
				break;
			case SelectedTargetOperation.NoteTapPosition.left:
				laneStart = ClampLaneStart(laneStart + selectedTargetOperation.deltaLane, laneEnd);
				break;
			case SelectedTargetOperation.NoteTapPosition.right:
				laneEnd = ClampLaneEnd(laneEnd + selectedTargetOperation.deltaLane, laneStart);
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		public static void ApplyNoteTicksDeltaWithConnectionConstraint(MusicScoreMakerData MusicScoreMakerData, ref long ticks, MusicScoreNoteBase musicScoreNoteBase, SelectedTargetOperation operation)
		{
			if (operation == null)
			{
				return;
			}
			if (MusicScoreMakerData == null)
			{
				return;
			}
			long actualMoveDelta = operation.deltaTicks;
			Dictionary<int, MusicScoreNoteBase> noteIdCache = MusicScoreMakerData.GetNoteIdCacheOrRebuild();
			HashSet<int> selectedNoteIds = MusicScoreMakerData.SelectedNoteTargetIdSet;
			if (selectedNoteIds != null && selectedNoteIds.Count >= 2)
			{
				long minSelectedTicks = FindMinSelectedNoteTicks(MusicScoreMakerData.SelectedNoteIdList, MusicScoreMakerData.SelectedTemporaryNoteIdList, noteIdCache);
				if (minSelectedTicks != long.MaxValue)
				{
					actualMoveDelta = CalculateSnapQuantizedTicks(actualMoveDelta, minSelectedTicks) - minSelectedTicks;
				}
			}
			else
			{
				actualMoveDelta = CalculateSnapQuantizedTicks(actualMoveDelta, ticks) - ticks;
			}
			long movedTicks = ClampTicksToValidRange(ticks + actualMoveDelta);
			if (musicScoreNoteBase != null && selectedNoteIds != null && !musicScoreNoteBase.IsSingle && actualMoveDelta != 0L)
			{
				(MusicScoreNoteBase adjacent, int skipCount) = GetAdjacentUnselectedNoteWithSkipCount(actualMoveDelta, musicScoreNoteBase, selectedNoteIds, noteIdCache);
				if (adjacent != null && !selectedNoteIds.Contains(adjacent.id))
				{
					long interval = skipCount + MinTickInterval;
					movedTicks = actualMoveDelta < 1L ? Math.Max(movedTicks, adjacent.ticks + interval) : Math.Min(movedTicks, adjacent.ticks - interval);
				}
			}
			ticks = movedTicks;
		}

		private static long FindMinSelectedNoteTicks(List<int> selectedNoteIds, List<int> selectedTemporaryNoteIds, Dictionary<int, MusicScoreNoteBase> noteIdCache)
		{
			long minTicks = long.MaxValue;
			ApplyMinSelectedNoteTicks(selectedNoteIds, noteIdCache, ref minTicks);
			ApplyMinSelectedNoteTicks(selectedTemporaryNoteIds, noteIdCache, ref minTicks);
			return minTicks;
		}

		private static void ApplyMinSelectedNoteTicks(List<int> noteIds, Dictionary<int, MusicScoreNoteBase> noteIdCache, ref long minTicks)
		{
			if (noteIds == null || noteIdCache == null)
			{
				return;
			}
			foreach (int noteId in noteIds)
			{
				if (noteIdCache.TryGetValue(noteId, out MusicScoreNoteBase note) && note != null)
				{
					minTicks = Math.Min(minTicks, note.ticks);
				}
			}
		}

		private static (MusicScoreNoteBase, int) GetAdjacentUnselectedNoteWithSkipCount(long actualMoveDelta, MusicScoreNoteBase musicScoreNoteBase, HashSet<int> selectedNoteIds, Dictionary<int, MusicScoreNoteBase> noteIdCache)
		{
			if (musicScoreNoteBase == null || selectedNoteIds == null || noteIdCache == null)
			{
				throw new ArgumentNullException();
			}
			List<MusicScoreNoteBase> connectedNotes = new List<MusicScoreNoteBase>();
			musicScoreNoteBase.FindConnectedNotes(noteIdCache, connectedNotes);
			int index = connectedNotes.FindIndex(note => note != null && note.id == musicScoreNoteBase.id);
			if (index < 0)
			{
				return (null, 0);
			}
			if (actualMoveDelta >= 1L)
			{
				int skipCount = 0;
				for (int i = index + 1; i < connectedNotes.Count; i++)
				{
					MusicScoreNoteBase note = connectedNotes[i];
					if (!selectedNoteIds.Contains(note.id))
					{
						return (note, skipCount);
					}
					skipCount++;
				}
				return (null, 0);
			}
			else
			{
				int skipCount = 0;
				for (int i = index - 1; i >= 0; i--)
				{
					MusicScoreNoteBase note = connectedNotes[i];
					if (!selectedNoteIds.Contains(note.id))
					{
						return (note, skipCount);
					}
					skipCount++;
				}
				return (null, 0);
			}
		}

		public static int CalcTapPositionToLane(Vector2 tapPosition, Vector2 sizeDelta)
		{
			float rate = tapPosition.x / sizeDelta.x + 0.5f;
			if (rate < 0f)
			{
				return -1;
			}
			if (rate >= 1f)
			{
				return MusicScoreMakerModel.LaneCount;
			}
			float lane = rate * MusicScoreMakerModel.LaneCount;
			return float.IsPositiveInfinity(lane) ? int.MinValue : (int)lane;
		}

		public static long QuantizeTicks(long ticks, int quantizeTicks)
		{
			if (quantizeTicks <= 0)
			{
				return ticks;
			}
			return (long)Math.Round((double)ticks / quantizeTicks, 0, MidpointRounding.AwayFromZero) * quantizeTicks;
		}

		public static long QuantizeTicksInBar(long ticks, int quantizeTicks)
		{
			if (quantizeTicks <= 0)
			{
				return ticks;
			}
			long barStart = ticks / TICKS_PER_BAR * TICKS_PER_BAR;
			return barStart + QuantizeTicks(ticks - barStart, quantizeTicks);
		}

		public static long CalcMoveTicksByDeltaY(float deltaY, float height, long startTicks, long endTicks, bool quantize)
		{
			float ticks = deltaY / height * (endTicks - startTicks);
			long deltaTicks = float.IsPositiveInfinity(ticks) ? long.MinValue : (long)ticks;
			return quantize ? QuantizeTicks(deltaTicks, (int)GetQuantizeTicks()) : deltaTicks;
		}

		public static void CalculateBarAndProgressFromTicks(long ticks, out int bar, out float barProgress, MusicScoreMakerData musicScoreMakerData)
		{
			if (musicScoreMakerData?.MusicScoreEventDataList == null || musicScoreMakerData.MusicScoreEventDataList.Count == 0)
			{
				bar = (int)(ticks / TICKS_PER_BAR);
				barProgress = (ticks % TICKS_PER_BAR) / (float)TICKS_PER_BAR;
				return;
			}
			BuildTimeSignatureDictionary(musicScoreMakerData.MusicScoreEventDataList, out List<MusicScoreEventData> timeSignatureEvents, out Dictionary<long, (int numerator, int denominator)> timeSignatureByTicks);
			if (timeSignatureEvents.Count == 0)
			{
				bar = (int)(ticks / TICKS_PER_BAR);
				barProgress = (ticks % TICKS_PER_BAR) / (float)TICKS_PER_BAR;
				return;
			}
			int numerator = 4;
			int denominator = 4;
			int barNumber = 0;
			long sectionStartTicks = 0L;
			foreach (MusicScoreEventData timeSignatureEvent in timeSignatureEvents)
			{
				if (timeSignatureEvent.ticks > ticks)
				{
					break;
				}
				long barLengthTicks = GetBarLengthTicks(numerator, denominator);
				if (barLengthTicks >= 1L)
				{
					barNumber += (int)((timeSignatureEvent.ticks - sectionStartTicks) / barLengthTicks);
				}
				sectionStartTicks = timeSignatureEvent.ticks;
				if (timeSignatureByTicks.TryGetValue(sectionStartTicks, out (int numerator, int denominator) value))
				{
					numerator = value.numerator;
					denominator = value.denominator;
				}
			}
			long currentBarLengthTicks = GetBarLengthTicks(numerator, denominator);
			if (currentBarLengthTicks < 1L)
			{
				bar = barNumber;
				barProgress = 0f;
				return;
			}
			long sectionTicks = ticks - sectionStartTicks;
			long barOffset = sectionTicks / currentBarLengthTicks;
			bar = barNumber + (int)barOffset;
			barProgress = (sectionTicks - barOffset * currentBarLengthTicks) / (float)currentBarLengthTicks;
		}

		public static long SnapToBarStart(long ticks)
		{
			MusicScoreMakerData musicScoreMakerData = GetMusicScoreMakerData();
			CalculateBarAndProgressFromTicks(ticks, out int bar, out _, musicScoreMakerData);
			return CalculateTicksFromBarAndProgress(bar, 0f, musicScoreMakerData?.MusicScoreEventDataList);
		}

		public static long SnapToBarStartPhysical(long ticks, List<MusicScoreEventData> eventDataList = null)
		{
			eventDataList ??= GetMusicScoreMakerData()?.MusicScoreEventDataList;
			if (eventDataList == null || eventDataList.Count == 0)
			{
				return TICKS_PER_BAR * (ticks / TICKS_PER_BAR);
			}
			BuildTimeSignatureDictionary(eventDataList, out List<MusicScoreEventData> timeSignatureEvents, out Dictionary<long, (int numerator, int denominator)> timeSignatureByTicks);
			return timeSignatureEvents.Count == 0
				? TICKS_PER_BAR * (ticks / TICKS_PER_BAR)
				: CalculateBarStartTicksWithTimeSignature(ticks, timeSignatureEvents, timeSignatureByTicks);
		}

		public static long SnapToBarStartOrTimeSignature(long ticks, List<MusicScoreEventData> eventDataList = null)
		{
			eventDataList ??= GetMusicScoreMakerData()?.MusicScoreEventDataList;
			if (eventDataList == null || eventDataList.Count == 0)
			{
				return TICKS_PER_BAR * (ticks / TICKS_PER_BAR);
			}
			long currentBarStartTicks = SnapToBarStartPhysical(ticks, eventDataList);
			BuildTimeSignatureDictionary(eventDataList, out List<MusicScoreEventData> timeSignatureEvents, out Dictionary<long, (int numerator, int denominator)> timeSignatureByTicks);
			long currentBarLengthTicks = TICKS_PER_BAR;
			if (timeSignatureEvents.Count > 0)
			{
				CalculateBarStartTicksWithTimeSignature(currentBarStartTicks, timeSignatureEvents, timeSignatureByTicks, out int numerator, out int denominator, out _);
				currentBarLengthTicks = GetBarLengthTicks(numerator, denominator);
			}
			long nextBarStartTicks = currentBarStartTicks + currentBarLengthTicks;
			long bestTicks = Math.Abs(currentBarStartTicks - ticks) <= Math.Abs(nextBarStartTicks - ticks) ? currentBarStartTicks : nextBarStartTicks;
			long bestDistance = Math.Min(Math.Abs(currentBarStartTicks - ticks), Math.Abs(nextBarStartTicks - ticks));
			foreach (MusicScoreEventData eventData in eventDataList)
			{
				if (eventData == null || eventData.eventType != MusicScoreEventType.TimeSignature)
				{
					continue;
				}
				long distance = Math.Abs(eventData.ticks - ticks);
				if (distance < bestDistance)
				{
					bestTicks = eventData.ticks;
					bestDistance = distance;
				}
			}
			return bestTicks;
		}

		private static long GetBarLengthTicks(int numerator, int denominator)
		{
			return denominator == 0 ? TICKS_PER_BAR : (long)Math.Round(TICKS_PER_BAR * (numerator / (float)denominator));
		}

		private static bool TryParseTimeSignature(object changeValue, out int numerator, out int denominator)
		{
			numerator = 4;
			denominator = 4;
			if (changeValue == null)
			{
				return false;
			}
			string text = changeValue.ToString();
			if (string.IsNullOrEmpty(text))
			{
				return false;
			}
			string[] parts = text.Split('/');
			if (parts.Length == 2
				&& int.TryParse(parts[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out numerator)
				&& int.TryParse(parts[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out denominator)
				&& numerator > 0
				&& denominator > 0)
			{
				return true;
			}
			if (float.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out float value))
			{
				numerator = Mathf.RoundToInt(value);
				denominator = 4;
				return numerator > 0;
			}
			return false;
		}

		private static void BuildTimeSignatureDictionary(List<MusicScoreEventData> eventDataList, out List<MusicScoreEventData> timeSignatureEvents, out Dictionary<long, (int numerator, int denominator)> timeSignatureByTicks)
		{
			int capacity = 0;
			if (eventDataList != null)
			{
				foreach (MusicScoreEventData eventData in eventDataList)
				{
					if (eventData != null && eventData.eventType == MusicScoreEventType.TimeSignature)
					{
						capacity++;
					}
				}
			}
			timeSignatureEvents = new List<MusicScoreEventData>(capacity);
			timeSignatureByTicks = new Dictionary<long, (int numerator, int denominator)>(capacity);
			if (eventDataList == null)
			{
				return;
			}
			foreach (MusicScoreEventData eventData in eventDataList)
			{
				if (eventData == null || eventData.eventType != MusicScoreEventType.TimeSignature)
				{
					continue;
				}
				timeSignatureEvents.Add(eventData);
				if (TryParseTimeSignature(eventData.changeValue, out int numerator, out int denominator))
				{
					timeSignatureByTicks[eventData.ticks] = (numerator, denominator);
				}
			}
			timeSignatureEvents.Sort((a, b) => a.ticks.CompareTo(b.ticks));
		}

		private static long CalculateBarStartTicksWithTimeSignature(long targetTicks, List<MusicScoreEventData> timeSignatureEvents, Dictionary<long, (int numerator, int denominator)> timeSignatureByTicks)
		{
			return CalculateBarStartTicksWithTimeSignature(targetTicks, timeSignatureEvents, timeSignatureByTicks, out _, out _, out _);
		}

		private static long CalculateBarStartTicksWithTimeSignature(long targetTicks, List<MusicScoreEventData> timeSignatureEvents, Dictionary<long, (int numerator, int denominator)> timeSignatureByTicks, out int numerator, out int denominator, out int barNumber)
		{
			numerator = 4;
			denominator = 4;
			barNumber = 0;
			if (timeSignatureEvents == null)
			{
				return TICKS_PER_BAR * (targetTicks / TICKS_PER_BAR);
			}
			long sectionStartTicks = 0L;
			foreach (MusicScoreEventData timeSignatureEvent in timeSignatureEvents)
			{
				if (timeSignatureEvent == null || timeSignatureEvent.ticks > targetTicks)
				{
					break;
				}
				long barLengthTicks = GetBarLengthTicks(numerator, denominator);
				if (barLengthTicks >= 1L)
				{
					barNumber += (int)((timeSignatureEvent.ticks - sectionStartTicks) / barLengthTicks);
					sectionStartTicks = timeSignatureEvent.ticks;
				}
				if (timeSignatureByTicks != null && timeSignatureByTicks.TryGetValue(timeSignatureEvent.ticks, out (int numerator, int denominator) value))
				{
					numerator = value.numerator;
					denominator = value.denominator;
				}
			}
			long currentBarLengthTicks = GetBarLengthTicks(numerator, denominator);
			if (currentBarLengthTicks > 0L)
			{
				long barOffset = (targetTicks - sectionStartTicks) / currentBarLengthTicks;
				sectionStartTicks += barOffset * currentBarLengthTicks;
				barNumber += (int)barOffset;
			}
			return sectionStartTicks;
		}

		public static long GetNextBarStartTicks(long ticks)
		{
			long currentBarStartTicks = SnapToBarStartPhysical(ticks);
			List<MusicScoreEventData> eventDataList = GetMusicScoreMakerData()?.MusicScoreEventDataList;
			if (eventDataList == null || eventDataList.Count == 0)
			{
				return currentBarStartTicks + TICKS_PER_BAR;
			}
			BuildTimeSignatureDictionary(eventDataList, out List<MusicScoreEventData> timeSignatureEvents, out Dictionary<long, (int numerator, int denominator)> timeSignatureByTicks);
			if (timeSignatureEvents.Count == 0)
			{
				return currentBarStartTicks + TICKS_PER_BAR;
			}
			CalculateBarStartTicksWithTimeSignature(currentBarStartTicks, timeSignatureEvents, timeSignatureByTicks, out int numerator, out int denominator, out _);
			return currentBarStartTicks + GetBarLengthTicks(numerator, denominator);
		}

		public static Vector2 CalcLocalPoint(PointerEventData eventData, Vector2 position, RectTransform rectTransform)
		{
			if (eventData == null)
			{
				throw new ArgumentNullException(nameof(eventData));
			}
			if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, position, eventData.pressEventCamera, out Vector2 localPoint))
			{
				throw new Exception("Failed to calculate local point.");
			}
			return localPoint;
		}

		public static long GetTicksFromTime(float currentTimeSec, MusicScoreInfo[] musicScoreInfoArray)
		{
			(long ticks, float bpm)[] bpmEvents = BuildBpmEvents(musicScoreInfoArray);
			return GetTicksFromTimeBpmOnly(currentTimeSec, bpmEvents, bpmEvents.Length);
		}

		public static long GetTicksFromTime(float currentTimeSec, List<MusicScoreEventData> musicScoreEventDataList)
		{
			return GetTicksFromTime(currentTimeSec, ConvertMusicScoreInfo(musicScoreEventDataList));
		}

		public static float GetBpmAtTicks(long ticks, List<MusicScoreEventData> musicScoreEventDataList)
		{
			float bpm = DEFAULT_BPM;
			if (musicScoreEventDataList == null)
			{
				return bpm;
			}
			foreach (MusicScoreEventData evt in musicScoreEventDataList)
			{
				if (evt.eventType == MusicScoreEventType.BPM && evt.ticks <= ticks)
				{
					bpm = ConvertToFloat(evt.changeValue, bpm);
				}
			}
			return bpm;
		}

		public static (int, int) GetTimeSignatureAtTicks(long ticks, List<MusicScoreEventData> musicScoreEventDataList)
		{
			(int numerator, int denominator) result = (4, 4);
			if (musicScoreEventDataList == null)
			{
				return result;
			}
			foreach (MusicScoreEventData evt in musicScoreEventDataList)
			{
				if (evt.eventType == MusicScoreEventType.TimeSignature && evt.ticks <= ticks)
				{
					result = GetTimeSignatureFromChangeValue(evt.changeValue);
				}
			}
			return result;
		}

		public static (int, int) GetTimeSignatureFromChangeValue(object changeValue)
		{
			if (TryParseTimeSignature(changeValue, out int numerator, out int denominator))
			{
				return (numerator, denominator);
			}
			float value = ConvertToFloat(changeValue, DEFAULT_TIME_SIGNATURE);
			string text = FormatTimeSignatureText(value);
			if (TryParseTimeSignature(text, out numerator, out denominator))
			{
				return (numerator, denominator);
			}
			return (4, 4);
		}

		public static float GetHighSpeedAtTicks(long ticks, List<MusicScoreEventData> musicScoreEventDataList)
		{
			float speedRatio = DEFAULT_SPEED_RATIO;
			if (musicScoreEventDataList == null)
			{
				return speedRatio;
			}
			foreach (MusicScoreEventData evt in musicScoreEventDataList)
			{
				if (evt.eventType == MusicScoreEventType.HighSpeed && evt.ticks <= ticks)
				{
					speedRatio = ConvertToFloat(evt.changeValue, speedRatio);
				}
			}
			return speedRatio;
		}

		private static long GetTicksFromTimeBpmOnly(float currentTimeSec, (long ticks, float bpm)[] bpmEvents, int count)
		{
			if (bpmEvents == null || count == 0)
			{
				return 0L;
			}
			float elapsed = 0f;
			for (int i = 0; i < count; i++)
			{
				long startTicks = bpmEvents[i].ticks;
				float bpm = bpmEvents[i].bpm <= 0f ? DEFAULT_BPM : bpmEvents[i].bpm;
				if (i + 1 < count)
				{
					long endTicks = bpmEvents[i + 1].ticks;
					float segmentSec = (endTicks - startTicks) / (float)TICKS_PER_BEAT * SECONDS_PER_MINUTES / bpm;
					if (currentTimeSec <= elapsed + segmentSec)
					{
						return startTicks + (long)Math.Round((currentTimeSec - elapsed) * bpm / SECONDS_PER_MINUTES * TICKS_PER_BEAT);
					}
					elapsed += segmentSec;
				}
				else
				{
					return startTicks + (long)Math.Round((currentTimeSec - elapsed) * bpm / SECONDS_PER_MINUTES * TICKS_PER_BEAT);
				}
			}
			return 0L;
		}

		private static float GetTimeFromTicksBpmOnly(long ticks, (long ticks, float bpm)[] bpmEvents, int count)
		{
			if (bpmEvents == null || count == 0)
			{
				return 0f;
			}
			float time = 0f;
			for (int i = 0; i < count; i++)
			{
				long startTicks = bpmEvents[i].ticks;
				float bpm = bpmEvents[i].bpm <= 0f ? DEFAULT_BPM : bpmEvents[i].bpm;
				long endTicks = i + 1 < count ? bpmEvents[i + 1].ticks : ticks;
				if (ticks <= endTicks || i + 1 == count)
				{
					return time + (ticks - startTicks) / (float)TICKS_PER_BEAT * SECONDS_PER_MINUTES / bpm;
				}
				time += (endTicks - startTicks) / (float)TICKS_PER_BEAT * SECONDS_PER_MINUTES / bpm;
			}
			return time;
		}

		public static MusicScoreInfo[] ConvertMusicScoreInfo(List<MusicScoreEventData> eventArray)
		{
			if (eventArray == null || eventArray.Count == 0)
			{
				return new[] { new MusicScoreInfo(0, 0f, 0f, DEFAULT_BPM, DEFAULT_TIME_SIGNATURE, DEFAULT_SPEED_RATIO, DEFAULT_SE_VOLUME) };
			}
			List<MusicScoreEventData> sorted = new List<MusicScoreEventData>(eventArray);
			sorted.Sort((a, b) => a.ticks.CompareTo(b.ticks));
			List<MusicScoreInfo> infos = new List<MusicScoreInfo>(sorted.Count);
			float bpm = DEFAULT_BPM;
			float timeSignature = DEFAULT_TIME_SIGNATURE;
			float speedRatio = DEFAULT_SPEED_RATIO;
			float seVolume = DEFAULT_SE_VOLUME;
			int index = 0;
			while (index < sorted.Count)
			{
				long ticks = sorted[index].ticks;
				while (index < sorted.Count && sorted[index].ticks == ticks)
				{
					MusicScoreEventData evt = sorted[index++];
					switch (evt.eventType)
					{
					case MusicScoreEventType.BPM:
						bpm = ConvertToFloat(evt.changeValue, bpm);
						break;
					case MusicScoreEventType.HighSpeed:
						speedRatio = ConvertToFloat(evt.changeValue, speedRatio);
						break;
					case MusicScoreEventType.SeVolume:
						seVolume = ConvertToFloat(evt.changeValue, seVolume);
						break;
					case MusicScoreEventType.TimeSignature:
					{
						(int numerator, int denominator) = GetTimeSignatureFromChangeValue(evt.changeValue);
						timeSignature = numerator / (float)denominator * 4f;
						break;
					}
					default:
						throw new ArgumentOutOfRangeException();
					}
				}
				infos.Add(new MusicScoreInfo((int)(ticks / TICKS_PER_BAR), (ticks % TICKS_PER_BAR) / (float)TICKS_PER_BAR, 0f, bpm, timeSignature, speedRatio, seVolume));
			}
			CalcTime(infos);
			return infos.ToArray();
		}

		private static void CalcTime(List<MusicScoreInfo> scoreInfoList)
		{
			if (scoreInfoList == null || scoreInfoList.Count == 0)
			{
				return;
			}
			scoreInfoList.Sort((a, b) =>
			{
				long at = TicksFromBarProgress(a.bar, a.barProgress);
				long bt = TicksFromBarProgress(b.bar, b.barProgress);
				return at.CompareTo(bt);
			});
			float time = 0f;
			long previousTicks = TicksFromBarProgress(scoreInfoList[0].bar, scoreInfoList[0].barProgress);
			float previousBpm = scoreInfoList[0].bpm <= 0f ? DEFAULT_BPM : scoreInfoList[0].bpm;
			for (int i = 0; i < scoreInfoList.Count; i++)
			{
				MusicScoreInfo info = scoreInfoList[i];
				long ticks = TicksFromBarProgress(info.bar, info.barProgress);
				if (i > 0)
				{
					time += (ticks - previousTicks) / (float)TICKS_PER_BEAT * SECONDS_PER_MINUTES / previousBpm;
				}
				info.time = time;
				scoreInfoList[i] = info;
				previousTicks = ticks;
				previousBpm = info.bpm <= 0f ? DEFAULT_BPM : info.bpm;
			}
		}

		public static float GetTimeFromTicks(long ticks, MusicScoreInfo[] musicScoreInfoArray)
		{
			(long ticks, float bpm)[] bpmEvents = BuildBpmEvents(musicScoreInfoArray);
			return GetTimeFromTicksBpmOnly(ticks, bpmEvents, bpmEvents.Length);
		}

		public static float CalcPreviewPositionYFromTicks(long startTicks, long endTicks, Vector2 parentSizeDelta, Vector2 parentPosition, long ticks)
		{
			return parentPosition.y + parentSizeDelta.y * CalcNormalizedPositionFromTicks(startTicks, endTicks, ticks) + parentSizeDelta.y * -0.5f;
		}

		public static long CalcTapPositionToTicks(Vector2 tapPosition, long startTicks, long endTicks, Vector2 sizeDelta, bool quantize = true)
		{
			if (endTicks <= startTicks)
			{
				return 0L;
			}
			float rawTicks = (tapPosition.y / sizeDelta.y + 0.5f) * (endTicks - startTicks);
			long ticks = startTicks + (float.IsPositiveInfinity(rawTicks) ? long.MinValue : (long)rawTicks);
			return quantize ? QuantizeTicksInBar(ticks, (int)GetQuantizeTicks()) : ticks;
		}

		public static MusicScoreInfo GenerateMusicScoreInfoFromTicks(long ticks, MusicScoreInfo[] musicScoreInfoArray, MusicScoreMakerData musicScoreMakerData)
		{
			if (musicScoreInfoArray == null || musicScoreInfoArray.Length == 0)
			{
				return default;
			}
			CalculateBarAndProgressFromTicks(ticks, out int bar, out float barProgress, musicScoreMakerData);
			return MusicScore.GenerateNoteMusicScoreInfo(bar, barProgress, musicScoreInfoArray);
		}

		private static int GetMusicScoreInfoPropertiesAtBarPosition(MusicScoreInfo[] musicScoreInfoArray, float targetBarPosition, out float currentBpm, out float currentTimeSignature, out float currentSpeedRatio, out float currentSeVolume)
		{
			currentBpm = DEFAULT_BPM;
			currentTimeSignature = DEFAULT_TIME_SIGNATURE;
			currentSpeedRatio = DEFAULT_SPEED_RATIO;
			currentSeVolume = DEFAULT_SE_VOLUME;
			if (musicScoreInfoArray == null || musicScoreInfoArray.Length == 0)
			{
				return -1;
			}
			if (musicScoreInfoArray[0].bar + musicScoreInfoArray[0].barProgress > targetBarPosition)
			{
				return 0;
			}
			for (int i = 0; i < musicScoreInfoArray.Length; i++)
			{
				MusicScoreInfo info = musicScoreInfoArray[i];
				currentBpm = info.bpm;
				currentTimeSignature = info.timeSignature;
				currentSpeedRatio = info.speedRatio;
				currentSeVolume = info.seVolume;
				if (i + 1 >= musicScoreInfoArray.Length || musicScoreInfoArray[i + 1].bar + musicScoreInfoArray[i + 1].barProgress > targetBarPosition)
				{
					return i;
				}
			}
			return musicScoreInfoArray.Length - 1;
		}

		public static (long, int, long, int) CalcAreaTicksLane(RectTransform notesPreviewRect, PointerEventData pointerEventData, bool quantize = true)
		{
			long timelineFocusDiff = GetTimelineFocusDiff();
			Vector2 pressLocalPoint = CalcLocalPoint(pointerEventData, pointerEventData.pressPosition, notesPreviewRect);
			Vector2 currentLocalPoint = CalcLocalPoint(pointerEventData, pointerEventData.position, notesPreviewRect);
			long previewStartTicks = GetPreviewStartTicks();
			long previewEndTicks = GetPreviewEndTicks();
			Vector2 size = notesPreviewRect.rect.size;
			long pressTicks = CalcTapPositionToTicks(pressLocalPoint, previewStartTicks, previewEndTicks, size, quantize);
			int pressLane = CalcTapPositionToLane(pressLocalPoint, size);
			long currentTicks = CalcTapPositionToTicks(currentLocalPoint, previewStartTicks, previewEndTicks, size, quantize);
			int currentLane = CalcTapPositionToLane(currentLocalPoint, size);
			pressLane = Math.Min(Math.Max(pressLane, 0), MusicScoreMakerModel.LaneCountMinus1);
			currentLane = Math.Min(Math.Max(currentLane, 0), MusicScoreMakerModel.LaneCountMinus1);
			long startTicks = Math.Min(pressTicks, currentTicks);
			long endTicks = Math.Max(pressTicks, currentTicks);
			int laneStart = Math.Min(pressLane, currentLane);
			int laneEnd = Math.Max(pressLane, currentLane);
			return (startTicks - Math.Max(timelineFocusDiff, 0L), laneStart, endTicks - Math.Min(timelineFocusDiff, 0L), laneEnd);
		}

		public static long CalcPressTicks(PointerEventData pointerEventData, RectTransform notesViewRectTransform)
		{
			Vector2 localPoint = CalcLocalPoint(pointerEventData, pointerEventData.pressPosition, notesViewRectTransform);
			return CalcTapPositionToTicks(localPoint, GetPreviewStartTicks(), GetPreviewEndTicks(), notesViewRectTransform.rect.size);
		}

		public static long FindMinTick(List<MusicScoreNoteBase> noteList, List<MusicScoreEventData> eventDataList)
		{
			long min = long.MaxValue;
			if (noteList != null)
			{
				foreach (MusicScoreNoteBase note in noteList)
				{
					if (note != null)
					{
						min = Math.Min(min, note.ticks);
					}
				}
			}
			if (eventDataList != null)
			{
				foreach (MusicScoreEventData evt in eventDataList)
				{
					if (evt != null)
					{
						min = Math.Min(min, evt.ticks);
					}
				}
			}
			return min == long.MaxValue ? 0L : min;
		}

		public static long FindMaxTick(List<MusicScoreNoteBase> noteList, List<MusicScoreEventData> eventDataList)
		{
			long max = 0L;
			if (noteList != null)
			{
				foreach (MusicScoreNoteBase note in noteList)
				{
					if (note != null)
					{
						max = Math.Max(max, note.ticks);
					}
				}
			}
			if (eventDataList != null)
			{
				foreach (MusicScoreEventData evt in eventDataList)
				{
					if (evt != null)
					{
						max = Math.Max(max, evt.ticks);
					}
				}
			}
			return max;
		}

		public static (long, int) CalcPressTicksAndLane(PointerEventData pointerEventData, RectTransform notesPreviewRect)
		{
			Vector2 localPoint = CalcLocalPoint(pointerEventData, pointerEventData.pressPosition, notesPreviewRect);
			Vector2 size = notesPreviewRect.rect.size;
			long ticks = CalcTapPositionToTicks(localPoint, GetPreviewStartTicks(), GetPreviewEndTicks(), size);
			int lane = CalcTapPositionToLane(localPoint, size);
			return (ticks, lane);
		}

		public static float CalcMusicScoreEventPreviewPositionX(MusicScoreEventData data, Vector2 parentSizeDelta)
		{
			if (data == null)
			{
				throw new ArgumentNullException(nameof(data));
			}
			float rate = data.eventType switch
			{
				MusicScoreEventType.BPM => -0.5f,
				MusicScoreEventType.HighSpeed => -0.25f,
				MusicScoreEventType.SeVolume => 0.25f,
				MusicScoreEventType.TimeSignature => 0f,
				_ => 0.5f
			};
			return parentSizeDelta.x * rate;
		}

		public static long CalculateSnapQuantizedTicks(long deltaTicks, long baseTicks)
		{
			return QuantizeTicksInBar(baseTicks + deltaTicks, (int)GetQuantizeTicks());
		}

		private static long GetTimelineFocusDiff()
		{
			return PublishFirstOrDefault<GetTimelineFocusDiffEvent, long>(new GetTimelineFocusDiffEvent(), 0L);
		}

		public static float CalcScrollDiffTicksToPreviewY(RectTransform notesPreviewRect)
		{
			long timelineFocusDiff = GetTimelineFocusDiff();
			if (timelineFocusDiff == 0L)
			{
				return 0f;
			}
			long showTicksRange = GetShowTicksRange();
			if (showTicksRange == 0L)
			{
				return 0f;
			}
			return timelineFocusDiff / (float)showTicksRange * notesPreviewRect.rect.height;
		}

		public static ToolType GetSelectedToolType()
		{
			return PublishFirstOrDefault<GetSelectedToolTypeEvent, ToolType>(new GetSelectedToolTypeEvent(), ToolType.None);
		}

		public static bool IsEditRestricted()
		{
			return PublishFirstOrDefault<IsEditRestrictedEvent, bool>(new IsEditRestrictedEvent(), false);
		}

		public static void AddEditedTick(long ticks)
		{
			_recentlyEditedTicks.Add(ticks);
		}

		public static void AddEditedTicks(IEnumerable<long> ticks)
		{
			if (ticks == null)
			{
				return;
			}
			foreach (long tick in ticks)
			{
				AddEditedTick(tick);
			}
		}

		public static IEnumerable<long> GetRecentlyEditedTicks()
		{
			if (_recentlyEditedTicks.Count == 0)
			{
				return null;
			}
			long[] ticks = new long[_recentlyEditedTicks.Count];
			_recentlyEditedTicks.CopyTo(ticks);
			_recentlyEditedTicks.Clear();
			return ticks;
		}

		public static IEnumerable<long> GetRecentlyEditedTicksSnapshot()
		{
			if (_recentlyEditedTicks.Count == 0)
			{
				return Array.Empty<long>();
			}
			long[] ticks = new long[_recentlyEditedTicks.Count];
			_recentlyEditedTicks.CopyTo(ticks);
			return ticks;
		}

		public static void SortAndUniqueInPlace(List<long> list)
		{
			if (list == null || list.Count <= 1)
			{
				return;
			}
			list.Sort();
			int write = 1;
			for (int read = 1; read < list.Count; read++)
			{
				if (list[read] != list[write - 1])
				{
					list[write++] = list[read];
				}
			}
			if (write < list.Count)
			{
				list.RemoveRange(write, list.Count - write);
			}
		}

		public static bool IsSameNoteGroup(List<MusicScoreNoteBase> notes, Dictionary<int, MusicScoreNoteBase> noteIdCache, out NoteGroupType groupType)
		{
			return NoteGroupUtility.IsSameNoteGroup(notes, noteIdCache, out groupType);
		}

		public static bool IsSameNoteGroup(MusicScoreNoteBase[] notes, Dictionary<int, MusicScoreNoteBase> noteIdCache, out NoteGroupType groupType)
		{
			return NoteGroupUtility.IsSameNoteGroup(notes, noteIdCache, out groupType);
		}

		public static float CalculateNoteYScale(float currentMusicScoreScale)
		{
			float startThreshold = MusicScoreMakerSettingsManager.NoteYScaleStartThreshold;
			float endThreshold = MusicScoreMakerSettingsManager.NoteYScaleEndThreshold;
			float minScale = MusicScoreMakerSettingsManager.NoteYScaleMin;
			if (currentMusicScoreScale < startThreshold)
			{
				return 1f;
			}
			if (currentMusicScoreScale >= endThreshold)
			{
				return minScale;
			}
			float rate = Mathf.Clamp01((currentMusicScoreScale - startThreshold) / (endThreshold - startThreshold));
			return Mathf.Lerp(1f, minScale, rate);
		}

		public static bool IsGuideCategory(NoteCategory category)
		{
			return category == NoteCategory.Guide || category == NoteCategory.GuideEnd || category == NoteCategory.GuideHidden;
		}

		public static bool IsExcludedFromChangeRateCheck(NoteCategory category)
		{
			return category == NoteCategory.Connection || category == NoteCategory.Hidden || IsGuideCategory(category);
		}

		public static bool IsNoteCategoryWithoutSe(NoteCategory category)
		{
			return category == NoteCategory.Hidden || IsGuideCategory(category);
		}

		public static bool ShouldFlipNoteIcon(NoteCategory noteCategory, NoteDirection noteDirection)
		{
			return noteDirection == NoteDirection.Left;
		}

		public static string GetNoteTypeIconSpriteName(NoteCategory noteCategory, NoteDirection noteDirection, NoteType noteType, NoteLineType noteLineType, NoteGroupType groupType, SelectedNoteDataButton.ButtonType buttonType)
		{
			string criticalSuffix = noteType == NoteType.Critical ? "_crtc" : string.Empty;
			string directionSuffix = noteDirection switch
			{
				NoteDirection.Left => "_l",
				NoteDirection.Right => "_r",
				_ => string.Empty
			};
			string lineSuffix = noteLineType switch
			{
				NoteLineType.EaseIn => "_line_easein",
				NoteLineType.EaseOut => "_line_easeout",
				_ => "_line_linear"
			};
			string shortLineSuffix = noteLineType switch
			{
				NoteLineType.EaseIn => "_easein",
				NoteLineType.EaseOut => "_easeout",
				_ => "_linear"
			};
			switch (noteCategory)
			{
			case NoteCategory.Long:
				if (groupType == NoteGroupType.Unknown)
				{
					return "notes_icon_long" + criticalSuffix;
				}
				if (groupType == NoteGroupType.LongStart)
				{
					return "notes_icon_long" + lineSuffix + criticalSuffix;
				}
				if (groupType == NoteGroupType.LongEnd && buttonType == SelectedNoteDataButton.ButtonType.Change)
				{
					return noteType == NoteType.Critical ? "notes_icon_normal_crtc" : "notes_icon_long2";
				}
				if (groupType == NoteGroupType.LongEnd && buttonType == SelectedNoteDataButton.ButtonType.SetSelected)
				{
					return "notes_icon_long" + criticalSuffix;
				}
				return "notes_icon_normal" + criticalSuffix;
			case NoteCategory.Connection:
				return "notes_icon_long_among" + lineSuffix + criticalSuffix;
			case NoteCategory.Flick:
				return (noteDirection == NoteDirection.Left || noteDirection == NoteDirection.Right ? "notes_icon_frick_l" : "notes_icon_frick") + criticalSuffix;
			case NoteCategory.Friction:
				return "notes_icon_trace" + criticalSuffix;
			case NoteCategory.FrictionHide:
				return groupType == NoteGroupType.LongEnd || groupType == NoteGroupType.CriticalLongEnd ? "notes_icon_clear" : "notes_icon_trace" + criticalSuffix;
			case NoteCategory.FrictionLong:
				return groupType == NoteGroupType.Unknown ? "notes_icon_tracelong_line_linear" + criticalSuffix : "notes_icon_tracelong" + lineSuffix + criticalSuffix;
			case NoteCategory.FrictionHideLong:
				return groupType == NoteGroupType.Unknown ? "notes_icon_clear" + criticalSuffix : "notes_icon_clear" + lineSuffix + criticalSuffix;
			case NoteCategory.FrictionFlick:
				return "notes_icon_tracefrick" + directionSuffix + criticalSuffix;
			case NoteCategory.Guide:
				return groupType == NoteGroupType.Unknown ? "notes_icon_guide" + criticalSuffix : "notes_icon_guide" + shortLineSuffix + criticalSuffix;
			case NoteCategory.GuideEnd:
				return "notes_icon_guide" + criticalSuffix;
			case NoteCategory.GuideHidden:
				return "notes_icon_guide_among" + lineSuffix + criticalSuffix;
			case NoteCategory.Combo:
			case NoteCategory.Skip:
				return "notes_icon_long_among" + criticalSuffix;
			case NoteCategory.Hidden:
				return "notes_icon_clear_among" + lineSuffix + criticalSuffix;
			default:
				return "notes_icon_normal" + criticalSuffix;
			}
		}

		public static bool CanCopySelectedNotes(List<int> selectedNoteIdList, Dictionary<int, MusicScoreNoteBase> noteIdCache)
		{
			if (selectedNoteIdList == null || selectedNoteIdList.Count == 0 || noteIdCache == null)
			{
				return false;
			}
			List<MusicScoreNoteBase> connectedNotes = new List<MusicScoreNoteBase>();
			foreach (int noteId in selectedNoteIdList)
			{
				if (!noteIdCache.TryGetValue(noteId, out MusicScoreNoteBase note) || note == null || note.IsSingle)
				{
					continue;
				}
				note.FindConnectedNotes(noteIdCache, connectedNotes);
				foreach (MusicScoreNoteBase connectedNote in connectedNotes)
				{
					if (connectedNote == null || !selectedNoteIdList.Contains(connectedNote.id))
					{
						return false;
					}
				}
			}
			return selectedNoteIdList.Count > 0;
		}

		public static bool HasPartiallySelectedConnectedNotes(List<int> selectedNoteIdList, Dictionary<int, MusicScoreNoteBase> noteIdCache)
		{
			if (selectedNoteIdList == null || selectedNoteIdList.Count == 0 || noteIdCache == null)
			{
				return false;
			}
			List<MusicScoreNoteBase> connectedNotes = new List<MusicScoreNoteBase>();
			foreach (int noteId in selectedNoteIdList)
			{
				if (!noteIdCache.TryGetValue(noteId, out MusicScoreNoteBase note) || note == null || note.IsSingle)
				{
					continue;
				}
				note.FindConnectedNotes(noteIdCache, connectedNotes);
				foreach (MusicScoreNoteBase connectedNote in connectedNotes)
				{
					if (connectedNote == null || !selectedNoteIdList.Contains(connectedNote.id))
					{
						return true;
					}
				}
			}
			return false;
		}

		public static bool ValidateLongNoteTicks(List<MusicScoreNoteBase> notes)
		{
			if (notes == null || notes.Count < 2)
			{
				return true;
			}
			long previousTicks = long.MinValue;
			foreach (MusicScoreNoteBase note in notes)
			{
				if (note == null)
				{
					continue;
				}
				if (note.ticks <= previousTicks)
				{
					return false;
				}
				previousTicks = note.ticks;
			}
			return true;
		}

		private static HashSet<int> GetParentLongNoteChainIds(Dictionary<int, MusicScoreNoteBase> noteIdCache, int parentNoteId)
		{
			if (parentNoteId == -1)
			{
				return null;
			}
			if (noteIdCache == null || !noteIdCache.TryGetValue(parentNoteId, out MusicScoreNoteBase parentNote) || parentNote == null)
			{
				return null;
			}
			List<MusicScoreNoteBase> connectedNotes = new List<MusicScoreNoteBase>();
			parentNote.FindConnectedNotes(noteIdCache, connectedNotes);
			HashSet<int> result = new HashSet<int>();
			foreach (MusicScoreNoteBase note in connectedNotes)
			{
				if (note != null)
				{
					result.Add(note.id);
				}
			}
			return result;
		}

		public static bool CanPlaceNoteAtTicks(MusicScoreMakerData data, long ticks, int excludeNoteId = -1, int parentNoteId = -1)
		{
			if (data?.NoteList == null)
			{
				return true;
			}
			HashSet<int> parentChainIds = parentNoteId == -1 ? null : GetParentLongNoteChainIds(data.GetNoteIdCacheOrRebuild(), parentNoteId);
			foreach (MusicScoreNoteBase note in data.NoteList)
			{
				if (note == null || note.id == excludeNoteId || note.ticks != ticks)
				{
					continue;
				}
				if (note.IsConnectedFirst || note.IsConnectedLast)
				{
					if (parentChainIds == null || parentChainIds.Contains(note.id))
					{
						return false;
					}
				}
			}
			return true;
		}

		public static bool CanPlaceConnectionNoteAtTicks(MusicScoreMakerData data, long ticks, float lane, int parentNoteId = -1)
		{
			if (data?.NoteList == null)
			{
				return true;
			}
			Dictionary<int, MusicScoreNoteBase> noteIdCache = data.GetNoteIdCacheOrRebuild();
			HashSet<int> parentChainIds = GetParentLongNoteChainIds(noteIdCache, parentNoteId);
			int laneCenter = float.IsPositiveInfinity(lane) ? int.MinValue : (int)lane;
			int laneStart = Math.Max(laneCenter - 1, 0);
			int laneEnd = Math.Min(laneCenter + 1, MusicScoreMakerModel.LaneCountMinus1);
			List<MusicScoreNoteBase> connectedNotes = new List<MusicScoreNoteBase>();
			MusicScoreNoteBase previousNote = null;
			MusicScoreNoteBase nextNote = null;
			long previousTicks = long.MinValue;
			long nextTicks = long.MaxValue;
			foreach (MusicScoreNoteBase note in data.NoteList)
			{
				if (note == null)
				{
					continue;
				}
				if (note.ticks == ticks)
				{
					if (parentChainIds == null || parentChainIds.Contains(note.id))
					{
						return false;
					}
					continue;
				}
				int noteLaneStart = Math.Min(Math.Max(note.laneStart, 0), MusicScoreMakerModel.LaneCountMinus1);
				int noteLaneEnd = Math.Min(Math.Max(note.laneEnd, 0), MusicScoreMakerModel.LaneCountMinus1);
				int noteMinLane = Math.Min(noteLaneStart, noteLaneEnd);
				int noteMaxLane = Math.Max(noteLaneStart, noteLaneEnd);
				if (noteMinLane > laneEnd || noteMaxLane < laneStart || note.IsSingle)
				{
					continue;
				}
				note.FindConnectedNotes(noteIdCache, connectedNotes);
				foreach (MusicScoreNoteBase connectedNote in connectedNotes)
				{
					if (connectedNote == null)
					{
						continue;
					}
					if (connectedNote.ticks < ticks && connectedNote.ticks > previousTicks)
					{
						previousTicks = connectedNote.ticks;
						previousNote = connectedNote;
					}
					if (connectedNote.ticks > ticks && connectedNote.ticks < nextTicks)
					{
						nextTicks = connectedNote.ticks;
						nextNote = connectedNote;
					}
				}
			}
			return (ticks - previousTicks > 0L || previousNote == null) && (nextTicks - ticks > 0L || nextNote == null);
		}

		public static void RequestTransitionToOutGame(MenuScreenType screenType = MenuScreenType.MusicScoreMakerTop)
		{
			void RequestScene()
			{
				if (screenType == MenuScreenType.MusicScoreMakerTop)
				{
					Sekai.MusicScoreMaker.Ingame.Presenters.MusicScoreMakerEntryPoint.BootData = null;
					SceneManager.Instance.RequestScene(SceneManager.Scene.MusicScoreMaker);
					return;
				}

				Type bootDataType = Type.GetType("Sekai.OutGameBootData, Assembly-CSharp");
				Type controllerType = Type.GetType("Sekai.OutGameController, Assembly-CSharp");
				if (bootDataType != null && controllerType != null)
				{
					object bootData = Activator.CreateInstance(bootDataType);
					bootDataType.GetProperty("BootScreenType")?.SetValue(bootData, screenType);
					controllerType.GetProperty("BootData")?.SetValue(null, bootData);
				}
				SceneManager.Instance.RequestScene(SceneManager.Scene.Title);
			}
			if (ScreenManager.Instance != null)
			{
				ScreenManager.Instance.FadeOut(0f, 0.25f, RequestScene);
			}
			else
			{
				RequestScene();
			}
		}

		public static float RoundToDisplayedPercent(float rate)
		{
			return (float)Math.Round(rate * 100f, 1, MidpointRounding.AwayFromZero);
		}

		public static bool IsChangeRateBelowThresholdByDisplay(float changeRate, float threshold)
		{
			return RoundToDisplayedPercent(changeRate) < RoundToDisplayedPercent(threshold);
		}

		public static MusicCategory ConvertLiveModeTypeToMusicCategory(LiveSettingData.LiveModeType liveModeType)
		{
			return liveModeType switch
			{
				LiveSettingData.LiveModeType.High3D => MusicCategory.mv,
				LiveSettingData.LiveModeType.Default3D => MusicCategory.mv,
				LiveSettingData.LiveModeType.Mode2D => MusicCategory.mv_2d,
				LiveSettingData.LiveModeType.Low => MusicCategory.none,
				LiveSettingData.LiveModeType.OriginalMV => MusicCategory.original,
				_ => MusicCategory.none
			};
		}

		private static TResult PublishFirstOrDefault<TEvent, TResult>(TEvent eventData, TResult defaultValue) where TEvent : MusicScoreMakerDispatcherEventBase
		{
			try
			{
				if (MusicScoreMakerEventDispatcher.ExistsInstance)
				{
					return MusicScoreMakerEventDispatcher.Instance.PublishFirst<TEvent, TResult>(eventData);
				}
			}
			catch
			{
				// Several restored callers query before the scene dispatcher has completed startup.
			}
			return defaultValue;
		}

		private static void Publish<TEvent>(TEvent eventData) where TEvent : MusicScoreMakerDispatcherEventBase
		{
			try
			{
				if (MusicScoreMakerEventDispatcher.ExistsInstance)
				{
					MusicScoreMakerEventDispatcher.Instance.Publish(eventData);
				}
			}
			catch
			{
				// Best-effort bridge while the surrounding presenter layer is still being restored.
			}
		}

		private static float ConvertToFloat(object value, float defaultValue)
		{
			if (value == null)
			{
				return defaultValue;
			}
			if (value is float floatValue)
			{
				return floatValue;
			}
			if (value is double doubleValue)
			{
				return (float)doubleValue;
			}
			if (value is int intValue)
			{
				return intValue;
			}
			if (float.TryParse(value.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out float parsed))
			{
				return parsed;
			}
			return defaultValue;
		}

		private static long TicksFromBarProgress(int bar, float barProgress)
		{
			return TICKS_PER_BAR * bar + (long)Math.Round(barProgress * TICKS_PER_BAR);
		}

		private static float ApplyLineEase(float rate, NoteLineType lineType)
		{
			rate = Mathf.Clamp01(rate);
			return lineType switch
			{
				NoteLineType.Linear => rate,
				NoteLineType.EaseOut => 1f - (1f - rate) * (1f - rate),
				NoteLineType.EaseIn => rate * rate,
				_ => throw new ArgumentOutOfRangeException(nameof(lineType), lineType, null)
			};
		}

		private static (long ticks, float bpm)[] BuildBpmEvents(MusicScoreInfo[] musicScoreInfoArray)
		{
			if (musicScoreInfoArray == null || musicScoreInfoArray.Length == 0)
			{
				return new[] { (0L, DEFAULT_BPM) };
			}
			List<(long ticks, float bpm)> events = new List<(long ticks, float bpm)>(musicScoreInfoArray.Length + 1);
			foreach (MusicScoreInfo info in musicScoreInfoArray)
			{
				events.Add((TicksFromBarProgress(info.bar, info.barProgress), info.bpm <= 0f ? DEFAULT_BPM : info.bpm));
			}
			events.Sort(new CompareByTicks());
			int write = 0;
			for (int read = 0; read < events.Count; read++)
			{
				if (read + 1 < events.Count && events[read + 1].ticks == events[read].ticks)
				{
					continue;
				}
				events[write++] = events[read];
			}
			if (write < events.Count)
			{
				events.RemoveRange(write, events.Count - write);
			}
			if (events.Count == 0 || events[0].ticks > 0L)
			{
				events.Insert(0, (0L, DEFAULT_BPM));
			}
			return events.ToArray();
		}

		static MusicScoreMakerUtility()
		{
			_getCurrentMusicScoreScaleEvent = new GetCurrentMusicScoreScaleEvent();
			_getMusicScoreTicksMaxEvent = new GetMusicScoreTicksMaxEvent();
			_getFocusTicksEvent = new GetFocusTicksEvent();
			_getCurrentMusicScoreStartTicksEvent = new GetCurrentMusicScoreStartTicksEvent();
			_recentlyEditedTicks = new HashSet<long>();
		}
	}
}
