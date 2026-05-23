using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Sekai.ApiData;

namespace Sekai.Service
{
	public sealed class CustomMusicScorePublishedListResult
	{
		[NotNull]
		public IReadOnlyList<UserCustomMusicScorePublishedResponse> MergedList
		{
			[CompilerGenerated]
			get
			{
				throw null;
			}
		}

		public CustomMusicScorePublishedListResult([NotNull] CustomMusicScorePublishedSearchListResponse response)
		{
			throw null;
		}

		private static IReadOnlyList<UserCustomMusicScorePublishedResponse> BuildMergedList(CustomMusicScorePublishedSearchListResponse response)
		{
			throw null;
		}

		[CanBeNull]
		public static UserCustomMusicScorePublishedResponse ConvertOfficialToUserResponse(CustomMusicScoreOfficialCreatorPublishedResponse official)
		{
			throw null;
		}
	}
}
