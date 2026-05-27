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
			transform.localScale = Vector3.one * progress;
			transform.localPosition = CalcNotePosition(ref progress);
			UpdateSpritePayload(progress);

			if (note.Category == Sekai.Live.NoteCategory.Long && spriteRenderer != null)
			{
				spriteRenderer.size = LiveUtility.CalcSpriteRendererSize(note.LaneEndF - note.LaneStartF);
			}
		}

		protected float CalcProgress()
		{
			if (note == null)
			{
				return 0f;
			}

			float progress = note.Progress;
			if ((note.ChildNote != null || note.State == NoteState.InputBegan) && progress >= 1f)
			{
				progress = 1f;
			}
			return LiveConfig.GetNoteViewProgress(progress);
		}

		protected override Vector3 CalcNotePosition(ref float progress)
		{
			if (note == null)
			{
				return Vector3.zero;
			}

			Vector2 center = GetLaneCenter(note.LaneStartF, note.LaneEndF);
			Vector2 position = Vector2.LerpUnclamped(spawnPosition, center, progress);
			float z = note.State >= NoteState.InputBegan ? 3f - posZ : posZ;
			return new Vector3(position.x, position.y, z);
		}

		public LongNoteView()
		{
		}
	}
}
