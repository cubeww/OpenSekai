using DG.Tweening;
using UnityEngine;

namespace Sekai.MusicScoreMaker.Outgame
{
	public abstract class MusicScoreListCellContentsBase : MonoBehaviour
	{
		[SerializeField]
		protected CanvasGroup _canvasGroup;

		[SerializeField]
		protected RectTransform _contentRectTransform;

		public void SetActive(bool isActive)
		{
			throw null;
		}

		public Tween DoChangeHighlight(bool isActive, float duration)
		{
			throw null;
		}

		public void ChangeVisible(bool isActive)
		{
			throw null;
		}

		protected MusicScoreListCellContentsBase()
		{
			throw null;
		}
	}
}
