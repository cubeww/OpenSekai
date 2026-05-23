namespace Sekai.Live
{
	public class TimeSignatureEventData : MusicBaseEventData
	{
		public float timeSignature;

		public TimeSignatureEventData(int barIndex, float timeSignature)
			: base(barIndex)
		{
			this.timeSignature = timeSignature;
		}
	}
}
