using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Sekai.MusicScoreMaker.OutGame.Common.Content;

namespace Sekai.MusicScoreMaker.OutGame.Created.Content
{
	public sealed class PublishedMusicScoreListSelectContent : ContentPresenterBase<PublishedMusicScoreListSelectBootData, PublishedMusicScoreListSelectViewData, PublishedMusicScoreListSelectView, PublishedMusicScoreListSelectModel>
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CExecuteDeleteMusicScoreAsync_003Ed__12 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public PublishedMusicScoreListSelectContent _003C_003E4__this;

			private MusicScoreData _003CmusicScoreData_003E5__2;

			private UniTask<bool>.Awaiter _003C_003Eu__1;

			private UniTask.Awaiter _003C_003Eu__2;

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
		private struct _003COnPostBootAsync_003Ed__1 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public PublishedMusicScoreListSelectContent _003C_003E4__this;

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
		private struct _003COnPostExitAsync_003Ed__2 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public PublishedMusicScoreListSelectContent _003C_003E4__this;

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
		private struct _003COnPostExitDetailAsync_003Ed__9 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public CancellationToken ct;

			public PublishedMusicScoreListSelectContent _003C_003E4__this;

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
		private struct _003COnPreEnterDetailAsync_003Ed__8 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public CancellationToken ct;

			public PublishedMusicScoreListSelectContent _003C_003E4__this;

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

		private bool _isExecutingDeleteMusicScore;

		[AsyncStateMachine(typeof(_003COnPostBootAsync_003Ed__1))]
		protected override UniTask OnPostBootAsync(CancellationToken ct)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003COnPostExitAsync_003Ed__2))]
		protected override UniTask OnPostExitAsync(CancellationToken ct)
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

		private void ApplySelectedMusicScore(int dataIndex)
		{
			throw null;
		}

		private void ApplySortOrder(SortOrderBy sortOrder)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003COnPreEnterDetailAsync_003Ed__8))]
		private UniTask OnPreEnterDetailAsync(CancellationToken ct)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003COnPostExitDetailAsync_003Ed__9))]
		private UniTask OnPostExitDetailAsync(CancellationToken ct)
		{
			throw null;
		}

		private void OnClickDeleteButton()
		{
			throw null;
		}

		private void OnDecideButtonClicked()
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CExecuteDeleteMusicScoreAsync_003Ed__12))]
		private UniTask ExecuteDeleteMusicScoreAsync()
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

		public override void Dispose()
		{
			throw null;
		}

		public PublishedMusicScoreListSelectContent()
		{
			throw null;
		}
	}
}
