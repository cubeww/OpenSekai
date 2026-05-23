using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Sekai.MusicScoreMaker.OutGame
{
	public sealed class MusicScoreTagsView : MonoBehaviour, IDisposable
	{
		[SerializeField]
		private LayoutGroup _tagLayoutGroup;

		[SerializeField]
		private MusicScoreTagCell _tagCellPrefab;

		private readonly List<MusicScoreTagCell> _cellPool;

		public void Setup([NotNull] string[] tagNames)
		{
			throw null;
		}

		public void SetActive(bool value)
		{
			throw null;
		}

		public void Dispose()
		{
			throw null;
		}

		public MusicScoreTagsView()
		{
			throw null;
		}
	}
}
