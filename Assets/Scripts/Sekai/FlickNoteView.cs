using Sekai.Live;
using UnityEngine;

namespace Sekai
{
	public class FlickNoteView : BaseNoteView
	{
		protected static readonly float ScrollCount = 2f;

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
			if (note == null)
			{
				return;
			}

			int spriteIndex = 0;
			if (defaultArrowSprites != null && defaultArrowSprites.Length > 0)
			{
				spriteIndex = Mathf.Clamp(note.DefaultRightLane - note.DefaultLeftLane, 0, defaultArrowSprites.Length - 1);
				if (defaultArrow != null)
				{
					defaultArrow.sprite = defaultArrowSprites[spriteIndex];
				}
			}
			else if (defaultArrow != null)
			{
				defaultArrow.sprite = null;
			}

			if (slanArrowSprites != null && slanArrowSprites.Length > 0)
			{
				spriteIndex = Mathf.Clamp(spriteIndex, 0, slanArrowSprites.Length - 1);
				if (slanArrow != null)
				{
					slanArrow.sprite = slanArrowSprites[spriteIndex];
				}
			}
			else if (slanArrow != null)
			{
				slanArrow.sprite = null;
			}

			bool slan = note.Direction != NoteDirection.Default;
			if (defaultArrow != null)
			{
				defaultArrow.transform.localScale = slan ? Vector3.zero : Vector3.one;
			}
			if (slanArrow != null)
			{
				slanArrow.transform.localScale = slan ? Vector3.one : Vector3.zero;
			}

			currentArrow = slan ? slanArrow : defaultArrow;
			currentMoveRoot = slan ? slanMoveRoot : defaultMoveRoot;

			if (slanRoot != null)
			{
				slanRoot.localScale = note.Direction == NoteDirection.Left ? LiveUtility.Vector3One : LiveUtility.Vector3OneMinusX;
				slanRoot.localEulerAngles = note.Direction == NoteDirection.Left ? LiveUtility.FlickLeftEulerAngles : LiveUtility.FlickRightEulerAngles;
			}
		}

		public override void Move()
		{
			base.Move();
			float scroll = Mathf.Repeat(MusicScore.CurrentFrameInfo.time * ScrollCount, 1f);
			if (currentMoveRoot != null)
			{
				currentMoveRoot.localPosition = LiveConfig.Vector3Up * (scroll * 2f);
			}
			if (currentArrow != null)
			{
				currentArrow.color = Color.Lerp(Color.white, new Color(1f, 1f, 1f, 0f), Mathf.Clamp01(scroll * 2f - 1f));
			}
		}
	}
}
