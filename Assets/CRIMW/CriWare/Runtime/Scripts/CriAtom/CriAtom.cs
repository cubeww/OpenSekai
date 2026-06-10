/****************************************************************************
 *
 * Copyright (c) 2011 CRI Middleware Co., Ltd.
 *
 ****************************************************************************/

/*---------------------------
 * Force Load Data with Async Method Defines
 *---------------------------*/
#if UNITY_WEBGL
	#define CRIWARE_FORCE_LOAD_ASYNC
#endif

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
#if !UNITY_EDITOR && (UNITY_WINRT && !ENABLE_IL2CPP)
using System.Reflection;
#endif

/*==========================================================================
 *      CRI Atom Unity Integration
 *=========================================================================*/

/**
 * \addtogroup CRIATOM_UNITY_INTEGRATION
 * @{
 */

namespace CriWare {

/**
 * <summary>Global class of the CRI Atom library.</summary>
 * <remarks>
 * <para header='Description'>Class that contains the initialization functions of the CRI Atom library as well as the definitions of the types used within the library. <br/></para>
 * </remarks>
 */
public static class CriAtomPlugin
{
	#region Editor/Runtime共通

#if UNITY_EDITOR
	public static bool showDebugLog = false;
	public delegate void PreviewCallback();
	public static PreviewCallback previewCallback = null;
#endif

	public static void Log(string log)
	{
	#if UNITY_EDITOR
		if (CriAtomPlugin.showDebugLog) {
			Debug.Log(log);
		}
	#endif
	}

	/* 初期化カウンタ */
	private static int initializationCount = 0;

	public static bool isInitialized { get { return initializationCount > 0; } }
	
	/**
	 * <summary>Event just before the Atom library initialization</summary>
	 * <remarks>
	 * <para header='Description'>This is a callback event that is called just before the Atom library is initialized. <br/>
	 * Once called, the content will not be cleared.</para>
	 * </remarks>
	 */
	public static event System.Action OnBeforeInitialize = null;

	/**
	 * <summary>Event immediately after the Atom library initialization</summary>
	 * <remarks>
	 * <para header='Description'>This is a callback event that is called just before the Atom library is initialized. <br/>
	 * Once called, the content will be cleared.</para>
	 * </remarks>
	 */
	public static event System.Action OnInitialized = null;

	/**
	 * <summary>Event just before the end of the Atom library</summary>
	 * <remarks>
	 * <para header='Description'>This is a callback event that is called just before the Atom library is closed. <br/>
	 * Once called, the content will not be cleared.</para>
	 * </remarks>
	 */
	public static event System.Action OnBeforeFinalize = null;

	/**
	 * <summary>Event immediately after finalizing the Atom library</summary>
	 * <remarks>
	 * <para header='Description'>The callback event being called immediately after the Atom library finalization.<br/>
	 * When called after finalizing the library, the contents will be cleared.</para>
	 * </remarks>
	 */
	public static event System.Action OnFinalized = null;

	/**
	 * <summary>Execution of the Cue Link callback</summary>
	 * <remarks>
	 * <para header='Description'>A function to execute a Cue Link callback event triggered by the Atom Server thread. <br/>
	 * When this function is called, the callback function registered in CriAtomEx::OnCueLinkCallback will be executed if an event has occurred.</para>
	 * <para header='Note'>This function is called periodically by the CriAtomServer component and usually does not need to be called by the user.<br/>
	 * Calls this function if callbacks are needed when the application is not running (e.g. in an Editor extension).</para>
	 * </remarks>
	 */
	public static void ExecuteQueuedCueLinkCallbacks()
	{
		if (CriAtom.HasCueLinkCallback){
			criAtomUnity_ExecuteQueuedCueLinkCallbacks();
		}
	}

	/**
	 * <summary>Execution of the sequence event callback</summary>
	 * <remarks>
	 * <para header='Description'>A function to execute a callback event synchronized with a callback marker triggered by the Atom Server thread. <br/>
	 * When this function is called, the callback function registered in CriAtomExSequencer::OnCallback will be executed if an event has occurred.</para>
	 * <para header='Note'>This function is called periodically by the CriAtomServer component and usually does not need to be called by the user.<br/>
	 * Calls this function if callbacks are needed when the application is not running (e.g. in an Editor extension).</para>
	 * </remarks>
	 */
	public static void ExecuteQueuedEventCallbacks()
	{
		if (CriAtom.HasUserCallback){
			criAtomUnitySequencer_ExecuteQueuedEventCallbacks();
		}
	}

	/**
	 * <summary>Execution of the beat synchronization callback</summary>
	 * <remarks>
	 * <para header='Description'>A function to execute a beat-synchronized callback event triggered by the Atom Server thread. <br/>
	 * When this function is called, the callback functions registered in CriAtomExPlayer::OnBeatSyncCallback and CriAtomExBeatSync::OnCallback will be executed if an event has occurred.</para>
	 * <para header='Note'>This function is called periodically by the CriAtomServer component and usually does not need to be called by the user.<br/>
	 * Calls this function if callbacks are needed when the application is not running (e.g. in an Editor extension).</para>
	 * </remarks>
	 */
	public static void ExecuteQueuedBeatSyncCallbacks()
	{
		if (CriAtom.HasBeatSyncCallback){
			criAtomUnity_ExecuteQueuedBeatSyncCallbacks();
		}
	}

	private static List<IntPtr> effectInterfaceList = null;
	public static bool GetAudioEffectInterfaceList(out List<IntPtr> effect_interface_list)
	{
		if (CriAtomPlugin.IsLibraryInitialized() == true) {
			effect_interface_list = null;
			return false;
		}
		if (effectInterfaceList == null) {
			effectInterfaceList = new List<IntPtr>();
		}
		effect_interface_list = effectInterfaceList;
		return true;
	}

	public static void SetConfigParameters(int max_virtual_voices,
		int max_voice_limit_groups, int max_categories,
		System.Byte max_aisacs, System.Byte max_bus_sends,
		int max_sequence_events_per_frame, int max_beatsync_callbacks_per_frame,
		int max_cuelink_callbacks_per_frame,
		int num_standard_memory_voices, int num_standard_streaming_voices,
		int num_hca_mx_memory_voices, int num_hca_mx_streaming_voices,
		int output_sampling_rate, int num_asr_output_channels, CriAtom.SpeakerMapping speakerMapping,
		bool uses_in_game_preview, float server_frequency,
		int max_parameter_blocks,  int categories_per_playback,
		int max_faders, int num_buses, float max_pitch,
		CriAtomEx.SoundRendererType sound_renderer_type, bool enable_sonicsync_for_common,
		bool enable_atom_sound_disabled_mode)
	{
		criAtomUnity_SetConfigParameters(max_virtual_voices,
			max_voice_limit_groups, max_categories,
			max_aisacs, max_bus_sends,
			max_sequence_events_per_frame, max_beatsync_callbacks_per_frame,
			max_cuelink_callbacks_per_frame,
			num_standard_memory_voices, num_standard_streaming_voices,
			num_hca_mx_memory_voices, num_hca_mx_streaming_voices,
			output_sampling_rate, num_asr_output_channels, speakerMapping,
			uses_in_game_preview, server_frequency,
			max_parameter_blocks, categories_per_playback,
			max_faders, num_buses, max_pitch, sound_renderer_type, enable_sonicsync_for_common,
			enable_atom_sound_disabled_mode
			);

		CriAtomPlugin.isConfigured = true;
	}

	public static void SetConfigMonitorParametes(int max_preview_objects,
		int communication_buffer_size, int playback_position_update_interval)
	{
		Debug.Assert(max_preview_objects >= 0 && communication_buffer_size >= 0 && playback_position_update_interval >= 0);
		criAtomUnity_SetConfigMonitorParameters((uint)max_preview_objects,
			(uint)(communication_buffer_size * 1024), playback_position_update_interval);
	}

	public static void SetConfigAdditionalParameters_EDITOR(bool enable_user_pcm_output, int user_pcm_buffer_length)
	{
		criAtomUnity_SetConfigAdditionalParameters_EDITOR(enable_user_pcm_output);
		CriAtomExAsr.SetPcmBufferSize(user_pcm_buffer_length);
	}

	public static void SetConfigAdditionalParameters_PC(long buffering_time_pc, bool use_microsoft_spatial_sound)
	{
		criAtomUnity_SetConfigAdditionalParameters_PC(buffering_time_pc, use_microsoft_spatial_sound);
	}


	public static void SetConfigAdditionalParameters_IOS(bool enable_sonicsync, uint buffering_time_ios, bool override_ipod_music_ios, bool enable_os_notification_handling)
	{
		criAtomUnity_SetConfigAdditionalParameters_IOS(enable_sonicsync, buffering_time_ios, override_ipod_music_ios, enable_os_notification_handling);
	}

