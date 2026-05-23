using UnityEngine;

namespace Sekai.UI
{
	public abstract class ButtonViewInteractionBase : MonoBehaviour
	{
		private const float FADE_IN_TIME = 0.1f;

		private const float FADE_OUT_TIME = 0.4f;

		protected virtual float FadeInTime
		{
			get
			{
				return FADE_IN_TIME;
			}
		}

		protected virtual float FadeOutTime
		{
			get
			{
				return FADE_OUT_TIME;
			}
		}

		public virtual void OnButtonViewEnabled()
		{
		}

		public virtual void OnButtonViewDisabled()
		{
			KillFadeTween();
		}

		public virtual void OnPressed()
		{
		}

		public virtual void OnReleased()
		{
		}

		public virtual void KillFadeTween()
		{
		}

		public virtual void ResetInteraction()
		{
		}

		protected virtual void OnDisable()
		{
			KillFadeTween();
		}

		protected virtual void OnDestroy()
		{
			KillFadeTween();
		}

		protected ButtonViewInteractionBase()
		{
		}
	}
}
