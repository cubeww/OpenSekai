using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Sekai
{
	public class ScreenLayerInsertNoti : ScreenLayer
	{
		public enum StateType
		{
			None = 0,
			Mission = 1,
			Friend = 2,
			LiveBonus = 3,
			FriendInvite = 4
		}

		[Serializable]
		public class NoticeWaitTimeInfo
		{
			public float forAchivedMissionTimeSec;
			public float forFriendRequestTimeSec;
			public float forCheckedFriendReqeustTimeSec;
			public float forLiveBonusWaitTimeSec;
			public float forFriendInviteTimeSec;
			public float forCheckedFriendInviteTimeSec;
			public float forHarvestCollectItemTimeSec;
		}

		[SerializeField]
		private List<InsertNoticeRoot> notices = new List<InsertNoticeRoot>();

		[SerializeField]
		private InsertNoticeRoot otherNoticeCountRoot;

		[SerializeField]
		private NoticeWaitTimeInfo noticeWaitTimeInfo;

		public List<(string name, int count)> LiveBonusNotices { get; } = new List<(string name, int count)>();

		public List<InsertNoticeRoot> Notices => notices;

		public InsertNoticeRoot OtherNoticeCountRoot => otherNoticeCountRoot;

		public bool ShowAnyNotice => LiveBonusNotices.Count > 0;

		public bool PauseViewWaitTime { get; private set; }

		public StateType CurrentNoticeType { get; private set; }

		protected override void OnBoot(BootArgBase bootArg)
		{
			if (ScreenManager.Instance != null)
			{
				ScreenManager.Instance.OnChangeUILayer -= OnChangeUILayer;
				ScreenManager.Instance.OnChangeUILayer += OnChangeUILayer;
			}

			ChangeStateTo(StateType.None);
		}

		protected override void OnInitComponent()
		{
			transform.SetSiblingIndex(0);
		}

		public override void OnWillExit()
		{
			ScreenWillExitDone();
		}

		protected override void OnExitStart()
		{
			if (ScreenManager.Instance != null)
			{
				ScreenManager.Instance.OnChangeUILayer -= OnChangeUILayer;
			}
		}

		protected override void OnExitScene()
		{
			CancelAll();
		}

		public void Execute()
		{
		}

		public void ExecuteArchivedMissionNotice()
		{
		}

		public void ExecuteFriendRequestNoritce()
		{
		}

		public void ExecuteFriendInviteNoitce()
		{
		}

		public void ExecuteLiveBonusNotice()
		{
		}

		public void UpdateCharacterMissionData()
		{
		}

		public void UpdateNormalMissionData()
		{
		}

		public void UpdateBeginnerMissionData()
		{
		}

		public void UpdateBeginnerMissionV2Data()
		{
		}

		public void UpdateHonorMissionData()
		{
		}

		public void UpdateEventMissionData()
		{
		}

		public void UpdateFriendRequestData(UserFriend[] newRequests)
		{
		}

		public void AddFriendInviteData(List<UserInvitation> newRequests)
		{
		}

		public void FakeFinishFriendInvite(string invitationId, bool isApprove)
		{
		}

		public void UpdateLiveBonusData(List<(string name, int count)> data)
		{
			LiveBonusNotices.Clear();
			if (data != null)
			{
				LiveBonusNotices.AddRange(data);
			}
		}

		public void CancelFriendNotice()
		{
		}

		public void CancelAchivedMissionNotice()
		{
		}

		public void CancelLiveBonusNotice()
		{
		}

		public void CancelFriendInvite()
		{
		}

		public void CancelAll()
		{
			LiveBonusNotices.Clear();
			ChangeStateTo(StateType.None);
		}

		public void PauseCalcWaitTime()
		{
			PauseViewWaitTime = true;
		}

		public void ResumeCalcWaitTime()
		{
			PauseViewWaitTime = false;
		}

		public void GotoNextState()
		{
			ChangeStateTo(StateType.None);
		}

		public UniTask RegisterWaitTimeSecAsync(CancellationToken cancelToken, StateType type, float maxWaitTime, bool ignorePause)
		{
			return UniTask.CompletedTask;
		}

		private void ChangeStateTo(StateType type)
		{
			CurrentNoticeType = type;
			if (type == StateType.None)
			{
				RefreshAllNotices();
			}
		}

		private void RefreshAllNotices()
		{
			if (notices != null)
			{
				foreach (var notice in notices)
				{
					notice?.Refresh();
				}
			}

			otherNoticeCountRoot?.Refresh();
		}

		private void OnChangeUILayer(MenuScreenType screenType)
		{
			// IDA only uses this to cancel invitation/friend notices for a small set of screens.
			// InvitationUtility is not restored yet, so keep the local layer in a hidden idle state.
			if (screenType == MenuScreenType.MusicScoreMaker)
			{
				ChangeStateTo(StateType.None);
			}
		}
	}
}
