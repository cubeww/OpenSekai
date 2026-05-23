using System;
using System.Collections.Generic;
using Sekai.Core.Live;
using Sekai.Live;
using UnityEngine;

namespace Sekai
{
	public class TapEffectView : MonoBehaviour
	{
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

		private static readonly int LaneCount;

		private const string COLLABORATION_SE_CATEGORY_1 = "collaborationTap1";

		private const string COLLABORATION_SE_CATEGORY_2 = "collaborationTap2";

		private ParticleSystemController[] tapLaneEffectList;

		private ParticleSystemController[] defaultLaneEffectList;

		private ParticleSystemController[] criticalLaneEffectList;

		private ParticleSystemController[] flickLaneEffectList;

		private ParticleSystemController[] criticalFlickLaneEffectList;

		private Dictionary<int, EffectPool> auraPoolDict;

		private Dictionary<int, EffectPool> genPoolDict;

		private Dictionary<NoteType, EffectPool> flashPoolDict;

		private Dictionary<NoteType, EffectPool> connectionPoolDict;

		private Dictionary<int, EffectPool> frictionPoolDict;

		private Dictionary<int, string> tapSEDict;

		private Dictionary<int, string> pairNoteSeMap;

		private NoteInfo noteInfo;

		private int lastFrame;

		private int[] lastTapLanes;

		private bool isEnableVibration;

		private Action _playSeAction;

		public void Setup(BaseLiveController liveBaseController)
		{
			isEnableVibration = liveBaseController?.Settings?.UseVibration ?? true;
			tapLaneEffectList = new ParticleSystemController[LaneCount];
			defaultLaneEffectList = new ParticleSystemController[LaneCount];
			criticalLaneEffectList = new ParticleSystemController[LaneCount];
			flickLaneEffectList = new ParticleSystemController[LaneCount];
			criticalFlickLaneEffectList = new ParticleSystemController[LaneCount];
			pairNoteSeMap = new Dictionary<int, string>();

			for (int i = 0; i < LaneCount; i++)
			{
				tapLaneEffectList[i] = LoadTapEffect(LivePrefabDefine.FX_LANE_TAP);
				defaultLaneEffectList[i] = LoadTapEffect(LivePrefabDefine.FX_LANE_DEFAULT);
				criticalLaneEffectList[i] = LoadTapEffect(LivePrefabDefine.FX_LANE_CRITICAL);
				flickLaneEffectList[i] = LoadTapEffect(LivePrefabDefine.FX_LANE_DEFAULT_FLICK);
				criticalFlickLaneEffectList[i] = LoadTapEffect(LivePrefabDefine.FX_LANE_CRITICAL_FLICK);
			}

			CreateTapEffect(LivePrefabDefine.FX_NORMAL_PERFECT_AURA, LivePrefabDefine.FX_NORMAL_PERFECT_GEN, NoteCategory.Normal, NoteType.Default, NoteResult.Perfect);
			CreateTapEffect(LivePrefabDefine.FX_NORMAL_GREAT_AURA, LivePrefabDefine.FX_NORMAL_GREAT_GEN, NoteCategory.Normal, NoteType.Default, NoteResult.Great);
			CreateTapEffect(LivePrefabDefine.FX_NORMAL_GOOD_AURA, LivePrefabDefine.FX_NORMAL_GOOD_GEN, NoteCategory.Normal, NoteType.Default, NoteResult.Good);
			CreateTapEffect(LivePrefabDefine.FX_CRITICAL_AURA, LivePrefabDefine.FX_CRITICAL_GEN, NoteCategory.Normal, NoteType.Critical, NoteResult.Perfect);
			CreateFrictionEffect(LivePrefabDefine.FX_FRICITION_AURA, NoteType.Default);
			CreateFrictionEffect(LivePrefabDefine.FX_CRITICAL_FRICITION_AURA, NoteType.Critical);
			CreateTapEffect(LivePrefabDefine.FX_FLICK_AURA, LivePrefabDefine.FX_FLICK_GEN, NoteCategory.Flick, NoteType.Default, NoteResult.Perfect);
			flashPoolDict[NoteType.Default] = CreateEffectPool(LivePrefabDefine.FX_FLICK_FLASH, 5);
			CreateTapEffect(LivePrefabDefine.FX_CRITICAL_FLICK_AURA, LivePrefabDefine.FX_CRITICAL_FLICK_GEN, NoteCategory.Flick, NoteType.Critical, NoteResult.Perfect);
			flashPoolDict[NoteType.Critical] = CreateEffectPool(LivePrefabDefine.FX_CRITICAL_FLICK_FLASH, 5);
			CreateTapEffect(LivePrefabDefine.FX_LONG_AURA, LivePrefabDefine.FX_LONG_GEN, NoteCategory.Long, NoteType.Default, NoteResult.Perfect);
			CreateTapEffect(LivePrefabDefine.FX_CRITICAL_LONG_AURA, LivePrefabDefine.FX_CRITICAL_LONG_GEN, NoteCategory.Long, NoteType.Critical, NoteResult.Perfect);
			connectionPoolDict[NoteType.Default] = CreateEffectPool(LivePrefabDefine.FX_CONNECT_AURA, 5);
			connectionPoolDict[NoteType.Critical] = CreateEffectPool(LivePrefabDefine.FX_CRITICAL_CONNECT_AURA, 5);

			bool isCollaborationMode = IsCollaborationMode(liveBaseController?.BootData);
			SetupSEDict(isCollaborationMode);
			SetupPlaySE(isCollaborationMode);
		}