	public static void SetConfigAdditionalParameters_ANDROID(bool enable_sonicsync,
															 int num_low_delay_memory_voices, int num_low_delay_streaming_voices,
															 int sound_buffering_time,        int sound_start_buffering_time,
															 bool use_fast_mixer,             bool use_aaudio,
															 int stream_type)
	{
		if (!enable_sonicsync) {
			stream_type = 0;
		}
		criAtomUnity_SetConfigAdditionalParameters_ANDROID(enable_sonicsync,
														   num_low_delay_memory_voices, num_low_delay_streaming_voices,
														   sound_buffering_time,        sound_start_buffering_time,
														   use_fast_mixer,              stream_type);
#if !UNITY_EDITOR && UNITY_ANDROID
		if (use_fast_mixer == true) {
			IntPtr android_context = IntPtr.Zero;
			using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
			using (AndroidJavaObject activity = jc.GetStatic<AndroidJavaObject>("currentActivity")) {
				android_context = activity.GetRawObject();
				criAtomUnity_ApplyHardwareProperty_ANDROID(android_context);
			}
		}
		criAtomUnity_EnableAAudio_ANDROID(use_aaudio);
#endif
	}


	public static void SetMaxSamplingRateForStandardVoicePool(int sampling_rate_for_memory, int sampling_rate_for_streaming)
	{
		criAtomUnity_SetMaxSamplingRateForStandardVoicePool(sampling_rate_for_memory, sampling_rate_for_streaming);
	}

	public static int GetRequiredMaxVirtualVoices(CriAtomConfig atomConfig)
	{
		/* バーチャルボイスは、全ボイスプールのボイスの合計値よりも多くなくてはならない */
		int requiredVirtualVoices = 0;

		requiredVirtualVoices += atomConfig.standardVoicePoolConfig.memoryVoices;
		requiredVirtualVoices += atomConfig.standardVoicePoolConfig.streamingVoices;
		requiredVirtualVoices += atomConfig.hcaMxVoicePoolConfig.memoryVoices;
		requiredVirtualVoices += atomConfig.hcaMxVoicePoolConfig.streamingVoices;

		#if UNITY_ANDROID
		requiredVirtualVoices += atomConfig.androidLowLatencyStandardVoicePoolConfig.memoryVoices;
		requiredVirtualVoices += atomConfig.androidLowLatencyStandardVoicePoolConfig.streamingVoices;
		#endif

		return requiredVirtualVoices;
	}

	public static void InitializeLibrary()
	{
		CriWare.Common.CheckBinaryVersionCompatibility();

		/* 初期化カウンタの更新 */
		CriAtomPlugin.initializationCount++;
		if (CriAtomPlugin.initializationCount != 1) {
			return;
		}

		/* シーン実行前に初期化済みの場合は終了させる */
		if (CriAtomPlugin.IsLibraryInitialized() == true) {
			CriAtomPlugin.FinalizeLibrary();
			CriAtomPlugin.initializationCount = 1;
		}

		/* 初期化パラメータが設定済みかどうかを確認 */
		if (CriAtomPlugin.isConfigured == false) {
			Debug.Log("[CRIWARE] Atom initialization parameters are not configured. "
				+ "Initializes Atom by default parameters.");
		}

		/* 依存ライブラリの初期化 */
		CriFsPlugin.InitializeLibrary();

		/* ライブラリの初期化 */
		OnBeforeInitialize?.Invoke();
		CriAtomPlugin.criAtomUnity_Initialize();
		OnInitialized?.Invoke();
		OnInitialized = null;


		/* CriAtomServerのインスタンスを生成 */
		#if UNITY_EDITOR
		/* ゲームプレビュー時のみ生成する */
		if (UnityEngine.Application.isPlaying) {
			CriAtomServer.CreateInstance();
		}
		#else
		CriAtomServer.CreateInstance();
		#endif

		/* CriAtomListenerが存在しない場合のためのダミーリスナーを生成 */
		CriAtomListener.CreateDummyNativeListener();
	}

	public static bool IsLibraryInitialized()
	{
		/* ライブラリが初期化済みかチェック */
		return criAtomUnity_IsInitialized();
	}

	public static void FinalizeLibrary()
	{
		/* 初期化カウンタの更新 */
		CriAtomPlugin.initializationCount--;
		if (CriAtomPlugin.initializationCount < 0) {
			CriAtomPlugin.initializationCount = 0;
			if (CriAtomPlugin.IsLibraryInitialized() == false) {
				return;
			}
		}
		if (CriAtomPlugin.initializationCount != 0) {
			return;
		}

		/* CriAtomListenerが存在しない場合のためのダミーリスナーを破棄 */
		CriAtomListener.DestroyDummyNativeListener();

		/* CriAtomServerのインスタンスを破棄 */
		CriAtomServer.DestroyInstance();

		/* 未破棄のDisposableを破棄 */
		CriDisposableObjectManager.CallOnModuleFinalization(CriDisposableObjectManager.ModuleType.Atom);

		/* ユーザエフェクトインタフェースのリストをクリア */
		if (effectInterfaceList != null) {
			effectInterfaceList.Clear();
			effectInterfaceList = null;
		}

		/* ライブラリの終了 */
		OnBeforeFinalize?.Invoke();
		CriAtomPlugin.criAtomUnity_Finalize();
		OnFinalized?.Invoke();
		OnFinalized = null;

		/* 依存ライブラリの終了 */
		CriFsPlugin.FinalizeLibrary();
	}

#if UNITY_EDITOR
	[UnityEditor.InitializeOnLoadMethod]
	private static void RegisterForceFinalize(){
		UnityEditor.AssemblyReloadEvents.beforeAssemblyReload += ForceFinalize;
	}
	
	private static void ForceFinalize()
	{
		if(UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode){
			return;
		}
		initializationCount = 1;
		FinalizeLibrary();
	}
#endif

	public static void Pause(bool pause)
	{
		if (isInitialized) {
			criAtomUnity_Pause(pause);
		}
	}

	private static bool isConfigured = false;
	private static float timeSinceStartup = 0;
	private static CriWare.Common.CpuUsage cpuUsage;
	public static CriWare.Common.CpuUsage GetCpuUsage()
	{
		float currentTimeSinceStartup = Time.realtimeSinceStartup;
		if (currentTimeSinceStartup - timeSinceStartup > 1.0f) {
			CriAtomEx.PerformanceInfo info;
			CriAtomEx.GetPerformanceInfo(out info);

			cpuUsage.last = info.lastServerTime * 100.0f / info.averageServerInterval;
			cpuUsage.average = info.averageServerTime * 100.0f / info.averageServerInterval;
			cpuUsage.peak = info.maxServerTime * 100.0f / info.averageServerInterval;

			CriAtomEx.ResetPerformanceMonitor();
			timeSinceStartup = currentTimeSinceStartup;
		}
		return cpuUsage;
	}

	/**
	 * <summary>Gets the output sampling rate</summary>
	 * <returns>Output sampling rate</returns>
	 * <remarks>
	 * <para header='Description'>Gets the sampling rate of the PCM data output by the Atom library.</para>
	 * </remarks>
	 */
	public static int GetOutputSamplingRate()
	{
		return criAtomUnity_GetAsrOutputSamplingRate();
	}


	/**
	 * <summary>Gets the number of output channels</summary>
	 * <returns>The number of output channels</returns>
	 * <remarks>
	 * <para header='Description'>Gets the number of channels of the PCM data output by the Atom library.</para>
	 * </remarks>
	 */
	 public static int GetOutputChannels()
	{
		return criAtomUnity_GetAsrOutputChannels();
	}

	public static bool IsInitializedForPcmOutput()
	{
	#if UNITY_EDITOR
		return criAtomUnity_IsInitializedForPcmOutput_EDITOR();
	#else
		return false;
	#endif
	}

	private static int CRIATOMUNITY_PARAMETER_ID_LOOP_COUNT = 0;
	private static ushort CRIATOMPARAMETER2_ID_INVALID = ushort.MaxValue;

	public static ushort GetLoopCountParameterId()
	{
		ushort ret = criAtomUnity_GetNativeParameterId(CRIATOMUNITY_PARAMETER_ID_LOOP_COUNT);
		if (ret == CRIATOMPARAMETER2_ID_INVALID) {
			throw new Exception("GetNativeParameterId failed.");
		}
		return ret;
	}

