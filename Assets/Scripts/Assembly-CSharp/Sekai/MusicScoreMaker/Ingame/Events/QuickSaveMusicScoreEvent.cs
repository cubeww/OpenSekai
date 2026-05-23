namespace Sekai.MusicScoreMaker.Ingame.Events
{
	public class QuickSaveMusicScoreEvent : MusicScoreMakerDispatcherEventBase
	{
		public readonly bool IsExitOnSave;

		public QuickSaveMusicScoreEvent()
		{
		}

		public QuickSaveMusicScoreEvent(bool isExitOnSave)
		{
			IsExitOnSave = isExitOnSave;
		}
	}
}
