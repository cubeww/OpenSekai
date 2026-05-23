using System;
using System.Collections.Generic;
using System.Reflection;
using Sekai.Multiplay;
using UnityEngine;
using uPalette.Generated;

namespace Sekai
{
	public static class ColorUtility
	{
		public enum RGBA
		{
			R = 0,
			G = 1,
			B = 2,
			A = 3
		}

		public const string FONT_COLOR_CODE_ORANGE_LITE = "#ffffee";
		public const string FONT_COLOR_CODE_GRAY = "#b0bdc8";
		public const string FONT_COLOR_CODE_DARK_GRAY = "#7e7e7e";
		public const string FONT_COLOR_CODE_DISABLE = "#7f7f90";
		public const string FONT_COLOR_CODE_DISABLE_MUSIC_SELECT = "#ccccdd";
		public const string FONT_COLOR_CODE_ORANGE = "#ffbb00";
		public const string COLOR_CODE_NAVY = "#444466";
		public const string COLOR_CODE_PINK = "#ff55AA";
		public const string COLOR_CODE_GREEN = "#00CCBB";
		public const string COLOR_CODE_GRAY = "#8888AA";
		public const string COLOR_CODE_FAST = "#55AAFF";
		public const string COLOR_CODE_LATE = "#FF5588";
		public const string INGAME_COLOR_CODE_GREEN = "#11DD88";
		public const string INGAME_COLOR_CODE_PURPLE = "#CCCCEE";
		public const string INGAME_COLOR_CODE_BLACK = "#444466";
		public const string INGAME_COLOR_CODE_WHITE = "#ffffff";
		public const string INGAME_COLOR_CODE_CLEAR = "#ffffff00";
		public const string RANK_LIVE_NOT_READY_COLOR_CODE = "#8888AA";
		public const string RANK_LIVE_MUSIC_ANNOUNCE_DIFFICULTY_DEFAULT = "#91A2AF";
		public const string CHALLENGE_LIVE_GAUGE_COLOR_NORMAL = "#00CCBB";
		public const string CHALLENGE_LIVE_GAUGE_COLOR_EX = "#FFBB00";
		public const string NORMAL_CHARACTER_MISSION_GAUGE_COLOR_CODE = "#00CCBB";
		public const string EX_CHARACTER_MISSION_GAUGE_COLOR_CODE = "#FFBB00";
		public const string NORMAL_CHARACTER_MISSION_PROGRESS_TEXT_COLOR_CODE = "#00CCBB";
		public const string EX_CHARACTER_MISSION_PROGRESS_TEXT_COLOR_CODE = "#FFAE00";
		public const string VIRTUAL_LIVE_FONT_COLOR_CODE_OWN_NAME = "#ffdd55";
		public const string VIRTUAL_LIVE_FONT_COLOR_CODE_OTHER_NAME = "#ddddee";
		public const string VIRTUAL_LIVE_CHEER_GAUGE_NORMAL_COLOR = "#00ccbb";
		public const string VIRTUAL_LIVE_CHEER_GAUGE_MAX_COLOR = "#ffbb00";
		public const string GENDER_MALE_COLOR_CODE = "#4d9bf5";
		public const string GENDER_FEMALE_COLOR_CODE = "#ffa2c1";

