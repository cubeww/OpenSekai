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
			base.Move();
			if (centerIconRenderer != null && spriteRenderer != null)
			{
				centerIconRenderer.color = spriteRenderer.color;
			}
			if ((note?.Category == NoteCategory.FrictionLong || note?.Category == NoteCategory.FrictionHideLong) && spriteRenderer != null)
			{
				spriteRenderer.size = LiveUtility.CalcSpriteRendererSize(note.LaneEndF - note.LaneStartF);
			}
		}

		public FrictionLongNoteView()
		{
		}
	}
}
