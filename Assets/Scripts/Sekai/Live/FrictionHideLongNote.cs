namespace Sekai.Live
{
	public class FrictionHideLongNote : FrictionLongNote
	{
		public override bool HasJudgment
		{
			get { return false; }
		}

		public FrictionHideLongNote(MusicScoreInfo info, int id, int laneStart, int laneEnd, NoteCategory category, NoteType type, LiveBundleBuildData bundleBuildData, float speedRatio, NoteLineType lineType = NoteLineType.Linear)
			: base(info, id, laneStart, laneEnd, category, type, bundleBuildData, speedRatio, lineType)
		{
		}
	}
}
