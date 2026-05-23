using System.Runtime.CompilerServices;
using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class MVOnlyDeckCostumeLocalData : IMessagePackSerializationCallbackReceiver
	{
		[Key("memberIndex")]
		public int memberIndex;

		[Key("headCostume3dId")]
		public int headCostume3dId;

		[Key("hairCostume3dId")]
		public int hairCostume3dId;

		[Key("bodyCostume3dId")]
		public int bodyCostume3dId;

		[Key("unit")]
		public string unit;

		[IgnoreMember]
		public UnitType UnitType
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

		public MVOnlyDeckCostumeLocalData()
		{
			throw null;
		}

		public MVOnlyDeckCostumeLocalData(int memberIndex, UnitType unitType)
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

		public void SetUnitType(UnitType unitType)
		{
			throw null;
		}
	}
}
