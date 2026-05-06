using System;
using System.Collections.Generic;
using Sekai.Core.Live;
using Sekai.Live;
using UnityEngine;

namespace Sekai
{
	public class TapEffectView : MonoBehaviour
	{
		[Serializable]
		public class EffectPrefab
		{
			public string name;
			public GameObject prefab;
			public int poolCount = 16;
		}

		private struct NoteInfo
		{
			public NoteResult result;

			public NoteCategory category;

			public NoteType type;

			public NoteDirection direction;

			public int laneStart;

			public int laneEnd;

			public INote pairNote;

			public int noteId;
		}

		private const int LaneCount = 12;

		private const int DefaultPoolCount = 16;

		[SerializeField] private EffectPrefab[] effectPrefabs;

		[SerializeField] private GameObject fallbackSinglePrefab;

		[SerializeField] private GameObject fallbackFlickPrefab;

		[SerializeField] private GameObject fallbackLoopPrefab;

		private BaseLiveController liveBaseController;

		private ParticleSystemController[] tapLaneEffectList;

		private ParticleSystemController[] defaultLaneEffectList;

		private ParticleSystemController[] criticalLaneEffectList;

		private ParticleSystemController[] flickLaneEffectList;

		private ParticleSystemController[] criticalFlickLaneEffectList;

		private Dictionary<string, EffectPool> poolDict;

		private Dictionary<int, EffectPool> auraPoolDict;

		private Dictionary<int, EffectPool> genPoolDict;

		private Dictionary<int, string> tapSEDict;

		private Dictionary<int, string> pairNoteSeMap;

		private Dictionary<NoteType, EffectPool> flashPoolDict;

		private Dictionary<NoteType, EffectPool> connectionPoolDict;

		private Dictionary<int, EffectPool> frictionPoolDict;

		private NoteInfo noteInfo;

		private int lastFrame;

		private int[] lastTapLanes;

		private bool isEnableVibration;

		private LiveSoundPlayer liveSoundPlayer;

		public void Setup(
			BaseLiveController liveBaseController,
			EffectPrefab[] effectPrefabs = null,
			GameObject fallbackSinglePrefab = null,
			GameObject fallbackFlickPrefab = null,
			GameObject fallbackLoopPrefab = null,
			LiveSoundPlayer liveSoundPlayer = null)
		{
			if (effectPrefabs != null)
			{
				this.effectPrefabs = effectPrefabs;
			}
			this.liveBaseController = liveBaseController;
			if (fallbackSinglePrefab != null)
			{
				this.fallbackSinglePrefab = fallbackSinglePrefab;
			}
			if (fallbackFlickPrefab != null)
			{
				this.fallbackFlickPrefab = fallbackFlickPrefab;
			}
			if (fallbackLoopPrefab != null)
			{
				this.fallbackLoopPrefab = fallbackLoopPrefab;
			}

			poolDict = new Dictionary<string, EffectPool>();
			auraPoolDict = new Dictionary<int, EffectPool>();
			genPoolDict = new Dictionary<int, EffectPool>();
			tapSEDict = new Dictionary<int, string>();
			pairNoteSeMap = new Dictionary<int, string>();
			flashPoolDict = new Dictionary<NoteType, EffectPool>();
			connectionPoolDict = new Dictionary<NoteType, EffectPool>();
			frictionPoolDict = new Dictionary<int, EffectPool>();
			lastTapLanes = new int[LaneCount];
			this.liveSoundPlayer = liveSoundPlayer != null
				? liveSoundPlayer
				: liveBaseController != null ? liveBaseController.GetComponentInChildren<LiveSoundPlayer>(true) : null;

			SetupEffectPools();
			SetupLaneEffects();
			SetupNoteEffectMaps();
			SetupSEDict();
			isEnableVibration = false;
		}

