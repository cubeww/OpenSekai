namespace Sekai.Live
{
	public class NormalNote : NoteBase
	{
		public NormalNote(MusicScoreInfo info, int id, int laneStart, int laneEnd, NoteCategory category, LiveBundleBuildData bundleBuildData, float speedRatio, NoteType type)
			: base(info, id, laneStart, laneEnd, category, bundleBuildData, speedRatio, type)
		{
		}
	}
}
