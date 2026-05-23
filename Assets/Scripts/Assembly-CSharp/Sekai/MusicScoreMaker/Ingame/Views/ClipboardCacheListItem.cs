using System;
using Sekai.MusicScoreMaker.Ingame.Models;
using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.Ingame.Views
{
	public class ClipboardCacheListItem : MonoBehaviour
	{
		[SerializeField]
		private CustomButton _pasteButton;

		[SerializeField]
		private ClipboardCachePreview _preview;

		private ClipboardCacheData _cacheData;

		private Action<ClipboardCacheData, bool> _onPasteCallback;

		private bool _isFlipHorizontal;

		public void Setup(ClipboardCacheData cacheData, Action<ClipboardCacheData, bool> onPaste)
		{
			_cacheData = cacheData;
			_onPasteCallback = onPaste;
			if (_pasteButton != null)
			{
				_pasteButton.onClick.RemoveListener(OnPasteButtonClicked);
				_pasteButton.onClick.AddListener(OnPasteButtonClicked);
			}
			if (_preview != null)
			{
				_preview.Setup();
			}
			UpdateDisplay();
		}

		public void SetFlipHorizontal(bool isFlipHorizontal)
		{
			_isFlipHorizontal = isFlipHorizontal;
			UpdateDisplay();
		}

		private void UpdateDisplay()
		{
			if (_cacheData != null && _preview != null)
			{
				_preview.UpdatePreview(_cacheData, _isFlipHorizontal);
			}
		}

		private void OnPasteButtonClicked()
		{
			_onPasteCallback?.Invoke(_cacheData, _isFlipHorizontal);
		}

		public ClipboardCacheListItem()
		{
		}
	}
}
