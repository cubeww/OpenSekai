using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Sekai.Live;
using Sekai.MusicScoreMaker.Ingame.Utilities;

namespace Sekai.MusicScoreMaker.Ingame.Models
{
	public class MusicScoreMakerData
	{
		public const int VERSION_1 = 1;

		[JsonIgnore]
		private Dictionary<int, MusicScoreNoteBase> _noteIdCache;

		[JsonIgnore]
		private bool _noteIdCacheValid;

		[JsonIgnore]
		private bool _noteListOrderDirty;

		[JsonIgnore]
		private bool _drawingOrderDirty;

		[JsonIgnore]
		private Dictionary<int, MusicScoreEventData> _eventIdCache;

		[JsonIgnore]
		private bool _eventIdCacheValid;

		[JsonIgnore]
		public List<int> SelectedNoteIdList;

		[JsonIgnore]
		public List<int> SelectedTemporaryNoteIdList;

		[JsonIgnore]
		private HashSet<int> _selectedNoteTargetIdSetCache;

		[JsonIgnore]
		private bool _selectedNoteTargetIdSetCacheValid;

		[JsonIgnore]
		public List<int> SelectedEventIdList;

		[JsonIgnore]
		public List<int> SelectedTemporaryEventIdList;

		[JsonIgnore]
		private HashSet<int> _selectedEventTargetIdSetCache;

		[JsonIgnore]
		private bool _selectedEventTargetIdSetCacheValid;

		[JsonIgnore]
		public List<MusicScoreNoteBase> CopiedNoteList;

		[JsonIgnore]
		public List<MusicScoreEventData> CopiedEventDataList;

		[JsonIgnore]
		public SelectedTargetOperation SelectedTargetOperation;

		private int _idCount;

		private readonly List<long> _recalcWorkTickList = new List<long>(128);

		private readonly List<int>[] _recalcWorkLaneNoteIds = new List<int>[MusicScoreMakerModel.LaneCount];

		private readonly HashSet<int> _recalcWorkNoteIdSet = new HashSet<int>();

		private readonly List<int> _tmpNoteIdsList = new List<int>(32);

		private readonly List<(long start, long end)> _judgmentNoteGapWorkRanges = new List<(long start, long end)>(128);

		private readonly List<InvalidPlacementInfo> _updateExistingGapWorkList = new List<InvalidPlacementInfo>(32);

		private static readonly List<int> EmptyNoteIdsForGap = new List<int>();

		private readonly List<long> _noteDensityWorkTicks = new List<long>(1024);

		private readonly List<(long start, long end)> _noteDensityOverflowWorkRanges = new List<(long start, long end)>(32);

		private readonly List<(float segStartTime, float segEndTime, long startTicks, long endTicks, float displayTime)> _longNoteMeshWorkSegments = new List<(float segStartTime, float segEndTime, long startTicks, long endTicks, float displayTime)>(256);

		private readonly List<(long startTicks, long endTicks, int maxMesh, MeshOverflowSpeedType speedType)> _longNoteMeshOverflowResults = new List<(long startTicks, long endTicks, int maxMesh, MeshOverflowSpeedType speedType)>(32);

		private readonly List<(float segStartTime, float segEndTime, long startTicks, long endTicks, float displayTime)> _guideMeshWorkSegments = new List<(float segStartTime, float segEndTime, long startTicks, long endTicks, float displayTime)>(256);

		private readonly List<(long startTicks, long endTicks, int maxMesh, MeshOverflowSpeedType speedType)> _guideMeshOverflowResults = new List<(long startTicks, long endTicks, int maxMesh, MeshOverflowSpeedType speedType)>(32);

		private const int DefaultLongNoteMeshSplitCount = 100;

		private static readonly Lazy<int> _longNoteMeshSplitCount = new Lazy<int>(() => DefaultLongNoteMeshSplitCount);

		private const int DefaultLongNoteMeshOverflowThreshold = 990;

		private static readonly Lazy<int> _longNoteMeshOverflowThreshold = new Lazy<int>(() => DefaultLongNoteMeshOverflowThreshold);

		private const float LongNoteMeshDisplayTimeFastest = 0.35f;

		private const float LongNoteMeshDisplayTimeSlowest = 4f;

		private static readonly HashSet<NoteCategory> ExcludedCategories = new HashSet<NoteCategory>
		{
			NoteCategory.Connection,
			NoteCategory.Guide,
			NoteCategory.GuideEnd,
			NoteCategory.GuideHidden,
			NoteCategory.FrictionHideLong,
			NoteCategory.FrictionHide,
			NoteCategory.Hidden
		};

		private static readonly IComparer<MusicScoreNoteBase> NoteTicksComparer = Comparer<MusicScoreNoteBase>.Create(CompareNoteByTicks);

		public static int CURRENT_VERSION
		{
			get
			{
				return GetVersionCodeFromDataVersion();
			}
		}

		public int VersionCode { get; set; }

		[JsonIgnore]
		public HashSet<int> SelectedNoteTargetIdSet
		{
			get
			{
				if (!_selectedNoteTargetIdSetCacheValid)
				{
					_selectedNoteTargetIdSetCache.Clear();
					_selectedNoteTargetIdSetCache.EnsureCapacity(SelectedNoteIdList.Count + SelectedTemporaryNoteIdList.Count);
					foreach (int id in SelectedNoteIdList)
					{
						_selectedNoteTargetIdSetCache.Add(id);
					}
					foreach (int id in SelectedTemporaryNoteIdList)
					{
						_selectedNoteTargetIdSetCache.Add(id);
					}
					_selectedNoteTargetIdSetCacheValid = true;
				}
				return _selectedNoteTargetIdSetCache;
			}
		}

		[JsonIgnore]
		public HashSet<int> SelectedEventTargetIdSet
		{
			get
			{
				if (!_selectedEventTargetIdSetCacheValid)
				{
					_selectedEventTargetIdSetCache.Clear();
					_selectedEventTargetIdSetCache.EnsureCapacity(SelectedEventIdList.Count + SelectedTemporaryEventIdList.Count);
					foreach (int id in SelectedEventIdList)
					{
						_selectedEventTargetIdSetCache.Add(id);
					}
					foreach (int id in SelectedTemporaryEventIdList)
					{
						_selectedEventTargetIdSetCache.Add(id);
					}
					_selectedEventTargetIdSetCacheValid = true;
				}
				return _selectedEventTargetIdSetCache;
			}
		}

		[JsonIgnore]
		public bool HasCopyAny
		{
			get
			{
				return CopiedNoteList.Count > 0 || CopiedEventDataList.Count > 0;
			}
		}

		[JsonIgnore]
		public SelectedTargetOperation LeftExpandOperation { get; set; }

		[JsonIgnore]
		public SelectedTargetOperation RightExpandOperation { get; set; }

		public List<MusicScoreEventData> MusicScoreEventDataList { get; set; }

		[JsonIgnore]
		public EventBase[] EventArray { get; set; }

		[JsonProperty("EventArray")]
		private EventBase[] SerializedEventArray
		{
			get
			{
				return FilterOutSkillFeverEvents(EventArray);
			}
			set
			{
				EventArray = value == null ? Array.Empty<EventBase>() : FilterOutSkillFeverEvents(value);
			}
		}

		public List<MusicScoreNoteBase> NoteList { get; set; }

		[JsonIgnore]
		public List<InvalidPlacementInfo> InvalidPlacements { get; set; }

		public long MusicScoreTicksMax { get; set; }

		public int MusicId { get; set; }

		[CanBeNull]
		public string FullComboDataHash { get; set; }

		private static int LongNoteMeshSplitCount
		{
			get
			{
				return _longNoteMeshSplitCount.Value;
			}
		}

		private static int LongNoteMeshOverflowThreshold
		{
			get
			{
				return _longNoteMeshOverflowThreshold.Value;
			}
		}

		[JsonIgnore]
		public QuantizeSettings QuantizeSettings { get; private set; }

		public bool ConsumeDrawingOrderDirty()
		{
			bool dirty = _drawingOrderDirty;
			_drawingOrderDirty = false;
			return dirty;
		}

		public void MarkDrawingOrderDirty()
		{
			_drawingOrderDirty = true;
		}

		public int GetVersionCode()
		{
			return VersionCode;
		}

		public static int GetCurrentVersion()
		{
			return CURRENT_VERSION;
		}

		private static int GetVersionCodeFromDataVersion()
		{
			return VERSION_1;
		}

		public static bool IsOlderVersion(int version)
		{
			return version == 0 || version < CURRENT_VERSION;
		}

		public static bool IsCurrentVersion(int version)
		{
			return version == CURRENT_VERSION;
		}

		public static bool IsNewerVersion(int version)
		{
			return version > CURRENT_VERSION;
		}

		public MusicScoreMakerData()
		{
			InitializeRuntimeState();
			MusicScoreEventDataList = new List<MusicScoreEventData>();
			NoteList = new List<MusicScoreNoteBase>();
			InvalidPlacements = new List<InvalidPlacementInfo>();
			SelectedNoteIdList = new List<int>();
			SelectedTemporaryNoteIdList = new List<int>();
			SelectedEventIdList = new List<int>();
			SelectedTemporaryEventIdList = new List<int>();
			CopiedNoteList = new List<MusicScoreNoteBase>();
			CopiedEventDataList = new List<MusicScoreEventData>();
			EventArray = Array.Empty<EventBase>();
			QuantizeSettings = new QuantizeSettings();
			VersionCode = CURRENT_VERSION;
		}

		public MusicScoreMakerData(MusicScore musicScore)
			: this()
		{
			MigrateToCurrentVersion();
			_idCount = 0;
			if (musicScore == null)
			{
				return;
			}
			AddEventRange(ConvertEventList(musicScore.musicScoreInfoArray));
			EventArray = FilterOutSkillFeverEvents(musicScore.EventArray);
			if (musicScore.NoteArray != null)
			{
				List<MusicScoreInfo> sortedInfos = MusicScoreMakerUtility.CreateSortedMusicScoreInfoList(musicScore.musicScoreInfoArray);
				NoteList = new List<MusicScoreNoteBase>(musicScore.NoteArray.Length * 2);
				foreach (NoteBase note in musicScore.NoteArray)
				{
					if (note == null)
					{
						continue;
					}
					NoteList.AddRange(MusicScoreNoteBase.FromNoteBaseArray(note, GetNewId, sortedInfos));
				}
				SortNoteListByTicks();
				InvalidateNoteIdCache();
				RebuildNoteIdCache();
				UpdateConnectionNotes();
			}
			MusicScoreTicksMax = GetMaxTicks();
			InvalidPlacements = new List<InvalidPlacementInfo>();
		}

		private void InvalidateSelectedNoteTargetIdSetCache()
		{
			_selectedNoteTargetIdSetCacheValid = false;
		}

		private void InvalidateSelectedEventTargetIdSetCache()
		{
			_selectedEventTargetIdSetCacheValid = false;
		}

		public void ResetNoteIdCount()
		{
			_idCount = 0;
		}

		public void InitializeIdCount()
		{
			_idCount = 0;
			if (NoteList != null)
			{
				foreach (MusicScoreNoteBase note in NoteList)
				{
					if (note != null && _idCount < note.id)
					{
						_idCount = note.id;
					}
				}
			}
			if (MusicScoreEventDataList != null)
			{
				foreach (MusicScoreEventData evt in MusicScoreEventDataList)
				{
					if (evt != null && _idCount < evt.id)
					{
						_idCount = evt.id;
					}
				}
			}
		}

		public int GetNewId()
		{
			return ++_idCount;
		}

