using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame.SaveDraft
{
	public sealed class MusicScoreSaveDraftCompletedDialog : SubWindowDialog, IDisposable
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CShowAsync_003Ed__4 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public SaveDraftCompletedViewData viewData;

			public CancellationToken ct;

			private MusicScoreSaveDraftCompletedDialog _003Cdialog_003E5__2;

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
		private CustomTextMesh _titleText;

		[SerializeField]
		private CustomTextMesh _memoText;

		[SerializeField]
		private CustomTextMesh _savedAtText;

		[SerializeField]
		private UITextureLoader _musicJacketLoader;

		[AsyncStateMachine(typeof(_003CShowAsync_003Ed__4))]
		public static UniTask ShowAsync(SaveDraftCompletedViewData viewData, CancellationToken ct)
		{
			throw null;
		}

		private void Setup(SaveDraftCompletedViewData viewData)
		{
			throw null;
		}

		public override void Close()
		{
			throw null;
		}

		public void Dispose()
		{
			throw null;
		}

		public MusicScoreSaveDraftCompletedDialog()
		{
			throw null;
		}
	}
}
