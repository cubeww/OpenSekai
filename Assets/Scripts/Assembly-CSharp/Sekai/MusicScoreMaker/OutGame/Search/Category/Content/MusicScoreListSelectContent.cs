using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Sekai.MusicScoreMaker.OutGame.Common;
using Sekai.MusicScoreMaker.OutGame.Common.Content;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame.Search.Category.Content
{
	public class MusicScoreListSelectContent : ContentPresenterBase<MusicScoreListSelectBootData, MusicScoreListSelectViewData, MusicScoreListSelectView, MusicScoreListSelectModel>
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CExecuteWithLoadingAsync_003Ed__16 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MusicScoreListSelectContent _003C_003E4__this;

			public CancellationToken ct;

			public Func<CancellationToken, UniTask> modelOperation;

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
		private struct _003COnPostBootAsync_003Ed__3 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public CancellationToken ct;

			public MusicScoreListSelectContent _003C_003E4__this;

			private CancellationTokenSource _003Ccts_003E5__2;

			private CancellationToken _003ClinkedCt_003E5__3;

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
		private struct _003COnPostExitAsync_003Ed__6 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MusicScoreListSelectContent _003C_003E4__this;

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
		private struct _003COnPostExitDetailAsync_003Ed__5 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public CancellationToken ct;

			public MusicScoreListSelectContent _003C_003E4__this;

			private CancellationTokenSource _003CcallbackCts_003E5__2;

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
		private struct _003COnPreBootAsync_003Ed__1 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MusicScoreListSelectContent _003C_003E4__this;

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
		private struct _003COnPreEnterDetailAsync_003Ed__4 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public CancellationToken ct;

			public MusicScoreListSelectContent _003C_003E4__this;

			private CancellationTokenSource _003CcallbackCts_003E5__2;

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
		private struct _003CRefreshViewAsync_003Ed__21 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MusicScoreListSelectContent _003C_003E4__this;

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
		private MusicScoreInfo _musicScoreInfo;

		[AsyncStateMachine(typeof(_003COnPreBootAsync_003Ed__1))]
		protected override UniTask OnPreBootAsync(CancellationToken ct)
		{
			throw null;
		}

		protected override UniTask PlayBootAnimationAsync(CancellationToken ct)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003COnPostBootAsync_003Ed__3))]
		protected override UniTask OnPostBootAsync(CancellationToken ct)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003COnPreEnterDetailAsync_003Ed__4))]
		private UniTask OnPreEnterDetailAsync(CancellationToken ct)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003COnPostExitDetailAsync_003Ed__5))]
		private UniTask OnPostExitDetailAsync(CancellationToken ct)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003COnPostExitAsync_003Ed__6))]
		protected override UniTask OnPostExitAsync(CancellationToken ct)
		{
			throw null;
		}

		private void UpdateSelectedMusicScore(int dataIndex)
		{
			throw null;
		}

		private void OnClickFilterButton()
		{
			throw null;
		}

		private void OnFiltered(MusicScoreFilterData filterData)
		{
			throw null;
		}

		private void ApplySortOrder(SortOrderBy sortOrder)
		{
			throw null;
		}

		private void OnBookMarkButtonClicked(string id, bool isBookmarked)
		{
			throw null;
		}

		private void OnMusicScoreReviewed(string id)
		{
			throw null;
		}

		private void OnToCreatorButtonClicked()
		{
			throw null;
		}

		private void OnDecideButtonClicked()
		{
			throw null;
		}

		private void OnMusicScoreDeleted(string id)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CExecuteWithLoadingAsync_003Ed__16))]
		private UniTask ExecuteWithLoadingAsync(Func<CancellationToken, UniTask> modelOperation, CancellationToken ct)
		{
			throw null;
		}

		public override void OnWillExit()
		{
			throw null;
		}

		protected override void OnPause()
		{
			throw null;
		}

		protected override void OnResume()
		{
			throw null;
		}

		public override void Dispose()
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CRefreshViewAsync_003Ed__21))]
		private UniTask RefreshViewAsync()
		{
			throw null;
		}

		public MusicScoreListSelectContent()
		{
			throw null;
		}
	}
}
