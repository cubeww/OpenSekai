using System.Collections.Generic;

namespace Sekai.MusicScoreMaker.Ingame.Models
{
	public class InvalidPlacementInfo
	{
		public InvalidPlacementType Type { get; set; }

		public long Ticks { get; set; }

		public long EndTicks { get; set; }

		public int OverlapLaneStart { get; set; }

		public int OverlapLaneEnd { get; set; }

		public List<int> Ids { get; set; }

		public int ActualComboCount { get; set; }

		public int ComboCountMinimum { get; set; }

		public int ComboCountMaximum { get; set; }

		public long CorrectBarStartTicks { get; set; }

		public int EstimatedMeshCount { get; set; }

		public int MeshPoolLimit { get; set; }

		public MeshOverflowSpeedType OverflowSpeedType { get; set; }

		public int ActualTapNotesCount { get; set; }

		public int TapNotesMinimum { get; set; }

		public int NoteDensityThreshold { get; set; }

		public int NoteDensityActualCount { get; set; }

		public float NoteDensityWindowSec { get; set; }

		public float GapThresholdSec { get; set; }

		public float GapActualSec { get; set; }

		public InvalidPlacementInfo()
		{
			Ids = new List<int>();
		}
	}
}
