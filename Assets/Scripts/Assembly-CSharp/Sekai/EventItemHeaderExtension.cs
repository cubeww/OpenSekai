using Sekai.UI;
using UnityEngine;

namespace Sekai
{
	public class EventItemHeaderExtension : HeaderExtensionBase
	{
		[SerializeField]
		protected UITextureLoader eventItemTextureLoader;

		[SerializeField]
		private CustomTextMesh eventItemNumText;

		private MasterEventItem masterEventItem;

		private MasterEvent masterEvent;

		public override HeaderCategory HeaderCategory
		{
			get
			{
				throw null;
			}
		}

		private void Awake()
		{
			throw null;
		}

		private void OnDestroy()
		{
			throw null;
		}

		public void Setup(int eventId, int? currentCharacterId = null)
		{
			throw null;
		}

		private void SetupEventItem(int eventId, int? currentCharacterId)
		{
			throw null;
		}

		private MasterEventItem GetMasterEventItem(int eventId, int? currentCharacterId)
		{
			throw null;
		}

		public override void UpdateInfo()
		{
			throw null;
		}

		private void UpdateEventItem()
		{
			throw null;
		}

		public void OnUpdateEventItem(UserEventItem[] update)
		{
			throw null;
		}

		public EventItemHeaderExtension()
		{
		}
	}
}