		public void Clear()
		{
			StopLaneEffects(tapLaneEffectList);
			StopLaneEffects(defaultLaneEffectList);
			StopLaneEffects(criticalLaneEffectList);
			StopLaneEffects(flickLaneEffectList);
			StopLaneEffects(criticalFlickLaneEffectList);
			lastFrame = 0;
			pairNoteSeMap?.Clear();
			if (lastTapLanes != null)
			{
				Array.Clear(lastTapLanes, 0, lastTapLanes.Length);
			}
		}

		public void Unpicked(int lane, ref LiveTouch touch)
		{
			if (touch.phase != TouchPhase.Began)
			{
				return;
			}

			PlayUnpickedEffect(lane);
		}

		public void Excute(INote note, Func<bool> checkPlayedHaptic)
		{
			if (note == null || note.Result == NoteResult.None || note.Result == NoteResult.Miss || note.Result == NoteResult.Pass)
			{
				return;
			}

			noteInfo.result = note.Result;
			noteInfo.category = note.Category;
			noteInfo.type = note.Type;
			noteInfo.direction = note.Direction;
			noteInfo.laneStart = note.LaneStart;
			noteInfo.laneEnd = note.LaneEnd;
			noteInfo.pairNote = note.PairNote;
			noteInfo.noteId = note.Id;

			switch (noteInfo.category)
			{
				case NoteCategory.Connection:
					Connection();
					break;
				case NoteCategory.Flick:
				case NoteCategory.FrictionFlick:
					PlayLaneEffect();
					TapAura();
					TapGen();
					FlickSlash();
					break;
				case NoteCategory.Friction:
				case NoteCategory.FrictionLong:
					Friction();
					break;
				case NoteCategory.FrictionHide:
				case NoteCategory.FrictionHideLong:
				case NoteCategory.Combo:
				case NoteCategory.Hidden:
					return;
				default:
					PlayLaneEffect();
					TapAura();
					TapGen();
					break;
			}

			PlaySe();
			PlayHaptic(checkPlayedHaptic);
		}

		private void SetupEffectPools()
		{
			if (effectPrefabs == null)
			{
				return;
			}

			for (int i = 0; i < effectPrefabs.Length; i++)
			{
				EffectPrefab entry = effectPrefabs[i];
				if (entry == null || entry.prefab == null || string.IsNullOrEmpty(entry.name))
				{
					continue;
				}

				EffectPool pool = CreatePool(entry.name, entry.prefab, Mathf.Max(1, entry.poolCount));
				poolDict[entry.name] = pool;
			}
		}

		private void SetupLaneEffects()
		{
			tapLaneEffectList = CreateLaneEffectList("fx_lane_tap");
			defaultLaneEffectList = CreateLaneEffectList("fx_lane_default");
			criticalLaneEffectList = CreateLaneEffectList("fx_lane_critical");
			flickLaneEffectList = CreateLaneEffectList("fx_lane_flick");
			criticalFlickLaneEffectList = CreateLaneEffectList("fx_lane_critical_flick");
		}

		private void SetupNoteEffectMaps()
		{
			CreateTapEffect("fx_note_normal_aura", "fx_note_normal_gen", NoteCategory.Normal, NoteType.Default, NoteResult.Perfect);
			CreateTapEffect("fx_note_normal_aura_Great", "fx_note_normal_gen_Great", NoteCategory.Normal, NoteType.Default, NoteResult.Great);
			CreateTapEffect("fx_note_normal_aura_Good", "fx_note_normal_gen_Good", NoteCategory.Normal, NoteType.Default, NoteResult.Good);
			CreateTapEffect("fx_note_critical_normal_aura", "fx_note_critical_normal_gen", NoteCategory.Normal, NoteType.Critical);

			CreateTapEffect("fx_note_long_aura", "fx_note_long_gen", NoteCategory.Long, NoteType.Default);
			CreateTapEffect("fx_note_critical_long_aura", "fx_note_critical_long_gen", NoteCategory.Long, NoteType.Critical);

			CreateTapEffect("fx_note_flick_aura", "fx_note_flick_gen", NoteCategory.Flick, NoteType.Default);
			CreateTapEffect("fx_note_critical_flick_aura", "fx_note_critical_flick_gen", NoteCategory.Flick, NoteType.Critical);
			flashPoolDict[NoteType.Default] = GetPool("fx_note_flick_flash", 5);
			flashPoolDict[NoteType.Critical] = GetPool("fx_note_critical_flick_flash", 5);

			connectionPoolDict[NoteType.Default] = GetPool("fx_note_long_hold_via_aura", 5);
			connectionPoolDict[NoteType.Critical] = GetPool("fx_note_critical_long_hold_via_aura", 5);

			CreateFrictionEffect("fx_note_trace_aura", NoteType.Default);
			CreateFrictionEffect("fx_note_critical_trace_aura", NoteType.Critical);
		}

