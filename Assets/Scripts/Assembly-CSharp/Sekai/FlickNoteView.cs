using Sekai.Live;
using UnityEngine;

namespace Sekai
{
	public class FlickNoteView : BaseNoteView
	{
		protected static readonly float ScrollCount;

		[SerializeField]
		private Sprite[] defaultArrowSprites;

		[SerializeField]
		private Sprite[] slanArrowSprites;

		[SerializeField]
		private SpriteRenderer defaultArrow;

		[SerializeField]
		private SpriteRenderer slanArrow;

		[SerializeField]
		private Transform defaultMoveRoot;

		[SerializeField]
		private Transform slanMoveRoot;

		[SerializeField]
		private Transform slanRoot;

		protected SpriteRenderer currentArrow;

		protected Transform currentMoveRoot;

		public override void Spawn(INote note, float posZ)
		{
			base.Spawn(note, posZ);

			int widthIndex = Mathf.Clamp(note.LaneEnd - note.LaneStart, 0, Mathf.Max(0, (defaultArrowSprites?.Length ?? 1) - 1));
			bool useSlan = note.Direction != NoteDirection.Default;
			SetArrow(defaultArrow, defaultArrowSprites, widthIndex, !useSlan);
			SetArrow(slanArrow, slanArrowSprites, widthIndex, useSlan);

			currentArrow = useSlan ? slanArrow : defaultArrow;
			currentMoveRoot = useSlan ? slanMoveRoot : defaultMoveRoot;

			Transform directionRoot = useSlan ? slanRoot : currentArrow != null ? currentArrow.transform : null;
			if (directionRoot != null)
			{
				if (note.Direction == NoteDirection.Left)
				{
					directionRoot.localScale = LiveUtility.Vector3One;
					directionRoot.localEulerAngles = LiveUtility.FlickLeftEulerAngles;
				}
				else
				{
					directionRoot.localScale = LiveUtility.Vector3OneMinusX;
					directionRoot.localEulerAngles = LiveUtility.FlickRightEulerAngles;
				}
			}
		}

		public override void Move()
		{
			base.Move();
			if (currentMoveRoot == null || currentArrow == null)
			{
				return;
			}

			float t = Mathf.Repeat(MusicScore.CurrentFrameInfo.time * ScrollCount, 1f);
			float y = t * 2f;
			currentMoveRoot.localPosition = Vector3.up * y;
			float alphaT = Mathf.Clamp01(y - 1f);
			currentArrow.color = Color.Lerp(ColorUtility.WHITE_ALPHA_0, ColorUtility.WHITE_ALPHA_1, alphaT);
		}

		public FlickNoteView()
		{
		}

		static FlickNoteView()
		{
			ScrollCount = 2f;
		}

		private static void SetArrow(SpriteRenderer renderer, Sprite[] sprites, int index, bool visible)
		{
			if (renderer == null)
			{
				return;
			}

			renderer.transform.localScale = visible ? Vector3.one : Vector3.zero;
			if (sprites != null && sprites.Length > 0)
			{
				renderer.sprite = sprites[Mathf.Clamp(index, 0, sprites.Length - 1)];
			}
		}
	}
}
