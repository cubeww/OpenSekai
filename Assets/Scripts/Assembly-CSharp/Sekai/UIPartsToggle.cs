using UnityEngine;

namespace Sekai
{
	public class UIPartsToggle : UIPartsToggleBase
	{
		[SerializeField]
		private GameObject desableGroup;

		[SerializeField]
		private GameObject offGroup;

		[SerializeField]
		private GameObject onGroup;

		protected override void UpdateView(State state)
		{
			if (desableGroup != null)
			{
				desableGroup.SetActive(state == State.Disable);
			}
			if (offGroup != null)
			{
				offGroup.SetActive(state == State.Off);
			}
			if (onGroup != null)
			{
				onGroup.SetActive(state == State.On);
			}
		}

		public UIPartsToggle()
		{
		}
	}
}
