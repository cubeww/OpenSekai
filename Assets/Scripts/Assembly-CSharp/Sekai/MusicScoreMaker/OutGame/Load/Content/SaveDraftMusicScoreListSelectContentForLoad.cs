using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Sekai.ApiData;
using Sekai.MusicScoreMaker.OutGame.Common.Content;
using Sekai.MusicScoreMaker.OutGame.Created.Content;

namespace Sekai.MusicScoreMaker.OutGame.Load.Content
{
	public sealed class SaveDraftMusicScoreListSelectContentForLoad : ContentPresenterBase<SaveDraftMusicScoreListSelectBootData, SaveDraftMusicScoreListSelectViewData, SaveDraftMusicScoreListSelectView, SaveDraftMusicScoreListSelectModel>
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CExecuteLoadAsync_003Ed__4 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public SaveDraftMusicScoreListSelectContentForLoad _003C_003E4__this;

			private UserCustomMusicScoreDraft _003CmusicScoreDraft_003E5__2;

			private string _003CmusicScorePath_003E5__3;

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
		private struct _003COnPostBootAsync_003Ed__1 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public SaveDraftMusicScoreListSelectContentForLoad _003C_003E4__this;

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
		private struct _003COnPostSetupAsync_003Ed__0 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public SaveDraftMusicScoreListSelectContentForLoad _003C_003E4__this;

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

		[AsyncStateMachine(typeof(_003COnPostSetupAsync_003Ed__0))]
		protected override UniTask OnPostSetupAsync(CancellationToken ct)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003COnPostBootAsync_003Ed__1))]
		protected override UniTask OnPostBootAsync(CancellationToken ct)
		{
			throw null;
		}

		private void OnSelectedSlotCell(int slotNumber)
		{
			throw null;
		}

		private void OnLoad()
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CExecuteLoadAsync_003Ed__4))]
		private UniTask ExecuteLoadAsync()
		{
			throw null;
		}

		public SaveDraftMusicScoreListSelectContentForLoad()
		{
			throw null;
		}
	}
}
