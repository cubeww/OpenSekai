using System;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame.Search.Category.Content.SearchDetail
{
	public sealed class MusicScoreTagFilterDialog : Common2ButtonDialog, IDisposable
	{
		[SerializeField]
		private MusicScoreTagFilterList _tagList;

		private Action<int> _onDecide;

		public static void Show(Action<int> onDecide, Action onCancel = null, int initialSelectedTagId = -1)
		{
			throw null;
		}

		private void Setup(Action<int> onDecide, int initialSelectedTagId)
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

		public MusicScoreTagFilterDialog()
		{
			throw null;
		}
	}
}
