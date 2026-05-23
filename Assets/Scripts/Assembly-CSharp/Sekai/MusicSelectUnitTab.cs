using System;
using System.Runtime.CompilerServices;
using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	public class MusicSelectUnitTab : ListViewItem
	{
		public enum Status
		{
			off = 0,
			on = 1
		}

		private enum Kind
		{
			icon = 0,
			text = 1
		}

		[SerializeField]
		private CustomImage iconImage;

		[SerializeField]
		private CustomTextMesh _tabText;

		[SerializeField]
		private GraphicButtonTapEffect[] _tapEffectList;

		[SerializeField]
		private CustomButton button;

		private string spriteBaseName;

		private Status currentStatus;

		public CategoryTabType CategoryTabType
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

		private Action<CategoryTabType> onClickTab
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

		public void Setup(CategoryTabType tabType, Action<CategoryTabType> onClick)
		{
			throw null;
		}

		private void OnClickTab()
		{
			throw null;
		}

		public void SwitchActive(Status status)
		{
			throw null;
		}

		public void UpdateText(string text)
		{
			throw null;
		}

		private void SetTabText(string text)
		{
			throw null;
		}

		public string GetText()
		{
			throw null;
		}

		private Kind GetKind(CategoryTabType type)
		{
			throw null;
		}

		private string GetText(CategoryTabType type)
		{
			throw null;
		}

		private string GetSpriteName(CategoryTabType tabType, Status status)
		{
			throw null;
		}

		public MusicSelectUnitTab()
		{
			throw null;
		}
	}
}
