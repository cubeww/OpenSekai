using System.Collections.Generic;
using UnityEngine;

namespace Sekai.Core.Live
{
	public class SoloSkillTextViewGroup : MonoBehaviour
	{
		[SerializeField]
		private SkillTextView skillTextViewPrefab;

		private readonly Dictionary<int, SkillTextView> skillTextViewMap;

		private const float THUMBNAIL_SIZE = 0.72f;

		public void Setup(LiveDeckMember[] members, SkillData[] skillDatas, int eventId)
		{
			if (members == null || skillTextViewPrefab == null)
			{
				return;
			}

			for (int i = 0; i < members.Length; i++)
			{
				LiveDeckMember member = members[i];
				if (member == null)
				{
					continue;
				}

				int cardId = SafeGetCardId(member, i);
				SkillData skillData = FindSkillData(skillDatas, cardId, i);
				if (skillData == null)
				{
					continue;
				}

				SkillTextView view = Instantiate(skillTextViewPrefab, transform);
				view.Setup(member, skillData, eventId);
				if (!skillTextViewMap.ContainsKey(cardId))
				{
					skillTextViewMap.Add(cardId, view);
				}
			}
		}

		public void Load()
		{
			foreach (SkillTextView view in skillTextViewMap.Values)
			{
				view?.Load();
			}
		}

		public void Execute(int index, LiveScore score, bool isEncore = false)
		{
			if (skillTextViewMap.TryGetValue(index, out SkillTextView view) && view != null)
			{
				view.Execute(score, isEncore);
				return;
			}

			Debug.LogErrorFormat("SkillTextView is not found. index:{0}", index);
		}

		public void Clear()
		{
			foreach (SkillTextView view in skillTextViewMap.Values)
			{
				view?.Clear();
			}
		}

		public void Dispose()
		{
			foreach (SkillTextView view in skillTextViewMap.Values)
			{
				if (view != null)
				{
					view.Dispose();
					Destroy(view.gameObject);
				}
			}
			skillTextViewMap.Clear();
		}

		public SoloSkillTextViewGroup()
		{
			skillTextViewMap = new Dictionary<int, SkillTextView>();
		}

		private static SkillData FindSkillData(SkillData[] skillDatas, int cardId, int fallbackIndex)
		{
			if (skillDatas == null)
			{
				return null;
			}
			foreach (SkillData skillData in skillDatas)
			{
				if (skillData == null)
				{
					continue;
				}
				int skillCardId = SafeGetSkillCardId(skillData, fallbackIndex);
				if (skillCardId == cardId)
				{
					return skillData;
				}
			}
			return fallbackIndex >= 0 && fallbackIndex < skillDatas.Length ? skillDatas[fallbackIndex] : null;
		}

		private static int SafeGetCardId(LiveDeckMember member, int fallback)
		{
			try
			{
				return member.CardId;
			}
			catch
			{
				return fallback;
			}
		}

		private static int SafeGetSkillCardId(SkillData skillData, int fallback)
		{
			try
			{
				return skillData.CardId;
			}
			catch
			{
				return fallback;
			}
		}
	}
}