	/**
	 * <summary>Get waveform data in the Cue</summary>
	 * <returns>Whether the acquisition succeeded</returns>
	 * <param name='acb'>ACB handle</param>
	 * <param name='cueName'>Cue name</param>
	 * <param name='decodeLpcmBuffer'>Array to store the data</param>
	 * <remarks>
	 * <para header='Description'>Get the waveform data in the specified Cue. <br/>
	 * If the Cue contains multiple waveforms, only the data for the first one will be returned. <br/>
	 * <br/>
	 * The data is returned as an interleaved array of length [number of samples * number of channels]. <br/>
	 * Please specify an array of sufficient length in <paramref name='decodeLpcmBuffer'/>.<code>
	 * acbHandle.GetWaveFormInfo(cueName, out var waveformInfo);
	 * var resultArray = new short[waveformInfo.numChannels * waveformInfo.numSamples];
	 * CriAtomPlugin.GetWaveSamples(acbHandle, cueName, resultArray);
	 * </code>
	 * </para>
	 * </remarks>
	 */
	public static bool GetWaveSamples(CriAtomExAcb acb, string cueName, System.Int16[] decodeLpcmBuffer)
	{
#if !UNITY_STANDALONE && !UNITY_EDITOR
		throw new NotSupportedException("[CRIWARE] This platform does not support the method CriAtomPlugin.GetWaveSamples");
#else
		var handle = GCHandle.Alloc(decodeLpcmBuffer, GCHandleType.Pinned);
		var result = criAtomUnityThumbnail_LoadWaveform(acb.nativeHandle, cueName, handle.AddrOfPinnedObject(), decodeLpcmBuffer.Length);
		handle.Free();
		return result;
#endif
	}




	#region Private Methods
	#endregion

	#region DLL Import
#if !CRIWARE_ENABLE_HEADLESS_MODE && (UNITY_STANDALONE || UNITY_EDITOR)
	[DllImport(CriWare.Common.pluginName, CallingConvention = CriWare.Common.pluginCallingConvention)]
	private static extern bool criAtomUnityThumbnail_LoadWaveform(IntPtr acbHn, string cue_name, IntPtr decode_lpcm_buffer, System.Int64 decodeLpcmBufferLength);
#else
	private static bool criAtomUnityThumbnail_LoadWaveform(IntPtr acbHn, string cue_name, IntPtr decode_lpcm_buffer, System.Int64 decodeLpcmBufferLength) {return false;}
#endif
	#if !CRIWARE_ENABLE_HEADLESS_MODE
	[DllImport(CriWare.Common.pluginName, CallingConvention = CriWare.Common.pluginCallingConvention)]
	private static extern void criAtomUnity_SetConfigParameters(int max_virtual_voices,
		int max_voice_limit_groups, int max_categories,
		System.Byte max_aisacs, System.Byte max_bus_sends,
		int max_sequence_events_per_frame, int max_beatsync_callbacks_per_frame,
		int max_cuelink_callbacks_per_frame,
		int num_standard_memory_voices, int num_standard_streaming_voices,
		int num_hca_mx_memory_voices, int num_hca_mx_streaming_voices,
		int output_sampling_rate, int num_asr_output_channels, CriAtom.SpeakerMapping speakerMapping,
		bool uses_in_game_preview, float server_frequency,
		int max_parameter_blocks, int categories_per_playback,
		int max_faders, int num_buses, float max_pitch,
		CriAtomEx.SoundRendererType sound_renderer_type, bool enable_sonicsync_for_common,
		bool enable_atom_sound_disabled_mode);

	[DllImport(CriWare.Common.pluginName, CallingConvention = CriWare.Common.pluginCallingConvention)]
	private static extern void criAtomUnity_SetConfigMonitorParameters(uint max_preivew_objects,
		uint communication_buffer_size, int playback_position_update_interval);

	[DllImport(CriWare.Common.pluginName, CallingConvention = CriWare.Common.pluginCallingConvention)]
	private static extern void criAtomUnity_SetConfigAdditionalParameters_EDITOR(bool enable_user_pcm_out_mode);

	[DllImport(CriWare.Common.pluginName, CallingConvention = CriWare.Common.pluginCallingConvention)]
	private static extern void criAtomUnity_SetConfigAdditionalParameters_PC(long buffering_time_pc, bool use_microsoft_spatial_sound);


	[DllImport(CriWare.Common.pluginName, CallingConvention = CriWare.Common.pluginCallingConvention)]
	private static extern void criAtomUnity_SetConfigAdditionalParameters_IOS(bool enable_sonicsync, uint buffering_time_ios, bool override_ipod_music_ios, bool enable_os_notification_handling);

	[DllImport(CriWare.Common.pluginName, CallingConvention = CriWare.Common.pluginCallingConvention)]
	private static extern void criAtomUnity_SetConfigAdditionalParameters_ANDROID(bool enable_sonicsync, 
																				  int num_low_delay_memory_voices, int num_low_delay_streaming_voices,
																				  int sound_buffering_time,        int sound_start_buffering_time,
																				  bool apply_hw_property,          int stream_type);

	#if !UNITY_EDITOR && UNITY_ANDROID
	[DllImport(CriWare.Common.pluginName, CallingConvention = CriWare.Common.pluginCallingConvention)]
	private static extern void criAtomUnity_ApplyHardwareProperty_ANDROID(IntPtr android_context);

	[DllImport(CriWare.Common.pluginName, CallingConvention = CriWare.Common.pluginCallingConvention)]
	private static extern void criAtomUnity_EnableAAudio_ANDROID(bool flag);
	#endif


	[DllImport(CriWare.Common.pluginName, CallingConvention = CriWare.Common.pluginCallingConvention)]
	private static extern void criAtomUnity_Initialize();

	[DllImport(CriWare.Common.pluginName, CallingConvention = CriWare.Common.pluginCallingConvention)]
	public static extern bool criAtomUnity_IsInitialized();

	[DllImport(CriWare.Common.pluginName, CallingConvention = CriWare.Common.pluginCallingConvention)]
	private static extern void criAtomUnity_Finalize();

	[DllImport(CriWare.Common.pluginName, CallingConvention = CriWare.Common.pluginCallingConvention)]
	private static extern void criAtomUnity_Pause(bool pause);

	[DllImport(CriWare.Common.pluginName, CallingConvention = CriWare.Common.pluginCallingConvention)]
	public static extern uint criAtomUnity_GetAllocatedHeapSize();

	[DllImport(CriWare.Common.pluginName, CallingConvention = CriWare.Common.pluginCallingConvention)]
	public static extern void criAtomUnity_ControlDataCompatibility(int code);

	[DllImport(CriWare.Common.pluginName, CallingConvention = CriWare.Common.pluginCallingConvention)]
	public static extern void criAtomUnitySequencer_SetEventCallback(IntPtr cbfunc, string separator_string);

	[DllImport(CriWare.Common.pluginName, CallingConvention = CriWare.Common.pluginCallingConvention)]
	public static extern void criAtomUnitySequencer_SetCallback(IntPtr cbfunc);

	[DllImport(CriWare.Common.pluginName, CallingConvention = CriWare.Common.pluginCallingConvention)]
	private static extern void criAtomUnitySequencer_ExecuteQueuedEventCallbacks();

	[DllImport(CriWare.Common.pluginName, CallingConvention = CriWare.Common.pluginCallingConvention)]
	public static extern void criAtomUnity_SetBeatSyncCallback(IntPtr cbfunc);

	[DllImport(CriWare.Common.pluginName, CallingConvention = CriWare.Common.pluginCallingConvention)]
	private static extern void criAtomUnity_ExecuteQueuedBeatSyncCallbacks();

	[DllImport(CriWare.Common.pluginName, CallingConvention = CriWare.Common.pluginCallingConvention)]
	public static extern void criAtomUnity_SetCueLinkCallback(IntPtr cbfunc);

	[DllImport(CriWare.Common.pluginName, CallingConvention = CriWare.Common.pluginCallingConvention)]
	private static extern void criAtomUnity_ExecuteQueuedCueLinkCallbacks();

	[DllImport(CriWare.Common.pluginName, CallingConvention = CriWare.Common.pluginCallingConvention)]
	private static extern void criAtomUnity_SetMaxSamplingRateForStandardVoicePool(int sampling_rate_for_memory, int sampling_rate_for_streaming);

	[DllImport(CriWare.Common.pluginName, CallingConvention = CriWare.Common.pluginCallingConvention)]
	public static extern void criAtomUnity_BeginLeCompatibleMode();

	[DllImport(CriWare.Common.pluginName, CallingConvention = CriWare.Common.pluginCallingConvention)]
	public static extern void criAtomUnity_EndLeCompatibleMode();


