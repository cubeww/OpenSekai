using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using JetBrains.Annotations;
using Sekai.MusicScoreMaker.OutGame.Common.Content;
using Sekai.MusicScoreMaker.OutGame.Common.PublishedMusicScoreList;
using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame.Created.Content
{
	public sealed class PublishedMusicScoreListSelectView : ContentViewBase<PublishedMusicScoreListSelectViewData>
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CCloseMusicScoreInfo_003Ed__7 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public PublishedMusicScoreListSelectView _003C_003E4__this;

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
		private struct _003CRefreshAsync_003Ed__5 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public PublishedMusicScoreListSelectView _003C_003E4__this;

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
		private PublishedMusicScoreListView _publishedListView;

		[SerializeField]
		private CustomTextMesh _musicScoreCountText;

		[CanBeNull]
		public MusicScoreData AppliedMusicScoreData
		{
			get
			{
				throw null;
			}
		}

		public void Setup(Action<int> onSelectMusicScore, Action<SortOrderBy> onChangeSortOrder, Func<CancellationToken, UniTask> onPreEnterDetailAsync, Func<CancellationToken, UniTask> onPostExitDetailAsync, Action onDelete, Action onDecideButtonClicked = null, Action onClickFilterButton = null)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CRefreshAsync_003Ed__5))]
		public override UniTask RefreshAsync()
		{
			throw null;
		}

		public void ApplySelectedMusicScore(MusicScoreData musicScoreData)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CCloseMusicScoreInfo_003Ed__7))]
		public UniTask CloseMusicScoreInfo()
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

		public override void Dispose()
		{
			throw null;
		}

		public void CancelPrepareSyncPlayback()
		{
			throw null;
		}

		public PublishedMusicScoreListSelectView()
		{
			throw null;
		}
	}
}
