using System;

namespace Sekai.MusicScoreMaker.OutGame
{
	public sealed class MusicScorePublishRequestData
	{
		public string Title { get; private set; }

		public string Description { get; private set; }

		public int MusicId { get; private set; }

		public MusicDifficulty Difficulty { get; private set; }

		public int PlayLevel { get; private set; }

		public int[] TagIdArray { get; private set; }

		public bool IsDerivativeAllowed { get; private set; }

		public MusicScorePublishRequestData(int musicId)
		{
			Title = string.Empty;
			Description = string.Empty;
			Difficulty = MusicDifficulty.none;
			PlayLevel = 0;
			MusicId = musicId;
			TagIdArray = Array.Empty<int>();
			IsDerivativeAllowed = true;
		}

		public void ApplyTitle(string title)
		{
			Title = title;
		}

		public void ApplyDescription(string description)
		{
			Description = description;
		}

		public void ApplyDifficulty(MusicDifficulty difficulty)
		{
			Difficulty = difficulty;
		}

		public void ApplyPlayLevel(int playLevel)
		{
			PlayLevel = playLevel;
		}

		public void ApplyTagIdArray(int[] tagIdArray)
		{
			TagIdArray = tagIdArray ?? Array.Empty<int>();
		}

		public void ApplyIsDerivativeAllowed(bool isDerivativeAllowed)
		{
			IsDerivativeAllowed = isDerivativeAllowed;
		}
	}
}
