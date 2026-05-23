using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using JetBrains.Annotations;
using Sekai.ApiData;
using Sekai.MusicScoreMaker.Ingame.Models;

namespace Sekai.MusicScoreMaker.OutGame.Publish
{
	public sealed class MusicScoreMakerPublishExecutor
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CExecuteAsync_003Ed__5 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder<bool> _003C_003Et__builder;

			public MusicScoreMakerPublishExecutor _003C_003E4__this;

			public CancellationToken ct;

			private UniTask<MusicScorePublishRequestData>.Awaiter _003C_003Eu__1;

			private UniTask<UserCustomMusicScorePublishedResponse>.Awaiter _003C_003Eu__2;

			private UniTask<bool>.Awaiter _003C_003Eu__3;

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
		private struct _003CExecuteRequestAsync_003Ed__6 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder<UserCustomMusicScorePublishedResponse> _003C_003Et__builder;

			public MusicScoreMakerPublishExecutor _003C_003E4__this;

			public MusicScorePublishRequestData requestData;

			private UniTask<UserCustomMusicScorePublishedResponse>.Awaiter _003C_003Eu__1;

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
		private struct _003CShowPublishCompletedDialogAsync_003Ed__7 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder<bool> _003C_003Et__builder;

			public UserCustomMusicScorePublishedResponse publishedResponse;

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

		private readonly MusicScorePreviewPlayData _previewPlayData;

		private readonly MusicScoreMakerData _musicScoreData;

		[CanBeNull]
		private readonly string _baseMusicScoreId;

		private readonly MusicDifficulty _recommendedDifficulty;

		public MusicScoreMakerPublishExecutor([NotNull] MusicScorePreviewPlayData previewPlayData, MusicScoreMakerData musicScoreData, [CanBeNull] string baseMusicScoreId, MusicDifficulty recommendedDifficulty)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CExecuteAsync_003Ed__5))]
		public UniTask<bool> ExecuteAsync(CancellationToken ct)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CExecuteRequestAsync_003Ed__6))]
		private UniTask<UserCustomMusicScorePublishedResponse> ExecuteRequestAsync(MusicScorePublishRequestData requestData)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CShowPublishCompletedDialogAsync_003Ed__7))]
		private UniTask<bool> ShowPublishCompletedDialogAsync([NotNull] UserCustomMusicScorePublishedResponse publishedResponse, CancellationToken ct)
		{
			throw null;
		}
	}
}
