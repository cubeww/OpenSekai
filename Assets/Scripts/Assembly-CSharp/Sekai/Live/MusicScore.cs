using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Sekai.Live
{
	public class MusicScore
	{
		public static MusicScoreInfo CurrentFrameInfo;

		public MusicScoreInfo[] musicScoreInfoArray
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			private set;
		}

		public EventBase[] EventArray
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			private set;
		}

		public float? FilterBeforeTimeSec
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			set;
		}

		public NoteBase[] NoteArray
		{
			[CompilerGenerated]
			get;
			[CompilerGenerated]
			private set;
		}

		public MusicScore()
		{
			CurrentFrameInfo.speedRatio = 1f;
		}

		public MusicScore(MusicScoreInfo[] musicScoreInfoArray, EventBase[] eventArray, NoteBase[] noteArray)
			: this()
		{
			this.musicScoreInfoArray = musicScoreInfoArray;
			EventArray = eventArray;
			NoteArray = noteArray;
		}

		public void SetMusicScoreInfo(MusicScoreInfo[] musicScoreInfo)
		{
			musicScoreInfoArray = musicScoreInfo;
		}

		public void AddEventArray(EventBase[] eventArray)
		{
			EventArray = eventArray;
		}

		public void AddNoteArray(NoteBase[] noteArray)
		{
			NoteArray = noteArray;
		}

		public float GetTimeSignature(int barIndex)
		{
			if (musicScoreInfoArray == null)
			{
				return 4f;
			}

			float timeSignature = 4f;
			foreach (MusicScoreInfo info in musicScoreInfoArray)
			{
				if (info.bar > barIndex)
				{
					break;
				}

				timeSignature = info.timeSignature;
			}

			return timeSignature;
		}

		public void Update(float time)
		{
			GetMusicScoreInfoFromTime(time, out CurrentFrameInfo);
		}

		public float GetTimeFromBar(int bar, float barProgress)
		{
			if (musicScoreInfoArray == null || musicScoreInfoArray.Length == 0)
			{
				return 0f;
			}

			int index = FindScoreInfoIndexForBar(bar, barProgress, musicScoreInfoArray);
			MusicScoreInfo info = musicScoreInfoArray[index];
			float barDuration = GetBarDuration(info);
			return info.time + barDuration * (bar - info.bar) + barDuration * (barProgress - info.barProgress);
		}

		public int GetCurrentBar(float currentTimeMs)
		{
			if (musicScoreInfoArray == null || musicScoreInfoArray.Length == 0)
			{
				return 0;
			}

			int index = FindScoreInfoIndexForTime(currentTimeMs);
			MusicScoreInfo info = musicScoreInfoArray[index];
			float elapsedSec = (currentTimeMs - info.time) / 1000f;
			float barDuration = GetBarDuration(info);
			int elapsedBars = barDuration <= 0f ? 0 : (int)Math.Floor(elapsedSec / barDuration);
			return info.bar + elapsedBars;
		}

		public static MusicScoreInfo GenerateNoteMusicScoreInfo(int bar, float barProgress, MusicScoreInfo[] musicScoreInfos)
		{
			if (musicScoreInfos == null || musicScoreInfos.Length == 0)
			{
				return new MusicScoreInfo(bar, barProgress, 0f, 120f, 4f, 1f, 1f);
			}

			int index = FindScoreInfoIndexForBar(bar, barProgress, musicScoreInfos);
			MusicScoreInfo info = musicScoreInfos[index];
			float barDuration = GetBarDuration(info);
			float time = info.time + barDuration * (bar - info.bar) + barDuration * (barProgress - info.barProgress);
			return new MusicScoreInfo(bar, barProgress, time, info.bpm, info.timeSignature, info.speedRatio, info.seVolume);
		}

		public void GetMusicScoreInfoFromTime(float time, out MusicScoreInfo scoreInfo)
		{
			if (musicScoreInfoArray == null || musicScoreInfoArray.Length == 0)
			{
				scoreInfo = new MusicScoreInfo(0, 0f, time, 120f, 4f, 1f, 1f);
				return;
			}

			int index = FindScoreInfoIndexForTime(time);
			GenerateScoreInfo(out scoreInfo, ref index, ref time);
		}

		private void GenerateScoreInfo(out MusicScoreInfo scoreInfo, ref int index, ref float time)
		{
			MusicScoreInfo info = musicScoreInfoArray[index];
			float barDuration = GetBarDuration(info);
			float progress = barDuration <= 0f ? 0f : info.barProgress + (time - info.time) / barDuration;
			int barOffset = (int)Math.Floor(progress);

			scoreInfo = new MusicScoreInfo(
				info.bar + barOffset,
				progress - barOffset,
				time,
				info.bpm,
				info.timeSignature,
				info.speedRatio,
				info.seVolume);
		}

		public void ResetNote()
		{
			if (NoteArray == null)
			{
				return;
			}

			foreach (NoteBase note in NoteArray)
			{
				if (note == null)
				{
					continue;
				}

				note.ResetNote();
				if (note.NoteList == null)
				{
					continue;
				}

				foreach (NoteBase child in note.NoteList)
				{
					child?.ResetNote();
				}
			}
		}

		public void InjectSkillFeverForCreatorScore(int musicId)
		{
			// TODO(original): restore creator-score skill/fever injection once MusicScoreFactory/SUS conversion is fully recovered.
			ResetEvent();
		}

		public void ResetEvent()
		{
			if (EventArray == null)
			{
				return;
			}

			foreach (EventBase eventBase in EventArray)
			{
				eventBase?.ResetEvent();
			}
		}

		public List<string> CompareWith(MusicScore other, float tolerance = 0.001f)
		{
			List<string> differences = new List<string>();
			if (other == null)
			{
				differences.Add("other is null");
				return differences;
			}

			int thisInfoCount = musicScoreInfoArray?.Length ?? 0;
			int otherInfoCount = other.musicScoreInfoArray?.Length ?? 0;
			if (thisInfoCount != otherInfoCount)
			{
				differences.Add($"musicScoreInfoArray length: {thisInfoCount} != {otherInfoCount}");
			}

			int compareInfoCount = Math.Min(thisInfoCount, otherInfoCount);
			for (int i = 0; i < compareInfoCount; i++)
			{
				differences.AddRange(CompareMusicScoreInfo(musicScoreInfoArray[i], other.musicScoreInfoArray[i], tolerance, $"musicScoreInfoArray[{i}]"));
			}

			int thisEventCount = EventArray?.Length ?? 0;
			int otherEventCount = other.EventArray?.Length ?? 0;
			if (thisEventCount != otherEventCount)
			{
				differences.Add($"EventArray length: {thisEventCount} != {otherEventCount}");
			}

			int thisNoteCount = NoteArray?.Length ?? 0;
			int otherNoteCount = other.NoteArray?.Length ?? 0;
			if (thisNoteCount != otherNoteCount)
			{
				differences.Add($"NoteArray length: {thisNoteCount} != {otherNoteCount}");
			}

			return differences;
		}

		private List<string> CompareMusicScoreInfo(MusicScoreInfo thisInfo, MusicScoreInfo otherInfo, float tolerance, string prefix)
		{
			List<string> differences = new List<string>();
			if (thisInfo.bar != otherInfo.bar)
			{
				differences.Add($"{prefix}.bar: {thisInfo.bar} != {otherInfo.bar}");
			}

			AddFloatDifference(differences, thisInfo.barProgress, otherInfo.barProgress, tolerance, $"{prefix}.barProgress");
			AddFloatDifference(differences, thisInfo.time, otherInfo.time, tolerance, $"{prefix}.time");
			AddFloatDifference(differences, thisInfo.bpm, otherInfo.bpm, tolerance, $"{prefix}.bpm");
			AddFloatDifference(differences, thisInfo.timeSignature, otherInfo.timeSignature, tolerance, $"{prefix}.timeSignature");
			AddFloatDifference(differences, thisInfo.speedRatio, otherInfo.speedRatio, tolerance, $"{prefix}.speedRatio");
			AddFloatDifference(differences, thisInfo.seVolume, otherInfo.seVolume, tolerance, $"{prefix}.seVolume");
			return differences;
		}

		private static int FindScoreInfoIndexForBar(int bar, float barProgress, MusicScoreInfo[] infos)
		{
			for (int i = 0; i < infos.Length; i++)
			{
				if (i + 1 == infos.Length || IsGreaterOrEqual(bar, barProgress, infos[i + 1]))
				{
					return i;
				}
			}

			return 0;
		}

		private int FindScoreInfoIndexForTime(float time)
		{
			for (int i = musicScoreInfoArray.Length - 1; i >= 0; i--)
			{
				if (musicScoreInfoArray[i].time <= time)
				{
					return i;
				}
			}

			return 0;
		}

		private static bool IsGreaterOrEqual(int bar, float barProgress, MusicScoreInfo info)
		{
			return info.bar > bar || (info.bar == bar && info.barProgress >= barProgress);
		}

		private static float GetBarDuration(MusicScoreInfo info)
		{
			if (info.bpm == 0f)
			{
				return 0f;
			}

			return info.timeSignature * 60f / info.bpm;
		}

		private static void AddFloatDifference(List<string> differences, float left, float right, float tolerance, string prefix)
		{
			if (Math.Abs(left - right) > tolerance)
			{
				differences.Add($"{prefix}: {left} != {right}");
			}
		}
	}
}
