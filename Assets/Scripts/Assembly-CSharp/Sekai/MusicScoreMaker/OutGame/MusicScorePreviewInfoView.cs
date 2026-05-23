using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Sekai.MusicScoreMaker.Ingame.Controllers;
using Sekai.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Sekai.MusicScoreMaker.OutGame
{
	public sealed class MusicScorePreviewInfoView : MonoBehaviour, IDisposable
	{
		public enum RefreshDirectionType
		{
			Animation = 0,
			Immediate = 1
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CFadeInPreviewAsync_003Ed__27 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MusicScorePreviewInfoView _003C_003E4__this;

			public float duration;

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
		private struct _003CFadeOutPreviewAsync_003Ed__28 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MusicScorePreviewInfoView _003C_003E4__this;

			public float duration;

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
		private struct _003CPlayInAnimationAsync_003Ed__20 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MusicScorePreviewInfoView _003C_003E4__this;

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
		private struct _003CPlayMusicTitleAnimationAsync_003Ed__36 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public CancellationToken cancellationToken;

			public MusicScorePreviewInfoView _003C_003E4__this;

			public string title;

			private CancellationTokenSource _003ClinkedCts_003E5__2;

			private CancellationToken _003Cct_003E5__3;

			private int _003Ci_003E5__4;

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
		private struct _003CPlayRefreshAnimationAsync_003Ed__34 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MusicScorePreviewInfoView _003C_003E4__this;

			public string headerTitle;

			private CancellationTokenSource _003ClinkedCts_003E5__2;

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
		private struct _003CRefreshPreviewAsync_003Ed__30 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MusicScorePreviewInfoView _003C_003E4__this;

			public CancellationToken cancellationToken;

			private CancellationTokenSource _003ClinkedCts_003E5__2;

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

		private const string InAnimationStateName = "In";

		[SerializeField]
		private CanvasGroup _emptyLogoFadeCanvasGroup;

		[SerializeField]
		private Graphic[] _difficultyDecorations;

		[SerializeField]
		private StandaloneMusicScorePreviewController _musicScorePreviewController;

		[SerializeField]
		private CustomTextMesh _headerMusicTitleText;

		[SerializeField]
		private UIPartsMarquee _headerMarquee;

		[SerializeField]
		private CustomTextMesh _musicTitleText;

		[SerializeField]
		private CanvasGroup _contentFadeCanvasGroup;

		[SerializeField]
		private CanvasGroup _previewAreaCanvasGroup;

		[SerializeField]
		private CustomTextMesh _musicArtistNameText;

		[SerializeField]
		private UITextureLoader _jacketLoader;

		[SerializeField]
		private CustomTextMesh _commentText;

		[SerializeField]
		private CustomButton _showDetailButton;

		[SerializeField]
		private MusicScoreTagsView _tagsView;

		[SerializeField]
		private Animator _animator;

		private MusicScorePreviewInfoViewData _viewData;

		private CancellationTokenSource _refreshAnimationCts;

		private Action _onFinishRefreshAnimation;

		public void Setup(MusicScorePreviewInfoViewData data, Action onFinishRefreshAnimation, Action onShowsDetailButtonClicked = null)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CPlayInAnimationAsync_003Ed__20))]
		public UniTask PlayInAnimationAsync(CancellationToken ct)
		{
			throw null;
		}

		public void Refresh()
		{
			throw null;
		}

		public void OnPause()
		{
			throw null;
		}

		public void OnResume()
		{
			throw null;
		}

		public void RefreshContent()
		{
			throw null;
		}

		public void SyncWithMusicPlayback()
		{
			throw null;
		}

		public void SetPreviewAlpha(float alpha)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CFadeInPreviewAsync_003Ed__27))]
		public UniTask FadeInPreviewAsync(float duration, CancellationToken ct)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CFadeOutPreviewAsync_003Ed__28))]
		public UniTask FadeOutPreviewAsync(float duration, CancellationToken ct)
		{
			throw null;
		}

		private void LoadJacket()
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CRefreshPreviewAsync_003Ed__30))]
		public UniTask RefreshPreviewAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			throw null;
		}

		private Color GetDecorationColor(MusicDifficulty difficulty)
		{
			throw null;
		}

		public void Dispose()
		{
			throw null;
		}

		private void PlayRefreshAnimation(string headerTitle, RefreshDirectionType refreshDirection)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CPlayRefreshAnimationAsync_003Ed__34))]
		private UniTask PlayRefreshAnimationAsync(string headerTitle)
		{
			throw null;
		}

		private void OnFinishRefreshAnimation()
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CPlayMusicTitleAnimationAsync_003Ed__36))]
		private UniTask PlayMusicTitleAnimationAsync(string title, CancellationToken cancellationToken)
		{
			throw null;
		}

		public MusicScorePreviewInfoView()
		{
			throw null;
		}
	}
}
