using System;
using System.Collections;
using System.Reflection;
using CP;

namespace Sekai.MusicScoreMaker.OutGame
{
	public sealed class MusicScorePlayLevelRangeProvider
	{
		public (int, int) GetPlayLevelRange(MusicDifficulty difficulty)
		{
			if (difficulty == MusicDifficulty.none)
			{
				return GetPlayLevelRangeForAll();
			}

			foreach (object value in EnumerateMasterValues())
			{
				string musicDifficultyType = GetString(value, "musicDifficultyType");
				if (Enum.TryParse(musicDifficultyType, ignoreCase: true, out MusicDifficulty parsed) && parsed == difficulty)
				{
					return (GetInt(value, "minPlayLevel"), GetInt(value, "maxPlayLevel"));
				}
			}

			LogUtility.LogError("MasterCustomMusicScoreDifficultyPlayLevel is not found. difficulty={0}", difficulty);
			return (0, 0);
		}

		private (int, int) GetPlayLevelRangeForAll()
		{
			bool hasValue = false;
			int min = 0;
			int max = 0;

			foreach (object value in EnumerateMasterValues())
			{
				int currentMin = GetInt(value, "minPlayLevel");
				int currentMax = GetInt(value, "maxPlayLevel");
				if (!hasValue)
				{
					min = currentMin;
					max = currentMax;
					hasValue = true;
					continue;
				}

				min = Math.Min(min, currentMin);
				max = Math.Max(max, currentMax);
			}

			if (!hasValue)
			{
				LogUtility.LogError("MasterCustomMusicScoreDifficultyPlayLevel is empty.", Array.Empty<object>());
				return (0, 0);
			}

			return (min, max);
		}

		public MusicScorePlayLevelRangeProvider()
		{
		}

		private static IEnumerable EnumerateMasterValues()
		{
			object masterDataManager = GetMasterDataManager();
			object levels = Invoke(masterDataManager, "GetMasterCustomMusicScoreDifficultyPlayLevels");
			if (levels == null)
			{
				return Array.Empty<object>();
			}

			object values = levels.GetType().GetProperty("Values")?.GetValue(levels);
			return values as IEnumerable ?? Array.Empty<object>();
		}

		private static object GetMasterDataManager()
		{
			Type type = FindType("Sekai.MasterDataManager");
			return type?.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static)?.GetValue(null);
		}

		private static object Invoke(object target, string methodName)
		{
			return target?.GetType().GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)?.Invoke(target, null);
		}

		private static int GetInt(object source, string memberName)
		{
			object value = GetMember(source, memberName);
			return value == null ? 0 : Convert.ToInt32(value);
		}

		private static string GetString(object source, string memberName)
		{
			return GetMember(source, memberName) as string;
		}

		private static object GetMember(object source, string memberName)
		{
			if (source == null)
			{
				return null;
			}

			Type type = source.GetType();
			FieldInfo field = type.GetField(memberName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
			if (field != null)
			{
				return field.GetValue(source);
			}

			return type.GetProperty(memberName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(source);
		}

		private static Type FindType(string fullName)
		{
			Type type = Type.GetType(fullName);
			if (type != null)
			{
				return type;
			}

			foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				type = assembly.GetType(fullName);
				if (type != null)
				{
					return type;
				}
			}

			return null;
		}
	}
}
