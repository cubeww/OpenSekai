using System;

namespace Sekai.MusicScoreMaker.Outgame
{
	public sealed class AutoSaveMusicScoreSlotCellData
	{
		public int Number { get; private set; }

		public string MusicJacketAssetBundleName { get; private set; }

		public string MusicJacketFileName { get; private set; }

		public DateTime SaveDate { get; private set; }

		public bool IsSelected { get; private set; }

		public string MusicScoreFileName { get; private set; }

		public bool HasBaseMusicScore { get; private set; }

		public void Apply(int number, string musicJacketAssetBundleName, string musicJacketFileName, DateTime saveDate, bool isSelected, string musicScoreFileName, bool hasBaseMusicScore)
		{
			Number = number;
			MusicJacketAssetBundleName = musicJacketAssetBundleName;
			MusicJacketFileName = musicJacketFileName;
			SaveDate = saveDate;
			IsSelected = isSelected;
			MusicScoreFileName = musicScoreFileName;
			HasBaseMusicScore = hasBaseMusicScore;
		}

		public void ApplySelected(bool isSelected)
		{
			IsSelected = isSelected;
		}

		public AutoSaveMusicScoreSlotCellData()
		{
		}
	}
}
