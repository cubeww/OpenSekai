using System;
using System.Collections.Generic;
using Sekai.Live;
using Sekai.MusicScoreMaker.Ingame.Models;
using UnityEngine;

namespace Sekai
{
	public static class LiveUtility
	{
		public const float NoteEndIgnoreFrameTime = 1f / 6f;

		public const int LaneCount = 11;

		public const float OneFrameTime = 1f / 60f;

		public const float HalfFrameTime = 1f / 120f;

		public const float JudgmentLifeTime = 0.3f;

		public const float JudgmentScaleCoefficient = 20f;

		public static readonly Vector2 Vector2Zero;

		public static readonly Vector3 Vector3Zero;

		public static readonly Vector3 Vector3One;

		public static readonly Vector3 Vector3OneMinusX;

		public static readonly Vector2 Vector2Left;

		public static readonly Vector2 Vector2Right;

		public static readonly Vector3 FlickLeftEulerAngles;

		public static readonly Vector3 FlickRightEulerAngles;

		private static readonly float NoteViewProgressStartValue;

		private static int baseOrderInLayer;

		private static int progressOrderInLayer;

		public static float ScreenDpiToInch(float distance)
		{
			return Screen.dpi / 2.54f * distance;
		}

		public static bool IsNotPublishMusicError(APIResult result)
		{
			return result?.ClientErrorInfo?.Response?.ErrorCode == "not_publish_music";
		}

		public static bool IsCustomMusicScoreUnavailableError(APIResult result)
		{
			return result?.ClientErrorInfo?.Response?.ErrorCode == "custom_music_score_unavailable";
		}

		public static void OpenNotPublishMusicErrorDialog(Action onNotPublish)
		{
			// TODO(original): restore original dialog flow when the live API screens are imported.
			onNotPublish?.Invoke();
		}

		public static void OpenStartLiveAPIErrorRetryCancelDialog(Action onRetry, Action onCancel)
		{
			// TODO(original): restore original retry/cancel dialog once common dialog wiring is complete.
			onCancel?.Invoke();
		}

		public static void OpenStartLiveAPIErrorCancelDialog(Action callback)
		{
			// TODO(original): restore original API error dialog once common dialog wiring is complete.
			callback?.Invoke();
		}

		public static MusicCategory GetMusicCategoryByLiveMode(LiveSettingData.LiveModeType liveMode)
		{
			return liveMode switch
			{
				LiveSettingData.LiveModeType.Mode2D => MusicCategory.mv_2d,
				LiveSettingData.LiveModeType.OriginalMV => MusicCategory.original,
				LiveSettingData.LiveModeType.Low => MusicCategory.image,
				_ => MusicCategory.mv
			};
		}

		public static Queue<CutinData> GetValidateCutinQueue(List<CutinData> data)
		{
			return new Queue<CutinData>(data ?? new List<CutinData>());
		}

		public static JudgeFrameType GetINoteToJudgeFrameType(INote note)
		{
			if (note == null)
			{
				throw new ArgumentNullException(nameof(note));
			}

			var hasParent = note.ParentNote != null;
			var isCritical = note.Type == NoteType.Critical;

			switch (note.Category)
			{
				case NoteCategory.Normal:
					if (hasParent)
					{
						return isCritical ? JudgeFrameType.Long_End_Critical : JudgeFrameType.Long_End;
					}

					return isCritical ? JudgeFrameType.Normal_Critical : JudgeFrameType.Normal;
				case NoteCategory.Long:
					if (hasParent)
					{
						return isCritical ? JudgeFrameType.Long_End_Critical : JudgeFrameType.Long_End;
					}

					return isCritical ? JudgeFrameType.Long_Critical : JudgeFrameType.Long;
				case NoteCategory.Flick:
					if (hasParent)
					{
						return isCritical ? JudgeFrameType.Long_End_Flick_Critical : JudgeFrameType.Long_End_Flick;
					}

					return isCritical ? JudgeFrameType.Flick_Critical : JudgeFrameType.Flick;
				case NoteCategory.Friction:
				case NoteCategory.FrictionHide:
					if (hasParent)
					{
						return isCritical ? JudgeFrameType.Friction_Long_End_Critical : JudgeFrameType.Friction_Long_End;
					}

					return isCritical ? JudgeFrameType.Friction_Critical : JudgeFrameType.Friction;
				case NoteCategory.FrictionLong:
				case NoteCategory.FrictionHideLong:
					if (hasParent)
					{
						return isCritical ? JudgeFrameType.Friction_Long_End_Critical : JudgeFrameType.Friction_Long_End;
					}

					return isCritical ? JudgeFrameType.Friction_Long_Critical : JudgeFrameType.Friction_Long;
				case NoteCategory.FrictionFlick:
					if (hasParent)
					{
						return isCritical ? JudgeFrameType.Long_End_Flick_Critical : JudgeFrameType.Long_End_Flick;
					}

					return isCritical ? JudgeFrameType.Friction_Flick_Critical : JudgeFrameType.Friction_Flick;
				default:
					return JudgeFrameType.Normal;
			}
		}

