using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame
{
	public sealed class MusicScoreDetailInfoView : MonoBehaviour, IDisposable
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CPlayInAnimationAsync_003Ed__35 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MusicScoreDetailInfoView _003C_003E4__this;

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
		private struct _003CPlaySlideInAnimationAsync_003Ed__36 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MusicScoreDetailInfoView _003C_003E4__this;

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
		private struct _003CPlaySlideOutAnimationAsync_003Ed__37 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MusicScoreDetailInfoView _003C_003E4__this;

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

		private const string InAnimationStateName = "In";

		private static readonly string SlideInAnimationStateName;

		private static readonly string SlideOutAnimationStateName;

		private const float DefaultLabelFontSize = 20f;

		private const float AppendLabelFontSize = 16f;

		private const int AppendPrefixFontSize = 14;

		[SerializeField]
		private CustomTextMesh _scoreTitleText;

		[SerializeField]
		private CustomTextMesh _scoreLevelLabelText;

		[SerializeField]
		private CustomTextMesh _scoreLevelText;

		[SerializeField]
		private UIPartsMusicDifficultyLabel _difficultyLabel;

		[SerializeField]
		private UIPartsMusicDifficulty[] _difficultyDecorations;

		[SerializeField]
		private CustomTextMesh _creatorNameText;

		[SerializeField]
		private CustomTextMesh _playCountText;

		[SerializeField]
		private CustomTextMesh _reviewCountText;

		[SerializeField]
		private CustomTextMesh _fullComboRateText;

		[SerializeField]
		private CustomTextMesh _commentText;

		[SerializeField]
		private UIPartsMusicDifficultyClearStatus _clearStatus;

		[SerializeField]
		private MusicScoreTagsView _tagsView;

		[SerializeField]
		private GameObject _baseMusicScoreMarker;

		[SerializeField]
		private GameObject _derivativeAllowedMarker;

		[SerializeField]
		private CustomButton _toCreatorButton;

		[SerializeField]
		private Animator _animator;

		[SerializeField]
		private UIPartsSpectrumAnalyzer _spectrumAnalyzerLeft;

		[SerializeField]
		private UIPartsSpectrumAnalyzer _spectrumAnalyzerRight;

		private MusicScoreDetailInfoViewData _viewData;

		private Action _onToCreatorButtonClicked;

		public void Setup(MusicScoreDetailInfoViewData viewData, Action onToCreatorButtonClicked = null)
		{
			throw null;
		}

		public void RefreshReviewCount()
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

		public void OnPause()
		{
			throw null;
		}

		public void OnResume()
		{
			throw null;
		}

		public void ResetAnimatorState()
		{
			throw null;
		}

		public void Show()
		{
			throw null;
		}

		public void Hide()
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CPlayInAnimationAsync_003Ed__35))]
		public UniTask PlayInAnimationAsync(CancellationToken ct)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CPlaySlideInAnimationAsync_003Ed__36))]
		public UniTask PlaySlideInAnimationAsync(CancellationToken ct)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CPlaySlideOutAnimationAsync_003Ed__37))]
		public UniTask PlaySlideOutAnimationAsync(CancellationToken ct)
		{
			throw null;
		}

		public void Dispose()
		{
			throw null;
		}

		public MusicScoreDetailInfoView()
		{
			throw null;
		}

		static MusicScoreDetailInfoView()
		{
			throw null;
		}
	}
}
