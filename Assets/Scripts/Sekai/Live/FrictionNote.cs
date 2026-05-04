namespace Sekai.Live
{
	public class FrictionNote : NoteBase
	{
		private readonly NoteLineType lineType;

		public override NoteLineType LineType
		{
			get { return lineType; }
		}

		public FrictionNote(MusicScoreInfo info, int id, int laneStart, int laneEnd, NoteCategory category, LiveBundleBuildData bundleBuildData, NoteType type, float speedRatio, NoteLineType lineType = NoteLineType.Linear)
			: base(info, id, laneStart, laneEnd, category, bundleBuildData, speedRatio, type)
		{
			this.lineType = lineType;
		}
	}
}
