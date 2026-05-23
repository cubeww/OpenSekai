using System.Threading;
using Cysharp.Threading.Tasks;

namespace Sekai.MusicScoreMaker.OutGame.SaveDraft
{
	public abstract class SaveDraftExecutorBase
	{
		protected readonly int _slotNo;

		protected APICoreParam.AfterErrorDetectionType _afterErrorDetectionType;

		public void SetAfterErrorDetectionType(APICoreParam.AfterErrorDetectionType type)
		{
			_afterErrorDetectionType = type;
		}

		protected SaveDraftExecutorBase(int slotNo)
		{
			_slotNo = slotNo;
		}

		public abstract UniTask<SaveDraftResult> ExecuteAsync(CancellationToken ct);
	}
}
