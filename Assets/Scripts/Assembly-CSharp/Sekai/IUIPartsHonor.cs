using UnityEngine;

namespace Sekai
{
	public interface IUIPartsHonor
	{
		int Id { get; }

		HonorType Type { get; }

		bool Enabled { get; set; }

		bool IsLoading { get; }

		Vector2 SizeDelta { get; }

		void SetSlot(HonorSlot slotType);

		void SetScale(UIPartsHonorImage.Size sizeType);

		void Release();

		void KillIfLoading();

		void SetActive(bool active);
	}
}
