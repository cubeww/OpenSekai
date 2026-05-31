using Newtonsoft.Json;

namespace Sekai.MusicScoreMaker.Common
{
	public sealed class CustomMusicScoreManifest
	{
		public int formatVersion = 1;

		public string id;

		public string title;

		public string scoreTitle;

		public string composer;

		public string lyricist;

		public string arranger;

		public string singer;

		public string collaborationLabel;

		public string description;

		public string audioFileName;

		public string jacketFileName;

		public string scoreFileName;

		public float fillerSec;

		public int secForMusicScoreMaker;

		public float previewStartTimeSec;

		public string userName;

		public string musicDifficultyType;

		public int playLevel;

		[JsonIgnore]
		public bool HasScoreFileName => !string.IsNullOrEmpty(scoreFileName);

		[JsonIgnore]
		public bool HasAudioFileName => !string.IsNullOrEmpty(audioFileName);

		public void Normalize()
		{
			if (formatVersion <= 0)
			{
				formatVersion = 1;
			}
			if (string.IsNullOrWhiteSpace(id))
			{
				id = CustomMusicScoreStorage.GenerateShortId();
			}
			if (string.IsNullOrWhiteSpace(title))
			{
				title = "Untitled";
			}
			if (string.IsNullOrWhiteSpace(scoreTitle))
			{
				scoreTitle = title;
			}
			composer ??= string.Empty;
			lyricist ??= string.Empty;
			arranger ??= string.Empty;
			singer ??= string.Empty;
			collaborationLabel ??= string.Empty;
			description ??= string.Empty;
			audioFileName ??= "audio.ogg";
			jacketFileName ??= "jacket.png";
			scoreFileName ??= "score.json";
			if (fillerSec < 0f)
			{
				fillerSec = 0f;
			}
			if (secForMusicScoreMaker < 0)
			{
				secForMusicScoreMaker = 0;
			}
			if (previewStartTimeSec < 0f)
			{
				previewStartTimeSec = 0f;
			}
			userName ??= string.Empty;
			if (string.IsNullOrWhiteSpace(musicDifficultyType))
			{
				musicDifficultyType = "master";
			}
			if (playLevel < 0)
			{
				playLevel = 0;
			}
		}
	}
}
