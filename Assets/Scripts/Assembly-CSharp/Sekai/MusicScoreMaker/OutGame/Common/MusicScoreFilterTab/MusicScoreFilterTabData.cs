using Sekai.UI;

namespace Sekai.MusicScoreMaker.OutGame.Common.MusicScoreFilterTab
{
	public sealed class MusicScoreFilterTabData : GenericTabData
	{
		public readonly TabType TabType;

		public readonly string DefaultText;

		public readonly MusicDifficulty MusicDifficulty;

		public MusicScoreFilterTabData(int index, string defaultText)
			: base(index, true)
		{
			TabType = TabType.Default;
			DefaultText = defaultText;
			MusicDifficulty = 0;
		}

		public MusicScoreFilterTabData(int index, MusicDifficulty musicDifficulty)
			: base(index, true)
		{
			TabType = TabType.MusicDifficulty;
			DefaultText = string.Empty;
			MusicDifficulty = musicDifficulty;
		}
	}
}
