using System.Collections.Generic;

namespace Sekai
{
	public class ChainDialogPlayer
	{
		private readonly Queue<DialogBase> dialogs = new Queue<DialogBase>();

		public void Enqueue(DialogBase dialog)
		{
			if (dialog != null)
			{
				dialogs.Enqueue(dialog);
			}
		}

		public void Play()
		{
			if (dialogs.Count > 0)
			{
				dialogs.Dequeue().Open();
			}
		}
	}
}