		private void CreateTapEffect(string effectName, string genName, NoteCategory category, NoteType type, NoteResult result = NoteResult.Perfect)
		{
			int key = CreateEffectKey(category, type, result);
			auraPoolDict[key] = GetPool(effectName, 12);
			genPoolDict[key] = GetPool(genName, 6);
		}

		private void CreateFrictionEffect(string effectName, NoteType type)
		{
			frictionPoolDict[(int)type] = GetPool(effectName, 5);
		}

		private void TapLane(int lane)
		{
			ParticleSystemController[] effects = GetLaneEffects();
			if (effects == null || lane < 0 || lane >= effects.Length)
			{
				return;
			}

			ParticleSystemController effect = effects[lane];
			if (effect == null)
			{
				return;
			}

			effect.transform.localPosition = GetLaneGroundPosition(lane);
			effect.Play();
		}

		private void TapAura()
		{
			EffectPool pool = GetNotePool(auraPoolDict);
			if (pool == null)
			{
				return;
			}

			int laneStart = Mathf.Clamp(noteInfo.laneStart, 0, LaneCount - 1);
			int laneEnd = Mathf.Clamp(noteInfo.laneEnd, laneStart, LaneCount - 1);
			for (int lane = laneStart; lane <= laneEnd; lane++)
			{
				SpawnAtLane(pool, lane);
			}
		}

		private void TapGen()
		{
			EffectPool pool = GetNotePool(genPoolDict);
			SpawnAtNoteCenter(pool);
		}

		private void FlickSlash()
		{
			if (noteInfo.category != NoteCategory.Flick && noteInfo.category != NoteCategory.FrictionFlick)
			{
				return;
			}
			if (!flashPoolDict.TryGetValue(noteInfo.type, out EffectPool pool))
			{
				return;
			}

			ParticleSystemController effect = SpawnAtNoteCenter(pool, false);
			if (effect == null)
			{
				return;
			}

			if (noteInfo.direction == NoteDirection.Left)
			{
				effect.transform.localEulerAngles = new Vector3(0f, 0f, 15f);
			}
			else if (noteInfo.direction == NoteDirection.Right)
			{
				effect.transform.localEulerAngles = new Vector3(0f, 0f, -15f);
			}
			else
			{
				effect.transform.localEulerAngles = Vector3.zero;
			}
			effect.transform.localScale = new Vector3(noteInfo.laneEnd + 1 - noteInfo.laneStart, 1f, 1f);
			effect.Play();
		}

		private void PlayUnpickedEffect(int lane)
		{
			TapLane(Mathf.Clamp(lane, 0, LaneCount - 1));
			PlayOneShot(LiveSoundDefine.SE_LIVE_TAP);
		}

		private void PlayLaneEffect()
		{
			int laneStart = Mathf.Clamp(noteInfo.laneStart, 0, LaneCount - 1);
			int laneEnd = Mathf.Clamp(noteInfo.laneEnd, laneStart, LaneCount - 1);
			for (int lane = laneStart; lane <= laneEnd; lane++)
			{
				TapLane(lane);
			}
		}

		private void Connection()
		{
			if (noteInfo.category != NoteCategory.Connection)
			{
				return;
			}
			if (!connectionPoolDict.TryGetValue(noteInfo.type, out EffectPool pool))
			{
				return;
			}
			SpawnAtNoteCenter(pool);
		}