		private List<MusicScoreEventData> ConvertEventList(MusicScoreInfo[] infos)
		{
			List<MusicScoreEventData> result = new List<MusicScoreEventData>();
			List<MusicScoreInfo> sortedInfos = MusicScoreMakerUtility.CreateSortedMusicScoreInfoList(infos);
			SetFirstEvents(infos, result, sortedInfos);
			if (infos == null)
			{
				return result;
			}
			for (int i = 1; i < infos.Length; i++)
			{
				AddChangedEvent(infos[i], infos[i - 1], result, sortedInfos);
			}
			return result;
		}

		private void SetFirstEvents(MusicScoreInfo[] infos, List<MusicScoreEventData> list, List<MusicScoreInfo> sortedInfos)
		{
			if (infos == null || infos.Length == 0)
			{
				return;
			}
			MusicScoreInfo info = infos[0];
			long ticks = MusicScoreMakerUtility.CalculateTicksFromBarAndProgressSorted(info.bar, info.barProgress, sortedInfos);
			list.Add(CreateEvent(GetNewId(), MusicScoreEventType.BPM, ticks, info.bpm));
			list.Add(CreateEvent(GetNewId(), MusicScoreEventType.TimeSignature, ticks, MusicScoreMakerUtility.FormatTimeSignatureText(info.timeSignature)));
			list.Add(CreateEvent(GetNewId(), MusicScoreEventType.HighSpeed, ticks, info.speedRatio));
			list.Add(CreateEvent(GetNewId(), MusicScoreEventType.SeVolume, ticks, info.seVolume));
		}

		private void AddChangedEvent(MusicScoreInfo musicScoreInfo, MusicScoreInfo beforeScoreInfo, List<MusicScoreEventData> result, List<MusicScoreInfo> sortedInfos)
		{
			long ticks = MusicScoreMakerUtility.CalculateTicksFromBarAndProgressSorted(musicScoreInfo.bar, musicScoreInfo.barProgress, sortedInfos);
			if (musicScoreInfo.bpm != beforeScoreInfo.bpm)
			{
				result.Add(CreateEvent(GetNewId(), MusicScoreEventType.BPM, ticks, musicScoreInfo.bpm));
			}
			if (musicScoreInfo.timeSignature != beforeScoreInfo.timeSignature)
			{
				result.Add(CreateEvent(GetNewId(), MusicScoreEventType.TimeSignature, ticks, MusicScoreMakerUtility.FormatTimeSignatureText(musicScoreInfo.timeSignature)));
			}
			if (musicScoreInfo.speedRatio != beforeScoreInfo.speedRatio)
			{
				result.Add(CreateEvent(GetNewId(), MusicScoreEventType.HighSpeed, ticks, musicScoreInfo.speedRatio));
			}
			if (musicScoreInfo.seVolume != beforeScoreInfo.seVolume)
			{
				result.Add(CreateEvent(GetNewId(), MusicScoreEventType.SeVolume, ticks, musicScoreInfo.seVolume));
			}
		}

		private static bool IsNoteIdBasedPlacementType(InvalidPlacementType type)
		{
			return type == InvalidPlacementType.LaneOverlap;
		}

		private List<InvalidPlacementInfo> EnsureInvalidPlacements()
		{
			return InvalidPlacements ?? (InvalidPlacements = new List<InvalidPlacementInfo>());
		}

		private void RemoveInvalidPlacementsByType(InvalidPlacementType type)
		{
			if (InvalidPlacements != null)
			{
				InvalidPlacements.RemoveAll(info => info.Type == type);
			}
		}

		private void RemoveInvalidPlacementsByTypes(InvalidPlacementType type1, InvalidPlacementType type2)
		{
			if (InvalidPlacements != null)
			{
				InvalidPlacements.RemoveAll(info => info.Type == type1 || info.Type == type2);
			}
		}

		private void RemoveInvalidPlacementsByTypeAndTicks(InvalidPlacementType type, long ticks)
		{
			if (InvalidPlacements != null)
			{
				InvalidPlacements.RemoveAll(info => info.Type == type && info.Ticks == ticks);
			}
		}

		private void AddInvalidPlacement(InvalidPlacementInfo info)
		{
			EnsureInvalidPlacements().Add(info);
		}

		private bool HasInvalidPlacementOfType(InvalidPlacementType type)
		{
			return InvalidPlacements != null && InvalidPlacements.Exists(info => info.Type == type);
		}

		public void ClearAllInvalidPlacements()
		{
			EnsureInvalidPlacements().Clear();
		}

		public void ClearNotesAndSpeedEvents()
		{
			ClearNotes();
			ClearSpeedChangeEvents();
		}

		public void ClearSpeedChangeEvents()
		{
			RemoveEventsWhere(e => e.eventType == MusicScoreEventType.HighSpeed);
		}

		public void DiscardDifficultyScoreInfo()
		{
			ClearNotesAndSpeedEvents();
			EventArray = Array.Empty<EventBase>();
			FullComboDataHash = null;
		}

		private static bool IsTimeSignatureEvent(MusicScoreEventData e)
		{
			return e != null && e.eventType == MusicScoreEventType.TimeSignature;
		}

		private static int CompareEventTicks(MusicScoreEventData a, MusicScoreEventData b)
		{
			return a.ticks.CompareTo(b.ticks);
		}

		public void ApplyInfoOverlay(MusicScore infoScore)
		{
			MusicScoreEventDataList ??= new List<MusicScoreEventData>();
			RemoveEventsWhere(IsTimeSignatureEvent);
			MusicScoreInfo[] musicScoreInfos = infoScore?.musicScoreInfoArray;
			if (musicScoreInfos != null && musicScoreInfos.Length > 0)
			{
				float previousTimeSignature = -1f;
				for (int i = 0; i < musicScoreInfos.Length; i++)
				{
					MusicScoreInfo info = musicScoreInfos[i];
					if (i == 0 || info.timeSignature != previousTimeSignature)
					{
						long ticks = MusicScoreMakerUtility.CalculateTicksFromBarAndProgress(info.bar, info.barProgress, musicScoreInfos);
						AddEvent(CreateEvent(GetNewId(), MusicScoreEventType.TimeSignature, ticks, MusicScoreMakerUtility.FormatTimeSignatureText(info.timeSignature)));
						previousTimeSignature = info.timeSignature;
					}
				}
				SortEventListByTicks();
			}
			else
			{
				AddEvent(CreateEvent(GetNewId(), MusicScoreEventType.TimeSignature, 0L, MusicScoreMakerUtility.FormatTimeSignatureText(4f)));
			}
			EventArray = FilterOutSkillFeverEvents(EventArray);
		}

		public static EventBase[] BuildMergedSkillFeverEvents(MusicScore infoScore, MusicScore difficultyScore, MusicScore feverEndReferenceScore = null, MusicScoreInfo[] targetMusicScoreInfoArray = null)
		{
			return BuildSkillFeverEvents(infoScore, feverEndReferenceScore ?? difficultyScore, targetMusicScoreInfoArray, false);
		}

		public static EventBase[] BuildCreatorScoreSkillFeverEvents(MusicScore infoScore, MusicScore creatorScore, MusicScoreInfo[] targetMusicScoreInfoArray = null)
		{
			return BuildSkillFeverEvents(infoScore, creatorScore, targetMusicScoreInfoArray, true);
		}

		private static List<NoteBase> BuildJudgeSortedNotes(MusicScore difficultyScore)
		{
			if (difficultyScore?.NoteArray == null || difficultyScore.NoteArray.Length == 0)
			{
				return new List<NoteBase>(0);
			}
			int capacity = 0;
			foreach (NoteBase rootNote in difficultyScore.NoteArray)
			{
				if (rootNote?.NoteList != null)
				{
					capacity += rootNote.NoteList.Count;
				}
			}
			List<NoteBase> notes = new List<NoteBase>(capacity);
			foreach (NoteBase rootNote in difficultyScore.NoteArray)
			{
				if (rootNote?.NoteList == null)
				{
					continue;
				}
				foreach (NoteBase note in rootNote.NoteList)
				{
					if (note != null && note.HasJudgment)
					{
						notes.Add(note);
					}
				}
			}
			notes.Sort(CompareNoteBaseByTime);
			return notes;
		}

		private static int CompareNoteBaseByTime(NoteBase a, NoteBase b)
		{
			if (ReferenceEquals(a, b))
			{
				return 0;
			}
			if (a == null)
			{
				return 1;
			}
			if (b == null)
			{
				return -1;
			}
			return a.MusicScoreInfo.time.CompareTo(b.MusicScoreInfo.time);
		}

		private static int CompareEventBaseByTime(EventBase a, EventBase b)
		{
			if (ReferenceEquals(a, b))
			{
				return 0;
			}
			if (a == null)
			{
				return 1;
			}
			if (b == null)
			{
				return -1;
			}
			return a.MusicScoreInfo.time.CompareTo(b.MusicScoreInfo.time);
		}

		private static EventBase[] BuildSkillFeverEvents(MusicScore infoScore, MusicScore judgeScore, MusicScoreInfo[] targetMusicScoreInfoArray, bool markCreatorNotesFever)
		{
			EventBase[] sourceEvents = infoScore?.EventArray;
			if (sourceEvents == null || sourceEvents.Length == 0)
			{
				return Array.Empty<EventBase>();
			}
			List<EventBase> events = new List<EventBase>(sourceEvents.Length);
			List<NoteBase> judgeSortedNotes = BuildJudgeSortedNotes(judgeScore);
			if (markCreatorNotesFever)
			{
				foreach (NoteBase note in judgeSortedNotes)
				{
					note.SetFever(false);
				}
			}
			FeverBeginEvent pendingFeverBegin = null;
			foreach (EventBase sourceEvent in sourceEvents)
			{
				if (sourceEvent == null)
				{
					continue;
				}
				MusicScoreInfo eventInfo = ConvertEventInfo(sourceEvent.MusicScoreInfo, targetMusicScoreInfoArray);
				if (sourceEvent is SkillEvent)
				{
					events.Add(new SkillEvent(eventInfo));
				}
				else if (sourceEvent is FeverBeginEvent)
				{
					pendingFeverBegin = new FeverBeginEvent(eventInfo);
				}
				else if (sourceEvent is FeverStartEvent && pendingFeverBegin != null)
				{
					FeverStartEvent feverStart = new FeverStartEvent(eventInfo);
					MusicScoreInfo feverEndInfo = SetupFeverBeginAndFindEndInfo(pendingFeverBegin, feverStart, judgeSortedNotes, markCreatorNotesFever);
					events.Add(pendingFeverBegin);
					events.Add(feverStart);
					events.Add(new FeverEndEvent(feverEndInfo));
					pendingFeverBegin = null;
				}
			}
			events.Sort(CompareEventBaseByTime);
			return events.ToArray();
		}

		private static MusicScoreInfo ConvertEventInfo(MusicScoreInfo sourceInfo, MusicScoreInfo[] targetMusicScoreInfoArray)
		{
			return targetMusicScoreInfoArray == null
				? sourceInfo
				: MusicScore.GenerateNoteMusicScoreInfo(sourceInfo.bar, sourceInfo.barProgress, targetMusicScoreInfoArray);
		}

