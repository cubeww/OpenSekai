using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using DG.Tweening;
using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	public class UIPartsThumbnail : MonoBehaviour
	{
		public enum Size
		{
			S = 0,
			M = 1,
			MM = 2,
			ML = 3,
			L = 4,
			LM = 5,
			LL = 6,
			XL = 7,
			Manual = 8
		}

		public enum LabelType
		{
			None = 0,
			Limited = 1,
			Fes = 2
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CLoadAsync_003Ed__50 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public UIPartsThumbnail _003C_003E4__this;

			public Action<AssetManager.BundleElement> onSuccess;

			public string bundleName;

			public string fileName;

			public LruAssetCacheGeneral<UnityEngine.Object> lruCache;

			public CancellationToken ct;

			public Action<AssetManager.BundleElement> onError;

			private UniTask<(bool isSuccess, AssetManager.BundleElement element)>.Awaiter _003C_003Eu__1;

			private void MoveNext()
			{
				throw null;
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
				throw null;
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		private const float SIZE_S = 0.3f;

		private const float SIZE_M = 0.66f;

		private const float SIZE_MM = 0.8f;

		private const float SIZE_ML = 0.85f;

		private const float SIZE_L = 1f;

		private const float SIZE_LM = 1.083f;

		private const float SIZE_LL = 1.2f;

		private const float SIZE_XL = 1.5f;

		[SerializeField]
		protected CustomRawImage thumbnailImage;

		[SerializeField]
		protected UITextureLoader textureLoader;

		[SerializeField]
		protected CustomTextMesh innerText;

		[SerializeField]
		protected CanvasGroup thumbnailCanvasGroup;

		[SerializeField]
		protected GameObject disableCover;

		[SerializeField]
		private bool preserveAspect;

		[SerializeField]
		private CustomImage labelImage;

		private Action<AssetManager.BundleElement> onLoadSuccessEvent;

		protected UserResource userResource;

		public CustomRawImage ThumbnailImage
		{
			get
			{
				throw null;
			}
		}

		public UserResource UserResource
		{
			get
			{
				throw null;
			}
		}

		public CustomTextMesh InterText
		{
			get
			{
				throw null;
			}
		}

		public bool VisibleInnerText
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

		public bool Enabled
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

		public bool PreserveAspect
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

		public AssetManager.BundleElementLoadStatus LoadStatus
		{
			get
			{
				throw null;
			}
		}

		public LruAssetCacheLoadingState LruCacheState
		{
			get
			{
				throw null;
			}
		}

		public int ItemQuantity
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			private set
			{
				throw null;
			}
		}

		public void SetSize(Size size)
		{
			throw null;
		}

		private float GetSize()
		{
			throw null;
		}

		public void SetAlpha(float alpha)
		{
			throw null;
		}

		public void Show()
		{
			throw null;
		}

		public void Hide()
		{
			throw null;
		}

		public Sequence ShowThumbnailWithEase(float duration, float delay, float startSizeMultiply = 1.2f, Size size = Size.L, Action onFinished = null)
		{
			throw null;
		}

		public void SetSizeDelta(Vector2 size)
		{
			throw null;
		}

		public virtual void Load(string bundleName, string fileName, Action<AssetManager.BundleElement> onSuccess = null, Action<AssetManager.BundleElement> onError = null, LruAssetCacheGeneral<UnityEngine.Object> lruCache = null)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CLoadAsync_003Ed__50))]
		public virtual UniTask LoadAsync(string bundleName, string fileName, CancellationToken ct, Action<AssetManager.BundleElement> onSuccess = null, Action<AssetManager.BundleElement> onError = null, LruAssetCacheGeneral<UnityEngine.Object> lruCache = null)
		{
			throw null;
		}

		protected void OnLoadSuccess(AssetManager.BundleElement element)
		{
			throw null;
		}

		public void SetQuantity(int num)
		{
			throw null;
		}

		public void SetQuantityWithColor(int quantity, Color numberColor)
		{
			throw null;
		}

		public void SetLabelType(LabelType labelType)
		{
			throw null;
		}

		public void SetNativeSize()
		{
			throw null;
		}

		public void Unload()
		{
			throw null;
		}

		public void ClearTexture()
		{
			throw null;
		}

		public void Stretch()
		{
			throw null;
		}

		public void ResetLoadingObject()
		{
			throw null;
		}

		public void SetDisableCover(bool value)
		{
			throw null;
		}

		public UIPartsThumbnail()
		{
			throw null;
		}
	}
}
