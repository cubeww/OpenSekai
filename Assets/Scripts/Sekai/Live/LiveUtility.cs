using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sekai.Live
{
	public static class LiveUtility
	{
		public const float NoteEndIgnoreFrameTime = 1f / 6f;

		public const float OneFrameTime = 1f / 60f;

		public static readonly Vector2 Vector2Zero = Vector2.zero;

		public static readonly Vector3 Vector3Zero = Vector3.zero;

		public static readonly Vector3 Vector3One = Vector3.one;

		public static readonly Vector3 Vector3OneMinusX = new Vector3(-1f, 1f, 1f);

		public static readonly Vector2 Vector2Left = new Vector2(-1f, 1f);

		public static readonly Vector2 Vector2Right = Vector2.one;

		public static readonly Vector3 FlickLeftEulerAngles = new Vector3(0f, 0f, 30f);

		public static readonly Vector3 FlickRightEulerAngles = new Vector3(0f, 0f, -30f);

		public static float GetLaneOffset(NoteCategory category, LiveBundleBuildData bundleBuildData)
		{
			if (bundleBuildData == null)
			{
				switch (category)
				{
					case NoteCategory.Normal:
					case NoteCategory.Friction:
					case NoteCategory.FrictionHide:
						return 1.25f;
					case NoteCategory.Guide:
					case NoteCategory.GuideEnd:
					case NoteCategory.GuideHidden:
						return 0f;
					default:
						return 1.5f;
				}
			}

			switch (category)
			{
				case NoteCategory.Normal:
				case NoteCategory.Friction:
				case NoteCategory.FrictionHide:
					return bundleBuildData.NormalNoteOffsetX;
				case NoteCategory.Long:
				case NoteCategory.Connection:
				case NoteCategory.FrictionLong:
				case NoteCategory.FrictionHideLong:
				case NoteCategory.Combo:
				case NoteCategory.Hidden:
				case NoteCategory.Skip:
				case NoteCategory.Error:
					return bundleBuildData.LongNoteOffsetX;
				case NoteCategory.Flick:
				case NoteCategory.FrictionFlick:
					return bundleBuildData.FlickNoteOffsetX;
				case NoteCategory.Guide:
				case NoteCategory.GuideEnd:
				case NoteCategory.GuideHidden:
					return 0f;
				default:
					throw new ArgumentOutOfRangeException("category", category, null);
			}
		}

		public static float ScreenDpiToInch(float distance)
		{
			float dpi = Screen.dpi;
			float pixelPerCentimeter = dpi == 0f ? 118.11f : dpi / 2.54f;
			return pixelPerCentimeter * distance;
		}

		public static Vector2 EarlyVec2Lerp(Vector2 a, Vector2 b, float t)
		{
			return a + (b - a) * Mathf.Max(t, 0f);
		}

		public static Vector2 CalcSpriteRendererSize(float size)
		{
			return new Vector2(size * 1.2178f + 2.06f, 1.7222f);
		}

		public static void CalcExcuteNoteLane(NoteBase noteBase, ref INote currentNote, ref INote nextNote, ref MusicScoreInfo currentFrameInfo, MusicScoreInfo musicScoreInfo, INote childNote)
		{
			if (noteBase == null || currentNote == null || nextNote == null || childNote == null)
			{
				return;
			}

			float progress = LiveConfig.GetNoteLineParentProgress(currentFrameInfo.time, currentNote, nextNote);
			noteBase.LaneStartF = Mathf.Lerp(currentNote.DefaultLeftLane, nextNote.DefaultLeftLane, progress);
			noteBase.LaneEndF = Mathf.Lerp(currentNote.DefaultRightLane, nextNote.DefaultRightLane, progress);
			float childTime = childNote.MusicScoreInfo.time;
			noteBase.OffsetJudgeTime = Mathf.Min(currentFrameInfo.time - musicScoreInfo.time, Mathf.Max(currentFrameInfo.time - childTime, 0f));
		}

		public static Dictionary<(NoteCategory, NoteType), int> CalculateMaxSimultaneousNotes(MusicScore musicScore, float baseDisplayTime)
		{
			Dictionary<(NoteCategory, NoteType), List<(float, float)>> allNoteDisplayTime = new Dictionary<(NoteCategory, NoteType), List<(float, float)>>();
			Dictionary<(NoteCategory, NoteType), int> maxSimultaneousNotes = CreateNoteCountDictionary();
			if (musicScore == null || musicScore.NoteArray == null || musicScore.musicScoreInfoArray == null)
			{
				return maxSimultaneousNotes;
			}

			(NoteCategory, NoteType)[] keys = GetViewNoteCountKeys();
			for (int i = 0; i < keys.Length; i++)
			{
				allNoteDisplayTime[keys[i]] = new List<(float, float)>();
				maxSimultaneousNotes[keys[i]] = 0;
			}

			CalculateAllNoteDisplayTime(musicScore.NoteArray, musicScore.musicScoreInfoArray, baseDisplayTime, ref allNoteDisplayTime);
			for (int i = 0; i < keys.Length; i++)
			{
				maxSimultaneousNotes[keys[i]] = CalculateMaxSimultaneousNotes(allNoteDisplayTime[keys[i]]);
			}

			return maxSimultaneousNotes;
		}

		private static void CalculateAllNoteDisplayTime(NoteBase[] noteArray, MusicScoreInfo[] musicScoreInfoArray, float baseDisplayTime, ref Dictionary<(NoteCategory, NoteType), List<(float, float)>> allNoteDisplayTime)
		{
			for (int i = 0; i < noteArray.Length; i++)
			{
				NoteBase note = noteArray[i];
				if (note == null || IsNoViewCategory(note.Category))
				{
					continue;
				}

				float displayTime = CalculateNoteDisplayTime(note, musicScoreInfoArray, baseDisplayTime);
				(NoteCategory, NoteType) key = (NormalizeViewCategory(note.Category), note.Type);
				EnsureDisplayTimeList(key, ref allNoteDisplayTime);
				if (note.NoteList == null || note.NoteList.Count < 2)
				{
					float startTime = note.MusicScoreInfo.time;
					allNoteDisplayTime[key].Add((startTime, startTime + displayTime));
					continue;
				}

				(float, float) lastDisplayTime = default;
				for (int j = 1; j < note.NoteList.Count; j++)
				{
					NoteBase childNote = note.NoteList[j];
					if (childNote == null || IsNoViewCategory(childNote.Category))
					{
						continue;
					}

					CalculateChildNoteDisplayTime(childNote, musicScoreInfoArray, baseDisplayTime, ref allNoteDisplayTime);
					(NoteCategory, NoteType) childKey = (NormalizeViewCategory(childNote.Category), childNote.Type);
					if (!allNoteDisplayTime.TryGetValue(childKey, out List<(float, float)> childTimes) || childTimes.Count == 0)
					{
						continue;
					}
					lastDisplayTime = childTimes[childTimes.Count - 1];
				}

				float endDisplayTime = lastDisplayTime.Item2 > 0f
					? lastDisplayTime.Item2
					: note.MusicScoreInfo.time + displayTime;
				allNoteDisplayTime[key].Add((note.MusicScoreInfo.time, endDisplayTime));
			}
		}

		private static void CalculateChildNoteDisplayTime(NoteBase note, MusicScoreInfo[] musicScoreInfoArray, float baseDisplayTime, ref Dictionary<(NoteCategory, NoteType), List<(float, float)>> allNoteDisplayTime)
		{
			if (note == null || IsNoViewCategory(note.Category))
			{
				return;
			}

			float displayTime = CalculateNoteDisplayTime(note, musicScoreInfoArray, baseDisplayTime);
			(NoteCategory, NoteType) key = (NormalizeViewCategory(note.Category), note.Type);
			EnsureDisplayTimeList(key, ref allNoteDisplayTime);
			float startTime = note.MusicScoreInfo.time;
			allNoteDisplayTime[key].Add((startTime, startTime + displayTime));
		}

		private static float CalculateNoteDisplayTime(NoteBase note, MusicScoreInfo[] musicScoreInfoArray, float baseDisplayTime)
		{
			if (note == null || musicScoreInfoArray == null || musicScoreInfoArray.Length == 0)
			{
				return 0.25f;
			}

			float noteTime = note.MusicScoreInfo.time;
			int eventIndex = -1;
			for (int i = musicScoreInfoArray.Length - 1; i >= 0; i--)
			{
				if (musicScoreInfoArray[i].time < noteTime)
				{
					eventIndex = i;
					break;
				}
			}

			if (eventIndex < 0)
			{
				return 0.25f;
			}

			float consumedTime = 0f;
			float displayTime = 0f;
			float currentTime = noteTime;
			for (int i = eventIndex; i >= 0 && consumedTime < baseDisplayTime; i--)
			{
				float segmentTime = currentTime - musicScoreInfoArray[i].time;
				float useTime = Mathf.Min(segmentTime, baseDisplayTime - consumedTime);
				displayTime += SafeCalcDisplayTime(useTime, musicScoreInfoArray[i].speedRatio, note.speedRatio);
				consumedTime += useTime;
				currentTime = musicScoreInfoArray[i].time;
			}

			if (consumedTime < baseDisplayTime)
			{
				displayTime += SafeCalcDisplayTime(baseDisplayTime - consumedTime, musicScoreInfoArray[0].speedRatio, note.speedRatio);
			}

			return displayTime * 1.2f + 0.25f;
		}

		private static float SafeCalcDisplayTime(float useTime, float eventSpeedRatio, float noteSpeedRatio)
		{
			return useTime / Mathf.Min(eventSpeedRatio * noteSpeedRatio, 100f);
		}

		private static int CalculateMaxSimultaneousNotes(List<(float start, float end)> noteTypeDisplayTimes)
		{
			int maxCount = 0;
			for (int i = 0; i < noteTypeDisplayTimes.Count; i++)
			{
				int count = 0;
				for (int j = 0; j < noteTypeDisplayTimes.Count; j++)
				{
					if (noteTypeDisplayTimes[i].start <= noteTypeDisplayTimes[j].end && noteTypeDisplayTimes[j].start <= noteTypeDisplayTimes[i].end)
					{
						count++;
					}
				}

				maxCount = Mathf.Max(maxCount, count);
			}

			return maxCount;
		}

		private static Dictionary<(NoteCategory, NoteType), int> CreateNoteCountDictionary()
		{
			Dictionary<(NoteCategory, NoteType), int> result = new Dictionary<(NoteCategory, NoteType), int>();
			(NoteCategory, NoteType)[] keys = GetViewNoteCountKeys();
			for (int i = 0; i < keys.Length; i++)
			{
				result[keys[i]] = 0;
			}

			return result;
		}

		private static (NoteCategory, NoteType)[] GetViewNoteCountKeys()
		{
			return new (NoteCategory, NoteType)[]
			{
				(NoteCategory.Normal, NoteType.Default),
				(NoteCategory.Normal, NoteType.Critical),
				(NoteCategory.Long, NoteType.Default),
				(NoteCategory.Long, NoteType.Critical),
				(NoteCategory.Connection, NoteType.Default),
				(NoteCategory.Connection, NoteType.Critical),
				(NoteCategory.Flick, NoteType.Default),
				(NoteCategory.Flick, NoteType.Critical),
				(NoteCategory.Friction, NoteType.Default),
				(NoteCategory.Friction, NoteType.Critical),
				(NoteCategory.FrictionLong, NoteType.Default),
				(NoteCategory.FrictionLong, NoteType.Critical),
				(NoteCategory.FrictionFlick, NoteType.Default),
				(NoteCategory.FrictionFlick, NoteType.Critical)
			};
		}

		private static void EnsureDisplayTimeList((NoteCategory, NoteType) key, ref Dictionary<(NoteCategory, NoteType), List<(float, float)>> allNoteDisplayTime)
		{
			if (!allNoteDisplayTime.ContainsKey(key))
			{
				allNoteDisplayTime[key] = new List<(float, float)>();
			}
		}

		private static bool IsNoViewCategory(NoteCategory category)
		{
			return category == NoteCategory.Guide ||
				category == NoteCategory.GuideEnd ||
				category == NoteCategory.GuideHidden ||
				category == NoteCategory.Combo ||
				category == NoteCategory.Skip ||
				category == NoteCategory.Error;
		}

		public static NoteCategory NormalizeViewCategory(NoteCategory category)
		{
			switch (category)
			{
				case NoteCategory.FrictionHide:
					return NoteCategory.Friction;
				case NoteCategory.FrictionHideLong:
					return NoteCategory.FrictionLong;
				case NoteCategory.Hidden:
					return NoteCategory.Connection;
				default:
					return category;
			}
		}
	}
}
