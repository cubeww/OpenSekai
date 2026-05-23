using UnityEngine;

namespace Sekai.UI
{
	public class MultiButtonTapEffect : ButtonViewInteractionBase
	{
		[SerializeField]
		private ButtonViewInteractionBase[] _tapEffectList;

		public override void OnButtonViewEnabled()
		{
			ForEachEffect(effect => effect.OnButtonViewEnabled());
		}

		public override void OnButtonViewDisabled()
		{
			ForEachEffect(effect => effect.OnButtonViewDisabled());
		}

		public override void OnPressed()
		{
			ForEachEffect(effect => effect.OnPressed());
		}

		public override void OnReleased()
		{
			ForEachEffect(effect => effect.OnReleased());
		}

		public override void KillFadeTween()
		{
			ForEachEffect(effect => effect.KillFadeTween());
		}

		public override void ResetInteraction()
		{
			ForEachEffect(effect => effect.ResetInteraction());
		}

		private void ForEachEffect(System.Action<ButtonViewInteractionBase> action)
		{
			if (_tapEffectList == null)
			{
				return;
			}

			foreach (ButtonViewInteractionBase effect in _tapEffectList)
			{
				if (effect != null)
				{
					action(effect);
				}
			}
		}

		public MultiButtonTapEffect()
		{
		}
	}
}
