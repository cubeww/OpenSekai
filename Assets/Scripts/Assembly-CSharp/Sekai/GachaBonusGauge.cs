using DG.Tweening;
using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	public class GachaBonusGauge : MonoBehaviour
	{
		[SerializeField]
		private CustomImage bodyImage;

		[SerializeField]
		private CustomImage maskImage;

		[SerializeField]
		private CustomTextMesh valueText;

		private UserGachaBonusPoint current;

		private Tweener animationTweener;

		public void Setup(int gachaId)
		{
			throw null;
		}

		public void SetupAnimation(int gachaId, int spinCount, float delay, bool isOnce)
		{
			throw null;
		}

		public void StopAnimation()
		{
			throw null;
		}

		private void UpdateGauge(float value, bool updateText = true)
		{
			throw null;
		}

		public void Empty()
		{
			throw null;
		}

		public GachaBonusGauge()
		{
		}
	}
}
