using System.Threading;
using Cysharp.Threading.Tasks;

namespace Sekai.MusicScoreMaker.OutGame.Common.Content
{
	public interface IContentNavigator
	{
		UniTask PushContentAsync(Defines.ContentType next, IContentBootData bootData, CancellationToken ct);

		UniTask ChangeContentAsync(Defines.ContentType next, IContentBootData bootData, CancellationToken ct);

		UniTask RequestBackAsync(CancellationToken ct);
	}
}
