using UnityEngine;

namespace Sekai.Live
{
	public static class LiveConfig
	{
		public struct MasterParamater
		{
			public int MissMesh;

			public float JudgeTimeBefore;

			public float JudgeTimeAfter;

			public float JudgeTime;
		}

		public const float DefaultNoteSpeed = 7f;

		public static readonly string ConfigBundleNamePath = "Live/Config/LiveBundleBuildData";

		public static int Life = 1000;

		public static float JudgeTime = 0.1f;

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
			return -LiveMasterData.JudgeTimeAfter <= offsetTime && offsetTime <= LiveMasterData.JudgeTimeBefore;
		}
	}
}
