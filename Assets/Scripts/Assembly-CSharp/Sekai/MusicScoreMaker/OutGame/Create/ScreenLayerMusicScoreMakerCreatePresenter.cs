using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Sekai.MusicScoreMaker.OutGame.Common;
using Sekai.MusicScoreMaker.OutGame.Create.Category;

namespace Sekai.MusicScoreMaker.OutGame.Create
{
	public sealed class ScreenLayerMusicScoreMakerCreatePresenter : IDisposable, IScreenNavigator
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CExecuteBackProcessAsync_003Ed__15 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public ScreenLayerMusicScoreMakerCreatePresenter _003C_003E4__this;

			private UniTask<bool>.Awaiter _003C_003Eu__1;

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
		private struct _003COnBackBeforeContentAsync_003Ed__13 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder<bool> _003C_003Et__builder;

			public ScreenLayerMusicScoreMakerCreatePresenter _003C_003E4__this;

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
		private struct _003CPlayInAnimationAsync_003Ed__6 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public ScreenLayerMusicScoreMakerCreatePresenter _003C_003E4__this;

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
		private struct _003CSetupCategoryAsync_003Ed__7 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public ScreenLayerMusicScoreMakerCreatePresenter _003C_003E4__this;

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

		private ScreenLayerMusicScoreMakerCreateView _view;

		private ScreenLayerMusicScoreMakerCreateModel _model;

		private bool _isProcessingBack;

		private MusicScoreCreateCategory Category
		{
			get
			{
				throw null;
			}
		}

		public void Setup(ScreenLayerMusicScoreMakerCreateView view)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CPlayInAnimationAsync_003Ed__6))]
		public UniTask PlayInAnimationAsync(CancellationToken ct)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CSetupCategoryAsync_003Ed__7))]
		public UniTask SetupCategoryAsync(CancellationToken ct)
		{
			throw null;
		}

		public bool GetShowingHeader()
		{
			throw null;
		}

		public void ShowHeaderImmediate()
		{
			throw null;
		}

		public UniTask ShowHeaderAsync(CancellationToken ct)
		{
			throw null;
		}

		public UniTask HideHeaderAsync(CancellationToken ct)
		{
			throw null;
		}

		public MenuScreenType GetScreenType()
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003COnBackBeforeContentAsync_003Ed__13))]
		public UniTask<bool> OnBackBeforeContentAsync()
		{
			throw null;
		}

		private void OnBackUIScreenOverride(bool _)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CExecuteBackProcessAsync_003Ed__15))]
		private UniTask ExecuteBackProcessAsync()
		{
			throw null;
		}

		public void Dispose()
		{
			throw null;
		}

		public ScreenLayerMusicScoreMakerCreatePresenter()
		{
			throw null;
		}
	}
}
