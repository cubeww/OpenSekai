namespace Sekai.Live
{
	public class GuideNote : LongNote
	{
		public override bool HasJudgment
		{
			get { return false; }
		}

		public GuideNote(MusicScoreInfo info, int id, int laneStart, int laneEnd, NoteCategory category, NoteType type, LiveBundleBuildData bundleBuildData, float speedRatio, NoteLineType lineType = NoteLineType.Linear)
			: base(info, id, laneStart, laneEnd, category, type, bundleBuildData, speedRatio, lineType)
		{
		}
	}
}
