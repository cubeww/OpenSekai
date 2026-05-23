using System;
using System.Collections.Generic;
using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame.Publish
{
	public sealed class MusicScorePublishTagView : MonoBehaviour, IDisposable
	{
		[SerializeField]
		private RectTransform _tagSelectedRoot;

		[SerializeField]
		private MusicScorePublishTagCell _tagCellPrefab;

		[SerializeField]
		private CustomButton _tagSelectStartButton;

		[SerializeField]
		private GameObject _addTagMarker;

		[SerializeField]
		private CustomTextMesh _addTagText;

		private readonly List<MusicScorePublishTagCell> _tagCellList;

		private Action<int> _onTagRemoved;

		public void Setup(Action<int> onTagRemoved, Action onTagSelectStartButtonClicked)
		{
			throw null;
		}

		public void RefreshTags(List<int> selectedTagIds)
		{
			throw null;
		}

		private void ClearAllTagCells()
		{
			throw null;
		}

		private void RefreshTagUI(int currentTagCount)
		{
			throw null;
		}

		private void OnTagCellRemoveClicked(int tagId)
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

		public MusicScorePublishTagView()
		{
			throw null;
		}
	}
}
