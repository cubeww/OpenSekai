namespace Sekai.SUS
{
	public class BpmInfo
	{
		public float bpm;

		public int bar;

		public float barProgress;

		public BpmInfo(float bpm, int bar, float barProgress)
		{
			this.bpm = bpm;
			this.bar = bar;
			this.barProgress = barProgress;
		}
	}
}