		private void Friction()
		{
			if (noteInfo.category != NoteCategory.Friction && noteInfo.category != NoteCategory.FrictionLong && noteInfo.category != NoteCategory.FrictionFlick)
			{
				return;
			}
			if (!frictionPoolDict.TryGetValue((int)noteInfo.type, out EffectPool pool))
			{
				return;
			}
			SpawnAtNoteCenter(pool);
		}

		private void PlayHaptic(Func<bool> checkPlayedHaptic)
		{
			if (!isEnableVibration || checkPlayedHaptic == null || checkPlayedHaptic())
			{
				return;
			}

			Handheld.Vibrate();
		}

		private ParticleSystemController SpawnAtNoteCenter(EffectPool pool, bool play = true)
		{
			if (pool == null)
			{
				return null;
			}

			ParticleSystemController effect = pool.Spawn();
			if (effect == null)
			{
				return null;
			}

			effect.transform.localPosition = GetNoteCenterGroundPosition();
			effect.transform.localEulerAngles = Vector3.zero;
			effect.transform.localScale = Vector3.one;
			if (play)
			{
				effect.Play();
			}
			return effect;
		}

		private ParticleSystemController SpawnAtLane(EffectPool pool, int lane)
		{
			if (pool == null)
			{
				return null;
			}

			ParticleSystemController effect = pool.Spawn();
			if (effect == null)
			{
				return null;
			}

			effect.transform.localPosition = GetLaneGroundPosition(lane);
			effect.transform.localEulerAngles = Vector3.zero;
			effect.transform.localScale = Vector3.one;
			effect.Play();
			return effect;
		}

		private ParticleSystemController[] CreateLaneEffectList(string effectName)
		{
			GameObject prefab = GetPrefab(effectName);
			ParticleSystemController[] effects = new ParticleSystemController[LaneCount];
			if (prefab == null)
			{
				return effects;
			}

			Transform root = CreateChild(effectName + "Root");
			for (int i = 0; i < effects.Length; i++)
			{
				GameObject instance = UnityEngine.Object.Instantiate(prefab, root, false);
				instance.name = effectName + "_" + i;
				instance.transform.localPosition = GetLaneGroundPosition(i);
				ParticleSystemController controller = instance.GetComponent<ParticleSystemController>();
				if (controller == null)
				{
					controller = instance.AddComponent<ParticleSystemController>();
				}
				controller.RegisterToLiveController(liveBaseController);
				controller.Stop();
				effects[i] = controller;
			}
			return effects;
		}

		private EffectPool CreatePool(string effectName, GameObject prefab, int poolCount)
		{
			Transform root = CreateChild(effectName + "Pool");
			EffectPool pool = root.gameObject.AddComponent<EffectPool>();
			pool.Setup(prefab, poolCount, liveBaseController);
			return pool;
		}

		private EffectPool GetNotePool(Dictionary<int, EffectPool> dict)
		{
			int key = CreateEffectKey(noteInfo.category, noteInfo.type, noteInfo.result);
			if (dict.TryGetValue(key, out EffectPool pool) && pool != null)
			{
				return pool;
			}

			key = CreateEffectKey(noteInfo.category, noteInfo.type, NoteResult.Perfect);
			if (dict.TryGetValue(key, out pool))
			{
				return pool;
			}

			key = CreateEffectKey(LiveUtility.NormalizeViewCategory(noteInfo.category), NoteType.Default, NoteResult.Perfect);
			dict.TryGetValue(key, out pool);
			return pool;
		}

		private EffectPool GetPool(string effectName, int fallbackPoolCount = DefaultPoolCount)
		{
			if (string.IsNullOrEmpty(effectName) || poolDict == null)
			{
				return null;
			}
			if (poolDict.TryGetValue(effectName, out EffectPool pool) && pool != null)
			{
				return pool;
			}
			pool = CreateFallbackPool(effectName, fallbackPoolCount);
			if (pool != null)
			{
				poolDict[effectName] = pool;
			}
			return pool;
		}

