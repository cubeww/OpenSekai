using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	public class MaterialExchangeHeaderCeilItem : MonoBehaviour
	{
		public class ViewData
		{
			public string AssetBundleName;

			public string AssetBundleAtlasName;

			public string IconName;

			public int Number;

			public ViewData()
			{
			}
		}

		[SerializeField]
		private CustomImage _recoveryIcon;

		[SerializeField]
		private UIAtlasImageLoader _atlasLoader;

		[SerializeField]
		private CustomTextMesh _number;

		private ViewData _viewData;

		public void Setup(ViewData data, bool setNativeSize = false)
		{
			_viewData = data;
			_atlasLoader.gameObject.SetActive(false);
			_recoveryIcon.SetActive(false);

			_atlasLoader.Load(data.AssetBundleName, data.AssetBundleAtlasName, data.IconName, delegate
			{
				OnSuccessLoadAtlas(setNativeSize);
			}, delegate
			{
				OnErrorLoadAtlas(setNativeSize);
			});
			_number.SetText(data.Number.ToString());
		}

		private void OnSuccessLoadAtlas(bool setNativeSize)
		{
			_atlasLoader.gameObject.SetActive(true);
			_recoveryIcon.SetActive(false);
			if (setNativeSize)
			{
				_atlasLoader.AtlasImage.SetNativeSize();
			}
		}

		private void OnErrorLoadAtlas(bool setNativeSize)
		{
			_atlasLoader.gameObject.SetActive(false);
			_recoveryIcon.SetActive(true);
			_recoveryIcon.SpriteName = _viewData.IconName;

			if (setNativeSize)
			{
				_recoveryIcon.SetNativeSize();
			}

			if (_recoveryIcon.sprite == null)
			{
				string atlasName = _recoveryIcon.Atlas != null ? _recoveryIcon.Atlas.name : "_recoveryIconにアトラスが設定されていません";
				CP.LogUtility.LogError(
					_viewData.IconName + "が見つかりませんでした。 参照先[" +
					_viewData.AssetBundleName + "/" + _viewData.AssetBundleAtlasName + "], [" + atlasName + "]");
			}
		}

		public MaterialExchangeHeaderCeilItem()
		{
		}
	}
}
