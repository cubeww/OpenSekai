using System;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame
{
	public sealed class MusicScoreTagSelectDialog : Common2ButtonDialog, IDisposable
	{
		[SerializeField]
		private MusicScoreTagSelectList _tagList;

		private Action<int[]> _onDecide;

		public static void Show(Action<int[]> onDecide, Action onCancel = null, int[] alreadySelectedTagIds = null)
		{
			throw null;
		}

		private void Setup(Action<int[]> onDecide, int[] alreadySelectedTagIds = null)
		{
			throw null;
		}

		private void RefreshDecideButton()
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

		public MusicScoreTagSelectDialog()
		{
			throw null;
		}
	}
}