	[DllImport(CriWare.Common.pluginName, CallingConvention = CriWare.Common.pluginCallingConvention)]
	public static extern ushort criAtomUnity_GetNativeParameterId(int id);

	[DllImport(CriWare.Common.pluginName, CallingConvention = CriWare.Common.pluginCallingConvention)]
	private static extern bool criAtomUnity_IsInitializedForPcmOutput_EDITOR();

	[DllImport(CriWare.Common.pluginName, CallingConvention = CriWare.Common.pluginCallingConvention)]
	private static extern int criAtomUnity_GetAsrOutputSamplingRate();

	[DllImport(CriWare.Common.pluginName, CallingConvention = CriWare.Common.pluginCallingConvention)]
	private static extern int criAtomUnity_GetAsrOutputChannels();

	#else
	private static void criAtomUnity_SetConfigParameters(int max_virtual_voices,
		int max_voice_limit_groups, int max_categories,
		System.Byte max_aisacs, System.Byte max_bus_sends,
		int max_sequence_events_per_frame, int max_beatsync_callbacks_per_frame,
		int max_cuelink_callbacks_per_frame,
		int num_standard_memory_voices, int num_standard_streaming_voices,
		int num_hca_mx_memory_voices, int num_hca_mx_streaming_voices,
		int output_sampling_rate, int num_asr_output_channels, CriAtom.SpeakerMapping speakerMapping,
		bool uses_in_game_preview, float server_frequency,
		int max_parameter_blocks, int categories_per_playback,
		int max_faders, int num_buses, float max_pitch,
		CriAtomEx.SoundRendererType sound_renderer_type, bool enable_sonicsync_for_common,
		bool enable_atom_sound_disabled_mode) { }
	private static void criAtomUnity_SetConfigMonitorParameters(uint max_preivew_objects,
		uint communication_buffer_size, int playback_position_update_interval) { }
	private static void criAtomUnity_SetConfigAdditionalParameters_EDITOR(bool enable_user_pcm_out_mode) { }
	private static void criAtomUnity_SetConfigAdditionalParameters_PC(long buffering_time_pc, bool use_microsoft_spatial_sound) { }
	private static void criAtomUnity_SetConfigAdditionalParameters_IOS(bool enable_sonicsync, uint buffering_time_ios, bool override_ipod_music_ios, bool enable_os_notification_handling) { }
	private static void criAtomUnity_SetConfigAdditionalParameters_ANDROID(bool enable_sonicsync, 
																		   int num_low_delay_memory_voices, int num_low_delay_streaming_voices,
																		   int sound_buffering_time,        int sound_start_buffering_time,
																		   bool apply_hw_property,          int stream_type) { }
	#if !UNITY_EDITOR && UNITY_ANDROID
	private static void criAtomUnity_ApplyHardwareProperty_ANDROID(IntPtr android_context) { }
	private static void criAtomUnity_EnableAAudio_ANDROID(bool flag) { }
	#endif

	private static bool _dummyInitialized = false;
	private static void criAtomUnity_Initialize() { _dummyInitialized = true; }
	public static bool criAtomUnity_IsInitialized() { return _dummyInitialized; }
	private static void criAtomUnity_Finalize() { _dummyInitialized = false; }
	private static void criAtomUnity_Pause(bool pause) { }
	public static uint criAtomUnity_GetAllocatedHeapSize() { return 0; }
	public static void criAtomUnity_ControlDataCompatibility(int code) { }
	public static void criAtomUnitySequencer_SetEventCallback(IntPtr cbfunc, string separator_string) { }
	public static void criAtomUnitySequencer_SetCallback(IntPtr cbfunc) { }
	public static void criAtomUnitySequencer_ExecuteQueuedEventCallbacks() { }
	public static void criAtomUnity_SetBeatSyncCallback(IntPtr cbfunc) { }
	public static void criAtomUnity_ExecuteQueuedBeatSyncCallbacks() { }
	public static void criAtomUnity_SetCueLinkCallback(IntPtr cbfunc) { }
	public static void criAtomUnity_ExecuteQueuedCueLinkCallbacks() { }
	private static void criAtomUnity_SetMaxSamplingRateForStandardVoicePool(int sampling_rate_for_memory, int sampling_rate_for_streaming) { }
	public static void criAtomUnity_BeginLeCompatibleMode() { }
	public static void criAtomUnity_EndLeCompatibleMode() { }
	public static ushort criAtomUnity_GetNativeParameterId(int id) { return 0; }
	private static bool criAtomUnity_IsInitializedForPcmOutput_EDITOR() { return false; }
	private static int criAtomUnity_GetAsrOutputSamplingRate() { return 0; }
	private static int criAtomUnity_GetAsrOutputChannels() { return 0; }
	#endif
	#endregion

	#endregion
}

[Serializable]
public class CriAtomCueSheet
{
	public string name = "";
	public string acbFile = "";
	public string awbFile = "";
	public CriAtomExAcb acb;
	public CriAtomExAcbLoader.Status loaderStatus = CriAtomExAcbLoader.Status.Stop;
	public bool IsLoading { get { return loaderStatus == CriAtomExAcbLoader.Status.Loading; } }
	public bool IsError { get { return (loaderStatus == CriAtomExAcbLoader.Status.Error) || (!IsLoading && acb == null); } }
}

} //namespace CriWare

/**
 * @}
 */

/**
 * \addtogroup CRIATOM_UNITY_COMPONENT
 * @{
 */

namespace CriWare {

/**
 * <summary>A component for controlling the overall sound playback.</summary>
 * <remarks>
 * <para header='Description'>You need to prepare one for each scene.<br/>
 * If you create a CriAtomSource using the CRI Atom window on UnityEditor,
 * it will automatically be created as an object with the name "CRIWARE". Normally, you do not need to create it.</para>
 * </remarks>
 */
[AddComponentMenu("CRIWARE/CRI Atom")]
public partial class CriAtom : CriMonoBehaviour
{
		#region Types

		/**
		 * <summary>Mixer speaker mapping type</summary>
		 * <remarks>
		 * <para header='Description'>Specify the ASR speaker mapping.<br/></para>
		 * </remarks>
		 * <seealso cref='CriAtomExAsrRack.Config.speakerMapping'/>
		 * <seealso cref='CriAtomConfig.speakerMapping'/>
		 */
		public enum SpeakerMapping : System.Int32
		{
			Auto = 0,    /**< auto-configuration */
			Monaural,    /**< 1ch */
			Stereo,      /**< 2ch */
			Ch5_1,       /**< 5.1ch */
			Ch7_1,       /**< 7.1ch */
			Ch5_1_2,     /**< 5.1.2ch */
			Ch7_1_2,     /**< 7.1.2ch */
			Ch7_1_4,     /**< 7.1.4ch */
			Ch7_1_4_4,   /**< 7.1.4.4ch */
			Ambisonics1p,/**< 1st order Ambisonics */
			Ambisonics2p,/**< 2st order Ambisonics */
			Ambisonics3p,/**< 3st order Ambisonics */
			Object,      /**< Object Base Audio */
			Custom,      /**< Custom Speaker Mapping */
		}

		#endregion

	/* @cond DOXYGEN_IGNORE */
	public string acfFile = "";
	private bool acfIsLoading = false;
#if CRIWARE_FORCE_LOAD_ASYNC
	private byte[] acfData = null;
#endif
	public CriAtomCueSheet[] cueSheets = {};
	public string dspBusSetting = "";
	public bool dontDestroyOnLoad = false;

	private static CriAtomExSequencer.EventCallback eventUserCallback = null;
	internal static bool HasUserCallback => eventUserCallback != null;
	internal static event CriAtomExSequencer.EventCallback OnEventSequencerCallback {
		add {
			RegisterEventCallbackChain(value);
		}
		remove {
			UnregisterEventCallbackChain(value);
		}
	}
	private static CriAtomExSequencer.EventCbFunc eventUserCbFunc = null;

	private static event CriAtomExBeatSync.CbFunc beatsyncUserCbFunc = null;
	internal static bool HasBeatSyncCallback => beatsyncUserCbFunc != null;
	internal static event CriAtomExBeatSync.CbFunc OnBeatSyncCallback {
		add {
			RegisterBeatSyncCallbackChain(value);
		}
		remove {
			UnregisterBeatSyncCallbackChain(value);
		}
	}
	private static CriAtomExBeatSync.CbFunc obsoleteBeatSyncFunc = null;

	private static event CriAtomEx.CueLinkCbFunc cueLinkUserCbFunc = null;
	internal static bool HasCueLinkCallback => cueLinkUserCbFunc != null;
	internal static event CriAtomEx.CueLinkCbFunc OnCueLinkCallback {
		add {
			RegisterCueLinkCallbackChain(value);
		}
		remove {
			UnregisterCueLinkCallbackChain(value);
		}
	}

