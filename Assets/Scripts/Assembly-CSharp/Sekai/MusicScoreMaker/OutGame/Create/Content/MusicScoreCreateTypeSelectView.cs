using System;
using Sekai.MusicScoreMaker.OutGame.Common.Content;
using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame.Create.Content
{
	public sealed class MusicScoreCreateTypeSelectView : ContentViewBase<MusicScoreCreateTypeSelectViewData>
	{
		[SerializeField]
		private CustomButton _createNewButton;

		[SerializeField]
		private CustomButton _createFromOfficialScoreButton;

		[SerializeField]
		private CustomButton _createFromMyScoreButton;

		[SerializeField]
		private CustomButton _createFromBookmarkButton;

		public void Setup(Action onClickCreateNew, Action onClickCreateFromOfficial, Action onClickCreateFromMy, Action onClickCreateFromBookmark)
		{
			throw null;
		}

		public override void Dispose()
		{
			throw null;
		}

		private void ClearCallbacks()
		{
			throw null;
		}

		public MusicScoreCreateTypeSelectView()
		{
			throw null;
		}
	}
}
