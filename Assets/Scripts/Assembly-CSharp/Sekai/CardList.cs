using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Sekai.UI;
using Sekai.UI.Interface;
using UnityEngine;

namespace Sekai
{
	public class CardList : MonoBehaviour
	{
		public enum FilterUnit
		{
			All = 0,
			Unit1 = 1,
			Unit2 = 2,
			Unit3 = 3,
			Unit4 = 4,
			Unit5 = 5,
			Unit6 = 6
		}

		public enum FilterAttribute
		{
			All = 0,
			Attribute1 = 1,
			Attribute2 = 2,
			Attribute3 = 3,
			Attribute4 = 4,
			Attribute5 = 5
		}

		public enum FilterEpisode
		{
			All = 0,
			Unread = 1,
			FirstUnread = 2,
			LastUnread = 3,
			AlreadyRead = 4,
			SkipOrFast = 5
		}

		public enum FilterAnotherCutIn
		{
			All = 0,
			AnotherCutIn = 1
		}

		public enum FilterType
		{
			All = 0,
			ColorfulLimited = 1,
			BloomLimited = 2,
			Normal = 3,
			Fixed = 4
		}

		public enum FilterCostumeType
		{
			CostumeAndAccessory = 0,
			HairStyle = 1,
			WithoutCostume = 2
		}

		public enum SortKey
		{
			Get = 0,
			TotalPower = 1,
			Rarity = 2,
			Level = 3,
			SkillLevel = 4,
			MasterRank = 5,
			EventBonus = 6,
			SkillEffect = 7,
			EventSupportBonus = 8
		}

		public class CellData
		{
			public enum EpisodeState
			{
				None = 0,
				Unread = 1,
				FirstUnread = 2,
				LastUnread = 3,
				AlreadyRead = 4,
				SkipOrFast = 5
			}

			public CardThumbnailViewDataBase ViewData
			{
				[CompilerGenerated]
				get
				{
					throw null;
				}
				[CompilerGenerated]
				set
				{
					throw null;
				}
			}

			public MasterGameCharacter MasterCharacter
			{
				get
				{
					throw null;
				}
			}

			public byte Unit
			{
				get
				{
					throw null;
				}
			}

			public byte SubUnit
			{
				get
				{
					throw null;
				}
			}

			public EpisodeState EpisodeReadState
			{
				[CompilerGenerated]
				get
				{
					throw null;
				}
				[CompilerGenerated]
				set
				{
					throw null;
				}
			}

			public bool IsSkipped
			{
				[CompilerGenerated]
				get
				{
					throw null;
				}
				[CompilerGenerated]
				set
				{
					throw null;
				}
			}

			public CardRarityFilterType RarityFilterType
			{
				[CompilerGenerated]
				get
				{
					throw null;
				}
				[CompilerGenerated]
				set
				{
					throw null;
				}
			}

			public SkillFilterType[] SkillFilterTypes
			{
				[CompilerGenerated]
				get
				{
					throw null;
				}
				[CompilerGenerated]
				set
				{
					throw null;
				}
			}

			public long CreatedAtDate
			{
				[CompilerGenerated]
				get
				{
					throw null;
				}
				[CompilerGenerated]
				set
				{
					throw null;
				}
			}

			public bool IsCardLvMax
			{
				get
				{
					throw null;
				}
			}

			public bool IsSkillLvMax
			{
				get
				{
					throw null;
				}
			}

			public bool IsMasterLvMax
			{
				get
				{
					throw null;
				}
			}

			public bool HasAnother3dmvCutin
			{
				get
				{
					throw null;
				}
			}

			public CellData(CardThumbnailViewDataBase viewData, EpisodeState episodeReadState, CardRarityFilterType rarityFilterType, SkillFilterType[] skillFilterTypes, long craetedAtDate, bool isSkipped)
			{
				throw null;
			}

			public CellData(CellData data)
			{
				throw null;
			}

