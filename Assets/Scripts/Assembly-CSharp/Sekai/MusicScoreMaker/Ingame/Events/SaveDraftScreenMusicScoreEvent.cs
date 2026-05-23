namespace Sekai.MusicScoreMaker.Ingame.Events
{
	public class SaveDraftScreenMusicScoreEvent : MusicScoreMakerDispatcherEventBase
	{
		public readonly bool IsExitOnSave;

		public SaveDraftScreenMusicScoreEvent()
		{
		}

		public SaveDraftScreenMusicScoreEvent(bool isExitOnSave)
		{
			IsExitOnSave = isExitOnSave;
		}
	}
}
