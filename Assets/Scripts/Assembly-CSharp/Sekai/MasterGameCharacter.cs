using System.Runtime.CompilerServices;
using MessagePack;

namespace Sekai
{
	[MessagePackObject(false)]
	public class MasterGameCharacter : IMessagePackSerializationCallbackReceiver
	{
		public const string GENDER_TYPE_MALE = "male";

		public const string GENDER_TYPE_FEMALE = "female";

		public const string GENDER_TYPE_SECRET = "secret";

		public const string FIGURE_TYPE_BOYS = "boys";

		public const string FIGURE_TYPE_MENS = "mens";

		public const string FIGURE_TYPE_GIRLS = "girls";

		public const string FIGURE_TYPE_LADIES = "ladies";

		public const string CHARACTER_UNIT_THEME_PARK = "theme_park";

		public const string CHARACTER_UNIT_IDOL = "idol";

		public const string CHARACTER_UNIT_STREET = "street";

		public const string CHARACTER_UNIT_LIGHT_SOUND = "light_sound";

		public const string CHARACTER_UNIT_SCHOOL_REFUSAL = "school_refusal";

		public const string CHARACTER_UNIT_PIAPRO = "piapro";

		public const string CHARACTER_UNIT_NONE = "none";

		[Key("id")]
		public int id;

		[Key("resourceId")]
		public int resourceId;

		[Key("firstName")]
		public string firstName;

		[Key("givenName")]
		public string givenName;

		[Key("firstNameEnglish")]
		public string firstNameEnglish;

		[Key("givenNameEnglish")]
		public string givenNameEnglish;

		[Key("ruby")]
		public string ruby;

		[Key("gender")]
		public string gender;

		[Key("height")]
		public float height;

		[Key("live2dHeightAdjustment")]
		public float live2dHeightAdjustment;

		[Key("figure")]
		public string figure;

		[Key("breastSize")]
		public string breastSize;

		[Key("unit")]
		public string unit;

		[Key("supportUnitType")]
		public string supportUnitType;

		[IgnoreMember]
		public string FullName
		{
			get
			{
				throw null;
			}
		}

		[IgnoreMember]
		public string FullNameEnglish
		{
			get
			{
				throw null;
			}
		}

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

		[IgnoreMember]
		public SupportUnitType SupportUnitType
		{
			get
			{
				throw null;
			}
		}

		[IgnoreMember]
		public bool IsFigureMan
		{
			get
			{
				throw null;
			}
		}

		[IgnoreMember]
		public GenderType Gender
		{
			get
			{
				throw null;
			}
		}

		[IgnoreMember]
		public CostumeType CostumeType
		{
			get
			{
				throw null;
			}
		}

		[IgnoreMember]
		public FigureType Figure
		{
			get
			{
				throw null;
			}
		}

		public void ConvertUnitType()
		{
			throw null;
		}

		void IMessagePackSerializationCallbackReceiver.OnBeforeSerialize()
		{
			throw null;
		}

		void IMessagePackSerializationCallbackReceiver.OnAfterDeserialize()
		{
			throw null;
		}

		public MasterGameCharacter()
		{
		}
	}
}
