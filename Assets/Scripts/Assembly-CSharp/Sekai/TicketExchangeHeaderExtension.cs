using UnityEngine;
using UnityEngine.Events;

namespace Sekai
{
	public class TicketExchangeHeaderExtension : HeaderExtensionBase
	{
		[SerializeField]
		private UIPartsHeaderExchangeViewPanel _panel;

		[SerializeField]
		private UIPartsHeaderExchangeViewPanel _commonTicketPanel;

		[SerializeField]
		private UIPartsFilterButton filterButton;

		[SerializeField]
		private UIPartsSortOrder sortButton;

		[SerializeField]
		private GameObject _panelParent;

		private MasterMaterialExchangeSummary summary;

		public override HeaderCategory HeaderCategory
		{
			get
			{
				throw null;
			}
		}

		private string iconName
		{
			get
			{
				throw null;
			}
		}

		private string headerTitle
		{
			get
			{
				throw null;
			}
		}

		public void UpdateView(int numOfExchangeTicket, MasterMaterialExchangeSummary summary, UnityAction onFilterButtonClicked, UnityAction onSortButtonClicked, bool isFiltered, bool isSortOrder)
		{
			throw null;
		}

		public TicketExchangeHeaderExtension()
		{
		}
	}
}
