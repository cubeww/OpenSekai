using System.Runtime.CompilerServices;

namespace Sekai.SUS
{
	public class VolumeInfo
	{
		public int BarIndex
		{
			[CompilerGenerated]
			get;
		}

		public int TickOffset
		{
			[CompilerGenerated]
			get;
		}

		public float Volume
		{
			[CompilerGenerated]
			get;
		}

		public VolumeInfo(int barIndex, int tickOffset, float volume)
		{
			BarIndex = barIndex;
			TickOffset = tickOffset;
			Volume = volume;
		}
	}
}