	private static CriAtom instance {
		get; set;
	}
	private GCHandle acfRegisterGCHandle;

	/* @endcond */

	#region Functions

	/**
	 * <summary>Attaching the DSP bus settings</summary>
	 * <param name='settingName'>Name of the DSP bus setting</param>
	 * <remarks>
	 * <para header='Description'>Builds a DSP bus from the DSP bus settings and attaches it to the sound renderer.<br/>
	 * If you want to switch the DSP bus settings currently set, detach the bus and then reattach it.
	 * <br/></para>
	 * <para header='Note'>This function is a return-on-complete function.<br/>
	 * Calling this function blocks the server processing of the Atom library for a while.<br/>
	 * If this function is called during sound playback, problems such as sound interruption may occur,
	 * so call this function at a timing when load fluctuations is accepted such as when switching scenes.<br/></para>
	 * </remarks>
	 * <seealso cref='CriAtom::DetachDspBusSetting'/>
	 */
	public static void AttachDspBusSetting(string settingName)
	{
		CriAtom.instance.dspBusSetting = settingName;
		if (!String.IsNullOrEmpty(settingName)) {
			CriAtomEx.AttachDspBusSetting(settingName);
		} else {
			CriAtomEx.DetachDspBusSetting();
		}
	}

	/**
	 * <summary>Detaches the DSP bus settings</summary>
	 * <remarks>
	 * <para header='Description'>Detaches the DSP bus settings.<br/>
	 * <br/></para>
	 * <para header='Note'>This function is a return-on-complete function.<br/>
	 * Calling this function blocks the server processing of the Atom library for a while.<br/>
	 * If this function is called during sound playback, problems such as sound interruption may occur,
	 * so call this function at a timing when load fluctuations is accepted such as when switching scenes.<br/></para>
	 * </remarks>
	 * <seealso cref='CriAtom::DetachDspBusSetting'/>
	 */
	public static void DetachDspBusSetting()
	{
		CriAtom.instance.dspBusSetting = "";
		CriAtomEx.DetachDspBusSetting();
	}

	/**
	 * <summary>Gets a Cue Sheet</summary>
	 * <param name='name'>Cue Sheet name</param>
	 * <returns>Cue Sheet object</returns>
	 * <remarks>
	 * <para header='Description'>Gets the registered Cue Sheet object based on the Cue Sheet name specified in the argument.<br/></para>
	 * </remarks>
	 */
	public static CriAtomCueSheet GetCueSheet(string name)
	{
		return CriAtom.instance.GetCueSheetInternal(name);
	}

	/**
	 * <summary>Adds a Cue Sheet</summary>
	 * <param name='name'>Cue Sheet name</param>
	 * <param name='acbFile'>ACB file path</param>
	 * <param name='awbFile'>AWB file path</param>
	 * <param name='binder'>Binder object (optional)</param>
	 * <returns>Cue Sheet object</returns>
	 * <remarks>
	 * <para header='Description'>A Cue Sheet is added based on the file path information specified in the argument.<br/>
	 * It is possible to register multiple Cue Sheets at the same time.<br/>
	 * <br/>
	 * If a relative path is specified for each file path, the file is loaded relative to the StreamingAssets folder.<br/>
	 * The file is loaded using the specified path if an absolute path or URL is specified.<br/>
	 * <br/>
	 * When adding a Cue Sheet from the ACB and AWB files packed in the CPK file,
	 * specify the binder which bound the CPK in the binder argument.<br/>
	 * The binder function cannot be used in ADXLE.<br/></para>
	 * </remarks>
	 */
	public static CriAtomCueSheet AddCueSheet(string name, string acbFile, string awbFile, CriFsBinder binder = null)
	{
	#if CRIWARE_FORCE_LOAD_ASYNC
		return CriAtom.AddCueSheetAsync(name, acbFile, awbFile, binder);
	#else
		CriAtomCueSheet cueSheet = CriAtom.instance.AddCueSheetInternal(name, acbFile, awbFile, binder);
		if (Application.isPlaying) {
			cueSheet.acb = CriAtom.instance.LoadAcbFile(binder, acbFile, awbFile);
		}
		return cueSheet;
	#endif
	}

	/**
	 * <summary>Adds a Cue Sheet asynchronously</summary>
	 * <param name='name'>Cue Sheet name</param>
	 * <param name='acbFile'>ACB file path</param>
	 * <param name='awbFile'>AWB file path</param>
	 * <param name='binder'>Binder object (optional)</param>
	 * <param name='loadAwbOnMemory'>Whether to load the AWB file on the memory (optional)</param>
	 * <returns>Cue Sheet object</returns>
	 * <remarks>
	 * <para header='Description'>A Cue Sheet is added asynchronously based on the file path information specified in the argument.<br/>
	 * It is possible to register multiple Cue Sheets at the same time.<br/>
	 * <br/>
	 * If a relative path is specified for each file path, the file is loaded relative to the StreamingAssets folder.<br/>
	 * The file is loaded using the specified path if an absolute path or URL is specified.<br/>
	 * <br/>
	 * When adding a Cue Sheet from the ACB and AWB files packed in the CPK file,
	 * specify the binder which bound the CPK in the binder argument.<br/>
	 * The binder function cannot be used in ADXLE.<br/>
	 * <br/>
	 * Loading is in progress while the CriAtomCueSheet::isLoading member of the returned Cue Sheet object is true.<br/>
	 * Be sure to check that it returns false before playing the Cue.<br/>
	 * <br/>
	 * If loadAwbOnMemory is set to false, only the header part of the AWB file is loaded on the memory and stream playback is performed.<br/>
	 * If loadAwbOnMemory is set to true, the entire AWB file is loaded into memory, effectively resulting in memory playback.<br/>
	 * In WebGL(Editor running), loadAwbOnMemory is forced to true for internal reasons.<br/></para>
	 * </remarks>
	 */
	public static CriAtomCueSheet AddCueSheetAsync(string name, string acbFile, string awbFile, CriFsBinder binder = null, bool loadAwbOnMemory = false)
	{
	#if UNITY_EDITOR && UNITY_WEBGL
		loadAwbOnMemory = true;
	#endif
		CriAtomCueSheet cueSheet = CriAtom.instance.AddCueSheetInternal(name, acbFile, awbFile, binder);
		if (Application.isPlaying) {
			CriAtom.instance.LoadAcbFileAsync(cueSheet, binder, acbFile, awbFile, loadAwbOnMemory);
		}
		return cueSheet;
	}

	/**
	 * <summary>Adds a Cue Sheet (reading from memory)</summary>
	 * <param name='name'>Cue Sheet name</param>
	 * <param name='acbData'>ACB data</param>
	 * <param name='awbFile'>AWB file path</param>
	 * <param name='awbBinder'>Binder object for AWB (optional)</param>
	 * <returns>Cue Sheet object</returns>
	 * <remarks>
	 * <para header='Description'>Adds a Cue Sheet from the data and file path information specified in the arguments.<br/>
	 * It is possible to register multiple Cue Sheets at the same time.<br/>
	 * <br/>
	 * If a relative path is specified for the file path, the file is loaded relative to the StreamingAssets folder.<br/>
	 * The file is loaded using the specified path if an absolute path or URL is specified.<br/>
	 * <br/>
	 * When adding a Cue Sheet from the AWB file packed in the CPK file,
	 * specify the binder which bound the CPK in the awbBinder argument.<br/>
	 * The binder function cannot be used in ADXLE.<br/></para>
	 * </remarks>
	 */
	public static CriAtomCueSheet AddCueSheet(string name, byte[] acbData, string awbFile, CriFsBinder awbBinder = null)
	{
	#if CRIWARE_FORCE_LOAD_ASYNC
		return CriAtom.AddCueSheetAsync(name, acbData, awbFile, awbBinder);
	#else
		CriAtomCueSheet cueSheet = CriAtom.instance.AddCueSheetInternal(name, "", awbFile, awbBinder);
		if (Application.isPlaying) {
			cueSheet.acb = CriAtom.instance.LoadAcbData(acbData, awbBinder, awbFile);
		}
		return cueSheet;
	#endif
	}

