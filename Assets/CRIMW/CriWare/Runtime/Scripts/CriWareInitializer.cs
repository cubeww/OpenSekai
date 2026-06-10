/****************************************************************************
 *
 * Copyright (c) 2012 CRI Middleware Co., Ltd.
 *
 ****************************************************************************/

using UnityEngine;
using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Collections.Generic;

namespace CriWare {

/**
 * <summary>Parameters for initializing CRI File System</summary>
 */
[System.Serializable]
public class CriFsConfig {
	/** Default reading performance of the device (bps) */
	public const int defaultAndroidDeviceReadBitrate = 50000000;

	/** Number of the loaders */
	public int numberOfLoaders    = 16;
	/** Number of binders */
	public int numberOfBinders    = 8;
	/** Number of installers */
	public int numberOfInstallers = 2;
	/** Install buffer size */
	public int installBufferSize  = CriFsPlugin.defaultInstallBufferSize / 1024;
	/** Maximum length of the path */
	public int maxPath            = 256;
	/** User agent string */
	public string userAgentString = "";
	/** Flag of the file descriptor saving mode */
	public bool minimizeFileDescriptorUsage = false;
	/** Whether to do CRC check on the CPK file */
	public bool enableCrcCheck = false;
	/** [Android] Device reading performance (bps) */
	public int androidDeviceReadBitrate = defaultAndroidDeviceReadBitrate;

}

/**
 * <summary>Parameters for initializing CRI Atom</summary>
 */
[System.Serializable]
public class CriAtomConfig {
	/**
	 * <summary>ACF file name</summary>
	 * <remarks>
	 * <para header='Note'>The ACF file need to be placed in the StreamingAssets folder.</para>
	 * </remarks>
	 */
	public string acfFileName = "";

	/** Parameters for creating standard Voice Pool */
	[System.Serializable]
	public class StandardVoicePoolConfig {
		public int memoryVoices    = 16;
		public int streamingVoices = 8;
	}

	/** HCA-MX Voice Pool creation parameters */
	[System.Serializable]
	public class HcaMxVoicePoolConfig {
		public int memoryVoices    = 0;
		public int streamingVoices = 0;
	}

	/** In-Game Preview settings */
	[System.Serializable]
	public enum InGamePreviewSwitchMode {
		Disable,                /** Disabled */
		Enable,                 /** Enabled */
		FollowBuildSetting,     /** Available for Development Build only */
		Default                 /** "usesInGamePreview" */
	}

	/** In-Game Preview parameters */
	[System.Serializable]
	public class InGamePreviewConfig {
		/** Maximum number of preview objects */
		public int maxPreviewObjects                = 200;
		/** Communication buffer size (KiB) */
		public int communicationBufferSize          = 2048;
		/** Interval at which the playback position is updated (number of server processing calls) */
		public int playbackPositionUpdateInterval   = 8;
	}

