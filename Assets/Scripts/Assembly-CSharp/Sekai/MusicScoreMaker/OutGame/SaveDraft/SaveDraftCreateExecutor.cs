using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using JetBrains.Annotations;
using Sekai.ApiData;
using Sekai.MusicScoreMaker.Ingame.Models;
using Sekai.Service;

namespace Sekai.MusicScoreMaker.OutGame.SaveDraft
{
	public sealed class SaveDraftCreateExecutor : SaveDraftExecutorBase
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CExecuteAsync_003Ed__4 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder<SaveDraftResult> _003C_003Et__builder;

			public CancellationToken ct;

			public SaveDraftCreateExecutor _003C_003E4__this;

			private MusicScoreSaveDraftCreateService _003Cservice_003E5__2;

			private UserCustomMusicScoreDraftListResponse _003Cresponse_003E5__3;

			private UniTask<SaveDraftRequestData>.Awaiter _003C_003Eu__1;

			private UniTask<UserCustomMusicScoreDraftListResponse>.Awaiter _003C_003Eu__2;

			private UniTask.Awaiter _003C_003Eu__3;

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

		private readonly MusicScoreMakerData _musicScoreData;

		[CanBeNull]
		private readonly string _baseMusicScoreId;

		private readonly int _baseMusicDifficultyId;

		public SaveDraftCreateExecutor(int slotNo, MusicScoreMakerData musicScoreData, [CanBeNull] string baseMusicScoreId, int baseMusicDifficultyId = -1)
			: base(slotNo)
		{
			_musicScoreData = musicScoreData;
			_baseMusicScoreId = baseMusicScoreId;
			_baseMusicDifficultyId = baseMusicDifficultyId;
		}

		[AsyncStateMachine(typeof(_003CExecuteAsync_003Ed__4))]
		public override UniTask<SaveDraftResult> ExecuteAsync(CancellationToken ct)
		{
			throw null;
		}
	}
}
