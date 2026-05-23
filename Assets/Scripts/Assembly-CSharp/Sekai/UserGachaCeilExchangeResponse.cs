using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class UserGachaCeilExchangeResponse
	{
		[Key("obtainUserResources")]
		public UserResource[] obtainUserResources;

		[Key("updatedResources")]
		// TODO(original): restore SuiteUser once the broader user-data API surface is needed.
		// MusicScoreMaker only needs this class to satisfy the gacha header dependency chain;
		// keeping it as object avoids importing the entire main-game user model graph.
		public object updatedResources;

		public UserGachaCeilExchangeResponse()
		{
		}
	}
}
