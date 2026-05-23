using Sekai.ApiData;

namespace Sekai.Service
{
	public sealed class MusicScoreSaveDraftUpdateService : MusicScoreSaveDraftServiceBase<UserCustomMusicScoreDraftUpdateRequest>
	{
		public MusicScoreSaveDraftUpdateService(int slotNo, UserCustomMusicScoreDraftUpdateRequest request)
			: base(slotNo, request)
		{
			throw null;
		}

		protected override APICaller<UserCustomMusicScoreDraftUpdateRequest, UserCustomMusicScoreDraftListResponse> CreateApi(long userId, int slotNo)
		{
			throw null;
		}
	}
}
