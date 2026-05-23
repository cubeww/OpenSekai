using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Sekai.MusicScoreMaker.Ingame.Models;

namespace Sekai.MusicScoreMaker.OutGame.Publish
{
	public sealed class MusicScorePublishConfirmModel
	{
		public readonly MusicScoreMakerData MusicScoreData;

		public readonly bool HasBaseMusicScore;

		public readonly MusicDifficulty RecommendedDifficulty;

		public readonly bool IsValidDifficultyAppend;

		private readonly MusicDifficulty _baseRecommendedDifficulty;

		public MusicScorePublishRequestData PublishRequestData
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			private set
			{
				throw null;
			}
		}

		public MusicDifficulty[] ValidDifficulties
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			private set
			{
				throw null;
			}
		}

		public List<int> PlayLevelList
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			private set
			{
				throw null;
			}
		}

		public List<int> SelectedTagIds
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			private set
			{
				throw null;
			}
		}

		public MusicScorePublishConfirmModel([NotNull] MusicScoreMakerData musicScoreData, bool hasBaseMusicScore, MusicDifficulty recommendedDifficulty)
		{
			throw null;
		}

		public void ApplyTitle(string title)
		{
			throw null;
		}

		public void ApplyDescription(string description)
		{
			throw null;
		}

		private void ApplyValidDifficulty()
		{
			throw null;
		}

		private IEnumerable<MusicDifficulty> CreateBasicDifficultyRange()
		{
			throw null;
		}

		public void ApplySelectedDifficulty(MusicDifficulty difficulty)
		{
			throw null;
		}

		private (int, int) GetPlayLevelRangeForRecommend(MusicScorePlayLevelRangeProvider provider)
		{
			throw null;
		}

		public void ApplyPlayLevelByIndex(int index)
		{
			throw null;
		}

		private void ApplyPlayLevel(int playLevel)
		{
			throw null;
		}

		public void ApplySelectedTags(int[] tagIds)
		{
			throw null;
		}

		public void RemoveTag(int tagId)
		{
			throw null;
		}

		public void ApplyDerivativeAllowed(bool isAllowed)
		{
			throw null;
		}

		public bool IsAllowedToPublish()
		{
			throw null;
		}
	}
}
