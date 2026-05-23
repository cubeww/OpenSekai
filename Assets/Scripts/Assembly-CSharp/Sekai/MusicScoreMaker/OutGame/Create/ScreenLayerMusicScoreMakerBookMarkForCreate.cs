using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame.Create
{
	public class ScreenLayerMusicScoreMakerBookMarkForCreate : ScreenLayer
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CPauseAndExitAsync_003Ed__10 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public ScreenLayerMusicScoreMakerBookMarkForCreate _003C_003E4__this;

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
		private ScreenLayerMusicScoreMakerBookMarkForCreateView _view;

		private ScreenLayerMusicScoreMakerBookMarkForCreatePresenter _presenter;

		private bool _isBackExit;

		private bool IsForwardInsert
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

		protected override void OnInitComponent()
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

		[AsyncStateMachine(typeof(_003CPauseAndExitAsync_003Ed__10))]
		private UniTask PauseAndExitAsync()
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

		public ScreenLayerMusicScoreMakerBookMarkForCreate()
		{
			throw null;
		}
	}
}
