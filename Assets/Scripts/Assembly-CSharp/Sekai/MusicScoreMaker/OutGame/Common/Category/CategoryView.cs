using System;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Sekai.MusicScoreMaker.OutGame.Common.Content;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame.Common.Category
{
	public class CategoryView : MonoBehaviour, IDisposable
	{
		private ContentPool _contentPool;

		public void Setup()
		{
			throw null;
		}

		public UniTask ShowAsync()
		{
			throw null;
		}

		public UniTask HideAsync()
		{
			throw null;
		}

		[CanBeNull]
		public GameObject CreateContent(Defines.ContentType contentType)
		{
			throw null;
		}

		public void Dispose()
		{
			throw null;
		}

		private void ClearContent()
		{
			throw null;
		}

		public CategoryView()
		{
			throw null;
		}
	}
}
