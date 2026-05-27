using System;
using System.IO;
using Beebyte.Obfuscator;
using MessagePack;
using Newtonsoft.Json;
using UnityEngine;

namespace Sekai
{
	[MessagePackObject(false)]
	[Skip]
	public class LiveSettingData
	{
		public enum LiveModeType
		{
			High3D = 0,
			Default3D = 1,
			Mode2D = 2,
			Low = 3,
			OriginalMV = 4
		}

		private const string StorageFileName = "LiveSettingData.json";

		private static LiveSettingData cachedStorage;

		[Key("NoteSpeed")]
		public float NoteSpeed { get; set; }

		[Key("TimingAdjustData")]
		public float TimingAdjustData { get; set; }

		[Key("Brightness")]
		public float Brightness { get; set; }

		[Key("LaneTransparent")]
		public float LaneTransparent { get; set; }

		[Key("UseCutIn")]
		public bool UseCutIn { get; set; }

		[Key("HiddenSkillAndPraise")]
		public bool HiddenSkillAndPraise { get; set; }

		[Key("UseSimultaneousPushingLine")]
		public bool UseSimultaneousPushingLine { get; set; }

		[Key("UseVibration")]
		public bool UseVibration { get; set; }

		[Key("UseAllPerfectEffect")]
		public bool UseAllPerfectEffect { get; set; }

		[Key("LiveMode")]
		public LiveModeType LiveMode { get; set; }

		[Key("NoteAlpha")]
		public float NoteAlpha { get; set; }

		[Key("GuideAlpha")]
		public float GuideAlpha { get; set; }

		[Key("NoteSkinIndex")]
		public int NoteSkinIndex { get; set; }

		[Key("NoteSeIndex")]
		public int NoteSeIndex { get; set; }

		[Key("IsMirror")]
		public bool IsMirror { get; set; }

		[Key("QualityType")]
		public MVQualityType QualityType { get; set; }

		[Key("IsFastLateFlick")]
		public bool IsFastLateFlick { get; set; }

		[Key("Use120FPS")]
		public bool Use120FPS { get; set; }

		[Key("UsedVSync")]
		public bool? UseVSync { get; set; }

		[Key("NoteEffect")]
		public int NoteEffect { get; set; }

		[Key("_noteShowRate")]
		public float _noteShowRate { get; set; }

		[IgnoreMember]
		[JsonIgnore]
		public float NoteShowRate
		{
			get
			{
				return 1f - _noteShowRate;
			}
			set
			{
				_noteShowRate = 1f - value;
			}
		}

		[Key("FeverEffectTypeIndex")]
		public int FeverEffectTypeIndex { get; set; }

		[Key("TotalPowerUpperLimit")]
		public int? TotalPowerUpperLimit { get; set; }

		[Key("TotalPowerLowerLimit")]
		public int? TotalPowerLowerLimit { get; set; }

		[Key("CustomRoomTotalPowerUpperLimit")]
		public int? CustomRoomTotalPowerUpperLimit { get; set; }

		[Key("CustomRoomTotalPowerLowerLimit")]
		public int? CustomRoomTotalPowerLowerLimit { get; set; }

		[Key("ShowsRoomId")]
		public bool ShowsRoomId { get; set; }

		[Key("CustomRoomScoreSettingIndex")]
		public int CustomRoomScoreSettingIndex { get; set; }

		[Key("CustomRoomIsDisplayPlayerInfo")]
		public bool CustomRoomIsDisplayPlayerInfo { get; set; }

		[Key("CustomRoomSelectedDifficulties")]
		public int[] CustomRoomSelectedDifficulties { get; set; }

		[Key("CustomRoomSelectedMusicType")]
		public int CustomRoomSelectedMusicType { get; set; }

		[Key("ScoreSelectType")]
		public int ScoreSelectType { get; set; }

		public LiveSettingData()
		{
			ShowsRoomId = true;
			NoteAlpha = 1f;
			GuideAlpha = 0.6f;
			CustomRoomIsDisplayPlayerInfo = true;
			UseCutIn = true;
			LiveMode = LiveModeType.Default3D;
			UseAllPerfectEffect = true;
			QualityType = MVQualityType.Default;
			Use120FPS = false;
			NoteEffect = 0;
			_noteShowRate = 0f;
			FeverEffectTypeIndex = 0;
			UseSimultaneousPushingLine = true;
			UseVibration = true;
			NoteSpeed = 6f;
			TimingAdjustData = 0f;
			Brightness = 1f;
			LaneTransparent = 1f;
		}

		public static LiveSettingData LoadFromStorage()
		{
			if (cachedStorage != null)
			{
				return cachedStorage;
			}

			LiveSettingData data = null;
			string path = StoragePath;
			if (File.Exists(path))
			{
				try
				{
					data = JsonConvert.DeserializeObject<LiveSettingData>(File.ReadAllText(path));
				}
				catch (Exception exception)
				{
					Debug.LogWarningFormat("LiveSettingData could not be loaded. path:{0} error:{1}", path, exception.Message);
				}
			}

			cachedStorage = data ?? new LiveSettingData();
			return cachedStorage;
		}

		[Skip]
		public static void SaveToStorage(LiveSettingData data)
		{
			cachedStorage = data ?? new LiveSettingData();
			string path = StoragePath;
			try
			{
				Directory.CreateDirectory(Path.GetDirectoryName(path));
				File.WriteAllText(path, JsonConvert.SerializeObject(cachedStorage, Formatting.Indented));
			}
			catch (Exception exception)
			{
				Debug.LogWarningFormat("LiveSettingData could not be saved. path:{0} error:{1}", path, exception.Message);
			}
			SUS.Converter.InvalidateLiveSettingCache();
		}

		private static string StoragePath => Path.Combine(Application.persistentDataPath, StorageFileName);

		public static string GetLiveModeTextKey(LiveModeType mode)
		{
			switch (mode)
			{
				case LiveModeType.High3D:
				case LiveModeType.Default3D:
					return "WORD_3DMV";
				case LiveModeType.Mode2D:
					return "WORD_2DMV";
				case LiveModeType.Low:
					return "WORD_2D";
				case LiveModeType.OriginalMV:
					return "WORD_ORIGINAL_MV";
				default:
					return "WORD_3DMV";
			}
		}

		public float GetNoteAlpha()
		{
			return NoteAlpha == 0f ? 1f : NoteAlpha;
		}

		public float GetGuideAlpha()
		{
			return GuideAlpha == 0f ? 0.6f : GuideAlpha;
		}

		public void SetNoteShowRate(float optionValue)
		{
			_noteShowRate = optionValue / 100f;
		}

		public static float CalculateNoteShowRate(float optionValue)
		{
			return 1f - optionValue / 100f;
		}
	}
}