		private static MusicScoreInfo SetupFeverBeginAndFindEndInfo(FeverBeginEvent feverBegin, FeverStartEvent feverStart, List<NoteBase> judgeSortedNotes, bool markCreatorNotesFever)
		{
			if (judgeSortedNotes == null || judgeSortedNotes.Count == 0)
			{
				feverBegin.Setup(0);
				return feverStart.MusicScoreInfo;
			}
			float feverBeginTime = feverBegin.MusicScoreInfo.time;
			float feverStartTime = feverStart.MusicScoreInfo.time;
			float feverCountEndTime = feverBeginTime + (feverStartTime - feverBeginTime) * 0.9f;
			int feverNoteCount = 0;
			for (int i = 0; i < judgeSortedNotes.Count; i++)
			{
				NoteBase note = judgeSortedNotes[i];
				if (note == null)
				{
					continue;
				}
				float noteTime = note.MusicScoreInfo.time;
				if (noteTime >= feverBeginTime && noteTime < feverCountEndTime)
				{
					feverNoteCount++;
					if (markCreatorNotesFever)
					{
						note.SetFever(true);
					}
				}
			}
			feverBegin.Setup(feverNoteCount);
			for (int i = 0; i < judgeSortedNotes.Count; i++)
			{
				NoteBase note = judgeSortedNotes[i];
				if (note == null)
				{
					continue;
				}
				if (note.MusicScoreInfo.time >= feverStartTime)
				{
					int endIndex = Math.Min(i + Math.Max(1, judgeSortedNotes.Count / 10) - 1, judgeSortedNotes.Count - 1);
					return judgeSortedNotes[endIndex].MusicScoreInfo;
				}
			}
			return feverStart.MusicScoreInfo;
		}

		private static bool IsExcludedCategory(NoteCategory category)
		{
			return ExcludedCategories.Contains(category);
		}

		public static bool HasThreeOrMoreJudgmentNotesAtSameTicks(MusicScoreMakerData data)
		{
			if (data?.NoteList == null)
			{
				return false;
			}
			Dictionary<long, int> counts = new Dictionary<long, int>();
			foreach (MusicScoreNoteBase note in data.NoteList)
			{
				if (note == null || IsExcludedCategory(note.category))
				{
					continue;
				}
				counts.TryGetValue(note.ticks, out int count);
				count++;
				if (count >= 3)
				{
					return true;
				}
				counts[note.ticks] = count;
			}
			return false;
		}

		public long GetMaxTicks()
		{
			return MusicScoreMakerUtility.FindMaxTick(NoteList, MusicScoreEventDataList);
		}

		private void CleanupInvalidPlacementNoteIds()
		{
			if (InvalidPlacements == null)
			{
				return;
			}
			Dictionary<int, MusicScoreNoteBase> cache = GetNoteIdCacheOrRebuild();
			foreach (InvalidPlacementInfo info in InvalidPlacements)
			{
				if (info?.Ids == null || !IsNoteIdBasedPlacementType(info.Type))
				{
					continue;
				}
				info.Ids.RemoveAll(id => !cache.ContainsKey(id));
			}
		}

		public void RecalculateInvalidPlacementsForTicks(IEnumerable<long> ticks)
		{
			EnsureInvalidPlacements();
			CleanupInvalidPlacementNoteIds();
			if (ticks == null)
			{
				return;
			}
			_recalcWorkTickList.Clear();
			_recalcWorkTickList.AddRange(ticks);
			MusicScoreMakerUtility.SortAndUniqueInPlace(_recalcWorkTickList);
			foreach (long tick in _recalcWorkTickList)
			{
				RemoveInvalidPlacementsByTypeAndTicks(InvalidPlacementType.LaneOverlap, tick);
				AddLaneOverlapPlacementsForTick(tick);
			}
		}

		public void RecalculateInvalidPlacements()
		{
			ClearAllInvalidPlacements();
			if (NoteList == null || NoteList.Count == 0)
			{
				return;
			}
			_recalcWorkTickList.Clear();
			foreach (MusicScoreNoteBase note in NoteList)
			{
				if (note != null)
				{
					_recalcWorkTickList.Add(note.ticks);
				}
			}
			MusicScoreMakerUtility.SortAndUniqueInPlace(_recalcWorkTickList);
			foreach (long tick in _recalcWorkTickList)
			{
				AddLaneOverlapPlacementsForTick(tick);
			}
		}

		public void RecalculateJudgmentNoteGapPlacements(float scoreLengthSec)
		{
			RemoveInvalidPlacementsByType(InvalidPlacementType.JudgmentNoteGap);
			if (MusicScoreEventDataList == null || NoteList == null)
			{
				return;
			}
			float startCheckSec = GetJudgmentNoteGapExcludeMarginSec();
			float endCheckSec = scoreLengthSec - GetJudgmentNoteGapExcludeMarginSec();
			if (endCheckSec <= startCheckSec)
			{
				return;
			}
			CollectJudgmentNoteCoveredRanges();
			MergeOverlappingRanges(_judgmentNoteGapWorkRanges);
			MusicScoreInfo[] musicScoreInfoArray = MusicScoreMakerUtility.ConvertMusicScoreInfo(MusicScoreEventDataList);
			long startCheckTicks = MusicScoreMakerUtility.GetTicksFromTime(startCheckSec, MusicScoreEventDataList);
			long endCheckTicks = MusicScoreMakerUtility.GetTicksFromTime(endCheckSec, MusicScoreEventDataList);
			EnsureInvalidPlacements();
			AddGapPlacementsFromMergedRanges(musicScoreInfoArray, startCheckSec, endCheckSec, startCheckTicks, endCheckTicks);
		}

		public void RecalculateNoteDensityPlacements(float scoreLengthSec)
		{
			RemoveInvalidPlacementsByType(InvalidPlacementType.NoteDensityOverflow);
			if (MusicScoreEventDataList == null || NoteList == null || scoreLengthSec <= 0f)
			{
				return;
			}
			CollectNoteDensityTicks(scoreLengthSec);
			int threshold = GetNoteDensityThresholdCount();
			int count = _noteDensityWorkTicks.Count;
			if (count < threshold || threshold <= 0)
			{
				return;
			}
			float windowSec = GetNoteDensityWindowSec();
			MusicScoreInfo[] musicScoreInfoArray = MusicScoreMakerUtility.ConvertMusicScoreInfo(MusicScoreEventDataList);
			_noteDensityOverflowWorkRanges.Clear();
			for (int i = 0; i < count; i++)
			{
				long startTicks = _noteDensityWorkTicks[i];
				float endTime = MusicScoreMakerUtility.GetTimeFromTicks(startTicks, musicScoreInfoArray) + windowSec;
				if (endTime > scoreLengthSec)
				{
					break;
				}
				long endTicks = MusicScoreMakerUtility.GetTicksFromTime(endTime, MusicScoreEventDataList);
				int endIndex = LowerBound(_noteDensityWorkTicks, i, count, endTicks);
				if (endIndex - i >= threshold)
				{
					_noteDensityOverflowWorkRanges.Add((startTicks, endTicks));
				}
			}
			if (_noteDensityOverflowWorkRanges.Count == 0)
			{
				return;
			}
			MergeOverlappingRanges(_noteDensityOverflowWorkRanges);
			foreach ((long start, long end) in _noteDensityOverflowWorkRanges)
			{
				AddInvalidPlacement(new InvalidPlacementInfo
				{
					Type = InvalidPlacementType.NoteDensityOverflow,
					Ticks = start,
					EndTicks = end,
					Ids = EmptyNoteIdsForGap,
					NoteDensityThreshold = threshold,
					NoteDensityActualCount = CountTicksInRange(_noteDensityWorkTicks, count, start, end),
					NoteDensityWindowSec = windowSec
				});
			}
		}

		public (int, long, long) CalculateMaxNoteDensity(float scoreLengthSec)
		{
			if (MusicScoreEventDataList == null || NoteList == null || scoreLengthSec <= 0f)
			{
				return (0, 0L, 0L);
			}
			CollectNoteDensityTicks(scoreLengthSec);
			int count = _noteDensityWorkTicks.Count;
			if (count == 0)
			{
				return (0, 0L, 0L);
			}
			float windowSec = GetNoteDensityWindowSec();
			MusicScoreInfo[] musicScoreInfoArray = MusicScoreMakerUtility.ConvertMusicScoreInfo(MusicScoreEventDataList);
			int maxCount = 0;
			long maxStartTicks = 0L;
			long maxEndTicks = 0L;
			for (int i = 0; i < count; i++)
			{
				long startTicks = _noteDensityWorkTicks[i];
				float endTime = MusicScoreMakerUtility.GetTimeFromTicks(startTicks, musicScoreInfoArray) + windowSec;
				if (endTime > scoreLengthSec)
				{
					break;
				}
				long endTicks = MusicScoreMakerUtility.GetTicksFromTime(endTime, MusicScoreEventDataList);
				int actualCount = LowerBound(_noteDensityWorkTicks, i, count, endTicks) - i;
				if (actualCount > maxCount)
				{
					maxCount = actualCount;
					maxStartTicks = startTicks;
					maxEndTicks = endTicks;
				}
			}
			return (maxCount, maxStartTicks, maxEndTicks);
		}

		public void RecalculateComboCountValidation(int minCombo)
		{
			RemoveInvalidPlacementsByTypes(InvalidPlacementType.ComboCountUnderflow, InvalidPlacementType.ComboCountOverflow);
			if (NoteList == null)
			{
				return;
			}
			NoteAndComboCountInfo countInfo = NoteAndComboCountInfo.Calculate(this);
			int totalComboCount = countInfo.TotalComboCount;
			int maxCombo = GetComboCountMaximum();
			if (totalComboCount >= minCombo && totalComboCount <= maxCombo)
			{
				return;
			}
			AddInvalidPlacement(new InvalidPlacementInfo
			{
				Type = totalComboCount < minCombo ? InvalidPlacementType.ComboCountUnderflow : InvalidPlacementType.ComboCountOverflow,
				Ticks = GetFirstNoteTicksOrZero(),
				Ids = new List<int>(),
				ActualComboCount = totalComboCount,
				ComboCountMinimum = minCombo,
				ComboCountMaximum = maxCombo
			});
		}

		public void RecalculateTapNotesMinimumValidation()
		{
			RemoveInvalidPlacementsByType(InvalidPlacementType.TapNotesUnderflow);
			if (NoteList == null)
			{
				return;
			}
			int tapNotesCount = 0;
			foreach (MusicScoreNoteBase note in NoteList)
			{
				if (note != null && (note.category == NoteCategory.Normal || note.category == NoteCategory.Long || note.category == NoteCategory.Flick))
				{
					tapNotesCount++;
				}
			}
			int minimum = GetTapNotesMinimumCount();
			if (tapNotesCount >= minimum)
			{
				return;
			}
			AddInvalidPlacement(new InvalidPlacementInfo
			{
				Type = InvalidPlacementType.TapNotesUnderflow,
				Ticks = GetFirstNoteTicksOrZero(),
				ActualTapNotesCount = tapNotesCount,
				TapNotesMinimum = minimum
			});
		}

		private void CollectJudgmentNoteCoveredRanges()
		{
			_judgmentNoteGapWorkRanges.Clear();
			if (NoteList == null)
			{
				return;
			}
			Dictionary<int, MusicScoreNoteBase> noteById = GetNoteIdCacheOrRebuild();
			foreach (MusicScoreNoteBase note in NoteList)
			{
				if (note == null || note.previousConnectionId != -1)
				{
					continue;
				}
				long startTicks;
				long endTicks;
				if (note.nextConnectionId == -1)
				{
					if (IsExcludedCategory(note.category))
					{
						continue;
					}
					startTicks = note.ticks;
					endTicks = note.ticks;
				}
				else
				{
					if (note.category == NoteCategory.Guide || note.category == NoteCategory.GuideEnd || note.category == NoteCategory.GuideHidden)
					{
						continue;
					}
					startTicks = note.ticks;
					endTicks = GetEndTicks(note, noteById);
				}
				_judgmentNoteGapWorkRanges.Add((startTicks, endTicks));
			}
		}

