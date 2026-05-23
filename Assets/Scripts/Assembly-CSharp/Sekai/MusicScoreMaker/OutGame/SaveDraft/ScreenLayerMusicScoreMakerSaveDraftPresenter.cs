using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using JetBrains.Annotations;
using Sekai.ApiData;
using Sekai.MusicScoreMaker.Ingame.Models;

namespace Sekai.MusicScoreMaker.OutGame.SaveDraft
{
	public class ScreenLayerMusicScoreMakerSaveDraftPresenter : IDisposable
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CExecuteDeleteAsync_003Ed__11 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public ScreenLayerMusicScoreMakerSaveDraftPresenter _003C_003E4__this;

			private int _003CdeletedSlotNo_003E5__2;

			private UniTask<SaveDraftResult>.Awaiter _003C_003Eu__1;

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
		private struct _003CExecuteSaveAsync_003Ed__9 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public ScreenLayerMusicScoreMakerSaveDraftPresenter _003C_003E4__this;

			private UniTask<SaveDraftResult>.Awaiter _003C_003Eu__1;

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
		private struct _003CRefreshAsync_003Ed__6 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public ScreenLayerMusicScoreMakerSaveDraftPresenter _003C_003E4__this;

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
		private struct _003CShowConnectionErrorDialogAsync_003Ed__12 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

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

		private ScreenLayerMusicScoreMakerSaveDraftView _view;

		private ScreenLayerMusicScoreMakerSaveDraftModel _model;

		[CanBeNull]
		private Action _onSaveCompleted;

		[CanBeNull]
		private Action<int, UserCustomMusicScoreDraft> _onSaveDraftInfoCallback;

		[CanBeNull]
		private Action<int> _onDraftDeletedCallback;

		public void Setup(ScreenLayerMusicScoreMakerSaveDraftView view, [NotNull] MusicScoreMakerData musicScoreMakerData, bool isExitOnSave, [CanBeNull] string baseMusicScoreId, int baseMusicDifficultyId = -1, [CanBeNull] Action onSaveCompleted = null, [CanBeNull] Action<int, UserCustomMusicScoreDraft> onSaveDraftInfoCallback = null, [CanBeNull] Action<int> onDraftDeletedCallback = null)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CRefreshAsync_003Ed__6))]
		public UniTask RefreshAsync()
		{
			throw null;
		}

		private void OnSelectedSlot(int slotNumber)
		{
			throw null;
		}

		private void OnSave()
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CExecuteSaveAsync_003Ed__9))]
		private UniTask ExecuteSaveAsync()
		{
			throw null;
		}

		private void OnDelete()
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CExecuteDeleteAsync_003Ed__11))]
		private UniTask ExecuteDeleteAsync()
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CShowConnectionErrorDialogAsync_003Ed__12))]
		private UniTask ShowConnectionErrorDialogAsync()
		{
			throw null;
		}

		public void ResetSelectedSlot()
		{
			throw null;
		}

		public void Dispose()
		{
			throw null;
		}

		public ScreenLayerMusicScoreMakerSaveDraftPresenter()
		{
			throw null;
		}
	}
}
