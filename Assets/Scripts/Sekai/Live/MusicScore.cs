using UnityEngine;

namespace Sekai.Live
{
	public class MusicScore
	{
		private const string NoBpmEventWarning = "BPM event is not registered.";

		private const string MusicScoreInfoArrayNotFound = "musicScoreInfoArray not found.";

		public static MusicScoreInfo CurrentFrameInfo;

		public MusicScoreInfo[] musicScoreInfoArray { get; private set; }

		public EventBase[] EventArray { get; private set; }

		public NoteBase[] NoteArray { get; private set; }

		public MusicScore()
		{
			CurrentFrameInfo.speedRatio = 1f;
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
			MusicScoreInfo[] array = musicScoreInfoArray;
			if (array == null)
			{
				return 4f;
			}

			float timeSignature = 4f;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].bar > barIndex)
				{
					return timeSignature;
				}
				timeSignature = array[i].timeSignature;
			}
			return timeSignature;
		}

		public void Update(float time)
		{
			GetMusicScoreInfoFromTime(time, out CurrentFrameInfo);
		}

		public float GetTimeFromBar(int bar, float barProgress)
		{
			MusicScoreInfo[] array = musicScoreInfoArray;
			if (array == null)
			{
				Debug.LogError(MusicScoreInfoArrayNotFound);
				return 0f;
			}
			if (array.Length == 0)
			{
				Debug.LogWarning(NoBpmEventWarning);
				return 0f;
			}

			int index = FindMusicScoreInfoIndex(bar, barProgress);
			if (index < 0 || index >= array.Length)
			{
				return 0f;
			}

			MusicScoreInfo info = array[index];
			float secondsPerBar = info.timeSignature * 60f / info.bpm;
			return info.time + secondsPerBar * (bar - info.bar) + secondsPerBar * (barProgress - info.barProgress);
		}

		public MusicScoreInfo GenerateNoteMusicScoreInfo(int bar, float barProgress)
		{
			MusicScoreInfo[] array = musicScoreInfoArray;
			if (array == null || array.Length == 0)
			{
				Debug.LogError(MusicScoreInfoArrayNotFound);
				return default(MusicScoreInfo);
			}

			int index = FindMusicScoreInfoIndex(bar, barProgress);
			if (index < 0 || index >= array.Length)
			{
				Debug.LogError(MusicScoreInfoArrayNotFound);
				return array[0];
			}

			MusicScoreInfo info = array[index];
			float secondsPerBar = info.timeSignature * 60f / info.bpm;
			float time = info.time + secondsPerBar * (bar - info.bar) + secondsPerBar * (barProgress - info.barProgress);
			return new MusicScoreInfo(bar, barProgress, time, info.bpm, info.timeSignature, info.speedRatio, info.seVolume);
		}

		public void GetMusicScoreInfoFromTime(float time, out MusicScoreInfo scoreInfo)
		{
			MusicScoreInfo[] array = musicScoreInfoArray;
			if (array == null)
			{
				Debug.LogError(MusicScoreInfoArrayNotFound);
				scoreInfo = default(MusicScoreInfo);
				return;
			}
			if (array.Length == 0)
			{
				Debug.LogWarning(NoBpmEventWarning);
				scoreInfo = default(MusicScoreInfo);
				return;
			}

			int index = 0;
			for (int i = array.Length - 1; i >= 0; i--)
			{
				if (array[i].time <= time)
				{
					index = i;
					break;
				}
			}
			GenerateScoreInfo(out scoreInfo, ref index, ref time);
		}

		private void GenerateScoreInfo(out MusicScoreInfo scoreInfo, ref int index, ref float time)
		{
			MusicScoreInfo[] array = musicScoreInfoArray;
			if (array == null || index < 0 || index >= array.Length)
			{
				Debug.LogError(MusicScoreInfoArrayNotFound);
				scoreInfo = default(MusicScoreInfo);
				return;
			}

			MusicScoreInfo source = array[index];
			float secondsPerBar = source.timeSignature * 60f / source.bpm;
			float barPosition = source.barProgress + (time - source.time) / secondsPerBar;
			int barOffset = Mathf.FloorToInt(barPosition);
			scoreInfo = new MusicScoreInfo(
				source.bar + barOffset,
				barPosition - barOffset,
				time,
				source.bpm,
				source.timeSignature,
				source.speedRatio,
				source.seVolume);
		}

		private int FindMusicScoreInfoIndex(int bar, float barProgress)
		{
			MusicScoreInfo[] array = musicScoreInfoArray;
			if (array == null)
			{
				return -1;
			}

			for (int i = 0; i < array.Length; i++)
			{
				if (i + 1 == array.Length || GreaterEqual(bar, barProgress, ref array[i + 1]))
				{
					return i;
				}
			}
			return -1;
		}

		private static bool GreaterEqual(int bar, float barProgress, ref MusicScoreInfo info)
		{
			return info.bar > bar || (info.bar == bar && info.barProgress >= barProgress);
		}
	}
}