		private GameObject GetPrefab(string effectName)
		{
			if (effectPrefabs == null)
			{
				return GetFallbackPrefab(effectName);
			}

			for (int i = 0; i < effectPrefabs.Length; i++)
			{
				EffectPrefab entry = effectPrefabs[i];
				if (entry != null && entry.name == effectName)
				{
					return entry.prefab != null ? entry.prefab : GetFallbackPrefab(effectName);
				}
			}
			return GetFallbackPrefab(effectName);
		}

		private EffectPool CreateFallbackPool(string effectName, int poolCount)
		{
			GameObject prefab = GetFallbackPrefab(effectName);
			if (prefab == null)
			{
				return null;
			}
			return CreatePool(effectName, prefab, Mathf.Max(1, poolCount));
		}

		private GameObject GetFallbackPrefab(string effectName)
		{
			if (string.IsNullOrEmpty(effectName))
			{
				return null;
			}

			if (effectName.Contains("hold") || effectName.Contains("long_hold") || effectName.Contains("via") || effectName.Contains("trace"))
			{
				return fallbackLoopPrefab != null ? fallbackLoopPrefab : fallbackSinglePrefab;
			}
			if (effectName.Contains("flick"))
			{
				return fallbackFlickPrefab != null ? fallbackFlickPrefab : fallbackSinglePrefab;
			}
			if (effectName.Contains("lane"))
			{
				return fallbackSinglePrefab;
			}
			return fallbackSinglePrefab;
		}

		private ParticleSystemController[] GetLaneEffects()
		{
			if (noteInfo.category == NoteCategory.Flick || noteInfo.category == NoteCategory.FrictionFlick)
			{
				return noteInfo.type == NoteType.Critical ? criticalFlickLaneEffectList : flickLaneEffectList;
			}
			if (noteInfo.type == NoteType.Critical)
			{
				return criticalLaneEffectList;
			}
			return defaultLaneEffectList ?? tapLaneEffectList;
		}

		private void SetupSEDict()
		{
			tapSEDict[CreateSeKey(NoteCategory.Normal, NoteType.Default)] = LiveSoundDefine.SE_LIVE_PERFECT;
			tapSEDict[CreateSeKey(NoteCategory.Normal, NoteType.Critical)] = LiveSoundDefine.SE_LIVE_CRITICAL;
			tapSEDict[CreateSeKey(NoteCategory.Flick, NoteType.Default)] = LiveSoundDefine.SE_LIVE_FLICK;
			tapSEDict[CreateSeKey(NoteCategory.Flick, NoteType.Critical)] = LiveSoundDefine.SE_LIVE_FLICK_CRITICAL;
			tapSEDict[CreateSeKey(NoteCategory.Long, NoteType.Default)] = LiveSoundDefine.SE_LIVE_PERFECT;
			tapSEDict[CreateSeKey(NoteCategory.Long, NoteType.Critical)] = LiveSoundDefine.SE_LIVE_PERFECT;
			tapSEDict[CreateSeKey(NoteCategory.Friction, NoteType.Default)] = LiveSoundDefine.SE_LIVE_FRICTION;
			tapSEDict[CreateSeKey(NoteCategory.Friction, NoteType.Critical)] = LiveSoundDefine.SE_LIVE_FRICTION_CRITICAL;
			tapSEDict[CreateSeKey(NoteCategory.FrictionLong, NoteType.Default)] = LiveSoundDefine.SE_LIVE_FRICTION;
			tapSEDict[CreateSeKey(NoteCategory.FrictionLong, NoteType.Critical)] = LiveSoundDefine.SE_LIVE_FRICTION_CRITICAL;
			tapSEDict[CreateSeKey(NoteCategory.FrictionFlick, NoteType.Default)] = LiveSoundDefine.SE_LIVE_FLICK;
			tapSEDict[CreateSeKey(NoteCategory.FrictionFlick, NoteType.Critical)] = LiveSoundDefine.SE_LIVE_FLICK_CRITICAL;
			tapSEDict[CreateSeKey(NoteCategory.Connection, NoteType.Default)] = LiveSoundDefine.SE_LIVE_CONNECT;
			tapSEDict[CreateSeKey(NoteCategory.Connection, NoteType.Critical)] = LiveSoundDefine.SE_LIVE_CONNECT_CRITICAL;
		}