		public static readonly string VIRTUAL_LIVE_AVATOR_FONT_COLOR_MINE;
		public static readonly string VIRTUAL_LIVE_AVATOR_FONT_COLOR_FRIEND;
		public static readonly string VIRTUAL_LIVE_AVATOR_FONT_COLOR_OTHER;
		public static readonly Color FONT_COLOR_PINK;
		public static readonly Color FONT_COLOR_PINK2;
		public static readonly Color FONT_COLOR_BLACK;
		public static readonly Color FONT_COLOR_WHITE;
		public static readonly Color FONT_COLOR_DGR_ALPHA_50;
		public static readonly Color FONT_COLOR_WHITE_ALPHA_30;
		public static readonly Color FONT_COLOR_DISABLE;
		public static readonly Color FONT_COLOR_DISABLE_MUSIC_SELECT;
		public static readonly Color FONT_COLOR_GRAY;
		public static readonly Color FONT_COLOR_DARK_GRAY;
		public static readonly Color BLACK_ALPHA_0;
		public static readonly Color BLACK_ALPHA_HALF;
		public static readonly Color BLACK_ALPHA_1;
		public static readonly Color WHITE_ALPHA_0;
		public static readonly Color WHITE_ALPHA_HALF;
		public static readonly Color WHITE_ALPHA_1;
		public static readonly Color SEKAI_BLACK_ALPHA_0;
		public static readonly Color SEKAI_BLACK_ALPHA_HALF;
		public static readonly Color SEKAI_BLACK_ALPHA_1;
		public static readonly Color COLOR_BASE_WH;
		public static readonly Color COLOR_BASE_DBL;
		public static readonly Color COLOR_BASE_DBL_ALPHA_50;
		public static readonly Color COLOR_BASE_DBL_ALPHA_30;
		public static readonly Color COLOR_BASE_LGR;
		public static readonly Color COLOR_BASE_BGR;
		public static readonly Color COLOR_BASE_GN;
		public static readonly Color COLOR_BASE_PK;
		public static readonly Color COLOR_TAP_WH_1;
		public static readonly Color COLOR_TAP_GN_1;
		public static readonly Color COLOR_TAP_GN_2;
		public static readonly Color COLOR_TAP_GN_3;
		public static readonly Color COLOR_TAP_GN_4;
		public static readonly Color COLOR_DISABLE_ALPHA_50;
		public static readonly Color COLOR_NAVY;
		public static readonly Color COLOR_PINK;
		public static readonly Color COLOR_GREEN;
		public static readonly Color COLOR_GRAY;
		public static readonly Color COLOR_FAST;
		public static readonly Color COLOR_LATE;
		public static readonly Color COLOR_GRAY_NOT_READY;
		public static readonly Color RANK_LIVE_MUSIC_ANNOUNCE_DIFFICULTY_DEFAULT_COLOR;
		public static readonly Color CHALLENGE_LIVE_GAUGE_NORMAL;
		public static readonly Color CHALLENGE_LIVE_GAUGE_EX;
		public static readonly Color VIRTUAL_LIVE_CHEER_GAUGE_NORMAL;
		public static readonly Color VIRTUAL_LIVE_CHEER_GAUGE_MAX;
		public static readonly Color NORMAL_CHARACTER_MISSION_GAUGE_COLOR;
		public static readonly Color EX_CHARACTER_MISSION_GAUGE_COLOR;
		public static readonly Color NORMAL_CHARACTER_MISSION_PROGRESS_TEXT_COLOR;
		public static readonly Color EX_CHARACTER_MISSION_PROGRESS_TEXT_COLOR;
		public static readonly Color INVITE_COLOR_MULTI_LIVE;
		public static readonly Color INVITE_COLOR_CHEERFUL_LIVE;
		public static readonly Color INVITE_COLOR_VIRTUAL_LIVE;
		public static readonly Color INVITE_COLOR_CONNECT_LIVE;
		public static readonly Color RANK_COLOR_D;
		public static readonly Color RANK_COLOR_C;
		public static readonly Color RANK_COLOR_B;
		public static readonly Color RANK_COLOR_A;
		public static readonly Color RANK_COLOR_S;
		public static readonly Color COLORFUL_PASS_VIRTUAL_LIVE_BASE;
		public static readonly Color GENDER_MALE_COLOR;
		public static readonly Color GENDER_FEMALE_COLOR;
		public static readonly Color CHARACTER_SELECT_CELL_DISABLE;
		public static readonly Color POSITIVE_VALUE_COLOR;
		public static readonly Color NEGATIVE_VALUE_COLOR;

		private static readonly Dictionary<int, Color> CHARACTER_COLORS;
		public static readonly Dictionary<UnitType, Color> UNIT_SHADOW_COLORS;

		private const int STAGE_FIRST_START = 1;
		private const int STAGE_SECOND_START = 11;
		private const int STAGE_THIRD_START = 16;
		private const int STAGE_FOURTH_START = 21;
		private const int STAGE_FIFTH_START = 101;

