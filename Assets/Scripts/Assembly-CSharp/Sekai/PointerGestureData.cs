using UnityEngine;
using UnityEngine.EventSystems;

namespace Sekai
{
	public class PointerGestureData
	{
		private const float FLICK_THRESHOLD = 32f;

		public Vector2[] DragDelta { get; set; }

		public float NormalizeDeltaScale { get; set; }

		public float DeltaScale { get; set; }

		public float Distance { get; set; }

		public float PrevDistance { get; set; }

		public Vector2[] ScreenPosition { get; set; }

		public int PointerCount { get; set; }

		public int PrevPointerCount { get; set; }

		public Vector2 FlickVector { get; set; }

		private bool IsFlickHorizontal
		{
			get
			{
				return Mathf.Abs(FlickVector.x) > FLICK_THRESHOLD;
			}
		}

		public bool IsFlickRight
		{
			get
			{
				return IsFlickHorizontal && FlickVector.x > 0f;
			}
		}

		public bool IsFlickLeft
		{
			get
			{
				return IsFlickHorizontal && FlickVector.x < 0f;
			}
		}

		private bool IsFlickVertical
		{
			get
			{
				return Mathf.Abs(FlickVector.y) > FLICK_THRESHOLD;
			}
		}

		public bool IsFlickTop
		{
			get
			{
				return IsFlickVertical && FlickVector.y > 0f;
			}
		}

		public bool IsFlickBottom
		{
			get
			{
				return IsFlickVertical && FlickVector.y < 0f;
			}
		}

		public PointerEventData PointerEventData { get; set; }

		public PointerGestureData()
		{
			ScreenPosition = new[] { Vector2.zero, Vector2.zero };
			DragDelta = new[] { Vector2.zero, Vector2.zero };
			FlickVector = Vector2.zero;
		}
	}
}
