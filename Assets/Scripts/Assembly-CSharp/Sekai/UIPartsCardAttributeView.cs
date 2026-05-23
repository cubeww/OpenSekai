using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	[RequireComponent(typeof(CustomImage))]
	public class UIPartsCardAttributeView : MonoBehaviour
	{
		private enum AttributeSizeType
		{
			Small = 0,
			Large = 1
		}

		[SerializeField]
		private AttributeSizeType _attributeSize;

		[SerializeField]
		private CustomImage _attributeImage;

		public bool ActiveSelf
		{
			get
			{
				throw null;
			}
		}

		public void Setup(CardAttributeType cardAttributeType)
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

		private bool IsValidType(CardAttributeType cardAttributeType)
		{
			throw null;
		}

		private string GetSpriteName(CardAttributeType cardAttributeType)
		{
			throw null;
		}

		public UIPartsCardAttributeView()
		{
			throw null;
		}
	}
}
