using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using JetBrains.Annotations;
using Sekai.ApiData;
using Sekai.Live;
using Sekai.MusicScoreMaker.Common;
using Sekai.MusicScoreMaker.Ingame.Utilities;

namespace Sekai.MusicScoreMaker.Ingame.Models
{
	public class MusicScoreMakerModel
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CCalculateMinNoteChangeRateAsync_003Ed__182 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder<float?> _003C_003Et__builder;

			public MusicScoreMakerModel _003C_003E4__this;

			public CancellationToken ct;

			private UniTask<float>.Awaiter _003C_003Eu__1;

			private void MoveNext()
			{
				try
				{
					ct.ThrowIfCancellationRequested();
					string difficultyForComparison = _003C_003E4__this?.ResolveDifficultyForOfficialComparison();
					List<MusicScoreNoteBase> baseNotes = string.IsNullOrEmpty(difficultyForComparison) ? null : _003C_003E4__this.CreateOfficialNoteListForComparison(difficultyForComparison);
					_003C_003Et__builder.SetResult(baseNotes == null ? (float?)null : _003C_003E4__this.CalculateMinNoteChangeRateFromBase(baseNotes));
				}
				catch (Exception ex)
				{
					_003C_003Et__builder.SetException(ex);
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

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CCalculateMinNoteChangeRateFromBaseAsync_003Ed__183 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder<float?> _003C_003Et__builder;

			public List<MusicScoreNoteBase> baseNoteList;

			public MusicScoreMakerModel _003C_003E4__this;

			public CancellationToken ct;

			private UniTask<float>.Awaiter _003C_003Eu__1;

			private void MoveNext()
			{
				try
				{
					ct.ThrowIfCancellationRequested();
					_003C_003Et__builder.SetResult(baseNoteList == null || _003C_003E4__this == null ? (float?)null : _003C_003E4__this.CalculateMinNoteChangeRateFromBase(baseNoteList));
				}
				catch (Exception ex)
				{
					_003C_003Et__builder.SetException(ex);
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

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CIsNotesInsufficientlyChangedAsync_003Ed__172 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder<bool> _003C_003Et__builder;

			public MusicScoreMakerModel _003C_003E4__this;

			public CancellationToken ct;

			private UniTask<bool>.Awaiter _003C_003Eu__1;

			private void MoveNext()
			{
				try
				{
					ct.ThrowIfCancellationRequested();
					string difficultyForComparison = _003C_003E4__this?.ResolveDifficultyForOfficialComparison();
					List<MusicScoreNoteBase> baseNotes = string.IsNullOrEmpty(difficultyForComparison) ? null : _003C_003E4__this.CreateOfficialNoteListForComparison(difficultyForComparison);
					_003C_003Et__builder.SetResult(baseNotes != null && _003C_003E4__this.IsNotesInsufficientlyChangedFromBase(baseNotes));
				}
				catch (Exception ex)
				{
					_003C_003Et__builder.SetException(ex);
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

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CIsNotesInsufficientlyChangedFromBaseAsync_003Ed__181 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder<bool> _003C_003Et__builder;

			public List<MusicScoreNoteBase> baseNoteList;

			public MusicScoreMakerModel _003C_003E4__this;

			public CancellationToken ct;

			private UniTask<bool>.Awaiter _003C_003Eu__1;

			private void MoveNext()
			{
				try
				{
					ct.ThrowIfCancellationRequested();
					_003C_003Et__builder.SetResult(baseNoteList != null && _003C_003E4__this != null && _003C_003E4__this.IsNotesInsufficientlyChangedFromBase(baseNoteList));
				}
				catch (Exception ex)
				{
					_003C_003Et__builder.SetException(ex);
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

		private const long SCORE_TICK_MARGIN = 480L;

		private const int DEFAULT_QUANTIZE_DIVISION = 16;

		private const float NOTE_CHANGE_RATE_THRESHOLD_FALLBACK = 0.05f;

		private const int NOTE_TICKS_TOLERANCE_FALLBACK = 60;

		public static readonly int LaneCount;

		public static readonly int LaneCountMinus1;

		public MusicScoreMakerUtility.ToolType SelectedToolType;

		public NoteCategory SelectedNoteCategory;

		public NoteType SelectedNoteType;

		public NoteDirection SelectedNoteDirection;

		public NoteLineType SelectedNoteLineType;

		public bool SelectedIsSkip;

		public int LastNoteWidth;

		public const int DEFAULT_LAST_NOTE_WIDTH = 2;

		public bool IsNoteDataSelected;

		public float SelectedMusicScoreBarIndex;

		public float CurrentMusicScoreScale;

		public float PreviousMusicScoreScale;

		[CanBeNull]
		private string _fullComboDataHash;

		private bool _hasConfirmedEditAfterFullCombo;

		public const long InputStartTicksDefault = long.MinValue;

		public MusicScoreMakerData MusicScoreMakerData;

		private string _lastSavedDataHash;

		public LiveBundleBuildData LiveBundleBuildData;

		public long QuantizeTicks
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public int QuantizeDivision
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			private set;
		}

		public QuantizeSettings.QuantizeType QuantizeType
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public float QuantizeStrength
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public long FocusTicks
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public bool IsEventSettingMode
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public MusicScoreEventType? CurrentEventSettingType
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public MusicScoreEventType? SelectedEventSettingModeType
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public bool IsFullComboCleared
		{
			get
			{
				if (string.IsNullOrEmpty(_fullComboDataHash))
				{
					return false;
				}
				string serialized = SerializeNotesAndEventsForFullComboHash();
				return !string.IsNullOrEmpty(serialized) && string.Equals(ComputeHash(serialized), _fullComboDataHash, StringComparison.Ordinal);
			}
		}

		public bool HasConfirmedEditAfterFullCombo
		{
			get
			{
				return _hasConfirmedEditAfterFullCombo;
			}
		}

		public long InputStartTicks
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public string LastSelectFile
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public int MusicId
		{
			get
			{
				return MusicScoreMakerData?.MusicId ?? 0;
			}
			set
			{
				if (MusicScoreMakerData != null)
				{
					MusicScoreMakerData.MusicId = value;
				}
			}
		}

		public string Difficulty
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public int VocalId
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		[CanBeNull]
		public string BaseMusicScoreId
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public int BaseMusicDifficultyId
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public int LastSavedDraftSlotNo
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		[CanBeNull]
		public UserCustomMusicScoreDraft LastSavedDraft
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public float CurrentMusicTime
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public long MusicLength
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public int MasterMusicSec
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public int ComboCountMinimum
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			private set;
		}

		public float TimingAdjust
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public float FillerSec
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public string AssetbundleName
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		[CanBeNull]
		public CustomMusicScorePackage CustomMusicScorePackage
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public long MusicScoreTicksMax
		{
			get
			{
				long audioTicks = ComputeMaxTicksBasedOnAudioDuration();
				long dataTicks = MusicScoreMakerData != null ? MusicScoreMakerData.GetMaxTicks() + SCORE_TICK_MARGIN : SCORE_TICK_MARGIN;
				return Math.Max(0L, Math.Max(audioTicks, dataTicks));
			}
		}

		public LiveSettingData LiveSettings
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public bool IsMusicPlaying
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public bool AreaSelectMode
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public bool RemoveMode
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public bool CriticalFilterEnabled
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public bool IsEditRestricted
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public bool IsBasedOnOfficialScore
		{
			get
			{
				return !string.IsNullOrEmpty(ResolveDifficultyForOfficialComparison());
			}
		}

		public bool IsBasedOnCustomScore
		{
			get
			{
				return !string.IsNullOrEmpty(BaseMusicScoreId);
			}
		}

		public bool HasBaseScore
		{
			get
			{
				return IsBasedOnCustomScore || IsBasedOnOfficialScore;
			}
		}

		public void ConfirmEditAfterFullCombo()
		{
			_hasConfirmedEditAfterFullCombo = true;
		}

		public void UpdateFullComboDataHash(bool enableEditConfirmation = true)
		{
			string serialized = SerializeNotesAndEventsForFullComboHash();
			if (string.IsNullOrEmpty(serialized))
			{
				return;
			}
			_fullComboDataHash = ComputeHash(serialized);
			SyncFullComboDataHashToData();
			_hasConfirmedEditAfterFullCombo = !enableEditConfirmation;
		}

		public string GetFullComboDataHash()
		{
			return _fullComboDataHash;
		}

		public void SetFullComboDataHash(string hash, bool enableEditConfirmation)
		{
			_fullComboDataHash = hash;
			SyncFullComboDataHashToData();
			_hasConfirmedEditAfterFullCombo = !enableEditConfirmation;
		}

		[CanBeNull]
		public string ComputeCurrentDataHash()
		{
			string serialized = SerializeDataExcludingMetadata();
			return string.IsNullOrEmpty(serialized) ? null : ComputeHash(serialized);
		}

		[CanBeNull]
		public string ComputeFullComboHash()
		{
			string serialized = SerializeNotesAndEventsForFullComboHash();
			return string.IsNullOrEmpty(serialized) ? null : ComputeHash(serialized);
		}

		[CanBeNull]
		private string SerializeDataExcludingMetadata()
		{
			if (MusicScoreMakerData == null)
			{
				return null;
			}
			string savedFullComboHash = MusicScoreMakerData.FullComboDataHash;
			try
			{
				MusicScoreMakerData.FullComboDataHash = null;
				return DeepCopyHelper.ToJsonString(MusicScoreMakerData);
			}
			finally
			{
				MusicScoreMakerData.FullComboDataHash = savedFullComboHash;
			}
		}

		[CanBeNull]
		private string SerializeNotesAndEventsForFullComboHash()
		{
			if (MusicScoreMakerData?.NoteList == null || MusicScoreMakerData.MusicScoreEventDataList == null)
			{
				return null;
			}
			List<MusicScoreNoteBase> notes = new List<MusicScoreNoteBase>(MusicScoreMakerData.NoteList.Count);
			foreach (MusicScoreNoteBase note in MusicScoreMakerData.NoteList)
			{
				if (note != null && !MusicScoreMakerUtility.IsGuideCategory(note.category))
				{
					notes.Add(note);
				}
			}
			return DeepCopyHelper.ToJsonString(new
			{
				Notes = notes,
				Events = MusicScoreMakerData.MusicScoreEventDataList
			});
		}

		private void SyncFullComboDataHashToData()
		{
			if (MusicScoreMakerData != null)
			{
				MusicScoreMakerData.FullComboDataHash = _fullComboDataHash;
			}
		}

		public void EnableEventSettingMode(MusicScoreEventType eventType)
		{
			IsEventSettingMode = true;
			CurrentEventSettingType = eventType;
		}

		public void DisableEventSettingMode()
		{
			IsEventSettingMode = false;
			CurrentEventSettingType = null;
		}

		public MusicScoreMakerModel([CanBeNull] string baseMusicScoreId, int baseMusicDifficultyId = -1)
		{
			QuantizeTicks = 120L;
			QuantizeDivision = DEFAULT_QUANTIZE_DIVISION;
			QuantizeStrength = 1f;
			LastNoteWidth = DEFAULT_LAST_NOTE_WIDTH;
			CurrentMusicScoreScale = 1f;
			PreviousMusicScoreScale = 1f;
			InputStartTicks = InputStartTicksDefault;
			LastSavedDraftSlotNo = -1;
			BaseMusicScoreId = baseMusicScoreId;
			BaseMusicDifficultyId = baseMusicDifficultyId;
		}

		private long ComputeMaxTicksBasedOnAudioDuration()
		{
			if (MusicScoreMakerData?.MusicScoreEventDataList == null || MasterMusicSec < 1)
			{
				return 0L;
			}
			return MusicScoreMakerUtility.GetTicksFromTime(MasterMusicSec, MusicScoreMakerData.MusicScoreEventDataList);
		}

		public void SetQuantizeDivision(int division)
		{
			if (division <= 0)
			{
				division = DEFAULT_QUANTIZE_DIVISION;
			}
			QuantizeDivision = division;
			QuantizeTicks = MusicScoreMakerUtility.CalculateQuantizeTicks(division);
		}

		public void UpdateComboCountMinimum(int durationSec)
		{
			int estimatedMinimum = CalculateComboCountMinimum(durationSec);
			int officialEasyCount = GetOfficialEasyTotalNoteCount();
			ComboCountMinimum = officialEasyCount > 0 && officialEasyCount < estimatedMinimum ? officialEasyCount : estimatedMinimum;
		}

		private static int CalculateComboCountMinimum(int durationSec)
		{
			return Sekai.MusicScoreMaker.Common.MusicScoreDifficultyEstimator.CalculateComboCountMinimum(durationSec);
		}

		private int GetOfficialEasyTotalNoteCount()
		{
			// TODO(original): restore MasterDataManager lookup for official EASY total note count.
			return 0;
		}

		public void Dispose()
		{
		}

		public MusicScore ToMusicScore(long? filterBeforeTicks = null)
		{
			LiveSettingData settings = LiveSettingData.LoadFromStorage();
			bool isMirror = settings != null && settings.IsMirror;
			return MusicScoreMakerData?.ToMusicScore(LiveBundleBuildData, filterBeforeTicks, isMirror);
		}

		public void SetMusicScoreMakerDataAndUpdateHash(MusicScoreMakerData data)
		{
			MusicScoreMakerData = data;
			UpdateSavedDataHash();
		}

		public string GetSavedDataHash()
		{
			return _lastSavedDataHash;
		}

		public void SetSavedDataHash(string hash)
		{
			_lastSavedDataHash = hash;
		}

		public void UpdateSavedDataHash()
		{
			string serialized = SerializeDataExcludingMetadata();
			if (!string.IsNullOrEmpty(serialized))
			{
				_lastSavedDataHash = ComputeHash(serialized);
			}
		}

		public bool HasUnsavedChanges()
		{
			string serialized = SerializeDataExcludingMetadata();
			return !string.IsNullOrEmpty(serialized) && !string.Equals(ComputeHash(serialized), _lastSavedDataHash, StringComparison.Ordinal);
		}

		[AsyncStateMachine(typeof(_003CIsNotesInsufficientlyChangedAsync_003Ed__172))]
		public UniTask<bool> IsNotesInsufficientlyChangedAsync(CancellationToken ct = default(CancellationToken))
		{
			ct.ThrowIfCancellationRequested();
			string difficultyForComparison = ResolveDifficultyForOfficialComparison();
			List<MusicScoreNoteBase> baseNotes = string.IsNullOrEmpty(difficultyForComparison) ? null : CreateOfficialNoteListForComparison(difficultyForComparison);
			return UniTask.FromResult(baseNotes != null && IsNotesInsufficientlyChangedFromBase(baseNotes));
		}

		private string ResolveDifficultyForOfficialComparison()
		{
			const string Append = "append";
			if (!string.Equals(Difficulty, Append, StringComparison.OrdinalIgnoreCase))
			{
				return Difficulty;
			}
			// TODO(original): restore MasterDataManager lookup from BaseMusicDifficultyId for append scores.
			return null;
		}

		private List<MusicScoreNoteBase> CreateOfficialNoteListForComparison(string difficultyForComparison)
		{
			// TODO(original): restore Live.MusicScoreFactory official score load. Returning null keeps
			// the publish validation path conservative until official score assets are wired.
			return null;
		}

		[AsyncStateMachine(typeof(_003CIsNotesInsufficientlyChangedFromBaseAsync_003Ed__181))]
		public UniTask<bool> IsNotesInsufficientlyChangedFromBaseAsync(List<MusicScoreNoteBase> baseNoteList, CancellationToken ct = default(CancellationToken))
		{
			ct.ThrowIfCancellationRequested();
			return UniTask.FromResult(IsNotesInsufficientlyChangedFromBase(baseNoteList));
		}

		[AsyncStateMachine(typeof(_003CCalculateMinNoteChangeRateAsync_003Ed__182))]
		public UniTask<float?> CalculateMinNoteChangeRateAsync(CancellationToken ct = default(CancellationToken))
		{
			ct.ThrowIfCancellationRequested();
			string difficultyForComparison = ResolveDifficultyForOfficialComparison();
			List<MusicScoreNoteBase> baseNotes = string.IsNullOrEmpty(difficultyForComparison) ? null : CreateOfficialNoteListForComparison(difficultyForComparison);
			return UniTask.FromResult(baseNotes == null ? (float?)null : CalculateMinNoteChangeRateFromBase(baseNotes));
		}

		[AsyncStateMachine(typeof(_003CCalculateMinNoteChangeRateFromBaseAsync_003Ed__183))]
		public UniTask<float?> CalculateMinNoteChangeRateFromBaseAsync(List<MusicScoreNoteBase> baseNoteList, CancellationToken ct = default(CancellationToken))
		{
			ct.ThrowIfCancellationRequested();
			return UniTask.FromResult(baseNoteList == null ? (float?)null : CalculateMinNoteChangeRateFromBase(baseNoteList));
		}

		private bool IsNotesInsufficientlyChangedFromBase(List<MusicScoreNoteBase> baseNoteList)
		{
			if (MusicScoreMakerData?.NoteList == null || baseNoteList == null)
			{
				return false;
			}
			Dictionary<NoteShapeFingerprint, List<long>> currentShapeMap = CreateNoteShapeMap(MusicScoreMakerData.NoteList);
			Dictionary<NoteShapeFingerprint, List<long>> baseShapeMap = CreateNoteShapeMap(baseNoteList);
			int currentNoteCount = CountNotesInShapeMap(currentShapeMap);
			int baseNoteCount = CountNotesInShapeMap(baseShapeMap);
			if (currentNoteCount == 0 || baseNoteCount == 0)
			{
				return false;
			}
			return IsAnyNormalizedChangeRateBelowThreshold(currentShapeMap, CreateMirroredNoteShapeMap(currentShapeMap), currentNoteCount, baseShapeMap, baseNoteCount, GetNoteTicksTolerance(), GetNoteChangeRateThreshold());
		}

		private float CalculateMinNoteChangeRateFromBase(List<MusicScoreNoteBase> baseNoteList)
		{
			if (MusicScoreMakerData?.NoteList == null || baseNoteList == null)
			{
				return 1f;
			}
			Dictionary<NoteShapeFingerprint, List<long>> currentShapeMap = CreateNoteShapeMap(MusicScoreMakerData.NoteList);
			Dictionary<NoteShapeFingerprint, List<long>> baseShapeMap = CreateNoteShapeMap(baseNoteList);
			int currentNoteCount = CountNotesInShapeMap(currentShapeMap);
			int baseNoteCount = CountNotesInShapeMap(baseShapeMap);
			if (currentNoteCount == 0 || baseNoteCount == 0)
			{
				return 1f;
			}
			return CalculateMinNormalizedChangeRate(currentShapeMap, CreateMirroredNoteShapeMap(currentShapeMap), currentNoteCount, baseShapeMap, baseNoteCount, GetNoteTicksTolerance());
		}

		public static float GetNoteChangeRateThresholdValue()
		{
			return GetNoteChangeRateThreshold();
		}

		private static bool IsAnyNormalizedChangeRateBelowThreshold(Dictionary<NoteShapeFingerprint, List<long>> currentShapeMap, Dictionary<NoteShapeFingerprint, List<long>> currentMirroredShapeMap, int currentNoteCount, Dictionary<NoteShapeFingerprint, List<long>> baseShapeMap, int baseNoteCount, int ticksTolerance, float threshold)
		{
			float direct = CalculateNoteChangeRateFromShapeMaps(currentShapeMap, currentNoteCount, baseShapeMap, baseNoteCount, ticksTolerance, 0L);
			if (MusicScoreMakerUtility.IsChangeRateBelowThresholdByDisplay(direct, threshold))
			{
				return true;
			}
			long directOffset = EstimateTicksOffset(currentShapeMap, baseShapeMap);
			if (directOffset != 0L && MusicScoreMakerUtility.IsChangeRateBelowThresholdByDisplay(CalculateNoteChangeRateFromShapeMaps(currentShapeMap, currentNoteCount, baseShapeMap, baseNoteCount, ticksTolerance, directOffset), threshold))
			{
				return true;
			}
			float mirrored = CalculateNoteChangeRateFromShapeMaps(currentMirroredShapeMap, currentNoteCount, baseShapeMap, baseNoteCount, ticksTolerance, 0L);
			if (MusicScoreMakerUtility.IsChangeRateBelowThresholdByDisplay(mirrored, threshold))
			{
				return true;
			}
			long mirroredOffset = EstimateTicksOffset(currentMirroredShapeMap, baseShapeMap);
			return mirroredOffset != 0L && MusicScoreMakerUtility.IsChangeRateBelowThresholdByDisplay(CalculateNoteChangeRateFromShapeMaps(currentMirroredShapeMap, currentNoteCount, baseShapeMap, baseNoteCount, ticksTolerance, mirroredOffset), threshold);
		}

		private static float CalculateMinNormalizedChangeRate(Dictionary<NoteShapeFingerprint, List<long>> currentShapeMap, Dictionary<NoteShapeFingerprint, List<long>> currentMirroredShapeMap, int currentNoteCount, Dictionary<NoteShapeFingerprint, List<long>> baseShapeMap, int baseNoteCount, int ticksTolerance)
		{
			float min = CalculateNoteChangeRateFromShapeMaps(currentShapeMap, currentNoteCount, baseShapeMap, baseNoteCount, ticksTolerance, 0L);
			long directOffset = EstimateTicksOffset(currentShapeMap, baseShapeMap);
			if (directOffset != 0L)
			{
				min = Math.Min(min, CalculateNoteChangeRateFromShapeMaps(currentShapeMap, currentNoteCount, baseShapeMap, baseNoteCount, ticksTolerance, directOffset));
			}
			min = Math.Min(min, CalculateNoteChangeRateFromShapeMaps(currentMirroredShapeMap, currentNoteCount, baseShapeMap, baseNoteCount, ticksTolerance, 0L));
			long mirroredOffset = EstimateTicksOffset(currentMirroredShapeMap, baseShapeMap);
			if (mirroredOffset != 0L)
			{
				min = Math.Min(min, CalculateNoteChangeRateFromShapeMaps(currentMirroredShapeMap, currentNoteCount, baseShapeMap, baseNoteCount, ticksTolerance, mirroredOffset));
			}
			return min;
		}

		private static float CalculateNoteChangeRateFromShapeMaps(Dictionary<NoteShapeFingerprint, List<long>> currentShapeMap, int currentNoteCount, Dictionary<NoteShapeFingerprint, List<long>> baseShapeMap, int baseNoteCount, int ticksTolerance, long ticksOffset)
		{
			if (baseNoteCount == 0)
			{
				return 0f;
			}
			int matched = MatchShapeMaps(currentShapeMap, baseShapeMap, ticksTolerance, ticksOffset);
			Dictionary<NoteShapeFingerprint, List<long>> currentPositionMap = ConvertToPositionOnlyShapeMap(currentShapeMap);
			Dictionary<NoteShapeFingerprint, List<long>> basePositionMap = ConvertToPositionOnlyShapeMap(baseShapeMap);
			int positionMatched = MatchShapeMaps(currentPositionMap, basePositionMap, ticksTolerance, ticksOffset);
			int samePositionDifferentShapeCount = Math.Max(0, positionMatched - matched);
			return (Math.Max(baseNoteCount - positionMatched, currentNoteCount - positionMatched) + samePositionDifferentShapeCount) / (float)baseNoteCount;
		}

		private static int MatchShapeMaps(Dictionary<NoteShapeFingerprint, List<long>> currentShapeMap, Dictionary<NoteShapeFingerprint, List<long>> baseShapeMap, int ticksTolerance, long ticksOffset)
		{
			if (currentShapeMap == null || baseShapeMap == null)
			{
				return 0;
			}
			int matched = 0;
			foreach (KeyValuePair<NoteShapeFingerprint, List<long>> currentPair in currentShapeMap)
			{
				if (!baseShapeMap.TryGetValue(currentPair.Key, out List<long> baseTicks) || currentPair.Value == null || baseTicks == null)
				{
					continue;
				}
				List<long> currentTicks = currentPair.Value;
				int currentIndex = 0;
				int baseIndex = 0;
				while (currentIndex < currentTicks.Count && baseIndex < baseTicks.Count)
				{
					long adjustedCurrentTicks = currentTicks[currentIndex] - ticksOffset;
					long baseTick = baseTicks[baseIndex];
					long diff = Math.Abs(adjustedCurrentTicks - baseTick);
					if (diff <= ticksTolerance)
					{
						currentIndex++;
						baseIndex++;
						matched++;
					}
					else if (adjustedCurrentTicks < baseTick)
					{
						currentIndex++;
					}
					else
					{
						baseIndex++;
					}
				}
			}
			return matched;
		}

		private static Dictionary<NoteShapeFingerprint, List<long>> ConvertToPositionOnlyShapeMap(Dictionary<NoteShapeFingerprint, List<long>> shapeMap)
		{
			Dictionary<NoteShapeFingerprint, List<long>> result = new Dictionary<NoteShapeFingerprint, List<long>>(shapeMap?.Count ?? 0);
			if (shapeMap == null)
			{
				return result;
			}
			foreach (KeyValuePair<NoteShapeFingerprint, List<long>> pair in shapeMap)
			{
				NoteShapeFingerprint key = pair.Key.ToPositionOnly();
				if (!result.TryGetValue(key, out List<long> ticks))
				{
					ticks = new List<long>(pair.Value ?? Enumerable.Empty<long>());
					result[key] = ticks;
				}
				else if (pair.Value != null)
				{
					ticks.AddRange(pair.Value);
					ticks.Sort();
				}
			}
			return result;
		}

		private static long EstimateTicksOffset(Dictionary<NoteShapeFingerprint, List<long>> currentShapeMap, Dictionary<NoteShapeFingerprint, List<long>> baseShapeMap)
		{
			if (currentShapeMap == null || baseShapeMap == null)
			{
				return 0L;
			}
			List<long> offsets = new List<long>();
			foreach (KeyValuePair<NoteShapeFingerprint, List<long>> pair in currentShapeMap)
			{
				if (!baseShapeMap.TryGetValue(pair.Key, out List<long> baseTicks) || pair.Value == null || baseTicks == null)
				{
					continue;
				}
				int count = Math.Min(pair.Value.Count, baseTicks.Count);
				for (int i = 0; i < count; i++)
				{
					offsets.Add(pair.Value[i] - baseTicks[i]);
				}
			}
			if (offsets.Count == 0)
			{
				return 0L;
			}
			offsets.Sort();
			return offsets[offsets.Count / 2];
		}

		private static Dictionary<NoteShapeFingerprint, List<long>> CreateNoteShapeMap(List<MusicScoreNoteBase> noteList)
		{
			Dictionary<NoteShapeFingerprint, List<long>> result = new Dictionary<NoteShapeFingerprint, List<long>>();
			if (noteList == null)
			{
				return result;
			}
			foreach (MusicScoreNoteBase note in noteList)
			{
				if (note == null || MusicScoreMakerUtility.IsExcludedFromChangeRateCheck(note.category))
				{
					continue;
				}
				NoteShapeFingerprint key = NoteShapeFingerprint.FromNote(note);
				if (!result.TryGetValue(key, out List<long> ticks))
				{
					ticks = new List<long>();
					result[key] = ticks;
				}
				ticks.Add(note.ticks);
			}
			foreach (List<long> ticks in result.Values)
			{
				ticks.Sort();
			}
			return result;
		}

		private static int CountNotesInShapeMap(Dictionary<NoteShapeFingerprint, List<long>> shapeMap)
		{
			if (shapeMap == null)
			{
				return 0;
			}
			int count = 0;
			foreach (List<long> ticks in shapeMap.Values)
			{
				count += ticks?.Count ?? 0;
			}
			return count;
		}

		private static Dictionary<NoteShapeFingerprint, List<long>> CreateMirroredNoteShapeMap(Dictionary<NoteShapeFingerprint, List<long>> shapeMap)
		{
			Dictionary<NoteShapeFingerprint, List<long>> result = new Dictionary<NoteShapeFingerprint, List<long>>(shapeMap?.Count ?? 0);
			if (shapeMap == null)
			{
				return result;
			}
			foreach (KeyValuePair<NoteShapeFingerprint, List<long>> pair in shapeMap)
			{
				NoteShapeFingerprint key = pair.Key.Mirror(LaneCount);
				if (!result.TryGetValue(key, out List<long> ticks))
				{
					ticks = new List<long>(pair.Value ?? Enumerable.Empty<long>());
					result[key] = ticks;
				}
				else if (pair.Value != null)
				{
					ticks.AddRange(pair.Value);
				}
				ticks.Sort();
			}
			return result;
		}

		private static float GetNoteChangeRateThreshold()
		{
			return NOTE_CHANGE_RATE_THRESHOLD_FALLBACK;
		}

		private static int GetNoteTicksTolerance()
		{
			return NOTE_TICKS_TOLERANCE_FALLBACK;
		}

		private string ComputeHash(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return string.Empty;
			}
			using (MD5 md5 = MD5.Create())
			{
				byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(text));
				StringBuilder builder = new StringBuilder(32);
				foreach (byte value in hash)
				{
					builder.Append(value.ToString("x2"));
				}
				return builder.ToString();
			}
		}

		static MusicScoreMakerModel()
		{
			LaneCount = 12;
			LaneCountMinus1 = LaneCount - 1;
		}
	}
}
