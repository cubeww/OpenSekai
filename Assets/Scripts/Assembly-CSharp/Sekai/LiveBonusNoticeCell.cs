using Beebyte.Obfuscator;
using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	public class LiveBonusNoticeCell : MonoBehaviour
	{
		[SerializeField]
		private CustomText messgeText;

		[Skip]
		public void Refresh(string message)
		{
			throw null;
		}

		public LiveBonusNoticeCell()
		{
		}
	}
}
