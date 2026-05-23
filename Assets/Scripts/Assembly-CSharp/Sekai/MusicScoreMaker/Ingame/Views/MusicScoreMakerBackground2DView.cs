using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Sekai.MusicScoreMaker.Ingame.Views
{
	public class MusicScoreMakerBackground2DView : MonoBehaviour
	{
		[CompilerGenerated]
		private sealed class _003CDeactivateNextFrame_003Ed__17 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MusicScoreMakerBackground2DView _003C_003E4__this;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return _003C_003E2__current;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return _003C_003E2__current;
				}
			}

			[DebuggerHidden]
			public _003CDeactivateNextFrame_003Ed__17(int _003C_003E1__state)
			{
				this._003C_003E1__state = _003C_003E1__state;
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				if (_003C_003E1__state == 0)
				{
					_003C_003E1__state = 1;
					_003C_003E2__current = null;
					return true;
				}
				if (_003C_003E1__state == 1)
				{
					_003C_003E1__state = -1;
					if (_003C_003E4__this != null)
					{
						_003C_003E4__this.gameObject.SetActive(false);
					}
				}
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CDeactivateNextFrameAsync_003Ed__18 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MusicScoreMakerBackground2DView _003C_003E4__this;

			private Cysharp.Threading.Tasks.YieldAwaitable.Awaiter _003C_003Eu__1;

			private void MoveNext()
			{
				try
				{
					if (_003C_003E4__this != null)
					{
						_003C_003E4__this.DeactivateNextFrameAsyncCore().Forget();
					}
					_003C_003Et__builder.SetResult();
				}
				catch (Exception exception)
				{
					_003C_003Et__builder.SetException(exception);
				}
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
				_003C_003Et__builder.SetStateMachine(stateMachine);
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CRenderOnEnableAsync_003Ed__20 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MusicScoreMakerBackground2DView _003C_003E4__this;

			private UniTask.Awaiter _003C_003Eu__1;

			private void MoveNext()
			{
				try
				{
					if (_003C_003E4__this != null)
					{
						_003C_003E4__this.RenderOnEnableAsyncCore().Forget();
					}
					_003C_003Et__builder.SetResult();
				}
				catch (Exception exception)
				{
					_003C_003Et__builder.SetException(exception);
				}
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
				_003C_003Et__builder.SetStateMachine(stateMachine);
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CSetJacketAsync_003Ed__26 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MusicScoreMakerBackground2DView _003C_003E4__this;

			public int musicId;

			public CancellationToken ct;

			private bool _003CwasActive_003E5__2;

			private (string assetBundleName, string fileName) _003Cinfo_003E5__3;

			private UniTask<Texture2D>.Awaiter _003C_003Eu__1;

			private void MoveNext()
			{
				try
				{
					if (_003C_003E4__this != null)
					{
						_003C_003E4__this.SetJacketAsyncCore(musicId, ct).Forget();
					}
					_003C_003Et__builder.SetResult();
				}
				catch (Exception exception)
				{
					_003C_003Et__builder.SetException(exception);
				}
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
				_003C_003Et__builder.SetStateMachine(stateMachine);
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CSetupAsync_003Ed__15 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MusicScoreMakerBackground2DView _003C_003E4__this;

			public int musicId;

			public CancellationToken ct;

			private UniTask.Awaiter _003C_003Eu__1;

			private void MoveNext()
			{
				try
				{
					if (_003C_003E4__this != null)
					{
						_003C_003E4__this.SetupAsyncCore(musicId, ct).Forget();
					}
					_003C_003Et__builder.SetResult();
				}
				catch (Exception exception)
				{
					_003C_003Et__builder.SetException(exception);
				}
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
				_003C_003Et__builder.SetStateMachine(stateMachine);
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[FormerlySerializedAs("jacketCamera")]
		[SerializeField]
		private Camera backgroundCamera;

		[SerializeField]
		private SpriteRenderer backgroundRenderer;

		[FormerlySerializedAs("jackets")]
		[SerializeField]
		private SpriteRenderer[] _jacketRenderers;

		[SerializeField]
		private RenderTexture backgroundTexture;

		[SerializeField]
		private RawImage displayRawImage;

		[SerializeField]
		private int textureWidth;

		[SerializeField]
		private int textureHeight;

		[SerializeField]
		private bool createRenderTexture;

		private const string DEFAULT_BACKGROUND_ASSET_BUNDLE = "live/2dmode/background/default";

		private const string DEFAULT_BACKGROUND_FILE_NAME = "default";

		private RenderTexture _createdRenderTexture;

		private string _currentJacketAssetBundleName;

		private bool _isInitialized;

		private bool _needsRendering;

		private Texture2D _externalJacketTexture;

		private Sprite _externalJacketSprite;

		public void Setup()
		{
			SetupAsync(0).Forget();
		}

		[AsyncStateMachine(typeof(_003CSetupAsync_003Ed__15))]
		public UniTask SetupAsync(int musicId, CancellationToken ct = default(CancellationToken))
		{
			return SetupAsyncCore(musicId, ct);
		}

		[Obsolete("Use SetupAsync instead")]
		public void Setup(int musicId)
		{
			SetupAsync(musicId).Forget();
		}

		[IteratorStateMachine(typeof(_003CDeactivateNextFrame_003Ed__17))]
		private IEnumerator DeactivateNextFrame()
		{
			yield return null;
			gameObject.SetActive(false);
		}

		[AsyncStateMachine(typeof(_003CDeactivateNextFrameAsync_003Ed__18))]
		private UniTask DeactivateNextFrameAsync()
		{
			return DeactivateNextFrameAsyncCore();
		}

		private void OnEnable()
		{
			if (_isInitialized && _needsRendering)
			{
				RenderOnEnableAsync().Forget();
			}
		}

		[AsyncStateMachine(typeof(_003CRenderOnEnableAsync_003Ed__20))]
		private UniTask RenderOnEnableAsync()
		{
			return RenderOnEnableAsyncCore();
		}

		private void AdjustCameraClipPlanes()
		{
			if (backgroundCamera == null || backgroundRenderer == null)
			{
				return;
			}

			float distance = Vector3.Distance(backgroundCamera.transform.position, backgroundRenderer.transform.position);
			if (distance < 1f)
			{
				backgroundCamera.nearClipPlane = Mathf.Max(distance * 0.1f, 0.01f);
				backgroundCamera.farClipPlane = Mathf.Max(backgroundCamera.nearClipPlane + 0.1f, distance * 3f);
			}
		}

		private void SetupRenderTexture()
		{
			if (backgroundCamera == null)
			{
				return;
			}

			int width = Screen.width > 0 ? Screen.width : Mathf.Max(textureWidth, 1);
			int height = Screen.height > 0 ? Screen.height : Mathf.Max(textureHeight, 1);
			if (createRenderTexture)
			{
				if (_createdRenderTexture != null && (_createdRenderTexture.width != width || _createdRenderTexture.height != height))
				{
					_createdRenderTexture.Release();
					Destroy(_createdRenderTexture);
					_createdRenderTexture = null;
				}
				if (_createdRenderTexture == null)
				{
					_createdRenderTexture = new RenderTexture(width, height, 24)
					{
						name = "MusicScoreMakerBackground2DView"
					};
				}
				backgroundTexture = _createdRenderTexture;
			}
			else if (backgroundTexture != null && (backgroundTexture.width != width || backgroundTexture.height != height))
			{
				backgroundTexture.Release();
				backgroundTexture.width = width;
				backgroundTexture.height = height;
				backgroundTexture.Create();
			}

			if (backgroundTexture != null)
			{
				backgroundCamera.targetTexture = backgroundTexture;
				if (displayRawImage != null)
				{
					displayRawImage.texture = backgroundTexture;
				}
			}
		}

		private void RenderBackground()
		{
			if (backgroundCamera != null)
			{
				backgroundCamera.Render();
				_needsRendering = false;
			}
		}

		public RenderTexture GetBackgroundTexture()
		{
			return backgroundTexture;
		}

		private void LoadDefaultBackground()
		{
			if (backgroundRenderer == null)
			{
				return;
			}

			Sprite sprite = AssetBundleUtility.LoadAsset<Sprite>(DEFAULT_BACKGROUND_ASSET_BUNDLE, DEFAULT_BACKGROUND_FILE_NAME);
			if (sprite != null)
			{
				backgroundRenderer.sprite = sprite;
				_needsRendering = true;
			}

			if (backgroundRenderer.gameObject != null)
			{
				backgroundRenderer.gameObject.SetActive(true);
			}
		}

		[AsyncStateMachine(typeof(_003CSetJacketAsync_003Ed__26))]
		public UniTask SetJacketAsync(int musicId, CancellationToken ct = default(CancellationToken))
		{
			return SetJacketAsyncCore(musicId, ct);
		}

		[Obsolete("Use SetJacketAsync instead")]
		public void SetJacket(int musicId)
		{
			SetJacketAsync(musicId).Forget();
		}

		public void ClearJacket()
		{
			if (_jacketRenderers == null)
			{
				ClearExternalJacketAssets();
				return;
			}
			foreach (SpriteRenderer jacketRenderer in _jacketRenderers)
			{
				if (jacketRenderer != null)
				{
					jacketRenderer.sprite = null;
					jacketRenderer.gameObject.SetActive(false);
				}
			}
			ClearExternalJacketAssets();
			_needsRendering = true;
		}

		public async UniTask SetJacketFileAsync(string jacketPath, CancellationToken ct = default(CancellationToken))
		{
			ct.ThrowIfCancellationRequested();
			if (!_isInitialized)
			{
				SetupRenderTexture();
				LoadDefaultBackground();
				AdjustCameraClipPlanes();
				_isInitialized = true;
			}
			ClearJacket();
			if (string.IsNullOrEmpty(jacketPath) || !File.Exists(jacketPath))
			{
				await UniTask.Yield(PlayerLoopTiming.Update, ct);
				return;
			}

			byte[] bytes = File.ReadAllBytes(jacketPath);
			Texture2D texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
			if (!texture.LoadImage(bytes))
			{
				Destroy(texture);
				return;
			}

			SetJacketTexture(texture);
			await UniTask.Yield(PlayerLoopTiming.Update, ct);
		}

		private void SetJacketTexture(Texture2D texture)
		{
			if (texture == null || _jacketRenderers == null || _jacketRenderers.Length == 0)
			{
				if (texture != null)
				{
					Destroy(texture);
				}
				return;
			}

			_externalJacketTexture = texture;
			_externalJacketSprite = Sprite.Create(
				texture,
				new Rect(0f, 0f, texture.width, texture.height),
				new Vector2(0.5f, 0.5f),
				100f);

			foreach (SpriteRenderer jacketRenderer in _jacketRenderers)
			{
				if (jacketRenderer != null)
				{
					jacketRenderer.sprite = _externalJacketSprite;
					jacketRenderer.gameObject.SetActive(true);
				}
			}

			_needsRendering = true;
			if (gameObject.activeInHierarchy)
			{
				RenderBackground();
			}
		}

		private void ClearExternalJacketAssets()
		{
			if (_externalJacketSprite != null)
			{
				Destroy(_externalJacketSprite);
				_externalJacketSprite = null;
			}
			if (_externalJacketTexture != null)
			{
				Destroy(_externalJacketTexture);
				_externalJacketTexture = null;
			}
		}

		public void SetBrightness(float brightness)
		{
			bool wasActive = gameObject.activeSelf;
			if (!wasActive)
			{
				gameObject.SetActive(true);
			}

			float clampedBrightness = Mathf.Clamp01(brightness);
			Color color = new Color(clampedBrightness, clampedBrightness, clampedBrightness, 1f);
			if (backgroundRenderer != null)
			{
				backgroundRenderer.color = color;
			}
			if (_jacketRenderers != null)
			{
				foreach (SpriteRenderer jacketRenderer in _jacketRenderers)
				{
					if (jacketRenderer != null)
					{
						jacketRenderer.color = color;
					}
				}
			}

			_needsRendering = true;
			if (gameObject.activeSelf)
			{
				RenderBackground();
				if (!wasActive)
				{
					DeactivateNextFrameAsync().Forget();
				}
			}
		}

		public void Dispose()
		{
			_isInitialized = false;
			_needsRendering = false;
			ClearJacket();
			_currentJacketAssetBundleName = null;
			AssetBundleUtility.UnloadAssetBundle(DEFAULT_BACKGROUND_ASSET_BUNDLE);
			if (backgroundRenderer != null)
			{
				backgroundRenderer.sprite = null;
			}
			if (displayRawImage != null)
			{
				displayRawImage.texture = null;
			}
			if (_createdRenderTexture != null)
			{
				if (backgroundCamera != null)
				{
					backgroundCamera.targetTexture = null;
				}
				_createdRenderTexture.Release();
				Destroy(_createdRenderTexture);
				_createdRenderTexture = null;
			}
		}

		private async UniTask SetupAsyncCore(int musicId, CancellationToken ct)
		{
			_isInitialized = false;
			_needsRendering = false;
			SetupRenderTexture();
			ClearJacket();
			LoadDefaultBackground();
			AdjustCameraClipPlanes();
			if (musicId >= 1)
			{
				await SetJacketAsyncCore(musicId, ct);
			}
			if (!ct.IsCancellationRequested)
			{
				_isInitialized = true;
				_needsRendering = true;
				RenderBackground();
			}
		}

		private async UniTask SetJacketAsyncCore(int musicId, CancellationToken ct)
		{
			ct.ThrowIfCancellationRequested();
			ClearJacket();

			// Original code resolves the jacket bundle/file from MasterMusicAllModel and
			// AssetBundleUtility. That data path is not restored yet, so keep the default
			// background and leave the jacket layer empty for now.
			_currentJacketAssetBundleName = null;
			await UniTask.Yield(PlayerLoopTiming.Update, ct);
			_needsRendering = true;
			if (gameObject.activeInHierarchy)
			{
				RenderBackground();
			}
		}

		private async UniTask DeactivateNextFrameAsyncCore()
		{
			await UniTask.Yield(PlayerLoopTiming.Update);
			if (this != null)
			{
				gameObject.SetActive(false);
			}
		}

		private async UniTask RenderOnEnableAsyncCore()
		{
			await UniTask.Yield(PlayerLoopTiming.Update);
			if (_needsRendering)
			{
				RenderBackground();
			}
		}

		public MusicScoreMakerBackground2DView()
		{
		}
	}
}
