using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using JetBrains.Annotations;
using Sekai.MusicScoreMaker.Ingame.Models;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame.Publish
{
	public sealed class MusicScoreMakerPublishConfirmDialog : Common2ButtonDialog, IDisposable
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass3_0
		{
			public bool isFinished;

			public MusicScorePublishRequestData result;

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

			internal void _003CShowAsync_003Eb__2(MusicScorePublishRequestData x)
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

			public AsyncUniTaskMethodBuilder<MusicScorePublishRequestData> _003C_003Et__builder;

			public MusicScoreMakerData musicScoreData;

			public bool hasBaseMusicScore;

			public MusicDifficulty recommendedDifficulty;

			public CancellationToken ct;

			private _003C_003Ec__DisplayClass3_0 _003C_003E8__1;

			private MusicScoreMakerPublishConfirmDialog _003Cdialog_003E5__2;

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
		private MusicScorePublishConfirmView _view;

		private MusicScorePublishConfirmModel _model;

		private Action<MusicScorePublishRequestData> _onRequestSubmitCallback;

		[ItemCanBeNull]
		[AsyncStateMachine(typeof(_003CShowAsync_003Ed__3))]
		public static UniTask<MusicScorePublishRequestData> ShowAsync([NotNull] MusicScoreMakerData musicScoreData, bool hasBaseMusicScore, MusicDifficulty recommendedDifficulty, CancellationToken ct)
		{
			throw null;
		}

		private void Setup([NotNull] MusicScoreMakerData musicScoreData, bool hasBaseMusicScore, MusicDifficulty recommendedDifficulty, Action<MusicScorePublishRequestData> onRequestSubmitCallback)
		{
			throw null;
		}

		private void OnEndEditTitleSuccess([NotNull] string title)
		{
			throw null;
		}

		private void OnEndEditTitleError()
		{
			throw null;
		}

		private void OnEndEditDescriptionSuccess([NotNull] string description)
		{
			throw null;
		}

		private void OnEndEditDescriptionError()
		{
			throw null;
		}

		private void OnDifficultySelected(MusicDifficulty difficulty)
		{
			throw null;
		}

		private void OnPlayLevelSelected(int index)
		{
			throw null;
		}

		private void OnDerivativeAllowedChanged(int index)
		{
			throw null;
		}

		private void OnTagSelectStartButtonClicked()
		{
			throw null;
		}

		private void OnTagsSelected(int[] selectedTagIds)
		{
			throw null;
		}

		private void OnTagRemoved(int tagId)
		{
			throw null;
		}

		private void RefreshMusicScore()
		{
			throw null;
		}

		private void RefreshTitle()
		{
			throw null;
		}

		private void RefreshDescription()
		{
			throw null;
		}

		private void RefreshDifficulty()
		{
			throw null;
		}

		private void RefreshPlayLevel()
		{
			throw null;
		}

		private void RefreshTags()
		{
			throw null;
		}

		private void RefreshDerivativeAllowed()
		{
			throw null;
		}

		private void RefreshSubmitButton()
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

		public MusicScoreMakerPublishConfirmDialog()
		{
			throw null;
		}
	}
}
