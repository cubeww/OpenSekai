using System;
using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame.Publish
{
	public sealed class MusicScorePublishTagCell : MonoBehaviour, IDisposable
	{
		[SerializeField]
		private CustomTextMesh _tagNameText;

		[SerializeField]
		private CustomButton _removeButton;

		private int _tagId;

		private Action<int> _onRemoveClicked;

		public void Setup(int tagId, string tagName, Action<int> onRemoveClicked)
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

		private void OnRemoveButtonClicked()
		{
			throw null;
		}

		public void Dispose()
		{
			throw null;
		}

		private void OnDestroy()
		{
			throw null;
		}

		public MusicScorePublishTagCell()
		{
			throw null;
		}
	}
}
