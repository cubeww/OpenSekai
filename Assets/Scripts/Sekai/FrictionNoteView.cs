using Sekai.Live;
using UnityEngine;

namespace Sekai
{
	public class FrictionNoteView : BaseNoteView
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
			transform.localPosition = CalcNotePosition(ref progress);
			transform.localScale = progress * LiveConfig.Vector3One;
			UpdateMaterialParameters(progress);
			if (centerIconRenderer != null && spriteRenderer != null)
			{
				centerIconRenderer.color = spriteRenderer.color;
			}
			if (note.ParentNote != null)
			{
				UpdateLongSpriteSize();
			}
		}

		protected override Vector3 CalcNotePosition(ref float progress)
		{
			return CalcLaneInterpolatedNotePosition(ref progress, true);
		}

		private float CalcProgress()
		{
			if (note == null)
			{
				return 0f;
			}

			float progress = note.Progress;
			if (note.ChildNote != null && progress >= 1f)
			{
				progress = 1f;
			}
			return LiveConfig.GetNoteViewProgress(progress);
		}
	}
}
