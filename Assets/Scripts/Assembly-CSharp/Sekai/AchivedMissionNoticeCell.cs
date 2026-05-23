using Beebyte.Obfuscator;
using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	public class AchivedMissionNoticeCell : MonoBehaviour
	{
		[SerializeField]
		private CustomTextMesh _noticeText;

		[SerializeField]
		private Transform _normalMissionIconRoot;

		[SerializeField]
		private Transform _exMissionIconRoot;

		[Skip]
		public void Refresh(string text, bool isExMission)
		{
			throw null;
		}

		private void SetupIconObject(bool isExMission)
		{
			throw null;
		}

		public AchivedMissionNoticeCell()
		{
		}
	}
}