	/**
	 * <summary>Adds a Cue Sheet asynchronously (reading from memory)</summary>
	 * <param name='name'>Cue Sheet name</param>
	 * <param name='acbData'>ACB data</param>
	 * <param name='awbFile'>AWB file path</param>
	 * <param name='awbBinder'>Binder object for AWB (optional)</param>
	 * <param name='loadAwbOnMemory'>Whether to load the AWB file on the memory (optional)</param>
	 * <returns>Cue Sheet object</returns>
	 * <remarks>
	 * <para header='Description'>Adds a Cue Sheet from the data and file path information specified in the arguments.<br/>
	 * It is possible to register multiple Cue Sheets at the same time.<br/>
	 * <br/>
	 * If a relative path is specified for the file path, the file is loaded relative to the StreamingAssets folder.<br/>
	 * The file is loaded using the specified path if an absolute path or URL is specified.<br/>
	 * <br/>
	 * When adding a Cue Sheet from the AWB file packed in the CPK file,
	 * specify the binder which bound the CPK in the awbBinder argument.<br/>
	 * The binder function cannot be used in ADXLE.<br/>
	 * <br/>
	 * Loading is in progress while the CriAtomCueSheet::isLoading member of the returned Cue Sheet object is true.<br/>
	 * Be sure to check that it returns false before playing the Cue.<br/>
	 * <br/>
	 * If loadAwbOnMemory is set to false, only the header part of the AWB file is loaded on the memory and stream playback is performed.<br/>
	 * If loadAwbOnMemory is set to true, the entire AWB file is loaded into memory, effectively resulting in memory playback.<br/>
	 * In WebGL(Editor running), loadAwbOnMemory is forced to true for internal reasons.<br/></para>
	 * </remarks>
	 */
	public static CriAtomCueSheet AddCueSheetAsync(string name, byte[] acbData, string awbFile, CriFsBinder awbBinder = null, bool loadAwbOnMemory = false)
	{
	#if UNITY_EDITOR && UNITY_WEBGL
		loadAwbOnMemory = true;
	#endif
		CriAtomCueSheet cueSheet = CriAtom.instance.AddCueSheetInternal(name, "", awbFile, awbBinder);
		if (Application.isPlaying) {
			CriAtom.instance.LoadAcbDataAsync(cueSheet, acbData, awbBinder, awbFile, loadAwbOnMemory);
		}
		return cueSheet;
	}

	/**
	 * <summary>Removes a Cue Sheet</summary>
	 * <param name='name'>Cue Sheet name</param>
	 * <remarks>
	 * <para header='Description'>Removes the added Cue Sheet.<br/></para>
	 * </remarks>
	 */
	public static void RemoveCueSheet(string name)
	{
		if (CriAtom.instance == null) {
			return;
		}
		CriAtom.instance.RemoveCueSheetInternal(name);
	}

	/**
	 * <summary>Checks the completion of loading a Cue Sheet.</summary>
	 * <remarks>
	 * <para header='Description'>Checks the completion of loading all Cue Sheets.<br/></para>
	 * </remarks>
	 */
	public static bool CueSheetsAreLoading {
		get {
			if (CriAtom.instance == null) {
				return false;
			}
			foreach (var cueSheet in CriAtom.instance.cueSheets) {
				if (cueSheet.IsLoading) {
					return true;
				}
			}
			return false;
		}
	}

	/**
	 * <summary>Gets the ACB object</summary>
	 * <param name='cueSheetName'>Cue Sheet name</param>
	 * <returns>ACB object</returns>
	 * <remarks>
	 * <para header='Description'>Gets the ACB object corresponding to the specified Cue Sheet.<br/></para>
	 * </remarks>
	 */
	public static CriAtomExAcb GetAcb(string cueSheetName)
	{
		foreach (var cueSheet in CriAtom.instance.cueSheets) {
			if (cueSheetName == cueSheet.name) {
				return cueSheet.acb;
			}
		}
		Debug.LogWarning("[CRIWARE] Cue sheet named \""+ cueSheetName + "\" is not loaded.");
		return null;
	}

	/**
	 * <summary>Sets the Category volume by specifying the Category name.</summary>
	 * <param name='name'>Category name</param>
	 * <param name='volume'>Volume</param>
	 */
	public static void SetCategoryVolume(string name, float volume)
	{
		CriAtomExCategory.SetVolume(name, volume);
	}

	/**
	 * <summary>Sets the Category volume by specifying the Category ID.</summary>
	 * <param name='id'>Category ID</param>
	 * <param name='volume'>Volume</param>
	 */
	public static void SetCategoryVolume(int id, float volume)
	{
		CriAtomExCategory.SetVolume(id, volume);
	}

	/**
	 * <summary>Gets the Category volume by specifying the Category name.</summary>
	 * <param name='name'>Category name</param>
	 * <returns>Volume</returns>
	 */
	public static float GetCategoryVolume(string name)
	{
		return CriAtomExCategory.GetVolume(name);
	}
	/**
	 * <summary>Gets the Category volume by specifying the Category ID.</summary>
	 * <param name='id'>Category ID</param>
	 * <returns>Volume</returns>
	 */
	public static float GetCategoryVolume(int id)
	{
		return CriAtomExCategory.GetVolume(id);
	}

	/**
	 * <summary>Enables the acquisition of the bus information</summary>
	 * <param name='busName'>DSP bus name</param>
	 * <param name='sw'>True: Enable acquisition. False: Disable acquisition.</param>
	 */
	public static void SetBusAnalyzer(string busName, bool sw)
	{
	#if !UNITY_EDITOR && UNITY_PSP2
		// unsupported
	#else
		if (sw) {
			CriAtomExAsr.AttachBusAnalyzer(busName, 50, 1000);
		} else {
			CriAtomExAsr.DetachBusAnalyzer(busName);
		}
	#endif
	}

	/**
	 * <summary>Enables acquisition of all bus information.</summary>
	 * <param name='sw'>True: Enable acquisition. False: Disable acquisition.</param>
	 */
	public static void SetBusAnalyzer(bool sw)
	{
	#if !UNITY_EDITOR && UNITY_PSP2
		// unsupported
	#else
		if (sw) {
			CriAtomExAsr.AttachBusAnalyzer(50, 1000);
		} else {
			CriAtomExAsr.DetachBusAnalyzer();
		}
	#endif
	}

	/**
	 * <summary>Gets the bus information.</summary>
	 * <param name='busName'>DSP bus name</param>
	 * <returns>DSP bus information</returns>
	 */
	public static CriAtomExAsr.BusAnalyzerInfo GetBusAnalyzerInfo(string busName)
	{
		CriAtomExAsr.BusAnalyzerInfo info;
	#if !UNITY_EDITOR && UNITY_PSP2
		info = new CriAtomExAsr.BusAnalyzerInfo(null);
	#else
		CriAtomExAsr.GetBusAnalyzerInfo(busName, out info);
	#endif
		return info;
	}

	/**
	 * \deprecated
	 * This is a deprecated API that will be removed.
	 * Please consider using CriAtom.GetBusAnalyzerInfo(string busName) instead.
	*/
	[System.Obsolete("Use CriAtom.GetBusAnalyzerInfo(string busName)")]
	public static CriAtomExAsr.BusAnalyzerInfo GetBusAnalyzerInfo(int busId)
	{
		CriAtomExAsr.BusAnalyzerInfo info;
	#if !UNITY_EDITOR && UNITY_PSP2
		info = new CriAtomExAsr.BusAnalyzerInfo(null);
	#else
		CriAtomExAsr.GetBusAnalyzerInfo(busId, out info);
	#endif
		return info;
	}

	#endregion // Functions

	/* @cond DOXYGEN_IGNORE */
	#region Functions for internal use

	public void Setup()
	{
		if (CriAtom.instance != null && CriAtom.instance != this) {
			var obj = CriAtom.instance.gameObject;
			CriAtom.instance.Shutdown();
			CriAtomEx.UnregisterAcf();
			GameObject.Destroy(obj);
		}

		CriAtom.instance = this;

		CriAtomPlugin.InitializeLibrary();

		if (!String.IsNullOrEmpty(this.acfFile)) {
			string acfPath = Path.Combine(CriWare.Common.streamingAssetsPath, this.acfFile);
			CriAtomEx.RegisterAcf(null, acfPath);
		}

		if (!String.IsNullOrEmpty(dspBusSetting)) {
			AttachDspBusSetting(dspBusSetting);
		}

		foreach (var cueSheet in this.cueSheets) {
			cueSheet.acb = this.LoadAcbFile(null, cueSheet.acbFile, cueSheet.awbFile);
		}

		if (this.dontDestroyOnLoad) {
			GameObject.DontDestroyOnLoad(this.gameObject);
		}
	}

	public void Shutdown()
	{
		foreach (var cueSheet in this.cueSheets) {
			if (cueSheet.acb != null) {
				cueSheet.acb.Dispose();
				cueSheet.acb = null;
			}
		}
		CriAtomPlugin.FinalizeLibrary();
		if (CriAtom.instance == this) {
			CriAtom.instance = null;
		}
	}

