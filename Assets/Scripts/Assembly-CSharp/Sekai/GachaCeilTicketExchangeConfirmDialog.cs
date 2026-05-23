using System.Runtime.CompilerServices;
using Beebyte.Obfuscator;
using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	public class GachaCeilTicketExchangeConfirmDialog : Common2ButtonDialog
	{
		[SerializeField]
		private CustomImage costIconImage;

		[SerializeField]
		private CustomImage haveIconImage;

		[SerializeField]
		private CustomText costNumText;

		[SerializeField]
		private CustomText haveNumText;

		[SerializeField]
		private CustomText exchangeResultNumText;

		[SerializeField]
		private CustomText exchangeHaveNumText;

		[SerializeField]
		private CustomText limitText;

		[SerializeField]
		private CustomSlider countSlider;

		[SerializeField]
		private CustomText sliderNumText;

		private int simpleCost;

		private int exchangeItemGetNum;

		private int exchangeItemHaveNum;

		private UserMaterial userCostMaterial;

		private UserGachaCeilExchange userGachaCeilExchange;

		private MasterGachaCeilExchange masterGachaCeilExchange;

		public int ItemCount
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

		public void Setup(MasterGachaCeilExchange master)
		{
			throw null;
		}

		[Skip]
		public void OnSliderValueChange()
		{
			throw null;
		}

		public GachaCeilTicketExchangeConfirmDialog()
		{
		}
	}
}
