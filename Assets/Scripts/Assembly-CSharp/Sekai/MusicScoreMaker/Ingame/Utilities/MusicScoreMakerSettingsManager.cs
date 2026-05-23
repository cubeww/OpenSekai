using System.Runtime.CompilerServices;
using System.IO;
using Newtonsoft.Json;
using Sekai.Live;
using Sekai.MusicScoreMaker.Ingame.Events;
using Sekai.MusicScoreMaker.Ingame.Models;
using UnityEngine;

namespace Sekai.MusicScoreMaker.Ingame.Utilities
{
	public static class MusicScoreMakerSettingsManager
	{
		private const string SaveFolderName = "inappcache";

		private const string SaveFileName = "MusicScoreMakerSettingData.json";

		private static bool _lastSavedEnableInvalidPlacementCheck;

		private static MusicScoreMakerSettingData _settingData;

		public static float ZoomTimelineStep
		{
			get
			{
				return SettingData.ZoomTimelineStep;
			}
			set
			{
				SettingData.ZoomTimelineStep = value;
			}
		}

		public static float ZoomTimelineScaleMax
		{
			get
			{
				return SettingData.ZoomTimelineScaleMax;
			}
			set
			{
				SettingData.ZoomTimelineScaleMax = value;
			}
		}

		public static float ZoomTimelineScaleMin
		{
			get
			{
				return SettingData.ZoomTimelineScaleMin;
			}
			set
			{
				SettingData.ZoomTimelineScaleMin = value;
			}
		}

		public static int UndoStackLimit
		{
			get
			{
				return SettingData.UndoStackLimit;
			}
			set
			{
				SettingData.UndoStackLimit = value;
			}
		}

		public static bool AutoSaveEnabled
		{
			get
			{
				return SettingData.AutoSaveEnabled;
			}
			set
			{
				SettingData.AutoSaveEnabled = value;
			}
		}

		public static int AutoSaveInterval
		{
			get
			{
				return SettingData.AutoSaveInterval;
			}
			set
			{
				SettingData.AutoSaveInterval = value;
			}
		}

		public static bool PlayMusicSEEnabled
		{
			get
			{
				return SettingData.PlayMusicSEEnabled;
			}
			set
			{
				SettingData.PlayMusicSEEnabled = value;
			}
		}

		public static bool SetStartMusicTimeMsEnabled
		{
			get
			{
				return SettingData.SetStartMusicTimeMsEnabled;
			}
			set
			{
				SettingData.SetStartMusicTimeMsEnabled = value;
			}
		}

		public static int TestPlayStartOffsetMs
		{
			get
			{
				return SettingData.TestPlayStartOffsetMs;
			}
			set
			{
				SettingData.TestPlayStartOffsetMs = value;
			}
		}

		public static bool EnableInvalidPlacementCheck
		{
			get
			{
				return SettingData.EnableInvalidPlacementCheck;
			}
			set
			{
				SettingData.EnableInvalidPlacementCheck = value;
			}
		}

		public static float ShowFocusTicksRate
		{
			get
			{
				return SettingData.ShowFocusTicksRate;
			}
			set
			{
				SettingData.ShowFocusTicksRate = value;
			}
		}

		public static float TicksPerScrollStep
		{
			get
			{
				return SettingData.TicksPerScrollStep;
			}
			set
			{
				SettingData.TicksPerScrollStep = value;
			}
		}

		public static bool EnableSwipeScroll
		{
			get
			{
				return SettingData.EnableSwipeScroll;
			}
			set
			{
				SettingData.EnableSwipeScroll = value;
			}
		}

		public static bool ShowBarLines
		{
			get
			{
				return SettingData.ShowBarLines;
			}
			set
			{
				SettingData.ShowBarLines = value;
			}
		}

		public static bool ShowBeatLines
		{
			get
			{
				return SettingData.ShowBeatLines;
			}
			set
			{
				SettingData.ShowBeatLines = value;
			}
		}

		public static bool ShowQuantizeLines
		{
			get
			{
				return SettingData.ShowQuantizeLines;
			}
			set
			{
				SettingData.ShowQuantizeLines = value;
			}
		}

		public static float ScoreDisplayScaleHorizontal
		{
			get
			{
				return SettingData.ScoreDisplayScaleHorizontal;
			}
			set
			{
				if (SettingData.ScoreDisplayScaleHorizontal == value)
				{
					return;
				}
				SettingData.ScoreDisplayScaleHorizontal = value;
				PublishScoreDisplayScaleChanged();
			}
		}

		public static float ScoreDisplayScaleVertical
		{
			get
			{
				return SettingData.ScoreDisplayScaleVertical;
			}
			set
			{
				if (SettingData.ScoreDisplayScaleVertical == value)
				{
					return;
				}
				SettingData.ScoreDisplayScaleVertical = value;
				PublishScoreDisplayScaleChanged();
			}
		}

		public static float ToolWindowChildScale
		{
			get
			{
				return SettingData.ToolWindowChildScale;
			}
			set
			{
				if (SettingData.ToolWindowChildScale == value)
				{
					return;
				}
				SettingData.ToolWindowChildScale = value;
				Publish(new ToolWindowChildScaleChangedEvent());
			}
		}

		public static float NoteEdgeWidth
		{
			get
			{
				return SettingData.NoteEdgeWidth;
			}
			set
			{
				SettingData.NoteEdgeWidth = value;
			}
		}

		public static int LayoutPatternCount
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public static int SelectedLayoutPatternIndexPortrait
		{
			get
			{
				return SettingData.SelectedLayoutPatternIndexPortrait;
			}
			set
			{
				SettingData.SelectedLayoutPatternIndexPortrait = value;
			}
		}