		public static bool IsJudgmentTiming(JudgeFrameType frameType, float offsetJudgeTime)
		{
			if (!LiveConfig.noteTypeJudgeData.JudgeTimes.TryGetValue(frameType, out var judgeTimeData))
			{
				judgeTimeData = LiveConfig.noteTypeJudgeData.JudgeTimes[JudgeFrameType.Normal];
			}

			return judgeTimeData.BadBeforeJudgeTime > offsetJudgeTime && -judgeTimeData.BadAfterJudgeTime < offsetJudgeTime;
		}

		public static bool GreaterEqual(ref MusicScoreInfo a, ref MusicScoreInfo b)
		{
			return a.bar < b.bar || a.bar == b.bar && a.barProgress <= b.barProgress;
		}

		public static bool GreaterEqual(int bar, float barProgress, ref MusicScoreInfo b)
		{
			return b.bar > bar || b.bar == bar && b.barProgress >= barProgress;
		}

		public static Vector2 EarlyVec2Lerp(Vector2 a, Vector2 b, float t)
		{
			return a + (b - a) * Mathf.Max(t, 0f);
		}

		public static float CalcNoteShowRate(float settingsNoteShowRate)
		{
			return Mathf.Clamp01(settingsNoteShowRate);
		}

		public static float CalcNoteShowRate(Vector2 position)
		{
			var firstJudgmentPosition = LiveConfig.JudgmentPositions.Length > 0 ? LiveConfig.JudgmentPositions[0] : Vector2.zero;
			var totalDistance = Vector2.Distance(LiveConfig.SpawnPosition, firstJudgmentPosition);
			if (Mathf.Approximately(totalDistance, 0f))
			{
				return 1f;
			}

			return Mathf.Clamp01(1f - Vector2.Distance(position, firstJudgmentPosition) / totalDistance);
		}

		public static Vector2 CalcNoteShowRatePosition(float settingsNoteShowRate)
		{
			var judgmentPosition = LiveConfig.JudgmentPositions.Length > 0 ? LiveConfig.JudgmentPositions[0] : Vector2.zero;
			return CalcNoteShowRatePosition(settingsNoteShowRate, LiveConfig.SpawnPosition, judgmentPosition);
		}

		public static Vector2 CalcNoteShowRatePosition(float settingsNoteShowRate, Vector2 spawnPosition, Vector2 judgmentPosition)
		{
			var noteViewStartPosition = EarlyVec2Lerp(spawnPosition, judgmentPosition, NoteViewProgressStartValue);
			return EarlyVec2Lerp(noteViewStartPosition, judgmentPosition, 1f - settingsNoteShowRate);
		}

		public static float CalcNoteShowRateProgress(float showRate)
		{
			var progress = LiveConfig.GetNoteViewProgress(showRate);
			return progress - (1f - showRate) * (1f - showRate) * NoteViewProgressStartValue;
		}

		public static int CalcNoteSortingOrder(float progress)
		{
			var value = progressOrderInLayer * progress + baseOrderInLayer;
			return float.IsInfinity(value) ? int.MinValue : (int)value;
		}

		public static float GetLaneOffset(NoteCategory category, LiveBundleBuildData bundleBuildData)
		{
			if (bundleBuildData == null)
			{
				return 0f;
			}

			return category switch
			{
				NoteCategory.Flick or NoteCategory.FrictionFlick => bundleBuildData.FlickNoteOffsetX,
				NoteCategory.Long or NoteCategory.FrictionLong or NoteCategory.FrictionHideLong => bundleBuildData.LongNoteOffsetX,
				_ => bundleBuildData.NormalNoteOffsetX
			};
		}

		public static Dictionary<(NoteCategory, NoteType), int> CalculateMaxSimultaneousNotes(MusicScore musicScore, float baseDisplayTime)
		{
			var displayTimes = new Dictionary<(NoteCategory, NoteType), List<(float, float)>>();
			if (musicScore?.NoteArray != null)
			{
				CalculateAllNoteDisplayTime(musicScore.NoteArray, musicScore.musicScoreInfoArray, baseDisplayTime, ref displayTimes);
			}

			var result = new Dictionary<(NoteCategory, NoteType), int>();
			foreach (var pair in displayTimes)
			{
				result[pair.Key] = CalculateMaxSimultaneousNotes(pair.Value);
			}

			return result;
		}

		private static void CalculateAllNoteDisplayTime(NoteBase[] noteArray, MusicScoreInfo[] musicScoreInfoArray, float baseDisplayTime, ref Dictionary<(NoteCategory, NoteType), List<(float, float)>> allNoteDisplayTime)
		{
			if (noteArray == null)
			{
				return;
			}

			foreach (var note in noteArray)
			{
				CalculateChilledNoteDisplayTime(note, musicScoreInfoArray, baseDisplayTime, ref allNoteDisplayTime);
			}
		}

