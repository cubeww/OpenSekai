using UnityEngine;
using UnityEngine.UI;

namespace Sekai.UI
{
	public class ScrollMask : MonoBehaviour
	{
		[SerializeField]
		private RectMask2D mask;

		[SerializeField]
		private float maskThreshold;

		public Vector2 MaskSoftness
		{
			get
			{
				return mask != null ? new Vector2(mask.softness.x, mask.softness.y) : Vector2.zero;
			}
		}

		public void SetMaskVertical(Vector2 leftBottomViewportPos, Vector2 rightTopViewportPos, Vector2 leftBottomContentPos, Vector2 rightTopContentPos)
		{
			if (mask == null || maskThreshold <= 0f)
			{
				return;
			}

			var halfSoftness = mask.softness.y * 0.5f;
			var topRate = Mathf.Min(Mathf.Abs(rightTopViewportPos.y - rightTopContentPos.y), maskThreshold) / maskThreshold;
			var bottomRate = Mathf.Min(Mathf.Abs(leftBottomContentPos.y - leftBottomViewportPos.y), maskThreshold) / maskThreshold;
			mask.padding = new Vector4(0f, halfSoftness * bottomRate - halfSoftness, 0f, halfSoftness * topRate - halfSoftness);
		}

		public void SetMaskHorizontal(Vector2 leftBottomViewportPos, Vector2 rightTopViewportPos, Vector2 leftBottomContentPos, Vector2 rightTopContentPos)
		{
			if (mask == null || maskThreshold <= 0f)
			{
				return;
			}

			var halfSoftness = mask.softness.x * 0.5f;
			var leftRate = Mathf.Min(Mathf.Abs(leftBottomContentPos.x - leftBottomViewportPos.x), maskThreshold) / maskThreshold;
			var rightRate = Mathf.Min(Mathf.Abs(rightTopViewportPos.x - rightTopContentPos.x), maskThreshold) / maskThreshold;
			mask.padding = new Vector4(halfSoftness * leftRate - halfSoftness, 0f, halfSoftness * rightRate - halfSoftness, 0f);
		}

		public void SetMaskEnabled(bool maskEnabled)
		{
			if (mask != null)
			{
				mask.enabled = maskEnabled;
			}
		}

		public void ResetMaskPadding(bool isVertical)
		{
			if (isVertical)
			{
				SetMaskVertical(Vector2.zero, Vector2.zero, Vector2.zero, Vector2.zero);
			}
			else
			{
				SetMaskHorizontal(Vector2.zero, Vector2.zero, Vector2.zero, Vector2.zero);
			}
		}

		public ScrollMask()
		{
		}
	}
}
