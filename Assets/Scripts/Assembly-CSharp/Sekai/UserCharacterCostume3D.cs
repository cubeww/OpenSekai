using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class UserCharacterCostume3D
	{
		[Key("characterId")]
		public int characterId;

		[Key("unit")]
		public string unit;

		[Key("headCostume3dId")]
		public int headCostume3dId;

		[Key("hairCostume3dId")]
		public int hairCostume3dId;

		[Key("bodyCostume3dId")]
		public int bodyCostume3dId;

		[IgnoreMember]
		public UnitType UnitType
		{
			get
			{
				throw null;
			}
		}

		public UserCharacterCostume3D()
		{
		}

		public UserCharacterCostume3D(int characterId, string unit, int headCostume3dId, int hairCostume3dId, int bodyCostume3dId)
		{
			throw null;
		}
	}
}
