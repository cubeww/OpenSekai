using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	public class UIPartsBalloon : MonoBehaviour
	{
		[SerializeField]
		private bool enableHideOnAwake;

		[SerializeField]
		private CanvasGroup canvasGroup;

		[SerializeField]
		private CustomTextMesh message;

		[SerializeField]
		private Vector2 minSize;

		public bool Enable
		{
			set
			{
				throw null;
			}
		}

		public string Message
		{
			get
			{
				throw null;
			}
			set
			{
				throw null;
			}
		}

		public string MessageText
		{
			get
			{
				throw null;
			}
			set
			{
				throw null;
			}
		}

		private void Awake()
		{
			if (enableHideOnAwake && canvasGroup != null)
			{
				canvasGroup.alpha = 0f;
			}
		}

		public void Fade(float endValue, float duration)
		{
			throw null;
		}

		public void SetTextPosition(Vector2 pos)
		{
			throw null;
		}

		private void FitText()
		{
			throw null;
		}

		public UIPartsBalloon()
		{
			enableHideOnAwake = true;
			minSize = Vector2.zero;
		}
	}
}