		private bool IsCollaborationMode(LiveBootDataBase bootData)
		{
			return bootData?.MusicData?.CollaboModeState == LiveMusicData.CollaborationModeState.On;
		}

		private void SetupPlaySE(bool isCollaborationMode)
		{
			_playSeAction = isCollaborationMode ? PlayCollaborationSE : PlaySe;
		}

		public void Clear()
		{
			lastFrame = 0;
			pairNoteSeMap?.Clear();
		}

		public void Dispose()
		{
			Clear();
		}

		private void SetupSEDict(bool isCollaborationMode)
		{
			tapSEDict = new Dictionary<int, string>
			{
				[GetTapSeKey(NoteCategory.Normal, NoteType.Default)] = LiveSoundDefine.SE_LIVE_PERFECT,
				[GetTapSeKey(NoteCategory.Normal, NoteType.Critical)] = LiveSoundDefine.SE_LIVE_CRITICAL,
				[GetTapSeKey(NoteCategory.Long, NoteType.Default)] = LiveSoundDefine.SE_LIVE_PERFECT,
				[GetTapSeKey(NoteCategory.Long, NoteType.Critical)] = LiveSoundDefine.SE_LIVE_PERFECT,
				[GetTapSeKey(NoteCategory.Connection, NoteType.Default)] = LiveSoundDefine.SE_LIVE_CONNECT,
				[GetTapSeKey(NoteCategory.Connection, NoteType.Critical)] = LiveSoundDefine.SE_LIVE_CONNECT_CRITICAL,
				[GetTapSeKey(NoteCategory.Flick, NoteType.Default)] = LiveSoundDefine.SE_LIVE_FLICK,
				[GetTapSeKey(NoteCategory.Flick, NoteType.Critical)] = LiveSoundDefine.SE_LIVE_FLICK_CRITICAL,
				[GetTapSeKey(NoteCategory.Friction, NoteType.Default)] = LiveSoundDefine.SE_LIVE_FRICTION,
				[GetTapSeKey(NoteCategory.Friction, NoteType.Critical)] = LiveSoundDefine.SE_LIVE_FRICTION_CRITICAL,
				[GetTapSeKey(NoteCategory.FrictionLong, NoteType.Default)] = LiveSoundDefine.SE_LIVE_FRICTION,
				[GetTapSeKey(NoteCategory.FrictionLong, NoteType.Critical)] = LiveSoundDefine.SE_LIVE_FRICTION_CRITICAL,
				[GetTapSeKey(NoteCategory.FrictionFlick, NoteType.Default)] = LiveSoundDefine.SE_LIVE_FLICK,
				[GetTapSeKey(NoteCategory.FrictionFlick, NoteType.Critical)] = LiveSoundDefine.SE_LIVE_FLICK_CRITICAL
			};
			pairNoteSeMap = new Dictionary<int, string>();
		}

