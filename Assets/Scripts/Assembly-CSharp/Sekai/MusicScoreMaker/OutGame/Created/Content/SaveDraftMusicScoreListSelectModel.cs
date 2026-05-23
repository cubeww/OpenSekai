using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using JetBrains.Annotations;
using Sekai.ApiData;
using Sekai.MusicScoreMaker.OutGame.Common.Content;

namespace Sekai.MusicScoreMaker.OutGame.Created.Content
{
	public sealed class SaveDraftMusicScoreListSelectModel : ContentModelBase<SaveDraftMusicScoreListSelectViewData>
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CCreateDataAsync_003Ed__11 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public SaveDraftMusicScoreListSelectModel _003C_003E4__this;

			private UniTask<UserCustomMusicScoreDraftListResponse>.Awaiter _003C_003Eu__1;

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

		private IEnumerable<UserCustomMusicScoreDraft> _slotDataList;

		public int SelectedSlotNo
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

		public bool IsSelectedSlotValid
		{
			get
			{
				throw null;
			}
		}

		public bool IsSelectedSavedSlot
		{
			get
			{
				throw null;
			}
		}

		public void ResetSelectedSlot()
		{
			throw null;
		}

		public void Setup(Defines.SaveDraftListType listType)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CCreateDataAsync_003Ed__11))]
		public UniTask CreateDataAsync()
		{
			throw null;
		}

		public void ApplySlotData([CanBeNull] IEnumerable<UserCustomMusicScoreDraft> slotData)
		{
			throw null;
		}

		public void ApplySelectedSlot(int slotNumber)
		{
			throw null;
		}

		[CanBeNull]
		public UserCustomMusicScoreDraft GetSelectedMusicScoreDraft()
		{
			throw null;
		}

		private void CreateSlotCellDataArray()
		{
			throw null;
		}

		private void ApplyButtonEnabled()
		{
			throw null;
		}

		public SaveDraftMusicScoreListSelectModel()
		{
			throw null;
		}
	}
}
