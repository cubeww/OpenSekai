namespace Sekai.Live
{
	public class GuideEndNote : FrictionHideNote
	{
		public GuideEndNote(MusicScoreInfo info, int id, int laneStart, int laneEnd, NoteCategory category, LiveBundleBuildData bundleBuildData, NoteType type, float speedRatio, NoteLineType lineType = NoteLineType.Linear)
			: base(info, id, laneStart, laneEnd, category, bundleBuildData, type, speedRatio, lineType)
		{
		}

		public override void SetParentNote(LongNote note)
		{
			parentNote = note;
			if (parentNote != null && (parentNote.Type == NoteType.Critical || Type == NoteType.Critical))
			{
				Type = NoteType.Critical;
			}
			Category = NoteCategory.Guide;
		}
	}
}
