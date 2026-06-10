/****************************************************************************
 *
 * Copyright (c) 2019 CRI Middleware Co., Ltd.
 *
 ****************************************************************************/

#if UNITY_2018_1_OR_NEWER && CRIWARE_TIMELINE_1_OR_NEWER

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CriWare {

namespace CriTimeline.Atom
{
	[Serializable]
	public class CriAtomMixerBehaviour : PlayableBehaviour {
		internal PlayableDirector m_Director;
		internal TimelineClip[] m_Clips;
		internal CriAtomSourceBase m_Bind;
		internal string m_AisacControls;
		internal bool m_StopOnWrapping;
		internal bool m_StopAtGraphEnd;
		internal bool m_ApplyPlayableSpeed;
		internal bool m_CheckPosWithinClip;

		public Guid m_Guid { get; private set; }

		private const int cScratchTimeIntervalMs = 200;
		private const double cFrameSkipTolerance = double.Epsilon;
		private DateTime m_lastScrubTime;
		private double m_lastDirectorTime = 0;
		private CriAtomListener previewSelectedListenerObj = null;

		public override void OnPlayableCreate(Playable playable) {
			base.OnPlayableCreate(playable);
			m_Guid = Guid.NewGuid();
			m_lastDirectorTime = 0;
		}

		public override void OnPlayableDestroy(Playable playable) {
			base.OnPlayableDestroy(playable);
			if (IsEditor) {
				if (CriAtomTimelinePreviewer.IsInitialized) {
					CriAtomTimelinePreviewer.InstanceDispose();
				}
			}
			m_lastDirectorTime = 0;
		}

		public override void OnGraphStop(Playable playable) {
			base.OnGraphStop(playable);

			if (IsEditor) {
				if (CriAtomTimelinePreviewer.IsInitialized) {
					CriAtomTimelinePreviewer.Instance.StopAllTracks();
				}
			} else {
				if (m_Bind != null && this.m_StopAtGraphEnd) {
					m_Bind.Stop();
				}
			}
			m_lastDirectorTime = 0;
		}

		public override void ProcessFrame(Playable playable, FrameData info, object playerData) {
			if (IsEditor) {
				if (CriAtomPlugin.IsLibraryInitialized() == false) {
					CriAtomTimelinePreviewer.InstanceDispose();

					CriWareInitializer criInitializer = null;
#if UNITY_2023_1_OR_NEWER
					criInitializer = GameObject.FindAnyObjectByType<CriWareInitializer>();
#else
					criInitializer = GameObject.FindObjectOfType<CriWareInitializer>();
#endif
					if (criInitializer != null) {
						CriWareInitializer.InitializeAtom(criInitializer.atomConfig);
					} else {
						CriWareInitializer.InitializeAtom(new CriAtomConfig());
						Debug.Log("[CRIWARE] Timeline / Atom: Can't find CriWareInitializer component; Using default parameters in edit mode.");
					}
					CriAtomTimelinePreviewer.Instance.InitPreviewListenerList(
#if UNITY_2023_1_OR_NEWER
						GameObject.FindObjectsByType<CriAtomListener>(FindObjectsSortMode.None)
#else
						GameObject.FindObjectsOfType<CriAtomListener>()
#endif
					);
				}
			}

			if (playerData == null || m_Director.time == m_lastDirectorTime) { return; }

			if (IsEditor) {
				CriAtomTimelinePreviewer.Instance.Update3dTransform(m_Guid, m_Bind.gameObject.transform, info.deltaTime);
#if UNITY_EDITOR
				this.previewSelectedListenerObj = Selection.activeGameObject?.GetComponent<CriAtomListener>();
#endif
				CriAtomTimelinePreviewer.Instance.UpdateAllListeners(m_Guid, info.deltaTime, previewSelectedListenerObj);
			}

			bool frameSkipped = m_Director.time - m_lastDirectorTime > info.deltaTime + cFrameSkipTolerance;
			bool wrapped = m_Director.state == PlayState.Playing && m_Director.time < m_lastDirectorTime;
			m_lastDirectorTime = m_Director.time;

			float rootPlayableSpeed = 1f;
			if (m_ApplyPlayableSpeed) { rootPlayableSpeed = Convert.ToSingle(m_Director.playableGraph.GetRootPlayable(0).GetSpeed()); }
			
			int inputPort = 0;
			foreach (var clip in m_Clips) {
				ScriptPlayable<CriAtomBehaviour> inputPlayable = (ScriptPlayable<CriAtomBehaviour>)playable.GetInput(inputPort);
				CriAtomBehaviour clipBehaviour = inputPlayable.GetBehaviour();
				CriAtomClipBase criAtomClip = clip.asset as CriAtomClipBase;
				float inputWeight = criAtomClip.ignoreBlend ? 1f : playable.GetInputWeight(inputPort);

				if (clipBehaviour != null) {
					if (m_StopOnWrapping && wrapped || m_CheckPosWithinClip && frameSkipped) {
						if (clipBehaviour.IsClipPlaying) {
							clipBehaviour.Stop(criAtomClip.stopWithoutRelease);
						}
					}

					if (m_Director.time > clip.end ||
						m_Director.time < clip.start) {
						if (clipBehaviour.IsClipPlaying) {
							clipBehaviour.Stop(criAtomClip.stopWithoutRelease, criAtomClip.stopAtClipEnd == false);
						}
					} else if (criAtomClip.muted == false) {
						var speedmultiplyer = Math.Pow(2, clipBehaviour.pitch/1200f) * rootPlayableSpeed;
						long seekTimeMs = (long)((m_Director.time + clip.clipIn - clip.start ) * 1000.0 * speedmultiplyer);
						bool isDirectorPaused = (m_Director.state == PlayState.Paused);

						var playConfig = new CriAtomClipPlayConfig(
							criAtomClip,
							seekTimeMs,
							clip.timeScale,
							criAtomClip.loopWithinClip
						);

						if (clipBehaviour.IsClipPlaying == false) { /* Entering clip for the first time */
							if (IsEditor == false) {
								clipBehaviour.Play(m_Bind, playConfig);
							} else {
								clipBehaviour.PreviewPlay(m_Guid, isDirectorPaused, playConfig);
								m_lastScrubTime = DateTime.Now;
							}
							criAtomClip.SetClipDuration(clipBehaviour.CueLength);
						} else {
							var now = DateTime.Now;
							if (IsEditor == true && isDirectorPaused &&
								now - m_lastScrubTime > new TimeSpan(0, 0, 0, 0, cScratchTimeIntervalMs)) { /* Scrubing the track */
								clipBehaviour.Stop(true);
								clipBehaviour.PreviewPlay(m_Guid, isDirectorPaused, playConfig);
								m_lastScrubTime = now;
							}
						}

						if (IsEditor == true) {
							CriAtomTimelinePreviewer.Instance.SetVolume(m_Guid, clipBehaviour.volume * inputWeight);
							CriAtomTimelinePreviewer.Instance.SetPitch(m_Guid, clipBehaviour.pitch);
							if (string.IsNullOrEmpty(m_AisacControls) == false) {
								CriAtomTimelinePreviewer.Instance.SetAISAC(m_Guid, m_AisacControls, clipBehaviour.AISACValue);
							}
							CriAtomTimelinePreviewer.Instance.PlayerUpdateParameter(m_Guid, clipBehaviour.playback);
							CriAtomTimelinePreviewer.Instance.UpdateTimelineExtension(m_Bind, m_Guid);
						} else {
							m_Bind.player.SetVolume(clipBehaviour.volume * inputWeight);
 							if (m_ApplyPlayableSpeed) {
								float clipSpeed = rootPlayableSpeed * (float)clip.timeScale;
								float stretchSpeed = Mathf.Clamp(clipSpeed, 0.5f, 2.0f);
								m_Bind.player.SetDspTimeStretchRatio(1f / stretchSpeed);
								float targetPitch = 1200 * (Mathf.Log(clipSpeed / stretchSpeed) / Mathf.Log(2f));
								m_Bind.player.SetPitch(targetPitch + clipBehaviour.pitch);
							} else {
								m_Bind.player.SetPitch(clipBehaviour.pitch);
							}
							if (string.IsNullOrEmpty(m_AisacControls) == false) {
								m_Bind.player.SetAisacControl(m_AisacControls, clipBehaviour.AISACValue);
							}
							m_Bind.player.Update(clipBehaviour.playback);
						}
					}
				}

				inputPort++;
			}
		}

		static private bool IsEditor {
			get {
#if UNITY_EDITOR
				if (UnityEditor.EditorApplication.isPlaying == false) {
					return true;
				}
#endif
				return false;
			}
		}
	}

	public partial class CriAtomTimelinePreviewer : IDisposable {
		static private CriAtomTimelinePreviewer instance = null;
		static public CriAtomTimelinePreviewer Instance {
			get {
				if (instance == null) {
					instance = new CriAtomTimelinePreviewer();
#if UNITY_EDITOR
					UnityEditor.EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
#endif
				}
				return instance;
			}
		}
		static public void InstanceDispose() {
			if (instance != null) {
				instance.Dispose();
				instance = null;
#if UNITY_EDITOR
				UnityEditor.EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
#endif
			}
		}
		static public bool IsInitialized {
			get {
				if (instance == null) {
					return false;
				}
				return true;
			}
		}

#if UNITY_EDITOR
		static void OnPlayModeStateChanged(UnityEditor.PlayModeStateChange change)
		{
			CriAtomTimelinePreviewerExtensionManager.Instance.InstanceDispose();
			if (change == UnityEditor.PlayModeStateChange.EnteredEditMode)
				InstanceDispose();
		}
#endif

		class PlayerSource : IDisposable {
			public readonly CriAtomExPlayer player;
			public readonly CriAtomEx3dSource source3d;
			private Vector3? lastPos;
			private bool disposed = false;

			public PlayerSource() {
				this.player = new CriAtomExPlayer();
				this.source3d = new CriAtomEx3dSource();
				this.lastPos = null;

				this.player.Set3dSource(this.source3d);
				this.player.UpdateAll();
			}

			~PlayerSource() {
				this.dispose();
			}

			public void Dispose() {
				this.dispose();
				GC.SuppressFinalize(this);
			}

			private void dispose() {
				if (disposed == false) {
					this.player.Dispose();
					this.source3d.Dispose();
				}
				disposed = true;
			}

			public void Set3dTransform(Vector3 pos, Vector3 forward, Vector3 up, float deltaTime) {
				this.source3d.SetPosition(pos.x, pos.y, pos.z);
				if (lastPos.HasValue == false) {
					this.lastPos = pos;
				} else if (deltaTime > 0) {
					var vel = (pos - lastPos.Value) / deltaTime;
					this.source3d.SetVelocity(vel.x, vel.y, vel.z);
					this.lastPos = pos;
				}
				this.source3d.SetOrientation(forward, up);
				this.source3d.Update();
			}

			public void ClearLastPos() {
				this.lastPos = null;
			}
		}

		class PreviewListener : IDisposable {
			public readonly CriAtomEx3dListener listener;
			public readonly CriAtomListener transformObj;
			private Vector3? lastPos;
			private bool disposed = false;
			
			public PreviewListener(CriAtomListener listenerObj) {
				this.listener = new CriAtomEx3dListener();
				this.transformObj = listenerObj;
				this.lastPos = null;
			}

			~PreviewListener() { 
				this.dispose();
			}

			public void Dispose() { 
				this.dispose();
				GC.SuppressFinalize(this);
			}

			private void dispose() {
				if (disposed == false) {
					this.listener.Dispose();
				}
				disposed = true;
			}

			public void Set3dTransform(float deltaTime) {
				if (listener == null || transformObj == null) { return; }

				var transform = transformObj.transform;
				listener.SetPosition(transform.position.x, transform.position.y, transform.position.z);
				if (lastPos.HasValue == false) {
					lastPos = transform.position;
				} else if (deltaTime > 0) {
					var vel = (transform.position - lastPos.Value) / deltaTime;
					listener.SetVelocity(vel.x, vel.y, vel.z);
					lastPos = transform.position;
				}
				listener.SetOrientation(
					transform.forward.x,
					transform.forward.y,
					transform.forward.z,
					transform.up.x,
					transform.up.y,
					transform.up.z
				);
				listener.Update();
			}

			public void Exile() {
				listener.SetPosition(float.MaxValue, float.MaxValue, float.MaxValue);
				listener.Update();
			}

			public void ClearLastPos() {
				this.lastPos = null;
			}
		}

		private CriAtom atom;
		private string lastAcfFile = "";
		private Dictionary<string, CriAtomExAcb> acbTable;
		private Dictionary<Guid, PlayerSource> playerTable;  /* preview player for each track */
		private Dictionary<Guid, PreviewListener> listenerTable;
		private List< KeyValuePair<Guid, PreviewListener> > listenerPurgeList;
		private Guid? trackIdForListenerUpdate = null;

		public CriAtomTimelinePreviewer() {
			this.acbTable = new Dictionary<string, CriAtomExAcb>();
			this.playerTable = new Dictionary<Guid, PlayerSource>();
			this.listenerTable = new Dictionary<Guid, PreviewListener>();
			this.listenerPurgeList = new List< KeyValuePair<Guid, PreviewListener> >();
		}

		PlayerSource GetPlayer(Guid trackId) {
			if (this.playerTable.ContainsKey(trackId)) {
				return this.playerTable[trackId];
			} else {
				PlayerSource playerSource = new PlayerSource();
				try {
					this.playerTable.Add(trackId, playerSource);
				} catch (Exception e) { /* impossible */
					Debug.LogError("[CRIWARE] Timeline Previewer: Failed adding preview player (" + e.Message + ")");
				}
				return playerSource; /* return the created player anyway */
			}
		}

		public void Update3dTransform(Guid trackId, Transform transform, float deltaTime) {
			GetPlayer(trackId).Set3dTransform(transform.position, transform.forward, transform.up, deltaTime);
		}

		public void InitPreviewListenerList(CriAtomListener[] listenerList) {
			foreach (var elem in listenerTable.Values) {
				elem.Dispose();
			}
			listenerTable.Clear();

			if (listenerList != null && listenerList.Length > 0) {
				CriAtomListener.DummyNativeListener?.SetPosition(float.MaxValue, float.MaxValue, float.MaxValue);
				CriAtomListener.DummyNativeListener?.Update();
				for (int i = 0; i < listenerList.Length; ++i) {
					if(!listenerTable.ContainsKey(listenerList[i].guid))
						listenerTable.Add(listenerList[i].guid, new PreviewListener(listenerList[i]));
				}
			}
		}

		public void UpdateAllListeners(Guid trackId, float deltaTime, CriAtomListener exclusiveObj) {
			if (trackIdForListenerUpdate.HasValue == false) {
				trackIdForListenerUpdate = trackId;
			} else if (trackIdForListenerUpdate != trackId) {
				return; /* only do update on the first CRI Atom track */
			}

			if (exclusiveObj != null && listenerTable.ContainsKey(exclusiveObj.guid) == false) { /* add newly added listener */
				listenerTable.Add(exclusiveObj.guid, new PreviewListener(exclusiveObj));
			}
				
			listenerPurgeList.Clear();

			foreach (var elem in listenerTable) {
				if (elem.Value.transformObj == null) {  /* record unused listeners */
					listenerPurgeList.Add(elem);
					continue;
				}

				if (exclusiveObj != null) {
					if (elem.Key == exclusiveObj.guid) {
						elem.Value.Set3dTransform(deltaTime);
					} else {
						elem.Value.Exile();
					}
				} else {
					elem.Value.Set3dTransform(deltaTime);
				}
			}
			
			foreach (var elem in listenerPurgeList) { /* do purging */
				elem.Value.Dispose();
				listenerTable.Remove(elem.Key);
			}

			if (listenerTable.Count <= 0) { /* use default when no other listener exists */
				CriAtomListener.DummyNativeListener?.SetPosition(0, 0, 0);
				CriAtomListener.DummyNativeListener?.Update();
			} else { /* exile default listener */
				CriAtomListener.DummyNativeListener?.SetPosition(float.MaxValue, float.MaxValue, float.MaxValue);
				CriAtomListener.DummyNativeListener?.Update();
			}
		}

		public void SetCue(Guid trackId, CriAtomExAcb acb, string cueName) {
			if (acb != null && string.IsNullOrEmpty(cueName) == false) {
				this.GetPlayer(trackId).player.SetCue(acb, cueName);
			} else {
				Debug.LogWarning("[CRIWARE] Timeline Previewer: insufficient ACB or cue name");
			}
		}

		public CriAtomExAcb GetAcb(string acbPath, string awbPath) {
			if (string.IsNullOrEmpty(acbPath)) {
				Debug.LogWarning("[CRIWARE] Timeline Previewer: cuesheet path is vacant");
				return null;
			}

#if UNITY_2023_1_OR_NEWER
			this.atom = UnityEngine.Object.FindAnyObjectByType<CriAtom>();
#else
			this.atom = UnityEngine.Object.FindObjectOfType<CriAtom>();
#endif
			if (this.atom != null)
			{
				if (lastAcfFile != this.atom.acfFile) {
					CriAtomEx.UnregisterAcf();
					CriAtomEx.RegisterAcf(null, Path.Combine(CriWare.Common.streamingAssetsPath, atom.acfFile));
					lastAcfFile = this.atom.acfFile;
				}
			}

			if (this.acbTable.ContainsKey(acbPath)) {
				return acbTable[acbPath];
			} else {
				CriAtomExAcb acb = CriAtomExAcb.LoadAcbFile(null, acbPath, awbPath);
				if (acb != null) {
					try {
						acbTable.Add(acbPath, acb);
					} catch (Exception e) {
						if (e is ArgumentException) {
							/* impossible */
							Debug.LogWarning("[CRIWARE] Timeline Previewer: ACB already existing.");
						} else {
							Debug.LogWarning("[CRIWARE] Timeline Previewer: ACB Dictionary exception: " + e.Message);
						}
					}
				} else {
					Debug.LogWarning("[CRIWARE] Timeline Previewer: Failed loading ACB/AWB file.");
				}
				return acb;
			}
		}

		public CriAtomExPlayback Play(Guid trackId) {
			var playerSrc = this.GetPlayer(trackId);
			playerSrc.ClearLastPos();
			return playerSrc.player.Start();
		}

		public void StopTrack(Guid trackId, bool stopWithoutRelease = true) {
			if (stopWithoutRelease) {
				this.GetPlayer(trackId).player.StopWithoutReleaseTime();
			} else {
				this.GetPlayer(trackId).player.Stop();
			}
		}

		public void StopAllTracks(bool stopWithoutRelease = true) {
			foreach (var elem in playerTable) {
				if (stopWithoutRelease) {
					elem.Value.player.StopWithoutReleaseTime();
				} else {
					elem.Value.player.Stop();
				}
			}
		}

		public void SetStartTime(Guid trackId, long startTimeMs) {
			this.GetPlayer(trackId).player.SetStartTime(startTimeMs);
		}

		public void SetLoop(Guid trackId, bool sw) {
			this.GetPlayer(trackId).player.Loop(sw);
		}

		public void SetVolume(Guid trackId, float volume) {
			this.GetPlayer(trackId).player.SetVolume(volume);
		}

		public void SetPitch(Guid trackId, float pitch) {
			this.GetPlayer(trackId).player.SetPitch(pitch);
		}

		public void SetAISAC(Guid trackId, string controlName, float value) {
			this.GetPlayer(trackId).player.SetAisacControl(controlName, value);
		}

		public void PlayerUpdateParameter(Guid trackId, CriAtomExPlayback atomExPlayback) {
			this.GetPlayer(trackId).player.Update(atomExPlayback);
		}

		public void UpdateTimelineExtension(CriAtomSourceBase bindObject, Guid trackGuid) {
			CriAtomExPlayer previewPlayer = GetPlayer(trackGuid).player;
#if UNITY_EDITOR
			CriAtomTimelinePreviewerExtensionManager.Instance.UpdateExtensions(bindObject, trackGuid, previewPlayer);
#endif
		}

		~CriAtomTimelinePreviewer() {
			this.Dispose(false);
		}

		public void Dispose() {
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing) {
			foreach (var elem in playerTable.Values) {
				elem.Dispose();
			}
			playerTable.Clear();

			foreach (var elem in acbTable.Values) {
				elem.Dispose();
			}
			acbTable.Clear();

			foreach (var elem in listenerTable.Values) {
				elem.Dispose();
			}
			listenerTable.Clear();
			listenerPurgeList.Clear();
		}
	}
}

} //namespace CriWare

#endif