namespace Sekai.SUS
{
	public class HighSpeedInfo
	{
		public int BarIndex { get; private set; }

		public int TickOffset { get; private set; }

		public float SpeedRatio { get; private set; }

		public HighSpeedInfo(int barIndex, int tickOffset, float speedRatio)
		{
			BarIndex = barIndex;
			TickOffset = tickOffset;
			SpeedRatio = speedRatio;
		}
	}
}
