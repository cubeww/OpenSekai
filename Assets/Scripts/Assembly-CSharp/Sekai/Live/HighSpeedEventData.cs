namespace Sekai.Live
{
	public class HighSpeedEventData : MusicBaseEventData
	{
		public int tickOffset;

		public float speedRatio;

		public HighSpeedEventData(int barIndex, int tickOffset, float speedRatio)
			: base(barIndex)
		{
			this.tickOffset = tickOffset;
			this.speedRatio = speedRatio;
		}
	}
}
