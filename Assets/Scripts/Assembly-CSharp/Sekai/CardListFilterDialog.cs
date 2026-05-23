using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Cysharp.Threading.Tasks;
using Sekai.UI;
using Sekai.UI.Interface;
using UnityEngine;
using UnityEngine.UI;
using YieldAwaitable = Cysharp.Threading.Tasks.YieldAwaitable;

namespace Sekai
{
	public class CardListFilterDialog : Common2ButtonDialog, ICardListFilterSort
	{
		public class ViewData
		{
			public CardList.FilterUnit FixedUnitType
			{
				[CompilerGenerated]
				get
				{
					throw null;
				}
			}

			public bool IsFixedUnitType
			{
				get
				{
					throw null;
				}
			}

			public ViewData(CardList.FilterUnit fixedUnitType = CardList.FilterUnit.All)
			{
				throw null;
			}
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CSetup_003Ed__27 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncVoidMethodBuilder _003C_003Et__builder;

			public CardListFilterDialog _003C_003E4__this;

			public CardList.FilterUnit filterUnit;

			public CardList.FilterAttribute filterAttribute;

			public CardList.FilterEpisode filterEpisode;

			public CardList.FilterAnotherCutIn filterAnotherCutIn;

			public CardRarityFilterType[] filterRarity;

			public SkillFilterType[] filterSkill;

			public CardList.FilterCostumeType[] costumes;

			public bool isUseFilterCostume;

			public CardList.SortKey sortKey;

			public int[] unCheckedCharacterIds;

			public ViewData viewData;

			private bool _003CisEnableEventBonusButton_003E5__2;

			private YieldAwaitable.Awaiter _003C_003Eu__1;

			private void MoveNext()
			{
				throw null;
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
				throw null;
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		private CardListFilterSortSettingData data;

		[SerializeField]
		private CustomButton selectAllButton;

		[SerializeField]
		private CustomButton eventBonusButton;

		[SerializeField]
		private GameObject filterUnitGroupRoot;

		[SerializeField]
		private CustomIndexToggleGroup filterUnitGroup;

		[SerializeField]
		private CustomIndexToggleGroup filterAttributeGroup;

		[SerializeField]
		private CustomIndexToggleGroup filterEpisodeGroup;

		[SerializeField]
		private CustomIndexToggleGroup filterAnotherCutInGroup;

		[SerializeField]
		private VerticalLayoutGroup memberListGroup;

		[SerializeField]
		private CustomScrollRect scrollRectFilter;

		[SerializeField]
		private RectTransform[] buttonsRoots;

		[SerializeField]
		private UIPartsCheckBox[] rarityTypeCheckBoxies;

		[SerializeField]
		private UIPartsCheckBox[] skillFilterTypeCheckBoxes;

		[SerializeField]
		private GameObject _filterCostumeGroupRoot;

		[SerializeField]
		private UIPartsCheckBox[] _filterCostumeCheckBoxes;

		[SerializeField]
		private CharacterFilterContent characterFilterContent;

		private Dictionary<int, UIPartsCharacterIconAndCheckBox> characterCheckBoxDict;

		private Dictionary<CardList.FilterUnit, UIPartsCheckBox> unitCheckBoxDict;

		private Dictionary<SkillFilterType, UIPartsCharacterIconAndCheckBox> skillFilterTypeCheckBoxDict;

		[SerializeField]
		private UIPartsCheckBox allCheckBox;

		[SerializeField]
		private UIPartsCheckBox[] unitCheckBoxes;

		[SerializeField]
		private GameObject unReleasingBalloonObj;

		private ViewData _viewData;

		public CardListFilterSortSettingData SettingResult
		{
			get
			{
				throw null;
			}
		}

		protected override void Awake()
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CSetup_003Ed__27))]
		public void Setup(CardList.FilterUnit filterUnit, CardList.FilterAttribute filterAttribute, CardList.FilterEpisode filterEpisode, CardList.FilterAnotherCutIn filterAnotherCutIn, CardRarityFilterType[] filterRarity, SkillFilterType[] filterSkill, CardList.FilterCostumeType[] costumes, bool isUseFilterCostume, CardList.SortKey sortKey, int[] unCheckedCharacterIds, ViewData viewData)
		{
			throw null;
		}

		private void SetupCharacterList()
		{
			throw null;
		}

		private void SetRarityFilterType()
		{
			throw null;
		}

		private void SetupSkillFilterType()
		{
			throw null;
		}

		private void SetupFilterCostume()
		{
			throw null;
		}

		private void UpdateList()
		{
			throw null;
		}

		private void SetupView()
		{
			throw null;
		}

		private void InitializeComponent()
		{
			throw null;
		}

		protected override void OnClickOK()
		{
			throw null;
		}

		private void OnFilterUnitSelectedIndexChanged(int index)
		{
			throw null;
		}

		private void SetupUnits()
		{
			throw null;
		}

		private void OnFilterAttributeSelectedIndexChanged(int index)
		{
			throw null;
		}

		private void OnFilterEpisodeSelectedIndexChanged(int index)
		{
			throw null;
		}

		private void OnFilterAnotherCutInSelectedIndexChanged(int index)
		{
			throw null;
		}

		private SkillFilterType[] OnFilterSkillSelectedIndexChanged(int index)
		{
			throw null;
		}

		private void ChangeCheckSelectedAll(bool check)
		{
			throw null;
		}

		private void ChangeCheckSelectedUnit(bool check, UnitType unit)
		{
			throw null;
		}

		private void ChangeCheckSelectedCharacter(CardList.FilterUnit filterUnit)
		{
			throw null;
		}

		private CardRarityFilterType[] GetUnCheckedFilterRarityTypes()
		{
			throw null;
		}

		private SkillFilterType[] GetUnCheckedFilterSkillTypes()
		{
			throw null;
		}

		private CardList.FilterCostumeType[] GetFilterCostumeTypes()
		{
			throw null;
		}

		public int[] GetUnCheckedCharacterIds()
		{
			throw null;
		}

		private void SelectAll()
		{
			throw null;
		}

		private void SelectEventBonus()
		{
			throw null;
		}

		public CardListFilterDialog()
		{
			throw null;
		}
	}
}
