using Sekai.Live;
using UnityEngine;

namespace Sekai
{
	public class ConnectionNoteView : BaseNoteView
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

			float progress = LiveConfig.GetNoteViewProgress(note.Progress);
			transform.localScale = progress * LiveConfig.Vector3One;
			transform.localPosition = CalcNotePosition(ref progress);
			UpdateMaterialParameters(progress);
			if (IsLongBodyCategory(note.Category))
			{
				UpdateLongSpriteSize();
			}
		}

		protected override Vector3 CalcNotePosition(ref float progress)
		{
			return CalcLaneInterpolatedNotePosition(ref progress, false);
		}
	}
}
