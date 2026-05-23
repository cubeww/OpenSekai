using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame
{
	public sealed class ScreenLayerMusicScoreDetailView : MonoBehaviour, IDisposable
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CPlayInAnimationAsync_003Ed__6 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public ScreenLayerMusicScoreDetailView _003C_003E4__this;

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

		[SerializeField]
		private MusicScoreDetailInfoView _detailInfoView;

		[SerializeField]
		private MusicScorePreviewInfoView _previewInfoView;

		private MusicScoreDetailViewData _viewData;

		public void Setup(MusicScoreDetailViewData viewData, Action onFinishRefreshAnimation)
		{
			throw null;
		}

		public void Refresh()
		{
			throw null;
		}

		public void InitializeInAnimation()
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CPlayInAnimationAsync_003Ed__6))]
		public UniTask PlayInAnimationAsync(CancellationToken ct)
		{
			throw null;
		}

		public void SyncWithMusicPlayback()
		{
			throw null;
		}

		public void Dispose()
		{
			throw null;
		}

		public ScreenLayerMusicScoreDetailView()
		{
			throw null;
		}
	}
}
