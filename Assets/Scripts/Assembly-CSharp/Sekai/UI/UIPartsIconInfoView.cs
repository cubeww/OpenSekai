using UnityEngine;

namespace Sekai.UI
{
	public sealed class UIPartsIconInfoView : MonoBehaviour
	{
		public class ViewData
		{
			public string InfoTextKey;

			public bool IsLocked;

			public ViewData()
			{
			}
		}

		[SerializeField]
		private GameObject infoTextRoot;

		[SerializeField]
		private CustomTextMesh infoText;

		[SerializeField]
		private GameObject lockedObject;

		public void Setup(ViewData viewData)
		{
			if (viewData == null)
			{
				return;
			}

			SetInfoText(viewData.InfoTextKey);
			if (lockedObject != null)
			{
				lockedObject.SetActive(viewData.IsLocked);
			}
		}

		private void SetInfoText(string infoTextKey)
		{
			bool hasInfo = !string.IsNullOrEmpty(infoTextKey);
			if (infoTextRoot != null)
			{
				infoTextRoot.SetActive(hasInfo);
			}

			if (hasInfo && infoText != null)
			{
				infoText.SetText(WordingManager.Get(infoTextKey));
			}
		}

		public void Show()
		{
			gameObject.SetActive(true);
		}

		public void Hide()
		{
			gameObject.SetActive(false);
		}

		public UIPartsIconInfoView()
		{
		}
	}
}
