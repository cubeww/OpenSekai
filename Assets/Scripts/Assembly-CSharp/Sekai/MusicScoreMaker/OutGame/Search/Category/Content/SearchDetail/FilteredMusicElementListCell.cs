using System;
using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame.Search.Category.Content.SearchDetail
{
	public sealed class FilteredMusicElementListCell : ListViewItem
	{
		[SerializeField]
		private CustomTextMesh _titleText;

		[SerializeField]
		private CustomTextMesh _artistNameText;

		[SerializeField]
		private GameObject _noResultTextObject;

		[SerializeField]
		private CustomButton _applyButton;

		public void Setup(string title, string artistName, Action onApply)
		{
			throw null;
		}

		public void SetupAsNoResult()
		{
			throw null;
		}

		public void Setup(string title, string artistName)
		{
			throw null;
		}

		public void Clear()
		{
			throw null;
		}

		public void Show()
		{
			throw null;
		}

		public void Hide()
		{
			throw null;
		}

		private void SetTitle(string title)
		{
			throw null;
		}

		private void SetArtistName(string artistName)
		{
			throw null;
		}

		public FilteredMusicElementListCell()
		{
			throw null;
		}
	}
}
