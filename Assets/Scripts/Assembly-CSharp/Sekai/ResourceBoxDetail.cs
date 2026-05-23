using System.Runtime.CompilerServices;
using MessagePack;
using Sekai.Constants;

namespace Sekai
{
	[MessagePackObject(false)]
	public class ResourceBoxDetail : IMessagePackSerializationCallbackReceiver
	{
		[Key("resourceBoxPurpose")]
		public string resourceBoxPurpose;

		[Key("resourceBoxId")]
		public int resourceBoxId;

		[Key("seq")]
		public int seq;

		[Key("resourceType")]
		public string resourceType;

		[Key("resourceId")]
		public int resourceId;

		[Key("resourceLevel")]
		public int resourceLevel;

		[Key("resourceQuantity")]
		public int resourceQuantity;

		[IgnoreMember]
		public int Costume3dId
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
		public int Costume3dGroupId
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
		public CostumePartType PartType
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
		public FigureType Figure
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
		public int ColorId
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
		public ResourceType ResourceType
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

		public void CostumeSetup()
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

		public ResourceBoxDetail()
		{
		}
	}
}
