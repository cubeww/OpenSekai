using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Sekai.MusicScoreMaker.OutGame.Search.Presenter;
using Sekai.MusicScoreMaker.OutGame.Search.View;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame.Search
{
	public class ScreenLayerMusicScoreMakerSearch : ScreenLayer
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CInitComponentAsync_003Ed__11 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public ScreenLayerMusicScoreMakerSearch _003C_003E4__this;

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
		private struct _003CPauseAndExitAsync_003Ed__15 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

			public ScreenLayerMusicScoreMakerSearch _003C_003E4__this;

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
		private struct _003CSetupPresenterAsync_003Ed__9 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public ScreenLayerMusicScoreMakerSearch _003C_003E4__this;

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
		private ScreenLayerMusicScoreMakerSearchView _view;

		private ScreenLayerMusicScoreMakerSearchPresenter _presenter;

		private string _specifiedMusicScoreId;

		private bool _isBackExit;

		private bool IsForwardInsert
		{
			get
			{
				throw null;
			}
		}

		private bool IsFromTop
		{
			get
			{
				throw null;
			}
		}

		protected override void OnBoot(BootArgBase bootArg)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CSetupPresenterAsync_003Ed__9))]
		private UniTask SetupPresenterAsync()
		{
			throw null;
		}

		protected override void OnInitComponent()
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CInitComponentAsync_003Ed__11))]
		private UniTask InitComponentAsync()
		{
			throw null;
		}

		protected override void OnScreenStart()
		{
			throw null;
		}

		protected override void OnFinishStartAnimation()
		{
			throw null;
		}

		public override void OnWillExit()
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CPauseAndExitAsync_003Ed__15))]
		private UniTaskVoid PauseAndExitAsync()
		{
			throw null;
		}

		protected override void OnExitStart()
		{
			throw null;
		}

		protected override void OnExited()
		{
			throw null;
		}

		protected override void OnExitScene()
		{
			throw null;
		}

		public ScreenLayerMusicScoreMakerSearch()
		{
			throw null;
		}
	}
}
