using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using JetBrains.Annotations;
using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame.SaveDraft
{
	public sealed class MusicScoreSaveDraftConfirmDialog : Common2ButtonDialog, IDisposable
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass3_0
		{
			public bool isFinished;

			public SaveDraftRequestData result;

			public _003C_003Ec__DisplayClass3_0()
			{
				throw null;
			}

			internal void _003CShowAsync_003Eb__0()
			{
				throw null;
			}

			internal void _003CShowAsync_003Eb__1()
			{
				throw null;
			}

			internal void _003CShowAsync_003Eb__2(SaveDraftRequestData x)
			{
				throw null;
			}

			internal bool _003CShowAsync_003Eb__3()
			{
				throw null;
			}
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CShowAsync_003Ed__3 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder<SaveDraftRequestData> _003C_003Et__builder;

			public string messageBodyKey;

			public string okButtonLabelKey;

			public string title;

			public string memo;

			public CancellationToken ct;

			private _003C_003Ec__DisplayClass3_0 _003C_003E8__1;

			private MusicScoreSaveDraftConfirmDialog _003Cdialog_003E5__2;

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
		private CustomInputFieldTextMesh _titleInputField;

		[SerializeField]
		private CustomInputFieldTextMesh _memoInputField;

		private Action<SaveDraftRequestData> _onRequestSaveDraftCallback;

		[AsyncStateMachine(typeof(_003CShowAsync_003Ed__3))]
		[ItemCanBeNull]
		public static UniTask<SaveDraftRequestData> ShowAsync(string messageBodyKey, string okButtonLabelKey, string title, string memo, CancellationToken ct)
		{
			throw null;
		}

		private void Setup(string title, string memo, Action<SaveDraftRequestData> onRequestSaveDraftCallback)
		{
			throw null;
		}

		protected override void OnClickOK()
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

		public MusicScoreSaveDraftConfirmDialog()
		{
			throw null;
		}
	}
}
