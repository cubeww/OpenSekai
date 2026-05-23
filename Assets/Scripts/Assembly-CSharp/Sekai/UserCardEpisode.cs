using System.Runtime.CompilerServices;
using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class UserCardEpisode : IMessagePackSerializationCallbackReceiver
	{
		[Key("cardEpisodeId")]
		public int cardEpisodeId;

		[Key("scenarioStatus")]
		public string scenarioStatus;

		[Key("scenarioStatusReasons")]
		public string[] scenarioStatusReasons;

		[Key("isNotSkipped")]
		public bool isNotSkipped;

		[IgnoreMember]
		public EpisodeStatus ScenarioStatus
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

		[IgnoreMember]
		public bool IsReleased
		{
			get
			{
				throw null;
			}
		}

		[IgnoreMember]
		public bool IsAlreadyRead
		{
			get
			{
				throw null;
			}
		}

		[IgnoreMember]
		public EpisodeStatusReason[] ScenarioStatusReasons
		{
			get
			{
				throw null;
			}
		}

		public UserCardEpisode(UserCardEpisode source)
		{
			throw null;
		}

		public UserCardEpisode()
		{
		}

		void IMessagePackSerializationCallbackReceiver.OnAfterDeserialize()
		{
			throw null;
		}

		public void OnBeforeSerialize()
		{
			throw null;
		}
	}
}
