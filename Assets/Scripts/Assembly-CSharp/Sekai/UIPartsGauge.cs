using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	public class UIPartsGauge : MonoBehaviour
	{
		[SerializeField]
		protected CustomImage fillImage;

		[SerializeField]
		private CustomImage gaugeImage;

		public float Progress
		{
			get
			{
				throw null;
			}
		}

		public virtual void Setup(int now, int max)
		{
			throw null;
		}

		public virtual void Setup(float now, float max)
		{
			throw null;
		}

		public virtual void Setup(float progress)
		{
			throw null;
		}

		public virtual void ChangeGaugeColor(Color color)
		{
			throw null;
		}

		public UIPartsGauge()
		{
		}
	}
}