	void Awake()
	{
		if (CriAtom.instance != null && CriAtom.instance != this) {
			if (CriAtom.instance.acfFile != this.acfFile) {
				var obj = CriAtom.instance.gameObject;
				CriAtom.instance.Shutdown();
				CriAtomEx.UnregisterAcf();
				GameObject.Destroy(obj);
				return;
			}
			if (CriAtom.instance.dspBusSetting != this.dspBusSetting) {
				CriAtom.AttachDspBusSetting(this.dspBusSetting);
			}
			CriAtom.instance.MargeCueSheet(this.cueSheets, this.dontRemoveExistsCueSheet);
			GameObject.Destroy(this.gameObject);
		}
	}

	protected override void OnEnable()
	{
		base.OnEnable();
	#if UNITY_EDITOR
		if (CriAtomPlugin.previewCallback != null) {
			CriAtomPlugin.previewCallback();
		}
	#endif
		if (CriAtom.instance != null) {
			return;
		}

	#if CRIWARE_FORCE_LOAD_ASYNC
		this.SetupAsync();
	#else
		this.Setup();
	#endif
	}

	void OnDestroy()
	{
		if (this != CriAtom.instance) {
			return;
		}
		if (beatsyncUserCbFunc != null) {
			beatsyncUserCbFunc = null;
			CriAtomPlugin.criAtomUnity_SetBeatSyncCallback(IntPtr.Zero);
		}
		if (this.acfRegisterGCHandle.IsAllocated) {
			CriAtomEx.UnregisterAcf();
			this.acfRegisterGCHandle.Free();
		}
		this.Shutdown();
	}

	public override void CriInternalUpdate() { }

	public override void CriInternalLateUpdate() { }

	public CriAtomCueSheet GetCueSheetInternal(string name)
	{
		for (int i = 0; i < this.cueSheets.Length; i++) {
			CriAtomCueSheet cueSheet = this.cueSheets[i];
			if (cueSheet.name == name) {
				return cueSheet;
			}
		}
		return null;
	}

	public CriAtomCueSheet AddCueSheetInternal(string name, string acbFile, string awbFile, CriFsBinder binder)
	{
		var cueSheets = new CriAtomCueSheet[this.cueSheets.Length + 1];
		this.cueSheets.CopyTo(cueSheets, 0);
		this.cueSheets = cueSheets;

		var cueSheet = new CriAtomCueSheet();
		this.cueSheets[this.cueSheets.Length - 1] = cueSheet;
		if (String.IsNullOrEmpty(name)) {
			cueSheet.name = Path.GetFileNameWithoutExtension(acbFile);
		} else {
			cueSheet.name = name;
		}
		cueSheet.acbFile = acbFile;
		cueSheet.awbFile = awbFile;
		return cueSheet;
	}

	public void RemoveCueSheetInternal(string name)
	{
		int index = -1;
		for (int i = 0; i < this.cueSheets.Length; i++) {
			if (name == this.cueSheets[i].name) {
				index = i;
			}
		}
		if (index < 0) {
			return;
		}

		var cueSheet = this.cueSheets[index];
		if (cueSheet.acb != null) {
			cueSheet.acb.Dispose();
			cueSheet.acb = null;
		}

		var cueSheets = new CriAtomCueSheet[this.cueSheets.Length - 1];
		Array.Copy(this.cueSheets, 0, cueSheets, 0, index);
		Array.Copy(this.cueSheets, index + 1, cueSheets, index, this.cueSheets.Length - index - 1);
		this.cueSheets = cueSheets;
	}

	public bool dontRemoveExistsCueSheet = false;

	/*
	 * newDontRemoveExistsCueSheet == false の場合、古いキューシートを登録解除して、新しいキューシートの登録を行う。
	 * ただし同じキューシートの再登録は回避する
	 */
	private void MargeCueSheet(CriAtomCueSheet[] newCueSheets, bool newDontRemoveExistsCueSheet)
	{
		if (!newDontRemoveExistsCueSheet) {
			for (int i = 0; i < this.cueSheets.Length; ) {
				int index = Array.FindIndex(newCueSheets, sheet => sheet.name == this.cueSheets[i].name);
				if (index < 0) {
					CriAtom.RemoveCueSheet(this.cueSheets[i].name);
				} else {
					i++;
				}
			}
		}

		foreach (var sheet1 in newCueSheets) {
			var sheet2 = this.GetCueSheetInternal(sheet1.name);
			if (sheet2 == null) {
				CriAtom.AddCueSheet(null, sheet1.acbFile, sheet1.awbFile, null);
			}
		}
	}

	private CriAtomExAcb LoadAcbFile(CriFsBinder binder, string acbFile, string awbFile)
	{
		if (String.IsNullOrEmpty(acbFile)) {
			return null;
		}

		string acbPath = acbFile;
		if ((binder == null) && CriWare.Common.IsStreamingAssetsPath(acbPath)) {
			acbPath = Path.Combine(CriWare.Common.streamingAssetsPath, acbPath);
		}

		string awbPath = awbFile;
		if (!String.IsNullOrEmpty(awbPath)) {
			if ((binder == null) && CriWare.Common.IsStreamingAssetsPath(awbPath)) {
				awbPath = Path.Combine(CriWare.Common.streamingAssetsPath, awbPath);
			}
		}

		return CriAtomExAcb.LoadAcbFile(binder, acbPath, awbPath);
	}

	private CriAtomExAcb LoadAcbData(byte[] acbData, CriFsBinder binder, string awbFile)
	{
		if (acbData == null) {
			return null;
		}

		string awbPath = awbFile;
		if (!String.IsNullOrEmpty(awbPath)) {
			if ((binder == null) && CriWare.Common.IsStreamingAssetsPath(awbPath)) {
				awbPath = Path.Combine(CriWare.Common.streamingAssetsPath, awbPath);
			}
		}

		return CriAtomExAcb.LoadAcbData(acbData, binder, awbPath);
	}

#if CRIWARE_FORCE_LOAD_ASYNC
	private void SetupAsync()
	{
		CriAtom.instance = this;

		CriAtomPlugin.InitializeLibrary();

		if (this.dontDestroyOnLoad) {
			GameObject.DontDestroyOnLoad(this.gameObject);
		}

		if (!String.IsNullOrEmpty(this.acfFile)) {
			this.acfIsLoading = true;
			StartCoroutine(RegisterAcfAsync(this.acfFile, this.dspBusSetting));
		}

		foreach (var cueSheet in this.cueSheets) {
			#if UNITY_EDITOR
			bool loadAwbOnMemory = true;
			#else
			bool loadAwbOnMemory = false;
			#endif
			StartCoroutine(LoadAcbFileCoroutine(cueSheet, null, cueSheet.acbFile, cueSheet.awbFile, loadAwbOnMemory));
		}
	}

	private IEnumerator RegisterAcfAsync(string acfFile, string dspBusSetting)
	{
	#if UNITY_EDITOR
		string acfPath = "file://" + CriWare.Common.streamingAssetsPath + "/" + acfFile;
	#else
		string acfPath = CriWare.Common.streamingAssetsPath + "/" + acfFile;
	#endif
		using (var req = new WWW(acfPath)) {
			yield return req;
			this.acfData = req.bytes;
		}
		/*
		* If there is already an allocated ACF data,
		* unregister and then release it.
		*/
		if (this.acfRegisterGCHandle.IsAllocated) {
			CriAtomEx.UnregisterAcf();
			this.acfRegisterGCHandle.Free();
		}
		this.acfRegisterGCHandle = GCHandle.Alloc(this.acfData, GCHandleType.Pinned);
		CriAtomEx.RegisterAcf(this.acfRegisterGCHandle.AddrOfPinnedObject(), acfData.Length);

		if (!String.IsNullOrEmpty(dspBusSetting)) {
			AttachDspBusSetting(dspBusSetting);
		}
		this.acfIsLoading = false;
	}
#endif


	private void LoadAcbFileAsync(CriAtomCueSheet cueSheet, CriFsBinder binder, string acbFile, string awbFile, bool loadAwbOnMemory)
	{
		if (String.IsNullOrEmpty(acbFile)) {
			return;
		}

		StartCoroutine(LoadAcbFileCoroutine(cueSheet, binder, acbFile, awbFile, loadAwbOnMemory));
	}