			public CellData(UserCard userCard)
			{
				throw null;
			}
		}

		public class DisplaySettingsData
		{
			public FilterUnit FilterUnit
			{
				[CompilerGenerated]
				get
				{
					throw null;
				}
				[CompilerGenerated]
				set
				{
					throw null;
				}
			}

			public FilterAttribute FilterAttribute
			{
				[CompilerGenerated]
				get
				{
					throw null;
				}
				[CompilerGenerated]
				set
				{
					throw null;
				}
			}

			public FilterEpisode FilterEpisode
			{
				[CompilerGenerated]
				get
				{
					throw null;
				}
				[CompilerGenerated]
				set
				{
					throw null;
				}
			}

			public FilterType FilterType
			{
				[CompilerGenerated]
				get
				{
					throw null;
				}
				[CompilerGenerated]
				set
				{
					throw null;
				}
			}

			public SkillFilterType[] UnCheckedSkillFilterTypes
			{
				[CompilerGenerated]
				get
				{
					throw null;
				}
				[CompilerGenerated]
				set
				{
					throw null;
				}
			}

			public SortKey SortKey
			{
				[CompilerGenerated]
				get
				{
					throw null;
				}
				[CompilerGenerated]
				set
				{
					throw null;
				}
			}

			public bool SortOrderByAsc
			{
				[CompilerGenerated]
				get
				{
					throw null;
				}
				[CompilerGenerated]
				set
				{
					throw null;
				}
			}

			public int[] UnCheckedCharacterIds
			{
				[CompilerGenerated]
				get
				{
					throw null;
				}
				[CompilerGenerated]
				set
				{
					throw null;
				}
			}

			public DisplaySettingsData()
			{
				throw null;
			}
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CInitializeAsync_003Ed__39 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncUniTaskMethodBuilder _003C_003Et__builder;

			public CardList _003C_003E4__this;

			public CellData[] data;

			public FilterUnit unit;

			public FilterAttribute attribute;

			public FilterEpisode episode;

			public FilterAnotherCutIn anotherCutIn;

			public CardRarityFilterType[] rarities;

			public SkillFilterType[] skills;

			public FilterCostumeType[] costumes;

			public bool isUseFilterCostume;

			public int[] characterIds;

			public SortKey key;

			public SortOrderBy sortOrderBy;

			public int frameCount;

			public CancellationToken ctx;

			private UniTask.Awaiter _003C_003Eu__1;

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

		[SerializeField]
		protected ListView listView;

		[SerializeField]
		protected UIPartsSortOrder sortOrderButton;

		[SerializeField]
		protected UIPartsFilterButton filterButton;

		[SerializeField]
		private CardListSortDropdown sortDropdown;

		[SerializeField]
		protected CustomTextMesh nothingText;

		[SerializeField]
		private UIPartsMasterLevel masterLevelTweenReference;

		protected List<CardThumbnailViewDataBase> cellDataList;

		protected CellData[] cellDatas;

		protected FilterUnit filterUnit;

		protected FilterAttribute filterAttribute;

		protected FilterEpisode filterEpisode;

		protected FilterAnotherCutIn filterAnotherCutIn;

		protected CardRarityFilterType[] unCheckedFilterRarityTypes;

		protected SkillFilterType[] unCheckedFilterSkillTypes;

		protected FilterCostumeType[] _filterCostumeTypes;

		protected bool _isUseFilterCostume;

		protected SortKey sortKey;

		protected int[] unCheckedCharacterIds;

		protected ICardListFilterSort filterDialog;

		protected SortOrderBy sortOrderBy;

		public Action<SortOrderBy> OnChangeSortOrderBy;

		public Action<CardListFilterSortSettingData> OnFilterOKEvent;

		public Action<CardThumbnailViewDataBase> OnClickCell;

		public Action<CardThumbnailViewDataBase> OnLongPressCell;

		public Action<ListViewItem, int> OnCreateCellEvent;

