using System;

namespace Sekai.Live
{
	public static class LiveUtility
	{
		public static float GetLaneOffset(NoteCategory category, LiveBundleBuildData bundleBuildData)
		{
			if (bundleBuildData == null)
			{
				switch (category)
				{
					case NoteCategory.Normal:
					case NoteCategory.Friction:
					case NoteCategory.FrictionHide:
						return 1.25f;
					case NoteCategory.Guide:
					case NoteCategory.GuideEnd:
					case NoteCategory.GuideHidden:
						return 0f;
					default:
						return 1.5f;
				}
			}

			switch (category)
			{
				case NoteCategory.Normal:
				case NoteCategory.Friction:
				case NoteCategory.FrictionHide:
					return bundleBuildData.NormalNoteOffsetX;
				case NoteCategory.Long:
				case NoteCategory.Connection:
				case NoteCategory.FrictionLong:
				case NoteCategory.FrictionHideLong:
				case NoteCategory.Combo:
				case NoteCategory.Hidden:
				case NoteCategory.Skip:
				case NoteCategory.Error:
					return bundleBuildData.LongNoteOffsetX;
				case NoteCategory.Flick:
				case NoteCategory.FrictionFlick:
					return bundleBuildData.FlickNoteOffsetX;
				case NoteCategory.Guide:
				case NoteCategory.GuideEnd:
				case NoteCategory.GuideHidden:
					return 0f;
				default:
					throw new ArgumentOutOfRangeException("category", category, null);
			}
		}
	}
}
