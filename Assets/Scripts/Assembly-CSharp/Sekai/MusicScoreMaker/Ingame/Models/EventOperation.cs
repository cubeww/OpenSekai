namespace Sekai.MusicScoreMaker.Ingame.Models
{
	public struct EventOperation
	{
		public int Id { get; }

		public long Ticks { get; }

		public EventOperation(int id, long ticks)
		{
			Id = id;
			Ticks = ticks;
		}
	}
}
