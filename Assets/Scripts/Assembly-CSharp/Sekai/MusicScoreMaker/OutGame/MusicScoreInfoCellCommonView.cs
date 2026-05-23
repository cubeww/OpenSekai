using System.Threading;
using DG.Tweening;
using Sekai.MusicScoreMaker.Outgame.Common;
using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.Outgame
{
	public sealed class MusicScoreInfoCellCommonView : MonoBehaviour
	{
		[SerializeField]
		[Header("ジャケットロードCanvasGroupを指定してください")]
		private CanvasGroup _jacketCanvasGroup;

		[Header("譜面名のタイトル表示用のテキストを指定してください")]
		[SerializeField]
		private CustomTextMesh _scoreTitleText;

		[SerializeField]
		private UITextureLoader _jacketLoader;

		[SerializeField]
		private CustomTextMesh _playCountText;

		[SerializeField]
		private CustomTextMesh _reviewCountText;

		[Header("難易度ラベル表示用のクラスを指定してください")]
		[SerializeField]
		private UIPartsMusicDifficultyPlayLevel _difficultyLevel;

		[SerializeField]
		private UIPartsMusicDifficultyClearStatus _clearStatus;

		[SerializeField]
		private BookmarkTag _bookmarkTag;

		private Tweener _jacketLoadSkip;

		private CancellationTokenSource _cts;

		private MusicScoreInfoCellCommonData _viewData;

		public void Setup(MusicScoreInfoCellCommonData viewData)
		{
			throw null;
		}

		private void SetAllMusicData(string titleName, MusicDifficulty difficulty, int difficultyLevelName)
		{
			throw null;
		}

		private void LoadJacket(MusicScoreInfoCellCommonData cellData)
		{
			throw null;
		}

		private void SetUpTitleText(string titleName)
		{
			throw null;
		}

		private void SetEnableView(bool enable, bool skipAnim = false)
		{
			throw null;
		}

		private void OnEnable()
		{
			throw null;
		}

		private void OnDisable()
		{
			throw null;
		}

		public void UpdateBookmarkState(bool isBookmarked)
		{
			throw null;
		}

		public MusicScoreInfoCellCommonView()
		{
			throw null;
		}
	}
}
