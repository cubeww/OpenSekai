using System.Collections.Generic;
using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	public class SpecialMaterialExchangeHeaderExtension : HeaderExtensionBase
	{
		[SerializeField]
		private CustomTextMesh _ceilItemName;

		[SerializeField]
		private RectTransform _ceilItemRoot;

		[SerializeField]
		private MaterialExchangeHeaderCeilItem _ceilItem;

		private List<MaterialExchangeHeaderCeilItem> _exchangeCeilItemList;

		public override HeaderCategory HeaderCategory
		{
			get
			{
				throw null;
			}
		}

		public void Setup(MasterMaterialExchangeSummary summary)
		{
			throw null;
		}

		private void UpdateExchangeCeilItem(params MaterialExchangeHeaderCeilItem.ViewData[] data)
		{
			throw null;
		}

		private string GetCeilItemName(MasterMaterialExchangeSummary summary)
		{
			throw null;
		}

		public SpecialMaterialExchangeHeaderExtension()
		{
		}
	}
}
