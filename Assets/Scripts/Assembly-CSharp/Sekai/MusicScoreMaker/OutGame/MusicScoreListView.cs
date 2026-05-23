using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using DG.Tweening;
using Sekai.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sekai.MusicScoreMaker.Outgame
{
	public class MusicScoreListView<TCell, TCellData, TNeutralContents, THighlightContents> : ListView where TCell : MusicScoreListCell<TCellData, TNeutralContents, THighlightContents> where TCellData : MusicScoreListCellData where TNeutralContents : MusicScoreListCellContentsBase where THighlightContents : MusicScoreListCellContentsBase
	{
		private enum SelectMode
		{
			None = 0,
			Drag = 1,
			Click = 2,
			FocusCell = 3
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CFadeInAsync_003Ed__16 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MusicScoreListView<TCell, TCellData, TNeutralContents, THighlightContents> _003C_003E4__this;

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
		private struct _003CFadeOutAsync_003Ed__17 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MusicScoreListView<TCell, TCellData, TNeutralContents, THighlightContents> _003C_003E4__this;

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
		private struct _003CPlayCellStaggerFadeInAsync_003Ed__19 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MusicScoreListView<TCell, TCellData, TNeutralContents, THighlightContents> _003C_003E4__this;

			public CancellationToken externalCt;

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

		private const float HIGHLIGHT_TWEEN_SEC = 0.1f;

		private const float SNAP_SPEED = 2000f;

		[SerializeField]
		[Header("スタガーアニメーション設定")]
		private float _cellStaggerDelaySec;

		[SerializeField]
		private float _underLineStaggerDelaySec;

		[SerializeField]
		private float snapSelectedCellSpd;

		[SerializeField]
		private CanvasGroup _rootCanvasGroup;

		private List<TCellData> _cellDataList;

		private CancellationTokenSource _staggerAnimationCts;

		private float _itemHighlightSizeY;

		private int _selectedDataIndex;

		private int _beforeSelectedDataIndex;

		private SelectMode _selectMode;

		private Sequence _highlightSequence;

		private Action<int> _onChangeSelectCellEvent;

		public void Setup(Action<int> onChangeSelectCell = null)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(MusicScoreListView<, , , >._003CFadeInAsync_003Ed__16))]
		public UniTask FadeInAsync(float duration = 0.2f, CancellationToken ct = default(CancellationToken))
		{
			throw null;
		}

		[AsyncStateMachine(typeof(MusicScoreListView<, , , >._003CFadeOutAsync_003Ed__17))]
		public UniTask FadeOutAsync(float duration = 0.2f, CancellationToken ct = default(CancellationToken))
		{
			throw null;
		}

		public void UpdateListData(IList<TCellData> cellDataList, int selectedIndex = 0, bool isSnap = true)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(MusicScoreListView<, , , >._003CPlayCellStaggerFadeInAsync_003Ed__19))]
		public UniTask PlayCellStaggerFadeInAsync(CancellationToken externalCt = default(CancellationToken))
		{
			throw null;
		}

		public void ResetScrollContentPosition()
		{
			throw null;
		}

		private void FocusCell(int selectedIndex = 0)
		{
			throw null;
		}

		private void FocusCellImmediate(int selectedIndex = 0)
		{
			throw null;
		}

		protected override void OnInstantiateCallback(ListViewItem listViewItem)
		{
			throw null;
		}

		protected override void OnCreateCellCallback(ListViewItem listViewItem, int dataIndex)
		{
			throw null;
		}

		protected override void SetListViewItemVertical(ListViewItem listViewItem, int dataIndex)
		{
			throw null;
		}

		protected override int GetFirstIndex()
		{
			throw null;
		}

		public override void OnBeginDrag(PointerEventData eventData)
		{
			throw null;
		}

		public override void OnEndDrag(PointerEventData eventData)
		{
			throw null;
		}

		private void OnClickCell(int selectDataIndex)
		{
			throw null;
		}

		private void ChangeSelectedCell(int selectDataIndex, bool isImmediate = false)
		{
			throw null;
		}

		private float GetSelectedCellSnapTargetY()
		{
			throw null;
		}

		private Tween GetResetNeutralViewAllTween(float duration)
		{
			throw null;
		}

		private void ResetNeutralViewAll()
		{
			throw null;
		}

		private Tween GetSnapToTargetPositionTween(float targetPosY, float duration)
		{
			throw null;
		}

		private void SnapToTargetPosition(float targetPosY)
		{
			throw null;
		}

		private Tween GetPlayHighlightTween()
		{
			throw null;
		}

		private void UpdateHighlightView()
		{
			throw null;
		}

		private Vector3 CalcAnchoredPosition(int dataIndex)
		{
			throw null;
		}

		private void UpdateAnchoredPositionOffset()
		{
			throw null;
		}

		private Vector3 GetAnchoredPositionOffset(int targetIndex)
		{
			throw null;
		}

		private void UpdateCellPositionAll()
		{
			throw null;
		}

		public void RefreshVisibleCells()
		{
			throw null;
		}

		public void UpdateCellBookmarkState(string musicScoreId, bool isBookmarked)
		{
			throw null;
		}

		private void CancelStaggerAnimation()
		{
			throw null;
		}

		public void OnDestroy()
		{
			throw null;
		}

		public MusicScoreListView()
		{
			throw null;
		}
	}
}
