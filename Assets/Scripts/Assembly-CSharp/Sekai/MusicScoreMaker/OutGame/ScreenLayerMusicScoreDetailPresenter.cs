using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;

namespace Sekai.MusicScoreMaker.OutGame
{
	public class ScreenLayerMusicScoreDetailPresenter : IDisposable
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CPlayInAnimationAsync_003Ed__5 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public ScreenLayerMusicScoreDetailPresenter _003C_003E4__this;

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

		private ScreenLayerMusicScoreDetailView _view;

		private ScreenLayerMusicScoreDetailModel _model;

		public ScreenLayerMusicScoreDetailPresenter(ScreenLayerMusicScoreDetailView view, MusicScoreData musicScoreData)
		{
			throw null;
		}

		public void RefreshView()
		{
			throw null;
		}

		public void InitializeInAnimation()
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CPlayInAnimationAsync_003Ed__5))]
		public UniTask PlayInAnimationAsync(CancellationToken ct)
		{
			throw null;
		}

		public void OnUpdate()
		{
			throw null;
		}

		public void Dispose()
		{
			throw null;
		}
	}
}