		private void PlaySe()
		{
			if (tapSEDict == null || !tapSEDict.TryGetValue(CreateSeKey(noteInfo.category, noteInfo.type), out string seName))
			{
				return;
			}

			string seCategory = seName;
			if (noteInfo.category == NoteCategory.Normal && noteInfo.type == NoteType.Default)
			{
				switch (noteInfo.result)
				{
					case NoteResult.Auto:
					case NoteResult.Perfect:
					case NoteResult.JustPerfect:
						seName = LiveSoundDefine.SE_LIVE_PERFECT;
						seCategory = "tap1";
						break;
					case NoteResult.Good:
						seName = LiveSoundDefine.SE_LIVE_GOOD;
						seCategory = "tap2";
						break;
					case NoteResult.Great:
						seName = LiveSoundDefine.SE_LIVE_GREAT;
						seCategory = "tap1";
						break;
					default:
						return;
				}
			}

			PlaySE(seName, seCategory, noteInfo.noteId, noteInfo.pairNote);
		}

		private void PlaySE(string seName, string category, int myNoteId, INote pairNote)
		{
			if (string.IsNullOrEmpty(seName))
			{
				return;
			}

			if (pairNote == null)
			{
				PlayOneShot(seName);
				return;
			}

			int pairNoteId = pairNote.Id;
			if (pairNoteSeMap != null && pairNoteSeMap.TryGetValue(pairNoteId, out string pairCategory))
			{
				if (pairCategory != category)
				{
					PlayOneShot(seName);
				}
				pairNoteSeMap.Remove(pairNoteId);
				return;
			}

			if (pairNoteSeMap != null && pairNoteSeMap.TryGetValue(myNoteId, out string myCategory))
			{
				if (myCategory != category)
				{
					PlayOneShot(seName);
				}
				pairNoteSeMap.Remove(myNoteId);
				return;
			}

			if (pairNoteSeMap != null)
			{
				pairNoteSeMap[myNoteId] = category;
			}
			PlayOneShot(seName);
		}

		private void PlayOneShot(string seName)
		{
			if (liveSoundPlayer == null && liveBaseController != null)
			{
				liveSoundPlayer = liveBaseController.GetComponentInChildren<LiveSoundPlayer>(true);
			}

			float seVolume = MusicScore.CurrentFrameInfo.seVolume;
			if (seVolume <= 0f)
			{
				seVolume = 1f;
			}
			liveSoundPlayer?.PlayIngameSEOneShot(seName, seVolume);
		}

		private Vector3 GetNoteCenterGroundPosition()
		{
			float lane = (noteInfo.laneStart + noteInfo.laneEnd) * 0.5f;
			return GetLaneGroundPosition(lane);
		}

		private static Vector3 GetLaneGroundPosition(float lane)
		{
			return new Vector3(LiveUtility.CalcLaneTransformX(lane), 0f, 0f);
		}

		private static int CreateEffectKey(NoteCategory category, NoteType type, NoteResult result)
		{
			return ((int)category * 100) + ((int)type * 10) + (int)result;
		}

		private static int CreateSeKey(NoteCategory category, NoteType type)
		{
			return (int)category + (int)type * 10;
		}

		private Transform CreateChild(string objectName)
		{
			Transform child = transform.Find(objectName);
			if (child != null)
			{
				return child;
			}

			GameObject gameObject = new GameObject(objectName);
			child = gameObject.transform;
			child.SetParent(transform, false);
			return child;
		}

		private static void StopLaneEffects(ParticleSystemController[] effects)
		{
			for (int i = 0; effects != null && i < effects.Length; i++)
			{
				effects[i]?.Stop();
			}
		}
	}
}
