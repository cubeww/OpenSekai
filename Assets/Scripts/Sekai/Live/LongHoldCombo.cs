namespace Sekai.Live
{
	public class LongHoldCombo : NoteBase
	{
		private readonly float laneOffset;

		public float LaneOffset
		{
			get { return laneOffset; }
		}

		public LongHoldCombo(MusicScoreInfo info, NoteType type, LiveBundleBuildData liveBundleBuildData, float speedRatio, float laneOffset)
			: base(info, 0, 0, 0, NoteCategory.Combo, liveBundleBuildData, speedRatio, type)
		{
			this.laneOffset = laneOffset;
		}
	}
}
