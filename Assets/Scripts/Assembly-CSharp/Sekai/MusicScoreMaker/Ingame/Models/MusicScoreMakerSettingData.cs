using System.Runtime.CompilerServices;
using Beebyte.Obfuscator;
using MessagePack;

namespace Sekai.MusicScoreMaker.Ingame.Models
{
	[Skip]
	[MessagePackObject(false)]
	public class MusicScoreMakerSettingData
	{
		public const float DEFAULT_ZOOM_TIMELINE_STEP = 0.104f;

		public const float MIN_ZOOM_TIMELINE_STEP = 0.01f;

		public const float MAX_ZOOM_TIMELINE_STEP = 0.2f;

		public const float DEFAULT_ZOOM_TIMELINE_SCALE_MAX = 10f;

		public const float DEFAULT_ZOOM_TIMELINE_SCALE_MIN = 0.01f;

		public const float MIN_ZOOM_TIMELINE_SCALE = 0.001f;

		public const float MAX_ZOOM_TIMELINE_SCALE = 100f;

		public const int DEFAULT_UNDO_STACK_LIMIT = 100;

		public const int MIN_UNDO_STACK_LIMIT = 1;

		public const int MAX_UNDO_STACK_LIMIT = 1000;

		public const bool DEFAULT_AUTO_SAVE_ENABLED = true;

		public const int DEFAULT_AUTO_SAVE_INTERVAL = 600;

		public const int MIN_AUTO_SAVE_INTERVAL = 5;

		public const int MAX_AUTO_SAVE_INTERVAL = 3600;

		public const float DEFAULT_SHOW_FOCUS_TICKS_RATE = 0.2f;

		public const float MIN_SHOW_FOCUS_TICKS_RATE = 0f;

		public const float MAX_SHOW_FOCUS_TICKS_RATE = 1f;

		public const float DEFAULT_TICKS_PER_SCROLL_STEP = 0.025f;

		public const float MIN_TICKS_PER_SCROLL_STEP = 0.01f;

		public const float MAX_TICKS_PER_SCROLL_STEP = 1f;

		public const bool DEFAULT_ENABLE_SWIPE_SCROLL = true;

		public const bool DEFAULT_PLAY_MUSIC_SE_ENABLED = true;

		public const bool DEFAULT_SET_START_MUSIC_TIME_MS_ENABLED = true;

		public const bool DEFAULT_PLAY_START_EFFECT_ENABLED = true;

		public const int DEFAULT_TEST_PLAY_START_OFFSET_MS = -1000;

		public const bool DEFAULT_SHOW_BAR_LINES = true;

		public const bool DEFAULT_SHOW_BEAT_LINES = true;

		public const bool DEFAULT_SHOW_QUANTIZE_LINES = true;

		public const float DEFAULT_SCORE_DISPLAY_SCALE_HORIZONTAL = 1f;

		public const float DEFAULT_SCORE_DISPLAY_SCALE_VERTICAL = 1f;

		public const float MIN_SCORE_DISPLAY_SCALE = 0.1f;

		public const float MAX_SCORE_DISPLAY_SCALE = 2f;

		public const float DEFAULT_NOTE_EDGE_WIDTH = 32f;

		public const float MIN_NOTE_EDGE_WIDTH = 10f;

		public const float MAX_NOTE_EDGE_WIDTH = 128f;

		public const int DEFAULT_SELECTED_LAYOUT_PATTERN_INDEX_PORTRAIT = 0;

		public const int DEFAULT_SELECTED_LAYOUT_PATTERN_INDEX_LANDSCAPE = 0;

		public const int LAYOUT_PATTERN_COUNT = 3;

		public const float DEFAULT_TOOL_WINDOW_CHILD_SCALE = 1f;

		public const float DEFAULT_TOOL_WINDOW_CHILD_SCALE_MIN = 0.5f;

		public const float DEFAULT_TOOL_WINDOW_CHILD_SCALE_MAX = 2f;

		public const bool DEFAULT_ENABLE_INVALID_PLACEMENT_CHECK = true;

		public const bool DEFAULT_AREA_SELECT_PARTIAL_OVERLAP = true;

		public const float DEFAULT_NOTE_Y_SCALE_START_THRESHOLD = 2f;

		public const float MIN_NOTE_Y_SCALE_START_THRESHOLD = 0.1f;

		public const float MAX_NOTE_Y_SCALE_START_THRESHOLD = 10f;

		public const float DEFAULT_NOTE_Y_SCALE_END_THRESHOLD = 4f;

		public const float MIN_NOTE_Y_SCALE_END_THRESHOLD = 0.3f;

		public const float MAX_NOTE_Y_SCALE_END_THRESHOLD = 10f;

		public const float DEFAULT_NOTE_Y_SCALE_MIN = 0.3f;

		public const float MIN_NOTE_Y_SCALE_MIN = 0.1f;

		public const float MAX_NOTE_Y_SCALE_MIN = 1f;

		public const bool DEFAULT_DRAW_SMALLER_TICK_TO_BACK = true;

		public const int DEFAULT_MAX_CLIPBOARD_CACHE_COUNT = 6;

		public const int MIN_MAX_CLIPBOARD_CACHE_COUNT = 1;

		public const int MAX_MAX_CLIPBOARD_CACHE_COUNT = 100;

		public const bool DEFAULT_AUTO_PLAY_ENABLED = false;

		public const int DEFAULT_TEST_PLAY_LIVE_MODE_TYPE_RAW = -1;

