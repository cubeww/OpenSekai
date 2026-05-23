using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;

namespace Sekai.MusicScoreMaker.OutGame.SaveDraft
{
	public sealed class SaveDraftDeleteExecutor : SaveDraftExecutorBase
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CExecuteAsync_003Ed__1 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder<SaveDraftResult> _003C_003Et__builder;

			public CancellationToken ct;

			public SaveDraftDeleteExecutor _003C_003E4__this;

			private UniTask<bool>.Awaiter _003C_003Eu__1;

			private UniTask<Sekai.ApiData.UserCustomMusicScoreDraftListResponse>.Awaiter _003C_003Eu__2;

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

		public SaveDraftDeleteExecutor(int slotNo)
			: base(slotNo)
		{
		}

		[AsyncStateMachine(typeof(_003CExecuteAsync_003Ed__1))]
		public override UniTask<SaveDraftResult> ExecuteAsync(CancellationToken ct)
		{
			throw null;
		}
	}
}