		static ColorUtility()
		{
			VIRTUAL_LIVE_AVATOR_FONT_COLOR_MINE = PaletteUtility.GetColorCode(ColorEntry.text_gn);
			VIRTUAL_LIVE_AVATOR_FONT_COLOR_FRIEND = PaletteUtility.GetColorCode(ColorEntry.base_pk);
			VIRTUAL_LIVE_AVATOR_FONT_COLOR_OTHER = PaletteUtility.GetColorCode(ColorEntry.text_wh);
			FONT_COLOR_PINK = PaletteUtility.GetColor(ColorEntry.text_pk);
			FONT_COLOR_PINK2 = PaletteUtility.GetColor(ColorEntry.text_rd);
			FONT_COLOR_BLACK = PaletteUtility.GetColor(ColorEntry.text_dbl);
			FONT_COLOR_WHITE = PaletteUtility.GetColor(ColorEntry.text_wh);
			FONT_COLOR_DGR_ALPHA_50 = PaletteUtility.GetColor(ColorEntry.text_dgr_a50);
			FONT_COLOR_WHITE_ALPHA_30 = PaletteUtility.GetColor(ColorEntry.text_wh_a30);
			FONT_COLOR_DISABLE = Create(FONT_COLOR_CODE_DISABLE);
			FONT_COLOR_DISABLE_MUSIC_SELECT = Create(FONT_COLOR_CODE_DISABLE_MUSIC_SELECT);
			FONT_COLOR_GRAY = Create(FONT_COLOR_CODE_GRAY);
			FONT_COLOR_DARK_GRAY = Create(FONT_COLOR_CODE_DARK_GRAY);
			BLACK_ALPHA_0 = new Color(0f, 0f, 0f, 0f);
			BLACK_ALPHA_HALF = new Color(0f, 0f, 0f, 0.5f);
			BLACK_ALPHA_1 = new Color(0f, 0f, 0f, 1f);
			WHITE_ALPHA_0 = new Color(1f, 1f, 1f, 0f);
			WHITE_ALPHA_HALF = new Color(1f, 1f, 1f, 0.5f);
			WHITE_ALPHA_1 = new Color(1f, 1f, 1f, 1f);
			SEKAI_BLACK_ALPHA_0 = Create(INGAME_COLOR_CODE_BLACK, 0f);
			SEKAI_BLACK_ALPHA_HALF = Create(INGAME_COLOR_CODE_BLACK, 0.5f);
			SEKAI_BLACK_ALPHA_1 = Create(INGAME_COLOR_CODE_BLACK);
			COLOR_BASE_WH = PaletteUtility.GetColor(ColorEntry.base_wh);
			COLOR_BASE_DBL = PaletteUtility.GetColor(ColorEntry.base_dbl);
			COLOR_BASE_DBL_ALPHA_50 = PaletteUtility.GetColor(ColorEntry.base_dbl_a50);
			COLOR_BASE_DBL_ALPHA_30 = PaletteUtility.GetColor(ColorEntry.base_dbl_a30);
			COLOR_BASE_LGR = PaletteUtility.GetColor(ColorEntry.base_lgr);
			COLOR_BASE_BGR = PaletteUtility.GetColor(ColorEntry.base_bgr);
			COLOR_BASE_GN = PaletteUtility.GetColor(ColorEntry.base_gn);
			COLOR_BASE_PK = PaletteUtility.GetColor(ColorEntry.base_pk);
			COLOR_TAP_WH_1 = PaletteUtility.GetColor(ColorEntry.tap_wh_1);
			COLOR_TAP_GN_1 = PaletteUtility.GetColor(ColorEntry.tap_gn_1);
			COLOR_TAP_GN_2 = PaletteUtility.GetColor(ColorEntry.tap_gn_2);
			COLOR_TAP_GN_3 = PaletteUtility.GetColor(ColorEntry.tap_gn_3);
			COLOR_TAP_GN_4 = PaletteUtility.GetColor(ColorEntry.tap_gn_4);
			COLOR_DISABLE_ALPHA_50 = PaletteUtility.GetColor(ColorEntry.disable_a50);
			COLOR_NAVY = Create(COLOR_CODE_NAVY);
			COLOR_PINK = Create(COLOR_CODE_PINK);
			COLOR_GREEN = Create(COLOR_CODE_GREEN);
			COLOR_GRAY = Create(COLOR_CODE_GRAY);
			COLOR_FAST = Create(COLOR_CODE_FAST);
			COLOR_LATE = Create(COLOR_CODE_LATE);
			COLOR_GRAY_NOT_READY = Create(RANK_LIVE_NOT_READY_COLOR_CODE);
			RANK_LIVE_MUSIC_ANNOUNCE_DIFFICULTY_DEFAULT_COLOR = Create(RANK_LIVE_MUSIC_ANNOUNCE_DIFFICULTY_DEFAULT);
			CHALLENGE_LIVE_GAUGE_NORMAL = Create(CHALLENGE_LIVE_GAUGE_COLOR_NORMAL);
			CHALLENGE_LIVE_GAUGE_EX = Create(CHALLENGE_LIVE_GAUGE_COLOR_EX);
			VIRTUAL_LIVE_CHEER_GAUGE_NORMAL = Create(VIRTUAL_LIVE_CHEER_GAUGE_NORMAL_COLOR);
			VIRTUAL_LIVE_CHEER_GAUGE_MAX = Create(VIRTUAL_LIVE_CHEER_GAUGE_MAX_COLOR);
			NORMAL_CHARACTER_MISSION_GAUGE_COLOR = Create(NORMAL_CHARACTER_MISSION_GAUGE_COLOR_CODE);
			EX_CHARACTER_MISSION_GAUGE_COLOR = Create(EX_CHARACTER_MISSION_GAUGE_COLOR_CODE);
			NORMAL_CHARACTER_MISSION_PROGRESS_TEXT_COLOR = Create(NORMAL_CHARACTER_MISSION_PROGRESS_TEXT_COLOR_CODE);
			EX_CHARACTER_MISSION_PROGRESS_TEXT_COLOR = Create(EX_CHARACTER_MISSION_PROGRESS_TEXT_COLOR_CODE);
			INVITE_COLOR_MULTI_LIVE = Create("#FF77CC");
			INVITE_COLOR_CHEERFUL_LIVE = Create("#FFA48F");
			INVITE_COLOR_VIRTUAL_LIVE = Create("#FBDD8A");
			INVITE_COLOR_CONNECT_LIVE = Create("#FBDD8A");
			RANK_COLOR_D = Create("#73ffcc");
			RANK_COLOR_C = Create("#55fdf7");
			RANK_COLOR_B = Create("#78adff");
			RANK_COLOR_A = Create("#e18aff");
			RANK_COLOR_S = Create("#FF8ec6");
			COLORFUL_PASS_VIRTUAL_LIVE_BASE = Create("#FFFFCC");
			GENDER_MALE_COLOR = Create(GENDER_MALE_COLOR_CODE);
			GENDER_FEMALE_COLOR = Create(GENDER_FEMALE_COLOR_CODE);
			CHARACTER_SELECT_CELL_DISABLE = Create("#8C8CA2");
			POSITIVE_VALUE_COLOR = PaletteUtility.GetColor(ColorEntry.text_pk);
			NEGATIVE_VALUE_COLOR = PaletteUtility.GetColor(ColorEntry.text_lbl);

			CHARACTER_COLORS = new Dictionary<int, Color>
			{
				{ 1, Create("#33aaee") },
				{ 2, Create("#ffdd44") },
				{ 3, Create("#ee6666") },
				{ 4, Create("#bbdd22") },
				{ 5, Create("#ffccaa") },
				{ 6, Create("#99ccff") },
				{ 7, Create("#ffaacc") },
				{ 8, Create("#99eedd") },
				{ 9, Create("#ff6699") },
				{ 10, Create("#00bbdd") },
				{ 11, Create("#ff7722") },
				{ 12, Create("#0077dd") },
				{ 13, Create("#ffbb00") },
				{ 14, Create("#ff66bb") },
				{ 15, Create("#33dd99") },
				{ 16, Create("#bb88ee") },
				{ 17, Create("#bb6688") },
				{ 18, Create("#8888cc") },
				{ 19, Create("#ccaa88") },
				{ 20, Create("#ddaacc") },
				{ 21, Create("#33ccbb") },
				{ 22, Create("#ffcc11") },
				{ 23, Create("#ffee11") },
				{ 24, Create("#ffbbcc") },
				{ 25, Create("#dd4444") },
				{ 26, Create("#3366cc") }
			};
			UNIT_SHADOW_COLORS = new Dictionary<UnitType, Color>
			{
				{ UnitType.light_sound, Create("#4455DD") },
				{ UnitType.idol, Create("#88DD44") },
				{ UnitType.street, Create("#EE1166") },
				{ UnitType.theme_park, Create("#FF9900") },
				{ UnitType.school_refusal, Create("#884499") },
				{ UnitType.piapro, Create("#AAAAAA") }
			};
		}

