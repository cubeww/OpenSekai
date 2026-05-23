using Sekai.ApiData;

namespace Sekai.Service
{
	public sealed class MusicScoreSaveDraftCreateService : MusicScoreSaveDraftServiceBase<UserCustomMusicScoreDraftCreateRequest>
	{
		public MusicScoreSaveDraftCreateService(int slotNo, UserCustomMusicScoreDraftCreateRequest request)
			: base(slotNo, request)
		{
		}

		protected override APICaller<UserCustomMusicScoreDraftCreateRequest, UserCustomMusicScoreDraftListResponse> CreateApi(long userId, int slotNo)
		{
			throw null;
		}
	}
}