		[Key(0)]
		public float ZoomTimelineStep
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		[Key(1)]
		public float ZoomTimelineScaleMax
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		[Key(2)]
		public float ZoomTimelineScaleMin
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		[Key(3)]
		public int UndoStackLimit
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		[Key(4)]
		public bool AutoSaveEnabled
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		[Key(5)]
		public int AutoSaveInterval
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		[Key(6)]
		public float ShowFocusTicksRate
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		[Key(7)]
		public float TicksPerScrollStep
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		[Key(8)]
		public bool EnableSwipeScroll
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		[Key(9)]
		public bool PlayMusicSEEnabled
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		[Key(10)]
		public bool SetStartMusicTimeMsEnabled
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		[Key(11)]
		public bool PlayStartEffectEnabled
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		[Key(12)]
		public int TestPlayStartOffsetMs
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		[Key(13)]
		public bool ShowBarLines
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		[Key(14)]
		public bool ShowBeatLines
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		[Key(15)]
		public bool ShowQuantizeLines
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		[Key(16)]
		public int SelectedLayoutPatternIndex
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		[Key(26)]
		public int SelectedLayoutPatternIndexPortrait
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		[Key(27)]
		public int SelectedLayoutPatternIndexLandscape
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		[Key(17)]
		public float ScoreDisplayScaleHorizontal
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		[Key(18)]
		public float ScoreDisplayScaleVertical
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		[Key(19)]
		public float ToolWindowChildScale
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		[Key(24)]
		public float NoteEdgeWidth
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		[Key(25)]
		public bool EnableInvalidPlacementCheck
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		[Key(28)]
		public bool AreaSelectPartialOverlap
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		[Key(29)]
		public float NoteYScaleStartThreshold
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		[Key(30)]
		public float NoteYScaleEndThreshold
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		[Key(31)]
		public float NoteYScaleMin
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		[Key(32)]
		public bool DrawSmallerTickToBack
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		[Key(33)]
		public int MaxClipboardCacheCount
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		[Key(34)]
		public bool AutoPlayEnabled
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		[Key(35)]
		public int TestPlayLiveModeTypeRaw
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public MusicScoreMakerSettingData()
		{
			ZoomTimelineStep = DEFAULT_ZOOM_TIMELINE_STEP;
			ZoomTimelineScaleMax = DEFAULT_ZOOM_TIMELINE_SCALE_MAX;
			ZoomTimelineScaleMin = DEFAULT_ZOOM_TIMELINE_SCALE_MIN;
			UndoStackLimit = DEFAULT_UNDO_STACK_LIMIT;
			AutoSaveEnabled = DEFAULT_AUTO_SAVE_ENABLED;
			AutoSaveInterval = DEFAULT_AUTO_SAVE_INTERVAL;
			ShowFocusTicksRate = DEFAULT_SHOW_FOCUS_TICKS_RATE;
			TicksPerScrollStep = DEFAULT_TICKS_PER_SCROLL_STEP;
			EnableSwipeScroll = DEFAULT_ENABLE_SWIPE_SCROLL;
			PlayMusicSEEnabled = DEFAULT_PLAY_MUSIC_SE_ENABLED;
			SetStartMusicTimeMsEnabled = DEFAULT_SET_START_MUSIC_TIME_MS_ENABLED;
			PlayStartEffectEnabled = DEFAULT_PLAY_START_EFFECT_ENABLED;
			TestPlayStartOffsetMs = DEFAULT_TEST_PLAY_START_OFFSET_MS;
			ShowBarLines = DEFAULT_SHOW_BAR_LINES;
			ShowBeatLines = DEFAULT_SHOW_BEAT_LINES;
			ShowQuantizeLines = DEFAULT_SHOW_QUANTIZE_LINES;
			SelectedLayoutPatternIndex = DEFAULT_SELECTED_LAYOUT_PATTERN_INDEX_PORTRAIT;
			SelectedLayoutPatternIndexPortrait = DEFAULT_SELECTED_LAYOUT_PATTERN_INDEX_PORTRAIT;
			SelectedLayoutPatternIndexLandscape = DEFAULT_SELECTED_LAYOUT_PATTERN_INDEX_LANDSCAPE;
			ScoreDisplayScaleHorizontal = DEFAULT_SCORE_DISPLAY_SCALE_HORIZONTAL;
			ScoreDisplayScaleVertical = DEFAULT_SCORE_DISPLAY_SCALE_VERTICAL;
			ToolWindowChildScale = DEFAULT_TOOL_WINDOW_CHILD_SCALE;
			NoteEdgeWidth = DEFAULT_NOTE_EDGE_WIDTH;
			EnableInvalidPlacementCheck = DEFAULT_ENABLE_INVALID_PLACEMENT_CHECK;
			AreaSelectPartialOverlap = DEFAULT_AREA_SELECT_PARTIAL_OVERLAP;
			NoteYScaleStartThreshold = DEFAULT_NOTE_Y_SCALE_START_THRESHOLD;
			NoteYScaleEndThreshold = DEFAULT_NOTE_Y_SCALE_END_THRESHOLD;
			NoteYScaleMin = DEFAULT_NOTE_Y_SCALE_MIN;
			DrawSmallerTickToBack = DEFAULT_DRAW_SMALLER_TICK_TO_BACK;
			MaxClipboardCacheCount = DEFAULT_MAX_CLIPBOARD_CACHE_COUNT;
			AutoPlayEnabled = DEFAULT_AUTO_PLAY_ENABLED;
			TestPlayLiveModeTypeRaw = DEFAULT_TEST_PLAY_LIVE_MODE_TYPE_RAW;
		}
	}
}