		private void CreateTapEffect(string effectName, string genName, NoteCategory category, NoteType type, NoteResult result = NoteResult.Perfect)
		{
			int key = GetTapEffectKey(category, type, result);
			auraPoolDict[key] = CreateEffectPool(effectName, 12);
			genPoolDict[key] = CreateEffectPool(genName, 6);
		}

		private void CreateFrictionEffect(string effectName, NoteType type)
		{
			frictionPoolDict[(int)type] = CreateEffectPool(effectName, 12);
		}

		private void TapLane(int lane)
		{
			ParticleSystemController[] list;
			if (noteInfo.category == NoteCategory.Flick || noteInfo.category == NoteCategory.FrictionFlick)
			{
				list = noteInfo.type == NoteType.Critical ? criticalFlickLaneEffectList : flickLaneEffectList;
			}
			else
			{
				list = noteInfo.type == NoteType.Critical ? criticalLaneEffectList : defaultLaneEffectList;
			}

			if (list == null || lane < 0 || lane >= list.Length || list[lane] == null)
			{
				return;
			}

			list[lane].transform.localPosition = new Vector3(LiveUtility.CalcLaneTransformX(lane), 0f, 0f);
			list[lane].Play();
		}

		private void TapAura()
		{
			EffectPool pool = GetTapEffectPool(auraPoolDict);
			if (pool == null)
			{
				return;
			}

			for (int lane = noteInfo.laneStart; lane <= noteInfo.laneEnd; lane++)
			{
				ParticleSystemController controller = pool.Spawn();
				if (controller == null)
				{
					continue;
				}

				controller.transform.localPosition = new Vector3(LiveUtility.CalcLaneTransformX(lane), 0f, 0f);
				controller.Play();
			}
		}

		private void TapGen()
		{
			EffectPool pool = GetTapEffectPool(genPoolDict);
			ParticleSystemController controller = pool?.Spawn();
			if (controller == null)
			{
				return;
			}

			controller.transform.localPosition = new Vector3(LiveUtility.CalcLaneTransformX(GetLaneCenter()), 0f, 0f);
			controller.Play();
		}

		private void FlickSlash()
		{
			if (flashPoolDict == null || !flashPoolDict.TryGetValue(noteInfo.type, out EffectPool pool))
			{
				return;
			}

			ParticleSystemController controller = pool.Spawn();
			if (controller == null)
			{
				return;
			}

			controller.transform.localPosition = new Vector3(LiveUtility.CalcLaneTransformX(GetLaneCenter()), 0f, 0f);
			controller.transform.localScale = new Vector3(noteInfo.laneEnd - noteInfo.laneStart + 1f, 1f, 1f);
			float z = noteInfo.direction == NoteDirection.Right ? -15f : noteInfo.direction == NoteDirection.Left ? 15f : 0f;
			controller.transform.localEulerAngles = new Vector3(0f, 0f, z);
			controller.Play();
		}

		private void PlayUnpickedEffect(int lane)
		{
			if (tapLaneEffectList != null && lane >= 0 && lane < tapLaneEffectList.Length)
			{
				ParticleSystemController controller = tapLaneEffectList[lane];
				if (controller != null)
				{
					controller.transform.localPosition = new Vector3(LiveUtility.CalcLaneTransformX(lane), 0f, 0f);
					controller.Play();
				}
			}

			SoundManager.Instance.PlayIngameSEOneShot(LiveSoundDefine.SE_LIVE_TAP);
		}

		public void Unpicked(int lane, ref LiveTouch touch)
		{
			if (touch.phase != UnityEngine.InputSystem.TouchPhase.Began)
			{
				if (lastFrame < Time.frameCount - 1)
				{
					return;
				}

				if (lastTapLanes == null || touch.fingerId >= lastTapLanes.Length)
				{
					lastFrame = Time.frameCount;
					return;
				}

				if (lastTapLanes[touch.fingerId] == lane)
				{
					lastFrame = Time.frameCount;
					return;
				}
			}

			PlayUnpickedEffect(lane);
			lastFrame = Time.frameCount;
			if (lastTapLanes != null && touch.fingerId >= 0 && touch.fingerId < lastTapLanes.Length)
			{
				lastTapLanes[touch.fingerId] = lane;
			}
		}

