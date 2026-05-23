using System;
using System.Collections.Generic;
using System.Reflection;
using Sekai.Live;
using UnityEngine;

namespace Sekai.MusicScoreMaker.Ingame.Models
{
	public struct NoteAndComboCountInfo
	{
		public int TapCount;

		public int LongCount;

		public int FlickCount;

		public int TraceCount;

		public int MiddleNoteCount;

		public int TotalComboCount;

		private static LiveBundleBuildData _cachedLiveBundleBuildData;

		public static NoteAndComboCountInfo Calculate(MusicScoreMakerData data)
		{
			if (data == null)
			{
				return default;
			}

			if (_cachedLiveBundleBuildData == null)
			{
				_cachedLiveBundleBuildData = Resources.Load<LiveBundleBuildData>(LiveConfig.ConfigBundleNamePath);
			}

			if (_cachedLiveBundleBuildData != null && TryConvertToMusicScore(data, out MusicScore musicScore))
			{
				return CalculateFromMusicScore(musicScore);
			}

			return CalculateFromNoteList(data);
		}

		private static NoteAndComboCountInfo CalculateFromNoteList(MusicScoreMakerData data)
		{
			NoteAndComboCountInfo info = default;
			List<MusicScoreNoteBase> noteList = GetMakerNoteList(data);
			if (noteList == null)
			{
				return info;
			}

			foreach (MusicScoreNoteBase note in noteList)
			{
				if (note != null)
				{
					UpdateCountInfo(ref info, note.category);
				}
			}

			return info;
		}

		private static void UpdateCountInfo(ref NoteAndComboCountInfo info, NoteCategory category)
		{
			switch (category)
			{
			case NoteCategory.Normal:
				info.TapCount++;
				break;
			case NoteCategory.Long:
				info.LongCount++;
				break;
			case NoteCategory.Connection:
			case NoteCategory.Combo:
				info.MiddleNoteCount++;
				break;
			case NoteCategory.Flick:
			case NoteCategory.FrictionFlick:
				info.FlickCount++;
				break;
			case NoteCategory.Friction:
			case NoteCategory.FrictionHide:
			case NoteCategory.FrictionLong:
			case NoteCategory.FrictionHideLong:
				info.TraceCount++;
				break;
			default:
				return;
			}

			info.TotalComboCount++;
		}

		private static NoteAndComboCountInfo CalculateFromMusicScore(MusicScore musicScore)
		{
			NoteAndComboCountInfo info = default;
			if (musicScore?.NoteArray == null)
			{
				return info;
			}

			foreach (NoteBase note in musicScore.NoteArray)
			{
				if (note?.NoteList == null)
				{
					continue;
				}

				foreach (NoteBase childNote in note.NoteList)
				{
					if (childNote != null && IsComboEnabled(childNote))
					{
						UpdateCountInfo(ref info, childNote.Category);
					}
				}
			}

			return info;
		}

		private static bool TryConvertToMusicScore(MusicScoreMakerData data, out MusicScore musicScore)
		{
			musicScore = null;
			MethodInfo method = data.GetType().GetMethod(
				"ToMusicScore",
				BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
				null,
				new[] { typeof(LiveBundleBuildData), typeof(long?), typeof(bool) },
				null);
			if (method == null)
			{
				return false;
			}

			try
			{
				musicScore = method.Invoke(data, new object[] { _cachedLiveBundleBuildData, null, false }) as MusicScore;
				return musicScore != null;
			}
			catch (TargetInvocationException)
			{
				return false;
			}
			catch (ArgumentException)
			{
				return false;
			}
		}

		private static List<MusicScoreNoteBase> GetMakerNoteList(MusicScoreMakerData data)
		{
			if (data == null)
			{
				return null;
			}

			Type type = data.GetType();
			PropertyInfo property = type.GetProperty("NoteList", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (property?.GetValue(data) is List<MusicScoreNoteBase> noteList)
			{
				return noteList;
			}

			FieldInfo field = type.GetField("_NoteList_k__BackingField", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
				?? type.GetField("<NoteList>k__BackingField", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			return field?.GetValue(data) as List<MusicScoreNoteBase>;
		}

		private static bool IsComboEnabled(NoteBase note)
		{
			PropertyInfo property = note.GetType().GetProperty("IsEnableCombo", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			return property == null || !(property.GetValue(note) is bool enabled) || enabled;
		}
	}
}
