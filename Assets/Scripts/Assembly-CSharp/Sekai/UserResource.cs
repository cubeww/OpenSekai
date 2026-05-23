using System;
using MessagePack;
using Sekai.ApiData;
using Sekai.Constants;

namespace Sekai
{
	[MessagePackObject(false)]
	public class UserResource : IMessagePackSerializationCallbackReceiver
	{
		public const string RESOURCE_TYPE_MATERIAL = "material";

		public const string RESOURCE_TYPE_MYSEKAI_TOOL = "mysekai_tool";

		public const string RESOURCE_TYPE_MYSEKAI_BLUEPRINT = "mysekai_blueprint";

		public const string RESOURCE_TYPE_MYSEKAI_MATERIAL = "mysekai_material";

		public const string RESOURCE_TYPE_MYSEKAI_ITEM = "mysekai_item";

		public const string RESOURCE_TYPE_MYSEKAI_FIXTURE = "mysekai_fixture";

		public const string RESOURCE_TYPE_MYSEKAI_RANK_EXP = "mysekai_rank_exp";

		public const string RESOURCE_TYPE_MYSEKAI_MATERIAL_POSSESSION = "mysekai_material_possession";

		public const string RESOURCE_TYPE_MYSEKAI_FIXTURE_POSSESSION = "mysekai_fixture_possession";

		public const string RESOURCE_TYPE_MYSEKAI_GATE_SKIN = "mysekai_gate_skin";

		public const string RESOURCE_TYPE_MYSEKAI_MUSIC_RECORD = "mysekai_music_record";

		public const string RESOURCE_TYPE_MYSEKAI_PHOTO = "mysekai_photo";

		public const string RESOURCE_TYPE_MYSEKAI_WORLD_PATH = "mysekai_colorful_pass";

		public const string RESOURCE_TYPE_AREAITEM = "area_item";

		public const string RESOURCE_TYPE_MUSIC = "music";

		public const string RESOURCE_TYPE_MUSIC_VARIANT = "music_variant";

		public const string RESOURCE_TYPE_MUSIC_DIFFICULTY = "music_difficulty";

		public const string RESOURCE_TYPE_MUSIC_VOCAL = "music_vocalitem";

		public const string RESOURCE_TYPE_MUSIC_VOCAL_SUPPLY = "music_vocal";

		public const string RESOURCE_TYPE_JEWEL = "jewel";

		public const string RESOURCE_TYPE_PAID_JEWEL = "paid_jewel";

		public const string RESOURCE_TYPE_CARD = "card";

		public const string RESOURCE_TYPE_COSTUME = "costume_3d";

		public const string RESOURCE_TYPE_PRACTICE_TICKET = "practice_ticket";

		public const string RESOURCE_TYPE_SKILL_PRACTICE_TICKET = "skill_practice_ticket";

		public const string RESOURCE_TYPE_MASTER_LESSON = "master_lesson";

		public const string RESOURCE_TYPE_STAMP = "stamp";

		public const string RESOURCE_TYPE_BOOST_ITEM = "boost_item";

		public const string RESOURCE_TYPE_GACHA_CEIL_ITEM = "gacha_ceil_item";

		public const string RESOURCE_TYPE_GACHA_TICKET = "gacha_ticket";

		public const string RESOURCE_TYPE_HONOR = "honor";

		public const string RESOURCE_TYPE_AVATAR_SKIN_COLOR = "avatar_skin_color";

		public const string RESOURCE_TYPE_AVATAR_COSTUME = "avatar_costume";

		public const string RESOURCE_TYPE_AVATAR_ACCESSORY = "avatar_accessory";

		public const string RESOURCE_TYPE_AVATAR_MOTION = "avatar_motion";

		public const string RESOURCE_TYPE_COLORFUL_PASS = "colorful_pass";

		public const string RESOURCE_TYPE_COLORFUL_PASS_V2 = "colorful_pass_v2";

		public const string RESOURCE_TYPE_AVATAR_COORDINATE = "avatar_coordinate";

		public const string RESOURCE_TYPE_PENLIGHT = "penlight";

		public const string RESOURCE_TYPE_LIVE_POINT = "live_point";

		public const string RESOURCE_TYPE_EVENT_ITEM = "event_item";

		public const string RESOURCE_TYPE_BOOST_PRESENT = "boost_present";

		public const string RESOURCE_TYPE_PAMPHLET = "virtual_live_pamphlet";

		public const string RESOURCE_TYPE_BONDS_HONOR = "bonds_honor";

		public const string RESOURCE_TYPE_BONDS_HONOR_WORD = "bonds_honor_word";

		public const string RESOURCE_TYPE_COLLECTION = "custom_profile_collection_resource";

		public const string RESOURCE_TYPE_CUT_IN_VOICE = "cut_in_voice";

		public const string RESOURCE_TYPE_NONE = "none";

		public const string RESOURCE_TYPE_SERIAL_CODE_ITEM = "serial_code_item";

		public const string RESOURCE_TYPE_PLAYER_FRAME = "player_frame";

		public const string RESOURCE_TYPE_VIRTUAL_TRANSITION_ITEM = "virtual_live_transition_item";

		public const string RESOURCE_TYPE_TOTAL_POWER = "total_power";

		public const string RESOURCE_TYPE_3D_MV_ANOTHER_CUT_IN = "another_cut_in";

		public const string RESOURCE_TYPE_CHARACTER_RANK_EXP = "character_rank_exp";

		public const string RESOURCE_TYPE_CHALLENGE_SLOT = "challenge_slot";

		public const int HEART_PIECE_MATERIAL_ID = 15;

		[Key("resourceId")]
		public int resourceId;

		[Key("resourceType")]
		public string resourceType;

		[Key("resourceLevel")]
		public int resourceLevel;

		[Key("quantity")]
		public int quantity;

		[IgnoreMember]
		public ResourceType ResourceType
		{
			get
			{
				throw null;
			}
		}

		public UserResource()
		{
		}

		[Obsolete("旧形式. ResourceType Enumに置き換え終わったらこのoverloadを消す.")]
		public UserResource(int id, string type, int level, int amount)
		{
			throw null;
		}

		public UserResource(int id, ResourceType resourceType, int level, int amount)
		{
			throw null;
		}

		public UserResource(ResourceBoxDetail boxDetail)
		{
			throw null;
		}

		public UserResource(MasterMysekaiMaterial master, int quantity)
		{
			throw null;
		}

		public UserResource(MasterMysekaiItem master, int quantity)
		{
			throw null;
		}

		public UserResource(ResourceBoxDetail boxDetail, int exchangeCount)
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
	}
}
