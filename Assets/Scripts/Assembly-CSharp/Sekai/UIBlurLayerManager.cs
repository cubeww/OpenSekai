using System.Threading;
using Cysharp.Threading.Tasks;
using Sekai.Rendering;
using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	public class UIBlurLayerManager : MonoBehaviour
	{
		public enum Mode
		{
			None = -1,
			OnShot = 0,
			Always = 1
		}

		[SerializeField]
		private Canvas canvas;

		[SerializeField]
		private CustomImage image;

		private CancellationToken cancellationToken;
		private Mode captureMode = Mode.OnShot;

		public Mode CaptureMode
		{
			get => captureMode;
			private set
			{
				if (captureMode == value)
				{
					return;
				}

				captureMode = value;
				if (image != null && image.enabled)
				{
					Enable();
				}
			}
		}

		public bool BlurExecuting => SekaiUIEffectSettings.Blur.IsActive;
		public bool IsEnabledBlurImage => image != null && image.enabled && image.gameObject.activeInHierarchy;

		private void Awake()
		{
			InitializeReferences();
			cancellationToken = gameObject.GetCancellationTokenOnDestroy();
			if (image != null)
			{
				image.enabled = false;
				image.raycastTarget = false;
			}
		}

		public void SetBaseCanvas(Canvas baseCanvas)
		{
			SetCanvas(baseCanvas);
		}

		public void SetCanvas(Canvas baseCanvas)
		{
			InitializeReferences();
			if (canvas == null || baseCanvas == null)
			{
				return;
			}

			canvas.renderMode = baseCanvas.renderMode;
			canvas.worldCamera = baseCanvas.worldCamera;
			canvas.planeDistance = baseCanvas.planeDistance;
			canvas.pixelPerfect = baseCanvas.pixelPerfect;
			canvas.overrideSorting = baseCanvas.overrideSorting;
			canvas.overridePixelPerfect = baseCanvas.overridePixelPerfect;
			canvas.sortingLayerID = baseCanvas.sortingLayerID;
			canvas.sortingOrder = baseCanvas.sortingOrder;
			canvas.targetDisplay = baseCanvas.targetDisplay;
		}

		public UniTask SetCaptureMode(Mode mode)
		{
			if (mode == Mode.None)
			{
				captureMode = mode;
				Disable();
				return UniTask.CompletedTask;
			}

			CaptureMode = mode;
			return UniTask.CompletedTask;
		}

		public void Enable()
		{
			InitializeReferences();
			if (CaptureMode == Mode.None)
			{
				return;
			}

			StartBlurCaptureAsync(SekaiUIEffectSettings.Blur, cancellationToken).Forget();
		}

		public void Disable()
		{
			SekaiUIEffectSettings.Blur.IsActive = false;
			if (image != null)
			{
				image.enabled = false;
			}
		}

		private async UniTaskVoid StartBlurCaptureAsync(SekaiUIBlurParameter blur, CancellationToken token)
		{
			token.ThrowIfCancellationRequested();
			if (image != null)
			{
				image.enabled = true;
				image.raycastTarget = false;
			}

			blur.IsActive = true;
			if (CaptureMode != Mode.OnShot)
			{
				return;
			}

			await UniTask.WaitForEndOfFrame(this, token);
			blur.IsActive = false;
		}

		private void InitializeReferences()
		{
			if (canvas == null)
			{
				canvas = GetComponent<Canvas>();
			}
			if (image == null)
			{
				image = GetComponentInChildren<CustomImage>(true);
			}
		}
	}
}
