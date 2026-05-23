using System;
using System.Runtime.CompilerServices;
using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	[RequireComponent(typeof(CustomButton))]
	public class UIPartsSortOrder : MonoBehaviour
	{
		[SerializeField]
		private CustomButton _button;

		[SerializeField]
		protected CustomImage ascImage;

		[SerializeField]
		protected CustomImage descImage;

		private Action<SortOrderBy> onChangeSortOrderBy;

		private CustomButton button;

		public SortOrderBy SortOrderBy
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
			[CompilerGenerated]
			private set
			{
				throw null;
			}
		}

		public void Initialize(Action<SortOrderBy> onChangeSortOrderBy)
		{
			throw null;
		}

		public void Setup(SortOrderBy initOrder, Action<SortOrderBy> onChangeSortOrderBy)
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

		private void SetupButton()
		{
			throw null;
		}

		public void SetSortOrder(SortOrderBy sortOrder)
		{
			throw null;
		}

		public void EnableClick()
		{
			throw null;
		}

		public void DisableClick()
		{
			throw null;
		}

		private void UpdateSortOrder()
		{
			throw null;
		}

		private void OnClickSortOrderBy()
		{
			throw null;
		}

		public void SetIntractable(bool interactable)
		{
			throw null;
		}

		public UIPartsSortOrder()
		{
		}
	}
}