	/** Maximum number of virtual Voices */
	public int maxVirtualVoices = 32;
	/** Maximum number of Voice limit groups */
	public int maxVoiceLimitGroups = 32;
	/** Maximum number of categories */
	public int maxCategories = 32;
	/** Maximum number of AISACs */
	public int maxAisacs = 8;
	/** Maximum number of bus sends */
	public int maxBusSends = 8;
	/** Maximum number of sequence events in each frame */
	public int maxSequenceEventsPerFrame = 2;
	/** Maximum number of beat-synchronization callbacks in each frame */
	public int maxBeatSyncCallbacksPerFrame = 1;
	/** Maximum number of Cue Link callbacks per frame */
	public int maxCueLinkCallbacksPerFrame = 1;
	/** Parameters for creating standard Voice Pool */
	public StandardVoicePoolConfig standardVoicePoolConfig = new StandardVoicePoolConfig();
	/** HCA-MX Voice Pool creation parameters */
	public HcaMxVoicePoolConfig hcaMxVoicePoolConfig = new HcaMxVoicePoolConfig();
	/** Output sampling rate */
	public int outputSamplingRate = 0;
	/** Whether to use In-Game Preview */
	public bool usesInGamePreview = false;
	/** In-Game Preview settings (effective only when specified in inspector) */
	public InGamePreviewSwitchMode inGamePreviewMode = InGamePreviewSwitchMode.Default;
	/** In-Game Preview parameters */
	public InGamePreviewConfig inGamePreviewConfig = new InGamePreviewConfig();
	/** Server frequency */
	public float serverFrequency  = 60.0f;
	/** ASR speaker mapping */
	public CriAtom.SpeakerMapping speakerMapping;
	/** The number of ASR output channels */
	public int asrOutputChannels  = 0;
	/** Whether to use time(System.DateTime.Now.Ticks) as random seed */
	public bool useRandomSeedWithTime = true;
	/** Number of category references per playback */
	public int categoriesPerPlayback = 4;
	/** Maximum number of faders */
	public int maxFaders = 4;
	/** Maximum number of buses */
	public int maxBuses = 8;
	/** Maximum pitch change */
	public float maxPitch = 2400f;
	/** Maximum number of parameter blocks */
	public int maxParameterBlocks = 1024;
	/** Whether to use VR sound output mode */
	public CriAtomEx.SoundRendererType soundRendererType = CriAtomEx.SoundRendererType.Native;
	/** Whether to pause the audio output when paused on a StandAlone platform or in the editor */
	public bool keepPlayingSoundOnPause = true;
	/** Whether to Enable SonicSYNC (excluding Android, iOS and Switch) */
	public bool enableSonicSync = true;
	/** Whether the audio output will be disabled or not (except on Linux) */
	public bool enableAtomSoundDisabledMode = false;
	/** Whether the audio output will be disabled or not (on Linux) */
	public bool enableAtomSoundDisabledModeLinux = false;

	/** [Editor] Parameters related to the User PCM Output Mode */
	[System.Serializable]
	public class EditorPcmOutputConfig
	{
		public bool enable = false;
		public int bufferLength = 4096;
	}
	/** [Editor] Parameters related to the User PCM Output Mode */
	public EditorPcmOutputConfig editorPcmOutputConfig = new EditorPcmOutputConfig();

	/** [PC] Output buffering time */
	public int pcBufferingTime = 0;
	/** [PC] Microsoft Spatial Sound enable flag */
	public bool useMicrosoftSpatialSound = true;


	/** [iOS] Enables SonicSYNC mode */
	public bool iosEnableSonicSync = true;
	/** [iOS] Output buffering time (milliseconds) */
	public int  iosBufferingTime     = 50;
	/** [iOS] Whether to override iPod playback */
	public bool iosOverrideIPodMusic = false;
	/** [iOS] Whether to suspend and resume the audio system in response to an audio interruption notification from the OS */
	public bool iosEnableOSNotificationHandling = true;

	/** [Android] Enables SonicSYNC Mode */
	public bool androidEnableSonicSync = true;
	/** [Android] ASR (normal playback) output buffering time */
	public int androidBufferingTime      = 133;
	/** [Android] NSR (Low Delay Playback) Playback Start Buffering Time */
	public int androidStartBufferingTime = 100;

	/** [Android] Parameters for creating low latency playback Voice Pool */
	[System.Serializable]
	public class AndroidLowLatencyStandardVoicePoolConfig {
		public int memoryVoices    = 0;
		public int streamingVoices = 0;
	}
	/** [Android] Parameters for creating low latency playback Voice Pool */
	public AndroidLowLatencyStandardVoicePoolConfig androidLowLatencyStandardVoicePoolConfig = new AndroidLowLatencyStandardVoicePoolConfig();
	/** [Android] Whether to use the Fast Mixer of Android to reduce the output delay during audio playback. Affects the output delay of ASR/NSR and the result of delay estimation function */
	public bool androidUsesAndroidFastMixer = true;
	/** [Android] Force CriAtomSource to use ASR for playback when low lantency playback is enabled */
	public bool androidForceToUseAsrForDefaultPlayback = true;
	/** [Android] Beta: Enable AAudio or not */
	public bool androidUsesAAudio = false;
	/** [Android] Stream type setting */
	public int androidStreamType = 0;

}

/**
 * <summary>Parameters for initializing CRI Mana</summary>
 */
[System.Serializable]
public class CriManaConfig {
	/** Number of decoders */
	public int  numberOfDecoders   = 8;
	/** Number of seamless playback entries */
	public int  numberOfMaxEntries = 4;
	/** Whether to enable multi-threaded texture drawing processing using GL.IssuePluginEvent */
	public readonly bool graphicsMultiThreaded = true; // always true.
	/** Flag indicating whether the streaming management module is used */
	public bool useStreamerManager = false;

}

} //namespace CriWare

