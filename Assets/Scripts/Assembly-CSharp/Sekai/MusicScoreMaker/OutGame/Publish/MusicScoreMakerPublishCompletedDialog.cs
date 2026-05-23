using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame.Publish
{
	public sealed class MusicScoreMakerPublishCompletedDialog : SubWindowDialog, IDisposable
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CShowAsync_003Ed__11 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public MusicScorePublishCompletedViewData viewData;

			public CancellationToken ct;

			private MusicScoreMakerPublishCompletedDialog _003Cdialog_003E5__2;

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

		private static readonly string AnimationStateName;

		[SerializeField]
		private CustomTextMesh _musicScoreTitleText;

		[SerializeField]
		private CustomTextMesh _musicTitleText;

		[SerializeField]
		private UIPartsMusicDifficultyPlayLevel _musicDifficultyPlayLevel;

		[SerializeField]
		private CustomTextMesh _musicScoreIdText;

		[SerializeField]
		private UITextureLoader _musicJacketLoader;

		[SerializeField]
		private UIPartsMusicDifficulty _musicDifficulty;

		[SerializeField]
		private Animator _animator;

		[SerializeField]
		private CustomButton _copyIdButton;

		[SerializeField]
		private GameObject _defaultDifficultyParent;

		[SerializeField]
		private GameObject _appendDifficultyParent;

		[AsyncStateMachine(typeof(_003CShowAsync_003Ed__11))]
		public static UniTask ShowAsync(MusicScorePublishCompletedViewData viewData, CancellationToken ct)
		{
			throw null;
		}

		private void Setup(MusicScorePublishCompletedViewData viewData)
		{
			throw null;
		}

		protected override void InitializeAnimation()
		{
			throw null;
		}

		protected override void OnFinishOpenAnimation()
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

		public MusicScoreMakerPublishCompletedDialog()
		{
			throw null;
		}

		static MusicScoreMakerPublishCompletedDialog()
		{
			throw null;
		}
	}
}
