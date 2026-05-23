using System;
using System.Collections.Generic;
using Sekai.UI;
using Sekai.UI.Interface;
using UnityEngine;

namespace Sekai
{
	[RequireComponent(typeof(CustomDropdown))]
	public class CardListSortDropdown : MonoBehaviour, ICardListFilterSort
	{
		[SerializeField]
		private CustomDropdown dropdown;

		private CardListFilterSortSettingData data;

		private CardList.SortKey[] _validSortKeyArray;

		public CardListFilterSortSettingData SettingResult
		{
			get
			{
				throw null;
			}
		}

		private static Dictionary<CardList.SortKey, string> SortWordDictionary
		{
			get
			{
				throw null;
			}
		}

		public void Setup(CardList.FilterUnit filterUnit, CardList.FilterAttribute filterAttribute, CardList.FilterEpisode filterEpisode, CardRarityFilterType[] filterRarity, SkillFilterType[] filterSkill, CardList.SortKey sortKey, int[] unCheckedCharacterIds, Action<CardList.SortKey> onCallback, bool shouldShowEventSupportBonus = false, CardList.FilterCostumeType[] costumes = null)
		{
			throw null;
		}

		public CardListSortDropdown()
		{
			throw null;
		}
	}
}
