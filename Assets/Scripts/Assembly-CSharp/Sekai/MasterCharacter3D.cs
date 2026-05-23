using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class MasterCharacter3D : IMessagePackSerializationCallbackReceiver
	{
		[Key("id")]
		public int id;

		[Key("characterType")]
		public string characterType;

		[Key("characterId")]
		public int characterId;

		[Key("name")]
		public string name;

		[Key("headCostume3dId")]
		public int headCostume3dId;

		[Key("hairCostume3dId")]
		public int hairCostume3dId;

		[Key("bodyCostume3dId")]
		public int bodyCostume3dId;

		[Key("unit")]
		public string unit;

		[Key("lookAtLimitX")]
		public float lookAtLimitX;

		[Key("lookAtLimitY")]
		public float lookAtLimitY;

		public UnitType GetUnitType()
		{
			throw null;
		}

		public void OnBeforeSerialize()
		{
			throw null;
		}

		public void OnAfterDeserialize()
		{
			throw null;
		}

		public MasterCharacter3D()
		{
		}
	}
}
