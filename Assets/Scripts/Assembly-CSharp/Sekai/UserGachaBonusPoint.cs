using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class UserGachaBonusPoint
	{
		public const float POINT_MAX = 100f;

		[Key("userId")]
		public long userId;

		[Key("gachaId")]
		public int gachaId;

		[Key("gachaBonusPoint")]
		public float gachaBonusPoint;

		[Key("totalGachaBonusPoint")]
		public float totalGachaBonusPoint;

		public UserGachaBonusPoint(int gachaId)
		{
			throw null;
		}
	}
}
