using UnityEngine;

namespace Sekai.Core.Live
{
	public class SkillTextView : MonoBehaviour
	{
		public void Setup(LiveDeckMember member, SkillData skillData, int eventId)
		{
			gameObject.SetActive(false);
		}

		public void Load()
		{
		}

		public void Execute(LiveScore score, bool isEncore = false)
		{
		}

		public void Clear()
		{
			gameObject.SetActive(false);
		}

		public void Dispose()
		{
		}
	}
}
