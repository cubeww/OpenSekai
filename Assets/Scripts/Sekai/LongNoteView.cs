using Sekai.Live;
using UnityEngine;

namespace Sekai
{
	public class LongNoteView : BaseNoteView
	{
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
			if (note.Category == NoteCategory.Long)
			{
				UpdateLongSpriteSize();
			}
		}

		protected float CalcProgress()
		{
			if (note == null)
			{
				return 0f;
			}

			float progress = note.Progress;
			if ((note.ChildNote != null || note.Category == NoteCategory.Connection) && progress >= 1f)
			{
				progress = 1f;
			}
			return LiveConfig.GetNoteViewProgress(progress);
		}

		protected override Vector3 CalcNotePosition(ref float progress)
		{
			return CalcLaneInterpolatedNotePosition(ref progress, true);
		}
	}
}
