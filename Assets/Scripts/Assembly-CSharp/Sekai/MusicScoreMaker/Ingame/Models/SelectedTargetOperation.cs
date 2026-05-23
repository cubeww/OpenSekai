namespace Sekai.MusicScoreMaker.Ingame.Models
{
	public class SelectedTargetOperation
	{
		public enum NoteTapPosition
		{
			none = 0,
			center = 1,
			left = 2,
			right = 3
		}

		private const float DRAG_TO_LANE_COEFFICIENT = 0.01f;

		public long deltaTicks;

		public int deltaLane;

		public NoteTapPosition noteTapPosition;

		public void Expand(NoteTapPosition position)
		{
			noteTapPosition = position;
		}

		public void ExpandDrag(NoteTapPosition position, float dragDelta)
		{
			noteTapPosition = position;
			deltaLane += (int)System.Math.Round(dragDelta * DRAG_TO_LANE_COEFFICIENT);
		}

		public SelectedTargetOperation()
		{
		}
	}
}
