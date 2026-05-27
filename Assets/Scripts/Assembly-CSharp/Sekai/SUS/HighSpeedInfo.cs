using System.Runtime.CompilerServices;

namespace Sekai.SUS
{
	public class HighSpeedInfo
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

		public float SpeedRatio
		{
			[CompilerGenerated]
			get;
		}

		public HighSpeedInfo(int barIndex, int tickOffset, float speedRatio)
		{
			BarIndex = barIndex;
			TickOffset = tickOffset;
			SpeedRatio = speedRatio;
		}
	}
}