		public void Excute(INote note, Func<bool> checkPlayedHaptic)
		{
			if (note == null)
			{
				return;
			}

			NoteResult result = note.Result;
			if (result == NoteResult.Miss || result == NoteResult.Pass)
			{
				return;
			}

			noteInfo = new NoteInfo
			{
				result = result,
				category = note.Category,
				type = note.Type,
				direction = note.Direction,
				laneStart = note.LaneStart,
				laneEnd = note.LaneEnd,
				pairNote = note.PairNote,
				noteId = note.Id
			};
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

			_playSeAction?.Invoke();
			if (checkPlayedHaptic == null || !checkPlayedHaptic())
			{
				PlayHaptic();
			}
		}

		private void PlayLaneEffect()
		{
			for (int lane = noteInfo.laneStart; lane <= noteInfo.laneEnd; lane++)
			{
				TapLane(lane);
			}
		}

		private void Connection()
		{
			if (connectionPoolDict == null || !connectionPoolDict.TryGetValue(noteInfo.type, out EffectPool pool))
			{
				return;
			}

			ParticleSystemController controller = pool.Spawn();
			if (controller == null)
			{
				return;
			}

			controller.transform.localPosition = new Vector3(LiveUtility.CalcLaneTransformX(GetLaneCenter()), 0f, 0f);
			controller.Play();
		}

		private void Friction()
		{
			if (frictionPoolDict == null || !frictionPoolDict.TryGetValue((int)noteInfo.type, out EffectPool pool))
			{
				return;
			}

			ParticleSystemController controller = pool.Spawn();
			if (controller == null)
			{
				return;
			}

			controller.transform.localPosition = new Vector3(LiveUtility.CalcLaneTransformX(GetLaneCenter()), 0f, 0f);
			controller.Play();
		}

		private void PlaySe()
		{
			if (tapSEDict == null || !tapSEDict.TryGetValue(GetTapSeKey(noteInfo.category, noteInfo.type), out string mapped))
			{
				return;
			}

			if (noteInfo.category == NoteCategory.Normal && noteInfo.type == NoteType.Default)
			{
				switch (noteInfo.result)
				{
					case NoteResult.Auto:
					case NoteResult.Perfect:
					case NoteResult.JustPerfect:
						PlaySE(LiveSoundDefine.SE_LIVE_PERFECT, "perfect", noteInfo.noteId, noteInfo.pairNote);
						return;
					case NoteResult.Good:
						PlaySE(LiveSoundDefine.SE_LIVE_GOOD, "good", noteInfo.noteId, noteInfo.pairNote);
						return;
					case NoteResult.Great:
						PlaySE(LiveSoundDefine.SE_LIVE_GREAT, "perfect", noteInfo.noteId, noteInfo.pairNote);
						return;
					default:
						return;
				}
			}

			PlaySE(mapped, mapped, noteInfo.noteId, noteInfo.pairNote);
		}

		private void PlayCollaborationSE()
		{
			if (tapSEDict == null || !tapSEDict.TryGetValue(GetTapSeKey(noteInfo.category, noteInfo.type), out string mapped))
			{
				return;
			}

			string category = GetCollaborationSECategory(noteInfo);
			if (noteInfo.category == NoteCategory.Normal && noteInfo.type == NoteType.Default)
			{
				switch (noteInfo.result)
				{
					case NoteResult.Auto:
					case NoteResult.Perfect:
					case NoteResult.JustPerfect:
						PlaySE(LiveSoundDefine.SE_LIVE_PERFECT, category, noteInfo.noteId, noteInfo.pairNote);
						return;
					case NoteResult.Good:
						PlaySE(LiveSoundDefine.SE_LIVE_GOOD, category, noteInfo.noteId, noteInfo.pairNote);
						return;
					case NoteResult.Great:
						PlaySE(LiveSoundDefine.SE_LIVE_GREAT, category, noteInfo.noteId, noteInfo.pairNote);
						return;
					default:
						return;
				}
			}

			PlaySE(mapped, category, noteInfo.noteId, noteInfo.pairNote);
		}

