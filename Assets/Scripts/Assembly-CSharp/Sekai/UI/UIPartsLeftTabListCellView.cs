using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sekai.UI
{
	public class UIPartsLeftTabListCellView : MonoBehaviour
	{
		[SerializeField]
		protected GameObject attentionBadge;

		[SerializeField]
		private GameObject selectedObj;

		[SerializeField]
		protected GameObject lockObj;

		[SerializeField]
		private CustomImage lineImage;

		[SerializeField]
		protected CustomTextMesh text;

		[SerializeField]
		private GameObject[] subTabMarkObjects;

		[SerializeField]
		private GameObject[] subTabArrowObjects;

		[SerializeField]
		private UIPartsCommonBalloon lockBalloon;

		[SerializeField]
		private GraphicButtonTapEffect _tapEffect;

		private static readonly Lazy<Dictionary<bool, Color>> ActiveTextColorTable;

		public void SetAttentionBadge(bool showsBadge)
		{
			if (attentionBadge != null)
			{
				attentionBadge.SetActive(showsBadge);
			}
		}

		public void SetLine(bool isSub)
		{
			if (lineImage == null)
			{
				return;
			}

			lineImage.SetActive(true);
			lineImage.color = isSub ? new Color(0.78f, 0.86f, 0.9f, 1f) : new Color(0.35f, 0.68f, 0.78f, 1f);
		}

		public void SetSubTabObject(bool hasSubData, bool isSelected, bool isDisplayingSubTab)
		{
			if (hasSubData)
			{
				SetActiveAt(subTabMarkObjects, 0, !isSelected);
				SetActiveAt(subTabMarkObjects, 1, isSelected);
				SetActiveAt(subTabArrowObjects, 0, !isSelected || !isDisplayingSubTab);
				SetActiveAt(subTabArrowObjects, 1, isSelected && isDisplayingSubTab);
				return;
			}

			SetActiveAll(subTabMarkObjects, false);
			SetActiveAll(subTabArrowObjects, false);
		}

		public void SetText(string name)
		{
			text?.SetText(name);
		}

		public void SetActiveText(bool isActive)
		{
			text?.SetActive(isActive);
		}

		public virtual void SetSelected(bool isSelected)
		{
			if (selectedObj != null)
			{
				selectedObj.SetActive(isSelected);
			}

			var color = ActiveTextColorTable.Value[isSelected];
			if (_tapEffect != null)
			{
				_tapEffect.SetDefaultColor(color);
				_tapEffect.ResetInteraction();
			}

			if (text != null)
			{
				text.color = color;
			}
		}

		public void SetLocked(bool isLocked)
		{
			if (text != null && !string.IsNullOrEmpty(text.Text))
			{
				var current = text.color;
				current.a = isLocked ? 0.45f : 1f;
				text.color = current;
			}

			if (lockObj != null)
			{
				lockObj.SetActive(isLocked);
			}
		}

		public void SetLockBalloon(bool showsBalloon, string text)
		{
			if (lockBalloon == null)
			{
				return;
			}

			lockBalloon.gameObject.SetActive(showsBalloon);
			if (showsBalloon)
			{
				lockBalloon.Setup(text, false, false);
			}
		}

		public UIPartsLeftTabListCellView()
		{
		}

		static UIPartsLeftTabListCellView()
		{
			ActiveTextColorTable = new Lazy<Dictionary<bool, Color>>(() => new Dictionary<bool, Color>
			{
				{ true, Color.black },
				{ false, Color.white }
			});
		}

		private static void SetActiveAt(GameObject[] objects, int index, bool active)
		{
			if (objects != null && index >= 0 && index < objects.Length && objects[index] != null)
			{
				objects[index].SetActive(active);
			}
		}

		private static void SetActiveAll(GameObject[] objects, bool active)
		{
			if (objects == null)
			{
				return;
			}

			foreach (var obj in objects)
			{
				if (obj != null)
				{
					obj.SetActive(active);
				}
			}
		}
	}
}
