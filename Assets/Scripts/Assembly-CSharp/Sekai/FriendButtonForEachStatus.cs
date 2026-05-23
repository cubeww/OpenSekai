using System;
using Beebyte.Obfuscator;
using Sekai.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Sekai
{
	public class FriendButtonForEachStatus : MonoBehaviour
	{
		[Serializable]
		public class StatusEvent : UnityEvent<FriendUtility.ApprovalStatusType>
		{
			public StatusEvent()
			{
			}
		}

		[Serializable]
		private struct StatusInfo
		{
			public FriendUtility.ApprovalStatusType targetStatus;

			public string targetWordKey;

			public GameObject icon;
		}

		[SerializeField]
		private CustomButton button;

		[SerializeField]
		private CustomText text;

		[SerializeField]
		private StatusInfo[] infoes;

		private FriendUtility.ApprovalStatusType currentStatus;

		public Button.ButtonClickedEvent OnClick
		{
			get
			{
				throw null;
			}
		}

		public bool Enable
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

		public void RemoveAllAndAddListener(Action onClick)
		{
			throw null;
		}

		[Skip]
		public void Refresh(FriendUtility.ApprovalStatusType status)
		{
			throw null;
		}

		private void RefreshInternal(ref StatusInfo info)
		{
			throw null;
		}

		private void Awake()
		{
			throw null;
		}

		public FriendButtonForEachStatus()
		{
		}
	}
}
