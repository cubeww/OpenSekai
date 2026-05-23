namespace Sekai.Live
{
	public class BpmEventData : MusicBaseEventData
	{
		public float barProgress;

		public float bpm;

		public BpmEventData(int barIndex, float barProgress, float bpm)
			: base(barIndex)
		{
			this.barProgress = barProgress;
			this.bpm = bpm;
		}
	}
}