		private string GetCollaborationSECategory(NoteInfo noteInfo)
		{
			return noteInfo.type == NoteType.Critical ? COLLABORATION_SE_CATEGORY_2 : COLLABORATION_SE_CATEGORY_1;
		}

		private void PlayHaptic()
		{
			if (!isEnableVibration)
			{
				return;
			}
		}

		private void PlaySE(string seName, string category, int myNoteId, INote pairNote)
		{
			if (string.IsNullOrEmpty(seName))
			{
				return;
			}

			category ??= seName;
			if (pairNote == null || pairNoteSeMap == null)
			{
				SoundManager.Instance.PlayIngameSEOneShot(seName);
				return;
			}

			int pairNoteId = pairNote.Id;
			if (pairNoteSeMap.ContainsKey(pairNoteId))
			{
				if (pairNoteSeMap[pairNoteId] != category)
				{
					SoundManager.Instance.PlayIngameSEOneShot(seName);
				}
				pairNoteSeMap.Remove(pairNoteId);
				return;
			}

			if (!pairNoteSeMap.ContainsKey(myNoteId))
			{
				pairNoteSeMap.Add(myNoteId, category);
				SoundManager.Instance.PlayIngameSEOneShot(seName);
				return;
			}

			if (pairNoteSeMap[myNoteId] != category)
			{
				SoundManager.Instance.PlayIngameSEOneShot(seName);
			}
			pairNoteSeMap.Remove(myNoteId);
		}

		private ParticleSystemController LoadTapEffect(string name)
		{
			GameObject prefab = AssetBundleUtility.LoadAsset<GameObject>(LiveConfig.EffectBundleName, name);
			if (prefab == null)
			{
				return null;
			}

			GameObject instance = Instantiate(prefab, transform);
			return instance.AddComponent<ParticleSystemController>();
		}

		public TapEffectView()
		{
			auraPoolDict = new Dictionary<int, EffectPool>();
			genPoolDict = new Dictionary<int, EffectPool>();
			flashPoolDict = new Dictionary<NoteType, EffectPool>();
			connectionPoolDict = new Dictionary<NoteType, EffectPool>();
			frictionPoolDict = new Dictionary<int, EffectPool>();
			tapSEDict = new Dictionary<int, string>();
			pairNoteSeMap = new Dictionary<int, string>();
			lastTapLanes = new int[10];
			isEnableVibration = true;
		}

		static TapEffectView()
		{
			LaneCount = LiveUtility.LaneCount + 1;
		}

		private static int GetTapSeKey(NoteCategory category, NoteType type)
		{
			return (int)category + 10 * (int)type;
		}

		private static int GetTapEffectKey(NoteCategory category, NoteType type, NoteResult result)
		{
			return (int)category + 10 * (int)type + 100 * (int)result;
		}

		private EffectPool CreateEffectPool(string effectName, int poolCount)
		{
			GameObject holder = new GameObject(effectName, typeof(EffectPool));
			EffectPool pool = holder.GetComponent<EffectPool>();
			holder.transform.SetParent(transform, false);
			GameObject prefab = AssetBundleUtility.LoadAsset<GameObject>(LiveConfig.EffectBundleName, effectName);
			pool.Setup(prefab, poolCount);
			return pool;
		}

		private EffectPool GetTapEffectPool(Dictionary<int, EffectPool> dictionary)
		{
			if (dictionary == null)
			{
				return null;
			}

			int key = GetTapEffectKey(noteInfo.category, noteInfo.type, noteInfo.result);
			if (dictionary.TryGetValue(key, out EffectPool pool))
			{
				return pool;
			}

			key = GetTapEffectKey(noteInfo.category, noteInfo.type, NoteResult.Perfect);
			return dictionary.TryGetValue(key, out pool) ? pool : null;
		}

		private float GetLaneCenter()
		{
			return (noteInfo.laneEnd - noteInfo.laneStart) * 0.5f + noteInfo.laneStart;
		}
	}
}
