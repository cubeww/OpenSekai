using JetBrains.Annotations;
using Sekai.MusicScoreMaker.OutGame;
using UnityEngine;

namespace Sekai.MusicScoreMaker.Outgame
{
	public class MusicScoreListCellData
	{
		public readonly MusicScoreInfoCellCommonData InfoCellCommonData;

		public string MusicScoreId
		{
			get
			{
				return InfoCellCommonData.id;
			}
		}

		public MusicDifficulty Difficulty
		{
			get
			{
				return InfoCellCommonData.difficulty;
			}
		}

		public Vector3 AnchoredPositionOffset { get; set; }

		public MusicScoreListCellData(MusicScoreInfoCellCommonData infoCellCommonData)
		{
			InfoCellCommonData = infoCellCommonData;
		}

		public MusicScoreListCellData([NotNull] MusicScoreData musicScoreData, bool enableDerivativeDisallowedIndicator = false)
		{
			InfoCellCommonData = new MusicScoreInfoCellCommonData(musicScoreData, enableDerivativeDisallowedIndicator);
		}
	}
}
