using System;
using System.Runtime.CompilerServices;
using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	public class FriendRequestNoticeCell : MonoBehaviour
	{
		[SerializeField]
		private GameObject requestRoot;

		[SerializeField]
		private GameObject rejectRoot;

		[SerializeField]
		private GameObject approveRoot;

		[SerializeField]
		private CustomTextMesh requestTitleText;

		[SerializeField]
		private CustomTextMesh requestMessageText;

		[SerializeField]
		private UIPartsMarquee requestTextMarquee;

		[SerializeField]
		private CustomButton requestRejectButton;

		[SerializeField]
		private CustomButton requestApproveButton;

		public Action onRequestApprove;

		public Action onRequestReject;

		public UIPartsMarquee RequestTextMarquee
		{
			get
			{
				return requestTextMarquee;
			}
		}

		public int RequestTextLength
		{
			get
			{
				return requestMessageText != null && requestMessageText.Text != null ? requestMessageText.Text.Length : 0;
			}
		}

		public long UserId
		{
			[CompilerGenerated]
			get
			{
				return _UserId;
			}
			[CompilerGenerated]
			private set
			{
				_UserId = value;
			}
		}

		[CompilerGenerated]
		private long _UserId;

		public void Refresh(long userId, string requestTitle, string requestMessage)
		{
			if (rejectRoot != null)
			{
				rejectRoot.SetActive(false);
			}

			if (approveRoot != null)
			{
				approveRoot.SetActive(false);
			}

			if (requestRoot != null)
			{
				requestRoot.SetActive(true);
			}

			UserId = userId;
			requestTitleText?.SetText(requestTitle);
			requestMessageText?.SetText(requestMessage);
		}

		public void SetEnableButton(bool enable)
		{
			if (requestRejectButton != null)
			{
				requestRejectButton.enabled = enable;
			}

			if (requestApproveButton != null)
			{
				requestApproveButton.enabled = enable;
			}
		}

		public void EnableRejectContent()
		{
			if (requestRoot != null)
			{
				requestRoot.SetActive(false);
			}

			if (rejectRoot != null)
			{
				rejectRoot.SetActive(true);
			}
		}

		public void EnableApproveContent()
		{
			if (requestRoot != null)
			{
				requestRoot.SetActive(false);
			}

			if (approveRoot != null)
			{
				approveRoot.SetActive(true);
			}
		}

		private void Awake()
		{
			if (requestRejectButton != null)
			{
				requestRejectButton.onClick.AddListener(() => onRequestReject?.Invoke());
			}

			if (requestApproveButton != null)
			{
				requestApproveButton.onClick.AddListener(() => onRequestApprove?.Invoke());
			}
		}

		private void OnDestroy()
		{
			if (requestApproveButton != null)
			{
				requestApproveButton.onClick.RemoveAllListeners();
			}

			if (requestRejectButton != null)
			{
				requestRejectButton.onClick.RemoveAllListeners();
			}
		}

		public FriendRequestNoticeCell()
		{
		}
	}
}
