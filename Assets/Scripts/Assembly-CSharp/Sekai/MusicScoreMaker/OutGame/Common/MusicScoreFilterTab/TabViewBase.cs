using Sekai.UI;
using UnityEngine;

namespace Sekai.MusicScoreMaker.OutGame.Common.MusicScoreFilterTab
{
	public abstract class TabViewBase : MonoBehaviour
	{
		[SerializeField]
		protected CustomTextMesh _title;

		[SerializeField]
		protected GameObject _offPartsRoot;

		[SerializeField]
		protected GameObject _onPartsRoot;

		protected void ApplyTitle(string title)
		{
			throw null;
		}

		public void ApplySelected(bool isSelected)
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

		private void RefreshTitleColor(bool isSelected)
		{
			throw null;
		}

		protected TabViewBase()
		{
			throw null;
		}
	}
}
