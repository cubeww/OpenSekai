using System.Runtime.CompilerServices;
using MessagePack;

namespace Sekai.ApiData
{
	[MessagePackObject(false)]
	public class UserPlayerFrame : IMessagePackSerializationCallbackReceiver
	{
		[Key("playerFrameId")]
		public int playerFrameId;

		[Key("playerFrameAttachStatus")]
		public string playerFrameAttachStatus;

		[IgnoreMember]
		public PlayerFrameAttachStatus PlayerFrameAttachStatus
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

		public void OnBeforeSerialize()
		{
			throw null;
		}

		public void OnAfterDeserialize()
		{
			throw null;
		}

		public void InitPlayerFrameAttachStatus()
		{
			throw null;
		}

		public UserPlayerFrame()
		{
		}
	}
}
