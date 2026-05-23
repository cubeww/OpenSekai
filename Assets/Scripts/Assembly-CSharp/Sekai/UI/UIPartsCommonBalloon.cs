using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Sekai.UI
{
	public class UIPartsCommonBalloon : MonoBehaviour
	{
		[Header("外部タップで閉じることが可能か")]
		[SerializeField]
		private bool isAllowedClose;

		[SerializeField]
		private CanvasGroup canvasGroup;

		[SerializeField]
		private CustomTextMesh mainText;

		[SerializeField]
		private TouchController touchController;

		[Space(24f)]
		[Header("ランタイムで自動サイズ調整をするか")]
		[SerializeField]
		private bool adjustSize;

		[Header("吹き出し付のベース画像")]
		[SerializeField]
		private RectTransform baseCenterRt;

		[SerializeField]
		[Header("スライスする左右のベース画像")]
		private RectTransform[] baseSideRts;

		[Header("テキスト表示のパディング")]
		[SerializeField]
		private RectOffset padding;

		[Header("バルーン自体の最大サイズ")]
		[SerializeField]
		private Vector2 maxSize;

		[Header("テキスト自動サイズ調整用")]
		[SerializeField]
		private ContentSizeFitter textSizeFitter;

		[Header("テキスト自動レイアウト用")]
		[SerializeField]
		private LayoutElement textLayoutElement;

		private bool _needsCanvas;

		private Tweener tween;

		protected RectTransform MainTextRt
		{
			get
			{
				return mainText != null ? mainText.RectTransform : null;
			}
		}

		public bool IsHide
		{
			get
			{
				return canvasGroup == null || canvasGroup.alpha <= 0f;
			}
		}

		public void Initialize()
		{
			if (isAllowedClose && touchController != null)
			{
				touchController.SetHitTarget(gameObject);
				touchController.OnHit = CheckClose;
			}

			Hide(false);
		}

		public void Setup(string text, bool needsCanvas = true, bool useShowAnimation = true)
		{
			_needsCanvas = needsCanvas;
			if (mainText != null)
			{
				mainText.SetText(text);
			}

			if (adjustSize)
			{
				AdjustSize(padding);
			}

			Show(useShowAnimation);
		}

		public void SetupFontAsset()
		{
			mainText?.SetDefaultFontDB();
		}

		private void AdjustSize()
		{
			AdjustSize(padding);
		}

		protected virtual void AdjustSize(RectOffset padding)
		{
			if (mainText == null || padding == null)
			{
				return;
			}

			Canvas.ForceUpdateCanvases();
			if (textSizeFitter != null)
			{
				textSizeFitter.enabled = false;
			}

			RectTransform textRt = mainText.RectTransform;
			if (textLayoutElement != null && maxSize.x > 0f)
			{
				textLayoutElement.preferredWidth = Mathf.Min(textRt.rect.width, Mathf.Max(0f, maxSize.x - padding.horizontal));
			}

			if (textLayoutElement != null && maxSize.y > 0f)
			{
				textLayoutElement.preferredHeight = Mathf.Min(textRt.rect.height, Mathf.Max(0f, maxSize.y - padding.vertical));
			}

			if (textSizeFitter != null)
			{
				textSizeFitter.enabled = true;
			}

			Canvas.ForceUpdateCanvases();
			Vector2 size = new Vector2(textRt.rect.width + padding.horizontal, textRt.rect.height + padding.vertical);
			RectTransform selfRt = transform as RectTransform;
			if (selfRt != null)
			{
				selfRt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x);
				selfRt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.y);
			}

			if (baseCenterRt != null && baseSideRts != null)
			{
				float sideWidth = Mathf.Max(0f, (size.x - baseCenterRt.rect.width) * 0.5f);
				foreach (RectTransform sideRt in baseSideRts)
				{
					if (sideRt != null)
					{
						sideRt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, sideWidth);
					}
				}
			}
		}

		private void CheckClose(bool isHit)
		{
			if (!isHit)
			{
				Hide();
			}
		}

		public void Show(bool useAnimation = true)
		{
			SetCanvas();
			if (canvasGroup == null)
			{
				return;
			}

			canvasGroup.blocksRaycasts = true;
			tween?.Kill(false);
			if (useAnimation)
			{
				tween = canvasGroup.DOFade(1f, 0.1f);
			}
			else
			{
				canvasGroup.alpha = 1f;
			}
		}

		public void Hide(bool useAnimation = true)
		{
			if (canvasGroup == null)
			{
				RemoveCanvas();
				return;
			}

			tween?.Kill(false);
			if (useAnimation)
			{
				tween = canvasGroup.DOFade(0f, 0.1f).OnComplete(() =>
				{
					canvasGroup.blocksRaycasts = false;
					RemoveCanvas();
				});
			}
			else
			{
				canvasGroup.alpha = 0f;
				canvasGroup.blocksRaycasts = false;
				RemoveCanvas();
			}
		}

		private void SetCanvas()
		{
			if (!_needsCanvas || GetComponent<Canvas>() != null)
			{
				return;
			}

			Canvas canvas = gameObject.AddComponent<Canvas>();
			canvas.overrideSorting = true;
		}

		private void RemoveCanvas()
		{
			if (!_needsCanvas)
			{
				return;
			}

			Canvas canvas = GetComponent<Canvas>();
			if (canvas != null)
			{
				Destroy(canvas);
			}
		}

		public UIPartsCommonBalloon()
		{
		}
	}
}