		private static void MergeOverlappingRanges(List<(long start, long end)> ranges)
		{
			if (ranges == null || ranges.Count < 2)
			{
				return;
			}
			ranges.Sort((a, b) => a.start.CompareTo(b.start));
			int writeIndex = 0;
			for (int readIndex = 1; readIndex < ranges.Count; readIndex++)
			{
				(long start, long end) current = ranges[writeIndex];
				(long start, long end) next = ranges[readIndex];
				if (next.start > current.end)
				{
					ranges[++writeIndex] = next;
				}
				else if (next.end > current.end)
				{
					ranges[writeIndex] = (current.start, next.end);
				}
			}
			ranges.RemoveRange(writeIndex + 1, ranges.Count - writeIndex - 1);
		}

		private static int CountTicksInRange(List<long> sortedTicks, int count, long startTicks, long endTicks)
		{
			if (sortedTicks == null || count <= 0)
			{
				return 0;
			}
			int startIndex = LowerBound(sortedTicks, 0, count, startTicks);
			int endIndex = LowerBound(sortedTicks, startIndex, count, endTicks);
			return endIndex - startIndex;
		}

		private void AddGapPlacementsFromMergedRanges(MusicScoreInfo[] musicScoreInfoArray, float startCheckSec, float endCheckSec, long startCheckTicks, long endCheckTicks)
		{
			float gapThresholdSec = GetJudgmentNoteGapThresholdSec();
			if (_judgmentNoteGapWorkRanges.Count == 0)
			{
				AddJudgmentGapPlacement(startCheckTicks, endCheckTicks, endCheckSec - startCheckSec, gapThresholdSec);
				return;
			}
			float currentStartSec = startCheckSec;
			long currentStartTicks = startCheckTicks;
			foreach ((long rangeStartTicks, long rangeEndTicks) in _judgmentNoteGapWorkRanges)
			{
				float rangeStartSec = MusicScoreMakerUtility.GetTimeFromTicks(rangeStartTicks, musicScoreInfoArray);
				float rangeEndSec = MusicScoreMakerUtility.GetTimeFromTicks(rangeEndTicks, musicScoreInfoArray);
				if (rangeStartSec > endCheckSec)
				{
					break;
				}
				if (rangeEndSec < startCheckSec)
				{
					continue;
				}
				float gapSec = rangeStartSec - currentStartSec;
				if (gapSec >= gapThresholdSec)
				{
					AddJudgmentGapPlacement(currentStartTicks, rangeStartTicks, gapSec, gapThresholdSec);
				}
				float nextStartSec = Math.Min(rangeEndSec, endCheckSec);
				if (nextStartSec > currentStartSec)
				{
					currentStartSec = nextStartSec;
					currentStartTicks = MusicScoreMakerUtility.GetTicksFromTime(nextStartSec, MusicScoreEventDataList);
				}
			}
			float tailGapSec = endCheckSec - currentStartSec;
			if (tailGapSec >= gapThresholdSec)
			{
				AddJudgmentGapPlacement(currentStartTicks, endCheckTicks, tailGapSec, gapThresholdSec);
			}
		}

		public void UpdateExistingJudgmentNoteGapPlacements(float scoreLengthSec)
		{
			if (!HasInvalidPlacementOfType(InvalidPlacementType.JudgmentNoteGap))
			{
				return;
			}
			// TODO(original): restore the original range-local recomputation. A full recompute is behaviorally safe
			// for the editor and avoids stale gap warnings while the optimized path is still being recovered.
			RecalculateJudgmentNoteGapPlacements(scoreLengthSec);
		}

		public void RecalculateInvalidTimeSignaturePlacements()
		{
			RemoveInvalidPlacementsByType(InvalidPlacementType.TimeSignatureOffset);
			if (MusicScoreEventDataList == null || MusicScoreEventDataList.Count == 0)
			{
				return;
			}
			List<MusicScoreEventData> timeSignatureEvents = new List<MusicScoreEventData>();
			foreach (MusicScoreEventData eventData in MusicScoreEventDataList)
			{
				if (eventData != null && eventData.eventType == MusicScoreEventType.TimeSignature)
				{
					timeSignatureEvents.Add(eventData);
				}
			}
			foreach (MusicScoreEventData eventData in timeSignatureEvents)
			{
				List<MusicScoreEventData> referenceEvents = new List<MusicScoreEventData>(MusicScoreEventDataList);
				referenceEvents.RemoveAll(e => ReferenceEquals(e, eventData));
				long correctTicks = MusicScoreMakerUtility.SnapToBarStartPhysical(eventData.ticks, referenceEvents);
				if (eventData.ticks == correctTicks)
				{
					continue;
				}
				AddInvalidPlacement(new InvalidPlacementInfo
				{
					Type = InvalidPlacementType.TimeSignatureOffset,
					Ticks = eventData.ticks,
					CorrectBarStartTicks = correctTicks,
					Ids = new List<int> { eventData.id }
				});
			}
		}

		public void RecalculateLongNoteMeshOverflowPlacements(float scoreLengthSec)
		{
			RemoveInvalidPlacementsByType(InvalidPlacementType.LongNoteMeshOverflow);
			if (MusicScoreEventDataList == null || NoteList == null || scoreLengthSec <= 0f)
			{
				return;
			}
			MusicScoreInfo[] musicScoreInfoArray = MusicScoreMakerUtility.ConvertMusicScoreInfo(MusicScoreEventDataList);
			Dictionary<int, MusicScoreNoteBase> noteById = GetNoteIdCacheOrRebuild();
			_longNoteMeshOverflowResults.Clear();
			DetectLongNoteMeshOverflow(musicScoreInfoArray, noteById, LongNoteMeshDisplayTimeFastest, scoreLengthSec, MeshOverflowSpeedType.Fastest, _longNoteMeshOverflowResults);
			DetectLongNoteMeshOverflow(musicScoreInfoArray, noteById, LongNoteMeshDisplayTimeSlowest, scoreLengthSec, MeshOverflowSpeedType.Slowest, _longNoteMeshOverflowResults);
			MergeMeshOverflowResults(_longNoteMeshOverflowResults);
			foreach ((long startTicks, long endTicks, int maxMesh, MeshOverflowSpeedType speedType) in _longNoteMeshOverflowResults)
			{
				AddLongNoteMeshOverflowPlacement(startTicks, endTicks, maxMesh, speedType);
			}
		}

		private void DetectLongNoteMeshOverflow(MusicScoreInfo[] musicScoreInfoArray, Dictionary<int, MusicScoreNoteBase> noteById, float displayTime, float scoreLengthSec, MeshOverflowSpeedType speedType, List<(long startTicks, long endTicks, int maxMesh, MeshOverflowSpeedType speedType)> results)
		{
			CollectLongNoteSegments(musicScoreInfoArray, displayTime, scoreLengthSec, noteById);
			DetectMeshOverflowFromSegments(_longNoteMeshWorkSegments, displayTime, speedType, results);
		}

		private void AddLongNoteMeshOverflowPlacement(long startTicks, long endTicks, int maxMeshCount, MeshOverflowSpeedType speedType)
		{
			AddInvalidPlacement(new InvalidPlacementInfo
			{
				Type = InvalidPlacementType.LongNoteMeshOverflow,
				Ticks = startTicks,
				EndTicks = endTicks,
				Ids = EmptyNoteIdsForGap,
				EstimatedMeshCount = maxMeshCount,
				MeshPoolLimit = LongNoteMeshOverflowThreshold,
				OverflowSpeedType = speedType
			});
		}

		private void CollectLongNoteSegments(MusicScoreInfo[] musicScoreInfoArray, float baseDisplayTime, float scoreLengthSec, Dictionary<int, MusicScoreNoteBase> noteById)
		{
			_longNoteMeshWorkSegments.Clear();
			CollectMeshSegments(musicScoreInfoArray, baseDisplayTime, scoreLengthSec, noteById, _longNoteMeshWorkSegments, false);
		}

		public void RecalculateGuideMeshOverflowPlacements(float scoreLengthSec)
		{
			RemoveInvalidPlacementsByType(InvalidPlacementType.GuideMeshOverflow);
			if (MusicScoreEventDataList == null || NoteList == null || scoreLengthSec <= 0f)
			{
				return;
			}
			MusicScoreInfo[] musicScoreInfoArray = MusicScoreMakerUtility.ConvertMusicScoreInfo(MusicScoreEventDataList);
			Dictionary<int, MusicScoreNoteBase> noteById = GetNoteIdCacheOrRebuild();
			_guideMeshOverflowResults.Clear();
			DetectGuideMeshOverflow(musicScoreInfoArray, noteById, LongNoteMeshDisplayTimeFastest, scoreLengthSec, MeshOverflowSpeedType.Fastest, _guideMeshOverflowResults);
			DetectGuideMeshOverflow(musicScoreInfoArray, noteById, LongNoteMeshDisplayTimeSlowest, scoreLengthSec, MeshOverflowSpeedType.Slowest, _guideMeshOverflowResults);
			MergeMeshOverflowResults(_guideMeshOverflowResults);
			foreach ((long startTicks, long endTicks, int maxMesh, MeshOverflowSpeedType speedType) in _guideMeshOverflowResults)
			{
				AddGuideMeshOverflowPlacement(startTicks, endTicks, maxMesh, speedType);
			}
		}

		private void DetectGuideMeshOverflow(MusicScoreInfo[] musicScoreInfoArray, Dictionary<int, MusicScoreNoteBase> noteById, float displayTime, float scoreLengthSec, MeshOverflowSpeedType speedType, List<(long startTicks, long endTicks, int maxMesh, MeshOverflowSpeedType speedType)> results)
		{
			CollectGuideSegments(musicScoreInfoArray, displayTime, scoreLengthSec, noteById);
			DetectMeshOverflowFromSegments(_guideMeshWorkSegments, displayTime, speedType, results);
		}

		private void AddGuideMeshOverflowPlacement(long startTicks, long endTicks, int maxMeshCount, MeshOverflowSpeedType speedType)
		{
			AddInvalidPlacement(new InvalidPlacementInfo
			{
				Type = InvalidPlacementType.GuideMeshOverflow,
				Ticks = startTicks,
				EndTicks = endTicks,
				Ids = EmptyNoteIdsForGap,
				EstimatedMeshCount = maxMeshCount,
				MeshPoolLimit = LongNoteMeshOverflowThreshold,
				OverflowSpeedType = speedType
			});
		}

		private void CollectGuideSegments(MusicScoreInfo[] musicScoreInfoArray, float baseDisplayTime, float scoreLengthSec, Dictionary<int, MusicScoreNoteBase> noteById)
		{
			_guideMeshWorkSegments.Clear();
			CollectMeshSegments(musicScoreInfoArray, baseDisplayTime, scoreLengthSec, noteById, _guideMeshWorkSegments, true);
		}

		private static float GetMinHighSpeedRatioInRange(MusicScoreInfo[] musicScoreInfoArray, float startTime, float endTime)
		{
			float minSpeed = 1f;
			if (musicScoreInfoArray == null || musicScoreInfoArray.Length == 0)
			{
				return minSpeed;
			}
			float activeSpeed = musicScoreInfoArray[0].speedRatio;
			for (int i = 0; i < musicScoreInfoArray.Length; i++)
			{
				if (musicScoreInfoArray[i].time <= startTime)
				{
					activeSpeed = musicScoreInfoArray[i].speedRatio;
				}
				else
				{
					break;
				}
			}
			minSpeed = Math.Min(activeSpeed, 1f);
			for (int i = 0; i < musicScoreInfoArray.Length; i++)
			{
				MusicScoreInfo info = musicScoreInfoArray[i];
				if (info.time > endTime)
				{
					break;
				}
				if (info.time >= startTime && minSpeed >= info.speedRatio)
				{
					minSpeed = info.speedRatio;
				}
			}
			return minSpeed;
		}

