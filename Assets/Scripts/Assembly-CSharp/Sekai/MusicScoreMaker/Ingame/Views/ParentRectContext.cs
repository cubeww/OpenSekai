using UnityEngine;

namespace Sekai.MusicScoreMaker.Ingame.Views
{
	public readonly struct ParentRectContext
	{
		public readonly float Width;

		public readonly float Height;

		public readonly Vector2 Position;

		public Vector2 Size
		{
			get
			{
				return new Vector2(Width, Height);
			}
		}

		public ParentRectContext(float width, float height, Vector2 position)
		{
			Width = width;
			Height = height;
			Position = position;
		}
	}
}
