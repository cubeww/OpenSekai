namespace Sekai.Live
{
	public class FeverBeginEvent : EventBase
	{
		public int FeverNoteCount { get; private set; }

		public FeverBeginEvent(MusicScoreInfo info)
			: base(info)
		{
		}

		public void Setup(int noteCount)
		{
			FeverNoteCount = noteCount;
		}
	}
}
