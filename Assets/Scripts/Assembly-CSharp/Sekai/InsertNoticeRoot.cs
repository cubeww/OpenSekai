using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Beebyte.Obfuscator;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using UnityEngine;

namespace Sekai
{
	public class InsertNoticeRoot : MonoBehaviour
	{
		public enum AnimeState
		{
			None = 0,
			In = 1,
			Move = 2,
			Out = 3
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CMoveAnime_003Ed__26 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public InsertNoticeRoot _003C_003E4__this;

			public Vector3 from;

			public Vector3 to;

			private UniTask.Awaiter _003C_003Eu__1;

			private void MoveNext()
			{
				throw null;
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
				throw null;
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CPlayInAnime_003Ed__27 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public InsertNoticeRoot _003C_003E4__this;

			private UniTask.Awaiter _003C_003Eu__1;

			private void MoveNext()
			{
				throw null;
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
				throw null;
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CPlayOutAnime_003Ed__28 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public InsertNoticeRoot _003C_003E4__this;

			private UniTask.Awaiter _003C_003Eu__1;

			private void MoveNext()
			{
				throw null;
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
				throw null;
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[SerializeField]
		private TweenPosition inTweenPos;

		[SerializeField]
		private TweenPosition outTweenPos;

		[SerializeField]
		private TweenAlpha alphaTweener;

		[SerializeField]
		private float moveAnimationSec;

		[SerializeField]
		private AchivedMissionNoticeCell achivedMissionNotice;

		[SerializeField]
		private FriendRequestNoticeCell friendRequestNotice;

		[SerializeField]
		private FriendInviteNoticeCell friendInviteNotice;

		[SerializeField]
		private LiveBonusNoticeCell liveBonusNotice;

		[SerializeField]
		private OtherNoticeCountCell otherNoticeCountCell;

		private AnimeState latestAnimeState;

		public AchivedMissionNoticeCell AchivedMissionNotice
		{
			get
			{
				return achivedMissionNotice;
			}
		}

		public FriendRequestNoticeCell FriendRequestNotice
		{
			get
			{
				return friendRequestNotice;
			}
		}

		public FriendInviteNoticeCell FriendInviteNotice
		{
			get
			{
				return friendInviteNotice;
			}
		}

		public OtherNoticeCountCell OtherNoticeCountCell
		{
			get
			{
				return otherNoticeCountCell;
			}
		}

		public LiveBonusNoticeCell LiveBonusNoticeCell
		{
			get
			{
				return liveBonusNotice;
			}
		}

		public AnimeState LatestAnimeState
		{
			[CompilerGenerated]
			get
			{
				return latestAnimeState;
			}
			[CompilerGenerated]
			private set
			{
				latestAnimeState = value;
			}
		}

		[Skip]
		public void Refresh()
		{
			SetNoticeActive(achivedMissionNotice, false);
			SetNoticeActive(friendRequestNotice, false);
			SetNoticeActive(otherNoticeCountCell, false);
			SetNoticeActive(liveBonusNotice, false);
			SetNoticeActive(friendInviteNotice, false);
			ResetTween();
		}

		public void ResetTween()
		{
			if (inTweenPos != null && transform is RectTransform rectTransform)
			{
				rectTransform.anchoredPosition = new Vector2(inTweenPos.From.x, 0f);
			}

			if (alphaTweener != null)
			{
				alphaTweener.Stop();
				if (alphaTweener.TweenTarget == null)
				{
					alphaTweener.SetupTarget();
				}

				if (alphaTweener.TweenTarget != null && alphaTweener.TweenTarget.IsExist)
				{
					alphaTweener.TweenTarget.Alpha = alphaTweener.From;
				}
			}

			LatestAnimeState = AnimeState.None;
		}

		[AsyncStateMachine(typeof(_003CMoveAnime_003Ed__26))]
		public UniTask MoveAnime(Vector3 from, Vector3 to)
		{
			if (transform is RectTransform rectTransform)
			{
				rectTransform.anchoredPosition = to;
			}
			else
			{
				transform.localPosition = to;
			}

			LatestAnimeState = AnimeState.Move;
			return UniTask.CompletedTask;
		}

		[AsyncStateMachine(typeof(_003CPlayInAnime_003Ed__27))]
		public UniTask PlayInAnime()
		{
			LatestAnimeState = AnimeState.In;
			inTweenPos?.Play(TweenBase.PlayDirection.Forward);
			alphaTweener?.Play(TweenBase.PlayDirection.Forward);
			return UniTask.CompletedTask;
		}

		[AsyncStateMachine(typeof(_003CPlayOutAnime_003Ed__28))]
		public UniTask PlayOutAnime()
		{
			LatestAnimeState = AnimeState.Out;
			outTweenPos?.Play(TweenBase.PlayDirection.Forward);
			alphaTweener?.Play(TweenBase.PlayDirection.Back);
			return UniTask.CompletedTask;
		}

		private void RefreshHorizontalTweener(ref TweenPosition tweener)
		{
			if (tweener == null)
			{
				return;
			}

			var from = tweener.From;
			var to = tweener.To;
			from.y = 0f;
			to.y = 0f;
			tweener.From = from;
			tweener.To = to;
		}

		private static void SetNoticeActive(Component component, bool active)
		{
			if (component != null)
			{
				component.gameObject.SetActive(active);
			}
		}

		public InsertNoticeRoot()
		{
		}
	}
}
