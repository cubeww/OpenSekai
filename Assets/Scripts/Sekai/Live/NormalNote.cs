namespace Sekai.Live
{
	public class NormalNote : NoteBase
	{
		public NormalNote(MusicScoreInfo info, int id, int laneStart, int laneEnd, NoteCategory category, LiveBundleBuildData bundleBuildData, float speedRatio, NoteType type)
			: base(info, id, laneStart, laneEnd, category, bundleBuildData, speedRatio, type)
		{
		}

		public override NoteState State
		{
			get { return base.State; }
			protected set
			{
				if (value == NoteState.Done && State != NoteState.Done && parentNote != null)
				{
					parentNote.ForceTerminate();
				}
				base.State = value;
			}
		}

		public override void SetParentNote(LongNote note)
		{
			base.SetParentNote(note);
			if (parentNote != null && (parentNote.Type == NoteType.Critical || Type == NoteType.Critical))
			{
				Type = NoteType.Critical;
			}
			Category = NoteCategory.Long;
		}
	}
}
