namespace Sekai.SUS
{
	public class VolumeInfo
	{
		public int BarIndex { get; private set; }

		public int TickOffset { get; private set; }

		public float Volume { get; private set; }

		public VolumeInfo(int barIndex, int tickOffset, float volume)
		{
			BarIndex = barIndex;
			TickOffset = tickOffset;
			Volume = volume;
		}
	}
}
