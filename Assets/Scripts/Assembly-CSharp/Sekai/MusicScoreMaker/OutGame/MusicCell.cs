using System;
using System.Threading;
using DG.Tweening;
using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.Outgame
{
	public sealed class MusicCell : ListViewItem
	{
		[SerializeField]
		private CustomButton _button;

		private MusicCellData _cellData;

		[Header("親キャンバスを指定してください")]
		[SerializeField]
		private CanvasGroup _contentsCanvasGroup;

		[SerializeField]
		private UIPartsIconInfoView _iconInfoView;

		[Header("楽曲名表示用のテキストを指定してください")]
		[SerializeField]
		private CustomTextMesh _musicNameText;

		[SerializeField]
		private UITextureLoader _jacketLoader;

		[SerializeField]
		private UIPartsReleaseConditionsBalloon _transitionBalloon;

		[SerializeField]
		private CustomButton _lockButton;

		private bool _isLoading;

		private Tweener _jacketLoadSkip;

		private CancellationTokenSource _cts;

		private bool IsEmptyCell
		{
			get
			{
				throw null;
			}
		}

		public void SetupEmpty()
		{
			throw null;
		}

		public void Setup(MusicCellData cellData, Action<MusicCellData> onClick)
		{
			throw null;
		}

		private void SetInfoView(bool isReleased)
		{
			throw null;
		}

		private void SetAllMusicData(string musicName)
		{
			throw null;
		}

		private void LoadJacket(MusicCellData cellData)
		{
			throw null;
		}

		private void SetUpJacketTitle(string soucndName)
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

		private void OnClickLock()
		{
			throw null;
		}

		public MusicCell()
		{
			throw null;
		}
	}
}
