using Beebyte.Obfuscator;
using MessagePack;

namespace Sekai
{
	[Skip]
	[MessagePackObject(false)]
	public class MasterResourceBox
	{
		public const string PURPOSE_CARD_EXCHANGE_RESOURCE = "card_exchange_resource";

		public const string PURPOSE_CHARACTER_RANK_REWARD = "character_rank_reward";

		public const string PURPOSE_EPISODE_REWARD = "episode_reward";

		public const string PURPOSE_GIFT_DETAIL = "gift_detail";

		public const string PURPOSE_MASTER_LESSON_REWARD = "master_lesson_reward";

		public const string PURPOSE_MATERIAL_EXCHANGE = "material_exchange";

		public const string PURPOSE_MULTI_SCORE_RANK_REWARD_DETAIL = "multi_score_rank_reward_detail";

		public const string PURPOSE_SCORE_RANK_REWARD_DETAIL = "score_rank_reward_detail";

		public const string PURPOSE_SHOP_ITEM = "shop_item";

		public const string PURPOSE_MISSION_REWARD = "mission_reward";

		public const string PURPOSE_EVENT_MISSION_SELECTABLE_REWARD = "event_mission_selectable_reward";

		public const string PURPOSE_GACHA_CEIL_EXCHANGE = "gacha_ceil_exchange";

		public const string PURPOSE_CONVERT_GACHA_CEIL_EXCHANGE = "convert_gacha_ceil_item";

		public const string PURPOSE_CHALLENGE_LIVE_SCORE_RANK_REWARD_DETAIL = "challenge_live_score_rank_reward_detail";

		public const string PURPOSE_CHALLENGE_LIVE_STAGE = "challenge_live_stage";

		public const string PURPOSE_CHALLENGE_LIVE_STAGE_EX = "challenge_live_stage_ex";

		public const string PURPOSE_CHALLENGE_HIGH_SCORE = "challenge_live_high_score";

		public const string CHALLENGE_LIVE_PLAY_DAY_REWARD = "challenge_live_play_day_reward";

		public const string PURPOSE_VIRTUAL_LIVE_REWARD = "virtual_live_reward";

		public const string PURPOSE_VIRTUAL_SHOP_ITEM = "virtual_shop_item";

		public const string PURPOSE_BILLING_SHOP_ITEM = "billing_shop_item";

		public const string PURPOSE_BILLING_SHOP_ITEM_BONUS = "billing_shop_item_bonus";

		public const string PURPOSE_MUSIC_ACHIEVEMENT = "music_achievement";

		public const string PURPOSE_LOGIN_BONUS = "login_bonus";

		public const string VIEW_TYPE_EXPAND = "expand";

		public const string VIEW_TYPE_LIST = "list";

		public const string VIEW_TYPE_COSTUME_3D = "costume_3d";

		public const string VIEW_TYPE_EVENT_EXCHANGE = "event_exchange";

		public const string VIEW_TYPE_EVENT_RANKING_REWARD = "event_ranking_reward";

		public const string WORLD_BLOOM_CHAPTER_RANKING_REWARD = "world_bloom_chapter_ranking_reward";

		public const string VIEW_TYPE_COLORFUL_PASS_V2 = "colorful_pass_v2";

		public const string PURPOSE_CHEERFUL_CARNIVAL_REWARD = "cheerful_carnival_reward";

		public const string PURPOSE_CHEERFUL_CARNIVAL_RESULT_REWARD = "cheerful_carnival_result_reward";

		public const string PURPOSE_GACHA_EXTRA = "gacha_extra";

		public const string PURPOSE_GACHA_BONUS_ITEM_RECEIVABLE_REWARD = "gacha_bonus_item_receivable_reward";

		public const string PURPOSE_GACHA_FREEBIE_GROUP = "gacha_freebie_group";

		public const string PURPOSE_BONDS_REWARD = "bonds_reward";

		public const string PURPOSE_RANK_MATCH_SEASON_TIER_REWARD = "rank_match_season_tier_reward";

		public const string PURPOSE_GIFT_GACHA_EXTRA = "gift_gacha_extra";

		public const string PAID_VIRTUAL_LIVE_SHOP_ITEM = "paid_virtual_live_shop_item";

		public const string CARD_EXTRA = "card_extra";

		public const string SPECIAL_TRAINING_REWARD = "special_training_reward";

		public const string SERIAL_CODE_STAMP_RALLY_REWARD = "serial_code_campaign_reward";

		public const string MYSEKAI_CONVERT_CONSUMPTION = "mysekai_convert_consumption";

		public const string MYSEKAI_CONVERT_OBTAIN = "mysekai_convert_obtain";

		public const string MYSEKAI_RECYCLE = "mysekai_recycle";

		public const string MYSEKAI_NORMAL_MISSION_REWARD = "mysekai_normal_mission_reward";

		public const string PURPOSE_STORY_MISSION = "story_mission";

		public const string MATERIAL_EXCHANGE_FREEBIE = "material_exchange_freebie";

		public const string PURPOSE_VIRTUAL_SHOP_ITEM_FIRST_PURCHASE_BONUS_BONUS = "virtual_shop_item_first_purchase_bonus_bonus";

		public const string PURPOSE_VIRTUAL_SHOP_ITEM_FIRST_PURCHASE_BONUS = "virtual_shop_item_first_purchase_bonus";

		public const string VIRTUAL_SHOP_BONUS_ITEM = "virtual_shop_bonus_item";

		public const string BIRTHDAY_PARTY_DELIVERY_TOTAL_REWARD = "birthday_party_delivery_total_reward";

		public const string FIXTURE_DISASSEMBLE_RESOURCE = "mysekai_fixture_disassemble_resource";

		[Key("resourceBoxPurpose")]
		public string resourceBoxPurpose;

		[Key("id")]
		public int id;

		[Key("resourceBoxType")]
		public string resourceBoxType;

		[Key("name")]
		public string name;

		[Key("description")]
		public string description;

		[Key("assetbundleName")]
		public string assetbundleName;

		[Key("details")]
		public ResourceBoxDetail[] details;

		[IgnoreMember]
		public bool HasMultipleDetails
		{
			get
			{
				throw null;
			}
		}

		public bool IsExistDetails()
		{
			throw null;
		}

		public bool IsCostumeDetails()
		{
			throw null;
		}

		public MasterResourceBox()
		{
			throw null;
		}
	}
}
