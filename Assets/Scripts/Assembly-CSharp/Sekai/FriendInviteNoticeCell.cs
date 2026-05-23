using System;
using System.Runtime.CompilerServices;
using Beebyte.Obfuscator;
using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	public class FriendInviteNoticeCell : MonoBehaviour
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
		private CustomButton requestRejectButton;

		[SerializeField]
		private CustomButton requestApproveButton;

		[SerializeField]
		private CustomImage symbolImage;

		public Action OnRequestApprove
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			set
			{
				throw null;
			}
		}

		public Action OnRequestReject
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			set
			{
				throw null;
			}
		}

		public string InvitationId
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

		public InvitationType InvitationType
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

		public string RoomId
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

		[Skip]
		public void Refresh(ReceiveFriendInviteViewData inviteViewData, string message)
		{
			throw null;
		}

		public void SetEnableButton(bool enable)
		{
			throw null;
		}

		public void EnableRejectContent()
		{
			throw null;
		}

		public void EnableApproveContent()
		{
			throw null;
		}

		private void Awake()
		{
			throw null;
		}

		private void OnDestroy()
		{
			throw null;
		}

		public FriendInviteNoticeCell()
		{
		}
	}
}