		public static int SelectedLayoutPatternIndexLandscape
		{
			get
			{
				return SettingData.SelectedLayoutPatternIndexLandscape;
			}
			set
			{
				SettingData.SelectedLayoutPatternIndexLandscape = value;
			}
		}

		public static bool AreaSelectPartialOverlap
		{
			get
			{
				return SettingData.AreaSelectPartialOverlap;
			}
			set
			{
				SettingData.AreaSelectPartialOverlap = value;
			}
		}

		public static float NoteYScaleStartThreshold
		{
			get
			{
				return SettingData.NoteYScaleStartThreshold;
			}
			set
			{
				SettingData.NoteYScaleStartThreshold = value;
			}
		}

		public static float NoteYScaleEndThreshold
		{
			get
			{
				return SettingData.NoteYScaleEndThreshold;
			}
			set
			{
				SettingData.NoteYScaleEndThreshold = value;
			}
		}

		public static float NoteYScaleMin
		{
			get
			{
				return SettingData.NoteYScaleMin;
			}
			set
			{
				SettingData.NoteYScaleMin = value;
			}
		}

		public static bool DrawSmallerTickToBack
		{
			get
			{
				return SettingData.DrawSmallerTickToBack;
			}
			set
			{
				SettingData.DrawSmallerTickToBack = value;
			}
		}

		public static int MaxClipboardCacheCount
		{
			get
			{
				return SettingData.MaxClipboardCacheCount;
			}
			set
			{
				SettingData.MaxClipboardCacheCount = value;
			}
		}

		public static bool AutoPlayEnabled
		{
			get
			{
				return SettingData.AutoPlayEnabled;
			}
			set
			{
				SettingData.AutoPlayEnabled = value;
			}
		}

		static MusicScoreMakerSettingsManager()
		{
			LayoutPatternCount = MusicScoreMakerSettingData.LAYOUT_PATTERN_COUNT;
			LoadSettingData();
		}

		private static MusicScoreMakerSettingData SettingData => _settingData ??= new MusicScoreMakerSettingData();

		private static string SavePath => Path.Combine(Application.persistentDataPath, SaveFolderName, SaveFileName);

		private static void LoadSettingData()
		{
			try
			{
				if (File.Exists(SavePath))
				{
					_settingData = JsonConvert.DeserializeObject<MusicScoreMakerSettingData>(File.ReadAllText(SavePath));
				}
			}
			catch
			{
				_settingData = null;
			}
			_settingData ??= new MusicScoreMakerSettingData();
			_lastSavedEnableInvalidPlacementCheck = _settingData.EnableInvalidPlacementCheck;
		}

		public static void SaveSettingData()
		{
			try
			{
				string directory = Path.GetDirectoryName(SavePath);
				if (!string.IsNullOrEmpty(directory))
				{
					Directory.CreateDirectory(directory);
				}
				File.WriteAllText(SavePath, JsonConvert.SerializeObject(SettingData));
			}
			catch
			{
			}
			if (_lastSavedEnableInvalidPlacementCheck == SettingData.EnableInvalidPlacementCheck)
			{
				return;
			}
			_lastSavedEnableInvalidPlacementCheck = SettingData.EnableInvalidPlacementCheck;
			Publish(new EnableInvalidPlacementCheckChangedEvent
			{
				IsEnabled = SettingData.EnableInvalidPlacementCheck
			});
		}

		public static void DeleteCache()
		{
			LoadSettingData();
		}

		public static void ResetSettingData()
		{
			try
			{
				if (File.Exists(SavePath))
				{
					File.Delete(SavePath);
				}
			}
			catch
			{
			}
			LoadSettingData();
			Publish(new ScoreDisplayScaleChangedEvent());
			Publish(new ToolWindowChildScaleChangedEvent());
			Publish(new UpdateMusicScoreEvent());
		}

		public static long GetFocusTicksMs(MusicScoreInfo[] musicScoreInfoArray)
		{
			long focusTicks = MusicScoreMakerUtility.GetFocusTicks();
			if (focusTicks < 1L)
			{
				return 0L;
			}
			float focusTimeMs = MusicScoreMakerUtility.GetTimeFromTicks(focusTicks, musicScoreInfoArray) * 1000f;
			return float.IsPositiveInfinity(focusTimeMs) ? long.MinValue : (long)focusTimeMs;
		}

		public static long CalcStartMusicTimeMs(FreeLiveBootData data, long fillerSec)
		{
			long startMusicTimeMs = 1000L * fillerSec;
			if (SetStartMusicTimeMsEnabled)
			{
				MusicScoreInfo[] musicScoreInfoArray = null;
				try
				{
					musicScoreInfoArray = data?.MusicData?.MusicScore?.musicScoreInfoArray;
				}
				catch
				{
				}
				startMusicTimeMs += GetFocusTicksMs(musicScoreInfoArray);
			}
			return startMusicTimeMs + TestPlayStartOffsetMs;
		}

		public static bool TryGetTestPlayLiveModeType(out LiveSettingData.LiveModeType liveModeType)
		{
			int raw = SettingData.TestPlayLiveModeTypeRaw;
			liveModeType = raw < 0 ? default : (LiveSettingData.LiveModeType)raw;
			return raw >= 0;
		}

		public static void SetTestPlayLiveModeType(LiveSettingData.LiveModeType liveModeType)
		{
			SettingData.TestPlayLiveModeTypeRaw = (int)liveModeType;
		}

		private static void PublishScoreDisplayScaleChanged()
		{
			Publish(new ScoreDisplayScaleChangedEvent());
			Publish(new UpdateMusicScoreEvent());
		}

		private static void Publish<T>(T eventData) where T : MusicScoreMakerDispatcherEventBase
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
			}
		}
	}
}
