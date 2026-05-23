using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Sekai.MusicScoreMaker.OutGame.Common;
using Sekai.MusicScoreMaker.OutGame.Create.Category;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame.Create
{
	public class ScreenLayerMusicScoreMakerCreateView : MonoBehaviour, IDisposable
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CPlayInAnimationAsync_003Ed__7 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public ScreenLayerMusicScoreMakerCreateView _003C_003E4__this;

			public CancellationToken ct;

			private TweenAwaiter _003C_003Eu__1;

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
		private MusicScoreCreateCategory _category;

		[SerializeField]
		private CanvasGroup _rootCanvasGroup;

		[SerializeField]
		private CanvasGroup _coverCanvasGroup;

		[SerializeField]
		private ScreenBackground _screenBackground;

		public MusicScoreCreateCategory Category
		{
			get
			{
				throw null;
			}
		}

		public void Setup()
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CPlayInAnimationAsync_003Ed__7))]
		public UniTask PlayInAnimationAsync(CancellationToken ct)
		{
			throw null;
		}

		public void Dispose()
		{
			throw null;
		}

		public ScreenLayerMusicScoreMakerCreateView()
		{
			throw null;
		}
	}
}
