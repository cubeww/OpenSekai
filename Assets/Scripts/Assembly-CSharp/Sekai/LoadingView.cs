using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	public class LoadingView : MonoBehaviour
	{
		[SerializeField]
		private CustomTextMesh title;

		[SerializeField]
		private CustomTextMesh description;

		[SerializeField]
		private UITextureLoader imageLoader;

		private MasterTip masterTip;

		public void Setup(MasterTip tip)
		{
			masterTip = tip;
			if (tip == null)
			{
				return;
			}

			if (tip.GetViewType() == MasterTip.ViewType.TYPE_TEXT_TIPS)
			{
				if (title != null)
				{
					title.SetText(tip.title ?? string.Empty);
				}
				if (description != null)
				{
					description.SetText(tip.description ?? string.Empty);
				}
			}
			else if (imageLoader != null && !string.IsNullOrEmpty(tip.assetbundleName))
			{
				imageLoader.Load("comic/one_frame", tip.assetbundleName);
			}
		}

		public void SetDescription(string text)
		{
			if (description != null)
			{
				description.SetText(text ?? string.Empty);
			}
		}

		public void Unload()
		{
			if (masterTip != null && masterTip.GetViewType() == MasterTip.ViewType.TYPE_COMIC)
			{
				imageLoader?.Unload();
			}
		}

		public LoadingView()
		{
		}
	}
}
