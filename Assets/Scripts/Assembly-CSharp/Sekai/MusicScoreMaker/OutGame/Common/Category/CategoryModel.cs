using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Sekai.MusicScoreMaker.OutGame.Common.Category
{
	public class CategoryModel
	{
		private readonly Stack<ContentNavigationData> _history;

		public Defines.ContentType CurrentContentType
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			set
			{
				throw null;
			}
		}

		public void PushContent(ContentNavigationData historyData)
		{
			throw null;
		}

		public bool TryPopContent(out ContentNavigationData historyData)
		{
			throw null;
		}

		public bool TryPeekContent(out ContentNavigationData historyData)
		{
			throw null;
		}

		public void ClearContent()
		{
			throw null;
		}

		public bool IsEnableBack()
		{
			throw null;
		}

		public CategoryModel()
		{
			throw null;
		}
	}
}
