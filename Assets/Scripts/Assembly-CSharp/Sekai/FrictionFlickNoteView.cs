using Sekai.Live;
using UnityEngine;

namespace Sekai
{
	public class FrictionFlickNoteView : FlickNoteView
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

			float progress = LiveConfig.GetNoteViewProgress(note.Progress);
			transform.localPosition = CalcNotePosition(ref progress);
			transform.localScale = Vector3.one * progress;
			UpdateFrictionSpritePayload(progress);
			UpdateFlickArrow();
		}

		private void UpdateFrictionSpritePayload(float progress)
		{
			if (spriteRenderer == null || note == null || judgmentPositions == null || judgmentPositions.Length == 0)
			{
				return;
			}

			Color color = spriteRenderer.color;
			color.r = (((float)note.LaneStart + note.LaneEnd) * 0.5f + 0.5f) / judgmentPositions.Length;
			color.g = ((float)(note.LaneEnd - note.LaneStart) + 0.5f) / judgmentPositions.Length;
			color.b = progress;
			spriteRenderer.color = color;
			if (centerIconRenderer != null)
			{
				centerIconRenderer.color = color;
			}
		}

		private void UpdateFlickArrow()
		{
			if (currentMoveRoot == null || currentArrow == null)
			{
				return;
			}

			float t = Mathf.Repeat(MusicScore.CurrentFrameInfo.time * ScrollCount, 1f);
			float y = t * 2f;
			currentMoveRoot.localPosition = Vector3.up * y;
			float alphaT = Mathf.Clamp01(y - 1f);
			currentArrow.color = new Color(1f, 1f, 1f, 1f - alphaT);
		}

		public FrictionFlickNoteView()
		{
		}
	}
}
