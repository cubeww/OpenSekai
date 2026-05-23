namespace Sekai.Live
{
	public class GuideHiddenConnectionNote : HiddenConnectionNote
	{
		public GuideHiddenConnectionNote()
		{
			Category = NoteCategory.GuideHidden;
		}

		public GuideHiddenConnectionNote(MusicScoreInfo info, int id, int laneStart, int laneEnd, NoteCategory category, LiveBundleBuildData bundleBuildData, NoteType type, float speedRatio, NoteLineType lineType = NoteLineType.Linear)
			: base(info, id, laneStart, laneEnd, category, type, speedRatio, LiveUtility.GetLaneOffset(category, bundleBuildData), lineType)
		{
		}
	}
}
