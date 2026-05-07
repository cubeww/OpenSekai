using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sekai.Live
{
	public static class LiveConfig
	{
		public struct MasterParamater
		{
			public int MissMesh;

			public Dictionary<JudgeFrameType, MasterIngameJudgeFrame> JudgeFrames;

			public float JudgeTimeBefore;

			public float JudgeTimeAfter;

			public float JudgeTime;
		}

		public const float DefaultNoteSpeed = 7f;

		public const float ContinueNoDamageTime = 1f;

		public static readonly float JustPerfectJudgeTime = 0.016667f;

		public static readonly float JustPerfectJudgeTimeHalf = JustPerfectJudgeTime * 0.5f;

		public static readonly float PerfectJudgeTime = 0.041667f;

		public static readonly float GreatJudgeTime = 0.11667f;

		public static readonly float GoodJudgeTime = 0.125f;

		public static readonly string ConfigBundleNamePath = "Live/Config/LiveBundleBuildData";

		public static int Life = 1000;

		public static float JudgeTime = 0.125f;

		public static readonly Vector2 SpawnPosition = new Vector2(0f, 5.46f);

		public static readonly Vector2[] JudgmentPositions =
		{
			new Vector2(-6.545f, -2.96f),
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
			new Vector2(6.545f, -2.96f)
		};

		public static readonly float widthX = 1.19f;

		public static readonly Vector3 Vector3One = Vector3.one;

		public static readonly Vector3 Vector3Up = Vector3.up;

		public static readonly Vector3 Vector3Zero = Vector3.zero;

		public static float CacheSpeedTime { get; set; }

		public static MasterParamater LiveMasterData = new MasterParamater
		{
			MissMesh = 2,
			JudgeFrames = CreateDefaultJudgeFrames(),
			JudgeTimeBefore = 0.125f,
			JudgeTimeAfter = 0.125f,
			JudgeTime = 0.125f
		};

		public static float LongNoteAlpha = 1f;

		public static float GetNoteDisplayOffsetTime(float noteSpeed)
		{
			if (CacheSpeedTime <= 0f)
			{
				float t = Mathf.Clamp01(Mathf.Max(0f, noteSpeed - 1f) / 11f);
				CacheSpeedTime = Mathf.Lerp(4f, 0.35f, 1f - Mathf.Pow(1f - t, 1.31f));
			}

			return CacheSpeedTime;
		}

		public static float GetNoteViewProgress(float progress)
		{
			float value = Mathf.Pow(1.06f, (Mathf.Min(progress, 2f) - 1f) * 45f);
			if (value <= 0.04f || float.IsNaN(value))
			{
				return 0f;
			}
			if (float.IsInfinity(value))
			{
				return 2f;
			}
			return value;
		}

		public static float GetNoteLineParentProgress(float time, INote startNote, INote endNote)
		{
			float endTime = endNote.MusicScoreInfo.time;
			float startTime = startNote.MusicScoreInfo.time;
			float duration = endTime - startTime;
			float t = Mathf.Approximately(duration, 0f) ? 0f : 1f - Mathf.Clamp01((endTime - time) / duration);

			if (startNote.LineType == NoteLineType.EaseOut)
			{
				return t * t;
			}
			if (startNote.LineType == NoteLineType.EaseIn)
			{
				return 1f - (1f - t) * (1f - t);
			}
			return t;
		}

		public static float GetNoteLineCenterProgress(float t, INote startNote)
		{
			if (startNote.LineType == NoteLineType.EaseIn)
			{
				return 1f - (1f - t) * (1f - t);
			}
			if (startNote.LineType == NoteLineType.EaseOut)
			{
				return t * t;
			}
			return t;
		}

		public static bool IsLongEndJudgeTime(float offsetTime)
		{
			EnsureJudgeFrames();
			return -LiveMasterData.JudgeTimeAfter <= offsetTime && offsetTime <= LiveMasterData.JudgeTimeBefore;
		}

		public static (NoteResult, NoteResultDescription) CalculateDefaultNoteResult(INote note, float time)
		{
			float offset = note.MusicScoreInfo.time - time;
			JudgeFrameType frameType = note.Type == NoteType.Critical ? JudgeFrameType.Normal_Critical : JudgeFrameType.Normal;
			return GetJudgeResultInternal(offset, frameType, true);
		}

		public static (NoteResult, NoteResultDescription) CalculateLongStartNoteResult(INote note, float time)
		{
			float offset = note.MusicScoreInfo.time - time;
			JudgeFrameType frameType = note.Type == NoteType.Critical ? JudgeFrameType.Long_Critical : JudgeFrameType.Long;
			return GetJudgeResultInternal(offset, frameType, true);
		}

		public static (NoteResult, NoteResultDescription) CalculateLongEndNoteResult(INote note, float time)
		{
			float offset = note.MusicScoreInfo.time - time;
			JudgeFrameType frameType = note.Type == NoteType.Critical ? JudgeFrameType.Long_End_Critical : JudgeFrameType.Long_End;
			return GetJudgeResultInternal(offset, frameType, true);
		}

		public static (NoteResult, NoteResultDescription) CalculateFlickNoteResult(INote note, float time, bool isDirection)
		{
			float offset = note.MusicScoreInfo.time - time;
			JudgeFrameType frameType = note.Type == NoteType.Critical ? JudgeFrameType.Flick_Critical : JudgeFrameType.Flick;
			return GetJudgeResultInternal(offset, frameType, isDirection);
		}

		public static (NoteResult, NoteResultDescription) CalculateLongEndFlickNoteResult(INote note, float time, bool isDirection)
		{
			float offset = note.MusicScoreInfo.time - time;
			JudgeFrameType frameType = note.Type == NoteType.Critical ? JudgeFrameType.Long_End_Flick_Critical : JudgeFrameType.Long_End_Flick;
			return GetJudgeResultInternal(offset, frameType, isDirection);
		}

		public static (NoteResult, NoteResultDescription) CalculateFrictionFlickNoteResult(INote note, float time, bool isDirection)
		{
			float offset = note.MusicScoreInfo.time - time;
			JudgeFrameType frameType = note.Type == NoteType.Critical ? JudgeFrameType.Friction_Flick_Critical : JudgeFrameType.Friction_Flick;
			return GetJudgeResultInternal(offset, frameType, isDirection);
		}

		public static (NoteResult, NoteResultDescription) GetPreviewJudgeResult(float viewTime)
		{
			return GetJudgeResultInternal(viewTime, JudgeFrameType.Normal, true);
		}

		private static (NoteResult, NoteResultDescription) GetJudgeResultInternal(float offset, JudgeFrameType judgeFrameType, bool isDirection)
		{
			EnsureJudgeFrames();
			MasterIngameJudgeFrame frame = LiveMasterData.JudgeFrames[judgeFrameType];
			if (isDirection && -JustPerfectJudgeTimeHalf <= offset && JustPerfectJudgeTimeHalf >= offset)
			{
				return (NoteResult.JustPerfect, GetNoteResultDescription(offset));
			}
			if (-frame.PerfectAfterJudgeTime <= offset && frame.PerfectBeforeJudgeTime >= offset)
			{
				if (!isDirection)
				{
					return (NoteResult.Great, NoteResultDescription.FlickMiss);
				}
				return (NoteResult.Perfect, GetNoteResultDescription(offset));
			}
			if (-frame.GreatAfterJudgeTime <= offset && frame.GreatBeforeJudgeTime >= offset)
			{
				return (NoteResult.Great, GetNoteResultDescription(offset));
			}
			if (-frame.GoodAfterJudgeTime <= offset && frame.GoodBeforeJudgeTime >= offset)
			{
				return (NoteResult.Good, GetNoteResultDescription(offset));
			}
			if (-frame.BadAfterJudgeTime <= offset && frame.BadBeforeJudgeTime >= offset)
			{
				return (NoteResult.Bad, GetNoteResultDescription(offset));
			}
			return (NoteResult.Miss, GetNoteResultDescription(offset));
		}

		private static NoteResultDescription GetNoteResultDescription(float judgeOffsetTime)
		{
			if (-JustPerfectJudgeTimeHalf <= judgeOffsetTime && JustPerfectJudgeTimeHalf >= judgeOffsetTime)
			{
				return NoteResultDescription.Just;
			}
			return judgeOffsetTime > JustPerfectJudgeTimeHalf ? NoteResultDescription.Fast : NoteResultDescription.Late;
		}

		private static void EnsureJudgeFrames()
		{
			if (LiveMasterData.JudgeFrames == null || LiveMasterData.JudgeFrames.Count == 0)
			{
				LiveMasterData.JudgeFrames = CreateDefaultJudgeFrames();
			}

			foreach (JudgeFrameType judgeFrameType in Enum.GetValues(typeof(JudgeFrameType)))
			{
				if (judgeFrameType == JudgeFrameType.Undefined)
				{
					continue;
				}
				if (!LiveMasterData.JudgeFrames.ContainsKey(judgeFrameType))
				{
					LiveMasterData.JudgeFrames[judgeFrameType] = CloneDefaultJudgeFrame(judgeFrameType);
				}
			}

			float judgeTimeBefore = 0f;
			float judgeTimeAfter = 0f;
			foreach (MasterIngameJudgeFrame judgeFrame in LiveMasterData.JudgeFrames.Values)
			{
				if (judgeFrame == null)
				{
					continue;
				}
				judgeTimeBefore = Mathf.Max(judgeTimeBefore, judgeFrame.BadBeforeJudgeTime);
				judgeTimeAfter = Mathf.Max(judgeTimeAfter, judgeFrame.BadAfterJudgeTime);
			}

			LiveMasterData.JudgeTimeBefore = judgeTimeBefore;
			LiveMasterData.JudgeTimeAfter = judgeTimeAfter;
			LiveMasterData.JudgeTime = Mathf.Max(judgeTimeBefore, judgeTimeAfter);
		}

		private static Dictionary<JudgeFrameType, MasterIngameJudgeFrame> CreateDefaultJudgeFrames()
		{
			Dictionary<JudgeFrameType, MasterIngameJudgeFrame> frames = new Dictionary<JudgeFrameType, MasterIngameJudgeFrame>();
			AddJudgeFrame(frames, JudgeFrameType.Normal, "normal", 2.5f, 2.5f, 5f, 5f, 6.5f, 6.5f, 7.5f, 7.5f);
			AddJudgeFrame(frames, JudgeFrameType.Flick, "flick", 2.5f, 2.5f, 6.5f, 7.5f, 7f, 8f, 7.5f, 8.5f);
			AddJudgeFrame(frames, JudgeFrameType.Long, "long", 2.5f, 2.5f, 5f, 5f, 6.5f, 6.5f, 7.5f, 7.5f);
			AddJudgeFrame(frames, JudgeFrameType.Long_End, "long_end", 3.5f, 4f, 6.5f, 8f, 7.5f, 8.5f, 7.5f, 8.5f);
			AddJudgeFrame(frames, JudgeFrameType.Long_End_Flick, "long_end_flick", 3.5f, 4f, 6.5f, 8f, 7.5f, 8.5f, 7.5f, 8.5f);
			AddJudgeFrame(frames, JudgeFrameType.Normal_Critical, "normal_critical", 3.3f, 3.3f, 4.5f, 4.5f, 6.5f, 6.5f, 7.5f, 7.5f);
			AddJudgeFrame(frames, JudgeFrameType.Flick_Critical, "flick_critical", 3.5f, 3.5f, 6.5f, 7.5f, 7f, 8f, 7.5f, 8.5f);
			AddJudgeFrame(frames, JudgeFrameType.Long_Critical, "long_critical", 3.3f, 3.3f, 4.5f, 4.5f, 6.5f, 6.5f, 7.5f, 7.5f);
			AddJudgeFrame(frames, JudgeFrameType.Long_End_Critical, "long_end_critical", 3.5f, 4f, 6.5f, 8f, 7.5f, 8.5f, 7.5f, 8.5f);
			AddJudgeFrame(frames, JudgeFrameType.Long_End_Flick_Critical, "long_end_flick_critical", 3.5f, 4f, 6.5f, 8f, 7.5f, 8.5f, 7.5f, 8.5f);

			frames[JudgeFrameType.Friction] = CloneJudgeFrame(JudgeFrameType.Friction, "friction", frames[JudgeFrameType.Long]);
			frames[JudgeFrameType.Friction_Critical] = CloneJudgeFrame(JudgeFrameType.Friction_Critical, "friction_critical", frames[JudgeFrameType.Long_Critical]);
			frames[JudgeFrameType.Friction_Flick] = CloneJudgeFrame(JudgeFrameType.Friction_Flick, "friction_flick", frames[JudgeFrameType.Flick]);
			frames[JudgeFrameType.Friction_Flick_Critical] = CloneJudgeFrame(JudgeFrameType.Friction_Flick_Critical, "friction_flick_critical", frames[JudgeFrameType.Flick_Critical]);
			frames[JudgeFrameType.Friction_Long] = CloneJudgeFrame(JudgeFrameType.Friction_Long, "friction_long", frames[JudgeFrameType.Long]);
			frames[JudgeFrameType.Friction_Long_Critical] = CloneJudgeFrame(JudgeFrameType.Friction_Long_Critical, "friction_long_critical", frames[JudgeFrameType.Long_Critical]);
			frames[JudgeFrameType.Friction_Long_End] = CloneJudgeFrame(JudgeFrameType.Friction_Long_End, "friction_long_end", frames[JudgeFrameType.Long_End]);
			frames[JudgeFrameType.Friction_Long_End_Critical] = CloneJudgeFrame(JudgeFrameType.Friction_Long_End_Critical, "friction_long_end_critical", frames[JudgeFrameType.Long_End_Critical]);
			return frames;
		}

		private static void AddJudgeFrame(
			Dictionary<JudgeFrameType, MasterIngameJudgeFrame> frames,
			JudgeFrameType type,
			string name,
			float perfectBefore,
			float perfectAfter,
			float greatBefore,
			float greatAfter,
			float goodBefore,
			float goodAfter,
			float badBefore,
			float badAfter)
		{
			frames[type] = new MasterIngameJudgeFrame(type, name, perfectBefore, perfectAfter, greatBefore, greatAfter, goodBefore, goodAfter, badBefore, badAfter);
		}

		private static MasterIngameJudgeFrame CloneDefaultJudgeFrame(JudgeFrameType type)
		{
			return CloneJudgeFrame(type, type.ToString().ToLowerInvariant(), CreateDefaultJudgeFrames()[JudgeFrameType.Normal]);
		}

		private static MasterIngameJudgeFrame CloneJudgeFrame(JudgeFrameType type, string name, MasterIngameJudgeFrame source)
		{
			return new MasterIngameJudgeFrame(
				type,
				name,
				source.perfectBefore,
				source.perfectAfter,
				source.greatBefore,
				source.greatAfter,
				source.goodBefore,
				source.goodAfter,
				source.badBefore,
				source.badAfter);
		}
	}
}
