namespace Sekai.Live
{
	public class VolumeEventData : MusicBaseEventData
	{
		public int tickOffset;

		public float seVolume;

		public VolumeEventData(int barIndex, int tickOffset, float seVolume)
			: base(barIndex)
		{
			this.tickOffset = tickOffset;
			this.seVolume = seVolume;
		}
	}
}