/**
 * \addtogroup CRIWARE_UNITY_COMPONENT
 * @{
 */

namespace CriWare {

/**
 * <summary>CRIWARE initialization component</summary>
 * <remarks>
 * <para header='Description'>This component is used to initialize the CRIWARE library.<br/></para>
 * </remarks>
 */
[AddComponentMenu("CRIWARE/Library Initializer")]
public class CriWareInitializer : CriMonoBehaviour {

	/** Whether to initialize the CRI File System library */
	public bool initializesFileSystem = true;

	/** Initialization configuration of the CRI File System library */
	public CriFsConfig fileSystemConfig = new CriFsConfig();

	/** Whether to initialize the CRI Atom library */
	public bool initializesAtom = true;

	/** Initialization configuration of the CRI Atom library */
	public CriAtomConfig atomConfig = new CriAtomConfig();

	/** Whether to initialize the CRI Mana library */
	public bool initializesMana = true;

	/** Initialization configuration of the CRI Mana library */
	public CriManaConfig manaConfig = new CriManaConfig();



	/** Whether to initialize the library on Awake */
	public bool dontInitializeOnAwake = false;

	/** Whether to finalize the library at scene change */
	public bool dontDestroyOnLoad = false;

	/* オブジェクト作成時の処理 */
	void Awake() {
		/* 現在のランタイムのバージョンが正しいかチェック */
		CriWare.Common.CheckBinaryVersionCompatibility();

		if (dontInitializeOnAwake) {
			/* フラグが立っていた場合はAwakeでは初期化を行わない */
			return;
		}

		/* プラグインの初期化 */
		this.Initialize();
	}

	/* Execution Order の設定を確実に有効にするために OnEnable をオーバーライド */
	protected override void OnEnable() {
		base.OnEnable();
	}

	void Start () { }

	void OnDestroy() {
		Shutdown();
	}

#if !UNITY_EDITOR && UNITY_IOS
	static int frameCnt = 0;
#endif
	public override void CriInternalUpdate() {
#if !UNITY_EDITOR && UNITY_IOS
		if (frameCnt > 3) {
			return;
		}
		frameCnt++;
		if (frameCnt == 2) {
			CriAtomPlugin.Pause(true);
		} else if (frameCnt == 3) {
			CriAtomPlugin.Pause(false);
		}
#endif
	}

	public override void CriInternalLateUpdate() { }

