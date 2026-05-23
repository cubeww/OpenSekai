using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Sekai.MusicScoreMaker.OutGame.Common.Content;
using Sekai.Sound;

namespace Sekai.MusicScoreMaker.OutGame.Create.Content
{
	public sealed class MyMusicScoreSelectForCreateContent : ContentPresenterBase<MyMusicScoreSelectForCreateBootData, MyMusicScoreSelectForCreateViewData, MyMusicScoreSelectForCreateView, MyMusicScoreSelectForCreateModel>
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass9_0
		{
			[StructLayout((LayoutKind)3)]
			private struct _003C_003CExecuteCreateMusicScoreAsync_003Eb__0_003Ed : IAsyncStateMachine
			{
				public int _003C_003E1__state;

				public AsyncUniTaskMethodBuilder _003C_003Et__builder;

				public _003C_003Ec__DisplayClass9_0 _003C_003E4__this;

				public float fadeTime;

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

			public IngameBGMPlayer bgmPlayer;

			public _003C_003Ec__DisplayClass9_0()
			{
				throw null;
			}

			[AsyncStateMachine(typeof(_003C_003CExecuteCreateMusicScoreAsync_003Eb__0_003Ed))]
			internal UniTask _003CExecuteCreateMusicScoreAsync_003Eb__0(float fadeTime)
			{
				throw null;
			}
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CExecuteCreateMusicScoreAsync_003Ed__9 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MyMusicScoreSelectForCreateContent _003C_003E4__this;

			private _003C_003Ec__DisplayClass9_0 _003C_003E8__1;

			private MusicScoreData _003CmusicScoreData_003E5__2;

			private UniTask<Sekai.MusicScoreMaker.Ingame.Models.MusicScoreMakerData>.Awaiter _003C_003Eu__1;

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
		private struct _003COnPostBootAsync_003Ed__0 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MyMusicScoreSelectForCreateContent _003C_003E4__this;

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
		private struct _003COnPostExitAsync_003Ed__1 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MyMusicScoreSelectForCreateContent _003C_003E4__this;

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
		private struct _003COnPostExitDetailAsync_003Ed__7 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public CancellationToken ct;

			public MyMusicScoreSelectForCreateContent _003C_003E4__this;

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
		private struct _003COnPreEnterDetailAsync_003Ed__6 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public CancellationToken ct;

			public MyMusicScoreSelectForCreateContent _003C_003E4__this;

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

		[AsyncStateMachine(typeof(_003COnPostBootAsync_003Ed__0))]
		protected override UniTask OnPostBootAsync(CancellationToken ct)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003COnPostExitAsync_003Ed__1))]
		protected override UniTask OnPostExitAsync(CancellationToken ct)
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

		[AsyncStateMachine(typeof(_003COnPreEnterDetailAsync_003Ed__6))]
		private UniTask OnPreEnterDetailAsync(CancellationToken ct)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003COnPostExitDetailAsync_003Ed__7))]
		private UniTask OnPostExitDetailAsync(CancellationToken ct)
		{
			throw null;
		}

		private void OnDecide()
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CExecuteCreateMusicScoreAsync_003Ed__9))]
		private UniTask ExecuteCreateMusicScoreAsync()
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

		public MyMusicScoreSelectForCreateContent()
		{
			throw null;
		}
	}
}
