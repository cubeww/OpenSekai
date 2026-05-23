using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using CriWare;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;

namespace Sekai.Sound
{
	public sealed class IngameBGMPlayer : IDisposable
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass26_0
		{
			public IngameBGMPlayer _003C_003E4__this;

			public long startTimeMs;

			public _003C_003Ec__DisplayClass26_0()
			{
				throw null;
			}

			internal bool _003CWaitForPlaybackReadyAsync_003Eb__0()
			{
				throw null;
			}

			internal bool _003CWaitForPlaybackReadyAsync_003Eb__1()
			{
				throw null;
			}
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CPlayFromPositionAsync_003Ed__21 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder<bool> _003C_003Et__builder;

			public IngameBGMPlayer _003C_003E4__this;

			public CancellationToken cancellationToken;

			public float startTimeSeconds;

			public float fadeInDurationSec;

			private CancellationTokenSource _003ClinkedCts_003E5__2;

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
		private struct _003CWaitForPlaybackReadyAsync_003Ed__26 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder<bool> _003C_003Et__builder;

			public IngameBGMPlayer _003C_003E4__this;

			public long startTimeMs;

			public CancellationToken cancellationToken;

			private _003C_003Ec__DisplayClass26_0 _003C_003E8__1;

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

		private const int AudioSyncTimeoutSeconds = 60;

		private CriAtomExPlayback _currentPlayback;

		private readonly string _assetBundleName;

		private readonly string _cueName;

		private bool _isDisposed;

		private bool _isPlaybackInitialized;

		private CancellationTokenSource _cancellationTokenSource;

		private bool _isStoppingWithFade;

		public string AssetBundleName
		{
			get
			{
				throw null;
			}
		}

		public CriAtomExPlayback.Status CurrentStatus
		{
			get
			{
				throw null;
			}
		}

		public float MusicLengthSeconds
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			private set
			{
				throw null;
			}
		}

		public float CurrentPositionSeconds
		{
			get
			{
				throw null;
			}
		}

		public bool IsFading
		{
			get
			{
				throw null;
			}
		}

		public IngameBGMPlayer(string assetBundleName, string cueName)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CPlayFromPositionAsync_003Ed__21))]
		public UniTask<bool> PlayFromPositionAsync(float startTimeSeconds = 0f, float fadeInDurationSec = 0f, CancellationToken cancellationToken = default(CancellationToken))
		{
			throw null;
		}

		public void Pause()
		{
			throw null;
		}

		public void Stop()
		{
			throw null;
		}

		public void Resume()
		{
			throw null;
		}

		public void StartFadeOut(float duration)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CWaitForPlaybackReadyAsync_003Ed__26))]
		private UniTask<bool> WaitForPlaybackReadyAsync(long startTimeMs, CancellationToken cancellationToken)
		{
			throw null;
		}

		private void LoadMusicInfo()
		{
			throw null;
		}

		public void Dispose()
		{
			throw null;
		}
	}
}