		public static Color Create(int r, int g, int b, int a = 255)
		{
			return new Color(r / 255f, g / 255f, b / 255f, a / 255f);
		}

		public static Color Create(string code)
		{
			if (UnityEngine.ColorUtility.TryParseHtmlString(code, out Color color))
			{
				return color;
			}
			Debug.LogError("Unknown color code... " + code);
			return Color.clear;
		}

		public static Color Create(string code, float alpha)
		{
			Color color = Create(code);
			color.a = alpha;
			return color;
		}

		public static string ToColorCode(int r, int g, int b, int a = 255)
		{
			return ToColorCode(Create(r, g, b, a));
		}

		public static string ToColorCode(Color color)
		{
			return "#" + UnityEngine.ColorUtility.ToHtmlStringRGBA(color);
		}

		public static Color GetDifficultyColor(MusicDifficulty difficulty)
		{
			return GetDifficultyColor(difficulty.ToString());
		}

		public static Color GetDifficultyColor(string difficulty)
		{
			switch ((difficulty ?? string.Empty).ToUpperInvariant())
			{
			case "EASY":
				return PaletteUtility.GetColor(ColorEntry.difficulty_easy);
			case "NORMAL":
				return PaletteUtility.GetColor(ColorEntry.difficulty_normal);
			case "HARD":
				return PaletteUtility.GetColor(ColorEntry.difficulty_hard);
			case "EXPERT":
				return PaletteUtility.GetColor(ColorEntry.difficulty_expert);
			case "MASTER":
				return PaletteUtility.GetColor(ColorEntry.difficulty_master);
			case "APPEND":
				return PaletteUtility.GetColor(ColorEntry.difficulty_append);
			case "NONE":
				return PaletteUtility.GetColor(ColorEntry.difficulty_none);
			default:
				return PaletteUtility.GetColor(ColorEntry.base_wh);
			}
		}