	private IEnumerator LoadAcbFileCoroutine(CriAtomCueSheet cueSheet, CriFsBinder binder, string acbPath, string awbPath, bool loadAwbOnMemory)
	{
		cueSheet.loaderStatus = CriAtomExAcbLoader.Status.Loading;

		if ((binder == null) && CriWare.Common.IsStreamingAssetsPath(acbPath)) {
			acbPath = Path.Combine(CriWare.Common.streamingAssetsPath, acbPath);
		}

		if (!String.IsNullOrEmpty(awbPath)) {
			if ((binder == null) && CriWare.Common.IsStreamingAssetsPath(awbPath)) {
				awbPath = Path.Combine(CriWare.Common.streamingAssetsPath, awbPath);
			}
		}

		while (this.acfIsLoading) {
			yield return null;
		}

		using (var asyncLoader = CriAtomExAcbLoader.LoadAcbFileAsync(binder, acbPath, awbPath, loadAwbOnMemory)) {
			if (asyncLoader == null) {
				cueSheet.loaderStatus = CriAtomExAcbLoader.Status.Error;
				yield break;
			}

			while (true) {
				var status = asyncLoader.GetStatus();
				cueSheet.loaderStatus = status;
				if (status == CriAtomExAcbLoader.Status.Complete) {
					cueSheet.acb = asyncLoader.MoveAcb();
					break;
				} else if (status == CriAtomExAcbLoader.Status.Error) {
					break;
				}
				yield return null;
			}
		}
	}

	private void LoadAcbDataAsync(CriAtomCueSheet cueSheet, byte[] acbData, CriFsBinder awbBinder, string awbFile, bool loadAwbOnMemory)
	{
		StartCoroutine(LoadAcbDataCoroutine(cueSheet, acbData, awbBinder, awbFile, loadAwbOnMemory));
	}

	private IEnumerator LoadAcbDataCoroutine(CriAtomCueSheet cueSheet, byte[] acbData, CriFsBinder awbBinder, string awbPath, bool loadAwbOnMemory)
	{
		cueSheet.loaderStatus = CriAtomExAcbLoader.Status.Loading;

		if (!String.IsNullOrEmpty(awbPath)) {
			if ((awbBinder == null) && CriWare.Common.IsStreamingAssetsPath(awbPath)) {
				awbPath = Path.Combine(CriWare.Common.streamingAssetsPath, awbPath);
			}
		}

		while (this.acfIsLoading) {
			yield return null;
		}

		using (var asyncLoader = CriAtomExAcbLoader.LoadAcbDataAsync(acbData, awbBinder, awbPath, loadAwbOnMemory)) {
			if (asyncLoader == null) {
				cueSheet.loaderStatus = CriAtomExAcbLoader.Status.Error;
				yield break;
			}

			while (true) {
				var status = asyncLoader.GetStatus();
				cueSheet.loaderStatus = status;
				if (status == CriAtomExAcbLoader.Status.Complete) {
					cueSheet.acb = asyncLoader.MoveAcb();
					break;
				} else if (status == CriAtomExAcbLoader.Status.Error) {
					break;
				}
				yield return null;
			}
		}
	}

	[AOT.MonoPInvokeCallback(typeof(CriAtomExSequencer.EventCbFunc))]
	public static void SequenceEventCallbackFromNative(string eventString)
	{
		/* 本関数はアプリケーションメインスレッドの CriAtom.Update から呼び出される */
		if (eventUserCbFunc != null) {
			eventUserCbFunc(eventString);
		}
	}

	[AOT.MonoPInvokeCallback(typeof(CriAtomExSequencer.EventCallback))]
	private static void SequenceCallbackFromNative(ref CriAtomExSequencer.CriAtomExSequenceEventInfo criAtomExSequenceInfo)
	{
		/* 本関数はアプリケーションメインスレッドの CriAtom.Update から呼び出される */
		if (eventUserCallback != null) {
			eventUserCallback(ref criAtomExSequenceInfo);
		}
	}

	[AOT.MonoPInvokeCallback(typeof(CriAtomExBeatSync.CbFunc))]
	public static void BeatSyncCallbackFromNative(ref CriAtomExBeatSync.Info info)
	{
		/* 本関数はアプリケーションメインスレッドの CriAtom.Update から呼び出される */
		if (beatsyncUserCbFunc != null) {
			beatsyncUserCbFunc(ref info);
		}
	}

	[AOT.MonoPInvokeCallback(typeof(CriAtomEx.CueLinkCbFunc))]
	public static void CueLinkCallbackFromNative(ref CriAtomEx.CueLinkInfo info)
	{
		/* 本関数はアプリケーションメインスレッドの CriAtom.Update から呼び出される */
		if (cueLinkUserCbFunc != null) {
			cueLinkUserCbFunc(ref info);
		}
	}

	/* プラグイン内部用API */
	public static void SetEventCallback(CriAtomExSequencer.EventCbFunc func, string separator)
	{
		/* ネイティブプラグインに関数ポインタを登録 */
		IntPtr ptr = IntPtr.Zero;
		eventUserCbFunc = func;
		if (func != null) {
			CriAtomExSequencer.EventCbFunc delegateObject;
			delegateObject = new CriAtomExSequencer.EventCbFunc(CriAtom.SequenceEventCallbackFromNative);
			ptr = Marshal.GetFunctionPointerForDelegate(delegateObject);
		}
		CriAtomPlugin.criAtomUnitySequencer_SetEventCallback(ptr, separator);
	}

	private static void RegisterEventCallbackChain(CriAtomExSequencer.EventCallback func)
	{
		/* Register callback function's pointer to native plugin side. */
		IntPtr ptr = IntPtr.Zero;
		if (eventUserCallback == null) {
			CriAtomExSequencer.EventCallback delegateObject;
			delegateObject = new CriAtomExSequencer.EventCallback(CriAtom.SequenceCallbackFromNative);
			ptr = Marshal.GetFunctionPointerForDelegate(delegateObject);
			CriAtomPlugin.criAtomUnitySequencer_SetCallback(ptr);
		}
		eventUserCallback += func;
	}

	private static void UnregisterEventCallbackChain(CriAtomExSequencer.EventCallback func)
	{
		eventUserCallback -= func;
		if (eventUserCallback == null) {
			CriAtomPlugin.criAtomUnitySequencer_SetCallback(IntPtr.Zero);
		}
	}

	public static void SetBeatSyncCallback(CriAtomExBeatSync.CbFunc func)
	{
		Debug.LogWarning("[CRIWARE] This function(CriAtom.SetBeatSyncCallback) is deprecated. Use CriAtomExBeatSync class.");
		if (func == null) {
			/* Only unregistered via CriAtomExBeatSync or CriAtom SetBeatSyncCallback(). */
			UnregisterBeatSyncCallbackChain(obsoleteBeatSyncFunc);
			obsoleteBeatSyncFunc = null;
			return;
		}
		if (obsoleteBeatSyncFunc != null) {
			UnregisterBeatSyncCallbackChain(obsoleteBeatSyncFunc);
		}
		obsoleteBeatSyncFunc = func;
		RegisterBeatSyncCallbackChain(obsoleteBeatSyncFunc);
	}

	private static void RegisterBeatSyncCallbackChain(CriAtomExBeatSync.CbFunc func)
	{
		if (beatsyncUserCbFunc == null) {
			CriAtomExBeatSync.CbFunc delegateObject;
			delegateObject = new CriAtomExBeatSync.CbFunc(CriAtom.BeatSyncCallbackFromNative);
			var ptr = Marshal.GetFunctionPointerForDelegate(delegateObject);
			CriAtomPlugin.criAtomUnity_SetBeatSyncCallback(ptr);
		}
		beatsyncUserCbFunc += func;
	}

	private static void UnregisterBeatSyncCallbackChain(CriAtomExBeatSync.CbFunc func)
	{
		beatsyncUserCbFunc -= func;
		if (beatsyncUserCbFunc == null) {
			CriAtomPlugin.criAtomUnity_SetBeatSyncCallback(IntPtr.Zero);
		}
	}

	private static void RegisterCueLinkCallbackChain(CriAtomEx.CueLinkCbFunc func)
	{
		if (cueLinkUserCbFunc == null) {
			CriAtomEx.CueLinkCbFunc delegateObject;
			delegateObject = new CriAtomEx.CueLinkCbFunc(CriAtom.CueLinkCallbackFromNative);
			var ptr = Marshal.GetFunctionPointerForDelegate(delegateObject);
			CriAtomPlugin.criAtomUnity_SetCueLinkCallback(ptr);
		}
		cueLinkUserCbFunc += func;
	}

	private static void UnregisterCueLinkCallbackChain(CriAtomEx.CueLinkCbFunc func)
	{
		cueLinkUserCbFunc -= func;
		if (cueLinkUserCbFunc == null) {
			CriAtomPlugin.criAtomUnity_SetCueLinkCallback(IntPtr.Zero);
		}
	}

	#endregion
	/* @endcond */

}

} //namespace CriWare
/** @} */
/* end of file */