	/**
	 * <summary>Initializes the plug-in (for manual initialization)</summary>
	 * <remarks>
	 * <para header='Description'>Initializes the plug-in.<br/>
	 * By default, this function is automatically called in the Awake function, so the application does not need to call it directly.<br/>
	 * <br/>
	 * Use this function if you want to dynamically change the initialization parameters from the script.<br/></para>
	 * <para header='Note'>When using this function, check the CriWare.CriWareInitializer::dontInitializeOnAwake
	 * property on the Inspector to disable the automatic initialization.<br/>
	 * In addition, this function must be called before any plug-in APIs.
	 * Call it in a script with a higher Script Execution Order.</para>
	 * </remarks>
	 *
	 */
	public void Initialize() {
		/* 初期化カウンタの更新 */
		initializationCount++;
		if (initializationCount != 1) {
			/* CriWareInitializer自身による多重初期化は許可しない */
			GameObject.Destroy(this);
			return;
		}

		ValidateConfig();

		/* 非実行時にライブラリ機能を使用していた場合は一度終了処理を行う */
		if ((CriFsPlugin.IsLibraryInitialized() == true && CriAtomPlugin.IsLibraryInitialized() == true && CriManaPlugin.IsLibraryInitialized() == true) ||
			(CriFsPlugin.IsLibraryInitialized() == true && CriAtomPlugin.IsLibraryInitialized() == true && CriManaPlugin.IsLibraryInitialized() == false) ||
			(CriFsPlugin.IsLibraryInitialized() == true && CriAtomPlugin.IsLibraryInitialized() == false && CriManaPlugin.IsLibraryInitialized() == false)) {
#if UNITY_EDITOR || (!UNITY_PS3)
			/* CRI Manaライブラリの終了 */
			if (initializesMana) {
				CriManaPlugin.FinalizeLibrary();
			}
#endif

			/* CRI Atomライブラリの終了 */
			if (initializesAtom) {
#pragma warning disable CS0618 // CriAtomExLatencyEstimator is obsoleted.
				/* EstimatorがStop状態になるまでFinalize */
				while (CriAtomExLatencyEstimator.GetCurrentInfo().status != CriAtomExLatencyEstimator.Status.Stop) {
					CriAtomExLatencyEstimator.FinalizeModule();
				}
#pragma warning restore CS0618

				/* 終了処理の実行 */
				CriAtomPlugin.FinalizeLibrary();
			}

			/* CRI File Systemライブラリの終了 */
			if (initializesFileSystem) {
				CriFsPlugin.FinalizeLibrary();
			}
		}

		/* CRI File Systemライブラリの初期化 */
		if (initializesFileSystem) {
			InitializeFileSystem(fileSystemConfig);
		}


		/* CRI Atomライブラリの初期化 */
		if (initializesAtom) {
			switch (atomConfig.inGamePreviewMode) {
				case CriAtomConfig.InGamePreviewSwitchMode.Disable:
					atomConfig.usesInGamePreview = false;
					break;
				case CriAtomConfig.InGamePreviewSwitchMode.Enable:
					atomConfig.usesInGamePreview = true;
					break;
				case CriAtomConfig.InGamePreviewSwitchMode.FollowBuildSetting:
					atomConfig.usesInGamePreview = UnityEngine.Debug.isDebugBuild;
					break;
				default:
					/* 既に設定されたフラグに従う */
					break;
			}
			InitializeAtom(atomConfig);
		}


		/* シーンチェンジ後もオブジェクトを維持するかどうかの設定 */
		if (dontDestroyOnLoad) {
			DontDestroyOnLoad(transform.gameObject);
		}
	}

	/**
	 * <summary>Terminates the plug-in (for manual termination)</summary>
	 * <remarks>
	 * <para header='Description'>Exits the plug-in.<br/>
	 * By default, this function is automatically called in the OnDestroy function, so the application does not need to call it directly.</para>
	 * </remarks>
	 */
	public void Shutdown() {
		/* 初期化カウンタの更新 */
		initializationCount--;
		if (initializationCount != 0) {
			initializationCount = initializationCount < 0 ? 0 : initializationCount;
			return;
		}


		/* CRI Atomライブラリの終了 */
		if (initializesAtom) {
#pragma warning disable CS0618 // CriAtomExLatencyEstimator is obsoleted.
			/* EstimatorがStop状態になるまでFinalize */
			while (CriAtomExLatencyEstimator.GetCurrentInfo().status != CriAtomExLatencyEstimator.Status.Stop) {
				CriAtomExLatencyEstimator.FinalizeModule();
			}
#pragma warning restore CS0618 // CriAtomExLatencyEstimator is obsoleted.

			/* 終了処理の実行 */
			CriAtomPlugin.FinalizeLibrary();
		}

		/* CRI File Systemライブラリの終了 */
		if (initializesFileSystem) {
			CriFsPlugin.FinalizeLibrary();
		}
	}

	/* 初期化カウンタ */
	private static int initializationCount = 0;

	/* 初期化実行チェック関数 */
	public static bool IsInitialized() {
		if (initializationCount > 0) {
			return true;
		} else {
			/* 現在のランタイムのバージョンが正しいかチェック */
			CriWare.Common.CheckBinaryVersionCompatibility();
			return false;
		}
	}

