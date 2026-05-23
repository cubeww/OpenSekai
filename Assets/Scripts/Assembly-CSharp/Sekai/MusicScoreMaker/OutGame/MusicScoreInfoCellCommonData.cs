using System;
using System.Reflection;
using JetBrains.Annotations;
using Sekai.MusicScoreMaker.OutGame;
using UnityEngine;

namespace Sekai.MusicScoreMaker.Outgame
{
	public sealed class MusicScoreInfoCellCommonData
	{
		private readonly MusicScoreData _musicScoreData;

		public string id;

		public int musicLevel;

		public string title;

		public int playCount;

		public bool isBookmarked;

		public bool isReused;

		public bool showDerivativeDisallowedIndicator;

		public bool showDerivativeAllowedBadge;

		public bool showBaseMusicScoreBadge;

		public MusicDifficulty difficulty;

		public MusicClearStatus clearStatus;

		public MasterMusicAllModel musicAllModel;

		public long publishedAt;

		public int reviewCount
		{
			get
			{
				return _musicScoreData?.ReviewCount ?? 0;
			}
		}

		public int reviewCountForDisplay
		{
			get
			{
				return Math.Min(reviewCount, 9999999);
			}
		}

		public string authorName
		{
			get
			{
				return _musicScoreData?.GetDisplayAuthorName() ?? string.Empty;
			}
		}

		public bool hasMarker
		{
			get
			{
				return showDerivativeAllowedBadge || showBaseMusicScoreBadge;
			}
		}

		public bool IsValid
		{
			get
			{
				if (musicAllModel == null)
				{
					return false;
				}

				try
				{
					return musicAllModel.Id > 0;
				}
				catch
				{
					return true;
				}
			}
		}

		public MusicScoreInfoCellCommonData()
		{
		}

		public MusicScoreInfoCellCommonData([NotNull] MusicScoreData musicScoreData, bool enableDerivativeDisallowedIndicator = false)
		{
			_musicScoreData = musicScoreData;
			id = musicScoreData.Id;
			title = musicScoreData.Title;
			playCount = musicScoreData.PlayCount;
			difficulty = musicScoreData.Difficulty;
			musicAllModel = CreateMusicAllModel(musicScoreData.MusicId);
			musicLevel = musicScoreData.PlayLevel;
			clearStatus = musicScoreData.ClearStatus;
			isReused = !string.IsNullOrEmpty(musicScoreData.BaseMusicScoreId);
			isBookmarked = musicScoreData.IsBookmarked;
			publishedAt = musicScoreData.PublishedAt;
			showDerivativeDisallowedIndicator = enableDerivativeDisallowedIndicator
				&& !musicScoreData.IsDerivativeAllowed
				&& !musicScoreData.IsMyMusicScore;
			showDerivativeAllowedBadge = musicScoreData.IsDerivativeAllowed;
			showBaseMusicScoreBadge = !string.IsNullOrEmpty(musicScoreData.BaseMusicScoreId);
		}

		public Color GetDifficultyColor()
		{
			switch (difficulty)
			{
				case MusicDifficulty.easy:
					return new Color(0.28f, 0.78f, 0.36f, 1f);
				case MusicDifficulty.normal:
					return new Color(0.23f, 0.63f, 0.94f, 1f);
				case MusicDifficulty.hard:
					return new Color(0.95f, 0.68f, 0.25f, 1f);
				case MusicDifficulty.expert:
					return new Color(0.86f, 0.33f, 0.48f, 1f);
				case MusicDifficulty.master:
					return new Color(0.67f, 0.42f, 0.90f, 1f);
				case MusicDifficulty.append:
					return new Color(0.91f, 0.40f, 0.82f, 1f);
				default:
					return Color.white;
			}
		}

		private static MasterMusicAllModel CreateMusicAllModel(int musicId)
		{
			try
			{
				object manager = GetSingletonInstance("Sekai.MasterDataManager");
				object music = InvokeMember(manager, "GetMasterMusicAll", musicId);
				return music is MasterMusicAll masterMusicAll ? new MasterMusicAllModel(masterMusicAll) : null;
			}
			catch
			{
				// TODO(original): restore direct MasterDataManager.GetMasterMusicAll dependency.
				return null;
			}
		}

		private static object GetSingletonInstance(string typeName)
		{
			Type type = FindType(typeName);
			return type?.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static)?.GetValue(null);
		}

		private static object InvokeMember(object target, string methodName, params object[] args)
		{
			if (target == null)
			{
				return null;
			}

			foreach (MethodInfo method in target.GetType().GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
			{
				if (method.Name == methodName && method.GetParameters().Length == args.Length)
				{
					return method.Invoke(target, args);
				}
			}

			return null;
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
