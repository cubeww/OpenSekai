using System;
using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame.Search.Category.Content.SearchDetail
{
	public sealed class MusicElementFreeWordFilterView : MonoBehaviour, IDisposable
	{
		[SerializeField]
		private CustomInputFieldTextMesh _inputField;

		[SerializeField]
		private GameObject _searchIconObject;

		[SerializeField]
		private CustomButton _clearFilterButton;

		[SerializeField]
		private FilteredMusicElementCell _filteredMusicElementCell;

		private Action<string> _onValueChanged;

		private Action<string> _onSubmit;

		private Action _onFocus;

		public void Setup(Action onFocus, Action<string> onValueChanged, Action<string> onSubmit, Action onClearEdit)
		{
			throw null;
		}

		public void Clear(string value = null)
		{
			throw null;
		}

		public void ApplyFilteredMusicElement(FilteredMusicElementData data)
		{
			throw null;
		}

		public void Dispose()
		{
			throw null;
		}

		private void OnValueChanged(string value)
		{
			throw null;
		}

		private void OnSubmit(string value)
		{
			throw null;
		}

		private void OnFocus()
		{
			throw null;
		}

		private void Refresh()
		{
			throw null;
		}

		public MusicElementFreeWordFilterView()
		{
			throw null;
		}
	}
}
