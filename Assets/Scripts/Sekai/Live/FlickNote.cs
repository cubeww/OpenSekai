namespace Sekai.Live
{
	public class FlickNote : NoteBase
	{
		public FlickNote(MusicScoreInfo info, int id, int laneStart, int laneEnd, NoteCategory category, LiveBundleBuildData bundleBuildData, float speedRatio, NoteType type, NoteDirection direction)
			: base(info, id, laneStart, laneEnd, category, bundleBuildData, speedRatio, type)
		{
			Direction = direction;
		}
	}
}