		private void AddLaneOverlapPlacementsForTick(long tick)
		{
			if (NoteList == null)
			{
				return;
			}
			for (int i = 0; i < _recalcWorkLaneNoteIds.Length; i++)
			{
				_recalcWorkLaneNoteIds[i].Clear();
			}
			int noteCountAtTick = 0;
			foreach (MusicScoreNoteBase note in NoteList)
			{
				if (note == null || note.ticks != tick || IsExcludedCategory(note.category))
				{
					continue;
				}
				noteCountAtTick++;
				int laneStart = Math.Max(0, Math.Min(MusicScoreMakerModel.LaneCountMinus1, note.laneStart));
				int laneEnd = Math.Max(0, Math.Min(MusicScoreMakerModel.LaneCountMinus1, note.laneEnd));
				if (laneEnd < laneStart)
				{
					(laneStart, laneEnd) = (laneEnd, laneStart);
				}
				for (int lane = laneStart; lane <= laneEnd; lane++)
				{
					_recalcWorkLaneNoteIds[lane].Add(note.id);
				}
			}
			if (noteCountAtTick < 2)
			{
				return;
			}
			bool inOverlap = false;
			int overlapLaneStart = 0;
			_recalcWorkNoteIdSet.Clear();
			for (int lane = 0; lane <= MusicScoreMakerModel.LaneCountMinus1; lane++)
			{
				List<int> laneNoteIds = _recalcWorkLaneNoteIds[lane];
				if (laneNoteIds.Count >= 2)
				{
					if (!inOverlap)
					{
						inOverlap = true;
						overlapLaneStart = lane;
						_recalcWorkNoteIdSet.Clear();
					}
					foreach (int id in laneNoteIds)
					{
						_recalcWorkNoteIdSet.Add(id);
					}
				}
				else if (inOverlap)
				{
					AddLaneOverlapPlacement(tick, overlapLaneStart, lane - 1);
					inOverlap = false;
				}
			}
			if (inOverlap)
			{
				AddLaneOverlapPlacement(tick, overlapLaneStart, MusicScoreMakerModel.LaneCountMinus1);
			}
		}

		private void AddLaneOverlapPlacement(long tick, int laneStart, int laneEnd)
		{
			if (laneEnd < laneStart)
			{
				return;
			}
			_tmpNoteIdsList.Clear();
			foreach (int id in _recalcWorkNoteIdSet)
			{
				_tmpNoteIdsList.Add(id);
			}
			AddInvalidPlacement(new InvalidPlacementInfo
			{
				Type = InvalidPlacementType.LaneOverlap,
				Ticks = tick,
				OverlapLaneStart = laneStart,
				OverlapLaneEnd = laneEnd,
				Ids = new List<int>(_tmpNoteIdsList)
			});
		}

		private void AddJudgmentGapPlacement(long startTicks, long endTicks, float actualGapSec, float thresholdSec)
		{
			AddInvalidPlacement(new InvalidPlacementInfo
			{
				Type = InvalidPlacementType.JudgmentNoteGap,
				Ticks = startTicks,
				EndTicks = endTicks,
				Ids = EmptyNoteIdsForGap,
				GapThresholdSec = thresholdSec,
				GapActualSec = actualGapSec
			});
		}

		private void CollectNoteDensityTicks(float scoreLengthSec)
		{
			_noteDensityWorkTicks.Clear();
			long startTicks = MusicScoreMakerUtility.GetTicksFromTime(0f, MusicScoreEventDataList);
			long endTicks = MusicScoreMakerUtility.GetTicksFromTime(scoreLengthSec, MusicScoreEventDataList);
			foreach (MusicScoreNoteBase note in NoteList)
			{
				if (note == null || IsNoteDensityExcludedCategory(note.category))
				{
					continue;
				}
				if (note.ticks >= startTicks && note.ticks < endTicks)
				{
					_noteDensityWorkTicks.Add(note.ticks);
				}
			}
			_noteDensityWorkTicks.Sort();
		}

		private static int LowerBound(List<long> sortedTicks, int startIndex, int count, long value)
		{
			int low = startIndex;
			int high = Math.Min(count, sortedTicks?.Count ?? 0);
			while (low < high)
			{
				int mid = (low + high) >> 1;
				if (sortedTicks[mid] < value)
				{
					low = mid + 1;
				}
				else
				{
					high = mid;
				}
			}
			return low;
		}

		private static bool IsNoteDensityExcludedCategory(NoteCategory category)
		{
			return category == NoteCategory.FrictionHide
				|| category == NoteCategory.FrictionHideLong
				|| category == NoteCategory.Guide
				|| category == NoteCategory.GuideHidden;
		}

		private long GetFirstNoteTicksOrZero()
		{
			return NoteList != null && NoteList.Count > 0 && NoteList[0] != null ? NoteList[0].ticks : 0L;
		}

		private static float GetJudgmentNoteGapExcludeMarginSec()
		{
			// TODO(original): read ClientConfig.MusicScoreMaker.JudgmentNoteGapExcludeMarginSec once ClientConfig is restored.
			return 0f;
		}

		private static int GetComboCountMaximum()
		{
			// Original ClientConfig.MusicScoreMaker.ComboCountMaximum, clientConfig id=200 in 6.5.0.51.
			return 5000;
		}

		private static int GetTapNotesMinimumCount()
		{
			// Original ClientConfig.MusicScoreMaker.TapNotesMinimumCount, clientConfig id=208 in 6.5.0.51.
			return 80;
		}

		private void CollectMeshSegments(
			MusicScoreInfo[] musicScoreInfoArray,
			float baseDisplayTime,
			float scoreLengthSec,
			Dictionary<int, MusicScoreNoteBase> noteById,
			List<(float segStartTime, float segEndTime, long startTicks, long endTicks, float displayTime)> results,
			bool collectGuide)
		{
			// TODO(original): restore the exact Mesh split estimation used by the runtime renderer.
			foreach (MusicScoreNoteBase note in NoteList)
			{
				if (note == null || note.previousConnectionId != -1 || note.nextConnectionId == -1)
				{
					continue;
				}
				if (collectGuide != IsGuideMeshCategory(note.category))
				{
					continue;
				}
				MusicScoreNoteBase current = note;
				while (current.nextConnectionId != -1 && noteById.TryGetValue(current.nextConnectionId, out MusicScoreNoteBase next))
				{
					float startTime = MusicScoreMakerUtility.GetTimeFromTicks(current.ticks, musicScoreInfoArray);
					float endTime = MusicScoreMakerUtility.GetTimeFromTicks(next.ticks, musicScoreInfoArray);
					if (endTime >= 0f && startTime <= scoreLengthSec)
					{
						float speedRatio = GetMinHighSpeedRatioInRange(musicScoreInfoArray, startTime, endTime);
						float displayTime = speedRatio > 0f ? baseDisplayTime / speedRatio : baseDisplayTime;
						results.Add((startTime, endTime, current.ticks, next.ticks, displayTime));
					}
					current = next;
				}
			}
		}

		private static bool IsGuideMeshCategory(NoteCategory category)
		{
			return category == NoteCategory.Guide || category == NoteCategory.GuideEnd || category == NoteCategory.GuideHidden;
		}

		private static void DetectMeshOverflowFromSegments(
			List<(float segStartTime, float segEndTime, long startTicks, long endTicks, float displayTime)> segments,
			float fallbackDisplayTime,
			MeshOverflowSpeedType speedType,
			List<(long startTicks, long endTicks, int maxMesh, MeshOverflowSpeedType speedType)> results)
		{
			if (segments == null || segments.Count == 0)
			{
				return;
			}
			List<(float time, int delta, long ticks)> sweep = new List<(float time, int delta, long ticks)>(segments.Count * 2);
			foreach ((float segStartTime, float segEndTime, long startTicks, long endTicks, float displayTime) in segments)
			{
				float visibleStart = segStartTime - Math.Max(displayTime, fallbackDisplayTime);
				sweep.Add((visibleStart, 1, startTicks));
				sweep.Add((segEndTime, -1, endTicks));
			}
			sweep.Sort((a, b) =>
			{
				int compare = a.time.CompareTo(b.time);
				return compare != 0 ? compare : a.delta.CompareTo(b.delta);
			});
			int activeSegments = 0;
			float rangeStartTime = 0f;
			long rangeStartTicks = 0L;
			int maxMeshInRange = 0;
			for (int i = 0; i < sweep.Count; i++)
			{
				(float time, int delta, long ticks) = sweep[i];
				int estimatedMesh = activeSegments * LongNoteMeshSplitCount;
				if (estimatedMesh > LongNoteMeshOverflowThreshold && maxMeshInRange == 0)
				{
					rangeStartTime = time;
					rangeStartTicks = ticks;
					maxMeshInRange = estimatedMesh;
				}
				if (maxMeshInRange > 0)
				{
					maxMeshInRange = Math.Max(maxMeshInRange, estimatedMesh);
				}
				activeSegments += delta;
				if (maxMeshInRange > 0 && activeSegments * LongNoteMeshSplitCount <= LongNoteMeshOverflowThreshold)
				{
					long endTicks = ticks;
					if (endTicks < rangeStartTicks)
					{
						endTicks = rangeStartTicks;
					}
					results.Add((rangeStartTicks, endTicks, maxMeshInRange, speedType));
					rangeStartTime = 0f;
					maxMeshInRange = 0;
				}
			}
			_ = rangeStartTime;
		}

		private static void MergeMeshOverflowResults(List<(long startTicks, long endTicks, int maxMesh, MeshOverflowSpeedType speedType)> results)
		{
			if (results == null || results.Count < 2)
			{
				return;
			}
			results.Sort((a, b) => a.startTicks.CompareTo(b.startTicks));
			int writeIndex = 0;
			for (int readIndex = 1; readIndex < results.Count; readIndex++)
			{
				(long startTicks, long endTicks, int maxMesh, MeshOverflowSpeedType speedType) current = results[writeIndex];
				(long startTicks, long endTicks, int maxMesh, MeshOverflowSpeedType speedType) next = results[readIndex];
				if (next.startTicks > current.endTicks)
				{
					results[++writeIndex] = next;
				}
				else
				{
					results[writeIndex] = (
						current.startTicks,
						Math.Max(current.endTicks, next.endTicks),
						Math.Max(current.maxMesh, next.maxMesh),
						current.speedType | next.speedType);
				}
			}
			results.RemoveRange(writeIndex + 1, results.Count - writeIndex - 1);
		}

		public MusicScore ToMusicScore(LiveBundleBuildData liveBundleBuildData, long? filterBeforeTicks = null, bool isMirror = false)
		{
			MusicScoreMakerData conversionData = CloneForMusicScoreConversion();
			conversionData.MusicScoreEventDataList ??= new List<MusicScoreEventData>();
			MusicScoreEventData existingTimeSignature = conversionData.MusicScoreEventDataList.Find(e => e != null && e.eventType == MusicScoreEventType.TimeSignature);
			int timeSignatureId = existingTimeSignature != null ? existingTimeSignature.id : GetNewId();
			conversionData.RemoveEventsWhere(e => e != null && e.eventType == MusicScoreEventType.TimeSignature);
			conversionData.AddEvent(new MusicScoreEventData
			{
				id = timeSignatureId,
				eventType = MusicScoreEventType.TimeSignature,
				ticks = 0L,
				changeValue = "4/4"
			});
			MusicScoreInfo[] musicScoreInfos = MusicScoreMakerUtility.ConvertMusicScoreInfo(conversionData.MusicScoreEventDataList);
			NoteBase[] noteArray = ConvertNoteArray(conversionData.NoteList, musicScoreInfos, liveBundleBuildData, conversionData, filterBeforeTicks);
			EventBase[] eventArray = FilterEventArray(EventArray, filterBeforeTicks, musicScoreInfos);
			MusicScore musicScore = new MusicScore(musicScoreInfos, eventArray, noteArray);
			if (filterBeforeTicks.HasValue && filterBeforeTicks.Value >= 1L)
			{
				musicScore.FilterBeforeTimeSec = MusicScoreMakerUtility.GetTimeFromTicks(filterBeforeTicks.Value, musicScoreInfos);
			}
			if (isMirror)
			{
				ApplyMirrorToNoteArray(noteArray);
			}
			return musicScore;
		}

