using System;
using JetBrains.Annotations;
using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.Outgame
{
	public sealed class AutoSaveMusicScoreSlotCell : ListViewItem, IDisposable
	{
		[SerializeField]
		private CustomButton _selectButton;

		[SerializeField]
		private CustomTextMesh _numberText;

		[SerializeField]
		private UITextureLoader _musicJacketLoader;

		[SerializeField]
		private CustomTextMesh _saveDateText;

		[SerializeField]
		private RectTransform _baseMusicScoreMarker;

		[SerializeField]
		private RectTransform _selectedFrame;

		private AutoSaveMusicScoreSlotCellData _cellData;

		public void Initialize([NotNull] AutoSaveMusicScoreSlotCellData cellData, Action<int> onSelectedCallback)
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

		public AutoSaveMusicScoreSlotCell()
		{
			throw null;
		}
	}
}
