using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.Outgame
{
	public sealed class MusicScoreListCellHighlightContents : MusicScoreListCellContentsBase
	{
		[SerializeField]
		private MusicScoreInfoCellCommonView _infoCellCommonView;

		[SerializeField]
		private UIPartsMusicDifficulty _musicDifficulty;

		[SerializeField]
		private CustomTextMesh _authorNameText;

		[SerializeField]
		private GameObject _derivativeDisallowedIndicator;

		[SerializeField]
		private GameObject _derivativeAllowedBadge;

		[SerializeField]
		private GameObject _baseMusicScoreBadge;

		[SerializeField]
		private RectTransform _scoreTitleRect;

		[SerializeField]
		private RectTransform _authorNameRect;

		[SerializeField]
		private RectTransform _countContentRect;

		[SerializeField]
		private RectTransform _markerRect;

		[SerializeField]
		private Vector2 _scoreTitlePosWithMarker;

		[SerializeField]
		private Vector2 _authorNamePosWithMarker;

		[SerializeField]
		private Vector2 _countContentPosWithMarker;

		[SerializeField]
		private Vector2 _markerPosWithMarker;

		[SerializeField]
		private Vector2 _scoreTitlePosDefault;

		[SerializeField]
		private Vector2 _authorNamePosDefault;

		[SerializeField]
		private Vector2 _countContentPosDefault;

		public void UpdateView(MusicScoreInfoCellCommonData cellCommonData)
		{
			throw null;
		}

		private void UpdatePosition(bool hasMarker)
		{
			throw null;
		}

		public void UpdateBookmarkState(bool isBookmarked)
		{
			throw null;
		}

		public MusicScoreListCellHighlightContents()
		{
			throw null;
		}
	}
}
