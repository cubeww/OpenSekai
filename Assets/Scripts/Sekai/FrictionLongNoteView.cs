using Sekai.Live;
using UnityEngine;

namespace Sekai
{
	public class FrictionLongNoteView : LongNoteView
	{
		[SerializeField]
		protected SpriteRenderer centerIconRenderer;

		public override void Move()
		{
			if (note == null)
			{
				return;
			}
			if (note.Progress <= 0f)
			{
				transform.localScale = Vector3.zero;
				return;
			}

			float progress = CalcProgress();
			transform.localScale = progress * LiveConfig.Vector3One;
			transform.localPosition = CalcNotePosition(ref progress);
			UpdateMaterialParameters(progress);
			if (centerIconRenderer != null && spriteRenderer != null)
			{
				centerIconRenderer.color = spriteRenderer.color;
			}
			if (IsLongBodyCategory(note.Category))
			{
				UpdateLongSpriteSize();
			}
		}
	}
}