	/**
	 * <summary>Registers the interface of a custom effect</summary>
	 * <remarks>
	 * <para header='Description'>A method for registering the interface of the ASR bus effect
	 * (custom effect) implemented by users.
	 * You can create your own ASR bus effect by using the
	 * CRI ADX Audio Effect Plugin SDK.
	 * <br/>
	 * Normally, you can only use the provided effect processing.
	 * By implementing the custom effects library according to the rules defined by CRIWARE,
	 * users can prepare the custom effects interface for CRIWAER Unity Plug-in.
	 * <br/>
	 * By registering the pointer to this interface with the CRIWAER Unity Plug-in using this function,
	 * the custom effect is enabled when the CRI library is initialized.
	 * <br/>
	 * Note that the registered custom effects is forcibly unregistered when the CRI library is finalized.
	 * When initializing the CRI library again, call this function again
	 * to register the custom effect interface.</para>
	 * <para header='Note'>Be sure to call this function before initializing the CRI library.
	 * The custom effect interface added by this function is actually enabled in the
	 * initialization process of the CRI library.</para>
	 * </remarks>
	 */
	static public void AddAudioEffectInterface(IntPtr effect_interface)
	{
		List<IntPtr> effect_interface_list = null;
		if (CriAtomPlugin.GetAudioEffectInterfaceList(out effect_interface_list))
		{
			effect_interface_list.Add(effect_interface);
		}
	}

	public static bool InitializeFileSystem(CriFsConfig config)
	{
		/* CRI File Systemライブラリの初期化 */
		if (!CriFsPlugin.IsLibraryInitialized()) {
			CriFsPlugin.SetConfigParameters(
				config.numberOfLoaders,
				config.numberOfBinders,
				config.numberOfInstallers,
				(config.installBufferSize * 1024),
				config.maxPath,
				config.minimizeFileDescriptorUsage,
				config.enableCrcCheck
				);
			{
				/* Ver.2.03.03 以前は 0 がデフォルト値だったことの互換性維持のための処理 */
				if (config.androidDeviceReadBitrate == 0) {
					config.androidDeviceReadBitrate = CriFsConfig.defaultAndroidDeviceReadBitrate;
				}
			}
			CriFsPlugin.SetConfigAdditionalParameters_ANDROID(config.androidDeviceReadBitrate);
			CriFsPlugin.InitializeLibrary();
			if (config.userAgentString.Length != 0) {
				CriFsUtility.SetUserAgentString(config.userAgentString);
			}
			return true;
		} else {
			return false;
		}
	}

