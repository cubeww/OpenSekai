using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Sekai.MusicScoreMaker.OutGame.Common.Content;
using Sekai.MusicScoreMaker.Outgame;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame.Search.Category.Content
{
	public class MusicScoreCreatorTopView : ContentViewBase<MusicScoreCreatorTopViewData>
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CFadeInCreatorInfoAsync_003Ed__9 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MusicScoreCreatorTopView _003C_003E4__this;

			public float duration;

			public CancellationToken ct;

			private TweenAwaiter _003C_003Eu__1;

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
		private struct _003CFadeInListAsync_003Ed__13 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MusicScoreCreatorTopView _003C_003E4__this;

			public float duration;

			public CancellationToken ct;

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
		private struct _003CFadeInListContentAsync_003Ed__11 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MusicScoreCreatorTopView _003C_003E4__this;

			public float duration;

			public CancellationToken ct;

			private TweenAwaiter _003C_003Eu__1;

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
		private struct _003CFadeOutCreatorInfoAsync_003Ed__10 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MusicScoreCreatorTopView _003C_003E4__this;

			public float duration;

			public CancellationToken ct;

			private TweenAwaiter _003C_003Eu__1;

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
		private struct _003CFadeOutListAsync_003Ed__14 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MusicScoreCreatorTopView _003C_003E4__this;

			public float duration;

			public CancellationToken ct;

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
		private struct _003CFadeOutListContentAsync_003Ed__12 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MusicScoreCreatorTopView _003C_003E4__this;

			public float duration;

			public CancellationToken ct;

			private TweenAwaiter _003C_003Eu__1;

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
		private struct _003CRefreshAsync_003Ed__7 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MusicScoreCreatorTopView _003C_003E4__this;

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
		private struct _003CRefreshListAsync_003Ed__8 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MusicScoreCreatorTopView _003C_003E4__this;

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
		private struct _003CSetupAsync_003Ed__6 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MusicScoreCreatorTopView _003C_003E4__this;

			public Action onClickToProfileButton;

			public Action<int> onSelectMusicScore;

			public Action onClickFilterButton;

			public Action<SortOrderBy> onChangeSortOrder;

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
		private MusicScoreCreatorTopCreatorInfoView _creatorInfoView;

		[SerializeField]
		private CommonMusicScoreListView _musicScoreListView;

		[SerializeField]
		private UIPartsFilterButton _musicScoreFilterButton;

		[SerializeField]
		private UIPartsSortOrder _musicScoreSortOrderButton;

		[SerializeField]
		private CanvasGroup _creatorInfoCanvasGroup;

		[SerializeField]
		private CanvasGroup _listContentCanvasGroup;

		[AsyncStateMachine(typeof(_003CSetupAsync_003Ed__6))]
		public UniTask SetupAsync(Action<int> onSelectMusicScore, Action onClickFilterButton, Action onClickToProfileButton, Action<SortOrderBy> onChangeSortOrder)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CRefreshAsync_003Ed__7))]
		public override UniTask RefreshAsync()
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CRefreshListAsync_003Ed__8))]
		public UniTask RefreshListAsync()
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CFadeInCreatorInfoAsync_003Ed__9))]
		public UniTask FadeInCreatorInfoAsync(float duration = 0.2f, CancellationToken ct = default(CancellationToken))
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CFadeOutCreatorInfoAsync_003Ed__10))]
		public UniTask FadeOutCreatorInfoAsync(float duration = 0.2f, CancellationToken ct = default(CancellationToken))
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CFadeInListContentAsync_003Ed__11))]
		public UniTask FadeInListContentAsync(float duration = 0.2f, CancellationToken ct = default(CancellationToken))
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CFadeOutListContentAsync_003Ed__12))]
		public UniTask FadeOutListContentAsync(float duration = 0.2f, CancellationToken ct = default(CancellationToken))
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CFadeInListAsync_003Ed__13))]
		public UniTask FadeInListAsync(float duration = 0.2f, CancellationToken ct = default(CancellationToken))
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CFadeOutListAsync_003Ed__14))]
		public UniTask FadeOutListAsync(float duration = 0.2f, CancellationToken ct = default(CancellationToken))
		{
			throw null;
		}

		public UniTask PlayCellStaggerFadeInAsync(CancellationToken ct = default(CancellationToken))
		{
			throw null;
		}

		public void UpdateCellBookmarkState(string musicScoreId, bool isBookmarked)
		{
			throw null;
		}

		public void RefreshCreatorName()
		{
			throw null;
		}

		public void RefreshVisibleCells()
		{
			throw null;
		}

		private void SetupFilterButtonIcon(bool isFiltered)
		{
			throw null;
		}

		public override void Dispose()
		{
			throw null;
		}

		public MusicScoreCreatorTopView()
		{
			throw null;
		}
	}
}
