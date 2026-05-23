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
	public class MusicScoreReviewButton : MonoBehaviour
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CHideAsync_003Ed__20 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MusicScoreReviewButton _003C_003E4__this;

			public CancellationToken ct;

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
		private struct _003CShowAsync_003Ed__19 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MusicScoreReviewButton _003C_003E4__this;

			public CancellationToken ct;

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

		private const string REVIEW_ANIM_ACTIVATING = "Activating";

		private const string REVIEW_ANIM_CANCEL = "Cancel";

		private const string REVIEW_ANIM_ON = "On";

		private const string REVIEW_ANIM_OFF = "Off";

		[SerializeField]
		private CustomButton _button;

		[SerializeField]
		private CustomTextMesh _musicScoreTitle;

		[SerializeField]
		private CustomTextMesh _userName;

		[SerializeField]
		private Animator _reviewAnimator;

		[SerializeField]
		private GameObject _reviewedBalloon;

		[SerializeField]
		private CanvasGroup _canvasGroup;

		[SerializeField]
		private UIPartsMusicDifficulty _musicDifficulty;

		private Action<bool> _onClick;

		private bool _reviewed;

		private bool _isReviewing;

		[SerializeField]
		private float _fadeDurationSeconds;

		private CancellationTokenSource _fadeCts;

		public void Setup(string userName, string title, string musicDifficultyType, bool reviewed, Action<bool> onClick, bool onlyOneClick = false)
		{
			throw null;
		}

		private void OnEnable()
		{
			throw null;
		}

		public void SetActive(bool isActive)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CShowAsync_003Ed__19))]
		public UniTask ShowAsync(CancellationToken ct = default(CancellationToken))
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CHideAsync_003Ed__20))]
		public UniTask HideAsync(CancellationToken ct = default(CancellationToken))
		{
			throw null;
		}

		private void CancelFade()
		{
			throw null;
		}

		public void Reset()
		{
			throw null;
		}

		public void ShowReviewedBalloon()
		{
			throw null;
		}

		public void Dispose()
		{
			throw null;
		}

		private void OnClick()
		{
			throw null;
		}

		private void OnOnlyOneClick()
		{
			throw null;
		}

		private void PlayReviewSE()
		{
			throw null;
		}

		public MusicScoreReviewButton()
		{
			throw null;
		}
	}
}
