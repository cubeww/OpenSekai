using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class MasterCardRarity
	{
		[Key("cardRarityType")]
		public string cardRarityType;

		[Key("seq")]
		public int seq;

		[Key("maxLevel")]
		public int maxLevel;

		[Key("trainingMaxLevel")]
		public int trainingMaxLevel;

		[Key("maxSkillLevel")]
		public int maxSkillLevel;

		[IgnoreMember]
		public CardRarityType Type
		{
			get
			{
				throw null;
			}
		}

		[IgnoreMember]
		public bool ValidSpecialTraining
		{
			get
			{
				throw null;
			}
		}

		public MasterCardRarity()
		{
		}
	}
}