		public static Color GetMultiLivePanelPlayerColor(MultiLivePartyMember member)
		{
			return GetMultiLivePlayerColor(member.UserId == string.Empty ? -1 : member.Index, false);
		}

		public static Color GetMultiLivePanelPlayerColor(int index)
		{
			return GetMultiLivePlayerColor(index, false);
		}

		public static Color GetMultiLiveThumbnailPlayerColor(MultiLivePartyMember member)
		{
			return GetMultiLivePlayerColor(member.UserId == string.Empty ? -1 : member.Index, true);
		}

		public static Color GetRankLiveThumbnailPlayerColor(long userId)
		{
			return GetRankLiveThumbnailPlayerColor(TryGetOwnUserId(out long ownUserId) && ownUserId == userId);
		}

		public static Color GetRankLiveThumbnailPlayerColor(bool own)
		{
			return PaletteUtility.GetColor(own ? ColorEntry.live_you : ColorEntry.live_rival);
		}

		public static Color GetMultiLiveThumbnailPlayerColor(int playerIndex)
		{
			return playerIndex < 0 ? GetMultiLivePlayerColor(-1, false) : GetMultiLivePlayerColor(playerIndex, true);
		}

		public static Color GetMultiLivePlayerColor(int index, bool thumnailFlag = true)
		{
			if ((uint)index < 5)
			{
				return PaletteUtility.GetColor((ColorEntry)((int)ColorEntry.player_1 + index));
			}
			return PaletteUtility.GetColor(thumnailFlag ? ColorEntry.player_none : ColorEntry.text_dbl);
		}

		public static Color[] GetPlayerColorList()
		{
			return new[]
			{
				PaletteUtility.GetColor(ColorEntry.player_1),
				PaletteUtility.GetColor(ColorEntry.player_2),
				PaletteUtility.GetColor(ColorEntry.player_3),
				PaletteUtility.GetColor(ColorEntry.player_4),
				PaletteUtility.GetColor(ColorEntry.player_5)
			};
		}

