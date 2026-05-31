using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sekai.Live
{
	public static class LiveConfig
	{
		public struct NoteTypeJudgeData
		{
			public int MissMesh;

			public Dictionary<JudgeFrameType, JudgeTimeData> JudgeTimes;

			public float JudgeTimeBefore;

			public float JudgeTimeAfter;
		}

		public const float DefaultNoteSpeed = 6f;

		public const int MinNoteSpeed = 1;

		public const int MaxNoteSpeed = 12;

		public const float DefaultNoteTiming = 0f;

		public const int MinNoteTiming = -20;

		public const int MaxNoteTiming = 20;

		public const int MaxNoteShowRate = 100;

		public const int MinNoteShowRate = 0;

		public static readonly float[][] VariationValues;

		public const int MinBrightness = 50;

		public const int MaxBrightness = 100;

		public const float VariationBrightness = 10f;

		public const int MinLaneAlpha = 0;

		public const int MaxLaneAlpha = 100;

		public const float VariationLaneAlpha = 10f;

		public const string PercentageFormat = "%";

		public static readonly float ContinueNoDamageTime;

		public static readonly int MaxSkillCount;

		public static readonly int Life;

		public static readonly float JustPerfectJudgeTime;

		public static readonly float JustPerfectJudgeTimeHalf;

		public static readonly float JudgeTime;

		public static readonly int LotteryPraiseCount;

		public static readonly float PraiseComboRate;

		public static readonly float PraiseScoreFactor;

		public static readonly float LotteryIntervalFactor;

		public static readonly Dictionary<NoteResult, int> Damages;

		public static readonly string ConfigBundleNamePath;

		public static string NoteBundleName;

		public static string NoteSeName;

		public static string EffectBundleName;

		public static string NotePreviewBundleName;

		public static readonly string NotePreviewDialogBundleName;

		public static string NoteSeViewName;

		public static readonly string ResultBundleName;

		public static readonly string CutinBundleName;

		public static string FeverBundleName;

		public static readonly string SkillBundleName;

		public static float LongNoteAlpha;

		public static float GuideAlpha;

		public static readonly string[] TimelineBundleNames;

		public static readonly Vector2 SpawnPosition;

		public static readonly Vector2[] JudgmentPositions;

		public static readonly float widthX;

		public static NoteTypeJudgeData noteTypeJudgeData;

		public static float MinNoteSpeedTime { get; set; }

		public static float MaxNoteSpeedTime { get; set; }

		public static float CacheSpeedTime { get; set; }

		public static float CacheTimingTime { get; set; }

		public static float NoteSpeedOffset { get; set; }

		public static float NoteSpeedCoefficient { get; set; }

		public static string GetNoteBundleName(int index)
		{
			return string.Format("live/note/custom{0:00}", index + 1);
		}

		public static string GetNotePreviewBundleName(int index)
		{
			return string.Format("live/note_preview/custom{0:00}", index + 1);
		}

		public static string GetNoteSeName(int index)
		{
			return string.Format("custom{0:00}", index + 1);
		}

		public static string GetNoteSeViewName(int index)
		{
			return string.Format("SOUND{0:00}", index + 1);
		}

		public static string GetEffectBundleName(int index)
		{
			return string.Format("effect_asset/live/tap_effect/{0}", index);
		}

		public static string GetFeverBundleName(int index)
		{
			return string.Format("effect_asset/live/fever/{0}", index);
		}

		public static void SetNoteSkinAssetBundleName(int index)
		{
			NoteBundleName = GetNoteBundleName(index);
			NotePreviewBundleName = GetNotePreviewBundleName(index);
		}

		public static void SetNoteSeName(int index)
		{
			NoteSeName = GetNoteSeName(index);
			NoteSeViewName = GetNoteSeViewName(index);
		}

		public static void SetNoteEffectName(int index)
		{
			EffectBundleName = GetEffectBundleName(index);
		}

		public static void SetFeverEffectName(int index)
		{
			FeverBundleName = GetFeverBundleName(index);
		}

		public static void LoadMasterConfig()
		{
			noteTypeJudgeData.JudgeTimes = new Dictionary<JudgeFrameType, JudgeTimeData>();
			AddJudgeFrame(JudgeFrameType.Normal, 2.5f, 2.5f, 5f, 5f, 6.5f, 6.5f, 7.5f, 7.5f);
			AddJudgeFrame(JudgeFrameType.Flick, 2.5f, 2.5f, 6.5f, 7.5f, 7f, 8f, 7.5f, 8.5f);
			AddJudgeFrame(JudgeFrameType.Long, 2.5f, 2.5f, 5f, 5f, 6.5f, 6.5f, 7.5f, 7.5f);
			AddJudgeFrame(JudgeFrameType.Long_End, 3.5f, 4f, 6.5f, 8f, 7.5f, 8.5f, 7.5f, 8.5f);
			AddJudgeFrame(JudgeFrameType.Long_End_Flick, 3.5f, 4f, 6.5f, 8f, 7.5f, 8.5f, 7.5f, 8.5f);
			AddJudgeFrame(JudgeFrameType.Normal_Critical, 3.3f, 3.3f, 4.5f, 4.5f, 6.5f, 6.5f, 7.5f, 7.5f);
			AddJudgeFrame(JudgeFrameType.Flick_Critical, 3.5f, 3.5f, 6.5f, 7.5f, 7f, 8f, 7.5f, 8.5f);
			AddJudgeFrame(JudgeFrameType.Long_Critical, 3.3f, 3.3f, 4.5f, 4.5f, 6.5f, 6.5f, 7.5f, 7.5f);
			AddJudgeFrame(JudgeFrameType.Long_End_Critical, 3.5f, 4f, 6.5f, 8f, 7.5f, 8.5f, 7.5f, 8.5f);
			AddJudgeFrame(JudgeFrameType.Long_End_Flick_Critical, 3.5f, 4f, 6.5f, 8f, 7.5f, 8.5f, 7.5f, 8.5f);

			// TODO(original): confirm the friction-specific judge frames once the matching master data is imported.
			CopyJudgeFrame(JudgeFrameType.Friction, JudgeFrameType.Flick);
			CopyJudgeFrame(JudgeFrameType.Friction_Critical, JudgeFrameType.Flick_Critical);
			CopyJudgeFrame(JudgeFrameType.Friction_Flick, JudgeFrameType.Flick);
			CopyJudgeFrame(JudgeFrameType.Friction_Flick_Critical, JudgeFrameType.Flick_Critical);
			CopyJudgeFrame(JudgeFrameType.Friction_Long, JudgeFrameType.Long);
			CopyJudgeFrame(JudgeFrameType.Friction_Long_Critical, JudgeFrameType.Long_Critical);
			CopyJudgeFrame(JudgeFrameType.Friction_Long_End, JudgeFrameType.Long_End);
			CopyJudgeFrame(JudgeFrameType.Friction_Long_End_Critical, JudgeFrameType.Long_End_Critical);
		}

		private static void AddJudgeFrame(JudgeFrameType noteType, float perfectBefore, float perfectAfter, float greatBefore, float greatAfter, float goodBefore, float goodAfter, float badBefore, float badAfter)
		{
			var judgeTimeData = new JudgeTimeData(
				noteType,
				CalcFrameToTime(perfectBefore),
				CalcFrameToTime(perfectAfter),
				CalcFrameToTime(greatBefore),
				CalcFrameToTime(greatAfter),
				CalcFrameToTime(goodBefore),
				CalcFrameToTime(goodAfter),
				CalcFrameToTime(badBefore),
				CalcFrameToTime(badAfter));

			noteTypeJudgeData.JudgeTimes[noteType] = judgeTimeData;
			UpdateJudgeTimeBeforeAndAfter(judgeTimeData);
		}

		private static void CopyJudgeFrame(JudgeFrameType noteType, JudgeFrameType source)
		{
			var sourceData = noteTypeJudgeData.JudgeTimes[source];
			var judgeTimeData = new JudgeTimeData(
				noteType,
				sourceData.PerfectBeforeJudgeTime,
				sourceData.PerfectAfterJudgeTime,
				sourceData.GreatBeforeJudgeTime,
				sourceData.GreatAfterJudgeTime,
				sourceData.GoodBeforeJudgeTime,
				sourceData.GoodAfterJudgeTime,
				sourceData.BadBeforeJudgeTime,
				sourceData.BadAfterJudgeTime);

			noteTypeJudgeData.JudgeTimes[noteType] = judgeTimeData;
			UpdateJudgeTimeBeforeAndAfter(judgeTimeData);
		}

		private static void UpdateJudgeTimeBeforeAndAfter(JudgeTimeData judgeTimeData)
		{
			if (judgeTimeData.BadBeforeJudgeTime > noteTypeJudgeData.JudgeTimeBefore)
			{
				noteTypeJudgeData.JudgeTimeBefore = judgeTimeData.BadBeforeJudgeTime;
			}

			if (judgeTimeData.BadAfterJudgeTime > noteTypeJudgeData.JudgeTimeAfter)
			{
				noteTypeJudgeData.JudgeTimeAfter = judgeTimeData.BadAfterJudgeTime;
			}
		}

		public static void LoadOptionData()
		{
			LiveSettingData liveSettingData = LiveSettingData.LoadFromStorage();
			LongNoteAlpha = liveSettingData.GetNoteAlpha();
			GuideAlpha = liveSettingData.GetGuideAlpha();
			SetNoteSkinAssetBundleName(Mathf.Clamp(liveSettingData.NoteSkinIndex, 0, 1));
			SetNoteSeName(liveSettingData.NoteSeIndex);
			SetNoteEffectName(liveSettingData.NoteEffect);
			SetFeverEffectName(liveSettingData.FeverEffectTypeIndex);
			CacheSpeedTime = 0f;
			CacheTimingTime = 0f;
			NoteSpeedOffset = 0.04f;
			NoteSpeedCoefficient = 0.22f;
		}

		public static float GetNoteDisplayOffsetTime(float noteSpeed)
		{
			if (CacheSpeedTime <= 0f)
			{
				var rate = Math.Max(noteSpeed - 1f, 0f) / 11f;
				var reverseRate = 1f - Math.Min(rate, 1f);
				var eased = 1f - Mathf.Pow(Math.Max(reverseRate, 0f), 1.31f);
				CacheSpeedTime = Math.Max(eased, 0f) * -3.65f + 4f;
			}

			return CacheSpeedTime;
		}

		public static float CalcFrameToTime(float frame)
		{
			return frame * (1f / 60f);
		}

		public static ScreenConfig.ScreenSize GetRenderTextureSize(MVQualityType qualityType, LivePlayMode playMode = LivePlayMode.MusicVideo)
		{
			var low = playMode == LivePlayMode.MusicVideo ? ScreenConfig.LowDPISize : ScreenConfig.LowDPISizeForIngame3DMV;
			var high = playMode == LivePlayMode.MusicVideo ? ScreenConfig.HighDPISize : ScreenConfig.HighDPISizeForIngame3DMV;
			return qualityType == MVQualityType.Low ? low : high;
		}

		public static bool IsLongEndJudgeTime(float offsetTime)
		{
			return -noteTypeJudgeData.JudgeTimeAfter <= offsetTime && noteTypeJudgeData.JudgeTimeBefore >= offsetTime;
		}

		public static float GetNoteViewProgress(float progress)
		{
			var value = Mathf.Pow(1.06f, (Mathf.Min(progress, 2f) - 1f) * 45f);
			if (value <= 0.04f || float.IsNaN(value))
			{
				return 0f;
			}

			return float.IsInfinity(value) ? 2f : value;
		}

		public static float GetNoteLineParentProgress(float time, INote startNote, INote endNote)
		{
			var duration = endNote.MusicScoreInfo.time - startNote.MusicScoreInfo.time;
			if (Mathf.Approximately(duration, 0f))
			{
				return 1f;
			}

			var progress = Mathf.Clamp01((time - startNote.MusicScoreInfo.time) / duration);
			return GetNoteLineCenterProgress(progress, startNote);
		}

		public static float GetNoteLineCenterProgress(float t, INote startNote)
		{
			t = Mathf.Clamp01(t);
			return startNote?.LineType switch
			{
				NoteLineType.EaseOut => 1f - (1f - t) * (1f - t),
				NoteLineType.EaseIn => t * t,
				_ => t
			};
		}

		public static (NoteResult, NoteResultDescription) CalculateDefaultNoteResult(INote note, float time)
		{
			return GetJudgeResultInternal(note.MusicScoreInfo.time - time, note.Type == NoteType.Critical ? JudgeFrameType.Normal_Critical : JudgeFrameType.Normal, true);
		}

		public static (NoteResult, NoteResultDescription) CalculateLongStartNoteResult(INote note, float time)
		{
			return GetJudgeResultInternal(note.MusicScoreInfo.time - time, note.Type == NoteType.Critical ? JudgeFrameType.Long_Critical : JudgeFrameType.Long, true);
		}

		public static (NoteResult, NoteResultDescription) CalculateLongEndNoteResult(INote note, float time)
		{
			return GetJudgeResultInternal(note.MusicScoreInfo.time - time, note.Type == NoteType.Critical ? JudgeFrameType.Long_End_Critical : JudgeFrameType.Long_End, true);
		}

		public static (NoteResult, NoteResultDescription) CalculateFlickNoteResult(INote note, float time, bool isDirection)
		{
			return GetJudgeResultInternal(note.MusicScoreInfo.time - time, note.Type == NoteType.Critical ? JudgeFrameType.Flick_Critical : JudgeFrameType.Flick, isDirection);
		}

		public static (NoteResult, NoteResultDescription) CalculateLongEndFlickNoteResult(INote note, float time, bool isDirection)
		{
			return GetJudgeResultInternal(note.MusicScoreInfo.time - time, note.Type == NoteType.Critical ? JudgeFrameType.Long_End_Flick_Critical : JudgeFrameType.Long_End_Flick, isDirection);
		}

		public static (NoteResult, NoteResultDescription) CalculateFrictionFlickNoteResult(INote note, float time, bool isDirection)
		{
			return GetJudgeResultInternal(note.MusicScoreInfo.time - time, note.Type == NoteType.Critical ? JudgeFrameType.Friction_Flick_Critical : JudgeFrameType.Friction_Flick, isDirection);
		}

		private static (NoteResult, NoteResultDescription) GetJudgeResultInternal(float offset, JudgeFrameType judgeFrameType, bool isDirection)
		{
			var data = noteTypeJudgeData.JudgeTimes.TryGetValue(judgeFrameType, out var judgeTimeData)
				? judgeTimeData
				: noteTypeJudgeData.JudgeTimes[JudgeFrameType.Normal];

			var perfectAfter = -data.PerfectAfterJudgeTime;
			if (isDirection)
			{
				if (-JustPerfectJudgeTimeHalf <= offset && JustPerfectJudgeTimeHalf >= offset)
				{
					return (NoteResult.JustPerfect, GetNoteResultDescription(offset));
				}

				if (data.PerfectBeforeJudgeTime >= offset && perfectAfter <= offset)
				{
					return (NoteResult.Perfect, GetNoteResultDescription(offset));
				}
			}
			else if (data.PerfectBeforeJudgeTime >= offset && perfectAfter <= offset)
			{
				return (NoteResult.Great, NoteResultDescription.FlickMiss);
			}

			if (data.GreatBeforeJudgeTime >= offset && -data.GreatAfterJudgeTime <= offset)
			{
				return (NoteResult.Great, GetNoteResultDescription(offset));
			}

			if (data.GoodBeforeJudgeTime >= offset && -data.GoodAfterJudgeTime <= offset)
			{
				return (NoteResult.Good, GetNoteResultDescription(offset));
			}

			if (data.BadBeforeJudgeTime >= offset && -data.BadAfterJudgeTime <= offset)
			{
				return (NoteResult.Bad, GetNoteResultDescription(offset));
			}

			return (NoteResult.Miss, GetNoteResultDescription(offset));
		}

		public static (NoteResult, NoteResultDescription) GetPreviewJudgeResult(float viewTime)
		{
			return GetJudgeResultInternal(viewTime, JudgeFrameType.Normal, true);
		}

		private static NoteResultDescription GetNoteResultDescription(float judgeOffsetTime)
		{
			if (-JustPerfectJudgeTimeHalf <= judgeOffsetTime && JustPerfectJudgeTimeHalf >= judgeOffsetTime)
			{
				return NoteResultDescription.Just;
			}

			return judgeOffsetTime > JustPerfectJudgeTimeHalf ? NoteResultDescription.Fast : NoteResultDescription.Late;
		}

		static LiveConfig()
		{
			VariationValues = new[]
			{
				new[] { 0.3f, 0f, 0f },
				new[] { 1f, 0.1f },
				new[] { 10f, 1f },
				new[] { 0.1f, 0.01f }
			};
			ContinueNoDamageTime = 1f;
			MaxSkillCount = 6;
			Life = 1000;
			JustPerfectJudgeTime = 1f / 60f;
			JustPerfectJudgeTimeHalf = 1f / 120f;
			JudgeTime = 0.125f;
			LotteryPraiseCount = 5;
			PraiseComboRate = 0.3f;
			PraiseScoreFactor = 0.7f;
			LotteryIntervalFactor = 1f;
			MinNoteSpeedTime = 0f;
			MaxNoteSpeedTime = 0.3f;
			CacheSpeedTime = 0f;
			CacheTimingTime = 0f;
			NoteSpeedOffset = 0.04f;
			NoteSpeedCoefficient = 0.22f;
			Damages = new Dictionary<NoteResult, int>
			{
				{ NoteResult.Bad, 60 },
				{ NoteResult.Miss, 100 }
			};
			ConfigBundleNamePath = "Live/Config/LiveBundleBuildData";
			NoteBundleName = "live/note/custom01";
			NoteSeName = "custom01";
			EffectBundleName = "effect_asset/live/tap_effect/0";
			NotePreviewBundleName = "live/note_preview/custom01";
			NotePreviewDialogBundleName = "live/note_preview/dialog";
			NoteSeViewName = "custom01";
			ResultBundleName = "effect_asset/live/result/default";
			CutinBundleName = "effect_asset/live/cutin/default";
			FeverBundleName = "effect_asset/live/fever/0";
			SkillBundleName = "effect_asset/live/skill/default";
			LongNoteAlpha = 1f;
			GuideAlpha = 0.6f;
			TimelineBundleNames = new[] { "camera", "character", "effect", "light", "stage", "penlight" };
			SpawnPosition = new Vector2(0f, 5.46f);
			JudgmentPositions = new[]
			{
				new Vector2(-6.544999f, -2.96f),
				new Vector2(-5.355f, -2.96f),
				new Vector2(-4.165f, -2.96f),
				new Vector2(-2.975f, -2.96f),
				new Vector2(-1.785f, -2.96f),
				new Vector2(-0.595f, -2.96f),
				new Vector2(0.595f, -2.96f),
				new Vector2(1.785f, -2.96f),
				new Vector2(2.975f, -2.96f),
				new Vector2(4.165f, -2.96f),
				new Vector2(5.355f, -2.96f),
				new Vector2(6.544999f, -2.96f)
			};
			widthX = 1.19f;
			noteTypeJudgeData = new NoteTypeJudgeData
			{
				MissMesh = 0,
				JudgeTimes = new Dictionary<JudgeFrameType, JudgeTimeData>(),
				JudgeTimeBefore = 0f,
				JudgeTimeAfter = 0f
			};
			LoadMasterConfig();
		}
	}
}
