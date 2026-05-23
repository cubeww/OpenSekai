using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using DG.Tweening;
using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.Outgame
{
	public abstract class MusicScoreListCell<TCellData, TNeutralContents, THighlightContents> : ListViewItem where TCellData : MusicScoreListCellData where TNeutralContents : MusicScoreListCellContentsBase where THighlightContents : MusicScoreListCellContentsBase
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CPlayFadeInAnimationAsync_003Ed__28 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MusicScoreListCell<TCellData, TNeutralContents, THighlightContents> _003C_003E4__this;

			public float cellDelay;

			public float underLineDelay;

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
		protected CustomButton _button;

		[SerializeField]
		protected TNeutralContents _neutralContents;

		[SerializeField]
		protected THighlightContents _highlightContents;

		[SerializeField]
		private GameObject _underLineObject;

		[SerializeField]
		private CanvasGroup _rootCanvasGroup;

		[SerializeField]
		[Header("セルフェードアニメーション設定")]
		private float _cellFadeInDuration;

		[SerializeField]
		private Ease _cellFadeInEase;

		[SerializeField]
		[Header("下線スケールアニメーション設定")]
		private float _underLineScaleDuration;

		[SerializeField]
		private Ease _underLineScaleEase;

		[SerializeField]
		private float _underLineInitialScaleX;

		private Tweener _fadeInTween;

		private Tweener _underLineScaleTween;

		[SerializeField]
		private Vector2 _highlightedSize;

		public Action<int> OnClickSelectedCellEvent;

		public Vector2 HighlightedSize
		{
			get
			{
				throw null;
			}
		}

		public void Initialize()
		{
			throw null;
		}

		public abstract void UpdateView(TCellData data, bool isSelected);

		public abstract void UpdateBookmarkState(bool isBookmarked);

		public Sequence DoChangeHighlightDisplay(bool isHighlight, float duration)
		{
			throw null;
		}

		public void ChangeHighlightDisplay(bool isHighlight)
		{
			throw null;
		}

		public Tween DoMovePosition(float deltaY, float duration)
		{
			throw null;
		}

		public void MovePosition(float deltaY)
		{
			throw null;
		}

		public void ResetNeutralView()
		{
			throw null;
		}

		public void ResetPosition()
		{
			throw null;
		}

		public void SetActiveUnderLine(bool isActive)
		{
			throw null;
		}

		public void ResetFadeAnimation()
		{
			throw null;
		}

		public void InitializeFadeAnimation()
		{
			throw null;
		}

		[AsyncStateMachine(typeof(MusicScoreListCell<, , >._003CPlayFadeInAnimationAsync_003Ed__28))]
		public UniTask PlayFadeInAnimationAsync(float cellDelay, float underLineDelay, CancellationToken ct)
		{
			throw null;
		}

		protected MusicScoreListCell()
		{
			throw null;
		}
	}
}
