using UnityEngine;

namespace Sekai
{
	public class FrictionFlickNoteView : FlickNoteView
	{
		[SerializeField]
		protected SpriteRenderer centerIconRenderer;

		public override void Move()
		{
			base.Move();
			if (centerIconRenderer != null && spriteRenderer != null)
			{
				centerIconRenderer.color = spriteRenderer.color;
			}
		}
	}
}