		private CardListFilterDialog.ViewData _filterDialogViewData;

		private int? _supportDeckCharacterId;

		private Vector2 backupScrollPosition;

		public List<CardThumbnailViewDataBase> CellDataList
		{
			get
			{
				throw null;
			}
		}

		public void Initialize(CellData[] data, FilterUnit unit, FilterAttribute attribute, FilterEpisode episode, FilterAnotherCutIn anotherCutIn, CardRarityFilterType[] rarity, SkillFilterType[] skills, SortKey key, SortOrderBy sortOrderBy, int[] characterIds, FilterCostumeType[] costumes = null, bool isUseFilterCostume = false, CardListFilterDialog.ViewData filterDialogViewData = null, int? supportDeckCharacterId = null)
		{
			throw null;
		}

		private void SetupSort(SortKey key)
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CInitializeAsync_003Ed__39))]
		public UniTask InitializeAsync(CellData[] data, FilterUnit unit, FilterAttribute attribute, FilterEpisode episode, FilterAnotherCutIn anotherCutIn, CardRarityFilterType[] rarities, SkillFilterType[] skills, SortKey key, SortOrderBy sortOrderBy, int[] characterIds, int frameCount, CancellationToken ctx, FilterCostumeType[] costumes = null, bool isUseFilterCostume = false)
		{
			throw null;
		}

		public void UpdateDialogSetting(FilterUnit unit, FilterAttribute attribute, FilterEpisode episode, FilterAnotherCutIn anotherCutIn, CardRarityFilterType[] rarities, SkillFilterType[] skills, SortKey key, int[] characterIds, FilterCostumeType[] costumes = null)
		{
			throw null;
		}

		private void UpdateSortOrderByDisplay()
		{
			throw null;
		}

		public void UpdateCellDataList()
		{
			throw null;
		}

		protected virtual List<CellData> GetFilteredCellDataList(CellData[] cellDatas)
		{
			throw null;
		}

		protected CellData.EpisodeState[] GetFilteredEpisodeStates()
		{
			throw null;
		}

		protected CardRarityFilterType[] GetUnCheckedFilterRarityTypes()
		{
			throw null;
		}

		protected SkillFilterType[] GetUnCheckedFilterSkillTypes()
		{
			throw null;
		}

		protected FilterCostumeType[] GetFilterCostumeTypes()
		{
			throw null;
		}

		protected bool IsCostumeFilteringTarget(int cardId, FilterCostumeType[] targetCostumeTypes)
		{
			throw null;
		}

		protected virtual List<CellData> SortCellDataList(List<CellData> sortList)
		{
			throw null;
		}

		protected List<CellData> GetSortListOrderBy(List<CellData> sortList)
		{
			throw null;
		}

		protected List<CellData> GetSortListOrderByDescending(List<CellData> sortList)
		{
			throw null;
		}

		private void OnInstantiate(ListViewItem item)
		{
			throw null;
		}

		protected virtual void OnCreateCell(ListViewItem item, int listIndex)
		{
			throw null;
		}

		public void ResetListViewContentPosition()
		{
			throw null;
		}

		public void UpdateListView()
		{
			throw null;
		}

		private void OnClickSortOrderBy()
		{
			throw null;
		}

		protected virtual void OnClickFilter()
		{
			throw null;
		}

		protected virtual void OnFilterOK()
		{
			throw null;
		}

		protected virtual void OnSortCallBack(SortKey sortKey)
		{
			throw null;
		}

		protected void FilterImageChange()
		{
			throw null;
		}

		protected virtual bool NothingMessage()
		{
			throw null;
		}

		public void BackupScrollPosition()
		{
			throw null;
		}

		private void ResetBackupScrollPosition()
		{
			throw null;
		}

		private bool IsMatchFilterUnitType(CellData data)
		{
			throw null;
		}

		public CardList()
		{
			throw null;
		}
	}
}
