using UnityEngine;
using uPalette.Generated;

namespace Sekai
{
	public static class PaletteUtility
	{
		public static Color GetColor(ColorEntry entry)
		{
			return ColorCodeToColor(GetColorCode(entry));
		}

		public static string GetColorCode(ColorEntry entry)
		{
			switch (entry)
			{
			case ColorEntry.base_dbl:
				return "#444466";
			case ColorEntry.base_dbl_a50:
				return "#44446680";
			case ColorEntry.base_wh:
				return "#FFFFFF";
			case ColorEntry.base_gn:
				return "#77EEDD";
			case ColorEntry.base_pk:
				return "#FF77AA";
			case ColorEntry.base_rd:
				return "#FF5588";
			case ColorEntry.base_lgr:
				return "#EBEBF2";
			case ColorEntry.base_bgr:
				return "#DFDFEA";
			case ColorEntry.base_gr:
				return "#BCBCD0";
			case ColorEntry.base_dgr:
				return "#A7A7BC";
			case ColorEntry.base_lyw:
				return "#FFFFEE";
			case ColorEntry.base_empty1:
				return "#B0BDC8";
			case ColorEntry.base_empty2:
				return "#91A2AF";
			case ColorEntry.base_dsb_a50:
				return "#00002280";
			case ColorEntry.text_dbl:
				return "#555577";
			case ColorEntry.text_gn:
				return "#00CCBB";
			case ColorEntry.text_pk:
				return "#FF5599";
			case ColorEntry.text_rd:
				return "#FF3366";
			case ColorEntry.text_wh:
				return "#FFFFFF";
			case ColorEntry.text_lbl:
				return "#55A9FF";
			case ColorEntry.gauge_gn:
				return "#22DDCC";
			case ColorEntry.player_none:
				return "#91A2AF";
			case ColorEntry.player_1:
				return "#FF5577";
			case ColorEntry.player_2:
				return "#FFBB33";
			case ColorEntry.player_3:
				return "#11CCCC";
			case ColorEntry.player_4:
				return "#11AAFF";
			case ColorEntry.player_5:
				return "#7766DD";
			case ColorEntry.difficulty_easy:
				return "#11DD77";
			case ColorEntry.difficulty_normal:
				return "#33CCFF";
			case ColorEntry.difficulty_hard:
				return "#FFCC00";
			case ColorEntry.difficulty_expert:
				return "#FF4477";
			case ColorEntry.difficulty_master:
				return "#CC33FF";
			case ColorEntry.live_rankmatch:
				return "#DD99FF";
			case ColorEntry.live_rival:
				return "#33BBFF";
			case ColorEntry.live_you:
				return "#FF9955";
			case ColorEntry.rank_chr:
				return "#33DDFF";
			case ColorEntry.rank_kizuna:
				return "#2EE5E5";
			case ColorEntry.difficulty_none:
				return "#8888AA";
			case ColorEntry.challengeStage_1:
				return "#11CCCC";
			case ColorEntry.challengeStage_2:
				return "#11AAFF";
			case ColorEntry.challengeStage_3:
				return "#FFBB33";
			case ColorEntry.challengeStage_4:
				return "#FF5577";
			case ColorEntry.challengeStage_5:
				return "#7766DD";
			case ColorEntry.tap_wh_1:
				return "#FFFFFF4C";
			case ColorEntry.tap_gn_1:
				return "#A1F4EB";
			case ColorEntry.tap_gn_2:
				return "#E4FCF8";
			case ColorEntry.tap_gn_3:
				return "#BBFFFF80";
			case ColorEntry.tap_gn_4:
				return "#44E9D780";
			case ColorEntry.base_dbl_a30:
				return "#4444664C";
			case ColorEntry.disable_a50:
				return "#00002280";
			case ColorEntry.base_wh_a0:
				return "#FFFFFF00";
			case ColorEntry.text_dgr_a50:
				return "#A7A7BC80";
			case ColorEntry.text_wh_a30:
				return "#FFFFFF4C";
			case ColorEntry.difficulty_append:
				return "#FF7DC9";
			case ColorEntry.base_lgr_a80:
				return "#EBEBF2CC";
			case ColorEntry.normal_stamina:
				return "#66EEFF";
			case ColorEntry.decrease_stamina:
				return "#FF5588";
			case ColorEntry.challengeStage_6:
				return "#FF55BBB2";
			case ColorEntry.base_wh_a20:
				return "#FFFFFF33";
			case ColorEntry.tap_pk_1:
				return "#FFAFCD99";
			default:
				return "#FFFFFF";
			}
		}

		private static Color ColorCodeToColor(string colorCode)
		{
			return UnityEngine.ColorUtility.TryParseHtmlString(colorCode, out Color color) ? color : Color.white;
		}
	}
}