		private static void ApplyMirrorToNoteArray(NoteBase[] noteArray)
		{
			if (noteArray == null)
			{
				return;
			}
			foreach (NoteBase rootNote in noteArray)
			{
				if (rootNote?.NoteList == null)
				{
					continue;
				}
				foreach (NoteBase note in rootNote.NoteList)
				{
					ApplyMirrorToNote(note);
				}
			}
		}

		private static void ApplyMirrorToNote(NoteBase note)
		{
			if (note == null)
			{
				return;
			}
			int defaultLeftLane = note.DefaultLeftLane;
			int defaultRightLane = note.DefaultRightLane;
			note.DefaultLeftLane = MusicScoreMakerModel.LaneCountMinus1 - defaultRightLane;
			note.DefaultRightLane = MusicScoreMakerModel.LaneCountMinus1 - defaultLeftLane;
			int laneStart = note.LaneStart;
			int laneEnd = note.LaneEnd;
			note.LaneStart = MusicScoreMakerModel.LaneCountMinus1 - laneEnd;
			note.LaneEnd = MusicScoreMakerModel.LaneCountMinus1 - laneStart;
			float laneStartF = note.LaneStartF;
			float laneEndF = note.LaneEndF;
			note.LaneStartF = MusicScoreMakerModel.LaneCountMinus1 - laneEndF;
			note.LaneEndF = MusicScoreMakerModel.LaneCountMinus1 - laneStartF;
			float judgeLaneStart = note.JudgeLaneStart;
			float judgeLaneEnd = note.JudgeLaneEnd;
			note.JudgeLaneStart = MusicScoreMakerModel.LaneCountMinus1 - judgeLaneEnd;
			note.JudgeLaneEnd = MusicScoreMakerModel.LaneCountMinus1 - judgeLaneStart;
			if (note.Direction == NoteDirection.Left)
			{
				note.Direction = NoteDirection.Right;
			}
			else if (note.Direction == NoteDirection.Right)
			{
				note.Direction = NoteDirection.Left;
			}
		}

		private NoteBase[] ConvertNoteArray(List<MusicScoreNoteBase> noteArray, MusicScoreInfo[] eventBaseArray, LiveBundleBuildData liveBundleBuildData, MusicScoreMakerData musicScoreMakerDataForConversion, long? filterBeforeTicks = null)
		{
			if (noteArray == null)
			{
				return Array.Empty<NoteBase>();
			}
			NormalizeLongEndNoteBaseTypes(noteArray);
			ConvertLongNoteTraceToNoJudgment(noteArray);
			HashSet<int> validStartNoteIds = new HashSet<int>();
			if (filterBeforeTicks.HasValue && filterBeforeTicks.Value >= 1L && musicScoreMakerDataForConversion != null)
			{
				Dictionary<int, MusicScoreNoteBase> noteById = musicScoreMakerDataForConversion.GetNoteIdCacheOrRebuild();
				foreach (MusicScoreNoteBase note in noteArray)
				{
					if (note == null)
					{
						continue;
					}
					if (GetEndTicks(note, noteById) >= filterBeforeTicks.Value)
					{
						MusicScoreNoteBase startNote = GetStartNote(note, noteById);
						if (startNote != null)
						{
							validStartNoteIds.Add(startNote.id);
						}
					}
				}
			}
			List<MusicScoreNoteBase> startNotes = new List<MusicScoreNoteBase>();
			foreach (MusicScoreNoteBase note in noteArray)
			{
				if (note == null || note.previousConnectionId != -1)
				{
					continue;
				}
				if (filterBeforeTicks.HasValue && filterBeforeTicks.Value >= 1L && !validStartNoteIds.Contains(note.id))
				{
					continue;
				}
				startNotes.Add(note);
			}
			startNotes.Sort(CompareNoteByTicks);
			List<NoteBase> liveNotes = new List<NoteBase>(startNotes.Count);
			foreach (MusicScoreNoteBase note in startNotes)
			{
				NoteBase liveNote = SetupNote(note, noteArray, eventBaseArray, liveBundleBuildData, musicScoreMakerDataForConversion);
				if (liveNote != null)
				{
					liveNotes.Add(liveNote);
				}
			}
			foreach (NoteBase note in liveNotes)
			{
				if (note?.NoteList == null)
				{
					continue;
				}
				foreach (NoteBase childNote in note.NoteList)
				{
					if (childNote != null)
					{
						childNote.Id = ++_idCount;
					}
				}
			}
			NoteBase[] result = liveNotes.ToArray();
			SetPairNotes(result);
			return result;
		}

		private static long GetEndTicks(MusicScoreNoteBase note, Dictionary<int, MusicScoreNoteBase> noteById)
		{
			if (note == null)
			{
				throw new ArgumentNullException(nameof(note));
			}
			MusicScoreNoteBase current = note;
			while (current.nextConnectionId != -1 && noteById != null && noteById.TryGetValue(current.nextConnectionId, out MusicScoreNoteBase next))
			{
				current = next;
			}
			return current.ticks;
		}

		private static MusicScoreNoteBase GetStartNote(MusicScoreNoteBase note, Dictionary<int, MusicScoreNoteBase> noteById)
		{
			if (note == null)
			{
				throw new ArgumentNullException(nameof(note));
			}
			MusicScoreNoteBase current = note;
			while (current.previousConnectionId != -1 && noteById != null && noteById.TryGetValue(current.previousConnectionId, out MusicScoreNoteBase previous))
			{
				current = previous;
			}
			return current;
		}

		private static EventBase[] FilterEventArray(EventBase[] eventArray, long? filterBeforeTicks, MusicScoreInfo[] musicScoreInfoArray)
		{
			if (eventArray == null)
			{
				return Array.Empty<EventBase>();
			}
			if (!filterBeforeTicks.HasValue)
			{
				return eventArray;
			}
			List<EventBase> result = new List<EventBase>(eventArray.Length);
			foreach (EventBase eventBase in eventArray)
			{
				if (eventBase == null)
				{
					continue;
				}
				long ticks = MusicScoreMakerUtility.GetTicksFromTime(eventBase.MusicScoreInfo.time, musicScoreInfoArray);
				if (ticks < filterBeforeTicks.Value)
				{
					result.Add(eventBase);
				}
			}
			return result.ToArray();
		}

		private static EventBase[] FilterOutSkillFeverEvents(EventBase[] eventArray)
		{
			if (eventArray == null)
			{
				return Array.Empty<EventBase>();
			}
			List<EventBase> result = new List<EventBase>(eventArray.Length);
			foreach (EventBase eventBase in eventArray)
			{
				if (eventBase == null || eventBase is SkillEvent || eventBase is FeverStartEvent || eventBase is FeverBeginEvent || eventBase is FeverEndEvent)
				{
					continue;
				}
				result.Add(eventBase);
			}
			return result.ToArray();
		}

		private static void ConvertLongNoteTraceToNoJudgment(List<MusicScoreNoteBase> noteArray)
		{
			if (noteArray == null)
			{
				return;
			}
			int nextId = 1;
			foreach (MusicScoreNoteBase note in noteArray)
			{
				if (note != null && note.id >= nextId)
				{
					nextId = note.id + 1;
				}
			}
			foreach (MusicScoreNoteBase note in new List<MusicScoreNoteBase>(noteArray))
			{
				if (note == null)
				{
					continue;
				}
				if (note.previousConnectionId == -1)
				{
					if (note.nextConnectionId != -1 && note.category == NoteCategory.FrictionLong)
					{
						note.category = NoteCategory.FrictionHideLong;
						note.noteBaseType = MusicScoreNoteBase.NoteBaseType.FrictionHideLong;
						noteArray.Add(CreateTraceNote(nextId++, note));
					}
				}
				else if (note.nextConnectionId == -1 && note.category == NoteCategory.Friction)
				{
					note.category = NoteCategory.FrictionHide;
					note.noteBaseType = MusicScoreNoteBase.NoteBaseType.FrictionHide;
					noteArray.Add(CreateTraceNote(nextId++, note));
				}
			}
			noteArray.Sort(CompareNoteByTicks);
		}

		private static void NormalizeLongEndNoteBaseTypes(List<MusicScoreNoteBase> noteArray)
		{
			if (noteArray == null)
			{
				return;
			}

			foreach (MusicScoreNoteBase note in noteArray)
			{
				if (note == null || note.previousConnectionId == -1 || note.nextConnectionId != -1)
				{
					continue;
				}

				switch (note.category)
				{
					case NoteCategory.Long:
						note.noteBaseType = MusicScoreNoteBase.NoteBaseType.Normal;
						break;
					case NoteCategory.FrictionLong:
						note.category = NoteCategory.Friction;
						note.noteBaseType = MusicScoreNoteBase.NoteBaseType.Friction;
						break;
					case NoteCategory.FrictionHideLong:
						note.category = NoteCategory.FrictionHide;
						note.noteBaseType = MusicScoreNoteBase.NoteBaseType.FrictionHide;
						break;
				}
			}
		}

		private static MusicScoreNoteBase CreateTraceNote(int id, MusicScoreNoteBase sourceNote)
		{
			if (sourceNote == null)
			{
				throw new ArgumentNullException(nameof(sourceNote));
			}
			return new MusicScoreNoteBase(
				id,
				sourceNote.ticks,
				sourceNote.laneStart,
				sourceNote.laneEnd,
				NoteCategory.Friction,
				sourceNote.type,
				sourceNote.speedRatio,
				sourceNote.noteLineType,
				MusicScoreNoteBase.NoteBaseType.Friction,
				true,
				sourceNote.direction);
		}

		private NoteBase SetupNote(MusicScoreNoteBase note, List<MusicScoreNoteBase> noteArray, MusicScoreInfo[] musicScoreInfos, LiveBundleBuildData liveBundleBuildData, MusicScoreMakerData musicScoreMakerData)
		{
			if (note == null)
			{
				return null;
			}
			NoteBase liveNote = note.ToNoteBase(liveBundleBuildData, noteArray, musicScoreInfos, musicScoreMakerData);
			if (liveNote == null)
			{
				return null;
			}
			if (liveNote.NoteList != null)
			{
				for (int i = 1; i < liveNote.NoteList.Count; i++)
				{
					NoteBase childNote = liveNote.NoteList[i];
					if (childNote != null && childNote.IsSkip)
					{
						CalcSkipNoteLane(liveBundleBuildData, childNote);
					}
				}
			}
			if (liveNote.Category != NoteCategory.Long && liveNote.Category != NoteCategory.FrictionLong && liveNote.Category != NoteCategory.FrictionHideLong)
			{
				return liveNote;
			}
			if (liveNote is LongNote longNote && liveNote.NoteList != null && liveNote.NoteList.Count > 0 && liveBundleBuildData != null)
			{
				NoteBase lastNote = liveNote.NoteList[liveNote.NoteList.Count - 1];
				AddComboNote(lastNote, longNote, liveBundleBuildData.LongNoteComboBeat, liveBundleBuildData, musicScoreInfos, liveNote.speedRatio);
			}
			return liveNote;
		}

