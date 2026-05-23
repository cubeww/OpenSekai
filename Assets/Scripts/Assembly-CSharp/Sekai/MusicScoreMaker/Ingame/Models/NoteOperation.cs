namespace Sekai.MusicScoreMaker.Ingame.Models
{
	public struct NoteOperation
	{
		public int Id { get; }

		public int StartLane { get; }

		public int EndLane { get; }

		public long Ticks { get; }

		public NoteOperation(int id, int startLane, int endLane, long ticks)
		{
			Id = id;
			StartLane = startLane;
			EndLane = endLane;
			Ticks = ticks;
		}

		public static bool operator ==(NoteOperation left, NoteOperation right)
		{
			return left.Id == right.Id && left.StartLane == right.StartLane && left.EndLane == right.EndLane && left.Ticks == right.Ticks;
		}

		public static bool operator !=(NoteOperation left, NoteOperation right)
		{
			return !(left == right);
		}
	}
}
