using System;
using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.Outgame
{
	public sealed class SaveDraftMusicScoreSlotCell : ListViewItem, IDisposable
	{
		[SerializeField]
		private CustomButton _selectButton;

		[SerializeField]
		private RectTransform _contentRoot;

		[SerializeField]
		private RectTransform _emptyRoot;

		[SerializeField]
		private CustomTextMesh _numberText;

		[SerializeField]
		private UITextureLoader _musicJacketLoader;

		[SerializeField]
		private CustomTextMesh _titleText;

		[SerializeField]
		private CustomTextMesh _memoText;

		[SerializeField]
		private CustomTextMesh _saveDateText;

		[SerializeField]
		private RectTransform _baseMusicScoreMarker;

		[SerializeField]
		private RectTransform _selectedFrame;

		private SaveDraftMusicScoreSlotCellData _cellData;

		public void Initialize(SaveDraftMusicScoreSlotCellData cellData, Action<int> onSelectedCallback)
		{
			throw null;
		}

		public void Refresh()
		{
			throw null;
		}

		public void RefreshSelected()
		{
			throw null;
		}

		private void LoadJacketIfNeeded()
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

		public void Dispose()
		{
			throw null;
		}

		public SaveDraftMusicScoreSlotCell()
		{
			throw null;
		}
	}
}
