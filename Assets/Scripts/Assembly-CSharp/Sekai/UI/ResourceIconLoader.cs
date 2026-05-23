using CP;
using Sekai.Constants;
using UnityEngine;

namespace Sekai.UI
{
	public class ResourceIconLoader : MonoBehaviour
	{
		[SerializeField]
		private CustomImage imageIcon;

		[SerializeField]
		private CustomRawImage rawImageIcon;

		public void Load(ResourceType resourceType, int resourceId)
		{
			SetImageActive(false);
			SetRawImageActive(false);

			switch (resourceType)
			{
				case ResourceType.virtual_coin:
					SetImageSprite("icon_virtual_coin");
					break;
				case ResourceType.jewel:
				case ResourceType.paid_jewel:
					SetImageSprite("icon_jewel");
					break;
				case ResourceType.none:
					break;
				case ResourceType.gacha_ticket:
					SetRawTexture(null);
					// TODO(original): restore GachaUtility.GetTicketResourceTex(resourceId).
					break;
				default:
					LogUtility.LogError("ResourceIconLoader unsupported resource type: {0}", resourceType);
					break;
			}
		}

		private void SetImageSprite(string spriteName)
		{
			if (imageIcon == null)
			{
				return;
			}

			imageIcon.gameObject.SetActive(true);
			imageIcon.SpriteName = spriteName;
		}

		private void SetRawTexture(Texture texture)
		{
			if (rawImageIcon == null)
			{
				return;
			}

			rawImageIcon.gameObject.SetActive(true);
			rawImageIcon.texture = texture;
		}

		private void SetImageActive(bool active)
		{
			if (imageIcon != null)
			{
				imageIcon.gameObject.SetActive(active);
			}
		}

		private void SetRawImageActive(bool active)
		{
			if (rawImageIcon != null)
			{
				rawImageIcon.gameObject.SetActive(active);
			}
		}

		public ResourceIconLoader()
		{
		}
	}
}
