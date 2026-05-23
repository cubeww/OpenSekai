using System;
using UnityEngine;

namespace Sekai.UI
{
	public sealed class UIPartsReleaseConditionsBalloon : UIPartsCommonBalloon
	{
		public class ViewData
		{
			public string Description { get; }

			public MasterReleaseCondition MasterReleaseCondition { get; }

			public Action OnTransition { get; }

			public bool IsTransitionable
			{
				get
				{
					return OnTransition != null;
				}
			}

			public bool NeedsCanvas { get; }

			public ViewData(int releaseConditionId, bool needsCanvas = true, int extraId = 0)
			{
				NeedsCanvas = needsCanvas;
				Description = releaseConditionId > 0 ? releaseConditionId.ToString() : string.Empty;
				// TODO(original): restore MasterDataManager.GetMasterReleaseCondition and transition route creation.
			}

			public ViewData(string description, Action onTransition = null, bool needsCanvas = true)
			{
				NeedsCanvas = needsCanvas;
				Description = description;
				OnTransition = onTransition;
			}
		}

		[Header("判定用の遷移ボタン")]
		[SerializeField]
		private CustomButton transitionButton;

		[SerializeField]
		[Header("遷移ボタン自体のRectTransform")]
		private RectTransform _transitionButtonRt;

		[Header("遷移ボタン用Offset")]
		[SerializeField]
		private int _transitionButtonOffset;

		private ViewData _viewData;

		public void Setup(ViewData viewData)
		{
			_viewData = viewData;
			SetupTransitionButton();
			base.Setup(viewData != null ? viewData.Description : string.Empty, viewData == null || viewData.NeedsCanvas);
		}

		protected override void AdjustSize(RectOffset padding)
		{
			base.AdjustSize(padding);
			if (_transitionButtonRt != null && _viewData != null && _viewData.IsTransitionable)
			{
				_transitionButtonRt.anchoredPosition = new Vector2(_transitionButtonRt.anchoredPosition.x, -_transitionButtonOffset);
			}
		}

		private void SetupTransitionButton()
		{
			if (transitionButton == null)
			{
				return;
			}

			bool enabled = _viewData != null && _viewData.IsTransitionable;
			transitionButton.gameObject.SetActive(enabled);
			transitionButton.onClick.RemoveListener(OnTransition);
			if (enabled)
			{
				transitionButton.onClick.AddListener(OnTransition);
			}
		}

		private void OnTransition()
		{
			_viewData?.OnTransition?.Invoke();
		}

		public UIPartsReleaseConditionsBalloon()
		{
		}
	}
}
