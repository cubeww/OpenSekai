using UnityEngine.U2D;

namespace Sekai
{
	public interface IUIPartsCharacterIconConfiguration
	{
		int Character2DId { get; }

		string IconSpriteName { get; }

		UIPartsCharacterIcon.Size IconSize { get; }

		bool UseAssetBundle { get; }

		SpriteAtlas CharacterIconAtlas { get; }
	}
}
