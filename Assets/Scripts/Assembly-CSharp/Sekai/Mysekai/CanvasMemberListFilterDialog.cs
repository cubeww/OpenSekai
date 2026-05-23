using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Sekai.UI;
using Sekai.UI.Interface;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Sekai.Mysekai
{
	public class CanvasMemberListFilterDialog : Common2ButtonDialog, ICanvasMemberListFilterSort
	{
		public enum FilterCanvasFinishType
		{
			All = 0,
			CanvasFinish = 1,
			CanvasNoFinish = 2
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CSetup_003Ed__19 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncVoidMethodBuilder _003C_003Et__builder;

			public CanvasMemberListFilterDialog _003C_003E4__this;

			public CardList.FilterUnit filterUnit;

			public CardList.SortKey sortKey;

			public int[] unCheckedCharacterIds;

			public CardRarityFilterType[] filterRarity;

			public FilterCanvasFinishType filterCanvasFinishType;

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

		[FormerlySerializedAs("filterCanvasFinshGroup")]
		[SerializeField]
		private CustomIndexToggleGroup _filterCanvasFinshGroup;

		[FormerlySerializedAs("memberListGroup")]
		[SerializeField]
		private VerticalLayoutGroup _memberListGroup;

		[SerializeField]
		[FormerlySerializedAs("rarityTypeCheckBoxies")]
		private UIPartsCheckBox[] _rarityTypeCheckBoxies;

		[FormerlySerializedAs("scrollRectFilter")]
		[SerializeField]
		private CustomScrollRect _scrollRectFilter;

		[FormerlySerializedAs("buttonsRoots")]
		[SerializeField]
		private RectTransform[] _buttonsRoots;

		[FormerlySerializedAs("characterFilterContent")]
		[SerializeField]
		private CharacterFilterContent _characterFilterContent;

		[FormerlySerializedAs("allCheckBox")]
		[SerializeField]
		private UIPartsCheckBox _allCheckBox;

		[SerializeField]
		[FormerlySerializedAs("unitCheckBoxes")]
		private UIPartsCheckBox[] _unitCheckBoxes;

		private CardListFilterSortSettingData _data;

		private Dictionary<int, UIPartsCharacterIconAndCheckBox> _characterCheckBoxDict;

		private Dictionary<CardList.FilterUnit, UIPartsCheckBox> _unitCheckBoxDict;

		public CardListFilterSortSettingData SettingResult
		{
			get
			{
				throw null;
			}
		}

		public FilterCanvasFinishType CanvasFinishType
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

		protected override void Awake()
		{
			throw null;
		}

		[AsyncStateMachine(typeof(_003CSetup_003Ed__19))]
		public void Setup(CardList.FilterUnit filterUnit, CardList.SortKey sortKey, int[] unCheckedCharacterIds, CardRarityFilterType[] filterRarity, FilterCanvasFinishType filterCanvasFinishType)
		{
			throw null;
		}

		private void SetupCharacterList()
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

		private void SetupUnits()
		{
			throw null;
		}

		private void SetRarityFilterType()
		{
			throw null;
		}

		private void OnCanvasFinshSelectedIndexChanged(int index)
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

		private int[] GetUnCheckedCharacterIds()
		{
			throw null;
		}

		private CardRarityFilterType[] GetUnCheckedFilterRarityTypes()
		{
			throw null;
		}

		public CanvasMemberListFilterDialog()
		{
			throw null;
		}
	}
}