		private static void CalcSkipNoteLane(LiveBundleBuildData liveBundleBuildData, NoteBase childNote)
		{
			if (childNote == null)
			{
				return;
			}
			(float laneStart, float laneEnd) = MusicScoreMakerUtility.CalcSkipNoteLane(childNote);
			childNote.LaneStart = (int)Math.Floor(laneStart);
			childNote.LaneEnd = (int)Math.Floor(laneEnd);
			childNote.LaneStartF = laneStart;
			childNote.LaneEndF = laneEnd;
			float laneOffset = LiveUtility.GetLaneOffset(NoteCategory.Combo, liveBundleBuildData);
			childNote.JudgeLaneStart = laneStart - laneOffset;
			childNote.JudgeLaneEnd = laneEnd + laneOffset;
		}

		private static void AddComboNote(NoteBase note, LongNote longNote, int longNoteComboBeat, LiveBundleBuildData liveBundleBuildData, MusicScoreInfo[] musicScoreInfoArray, float speedRatio)
		{
			if (note == null || longNote == null || longNoteComboBeat <= 0)
			{
				return;
			}
			int startIndex = (int)Math.Floor(longNote.MusicScoreInfo.barProgress * longNoteComboBeat) + 1 + longNote.MusicScoreInfo.bar * longNoteComboBeat;
			int endIndex = (int)Math.Ceiling(note.MusicScoreInfo.barProgress * longNoteComboBeat) - 1 + note.MusicScoreInfo.bar * longNoteComboBeat;
			for (int i = startIndex; i <= endIndex; i++)
			{
				MusicScoreInfo musicScoreInfo = MusicScore.GenerateNoteMusicScoreInfo(i / longNoteComboBeat, i % longNoteComboBeat / (float)longNoteComboBeat, musicScoreInfoArray);
				LongHoldCombo combo = new LongHoldCombo(musicScoreInfo, longNote.Type, speedRatio, LiveUtility.GetLaneOffset(NoteCategory.Combo, liveBundleBuildData));
				longNote.AddHoldCombo(combo);
				combo.SetSkip(true);
			}
			longNote.SortNoteList();
		}

		private static void SetPairNotes(NoteBase[] noteArray)
		{
			if (noteArray == null)
			{
				return;
			}
			bool isShowPair = true;
			try
			{
				isShowPair = LiveSettingData.LoadFromStorage()?.UseSimultaneousPushingLine ?? true;
			}
			catch
			{
				// LiveSettingData is outside MusicScoreMaker restoration; default to the original visible pair-line path.
			}
			Dictionary<float, List<NoteBase>> notesByPosition = new Dictionary<float, List<NoteBase>>();
			foreach (NoteBase rootNote in noteArray)
			{
				if (rootNote?.NoteList == null)
				{
					continue;
				}
				foreach (NoteBase note in rootNote.NoteList)
				{
					if (note == null || IsNotPairNoteTarget(note))
					{
						continue;
					}
					float position = note.MusicScoreInfo.bar + note.MusicScoreInfo.barProgress;
					if (!notesByPosition.TryGetValue(position, out List<NoteBase> notes))
					{
						notes = new List<NoteBase>();
						notesByPosition[position] = notes;
					}
					notes.Add(note);
				}
			}
			foreach (List<NoteBase> notes in notesByPosition.Values)
			{
				if (notes.Count < 2)
				{
					continue;
				}
				notes.Sort((a, b) => a.DefaultLeftLane.CompareTo(b.DefaultLeftLane));
				SetPairNote(notes[0], notes[notes.Count - 1], isShowPair);
			}
		}

		public static bool IsNotPairNoteTarget(NoteBase note)
		{
			if (note == null)
			{
				return true;
			}
			return note is FrictionHideNote
				|| note is FrictionHideLongNote
				|| note.Category == NoteCategory.Guide
				|| note is GuideHiddenConnectionNote
				|| note.Category == NoteCategory.Combo;
		}

		public static void ReapplySetPairNotes(NoteBase[] noteArray)
		{
			SetPairNotes(noteArray);
		}

		private static void SetPairNote(NoteBase first, NoteBase last, bool isShowPair)
		{
			if (!isShowPair || first == null || last == null)
			{
				return;
			}
			if (first.PairNote is NoteBase firstPair)
			{
				first.SetPairNote(null);
				firstPair.SetPairNote(null);
			}
			if (last.PairNote is NoteBase lastPair)
			{
				last.SetPairNote(null);
				lastPair.SetPairNote(null);
			}
			first.SetPairNote(last);
			last.SetPairNote(first);
		}

		public int QuantizeSelectedNotes()
		{
			if (QuantizeSettings == null || QuantizeSettings.CurrentQuantizeType == QuantizeSettings.QuantizeType.None || QuantizeSettings.QuantizeStrength <= 0f)
			{
				return 0;
			}
			if (SelectedNoteIdList == null || SelectedNoteIdList.Count == 0)
			{
				return 0;
			}
			int changedCount = 0;
			foreach (int noteId in SelectedNoteIdList)
			{
				if (QuantizeNote(FindNote(noteId)))
				{
					changedCount++;
				}
			}
			return changedCount;
		}

		private bool QuantizeNote(MusicScoreNoteBase note)
		{
			if (note == null || QuantizeSettings == null || QuantizeSettings.CurrentQuantizeType == QuantizeSettings.QuantizeType.None)
			{
				return false;
			}
			long quantizeTicks = MusicScoreMakerUtility.GetQuantizeTicks();
			if (quantizeTicks < 1L)
			{
				return false;
			}
			long beforeTicks = note.ticks;
			long quantizedTicks = (beforeTicks + (quantizeTicks >> 1)) / quantizeTicks * quantizeTicks;
			long validTicks = MusicScoreMakerUtility.ClampTicksToValidRange(quantizedTicks);
			if (beforeTicks == validTicks)
			{
				return false;
			}
			note.ticks = validTicks;
			_noteListOrderDirty = true;
			_drawingOrderDirty = true;
			return true;
		}

		public int QuantizeAllNotes()
		{
			if (QuantizeSettings == null || QuantizeSettings.CurrentQuantizeType == QuantizeSettings.QuantizeType.None || QuantizeSettings.QuantizeStrength <= 0f)
			{
				return 0;
			}
			if (NoteList == null)
			{
				return 0;
			}
			int changedCount = 0;
			foreach (MusicScoreNoteBase note in NoteList)
			{
				if (QuantizeNote(note))
				{
					changedCount++;
				}
			}
			if (changedCount > 0)
			{
				SortNoteListByTicks();
			}
			return changedCount;
		}

		public float GetTimeFromTicks(long ticks)
		{
			return MusicScoreMakerUtility.GetTimeFromTicks(ticks, MusicScoreMakerUtility.ConvertMusicScoreInfo(MusicScoreEventDataList));
		}

		private void RebuildNoteIdCache()
		{
			_noteIdCache.Clear();
			if (NoteList != null)
			{
				_noteIdCache.EnsureCapacity(NoteList.Count);
				foreach (MusicScoreNoteBase note in NoteList)
				{
					if (note != null)
					{
						_noteIdCache[note.id] = note;
					}
				}
			}
			_noteIdCacheValid = true;
		}

		private void InvalidateNoteIdCache()
		{
			_noteIdCacheValid = false;
		}

		public Dictionary<int, MusicScoreNoteBase> GetNoteIdCacheOrRebuild()
		{
			if (!_noteIdCacheValid)
			{
				RebuildNoteIdCache();
			}
			return _noteIdCache;
		}

		private void RebuildEventIdCache()
		{
			_eventIdCache.Clear();
			if (MusicScoreEventDataList != null)
			{
				_eventIdCache.EnsureCapacity(MusicScoreEventDataList.Count);
				foreach (MusicScoreEventData evt in MusicScoreEventDataList)
				{
					if (evt != null)
					{
						_eventIdCache[evt.id] = evt;
					}
				}
			}
			_eventIdCacheValid = true;
		}

		private void InvalidateEventIdCache()
		{
			_eventIdCacheValid = false;
		}

		public Dictionary<int, MusicScoreEventData> GetEventIdCacheOrRebuild()
		{
			if (!_eventIdCacheValid)
			{
				RebuildEventIdCache();
			}
			return _eventIdCache;
		}

		public MusicScoreNoteBase FindNote(int id)
		{
			GetNoteIdCacheOrRebuild().TryGetValue(id, out MusicScoreNoteBase note);
			return note;
		}

		public MusicScoreNoteBase FindRootNote(int noteId)
		{
			MusicScoreNoteBase note = FindNote(noteId);
			return note?.FindStartNote(GetNoteIdCacheOrRebuild());
		}

		public void UpdateConnectionNotes()
		{
			if (NoteList == null || NoteList.Count == 0)
			{
				return;
			}
			Dictionary<int, MusicScoreNoteBase> noteIdCache = GetNoteIdCacheOrRebuild();
			foreach (MusicScoreNoteBase note in NoteList)
			{
				note?.UpdateConnectedNotes(noteIdCache);
			}
		}

		private static int CompareNoteByTicks(MusicScoreNoteBase a, MusicScoreNoteBase b)
		{
			int result = a.ticks.CompareTo(b.ticks);
			return result != 0 ? result : a.id.CompareTo(b.id);
		}

		private void SortNoteListByTicks()
		{
			if (NoteList != null && NoteList.Count >= 2)
			{
				NoteList.Sort(NoteTicksComparer);
			}
		}

		public void AddNote(MusicScoreNoteBase note)
		{
			if (NoteList == null)
			{
				NoteList = new List<MusicScoreNoteBase>();
			}
			int index = NoteList.BinarySearch(note, NoteTicksComparer);
			if (index < 0)
			{
				index = ~index;
			}
			NoteList.Insert(index, note);
			InvalidateNoteIdCache();
			_noteListOrderDirty = true;
		}

		public void AddNoteRange(IEnumerable<MusicScoreNoteBase> notes)
		{
			if (NoteList == null)
			{
				NoteList = new List<MusicScoreNoteBase>();
			}
			NoteList.AddRange(notes);
			SortNoteListByTicks();
			InvalidateNoteIdCache();
			_noteListOrderDirty = true;
		}

		public bool RemoveNote(MusicScoreNoteBase note)
		{
			if (NoteList == null)
			{
				return false;
			}
			bool removed = NoteList.Remove(note);
			if (removed)
			{
				InvalidateNoteIdCache();
				_noteListOrderDirty = true;
			}
			return removed;
		}

		public void RemoveNoteRange(IEnumerable<MusicScoreNoteBase> notes)
		{
			if (NoteList == null)
			{
				return;
			}
			foreach (MusicScoreNoteBase note in notes)
			{
				NoteList.Remove(note);
			}
			InvalidateNoteIdCache();
			_noteListOrderDirty = true;
		}

		public void ClearNoteList()
		{
			if (NoteList != null)
			{
				NoteList.Clear();
			}
			InvalidateNoteIdCache();
			_noteListOrderDirty = true;
		}

		public void EnsureTicksOrder()
		{
			SortNoteListByTicks();
			SortEventListByTicks();
			_noteListOrderDirty = false;
		}

		public void MarkNoteListOrderDirty()
		{
			_noteListOrderDirty = true;
		}

		public void EnsureNoteListTicksOrderIfNeeded()
		{
			if (_noteListOrderDirty)
			{
				SortNoteListByTicks();
				_noteListOrderDirty = false;
			}
		}

		private void SortEventListByTicks()
		{
			if (MusicScoreEventDataList != null && MusicScoreEventDataList.Count >= 2)
			{
				MusicScoreEventDataList.Sort(CompareEventTicks);
			}
		}

