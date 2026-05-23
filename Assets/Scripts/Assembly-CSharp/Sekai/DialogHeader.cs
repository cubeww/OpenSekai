using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	public class DialogHeader : MonoBehaviour
	{
		[SerializeField]
		private CustomText title;

		public CustomText Title => title;

		public string TitleText
		{
			get => title != null ? title.Text : string.Empty;
			set
			{
				if (title != null)
				{
					title.UseWordingKey = false;
					title.SetText(value);
				}
			}
		}

		public string TitleKey
		{
			get => title != null ? title.WordingKey : string.Empty;
			set
			{
				if (title != null)
				{
					title.UseWordingKey = true;
					title.SetWordingText(value);
				}
			}
		}
	}
}
