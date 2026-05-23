using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	[ExecuteInEditMode]
	public class UIPartsImageOutline : MonoBehaviour
	{
		[SerializeField]
		private CustomImage _targetImage;

		[SerializeField]
		private float _outline;

		private RectTransform _targetTransform;

		private RectTransform _myTransform;

		private void Awake()
		{
			SetupTransforms();
		}

		private void SetupTransforms()
		{
			_targetTransform = _targetImage != null ? _targetImage.transform as RectTransform : null;
			_myTransform = transform as RectTransform;
		}

		private void Update()
		{
			if (_targetTransform == null || _myTransform == null)
			{
				SetupTransforms();
				if (_targetTransform == null || _myTransform == null)
				{
					return;
				}
			}

			_myTransform.anchoredPosition = _targetTransform.anchoredPosition;
			Vector2 targetSize = _targetTransform.sizeDelta;
			Vector3 targetScale = _targetTransform.localScale;
			_myTransform.sizeDelta = new Vector2(
				targetSize.x * targetScale.x + _outline,
				targetSize.y * targetScale.y + _outline);
		}

		public UIPartsImageOutline()
		{
		}
	}
}
