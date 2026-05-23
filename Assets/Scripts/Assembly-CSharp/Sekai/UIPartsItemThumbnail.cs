using System;
using CP;
using Sekai.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Sekai
{
	public class UIPartsItemThumbnail : UIPartsThumbnail
	{
		[SerializeField]
		protected CustomButton button;

		[SerializeField]
		protected ClickDetector clickDetector;

		[SerializeField]
		private CustomImage frameImage;

		[SerializeField]
		private Mask frameMask;

		[SerializeField]
		private CustomImage thumbnailBase;

		[SerializeField]
		private GameObject _zoomIcon;

		private SeType mysekaiHashTagSEType;

		public MasterResourceBox resourceBox;

		public Mask FrameMask
		{
			get
			{
				throw null;
			}
		}

		public bool VisibleMask
		{
			set
			{
				throw null;
			}
		}

		public bool VisibleFrame
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

		public bool Interactable
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

		public bool VisibleZoomIcon
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

		protected virtual void Awake()
		{
			throw null;
		}

		public void SetupCountText(int count)
		{
			throw null;
		}

		public void SetSeName(string seName)
		{
			throw null;
		}

		public void SetupInnerTextFontAsset()
		{
			throw null;
		}

		public void Setup(UserResource userResource, Action<AssetManager.BundleElement> onSuccess, Action<AssetManager.BundleElement> onError)
		{
			throw null;
		}

		public void Setup(UserResource userResource, Action<AssetManager.BundleElement> onSuccess = null, Action<AssetManager.BundleElement> onError = null, Action<UserResource> onClick = null)
		{
			throw null;
		}

		public void Setup(string assetbundleName, Action<AssetManager.BundleElement> onSuccess = null)
		{
			throw null;
		}

		public void Setup(string assetBundleName, string fileName, Action<AssetManager.BundleElement> onSuccess = null, Action<AssetManager.BundleElement> onError = null)
		{
			throw null;
		}

		public void Setup(UserResource userResource, bool isTapToDetail)
		{
			throw null;
		}

		public void Setup(MasterResourceBox resourceBox, bool isTapToDetail = false)
		{
			throw null;
		}

		public void Setup(string assetBundleName, string fileName, bool isTapToDetail)
		{
			throw null;
		}

		public void Setup(MasterCostume3DModel costume3DModel)
		{
			throw null;
		}

		private void SetupMysekaiFixture(int textureId, Action<AssetManager.BundleElement> onSuccess, Action<AssetManager.BundleElement> onError)
		{
			throw null;
		}

		public void SetupMysekaiFixture(UserResource userResource, int textureId, Action<AssetManager.BundleElement> onSuccess, Action<AssetManager.BundleElement> onError)
		{
			throw null;
		}

		public void ShowInnerText(bool isShow)
		{
			throw null;
		}

		public void SetInnerColor(Color color)
		{
			throw null;
		}

		public void SetButtonSE(SeType seType)
		{
			throw null;
		}

		public void SetHashTagSE(SeType seType)
		{
			throw null;
		}

		public void RegisterCallbackOnClickThumbnail(Action onClick)
		{
			throw null;
		}

		public void EnableTap()
		{
			throw null;
		}

		public void DisableTap()
		{
			throw null;
		}

		public void SetClickDetectorRaycastPaddng(Vector4 raycastPadding)
		{
			throw null;
		}

		protected void OnLongPress()
		{
			throw null;
		}

		private void OpenDetailDialog()
		{
			throw null;
		}

		private void OpenFixtureDescriptionDialog()
		{
			throw null;
		}

		private void OpenItemDetailDialog()
		{
			throw null;
		}

		private void OpenResourceBoxDetailDialog()
		{
			throw null;
		}

		protected void SetupMysekaiBlueprintThumbnail(int blueprintId, Action<AssetManager.BundleElement> onSuccess = null, Action<AssetManager.BundleElement> onError = null)
		{
			throw null;
		}

		public UIPartsItemThumbnail()
		{
			throw null;
		}
	}
}