		private static void CalculateChilledNoteDisplayTime(NoteBase note, MusicScoreInfo[] musicScoreInfoArray, float baseDisplayTime, ref Dictionary<(NoteCategory, NoteType), List<(float, float)>> allNoteDisplayTime)
		{
			if (note == null)
			{
				return;
			}

			var key = (note.Category, note.Type);
			if (!allNoteDisplayTime.TryGetValue(key, out var ranges))
			{
				ranges = new List<(float, float)>();
				allNoteDisplayTime[key] = ranges;
			}

			var displayTime = CalculateNoteDisplayTime(note, musicScoreInfoArray, baseDisplayTime);
			ranges.Add((note.MusicScoreInfo.time - displayTime, note.MusicScoreInfo.time + NoteEndIgnoreFrameTime));
		}

		private static float CalculateNoteDisplayTime(NoteBase note, MusicScoreInfo[] musicScoreInfoArray, float baseDisplayTime)
		{
			var noteSpeedRatio = Mathf.Max(note?.speedRatio ?? 1f, 0.01f);
			return SafeCalcDisplayTime(baseDisplayTime, 1f, noteSpeedRatio);
		}

		private static float SafeCalcDisplayTime(float useTime, float eventSpeedRatio, float noteSpeedRatio)
		{
			var speed = eventSpeedRatio * noteSpeedRatio;
			return Mathf.Approximately(speed, 0f) ? useTime : useTime / speed;
		}

		private static int CalculateMaxSimultaneousNotes(List<(float, float)> noteTypeDisplayTimes)
		{
			var max = 0;
			if (noteTypeDisplayTimes == null)
			{
				return max;
			}

			for (var i = 0; i < noteTypeDisplayTimes.Count; i++)
			{
				var count = 0;
				for (var j = 0; j < noteTypeDisplayTimes.Count; j++)
				{
					if (IsOverlapping(noteTypeDisplayTimes[i], noteTypeDisplayTimes[j]))
					{
						count++;
					}
				}

				max = Math.Max(max, count);
			}

			return max;
		}

		private static bool IsOverlapping((float, float) note1, (float, float) note2)
		{
			return note1.Item1 <= note2.Item2 && note2.Item1 <= note1.Item2;
		}

		public static float CalcLaneTransformX(int lane)
		{
			return lane * 0.84f - 4.62f;
		}

		public static float CalcLaneTransformX(float lane)
		{
			return lane * 0.84f - 4.62f;
		}

		public static MusicScore ConvertToMusicScore(MusicScoreMakerData musicScoreMakerData)
		{
			// TODO(original): restore MusicScoreMakerData -> MusicScore conversion after the full editor data model is copied.
			return new MusicScore(Array.Empty<MusicScoreInfo>(), Array.Empty<EventBase>(), Array.Empty<NoteBase>());
		}

		public static void CalcExcuteNoteLane(NoteBase noteBase, ref INote currentNote, ref INote nextNote, ref MusicScoreInfo currentFrameInfo, MusicScoreInfo MusicScoreInfo, INote childNote)
		{
			if (noteBase == null || currentNote == null || nextNote == null)
			{
				return;
			}

			var progress = Mathf.Clamp01(LiveConfig.GetNoteLineParentProgress(currentFrameInfo.time, currentNote, nextNote));
			noteBase.LaneStartF = Mathf.Lerp(currentNote.DefaultLeftLane, nextNote.DefaultLeftLane, progress);
			noteBase.LaneEndF = Mathf.Lerp(currentNote.DefaultRightLane, nextNote.DefaultRightLane, progress);

			if (childNote == null)
			{
				return;
			}

			var elapsedAfterEnd = Mathf.Max(currentFrameInfo.time - childNote.MusicScoreInfo.time, 0f);
			var elapsedFromStart = currentFrameInfo.time - MusicScoreInfo.time;
			noteBase.OffsetJudgeTime = Mathf.Min(elapsedAfterEnd, elapsedFromStart);
		}

		public static Vector2 CalcSpriteRendererSize(float size)
		{
			return new Vector2(size * 1.2178f + 2.06f, 1.7222f);
		}

		static LiveUtility()
		{
			Vector2Zero = Vector2.zero;
			Vector3Zero = Vector3.zero;
			Vector3One = Vector3.one;
			Vector3OneMinusX = new Vector3(-1f, 1f, 1f);
			Vector2Left = Vector2.left;
			Vector2Right = Vector2.right;
			FlickLeftEulerAngles = new Vector3(0f, 0f, 30f);
			FlickRightEulerAngles = new Vector3(0f, 0f, -30f);
			NoteViewProgressStartValue = 0.07265f;
			baseOrderInLayer = 30;
			progressOrderInLayer = 100;
		}
	}
}
