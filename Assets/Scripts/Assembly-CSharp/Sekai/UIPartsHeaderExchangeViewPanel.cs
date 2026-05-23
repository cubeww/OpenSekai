using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	public class UIPartsHeaderExchangeViewPanel : MonoBehaviour
	{
		[SerializeField]
		private CustomTextMesh _title;

		[SerializeField]
		private MaterialExchangeHeaderCeilItem _ceilItem;

		public void SetActive(bool active)
		{
			gameObject.SetActive(active);
		}

		public void UpdateView(string title, int num, string iconName)
		{
			throw null;
		}

		public UIPartsHeaderExchangeViewPanel()
		{
		}
	}
}