		public static Color GetScoreRankColor(ScoreRank rank)
		{
			return GetScoreRankColor((int)rank);
		}

		public static Color GetScoreRankColor(int index)
		{
			switch (index)
			{
			case 0:
				return RANK_COLOR_S;
			case 1:
				return RANK_COLOR_A;
			case 2:
				return RANK_COLOR_B;
			case 3:
				return RANK_COLOR_C;
			case 4:
				return RANK_COLOR_D;
			default:
				return Color.white;
			}
		}

		public static Color GetCharacterColor(int characterId)
		{
			return CHARACTER_COLORS.TryGetValue(characterId, out Color color) ? color : WHITE_ALPHA_0;
		}

		public static Color GetExchangeTextColor(int haveCount, int totalCost)
		{
			return GetExchangeTextColor(haveCount >= totalCost);
		}

		public static Color GetExchangeTextColor(bool canExchange)
		{
			return canExchange ? COLOR_NAVY : FONT_COLOR_PINK2;
		}

		public static Color SetColor(Color color, RGBA rgba, float value)
		{
			switch (rgba)
			{
			case RGBA.R:
				color.r = value;
				break;
			case RGBA.G:
				color.g = value;
				break;
			case RGBA.B:
				color.b = value;
				break;
			case RGBA.A:
				color.a = value;
				break;
			}
			return color;
		}

		public static Color GetChallengeStageColor(int stage, bool isEx = false)
		{
			if (isEx)
			{
				return PaletteUtility.GetColor(ColorEntry.challengeStage_6);
			}
			if ((uint)(stage - STAGE_FIRST_START) < 10u)
			{
				return PaletteUtility.GetColor(ColorEntry.challengeStage_1);
			}
			if ((uint)(stage - STAGE_SECOND_START) < 5u)
			{
				return PaletteUtility.GetColor(ColorEntry.challengeStage_2);
			}
			if ((uint)(stage - STAGE_THIRD_START) < 5u)
			{
				return PaletteUtility.GetColor(ColorEntry.challengeStage_3);
			}
			if ((uint)(stage - STAGE_FOURTH_START) < 80u)
			{
				return PaletteUtility.GetColor(ColorEntry.challengeStage_4);
			}
			return PaletteUtility.GetColor(stage >= STAGE_FIFTH_START ? ColorEntry.challengeStage_5 : ColorEntry.challengeStage_1);
		}

		public static Color ColorCodeToColor(string colorCode)
		{
			return UnityEngine.ColorUtility.TryParseHtmlString(colorCode, out Color color) ? color : FONT_COLOR_BLACK;
		}

		public static Color GetChangedValueTextColor(int current, int changed)
		{
			return GetChangedValueTextColor(current, changed, FONT_COLOR_WHITE);
		}

		public static Color GetChangedValueTextColor(int current, int changed, Color defaultColor)
		{
			if (current < changed)
			{
				return POSITIVE_VALUE_COLOR;
			}
			if (changed < current)
			{
				return NEGATIVE_VALUE_COLOR;
			}
			return defaultColor;
		}

		public static Color GetChangedValueTextColor(float current, float changed, Color defaultColor)
		{
			if (current < changed)
			{
				return POSITIVE_VALUE_COLOR;
			}
			if (changed < current)
			{
				return NEGATIVE_VALUE_COLOR;
			}
			return defaultColor;
		}

		private static bool TryGetOwnUserId(out long userId)
		{
			userId = 0L;
			Type managerType = Type.GetType("Sekai.UserDataManager");
			object manager = managerType?.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static)?.GetValue(null);
			object userInformation = GetMemberValue(manager, "UserInformation");
			object value = GetMemberValue(userInformation, "UserId");
			if (value is long longValue)
			{
				userId = longValue;
				return true;
			}
			if (value is int intValue)
			{
				userId = intValue;
				return true;
			}
			return value is string stringValue && long.TryParse(stringValue, out userId);
		}

		private static object GetMemberValue(object source, string name)
		{
			if (source == null)
			{
				return null;
			}
			Type type = source.GetType();
			return type.GetProperty(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(source)
				?? type.GetField(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(source);
		}
	}
}
