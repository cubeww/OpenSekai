namespace Sekai.Live
{
	public class FrictionHideNote : FrictionNote
	{
		public override bool HasJudgment
		{
			get { return false; }
		}

		public FrictionHideNote(MusicScoreInfo info, int id, int laneStart, int laneEnd, NoteCategory category, LiveBundleBuildData bundleBuildData, NoteType type, float speedRatio, NoteLineType lineType = NoteLineType.Linear)
			: base(info, id, laneStart, laneEnd, category, bundleBuildData, type, speedRatio, lineType)
		{
		}
	}
}
