using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame.Common
{
	public sealed class MusicScoreInfoView : MonoBehaviour, IDisposable
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CChangeInDetailModeAsync_003Ed__50 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public CancellationToken ct;

			public MusicScoreInfoView _003C_003E4__this;

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
		private struct _003CFadeInAsync_003Ed__44 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MusicScoreInfoView _003C_003E4__this;

			public float duration;

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

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CFadeOutAsync_003Ed__45 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MusicScoreInfoView _003C_003E4__this;

			public float duration;

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

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CPlayChangeInAnimationAsync_003Ed__48 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MusicScoreInfoView _003C_003E4__this;

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
		private struct _003CPlayChangeOutAnimationAsync_003Ed__49 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MusicScoreInfoView _003C_003E4__this;

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
		private struct _003CPlayMusicScorePreviewInAnimation_003Ed__47 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MusicScoreInfoView _003C_003E4__this;

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
		private struct _003CRefreshPreviewAsync_003Ed__35 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MusicScoreInfoView _003C_003E4__this;

			public CancellationToken cancellationToken;

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
		private CanvasGroup _rootCanvasGroup;

		[SerializeField]
		private MusicScorePreviewInfoView _previewInfoView;

		[SerializeField]
		private MusicScoreDetailInfoView _detailInfoView;

		[SerializeField]
		private CustomButton _showBaseMusicScoreButton;

		[SerializeField]
		private CustomButton _bookMarkMusicScoreButton;

		[SerializeField]
		private Animator _bookMarkAnimator;

		[SerializeField]
		private CustomTextMesh _bookMarkMusicScoreButtonText;

		[SerializeField]
		private CustomButton _decideButton;

		[SerializeField]
		private CustomTextMesh _decideButtonText;

		[SerializeField]
		private CustomButton _lockButton;

		[SerializeField]
		private CustomButton _deleteButton;

		[SerializeField]
		private UIPartsReleaseConditionsBalloon _releaseConditionsBalloon;

		[SerializeField]
		private CustomButton _liveMusicDownloadButton;

		[SerializeField]
		private CustomButton _musicScoreIdCopyButton;

		[SerializeField]
		private MusicScoreReviewButton _musicScoreReviewButton;

		[SerializeField]
		private GameObject _childMusicScoreInfoPanel;

		[SerializeField]
		private CustomTextMesh _childMusicScoreTitleText;

		[SerializeField]
		private CustomTextMesh _childMusicScoreAuthorNameText;

		private MusicScoreInfoViewData _viewData;

		private Action _onMusicScoreReviewButtonClicked;

		public void Setup(MusicScoreInfoViewData viewData, Action onShowsDetailButtonClicked, Action onFinishRefreshAnimation, Action onBookMarkButtonClicked, Action onToCreatorButtonClicked = null, Action onShowBaseMusicScoreButtonClicked = null, Action onDecideButtonClicked = null, Action onLockButtonClicked = null, Action onDeleteButtonClicked = null, Action onLiveMusicDownloadButtonClicked = null, Action onMusicScoreIdCopyButtonClicked = null, Action onMusicScoreReviewButtonClicked = null)
		{
			throw null;
		}

		public void Refresh()
		{
			throw null;
		}

		private void RefreshExcludingPreview()
		{
			throw null;
		}

		public void RefreshPreview()
		{
			throw null;
		}

		public void RefreshPreviewContent()
		{
			throw null;
		}

		private void RefreshBookMarkButton()
		{
			throw null;
		}

		private void RefreshBookMarkButtonText()
		{
			throw null;
		}

		private void RefreshDecideButton()
		{
			throw null;
		}

		private void RefreshLockButton()
		{
			throw null;
		}

		public void ShowReleaseConditionsBalloon(int releaseConditionId)
		{
			throw null;
		}

		public void RefreshLiveMusicDownloadButton()
		{
			throw null;
		}

		public void RefreshMusicScoreIdCopyButton()
		{
			throw null;
		}

		public void RefreshMusicScoreReviewButton()
		{
			throw null;
		}

		public void ShowReviewedBalloon()
		{
			throw null;
		}

		public void RefreshChildMusicScoreInfo()
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CRefreshPreviewAsync_003Ed__35))]
		public UniTask RefreshPreviewAsync(CancellationToken cancellationToken = default(CancellationToken))
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

		public void SyncWithMusicPlayback()
		{
			throw null;
		}

		public void SetPreviewAlpha(float alpha)
		{
			throw null;
		}

		public void FadeInPreviewOnLoop(float duration, CancellationToken ct)
		{
			throw null;
		}

		public void FadeOutPreviewOnLoop(float duration, CancellationToken ct)
		{
			throw null;
		}

		public void ApplyDetailInfoMode(CancellationToken ct)
		{
			throw null;
		}

		public void ClearDetailInfoMode(bool skipPreviewAnimation = false)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CFadeInAsync_003Ed__44))]
		public UniTask FadeInAsync(float duration = 0.2f, CancellationToken ct = default(CancellationToken))
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CFadeOutAsync_003Ed__45))]
		public UniTask FadeOutAsync(float duration = 0.2f, CancellationToken ct = default(CancellationToken))
		{
			throw null;
		}

		public void InitializeInAnimation()
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CPlayMusicScorePreviewInAnimation_003Ed__47))]
		public UniTask PlayMusicScorePreviewInAnimation(CancellationToken ct)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CPlayChangeInAnimationAsync_003Ed__48))]
		public UniTask PlayChangeInAnimationAsync(CancellationToken ct)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CPlayChangeOutAnimationAsync_003Ed__49))]
		public UniTask PlayChangeOutAnimationAsync(CancellationToken ct)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CChangeInDetailModeAsync_003Ed__50))]
		public UniTask ChangeInDetailModeAsync(CancellationToken ct)
		{
			throw null;
		}

		public void PlayBookmarkAnimation(bool isBookmarked)
		{
			throw null;
		}

		public void Dispose()
		{
			throw null;
		}

		public MusicScoreInfoView()
		{
			throw null;
		}
	}
}
