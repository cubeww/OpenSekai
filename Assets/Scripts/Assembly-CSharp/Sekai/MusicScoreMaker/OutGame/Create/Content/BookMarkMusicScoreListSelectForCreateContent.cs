using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Sekai.MusicScoreMaker.OutGame.Common;
using Sekai.MusicScoreMaker.OutGame.Common.Content;
using Sekai.Sound;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame.Create.Content
{
	public sealed class BookMarkMusicScoreListSelectForCreateContent : ContentPresenterBase<BookMarkMusicScoreSelectForCreateBootData, BookMarkMusicScoreListSelectForCreateViewData, BookMarkMusicScoreListSelectForCreateView, BookMarkMusicScoreListSelectForCreateModel>
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass15_0
		{
			[StructLayout((LayoutKind)3)]
			private struct _003C_003CExecuteCreateMusicScoreAsync_003Eb__0_003Ed : IAsyncStateMachine
			{
				public int _003C_003E1__state;

				public AsyncUniTaskMethodBuilder _003C_003Et__builder;

				public _003C_003Ec__DisplayClass15_0 _003C_003E4__this;

				public float fadeTime;

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

			public IngameBGMPlayer bgmPlayer;

			public _003C_003Ec__DisplayClass15_0()
			{
				throw null;
			}

			[AsyncStateMachine(typeof(_003C_003CExecuteCreateMusicScoreAsync_003Eb__0_003Ed))]
			internal UniTask _003CExecuteCreateMusicScoreAsync_003Eb__0(float fadeTime)
			{
				throw null;
			}
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CApplyFilterAsync_003Ed__9 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public BookMarkMusicScoreListSelectForCreateContent _003C_003E4__this;

			public CancellationToken ct;

			public int index;

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
		private struct _003CExecuteCreateMusicScoreAsync_003Ed__15 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public BookMarkMusicScoreListSelectForCreateContent _003C_003E4__this;

			private _003C_003Ec__DisplayClass15_0 _003C_003E8__1;

			private MusicScoreData _003CmusicScoreData_003E5__2;

			private string _003CmusicScoreId_003E5__3;

			private UniTask<Sekai.MusicScoreMaker.Ingame.Models.MusicScoreMakerData>.Awaiter _003C_003Eu__1;

			private int _003C_003E7__wrap3;

			private UniTask<Sekai.ApiData.UserCustomMusicScorePublishedResponse>.Awaiter _003C_003Eu__2;

			private UniTask.Awaiter _003C_003Eu__3;

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
		private struct _003COnPostBootAsync_003Ed__2 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public CancellationToken ct;

			public BookMarkMusicScoreListSelectForCreateContent _003C_003E4__this;

			private CancellationTokenSource _003Ccts_003E5__2;

			private CancellationToken _003ClinkedCt_003E5__3;

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
		private struct _003COnPostExitAsync_003Ed__5 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public BookMarkMusicScoreListSelectForCreateContent _003C_003E4__this;

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
		private struct _003COnPostExitDetailAsync_003Ed__4 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public CancellationToken ct;

			public BookMarkMusicScoreListSelectForCreateContent _003C_003E4__this;

			private CancellationTokenSource _003CcallbackCts_003E5__2;

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
		private struct _003COnPreBootAsync_003Ed__1 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public BookMarkMusicScoreListSelectForCreateContent _003C_003E4__this;

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
		private struct _003COnPreEnterDetailAsync_003Ed__3 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public CancellationToken ct;

			public BookMarkMusicScoreListSelectForCreateContent _003C_003E4__this;

			private CancellationTokenSource _003CcallbackCts_003E5__2;

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
		private MusicScoreInfo _musicScoreInfo;

		[AsyncStateMachine(typeof(_003COnPreBootAsync_003Ed__1))]
		protected override UniTask OnPreBootAsync(CancellationToken ct)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003COnPostBootAsync_003Ed__2))]
		protected override UniTask OnPostBootAsync(CancellationToken ct)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003COnPreEnterDetailAsync_003Ed__3))]
		private UniTask OnPreEnterDetailAsync(CancellationToken ct)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003COnPostExitDetailAsync_003Ed__4))]
		private UniTask OnPostExitDetailAsync(CancellationToken ct)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003COnPostExitAsync_003Ed__5))]
		protected override UniTask OnPostExitAsync(CancellationToken ct)
		{
			throw null;
		}

		private void OnMusicScoreDeleted(string id)
		{
			throw null;
		}

		private void UpdateSelectedMusicScore(int dataIndex)
		{
			throw null;
		}

		private void OnSelectFilterTab(int index)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CApplyFilterAsync_003Ed__9))]
		private UniTask ApplyFilterAsync(int index, CancellationToken ct)
		{
			throw null;
		}

		private void OnClickFilterButton()
		{
			throw null;
		}

		private void OnClickSortOrderButton(SortOrderBy sortOrderBy)
		{
			throw null;
		}

		private void OnFiltered(MusicScoreFilterData filterData)
		{
			throw null;
		}

		private void OnToCreatorButtonClicked()
		{
			throw null;
		}

		private void OnDecide()
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CExecuteCreateMusicScoreAsync_003Ed__15))]
		private UniTask ExecuteCreateMusicScoreAsync()
		{
			throw null;
		}

		protected override void OnPause()
		{
			throw null;
		}

		protected override void OnResume()
		{
			throw null;
		}

		public override void Dispose()
		{
			throw null;
		}

		public BookMarkMusicScoreListSelectForCreateContent()
		{
			throw null;
		}
	}
}