		public void AddEvent(MusicScoreEventData evt)
		{
			if (MusicScoreEventDataList == null)
			{
				MusicScoreEventDataList = new List<MusicScoreEventData>();
			}
			int index = MusicScoreEventDataList.BinarySearch(evt, Comparer<MusicScoreEventData>.Create(CompareEventTicks));
			if (index < 0)
			{
				index = ~index;
			}
			MusicScoreEventDataList.Insert(index, evt);
			InvalidateEventIdCache();
		}

		public void AddEventRange(IEnumerable<MusicScoreEventData> events)
		{
			if (MusicScoreEventDataList == null)
			{
				MusicScoreEventDataList = new List<MusicScoreEventData>();
			}
			MusicScoreEventDataList.AddRange(events);
			SortEventListByTicks();
			InvalidateEventIdCache();
		}

		public bool RemoveEvent(MusicScoreEventData evt)
		{
			if (MusicScoreEventDataList == null)
			{
				return false;
			}
			bool removed = MusicScoreEventDataList.Remove(evt);
			if (removed)
			{
				InvalidateEventIdCache();
			}
			return removed;
		}

		public void RemoveEventRange(IEnumerable<MusicScoreEventData> events)
		{
			if (MusicScoreEventDataList == null)
			{
				return;
			}
			foreach (MusicScoreEventData evt in events)
			{
				MusicScoreEventDataList.Remove(evt);
			}
			InvalidateEventIdCache();
		}

		public void RemoveEventsWhere(Predicate<MusicScoreEventData> match)
		{
			if (MusicScoreEventDataList != null)
			{
				MusicScoreEventDataList.RemoveAll(match);
				InvalidateEventIdCache();
			}
		}

		public void ClearEventList()
		{
			if (MusicScoreEventDataList != null)
			{
				MusicScoreEventDataList.Clear();
			}
			InvalidateEventIdCache();
		}

		public void RemoveOverlappingTraceGuideSingleNotes()
		{
			if (NoteList == null || NoteList.Count == 0)
			{
				return;
			}
			HashSet<MusicScoreNoteBase> removeSet = new HashSet<MusicScoreNoteBase>();
			foreach (MusicScoreNoteBase note in NoteList)
			{
				if (note == null || removeSet.Contains(note))
				{
					continue;
				}
				if (!IsTraceGuideSingleCleanupCategory(note.category) || note.previousConnectionId != -1 || note.nextConnectionId != -1)
				{
					continue;
				}
				foreach (MusicScoreNoteBase other in NoteList)
				{
					if (other == null || ReferenceEquals(note, other) || removeSet.Contains(other))
					{
						continue;
					}
					if (other.ticks == note.ticks
						&& (other.previousConnectionId != -1 || other.nextConnectionId != -1)
						&& IsLaneOverlappingForTraceGuide(note, other))
					{
						removeSet.Add(note);
						break;
					}
				}
			}
			if (removeSet.Count > 0)
			{
				RemoveNoteRange(removeSet);
			}
		}

		private static bool IsLaneOverlappingForTraceGuide(MusicScoreNoteBase a, MusicScoreNoteBase b)
		{
			if (a == null || b == null)
			{
				return false;
			}
			int aStart = Math.Max(0, Math.Min(MusicScoreMakerModel.LaneCountMinus1, a.laneStart));
			int aEnd = Math.Max(0, Math.Min(MusicScoreMakerModel.LaneCountMinus1, a.laneEnd));
			if (aEnd < aStart)
			{
				(aStart, aEnd) = (aEnd, aStart);
			}
			int bStart = Math.Max(0, Math.Min(MusicScoreMakerModel.LaneCountMinus1, b.laneStart));
			int bEnd = Math.Max(0, Math.Min(MusicScoreMakerModel.LaneCountMinus1, b.laneEnd));
			if (bEnd < bStart)
			{
				(bStart, bEnd) = (bEnd, bStart);
			}
			return aStart <= bEnd && aEnd >= bStart;
		}

		private static bool IsTraceGuideSingleCleanupCategory(NoteCategory category)
		{
			return category == NoteCategory.FrictionHide
				|| category == NoteCategory.Guide
				|| category == NoteCategory.GuideHidden
				|| category == NoteCategory.Hidden;
		}

		public void ClearNotes()
		{
			ClearNoteList();
			ClearSelectedNotes();
			ClearSelectedTemporaryNotes();
		}

		public void AddSelectedNote(int noteId)
		{
			SelectedNoteIdList.Add(noteId);
			InvalidateSelectedNoteTargetIdSetCache();
		}

		public void AddSelectedNoteRange(IEnumerable<int> noteIds)
		{
			SelectedNoteIdList.AddRange(noteIds);
			InvalidateSelectedNoteTargetIdSetCache();
		}

		public bool RemoveSelectedNote(int noteId)
		{
			bool removed = SelectedNoteIdList.Remove(noteId);
			if (removed)
			{
				InvalidateSelectedNoteTargetIdSetCache();
			}
			return removed;
		}

		public void RemoveSelectedNoteRange(IEnumerable<int> noteIds)
		{
			foreach (int noteId in noteIds)
			{
				SelectedNoteIdList.Remove(noteId);
			}
			InvalidateSelectedNoteTargetIdSetCache();
		}

		public void ClearSelectedNotes()
		{
			SelectedNoteIdList.Clear();
			InvalidateSelectedNoteTargetIdSetCache();
		}

		public void AddSelectedTemporaryNote(int noteId)
		{
			SelectedTemporaryNoteIdList.Add(noteId);
			InvalidateSelectedNoteTargetIdSetCache();
		}

		public void AddSelectedTemporaryNoteRange(IEnumerable<int> noteIds)
		{
			SelectedTemporaryNoteIdList.AddRange(noteIds);
			InvalidateSelectedNoteTargetIdSetCache();
		}

		public bool RemoveSelectedTemporaryNote(int noteId)
		{
			bool removed = SelectedTemporaryNoteIdList.Remove(noteId);
			if (removed)
			{
				InvalidateSelectedNoteTargetIdSetCache();
			}
			return removed;
		}

		public void RemoveSelectedTemporaryNoteRange(IEnumerable<int> noteIds)
		{
			foreach (int noteId in noteIds)
			{
				SelectedTemporaryNoteIdList.Remove(noteId);
			}
			InvalidateSelectedNoteTargetIdSetCache();
		}

		public void ClearSelectedTemporaryNotes()
		{
			SelectedTemporaryNoteIdList.Clear();
			InvalidateSelectedNoteTargetIdSetCache();
		}

		public void AddSelectedEvent(int eventId)
		{
			SelectedEventIdList.Add(eventId);
			InvalidateSelectedEventTargetIdSetCache();
		}

		public void AddSelectedEventRange(IEnumerable<int> eventIds)
		{
			SelectedEventIdList.AddRange(eventIds);
			InvalidateSelectedEventTargetIdSetCache();
		}

		public bool RemoveSelectedEvent(int eventId)
		{
			bool removed = SelectedEventIdList.Remove(eventId);
			if (removed)
			{
				InvalidateSelectedEventTargetIdSetCache();
			}
			return removed;
		}

		public void RemoveSelectedEventRange(IEnumerable<int> eventIds)
		{
			foreach (int eventId in eventIds)
			{
				SelectedEventIdList.Remove(eventId);
			}
			InvalidateSelectedEventTargetIdSetCache();
		}

		public void ClearSelectedEvents()
		{
			SelectedEventIdList.Clear();
			InvalidateSelectedEventTargetIdSetCache();
		}

		public void AddSelectedTemporaryEvent(int eventId)
		{
			SelectedTemporaryEventIdList.Add(eventId);
			InvalidateSelectedEventTargetIdSetCache();
		}

		public void AddSelectedTemporaryEventRange(IEnumerable<int> eventIds)
		{
			SelectedTemporaryEventIdList.AddRange(eventIds);
			InvalidateSelectedEventTargetIdSetCache();
		}

		public bool RemoveSelectedTemporaryEvent(int eventId)
		{
			bool removed = SelectedTemporaryEventIdList.Remove(eventId);
			if (removed)
			{
				InvalidateSelectedEventTargetIdSetCache();
			}
			return removed;
		}

		public void RemoveSelectedTemporaryEventRange(IEnumerable<int> eventIds)
		{
			foreach (int eventId in eventIds)
			{
				SelectedTemporaryEventIdList.Remove(eventId);
			}
			InvalidateSelectedEventTargetIdSetCache();
		}

		public void ClearSelectedTemporaryEvents()
		{
			SelectedTemporaryEventIdList.Clear();
			InvalidateSelectedEventTargetIdSetCache();
		}

		public void MigrateToCurrentVersion()
		{
			int oldVersion = VersionCode;
			if (oldVersion != 0)
			{
				VersionCode = CURRENT_VERSION;
				if (oldVersion < CURRENT_VERSION)
				{
					SortNoteListByTicks();
					SortEventListByTicks();
				}
			}
			else
			{
				VersionCode = CURRENT_VERSION;
			}
		}

		private static float GetJudgmentNoteGapThresholdSec()
		{
			return 0.1f;
		}

		private static float GetNoteDensityWindowSec()
		{
			return 1f;
		}

		private static int GetNoteDensityThresholdCount()
		{
			return 12;
		}

		private MusicScoreMakerData CloneForMusicScoreConversion()
		{
			MusicScoreMakerData clone = new MusicScoreMakerData
			{
				VersionCode = VersionCode,
				MusicScoreTicksMax = MusicScoreTicksMax,
				MusicId = MusicId,
				FullComboDataHash = FullComboDataHash,
				EventArray = EventArray ?? Array.Empty<EventBase>(),
				QuantizeSettings = QuantizeSettings
			};
			clone.NoteList = new List<MusicScoreNoteBase>(NoteList?.Count ?? 0);
			if (NoteList != null)
			{
				foreach (MusicScoreNoteBase note in NoteList)
				{
					clone.NoteList.Add(note?.Clone());
				}
			}
			clone.MusicScoreEventDataList = new List<MusicScoreEventData>(MusicScoreEventDataList?.Count ?? 0);
			if (MusicScoreEventDataList != null)
			{
				foreach (MusicScoreEventData eventData in MusicScoreEventDataList)
				{
					if (eventData == null)
					{
						clone.MusicScoreEventDataList.Add(null);
						continue;
					}
					clone.MusicScoreEventDataList.Add(new MusicScoreEventData
					{
						id = eventData.id,
						eventType = eventData.eventType,
						ticks = eventData.ticks,
						changeValue = eventData.changeValue
					});
				}
			}
			clone.InitializeIdCount();
			clone.InvalidateNoteIdCache();
			clone.InvalidateEventIdCache();
			return clone;
		}

		private void InitializeRuntimeState()
		{
			_noteIdCache = new Dictionary<int, MusicScoreNoteBase>();
			_noteIdCacheValid = false;
			_noteListOrderDirty = true;
			_eventIdCache = new Dictionary<int, MusicScoreEventData>();
			_eventIdCacheValid = false;
			_selectedNoteTargetIdSetCache = new HashSet<int>();
			_selectedNoteTargetIdSetCacheValid = false;
			_selectedEventTargetIdSetCache = new HashSet<int>();
			_selectedEventTargetIdSetCacheValid = false;
			SelectedTargetOperation = new SelectedTargetOperation();
			for (int i = 0; i < _recalcWorkLaneNoteIds.Length; i++)
			{
				_recalcWorkLaneNoteIds[i] = new List<int>();
			}
		}

		private static MusicScoreEventData CreateEvent(int id, MusicScoreEventType type, long ticks, object changeValue)
		{
			return new MusicScoreEventData
			{
				id = id,
				eventType = type,
				ticks = ticks,
				changeValue = changeValue
			};
		}

		static MusicScoreMakerData()
		{
		}
	}
}
