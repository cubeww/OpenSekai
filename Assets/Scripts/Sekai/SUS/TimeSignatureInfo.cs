namespace Sekai.SUS
{
	public class TimeSignatureInfo
	{
		public float TimeSignature;

		public int StartBarIndex;

		public TimeSignatureInfo(float timeSignature, int startBarIndex)
		{
			TimeSignature = timeSignature;
			StartBarIndex = startBarIndex;
		}
	}
}
