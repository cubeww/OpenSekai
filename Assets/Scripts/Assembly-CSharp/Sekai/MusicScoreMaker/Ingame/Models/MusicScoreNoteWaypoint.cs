using System;

namespace Sekai.MusicScoreMaker.Ingame.Models
{
	[Serializable]
	public class MusicScoreNoteWaypoint
	{
		public long ticks;

		public float lane;

		public MusicScoreNoteWaypoint()
		{
		}

		public MusicScoreNoteWaypoint(long ticks, float lane)
		{
			this.ticks = ticks;
			this.lane = lane;
		}
	}
}