	public static bool InitializeAtom(CriAtomConfig config)
	{
		/* CRI Atomライブラリの初期化 */
		if (CriAtomPlugin.IsLibraryInitialized() == false) {
			/* Sound Disabled Mode プラットフォームチェック*/
			bool enableAtomSoundDisabledModeResult = false;

#if UNITY_EDITOR_LINUX || (!UNITY_EDITOR && UNITY_STANDALONE_LINUX)
			enableAtomSoundDisabledModeResult = config.enableAtomSoundDisabledModeLinux;
#else
			enableAtomSoundDisabledModeResult = config.enableAtomSoundDisabledMode;
#endif

			/* 初期化処理の実行 */
			CriAtomPlugin.SetConfigParameters(
				(int)Math.Max(config.maxVirtualVoices, CriAtomPlugin.GetRequiredMaxVirtualVoices(config)),
				config.maxVoiceLimitGroups,
				config.maxCategories,
				(Byte)config.maxAisacs,
				(Byte)config.maxBusSends,
				config.maxSequenceEventsPerFrame,
				config.maxBeatSyncCallbacksPerFrame,
				config.maxCueLinkCallbacksPerFrame,
				config.standardVoicePoolConfig.memoryVoices,
				config.standardVoicePoolConfig.streamingVoices,
				config.hcaMxVoicePoolConfig.memoryVoices,
				config.hcaMxVoicePoolConfig.streamingVoices,
				config.outputSamplingRate,
				config.asrOutputChannels,
				config.speakerMapping,
				config.usesInGamePreview,
				config.serverFrequency,
				config.maxParameterBlocks,
				config.categoriesPerPlayback,
				config.maxFaders,
				config.maxBuses,
				config.maxPitch,
				config.soundRendererType,
				config.enableSonicSync,
				enableAtomSoundDisabledModeResult);

			CriAtomPlugin.SetConfigMonitorParametes(
				config.inGamePreviewConfig.maxPreviewObjects,
				config.inGamePreviewConfig.communicationBufferSize,
				config.inGamePreviewConfig.playbackPositionUpdateInterval
			);

			CriAtomPlugin.SetConfigAdditionalParameters_EDITOR(
				config.editorPcmOutputConfig.enable,
				config.editorPcmOutputConfig.bufferLength
			);

			CriAtomPlugin.SetConfigAdditionalParameters_PC(
				config.pcBufferingTime,
				config.useMicrosoftSpatialSound
				);


			CriAtomPlugin.SetConfigAdditionalParameters_IOS(
				config.iosEnableSonicSync,
				(uint)Math.Max(config.iosBufferingTime, 16),
				config.iosOverrideIPodMusic,
				config.iosEnableOSNotificationHandling
				);
			/* Android 固有の初期化パラメータを登録 */
			{
				/* Ver.2.03.03 以前は 0 がデフォルト値だったことの互換性維持のための処理 */
				if (config.androidBufferingTime == 0) {
					config.androidBufferingTime = (int)(4 * 1000.0 / config.serverFrequency);
				}
				if (config.androidStartBufferingTime == 0) {
					config.androidStartBufferingTime = (int)(3 * 1000.0 / config.serverFrequency);
				}
#if !UNITY_EDITOR && UNITY_ANDROID
				CriAtomEx.androidDefaultSoundRendererType = config.androidForceToUseAsrForDefaultPlayback ?
					CriAtomEx.SoundRendererType.Asr : CriAtomEx.SoundRendererType.Default;
#endif
				CriAtomPlugin.SetConfigAdditionalParameters_ANDROID(
					config.androidEnableSonicSync,
					config.androidLowLatencyStandardVoicePoolConfig.memoryVoices,
					config.androidLowLatencyStandardVoicePoolConfig.streamingVoices,
					config.androidBufferingTime,
					config.androidStartBufferingTime,
					config.androidUsesAndroidFastMixer,
					config.androidUsesAAudio,
					config.androidStreamType);
			}

			CriAtomPlugin.InitializeLibrary();

			if (config.useRandomSeedWithTime == true){
				/* 時刻を乱数種に設定 */
				CriAtomEx.SetRandomSeed((uint)System.DateTime.Now.Ticks);
			}

			/* ACFファイル指定時は登録 */
			if (config.acfFileName.Length != 0) {
			#if UNITY_WEBGL
				Debug.LogError("In WebGL, ACF File path should be set to CriAtom Component.");
			#else
				string acfPath = config.acfFileName;
				if (CriWare.Common.IsStreamingAssetsPath(acfPath)) {
					acfPath = Path.Combine(CriWare.Common.streamingAssetsPath, acfPath);
				}

				CriAtomEx.RegisterAcf(null, acfPath);
			#endif
			}
			CriAtomServer.KeepPlayingSoundOnPause = config.keepPlayingSoundOnPause;
			return true;
		} else {
			return false;
		}

	}

	public static bool InitializeMana(CriManaConfig config) {
		/* CRI Manaライブラリの初期化 */
		if (CriManaPlugin.IsLibraryInitialized() == false) {
			CriManaPlugin.SetConfigParameters(config.graphicsMultiThreaded, config.numberOfDecoders, config.numberOfMaxEntries);
#if UNITY_EDITOR_WIN || (!UNITY_EDITOR && UNITY_STANDALONE_WIN)
			bool useLegacyDecoder;
#if CRI_MANA_USE_LEGACY_DECODER_PC
			useLegacyDecoder = true;
#else
			useLegacyDecoder = false;
#endif
			CriManaPlugin.UseLegacyDecoder_PC(useLegacyDecoder);
#endif
	
			CriManaPlugin.InitializeLibrary();
#if !(!UNITY_EDITOR && UNITY_WEBGL)
			CriManaPlugin.UseStreamerManager(config.useStreamerManager);
#endif
			return true;
		} else {
			return false;
		}
	}

	private void OnValidate() {
		ValidateConfigEditor();

		ValidateConfig();
	}


	private void ValidateConfigEditor() {
		// for public

	}

	private void ValidateConfig() {
		// for public

	}

} // end of class

} //namespace CriWare
/** @} */

/* --- end of file --- */
