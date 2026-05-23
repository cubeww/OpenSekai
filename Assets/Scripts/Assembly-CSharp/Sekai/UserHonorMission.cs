using System.Runtime.CompilerServices;
using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class UserHonorMission : IMessagePackSerializationCallbackReceiver
	{
		[Key("userId")]
		public long userId;

		[Key("honorMissionType")]
		public string honorMissionType;

		[Key("progress")]
		public int progress;

		[Key("achievedMissionIds")]
		public int[] achievedMissionIds;

		[IgnoreMember]
		public HonorMissionType Type
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

		void IMessagePackSerializationCallbackReceiver.OnAfterDeserialize()
		{
			throw null;
		}

		void IMessagePackSerializationCallbackReceiver.OnBeforeSerialize()
		{
			throw null;
		}

		public UserHonorMission()
		{
		}
	}
}
