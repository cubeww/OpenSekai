/****************************************************************************
 *
 * Copyright (c) 2018 CRI Middleware Co., Ltd.
 *
 ****************************************************************************/

#if UNITY_EDITOR

namespace CriWare {

public partial class CriProfiler
{
	/* CRI Atom Preview connection ver. */
	public const int CRI_ATOM_PREVIEW_CONNECTION_VERSION = 0x01000000;

	/* the size of the parameter "packet size" in bytes */
	private const int DATA_LENGTH_PARAM_SIZE = 4;

	protected enum TcpCommandId {
		CRITCP_MAIL_OPEN = 1,
		CRITCP_MAIL_OPEN_RESULT,
		CRITCP_MAIL_RECV,
		CRITCP_MAIL_EXIT_TO_PROFILER,	// プロファイラーへの切断通知
		CRITCP_MAIL_EXIT,
		CRITCP_MAIL_SEND_CUESHEET_BINARY,
		CRITCP_MAIL_SEND_CUESHEET_BINARY_RESULT,
		CRITCP_MAIL_SEND_CUESHEET_BINARYDATA,
		CRITCP_MAIL_SEND_CUESHEET_BINARYDATA_RESULT,
		CRITCP_MAIL_START_MONITOR,                                      /* 10 */
		CRITCP_MAIL_START_MONITOR_RESULT,
		CRITCP_MAIL_STOP_MONITOR,
		CRITCP_MAIL_STOP_MONITOR_RESULT,
		CRITCP_MAIL_MONITOR_AUOBJ_STATUS,
		CRITCP_MAIL_MONITOR_AUOBJ_STATUS_RESULT,
		CRITCP_MAIL_SEND_LOG_REQUEST__DELETED,
		CRITCP_MAIL_SEND_LOG_REQUEST_RESULT__DELETED,
		CRITCP_MAIL_START_LOG_PLAYBACK__DELETED,
		CRITCP_MAIL_START_LOG_PLAYBACK_RESULT__DELETED,
		CRITCP_MAIL_STOP_LOG_PLAYBACK__DELETED,
		CRITCP_MAIL_STOP_LOG_PLAYBACK_RESULT__DELETED,
		CRITCP_MAIL_START_LOG_RECORD,
		CRITCP_MAIL_START_LOG_RECORD_RESULT,
		CRITCP_MAIL_STOP_LOG_RECORD,
		CRITCP_MAIL_STOP_LOG_RECORD_RESULT,
		CRITCP_MAIL_DO_AUDIO_API,
		CRITCP_MAIL_DO_AUDIO_API_RESULT,
		CRITCP_MAIL_GET_COMMAND,
		CRITCP_MAIL_NON,
		CRITCP_MAIL_NUM_AUOBJ,                                          /* 30 */
		CRITCP_MAIL_SEND_LOG,
		CRITCP_MAIL_ERROR,
		CRITCP_MAIL_PREVIEW_FLAG,
		CRITCP_MAIL_PREVIEW_FLAG_RESULT,
		CRITCP_MAIL_SEND_FILE_INFORMATION,
		CRITCP_MAIL_SEND_FILE_INFORMATION_RESULT,
		CRITCP_MAIL_SEND_FILE_DATA,
		CRITCP_MAIL_SEND_FILE_DATA_RESULT,
		CRITCP_MAIL_SEND_LOG_FOR_PLAYBACK__DELETED,
		CRITCP_MAIL_SEND_LOG_FOR_PLAYBACK_RESULT__DELETED,
		CRITCP_MAIL_PREVIEW_SEND_DETA_RESULT,
		CRITCP_MAIL_PREVIEW_SEND_DETA_RESULT_RESULT,
		CRITCP_MAIL_PREVIEW_CPU_LOAD,
		CRITCP_MAIL_PREVIEW_CPU_LOAD_RESULT,
		CRITCP_MAIL_PREVIEW_PLAYER_STATUS,
		CRITCP_MAIL_PREVIEW_PLAYER_STATUS_RESULT,
		CRITCP_MAIL_DO_FILE_SYSTEM_API,
		CRITCP_MAIL_DO_FILE_SYSTEM_API_RESULT,
		CRITCP_MAIL_SEND_REGISTER_ATOM_CONFIG_BINARY,
		CRITCP_MAIL_SEND_REGISTER_ATOM_CONFIG_BINARY_RESULT,            /* 50 */
		CRITCP_MAIL_SEND_ATOM_CONFIG_BINARYDATA,
		CRITCP_MAIL_SEND_ATOM_CONFIG_BINARYDATA_RESULT,
		CRITCP_MAIL_SEND_ATOM_CUESHEET_BINARY,
		CRITCP_MAIL_SEND_ATOM_CUESHEET_BINARY_RESULT,
		CRITCP_MAIL_SEND_ATOM_CUESHEET_BINARYDATA,
		CRITCP_MAIL_SEND_ATOM_CUESHEET_BINARYDATA_RESULT,
		CRITCP_MAIL_MONITOR_ATOM_EXPLAYBACKINFO_PLAY_POSITION,
		CRITCP_MAIL_MONITOR_ATOM_EXPLAYBACKINFO_PLAY_POSITION_RESULT,
		CRITCP_MAIL_MONITOR_ATOM_EXPLAYBACKINFO_PLAY_END,
		CRITCP_MAIL_MONITOR_ATOM_EXPLAYBACKINFO_PLAY_END_RESULT,        /* 60 */
		CRITCP_MAIL_MONITOR_ATOMEXPLAYERS_STATUS,
		CRITCP_MAIL_MONITOR_ATOMEXPLAYERS_STATUS_RESULT,
		CRITCP_MAIL_MONITOR_ATOM_AUTO_MODULATION_AISAC_CONTROL,
		CRITCP_MAIL_MONITOR_ATOM_AUTO_MODULATION_AISAC_CONTROL_RESULT,
		CRITCP_MAIL_UPDATE_ATOM_CUESHEET_BINARY,
		CRITCP_MAIL_UPDATE_ATOM_CUESHEET_BINARY_RESULT,
		CRITCP_MAIL_UPDATE_ATOM_CUESHEET_BINARYDATA,
		CRITCP_MAIL_UPDATE_ATOM_CUESHEET_BINARYDATA_RESULT,
		CRITCP_MAIL_REQUEST_SEND_ACB,
		CRITCP_MAIL_REQUEST_SEND_ACB_RESULT,                            /* 70 */
		CRITCP_MAIL_SHOW_WINDOW,
		CRITCP_MAIL_SHOW_WINDOW_RESULT,
		CRITCP_MAIL_MONITOR_OVERWRITE_ACB,
		CRITCP_MAIL_MONITOR_OVERWRITE_ACB_RESULT,
		CRITCP_MAIL_OVERWRITE_ACB,
		CRITCP_MAIL_OVERWRITE_ACB_RESULT,
		CRITCP_MAIL_OVERWRITE_ACB_DATA,
		CRITCP_MAIL_OVERWRITE_ACB_DATA_RESULT,
		CRITCP_MAIL_MONITOR_RELEASE_ACB,
		CRITCP_MAIL_MONITOR_RELEASE_ACB_RESULT,                         /* 80 */
		CRITCP_MAIL_SEND_DUMMY,
		CRITCP_MAIL_SEND_DUMMY_RESULT,
		CRITCP_MAIL_UPDATE_PART_OF_ACF,
		CRITCP_MAIL_UPDATE_PART_OF_ACF_RESULT,
		CRITCP_MAIL_UPDATE_PART_OF_ACF_DATA,
		CRITCP_MAIL_UPDATE_PART_OF_ACF_DATA_RESULT,
		CRITCP_MAIL_MONITOR_OVERWRITE_ACF,
		CRITCP_MAIL_MONITOR_OVERWRITE_ACF_RESULT,
		CRITCP_MAIL_OVERWRITE_ACF,
		CRITCP_MAIL_OVERWRITE_ACF_RESULT,                               /* 90 */
		CRITCP_MAIL_OVERWRITE_ACF_DATA,
		CRITCP_MAIL_OVERWRITE_ACF_DATA_RESULT,
		CRITCP_MAIL_MONITOR_ATOMEXASR_BUS_INFO,
		CRITCP_MAIL_MONITOR_ATOMEXASR_BUS_INFO_RESULT,
		CRITCP_MAIL_MONITOR_ACF_LINKAGE_RACK_INFO,
		CRITCP_MAIL_MONITOR_ATOMEX_ERR_INFO_RESULT,
		CRITCP_MAIL_MONITOR_ATOMEXASR_DSP_SPECTRA,
		CRITCP_MAIL_MONITOR_ATOMEXASR_DSP_SPECTRA_RESULT,
		CRITCP_MAIL_START_BOUNCE,
		CRITCP_MAIL_START_BOUNCE_RESULT,                                /* 100 */
		CRITCP_MAIL_STOP_BOUNCE,
		CRITCP_MAIL_STOP_BOUNCE_RESULT,
		CRITCP_MAIL_MONITOR_SEQUENCER_CALLBACK,
		CRITCP_MAIL_MONITOR_SEQUENCER_CALLBACK_RESULT,
		CRITCP_MAIL_MONITOR_ATOM_TRACK_MUTE,
		CRITCP_MAIL_MONITOR_ATOM_TRACK_MUTE_RESULT,
		CRITCP_MAIL_MONITOR_PREVIEW_INFO,
		CRITCP_MAIL_MONITOR_PREVIEW_INFO_RESULT,
		CRITCP_MAIL_MONITOR_STREAM_AWB_INFO,
		CRITCP_MAIL_MONITOR_STREAM_AWB_INFO_RESULT,                     /* 110 */
		CRITCP_MAIL_CONNECTION_VERSION,
		CRITCP_MAIL_CONNECTION_VERSION_TO_PROFILER,
		CRITCP_MAIL_MONITOR_ATOM_EXPLAYBACKINFO_PLAY_POSITION_WITH_PLAY_STATUS,
		CRITCP_MAIL_MONITOR_REJECT_OVERWRITE_ACB,
		CRITCP_MAIL_MONITOR_ATOMEXASR_LOUDNESS_INFO,
	};

	/* ログ用API識別番号 */
	protected const int logFuncBaseNum = 2000;
	protected enum LogFuncId {
		LOG_COMMAND_Non = 0,                                /* Non */
		LOG_COMMAND_Ex_Initialize,                          /* criAtomEx_Initialize */
		LOG_COMMAND_Ex_Finalize,                            /* criAtomEx_Finalize */
		LOG_COMMAND_ExAsr_Initialize,                       /* criAtomExAsr_Initialize */
		LOG_COMMAND_ExAsr_Finalize,                         /* criAtomExAsr_Finalize */
		LOG_COMMAND_ExHcaMx_Initialize,                     /* criAtomExHcaMx_Initialize */
		LOG_COMMAND_ExHcaMx_Finalize,                       /* criAtomExHcaMx_Finalize */
		LOG_COMMAND_Dbas_Create,                            /* criAtomDbas_Create */
		LOG_COMMAND_Dbas_Destroy,                           /* criAtomDbas_Destroy */
		LOG_COMMAND_StreamingCache_Create,                  /* criAtomStreamingCache_Create */
		LOG_COMMAND_StreamingCache_Destroy,                 /* criAtomStreamingCache_Destroy */
		LOG_COMMAND_ExVoicePool_AllocateStandardVoicePool,  /* criAtomExVoicePool_AllocateStandardVoicePool */
		LOG_COMMAND_ExVoicePool_AllocateAdxVoicePool,       /* criAtomExVoicePool_AllocateAdxVoicePool */
		LOG_COMMAND_ExVoicePool_AllocateAhxVoicePool,       /* criAtomExVoicePool_AllocateAhxVoicePool */
		LOG_COMMAND_ExVoicePool_AllocateHcaVoicePool,       /* criAtomExVoicePool_AllocateHcaVoicePool */
		LOG_COMMAND_ExVoicePool_AllocateHcaMxVoicePool,     /* criAtomExVoicePool_AllocateHcaMxVoicePool */
		LOG_COMMAND_ExVoicePool_AllocateWaveVoicePool,      /* criAtomExVoicePool_AllocateWaveVoicePool */
		LOG_COMMAND_ExVoicePool_AllocateRawPcmVoicePool,    /* criAtomExVoicePool_AllocateRawPcmVoicePool */
		LOG_COMMAND_ExVoicePool_AllocateAdpcmVoicePool_WII, /* criAtomExVoicePool_AllocateAdpcmVoicePool_WII */
		LOG_COMMAND_ExVoicePool_AllocateVagVoicePool_PSP,   /* criAtomExVoicePool_AllocateVagVoicePool_PSP */
		LOG_COMMAND_ExVoicePool_AllocateAdpcmVoicePool_3DS, /* criAtomExVoicePool_AllocateAdpcmVoicePool_3DS */
		LOG_COMMAND_ExVoicePool_AllocateVagVoicePool_VITA,  /* criAtomExVoicePool_AllocateVagVoicePool_VITA */
		LOG_COMMAND_ExVoicePool_AllocateAtrac3VoicePool_PSP,/* criAtomExVoicePool_AllocateAtrac3VoicePool_PSP */
		LOG_COMMAND_ExVoicePool_AllocateAt9VoicePool_VITA,  /* criAtomExVoicePool_AllocateAt9VoicePool_VITA */
		LOG_COMMAND_ExVoicePool_Free,                       /* criAtomExVoicePool_Free */
		LOG_COMMAND_ExPlayer_Create,                        /* criAtomExPlayer_Create */
		LOG_COMMAND_ExPlayer_Destroy,                       /* criAtomExPlayer_Destroy */
		LOG_COMMAND_ExTween_Create,                         /* criAtomExTween_Create */
		LOG_COMMAND_ExTween_Destroy,                        /* criAtomExTween_Destroy */
		LOG_COMMAND_Decrypter_Create,                       /* criAtomDecrypter_Create */
		LOG_COMMAND_Decrypter_Destroy,                      /* criAtomDecrypter_Destroy */
		LOG_COMMAND_Ex3dSource_Create,                      /* criAtomEx3dSource_Create */
		LOG_COMMAND_Ex3dSource_Destroy,                     /* criAtomEx3dSource_Destroy */
		LOG_COMMAND_Ex3dListener_Create,                    /* criAtomEx3dListener_Create */
		LOG_COMMAND_Ex3dListener_Destroy,                   /* criAtomEx3dListener_Destroy */
		LOG_COMMAND_ExPlayer_AttachFader,                   /* criAtomExPlayer_AttachFader */
		LOG_COMMAND_ExPlayer_DetachFader,                   /* criAtomExPlayer_DetachFader */
		LOG_COMMAND_Ex_RegisterAcfConfig,                   /* criAtomEx_RegisterAcfConfig */
		LOG_COMMAND_Ex_RegisterAcfData,                     /* criAtomEx_RegisterAcfData */
		LOG_COMMAND_Ex_RegisterAcfFile,                     /* criAtomEx_RegisterAcfFile */
		LOG_COMMAND_Ex_RegisterAcfFileById,                 /* criAtomEx_RegisterAcfFileById */
		LOG_COMMAND_Ex_UnregisterAcf,                       /* criAtomEx_UnregisterAcf */
		LOG_COMMAND_ExAcb_LoadAcbData,                      /* criAtomExAcb_LoadAcbData */
		LOG_COMMAND_ExAcb_LoadAcbDataById,                  /* criAtomExAcb_LoadAcbDataById */
		LOG_COMMAND_ExAcb_LoadAcbFile,                      /* criAtomExAcb_LoadAcbFile */
		LOG_COMMAND_ExAcb_LoadAcbFileById,                  /* criAtomExAcb_LoadAcbFileById */
		LOG_COMMAND_ExAcb_Release,                          /* criAtomExAcb_Release */
		LOG_COMMAND_ExAcb_ReleaseAll,                       /* criAtomExAcb_ReleaseAll */
		LOG_COMMAND_ExPlayer_Start,                         /* criAtomExPlayer_Start */
		LOG_COMMAND_ExPlayer_Prepare,                       /* criAtomExPlayer_Prepare */
		LOG_COMMAND_ExPlayer_Stop,                          /* criAtomExPlayer_Stop */
		LOG_COMMAND_ExPlayer_StopWithoutReleaseTime,        /* criAtomExPlayer_StopWithoutReleaseTime */
		LOG_COMMAND_ExPlayback_Stop,                        /* criAtomExPlayback_Stop */
		LOG_COMMAND_ExPlayback_StopWithoutReleaseTime,      /* criAtomExPlayback_StopWithoutReleaseTime */
		LOG_COMMAND_ExPlayer_Pause,                         /* criAtomExPlayer_Pause */
		LOG_COMMAND_ExPlayer_Resume,                        /* criAtomExPlayer_Resume */
		LOG_COMMAND_ExPlayback_Pause,                       /* criAtomExPlayback_Pause */
		LOG_COMMAND_ExPlayback_Resume,                      /* criAtomExPlayback_Resume */
		LOG_COMMAND_ExPlaybackInfo_AllocateInfo,            /* criAtomExPlaybackInfo_AllocateInfo */
		LOG_COMMAND_ExPlaybackInfo_FreeInfo,                /* criAtomExPlaybackInfo_FreeInfo */
		LOG_COMMAND_Error,                                  /* criError */
		LOG_COMMAND_ExPlaybackInfo_AddSound,                /* criAtomExPlaybackInfo_AddSound */
		LOG_COMMAND_ExPlaybackSound_FreeSound,              /* criAtomExPlaybackSound_FreeSound */
		LOG_COMMAND_SoundPlayer_Start,                      /* criAtomSoundPlayer_Start */
		LOG_COMMAND_SoundPlayer_Stop,                       /* criAtomSoundPlayer_Stop */
		LOG_COMMAND_SoundPlayer_StopWithoutRelease,         /* criAtomSoundPlayer_StopWithoutRelease */
		LOG_COMMAND_SoundPlayer_PausePlayback,              /* criAtomSoundPlayer_PausePlayback */
		LOG_COMMAND_SoundPlayer_SetWaveId,                  /* criAtomSoundPlayer_SetWaveId */
		LOG_COMMAND_SoundPlayer_SetContentId,               /* criAtomSoundPlayer_SetContentId */
		LOG_COMMAND_SoundPlayer_SetData,                    /* criAtomSoundPlayer_SetData */
		LOG_COMMAND_SoundPlayer_SetFileStringPointer,       /* criAtomSoundPlayer_SetFileStringPointer */
		LOG_COMMAND_ExPlayer_SetCueId,                      /* criAtomExPlayer_SetCueId */
		LOG_COMMAND_ExPlayer_SetCueName,                    /* criAtomExPlayer_SetCueName */
		LOG_COMMAND_ExPlayer_SetCueIndex,                   /* criAtomExPlayer_SetCueIndex */
		LOG_COMMAND_ExPlayer_SetData,                       /* criAtomExPlayer_SetData */
		LOG_COMMAND_ExPlayer_SetFile,                       /* criAtomExPlayer_SetFile */
		LOG_COMMAND_ExPlayer_SetContentId,                  /* criAtomExPlayer_SetContentId */
		LOG_COMMAND_ExPlayer_SetWaveId,                     /* criAtomExPlayer_SetWaveId */
		LOG_COMMAND_DbasId,                                 /* CriAtomDbasId */
		LOG_COMMAND_StreamingCacheId,                       /* CriAtomStreamingCacheId */
		LOG_COMMAND_ExVoicePoolHn,                          /* CriAtomExVoicePoolHn */
		LOG_COMMAND_ExPlayerHn,                             /* CriAtomExPlayerHn */
		LOG_COMMAND_ExTweenHn,                              /* CriAtomExTweenHn */
		LOG_COMMAND_DecrypterHn,                            /* CriAtomDecrypterHn */
		LOG_COMMAND_Ex3dSourceHn,                           /* CriAtomEx3dSourceHn */
		LOG_COMMAND_Ex3dListenerHn,                         /* CriAtomEx3dListenerHn */
		LOG_COMMAND_ExPlaybackId,                           /* CriAtomExPlaybackId */
		LOG_COMMAND_ExConfig,                               /* CriAtomExConfig */
		LOG_COMMAND_ExAsrConfig,                            /* CriAtomExAsrConfig */
		LOG_COMMAND_ExHcaMxConfig,                          /* CriAtomExHcaMxConfig */
		LOG_COMMAND_DbasConfig,                             /* CriAtomDbasConfig */
		LOG_COMMAND_StreamingCacheConfig,                   /* CriAtomStreamingCacheConfig */
		LOG_COMMAND_ExStandardVoicePoolConfig,              /* CriAtomExStandardVoicePoolConfig */
		LOG_COMMAND_ExAdxVoicePoolConfig,                   /* CriAtomExAdxVoicePoolConfig */
		LOG_COMMAND_ExAhxVoicePoolConfig,                   /* CriAtomExAhxVoicePoolConfig */
		LOG_COMMAND_ExHcaVoicePoolConfig,                   /* CriAtomExHcaVoicePoolConfig */
		LOG_COMMAND_ExHcaMxVoicePoolConfig,                 /* CriAtomExHcaMxVoicePoolConfig */
		LOG_COMMAND_ExWaveVoicePoolConfig,                  /* CriAtomExWaveVoicePoolConfig */
		LOG_COMMAND_ExRawPcmVoicePoolConfig,                /* CriAtomExRawPcmVoicePoolConfig */
		LOG_COMMAND_ExAdpcmVoicePoolConfig_3DS,             /* CriAtomExAdpcmVoicePoolConfig_3DS */
		LOG_COMMAND_ExAdpcmVoicePoolConfig_WII,             /* CriAtomExAdpcmVoicePoolConfig_WII */
		LOG_COMMAND_ExVagVoicePoolConfig_PSP,               /* CriAtomExVagVoicePoolConfig_PSP */
		LOG_COMMAND_ExAtrac3VoicePoolConfig_PSP,            /* CriAtomExAtrac3VoicePoolConfig_PSP */
		LOG_COMMAND_ExVagVoicePoolConfig_VITA,              /* CriAtomExVagVoicePoolConfig_VITA */
		LOG_COMMAND_ExAt9VoicePoolConfig_VITA,              /* CriAtomExAt9VoicePoolConfig_VITA */
		LOG_COMMAND_ExPlayerConfig,                         /* CriAtomExPlayerConfig */
		LOG_COMMAND_ExTweenConfig,                          /* CriAtomExTweenConfig */
		LOG_COMMAND_DecrypterConfig,                        /* CriAtomDecrypterConfig */
		LOG_COMMAND_ExAcfConfig,                            /* CriAtomExAcfConfig */
		LOG_COMMAND_Ex3dSourceConfig,                       /* CriAtomEx3dSourceConfig */
		LOG_COMMAND_Ex3dListenerConfig,                     /* CriAtomEx3dListenerConfig */
		LOG_COMMAND_ExFaderConfig,                          /* CriAtomExFaderConfig */
		LOG_COMMAND_ExAcbHn,                                /* CriAtomExAcbHn */
		LOG_COMMAND_ExFaderHn,                              /* CriAtomExFaderHn */
		LOG_COMMAND_PlayerPoolPlayerInfo,                   /* CriAtomPlayerPoolPlayerInfo */
		LOG_COMMAND_PlayerPool_ReleasePlayer,               /* criAtomPlayerPool_ReleasePlayer */
		LOG_COMMAND_SoundPlayer_Allocate_Element,           /* criatomsoundplayer_allocate_element */
		LOG_COMMAND_ExCategory_CancelCuePlayback,           /* criAtomExCategory_CancelCuePlayback */
		LOG_COMMAND_ExCue_StopByLimit,                      /* criAtomExCue_StopByLimit */
		LOG_COMMAND_ExCue_CancelCuePlayback,                /* criAtomExCue_CancelCuePlayback */
		LOG_COMMAND_CancelPlaybackByProbability,            /* criAtomExCue_CancelPlaybackByProbability */
		LOG_COMMAND_CancelPlaybackByCueTypeRandom,          /* criAtomExCue_CancelPlaybackByyCueTypeRandom */
		LOG_COMMAND_CancelPlaybackByCueTypeSwitch,          /* criAtomExCue_CancelPlaybackByyCueTypeSwitch */
		LOG_COMMAND_ExCategory_IncrementNumPlaybackCues,    /* criAtomExCategory_IncrementNumPlaybackCues */
		LOG_COMMAND_ExCategory_DecrementNumPlaybackCues,    /* criAtomExCategory_DecrementNumPlaybackCues */
		LOG_COMMAND_SoundVoice_Volume,                      /* criatomsoundvoice_parameter_update_on_changed:volume */
		LOG_COMMAND_SoundVoice_FreeVoice,                   /* criatomsoundvoice_FreeVoice */
		LOG_COMMAND_ExSequence_GetFreeBlock,                /* criAtomExSequence_GetFreeSequenceBlock */
		LOG_COMMAND_ExSequence_SetFreeBlock,                /* criAtomExSequence_SetFreeSequenceBlock */
		LOG_COMMAND_ExSequence_GetFreeSeqInfo,              /* criAtomExSequence_GetFreeSequenceInfo */
		LOG_COMMAND_ExSequence_SetFreeSeqInfo,              /* criAtomExSequence_SetFreeSequenceInfo */
		LOG_COMMAND_SoundVoice_Allocate,                    /* criAtomSoundVoice_AllocateVoice */
		LOG_COMMAND_GetAisacDestinationValue,               /* criAtomCueSheet_GetAisacDestinationValue */
		LOG_COMMAND_SequenceTrack_Mute,                     /* criAtomSequenceTrack_Mute */
		LOG_COMMAND_Preview_RequestSendLog_DELETED,         /* CriAtomMonitorLoc::MakeLogSendRequestPacket */
		LOG_COMMAND_RequestSendAcb,                         /* CriAtomMonitorLoc::MakeRequestPacket4SendAcb */
		LOG_COMMAND_Monitor_MakeClosePacket,                /* CriAtomMonitorLoc::MakeClosePacket */
		LOG_COMMAND_Monitor_MakeSendDataResultPacket,       /* CriAtomMonitorLoc::MakeSendDataResultPacket */
		LOG_COMMAND_CpuLoadAndNumUsedVoices,                /* CriAtomMonitorLoc::MakePerformanceInfoPacket */
		LOG_COMMAND_SequenceCallback,                       /* SequenceCallback */
		LOG_COMMAND_OverwriteAcf,                           /* CriAtomMonitorLoc::MakeRequestPacket4OverwriteAcf */
		LOG_COMMAND_StartLogging,                           /* CriAtomMonitorLoc::MakeLoggingStartTimePacket */
		LOG_COMMAND_SequenceLoopInfo,                       /* SequenceLoopInfo */
		LOG_COMMAND_Ex3dSource_Update,                      /* criAtomEx3dSource_Update */
		LOG_COMMAND_Ex3dListener_Update,                    /* criAtomEx3dListener_Update */
		LOG_COMMAND_ExVoicePool_AllocateAiffVoicePool,      /* criAtomExVoicePool_AllocateAiffVoicePool */
		LOG_COMMAND_ExAiffVoicePoolConfig,                  /* CriAtomExAiffVoicePoolConfig */
		LOG_COMMAND_ExVoicePool_AllocateAt9VoicePool_PS4,   /* criAtomExVoicePool_AllocateAt9VoicePool_PS4 */
		LOG_COMMAND_ExAt9VoicePoolConfig_PS4,               /* CriAtomExAt9VoicePoolConfig_PS4 */
		LOG_COMMAND_UserLog,                                /* criAtomExMonitor_OutputUserLog */
		LOG_COMMAND_ExCategoryConfig,                       /* CriAtomExCategoryConfig */
		LOG_COMMAND_SoundVoice_KillByLimit,                 /* criatomsoundvoice_KillByLimit */
		LOG_COMMAND_SoundVoice_Virtualize,                  /* criatomsoundvoice_Virtualize */
		LOG_COMMAND_SoundVoice_UnVirtualize,                /* criatomsoundvoice_UnVirtualize */
		LOG_COMMAND_AsrBusAnalyzeInfo,                      /* criAtomPreview_MakeAsrBusInfoPacket */
		LOG_COMMAND_LoudnessInfo,                           /* criAtomPreview_MakeLoudnessInfoPacket */
		LOG_COMMAND_StreamingInfo,                          /* criAtomPreview_MakeStreamingInfoPacket */
		LOG_COMMAND_PlayerPool_NumVoices,                   /* criAtomPreview_MakeLogPacketNumVoiceInVoicePool */
		LOG_COMMAND_ExVoicePool_AllocateAdpcmVoicePool_WIIU,/* criAtomExVoicePool_AllocateAdpcmVoicePool_WIIU */
		LOG_COMMAND_ExAdpcmVoicePoolConfig_WIIU,            /* CriAtomExAdpcmVoicePoolConfig_WIIU */
		LOG_COMMAND_StreamTypeMemory,                       /* StreamTypeMemory */
		LOG_COMMAND_StreamTypeStream,                       /* StreamTypeStream */
		LOG_COMMAND_StreamTypeZeroLatencyStream,            /* StreamTypeZeroLatencyStream */
		LOG_COMMAND_AsrBusAnalyzeInfoAllCh,                 /* criAtomPreview_MakeAsrBusInfoPacketForProfiler */
		LOG_COMMAND_ExPlayer_SetAisacControlValue,          /* criAtomExPlayer_SetAisacControlByName, ById */
		LOG_COMMAND_SequenceTrack_Start,                    /* SetupSeuenceTrack */
		LOG_COMMAND_SequenceTrack_Stop,                     /* criAtomSequence_FreeSequenceTrack */
		LOG_COMMAND_SoundPlayer_SetVibrationId,             /* criAtomSoundPlayer_SoundPlayer_SetVibrationId */
		LOG_COMMAND_ExGameVariableConfig,                   /* CriAtomExGameVariableConfig */
		LOG_COMMAND_SetGameVariable,                        /* SetGameVariableByname, ById */
		LOG_COMMAND_ExVibrationVoicePoolConfig,             /* CriAtomExVibrationVoicePoolConfig */
		LOG_COMMAND_ExVoicePool_AllocateVibrationVoicePool, /* criAtomExVoicePool_AllocateVibrationVoicePool */
		LOG_COMMAND_ExVoicePlayer_Pause,                    /* criAtomExPlayer_Pause */
		LOG_COMMAND_SoundPlayer_SetVibrationName,           /* criAtomSoundPlayer_SoundPlayer_SetVibrationName */
		LOG_COMMAND_ExCategory_Stop,                        /* criAtomExCategory_Stop */
		LOG_COMMAND_ExCategory_StopWithoutReleaseTime,      /* criAtomExCategory_StopWithoutReleaseTime */
		LOG_COMMAND_Ex3dTransceiver_Create,					/* criAtomEx3dTransceiver_Create */
		LOG_COMMAND_Ex3dTransceiver_Destroy,				/* criAtomEx3dTransceiver_Destroy */
		LOG_COMMAND_Ex3dTransceiverHn,						/* criAtomEx3dTransceiverHn */
		LOG_COMMAND_Ex3dTransceiverConfig,					/* criAtomEx3dTransceiverConfig */
		LOG_COMMAND_Ex3dTransceiver_UpdateInput,			/* criAtomEx3dTransceiver_UpdateInput */
		LOG_COMMAND_Ex3dTransceiver_UpdateOutput,			/* criAtomEx3dTransceiver_UpdateOutput */
		LOG_COMMAND_SoundVoice_CalcFinalVoiceParamAndSilentVolumeCore,	/* criAtomVoice_CalcFinalVoiceParamAndSilentVolumeCore */
		LOG_COMMAND_AdmPlayer_Create,						/* criAtomAdmPlayer_Create */
		LOG_COMMAND_AdmPlayer_Destroy,						/* criAtomAdmPlayer_Destroy */
		LOG_COMMAND_AdmPlayer_InternalStart,				/* criatomadmplayer_internal_start */	/* フェーズ単位の再生処理 */
		LOG_COMMAND_AdmPlayer_Start,						/* criAtomAdmPlayer_Start */
		LOG_COMMAND_AdmPlayer_Stop,							/* criAtomAdmPlayer_Stop */
		LOG_COMMAND_AdmPlayer_StopByPhraseEnd,				/* criAtomAdmPlayer_StopByPhraseEnd */
		LOG_COMMAND_AsrRack_CreateForAcfLinkage,			/* criAtomExAsrRack_CreateForAcfLinkage */
		LOG_COMMAND_AsrRack_DestroyForAcfLinkage,			/* criAtomExAsrRack_DestroyForAcfLinkage */
		LOG_COMMAND_LoudnessInfoWithRackId,					/* criAtomPreview_MakeLoudnessInfoPacketWithRackId */
		LOG_COMMAND_AsrBusAnalyzeInfoAllChWithRackId,		/* criAtomPreview_MakeAsrBusInfoPacketForProfilerWithRackId */
		LOG_COMMAND_AsrRackConfig,							/* CriAtomAsrRackConfig */
		LOG_COMMAND_AsrRack_Create,							/* criAsrRack_Create */
		LOG_COMMAND_AsrRack_Destroy,						/* criAsrRack_Destroy */
		LOG_COMMAND_SoundVoice_KillByLimit_2,				/* criatomsoundvoice_KillByLimit のパラメーター拡張版 */
		LOG_COMMAND_ExPlayer_Set3dSourceHn,					/* criAtomExPlayer_Set3dSourceHn */
		LOG_COMMAND_ExPlayer_Set3dListenerHn,				/* criAtomExPlayer_Set3dListenerHn */
		LOG_COMMAND_ExPlayer_UpdateAll,						/* criAtomExPlayer_UpdateAll */
		LOG_COMMAND_ExCategory_MuteByName,					/* criAtomExCategory_MuteByName */
		LOG_COMMAND_ExCategory_SoloByName,					/* criAtomExCategory_SoloByName */
		LOG_COMMAND_ExPlayer_SetParameterFloat32,			/* criAtomExPlayer_SetParameterFloat32 */
		LOG_COMMAND_ExPlayer_SetParameterSint32,			/* criAtomExPlayer_SetParameterSint32 */
		LOG_COMMAND_ExPlayer_SetParameterUint32,            /* criAtomExPlayer_SetParameterUint32 */
		LOG_COMMAND_ExPlayback_SetNextBlockIndex,			/* criAtomExPlayback_SetNextBlockIndex */
		LOG_COMMAND_CancelPlaybackCouldNotSetupTrack,       /* criAtomExCue_SetupSequenceTrack での再生キャンセル通知 */
		LOG_COMMAND_ChangesIgnored,                         /* APIによる状態変更がエラーなしにスキップされた */
		LOG_COMMAND_ExCategory_MuteById,                    /* criAtomExCategory_MuteById */
		LOG_COMMAND_ExCategory_SoloById,                    /* criAtomExCategory_SoloById */
		LOG_COMMAND_ExCategory_SetVolumeById,               /* criAtomExCategory_SetVolumeById */
		LOG_COMMAND_ExCategory_SetVolumeByName,             /* criAtomExCategory_SetVolumeByName */
		LOG_COMMAND_ExPlayer_AttachTween,                   /* criAtomExPlayer_AttachTween */
		LOG_COMMAND_ExTween_MoveTo,                         /* criAtomExTween_MoveTo */
		LOG_COMMAND_ExTween_MoveFrom,                       /* criAtomExTween_MoveFrom */
		LOG_COMMAND_ExTween_Stop,                           /* criAtomExTween_Stop */
		LOG_COMMAND_ExTween_Reset,                          /* criAtomExTween_Reset */
		LOG_COMMAND_ExPlayer_DetachTween,                   /* criAtomExPlayer_DetachTween */
		LOG_COMMAND_Ex_AttachDspBusSetting,                 /* criAtomEx_AttachDspBusSetting */
		LOG_COMMAND_Ex_DetachDspBusSetting,                 /* criAtomEx_DetachDspBusSetting */
		LOG_COMMAND_Ex_ApplyDspBusSnapshot,                 /* criAtomEx_ApplyDspBusSnapshot */
		LOG_COMMAND_ExCategory_SetAisacControlById,         /* criAtomExCategory_SetAisacControlById */
		LOG_COMMAND_ExCategory_SetAisacControlByName,       /* criAtomExCategory_SetAisacControlByName */
		LOG_COMMAND_ExAcf_SetGlobalLabelToSelectorByName,   /* criAtomExAcf_SetGlobalLabelToSelectorByName */
		LOG_COMMAND_ExAcf_SetGlobalLabelToSelectorByIndex,  /* criAtomExAcf_SetGlobalLabelToSelectorByIndex */
		LOG_COMMAND_ExCategory_SetFadeInTimeById,           /* criAtomExCategory_SetFadeInTimeById */
		LOG_COMMAND_ExCategory_SetFadeInTimeByName,         /* criAtomExCategory_SetFadeInTimeByName */
		LOG_COMMAND_ExCategory_SetFadeOutTimeById,          /* criAtomExCategory_SetFadeOutTimeById */
		LOG_COMMAND_ExCategory_SetFadeOutTimeByName,        /* criAtomExCategory_SetFadeOutTimeByName */
		LOG_COMMAND_ExCategory_AttachAisacById,             /* criAtomExCategory_AttachAisacById */
		LOG_COMMAND_ExCategory_AttachAisacByName,           /* criAtomExCategory_AttachAisacByName */
		LOG_COMMAND_ExCategory_DetachAisacById,             /* criAtomExCategory_DetachAisacById */
		LOG_COMMAND_ExCategory_DetachAisacByName,           /* criAtomExCategory_DetachAisacByName */
		LOG_COMMAND_ExCategory_DetachAisacAllById,          /* criAtomExCategory_DetachAisacAllById */
		LOG_COMMAND_ExCategory_DetachAisacAllByName,        /* criAtomExCategory_DetachAisacAllByName */
		LOG_COMMAND_ExPlayer_StopAllPlayers,                /* criAtomExPlayer_StopAllPlayers */
		LOG_COMMAND_ExPlayer_StopAllPlayersWithoutReleaseTime, /* criAtomExPlayer_StopAllPlayersWithoutReleaseTime */
		LOG_COMMAND_ExPlayer_SetVoicePoolIdentifier,        /* criAtomExPlayer_SetVoicePoolIdentifier */
		LOG_COMMAND_ExPlayer_SetAsrRackId,                  /* criAtomExPlayer_SetAsrRackId */
		LOG_COMMAND_ExPlayer_SetStartTime,                  /* criAtomExPlayer_SetStartTime */
		LOG_COMMAND_ExPlayer_Update,                        /* criAtomExPlayer_Update */
		LOG_COMMAND_ExPlayer_ResetParameters,               /* criAtomExPlayer_ResetParameters */
		LOG_COMMAND_ExPlayer_SetVolume,                     /* criAtomExPlayer_SetVolume */
		LOG_COMMAND_ExPlayer_SetPitch,                      /* criAtomExPlayer_SetPitch */
		LOG_COMMAND_ExPlayer_SetMaxPitch,                   /* criAtomExPlayer_SetMaxPitch */
		LOG_COMMAND_ExPlayer_SetPan3dAngle,                 /* criAtomExPlayer_SetPan3dAngle */
		LOG_COMMAND_ExPlayer_SetPan3dInteriorDistance,      /* criAtomExPlayer_SetPan3dInteriorDistance */
		LOG_COMMAND_ExPlayer_SetPan3dVolume,                /* criAtomExPlayer_SetPan3dVolume */
		LOG_COMMAND_ExPlayer_SetPanType,                    /* criAtomExPlayer_SetPanType */
		LOG_COMMAND_ExPlayer_SetPanSpeakerType,             /* criAtomExPlayer_SetPanSpeakerType */
		LOG_COMMAND_ExPlayer_SetPanAngleType,               /* criAtomExPlayer_SetPanAngleType */
		LOG_COMMAND_ExPlayer_SetPanSpread,                  /* criAtomExPlayer_SetPanSpread */
		LOG_COMMAND_ExPlayer_SetSendLevel,                  /* criAtomExPlayer_SetSendLevel */
		LOG_COMMAND_ExPlayer_SetBusSendLevelByName,         /* criAtomExPlayer_SetBusSendLevelByName */
		LOG_COMMAND_ExPlayer_ResetBusSends,                 /* criAtomExPlayer_ResetBusSends */
		LOG_COMMAND_ExPlayer_SetBusSendLevelOffsetByName,   /* criAtomExPlayer_SetBusSendLevelOffsetByName */
		LOG_COMMAND_ExPlayer_SetBandpassFilterParameters,   /* criAtomExPlayer_SetBandpassFilterParameters */
		LOG_COMMAND_ExPlayer_SetBiquadFilterParameters,     /* criAtomExPlayer_SetBiquadFilterParameters */
		LOG_COMMAND_ExPlayer_ClearAisacControls,            /* criAtomExPlayer_ClearAisacControls */
		LOG_COMMAND_ExPlayer_SetVoicePriority,              /* criAtomExPlayer_SetVoicePriority */
		LOG_COMMAND_ExPlayer_Set3dSourceListHn,             /* criAtomExPlayer_Set3dSourceListHn */
		LOG_COMMAND_ExPlayer_SetEnvelopeAttackTime,         /* criAtomExPlayer_SetEnvelopeAttackTime */
		LOG_COMMAND_ExPlayer_SetEnvelopeAttackCurve,        /* criAtomExPlayer_SetEnvelopeAttackCurve */
		LOG_COMMAND_ExPlayer_SetEnvelopeHoldTime,           /* criAtomExPlayer_SetEnvelopeHoldTime */
		LOG_COMMAND_ExPlayer_SetEnvelopeDecayTime,          /* criAtomExPlayer_SetEnvelopeDecayTime */
		LOG_COMMAND_ExPlayer_SetEnvelopeDecayCurve,         /* criAtomExPlayer_SetEnvelopeDecayCurve */
		LOG_COMMAND_ExPlayer_SetEnvelopeReleaseTime,        /* criAtomExPlayer_SetEnvelopeReleaseTime */
		LOG_COMMAND_ExPlayer_SetEnvelopeReleaseCurve,       /* criAtomExPlayer_SetEnvelopeReleaseCurve */
		LOG_COMMAND_ExPlayer_SetEnvelopeSustainLevel,       /* criAtomExPlayer_SetEnvelopeSustainLevel */
		LOG_COMMAND_ExPlayer_AttachAisac,                   /* criAtomExPlayer_AttachAisac */
		LOG_COMMAND_ExPlayer_AttachAisacByIndex,            /* criAtomExPlayer_AttachAisacByIndex */
		LOG_COMMAND_ExPlayer_DetachAisac,                   /* criAtomExPlayer_DetachAisac */
		LOG_COMMAND_ExPlayer_DetachAisacByIndex,            /* criAtomExPlayer_DetachAisacByIndex */
		LOG_COMMAND_ExPlayer_DetachAisacAll,                /* criAtomExPlayer_DetachAisacAll */
		LOG_COMMAND_ExPlayer_UnsetCategory,                 /* criAtomExPlayer_UnsetCategory */
		LOG_COMMAND_ExPlayer_SetCuePriority,                /* criAtomExPlayer_SetCuePriority */
		LOG_COMMAND_ExPlayer_SetPreDelayTime,               /* criAtomExPlayer_SetPreDelayTime */
		LOG_COMMAND_ExPlayer_SetDataRequestCallback,        /* criAtomExPlayer_SetDataRequestCallback */
		LOG_COMMAND_ExPlayer_SetStreamingCacheId,           /* criAtomExPlayer_SetStreamingCacheId */
		LOG_COMMAND_ExPlayer_SetRandomSeed,                 /* criAtomExPlayer_SetRandomSeed */
		LOG_COMMAND_ExPlayer_SetFirstBlockIndex,            /* criAtomExPlayer_SetFirstBlockIndex */
		LOG_COMMAND_ExPlayer_SetSelectorLabel,              /* criAtomExPlayer_SetSelectorLabel */
		LOG_COMMAND_ExPlayer_UnsetSelectorLabel,            /* criAtomExPlayer_UnsetSelectorLabel */
		LOG_COMMAND_ExPlayer_ClearAllSelectorLabels,        /* criAtomExPlayer_ClearSelectorLabels */
		LOG_COMMAND_ExPlayer_SetFadeInStartOffset,          /* criAtomExPlayer_SetFadeInStartOffset*/
		LOG_COMMAND_ExPlayer_SetFadeInTime,                 /* criAtomExPlayer_SetFadeInTime */
		LOG_COMMAND_ExPlayer_SetFadeOutTime,                /* criAtomExPlayer_SetFadeOutTime */
		LOG_COMMAND_ExPlayer_SetFadeOutEndDelay,            /* criAtomExPlayer_SetFadeOutEndDelay */
		LOG_COMMAND_ExPlayer_ResetFaderParameters,          /* criAtomExPlayer_ResetFaderParameters */
		LOG_COMMAND_ExPlayer_EnumeratePlayers,              /* criAtomExPlayer_EnumeratePlayers */
		LOG_COMMAND_ExPlayer_EnumeratePlaybacks,            /* criAtomExPlayer_EnumeratePlaybacks */
		LOG_COMMAND_ExPlayer_SetFormat,                     /* criAtomExPlayer_SetFormat */
		LOG_COMMAND_ExPlayer_SetNumChannels,                /* criAtomExPlayer_SetNumChannels */
		LOG_COMMAND_ExPlayer_SetSamplingRate,               /* criAtomExPlayer_SetSamplingRate */
		LOG_COMMAND_ExPlayer_SetSoundRendererType,          /* criAtomExPlayer_SetSoundRendererType */
		LOG_COMMAND_ExPlayer_SetGroupNumber,                /* criAtomExPlayer_SetGroupNumber */
		LOG_COMMAND_ExPlayer_SetVoiceControlMethod,         /* criAtomExPlayer_SetVoiceControlMethod */
		LOG_COMMAND_ExPlayer_SetHcaMxMixerId,               /* criAtomExPlayer_SetHcaMxMixerId */
		LOG_COMMAND_ExPlayer_SetAsrRackIdArray,             /* criAtomExPlayer_SetAsrRackIdArray */
		LOG_COMMAND_ExPlayer_SetSyncPlaybackId,             /* criAtomExPlayer_SetSyncPlaybackId */
		LOG_COMMAND_ExPlayer_SetPlaybackRatio,              /* criAtomExPlayer_SetPlaybackRatio */
		LOG_COMMAND_ExPlayer_LimitLoopCount,                /* criAtomExPlayer_LimitLoopCount */
		LOG_COMMAND_ExPlayer_AddMixDownCenterVolumeOffset,  /* criAtomExPlayer_AddMixDownCenterVolumeOffset */
		LOG_COMMAND_ExPlayer_AddMixDownLfeVolumeOffset,     /* criAtomExPlayer_AddMixDownLfeVolumeOffset */
		LOG_COMMAND_ExPlayer_ChangeDefaultPanSpeakerType,   /* criAtomExPlayer_ChangeDefaultPanSpeakerType */
		LOG_COMMAND_ExPlayer_OverrideDefaultPanMethod,      /* criAtomExPlayer_OverrideDefaultPanMethod */
		LOG_COMMAND_ExPlayer_SetTrackInfo,                  /* criAtomExPlayer_SetTrackInfo */
		LOG_COMMAND_ExPlayer_SetTrackVolume,                /* criAtomExPlayer_SetTrackVolume */
		LOG_COMMAND_ExPlayer_SetSilentMode,                 /* criAtomExPlayer_SetSilentMode */
		LOG_COMMAND_ExPlayer_SetFilterCallback,             /* criAtomExPlayer_SetFilterCallback */
		LOG_COMMAND_ExPlayer_SetDspParameter,               /* criAtomExPlayer_SetDspParameter */
		LOG_COMMAND_ExPlayer_SetDspBypass,                  /* criAtomExPlayer_SetDspBypass */
		LOG_COMMAND_ExPlayer_SetBlockTransitionCallback,    /* criAtomExPlayer_SetBlockTransitionCallback */
		LOG_COMMAND_ExPlayer_SetDrySendLevel,               /* criAtomExPlayer_SetDrySendLevel */
		LOG_COMMAND_ExPlayer_SetPlaybackTrackInfoNotificationCallback, /* criAtomExPlayer_SetPlaybackTrackInfoNotificationCallback */
		LOG_COMMAND_ExPlayer_SetPlaybackEventCallback,      /* criAtomExPlayer_SetPlaybackEventCallback */
		LOG_COMMAND_ExPlayer_SetChannelConfig,              /* criAtomExPlayer_SetChannelConfig */
		LOG_COMMAND_Ex3dSource_ResetParameters,             /* criAtomEx3dSource_ResetParameters */
		LOG_COMMAND_Ex3dSource_SetPosition,                 /* criAtomEx3dSource_SetPosition */
		LOG_COMMAND_Ex3dSource_SetVelocity,                 /* criAtomEx3dSource_SetVelocity */
		LOG_COMMAND_Ex3dSource_SetOrientation,              /* criAtomEx3dSource_SetOrientation */
		LOG_COMMAND_Ex3dSource_SetConeParameter,            /* criAtomEx3dSource_SetConeParameter */
		LOG_COMMAND_Ex3dSource_ChangeDefaultConeParameter,  /* criAtomEx3dSource_ChangeDefaultConeParameter */
		LOG_COMMAND_Ex3dSource_SetMinMaxAttenuationDistance,/* criAtomEx3dSource_SetMinMaxAttenuationDistance */
		LOG_COMMAND_Ex3dSource_ChangeDefaultMinMaxAttenuationDistance, /* criAtomEx3dSource_ChangeDefaultMinMaxAttenuationDistance */
		LOG_COMMAND_Ex3dSource_SetInteriorPanField,         /* criAtomEx3dSource_SetInteriorPanField */
		LOG_COMMAND_Ex3dSource_ChangeDefaultInteriorPanField, /* criAtomEx3dSource_ChangeDefaultInteriorPanField */
		LOG_COMMAND_Ex3dSource_SetDopplerFactor,            /* criAtomEx3dSource_SetDopplerFactor */
		LOG_COMMAND_Ex3dSource_ChangeDefaultDopplerFactor,  /* criAtomEx3dSource_ChangeDefaultDopplerFactor */
		LOG_COMMAND_Ex3dSource_SetVolume,                   /* criAtomEx3dSource_SetVolume */
		LOG_COMMAND_Ex3dSource_ChangeDefaultVolume,         /* criAtomEx3dSource_ChangeDefaultVolume */
		LOG_COMMAND_Ex3dSource_SetMaxAngleAisacDelta,       /* criAtomEx3dSource_SetMaxAngleAisacDelta */
		LOG_COMMAND_Ex3dSource_SetDistanceAisacControlId,   /* criAtomEx3dSource_SetDistanceAisacControlId */
		LOG_COMMAND_Ex3dSource_SetListenerBasedAzimuthAngleAisacControlId,  /* criAtomEx3dSource_SetListenerBasedAzimuthAngleAisacControlId */
		LOG_COMMAND_Ex3dSource_SetListenerBasedElevationAngleAisacControlId,    /* criAtomEx3dSource_SetListenerBasedElevationAngleAisacControlId */
		LOG_COMMAND_Ex3dSource_SetSourceBasedAzimuthAngleAisacControlId,    /* criAtomEx3dSource_SetSourceBasedAzimuthAngleAisacControlId */
		LOG_COMMAND_Ex3dSource_SetSourceBasedElevationAngleAisacControlId,  /* criAtomEx3dSource_SetSourceBasedElevationAngleAisacControlId */
		LOG_COMMAND_Ex3dSource_Set3dRegionHn,               /* criAtomEx3dSource_Set3dRegionHn */
		LOG_COMMAND_Ex3dSource_SetRandomPositionConfig,     /* criAtomEx3dSource_SetRandomPositionConfig */
		LOG_COMMAND_Ex3dSource_SetRandomPositionCalculationCallback,    /* criAtomEx3dSource_SetRandomPositionCalculationCallback */
		LOG_COMMAND_Ex3dSource_SetRandomPositionList,       /* criAtomEx3dSource_SetRandomPositionList */
		LOG_COMMAND_Ex3dSource_SetRandomPositionResultCallback,     /* criAtomEx3dSource_SetRandomPositionResultCallback */
		LOG_COMMAND_Ex3dSourceList_Create,                  /* criAtomEx3dSourceList_Create */
		LOG_COMMAND_Ex3dSourceList_Destroy,                 /* criAtomEx3dSourceList_Destroy */
		LOG_COMMAND_Ex3dSourceList_Add,                     /* criAtomEx3dSourceList_Add */
		LOG_COMMAND_Ex3dSourceList_Remove,                  /* criAtomEx3dSourceList_Remove */
		LOG_COMMAND_Ex3dSourceList_RemoveAll,               /* criAtomEx3dSourceList_RemoveAll */
		LOG_COMMAND_Ex3dListener_ResetParameters,           /* criAtomEx3dListener_ResetParameters */
		LOG_COMMAND_Ex3dListener_SetPosition,               /* criAtomEx3dListener_SetPosition */
		LOG_COMMAND_Ex3dListener_SetVelocity,               /* criAtomEx3dListener_SetVelocity */
		LOG_COMMAND_Ex3dListener_SetOrientation,            /* criAtomEx3dListener_SetOrientation */
		LOG_COMMAND_Ex3dListener_SetDopplerMultiplier,      /* criAtomEx3dListener_SetDopplerMultiplier */
		LOG_COMMAND_Ex3dListener_SetFocusPoint,             /* criAtomEx3dListener_SetFocusPoint */
		LOG_COMMAND_Ex3dListener_SetDistanceFocusLevel,     /* criAtomEx3dListener_SetDistanceFocusLevel */
		LOG_COMMAND_Ex3dListener_SetDirectionFocusLevel,    /* criAtomEx3dListener_SetDirectionFocusLevel */
		LOG_COMMAND_Ex3dRegion_Create,                      /* criAtomEx3dRegion_Create */
		LOG_COMMAND_Ex3dRegionConfig,                       /* criAtomEx3dRegion_Create 内で使われるconfig */
		LOG_COMMAND_Ex3dRegion_Destroy,                     /* criAtomEx3dRegion_Destroy */
		LOG_COMMAND_Ex3dTransceiver_SetInputPosition,       /* criAtomEx3dTransceiver_SetInputPosition */
		LOG_COMMAND_Ex3dTransceiver_SetOutputPosition,      /* criAtomEx3dTransceiver_SetOutputPosition */
		LOG_COMMAND_Ex3dTransceiver_SetInputOrientation,    /* criAtomEx3dTransceiver_SetInputOrientation */
		LOG_COMMAND_Ex3dTransceiver_SetOutputOrientation,   /* criAtomEx3dTransceiver_SetOutputOrientation */
		LOG_COMMAND_Ex3dTransceiver_SetOutputConeParameter, /* criAtomEx3dTransceiver_SetOutputConeParameter */
		LOG_COMMAND_Ex3dTransceiver_SetOutputMinMaxAttenuationDistance, /* criAtomEx3dTransceiver_SetOutputMinMaxAttenuationDistance */
		LOG_COMMAND_Ex3dTransceiver_SetOutputInteriorPanField,  /* criAtomEx3dTransceiver_SetOutputInteriorPanField */
		LOG_COMMAND_Ex3dTransceiver_SetInputCrossFadeField, /* criAtomEx3dTransceiver_SetInputCrossFadeField */
		LOG_COMMAND_Ex3dTransceiver_SetOutputVolume,        /* criAtomEx3dTransceiver_SetOutputVolume */
		LOG_COMMAND_Ex3dTransceiver_AttachAisac,            /* criAtomEx3dTransceiver_AttachAisac */
		LOG_COMMAND_Ex3dTransceiver_DetachAisac,            /* criAtomEx3dTransceiver_DetachAisac */
		LOG_COMMAND_Ex3dTransceiver_SetMaxAngleAisacDelta,  /* criAtomEx3dTransceiver_SetMaxAngleAisacDelta */
		LOG_COMMAND_Ex3dTransceiver_SetDistanceAisacControlId,  /* criAtomEx3dTransceiver_SetDistanceAisacControlId */
		LOG_COMMAND_Ex3dTransceiver_SetListenerBasedAzimuthAngleAisacControlId, /* criAtomEx3dTransceiver_SetListenerBasedAzimuthAngleAisacControlId */
		LOG_COMMAND_Ex3dTransceiver_SetListenerBasedElevationAngleAisacControlId,   /* criAtomEx3dTransceiver_SetListenerBasedElevationAngleAisacControlId */
		LOG_COMMAND_Ex3dTransceiver_SetTransceiverOutputBasedAzimuthAngleAisacControlId,    /* criAtomEx3dTransceiver_SetTransceiverOutputBasedAzimuthAngleAisacControlId */
		LOG_COMMAND_Ex3dTransceiver_SetTransceiverOutputBasedElevationAngleAisacControlId,  /* criAtomEx3dTransceiver_SetTransceiverOutputBasedElevationAngleAisacControlId */
		LOG_COMMAND_Ex3dTransceiver_Set3dRegionHn,          /* criAtomEx3dTransceiver_Set3dRegionHn */
		LOG_COMMAND_ExVoicePool_AttachDspPitchShifter,      /* criAtomExVoicePool_AttachDspPitchShifter */
		LOG_COMMAND_ExVoicePool_AttachDspTimeStretch,       /* criAtomExVoicePool_AttachDspTimeStretch */
		LOG_COMMAND_ExVoicePool_AttachDspAfx,               /* criAtomExVoicePool_AttachDspAfx */
		LOG_COMMAND_ExVoicePool_DetachDsp,                  /* criAtomExVoicePool_DetachDsp */
		LOG_COMMAND_Ex_SetRandomSeed,                       /* criAtomEx_SetRandomSeed */
		LOG_COMMAND_Ex_SetCueLinkCallback,                  /* criAtomEx_SetCueLinkCallback */
		LOG_COMMAND_Ex_SetSpeakerAngles,                    /* criAtomEx_SetSpeakerAngles */
		LOG_COMMAND_Ex_SetSpeakerAngleArray,                /* criAtomEx_SetSpeakerAngleArray */
		LOG_COMMAND_Ex_SetVirtualSpeakerAngleArray,         /* criAtomEx_SetVirtualSpeakerAngleArray */
		LOG_COMMAND_Ex_ControlVirtualSpeakerSetting,        /* criAtomEx_ControlVirtualSpeakerSetting */
		LOG_COMMAND_Ex_SetPlaybackCancelCallback,           /* criAtomEx_SetPlaybackCancelCallback */
		LOG_COMMAND_Ex_ControlAcfConsistencyCheck,          /* criAtomEx_ControlAcfConsistencyCheck */
		LOG_COMMAND_Ex_SetAcfConsistencyCheckErrorLevel,    /* criAtomEx_SetAcfConsistencyCheckErrorLevel */
		LOG_COMMAND_Ex_SetTrackTransitionBySelectorCallback,/* criAtomEx_SetTrackTransitionBySelectorCallback */
		LOG_COMMAND_Ex_EnableCalculationAisacControlFrom3dPosition, /* criAtomEx_EnableCalculationAisacControlFrom3dPosition */
		LOG_COMMAND_Ex_SetVoiceEventCallback,               /* criAtomEx_SetVoiceEventCallback */
		LOG_COMMAND_Ex_EnumerateVoiceInfos,                 /* criAtomEx_EnumerateVoiceInfos */
		LOG_COMMAND_Ex_SetMonitoringVoiceStopCallback,      /* criAtomEx_SetMonitoringVoiceStopCallback */
		LOG_COMMAND_Ex_SetMonitoringVoiceStopPlaybackId,    /* criAtomEx_SetMonitoringVoiceStopPlaybackId */
		LOG_COMMAND_Ex_ResetTimer,                          /* criAtomEx_ResetTimer */
		LOG_COMMAND_Ex_PauseTimer,                          /* criAtomEx_PauseTimer */
		LOG_COMMAND_Ex_Lock,                                /* criAtomEx_Lock */
		LOG_COMMAND_Ex_Unlock,                              /* criAtomEx_Unlock */
		LOG_COMMAND_ExHcaMx_SetBusSendLevelByName,          /* criAtomExHcaMx_SetBusSendLevelByName */
		LOG_COMMAND_ExHcaMx_SetFrequencyRatio,              /* criAtomExHcaMx_SetFrequencyRatio */
		LOG_COMMAND_ExHcaMx_SetAsrRackId,                   /* criAtomExHcaMx_SetAsrRackId */
		LOG_COMMAND_ExAcb_EnumerateHandles,                 /* criAtomExAcb_EnumerateHandles */
		LOG_COMMAND_ExAcb_SetDetectionInGamePreviewDataCallback,    /* criAtomExAcb_SetDetectionInGamePreviewDataCallback */
		LOG_COMMAND_ExAcb_ResetCueTypeStateByName,          /* criAtomExAcb_ResetCueTypeStateByName */
		LOG_COMMAND_ExAcb_ResetCueTypeStateById,            /* criAtomExAcb_ResetCueTypeStateById */
		LOG_COMMAND_ExAcb_ResetCueTypeStateByIndex,         /* criAtomExAcb_ResetCueTypeStateByIndex */
		LOG_COMMAND_ExPlayback_SetBeatSyncOffset,           /* criAtomExPlayback_SetBeatSyncOffset */
		LOG_COMMAND_ExSequencer_SetEventCallback,           /* criAtomExSequencer_SetEventCallback */
		LOG_COMMAND_ExBeatSync_SetCallback,                 /* criAtomExBeatSync_SetCallback */
		LOG_COMMAND_ExStreamingCache_LoadWaveformByIdAsync, /* criAtomExStreamingCache_LoadWaveformByIdAsync */
		LOG_COMMAND_ExStreamingCache_LoadWaveformByNameAsync,   /* criAtomExStreamingCache_LoadWaveformByNameAsync */
		LOG_COMMAND_ExStreamingCache_LoadWaveformById,      /* criAtomExStreamingCache_LoadWaveformById */
		LOG_COMMAND_ExStreamingCache_LoadWaveformByName,    /* criAtomExStreamingCache_LoadWaveformByName */
		LOG_COMMAND_ExSoundObject_Create,                   /* criAtomExSoundObject_Create */
		LOG_COMMAND_ExSoundObjectConfig,                    /* criAtomExSoundObject_Create のConfig */
		LOG_COMMAND_ExSoundObject_Destroy,                  /* criAtomExSoundObject_Destroy */
		LOG_COMMAND_ExSoundObject_AddPlayer,                /* criAtomExSoundObject_AddPlayer */
		LOG_COMMAND_ExSoundObject_DeletePlayer,             /* criAtomExSoundObject_DeletePlayer */
		LOG_COMMAND_ExSoundObject_DeleteAllPlayers,         /* criAtomExSoundObject_DeleteAllPlayers */
		LOG_COMMAND_ExAsrRack_SetBusVolumeByName,           /* criAtomExAsrRack_SetBusVolumeByName */
		LOG_COMMAND_ExAsrRack_SetBusPanInfoByName,          /* criAtomExAsrRack_SetBusPanInfoByName */
		LOG_COMMAND_ExAsrRack_SetBusMatrixByName,           /* criAtomExAsrRack_SetBusMatrixByName */
		LOG_COMMAND_ExAsrRack_SetBusSendLevelByName,        /* criAtomExAsrRack_SetBusSendLevelByName */
		LOG_COMMAND_ExAsrRack_SetEffectParameter,           /* criAtomExAsrRack_SetEffectParameter */
		LOG_COMMAND_ExAsrRack_UpdateEffectParameters,       /* criAtomExAsrRack_UpdateEffectParameters */
		LOG_COMMAND_ExAsrRack_SetEffectBypass,              /* criAtomExAsrRack_SetEffectBypass */
		LOG_COMMAND_ExAsrRack_AttachBusAnalyzerByName,      /* criAtomExAsrRack_AttachBusAnalyzerByName */
		LOG_COMMAND_ExAsrRack_DetachBusAnalyzerByName,      /* criAtomExAsrRack_DetachBusAnalyzerByName */
		LOG_COMMAND_ExAsrRack_SetBusFilterCallbackByName,   /* criAtomExAsrRack_SetBusFilterCallbackByName */
		LOG_COMMAND_ExAsr_RegisterEffectInterface,          /* criAtomExAsr_RegisterEffectInterface */
		LOG_COMMAND_ExAsr_UnregisterEffectInterface,        /* criAtomExAsr_UnregisterEffectInterface */
		LOG_COMMAND_ExAsr_ResetIrReverbPerformanceInfo,     /* criAtomExAsr_ResetIrReverbPerformanceInfo */
		LOG_COMMAND_ExAsrRack_Create,                       /* criAtomExAsrRack_Create */
		LOG_COMMAND_ExAsrRackConfig,                        /* criAtomExAsrRack_Create の Config*/
		LOG_COMMAND_ExAsrRack_Destroy,                      /* criAtomExAsrRack_Destroy */
		LOG_COMMAND_ExAsrRack_ResetPerformanceMonitor,      /* criAtomExAsrRack_ResetPerformanceMonitor */
		LOG_COMMAND_ExAsrRack_AttachDspBusSetting,          /* criAtomExAsrRack_AttachDspBusSetting */
		LOG_COMMAND_ExAsrRack_DetachDspBusSetting,          /* criAtomExAsrRack_DetachDspBusSetting */
		LOG_COMMAND_ExAsrRack_ApplyDspBusSnapshot,          /* criAtomExAsrRack_ApplyDspBusSnapshot */
		LOG_COMMAND_ExAsrRack_SetAlternateRackId,           /* criAtomExAsrRack_SetAlternateRackId */
		LOG_COMMAND_ExAsr_SetPcmBufferSize,                 /* criAtomExAsr_SetPcmBufferSize */
		LOG_COMMAND_ExAsrRack_SetAisacControlById,          /* criAtomExAsrRack_SetAisacControlById */
		LOG_COMMAND_ExAsrRack_SetAisacControlByName,        /* criAtomExAsrRack_SetAisacControlByName */
		LOG_COMMAND_ExAsr_EnableBinauralizer,               /* criAtomExAsr_EnableBinauralizer */
		LOG_COMMAND_ExPlayer_AddOutputPort,                 /* criAtomExPlayer_AddOutputPort */
		LOG_COMMAND_ExPlayer_RemoveOutputPort,              /* criAtomExPlayer_RemoveOutputPort */
		LOG_COMMAND_ExPlayer_ClearOutputPorts,              /* criAtomExPlayer_ClearOutputPorts */
		LOG_COMMAND_ExPlayer_AddPreferredOutputPort,        /* criAtomExPlayer_AddPreferredOutputPort */
		LOG_COMMAND_ExPlayer_RemovePreferredOutputPort,     /* criAtomExPlayer_RemovePreferredOutputPort */
		LOG_COMMAND_ExPlayer_RemovePreferredOutputPortByName,   /* criAtomExPlayer_RemovePreferredOutputPortByName */
		LOG_COMMAND_ExPlayer_ClearPreferredOutputPorts,     /* criAtomExPlayer_ClearPreferredOutputPorts */
		LOG_COMMAND_ExOutputPort_Create,                    /* criAtomExOutputPort_Create */
		LOG_COMMAND_ExOutputPortConfig,                     /* criAtomExOutputPortConfig */
		LOG_COMMAND_ExOutputPort_Destroy,                   /* criAtomExOutputPort_Destroy */
		LOG_COMMAND_ExOutputPort_SetAsrRackId,              /* criAtomExOutputPort_SetAsrRackId */
		LOG_COMMAND_ExOutputPort_SetVibrationChannelLevel,  /* criAtomExOutputPort_SetVibrationChannelLevel */
		LOG_COMMAND_ExOutputPort_SetMonauralMix,            /* criAtomExOutputPort_SetMonauralMix */
		LOG_COMMAND_ExOutputPort_IgnoreCategoryParametersById,  /* criAtomExOutputPort_IgnoreCategoryParametersById */
		LOG_COMMAND_ExOutputPort_ResetIgnoreCategory,       /* criAtomExOutputPort_ResetIgnoreCategory */
		LOG_COMMAND_Asr_PauseOutputVoice,                   /* criAtomAsr_PauseOutputVoice */
		LOG_COMMAND_ExAsr_AddResource,                      /* criAtomExAsr_AddResource */
		LOG_COMMAND_ExAsr_RemoveResource,                   /* criAtomExAsr_RemoveResource */
		LOG_COMMAND_ExAsrRack_CreateForAdditionalResource,  /* criAtomExAsrRack_CreateForAdditionalResource */
		LOG_COMMAND_ExAsr_EnableSoundXr,                    /* criAtomExAsr_EnableSoundXr */
		LOG_COMMAND_ExAsr_RegisterSpatializerInterface,     /* criAtomExAsr_RegisterSpatializerInterface */
		LOG_COMMAND_ExAsrRack_SetBusVolume,                 /* criAtomExAsrRack_SetBusVolume */
		LOG_COMMAND_ExAsrRack_SetBusPan3d,                  /* criAtomExAsrRack_SetBusPan3d */
		LOG_COMMAND_ExAsrRack_SetBusPan3dByName,            /* criAtomExAsrRack_SetBusPan3dByName */
		LOG_COMMAND_ExAsrRack_SetBusMatrix,                 /* criAtomExAsrRack_SetBusMatrix */
		LOG_COMMAND_ExAsrRack_SetBusSendLevel,              /* criAtomExAsrRack_SetBusSendLevel */
		LOG_COMMAND_ExAsrRack_AttachBusAnalyzer,            /* criAtomExAsrRack_AttachBusAnalyzer */
		LOG_COMMAND_ExAsrRack_DetachBusAnalyzer,            /* criAtomExAsrRack_DetachBusAnalyzer */
		LOG_COMMAND_ExAsrRack_SetBusFilterCallback,         /* criAtomExAsrRack_SetBusFilterCallback */
		LOG_COMMAND_Ex_SetGameVariableById,                 /* criAtomEx_SetGameVariableById */
		LOG_COMMAND_Ex_SetGameVariableByName,               /* criAtomEx_SetGameVariableByName */
		LOG_COMMAND_ExAcb_AttachAwbFile,                    /* criAtomExAcb_AttachAwbFile */
		LOG_COMMAND_ExAcb_DetachAwbFile,                    /* criAtomExAcb_DetachAwbFile */
		LOG_COMMAND_ExVoicePool_AllocateHcaVoicePoolInternal,   /* criAtomExVoicePool_AllocateHcaVoicePoolInternal */
		LOG_COMMAND_ExVoicePool_FreeAll,                    /* criAtomExVoicePool_FreeAll */
		LOG_COMMAND_ExAsr_PauseOutputVoice,                 /* criAtomExAsr_PauseOutputVoice */
		LOG_COMMAND_Ex_ExecuteMain,                         /* criAtomEx_ExecuteMain */
		LOG_COMMAND_Ex_ExecuteAudioProcess,                 /* criAtomEx_ExecuteAudioProcess */
		LOG_COMMAND_ExCategory_PauseById,                   /* criAtomExCategory_PauseById */
		LOG_COMMAND_ExCategory_PauseByName,                 /* criAtomExCategory_PauseByName */
		LOG_COMMAND_ExCategory_ResetAllAisacControlById,    /* criAtomExCategory_ResetAllAisacControlById */
		LOG_COMMAND_ExCategory_ResetAllAisacControlByName,  /* criAtomExCategory_ResetAllAisacControlByName */
		LOG_COMMAND_ExCategory_StopById,                    /* criAtomExCategory_StopById */
		LOG_COMMAND_ExCategory_StopByName,                  /* criAtomExCategory_StopByName */
		LOG_COMMAND_ExCategory_StopWithoutReleaseTimeById,  /* criAtomExCategory_StopWithoutReleaseTimeById */
		LOG_COMMAND_ExCategory_StopWithoutReleaseTimeByName,    /* criAtomExCategory_StopWithoutReleaseTimeByName */
		LOG_COMMAND_ExPlayer_SetCategoryById,               /* criAtomExPlayer_SetCategoryById */
		LOG_COMMAND_ExPlayer_SetCategoryByName,             /* criAtomExPlayer_SetCategoryByName */
		LOG_COMMAND_ExPlayer_DetachTweenAll,                /* criAtomExPlayer_DetachTweenAll */
		LOG_COMMAND_Ex3dListener_Set3dRegionHn,             /* criAtomEx3dListener_Set3dRegionHn */
		LOG_COMMAND_ExPlayer_StartAsync,                    /* criAtomExPlayer_StartAsync */
		LOG_COMMAND_ExPlayer_SetNextBlockIndex,             /* criAtomExPlayback_SetNextBlockIndex */
		LOG_COMMAND_ExCategory_SetReactParameter,			/* criAtomExCategory_SetReactParameter */
		LOG_COMMAND_ExVoicePool_AllocateInstrumentVoicePool,/* criAtomExVoicePool_AllocateInstrumentVoicePool */
		LOG_COMMAND_ExVoicePool_InstrumentVoicePoolConfig,  /* criAtomExVoicePool_AllocateInstrumentVoicePool */
		LOG_COMMAND_ExAcb_ReleaseAsync,						/* criAtomExAcb_ReleaseAsync */
		LOG_COMMAND_ExPlayer_UpdateAllAsync,				/* criAtomExPlayer_UpdateAllAsync */
		LOG_COMMAND_ExPlayback_EnumerateAtomPlayers,		/* criAtomExPlayback_EnumerateAtomPlayers */
		LOG_COMMAND_ExPlayback_EnumerateVoiceInfos,			/* criAtomExPlayback_EnumerateVoiceInfos */
		LOG_COMMAND_Ex3dSource_SetAttenuationDistanceSetting, /* criAtomEx3dSource_SetAttenuationDistanceSetting */
		LOG_COMMAND_Ex_AddResource,							/* criAtomEx_AddResource */
		LOG_COMMAND_Ex_RemoveResource,						/* criAtomEx_RemoveResource */
		LOG_COMMAND_ExPlayer_SetResourceType,				/* criAtomExPlayer_SetResourceType */
		LOG_COMMAND_ExPlayer_SetBusSendLevel,				/* criAtomExPlayer_SetBusSendLevel */
		LOG_COMMAND_Ex3dListener_SetDistanceFactor,			/* criAtomEx3dListener_SetDistanceFactor */
		LOG_COMMAND_ExPlayer_SetBusSendLevelOffset,			/* cruAtomExPlayer_SetBuSendLevelOffset */
		LOG_COMMAND_Ex3dSource_SetConeOrientation,			/* criAtomEx3dSource_SetConeOrientation */
		LOG_COMMAND_ExAsr_SetConfigForWorkSizeCalculation,	/* criAtomExAsr_SetConfigForWorkSizeCalculation */
		LOG_COMMAND_ExAsrRack_AttachLevelMeter,				/* criAtomExAsr_AttachLevelMater */
		LOG_COMMAND_ExAsrRack_DetachLevelMeter,				/* criAtomExAsr_DetachLevelMeter */
		LOG_COMMAND_ExAsrRack_AttachLoudnessMeter,			/* criAtomExAsr_AttachLoudnessMeter */
		LOG_COMMAND_ExAsrRack_DetachLoudnessMeter,			/* criAtomExAsr_DetachLoutnessMeter */
		LOG_COMMAND_ExAsrRack_ResetLoudnessMeter,			/* criAtomExAsr_ResetLoudnessMeter */
		LOG_COMMAND_ExAsrRack_AttachTruePeakMeter,			/* criAtomExAsr_AttachTruePeakMeter */
		LOG_COMMAND_ExAsrRack_DetachTruePeakMeter,			/* criAtomExAsr_DetachTruePeakMeter */
		LOG_COMMAND_Asr_Initialize,							/* criAtomAsr_Initialize */
		LOG_COMMAND_Asr_Finalize,							/* criAtomAsr_Finalize */
		LOG_COMMAND_Ex_InitializeForUserPcmOutput,			/* criAtomEx_InitializeForUserPcmOutput */
		LOG_COMMAND_Ex_FinalizeForUserPcmOut,				/* criAtomEx_FinalizeForUserPcmOutput */
		LOG_COMMAND_ExAsr_SetDspBypassByName,				/* criAtomExAsr_SetDspBypassByName */
		LOG_COMMAND_Ex3dSource_Create_Success,
		LOG_COMMAND_Ex3dListener_Create_Success,
		LOG_COMMAND_Ex3dRegion_Create_Success,
		LOG_COMMAND_Ex3dSourceList_Create_Success,
		LOG_COMMAND_Ex3dTransceiver_Create_Success,
		LOG_COMMAND_Ex_InitializeWASAPI,
		LOG_COMMAND_Ex_FinalizeWASAPI,
		LOG_COMMAND_Ex_InitializeCOMMON,
		LOG_COMMAND_Ex_FinalizeCOMMON,
		LOG_COMMAND_Ex_InitializeMACOSX,
		LOG_COMMAND_Ex_FinalizeMACOSX,
		LOG_COMMAND_ExPlayer_Create_Success,
		LOG_COMMAND_ExAcb_LoadAcbFile_Success,
		LOG_COMMAND_DoActionApplyMixerSnapshot,
		LOG_COMMAND_ExVoicePool_AllocateMp3VoicePool_PS5,
		LOG_COMMAND_ExVoicePool_AllocateMp3VoicePool_PS4,
		LOG_COMMAND_ExPlayer_Start_Success,
		LOG_COMMAND_Dbas_Create_Success,
		LOG_COMMAND_ExAsrRack_SetBusMuteByName,
		LOG_COMMAND_ExPlayer_SetStartTimeMicro,
		LOG_COMMAND_ExPlayer_StopWithOption,
	};

	protected enum LogParamId {
		LOG_STRINGS_ITEM_CRIATOMDEF = 0,                /* #CRIATOMDEF */
		LOG_STRINGS_ITEM_CRIATOM,                       /* #CRIATOM */
		LOG_STRINGS_ITEM_TimeStamp,                     /* TimeStamp */
		LOG_STRINGS_ITEM_ThreadId,                      /* ThreadId */
		LOG_STRINGS_ITEM_ThreadModel,                   /* thread_model */
		LOG_STRINGS_ITEM_ServerFrequency,               /* server_frequency */
		LOG_STRINGS_ITEM_ParameterUpdateInterval,       /* parameter_update_interval */
		LOG_STRINGS_ITEM_MaxVirtualVoices,              /* max_virtual_voices */
		LOG_STRINGS_ITEM_MaxVoiceLimitGroups,           /* max_voice_limit_groups */
		LOG_STRINGS_ITEM_MaxCategories,                 /* max_categories */
		LOG_STRINGS_ITEM_MaxSequences,                  /* max_sequences */
		LOG_STRINGS_ITEM_MaxTracks,                     /* max_tracks */
		LOG_STRINGS_ITEM_MaxTrackItems,                 /* max_track_items */
		LOG_STRINGS_ITEM_MaxAisacAutoModulations,       /* max_aisac_auto_modulations */
		LOG_STRINGS_ITEM_MaxPitch,                      /* max_pitch */
		LOG_STRINGS_ITEM_CoordinateSystem,              /* coordinate_system */
		LOG_STRINGS_ITEM_RngIf,                         /* rng_if */
		LOG_STRINGS_ITEM_FsConfig,                      /* fs_config */
		LOG_STRINGS_ITEM_Context,                       /* context */
		LOG_STRINGS_ITEM_OutputChannels,                /* output_channels */
		LOG_STRINGS_ITEM_OutputSamplingRate,            /* output_sampling_rate */
		LOG_STRINGS_ITEM_SoundRendererType_uint8,       /* sound_renderer_type */
		LOG_STRINGS_ITEM_NumMixers,                     /* num_mixers */
		LOG_STRINGS_ITEM_MaxVoices,                     /* max_voices */
		LOG_STRINGS_ITEM_MaxInputChannels,              /* max_input_channels */
		LOG_STRINGS_ITEM_MaxSamplingRate,               /* max_sampling_rate */
		LOG_STRINGS_ITEM_Identifier,                    /* identifier */
		LOG_STRINGS_ITEM_MaxStreams,                    /* max_streams */
		LOG_STRINGS_ITEM_MaxBps,                        /* max_bps */
		LOG_STRINGS_ITEM_DbasId,                        /* CriAtomDbasId */
		LOG_STRINGS_ITEM_MaxPath,                       /* max_path */
		LOG_STRINGS_ITEM_MaxFiles,                      /* max_files */
		LOG_STRINGS_ITEM_CacheSize,                     /* cache_size */
		LOG_STRINGS_ITEM_StreamingCacheId,              /* CriAtomStreamingCacheId */
		LOG_STRINGS_ITEM_NumVoices,                     /* num_voices */
		LOG_STRINGS_ITEM_MaxChannels,                   /* max_channels */
		LOG_STRINGS_ITEM_StreamingFlag,                 /* streaming_flag */
		LOG_STRINGS_ITEM_DecodeLatency,                 /* decode_latency */
		LOG_STRINGS_ITEM_ExVoicePoolHn,                 /* CriAtomExVoicePoolHn */
		LOG_STRINGS_ITEM_AllocationMethod,              /* allocation_method */
		LOG_STRINGS_ITEM_MaxPathStrings,                /* max_path_strings */
		LOG_STRINGS_ITEM_UpdatesTime,                   /* updates_time */
		LOG_STRINGS_ITEM_ExPlayerHn,                    /* CriAtomExPlayerHn */
		LOG_STRINGS_ITEM_Id,                            /* id */
		LOG_STRINGS_ITEM_ParameterType,                 /* parameter_type */
		LOG_STRINGS_ITEM_Key,                           /* key */
		LOG_STRINGS_ITEM_DecrypterHn,                   /* CriAtomDecrypterHn */
		LOG_STRINGS_ITEM_Work,                          /* work */
		LOG_STRINGS_ITEM_WorkSize,                      /* work_size */
		LOG_STRINGS_ITEM_Ex3dSourceHn,                  /* CriAtomEx3dSourceHn */
		LOG_STRINGS_ITEM_Ex3dListenerHn,                /* CriAtomEx3dListenerHn */
		LOG_STRINGS_ITEM_ExPlaybackId,                  /* CriAtomExPlaybackId */
		LOG_STRINGS_ITEM_ExFaderConfig,                 /* CriAtomExFaderConfig */
		LOG_STRINGS_ITEM_ExAcfConfig,                   /* CriAtomExAcfConfig */
		LOG_STRINGS_ITEM_AcfData,                       /* acf_data */
		LOG_STRINGS_ITEM_AcfDataSize,                   /* acf_data_size */
		LOG_STRINGS_ITEM_FsBinderHn,                    /* CriFsBinderHn */
		LOG_STRINGS_ITEM_Path,                          /* path */
		LOG_STRINGS_ITEM_AcbData,                       /* acb_data */
		LOG_STRINGS_ITEM_AcbDataSize,                   /* acb_data_size */
		LOG_STRINGS_ITEM_AwbPath,                       /* awb_path */
		LOG_STRINGS_ITEM_AwbId,                         /* awb_id */
		LOG_STRINGS_ITEM_AcbPath,                       /* acb_path */
		LOG_STRINGS_ITEM_AcbId,                         /* acb_id */
		LOG_STRINGS_ITEM_ExAcbHn,                       /* CriAtomExAcbHn */
		LOG_STRINGS_ITEM_Sw,                            /* sw */
		LOG_STRINGS_ITEM_ExResumeMode,                  /* CriAtomExResumeMode */
		LOG_STRINGS_ITEM_ErrorString,                   /* error_string */
		LOG_STRINGS_ITEM_SoundPlaybackId,               /* CriAtomSoundPlaybackId */
		LOG_STRINGS_ITEM_SoundPlayerHn,                 /* CriAtomSoundPlayerHn */
		LOG_STRINGS_ITEM_AwbHn,                         /* CriAtomAwbHn */
		LOG_STRINGS_ITEM_ExCueId,                       /* CriAtomExCueId */
		LOG_STRINGS_ITEM_CueName,                       /* cue_name */
		LOG_STRINGS_ITEM_ExCueIndex,                    /* CriAtomExCueIndex */
		LOG_STRINGS_ITEM_Buffer,                        /* buffer */
		LOG_STRINGS_ITEM_Size,                          /* size */
		LOG_STRINGS_ITEM_ExWaveId,                      /* CriAtomExWaveId */
		LOG_STRINGS_ITEM_MemoryAwbHn,                   /* CriAtomAwbHn for Memory */
		LOG_STRINGS_ITEM_StreamAwbHn,                   /* CriAtomAwbHn for Stream */
		LOG_STRINGS_ITEM_ExTweenHn,                     /* CriAtomExTweenHn */
		LOG_STRINGS_ITEM_ExConfig,                      /* CriAtomExConfig */
		LOG_STRINGS_ITEM_ExAsrConfig,                   /* CriAtomExAsrConfig */
		LOG_STRINGS_ITEM_ExHcaMxConfig,                 /* CriAtomExHcaMxConfig */
		LOG_STRINGS_ITEM_ExDbasConfig,                  /* CriAtomExDbasConfig */
		LOG_STRINGS_ITEM_ExStreamingCacheConfig,        /* CriAtomExStreamingCacheConfig */
		LOG_STRINGS_ITEM_ExStandardVoicePoolConfig,     /* CriAtomExStandardVoicePoolConfig */
		LOG_STRINGS_ITEM_ExAdxVoicePoolConfig,          /* CriAtomExAdxVoicePoolConfig */
		LOG_STRINGS_ITEM_ExAhxVoicePoolConfig,          /* CriAtomExAhxVoicePoolConfig */
		LOG_STRINGS_ITEM_ExHcaVoicePoolConfig,          /* CriAtomExHcaVoicePoolConfig */
		LOG_STRINGS_ITEM_ExHcaMxVoicePoolConfig,        /* CriAtomExHcaMxVoicePoolConfig */
		LOG_STRINGS_ITEM_ExWaveVoicePoolConfig,         /* CriAtomExWaveVoicePoolConfig */
		LOG_STRINGS_ITEM_ExRawPcmVoicePoolConfig,       /* CriAtomExRawPcmVoicePoolConfig */
		LOG_STRINGS_ITEM_ExPlayerConfig,                /* CriAtomExPlayerConfig */
		LOG_STRINGS_ITEM_ExTweenConfig,                 /* CriAtomExTweenConfig */
		LOG_STRINGS_ITEM_DecrypterConfig,               /* CriAtomDecrypterConfig */
		LOG_STRINGS_ITEM_Ex3dSourceConfig,              /* CriAtomEx3dSourceConfig */
		LOG_STRINGS_ITEM_Ex3dListenerConfig,            /* CriAtomEx3dListenerConfig */
		LOG_STRINGS_ITEM_ExAdpcmVoicePoolConfig_3DS,    /* CriAtomExAdpcmVoicePoolConfig_3DS */
		LOG_STRINGS_ITEM_ExAdpcmVoicePoolConfig_WII,    /* CriAtomExAdpcmVoicePoolConfig_WII */
		LOG_STRINGS_ITEM_ExVagVoicePoolConfig_PSP,      /* CriAtomExVagVoicePoolConfig_PSP */
		LOG_STRINGS_ITEM_ExAtrac3VoicePoolConfig_PSP,   /* CriAtomExAtrac3VoicePoolConfig_PSP */
		LOG_STRINGS_ITEM_ExVagVoicePoolConfig_VITA,     /* CriAtomExVagVoicePoolConfig_VITA */
		LOG_STRINGS_ITEM_ExAt9VoicePoolConfig_VITA,     /* CriAtomExAt9VoicePoolConfig_VITA */
		LOG_STRINGS_ITEM_NumGroups,                     /* num_groups */
		LOG_STRINGS_ITEM_VoicesPerGroup,                /* voices_per_group */
		LOG_STRINGS_ITEM_NumCategoryGroups,             /* num_category_groups */
		LOG_STRINGS_ITEM_NumCategories,                 /* num_categories */
		LOG_STRINGS_ITEM_reserved,                      /* reserved */
		LOG_STRINGS_ITEM_ExFaderHn,                     /* CriAtomExFaderHn */
		LOG_STRINGS_ITEM_Guid,                          /* Guid */
		LOG_STRINGS_ITEM_parent_info_id,                /* parent CriAtomExPlaybackId */
		LOG_STRINGS_ITEM_PlayerPoolPlayerInfo,          /* CriAtomPlayerPoolPlayerInfo */
		LOG_STRINGS_ITEM_SoundElementHn,                /* CriAtomSoundElementHn */
		LOG_STRINGS_ITEM_SoundVoiceHn,                  /* CriAtomSoundVoiceHn */
		LOG_STRINGS_ITEM_ExPlaybackId_Cause,            /* Cause CriAtomExPlaybackId */
		LOG_STRINGS_ITEM_Index,                         /* Index */
		LOG_STRINGS_ITEM_NumAllPlaybacks,               /* NumAllPlaybacks */
		LOG_STRINGS_ITEM_NumPlaybacks,                  /* NumPlaybacks */
		LOG_STRINGS_ITEM_CategoriesPerPlayback,         /* categories_per_playback */
		LOG_STRINGS_ITEM_EnableVoicePriorityDecay,      /* enable_voice_priority_decay */
		LOG_STRINGS_ITEM_Volume,                        /* volume */
		LOG_STRINGS_ITEM_SoundElementId,                /* CriAtomSoundElementId */
		LOG_STRINGS_ITEM_SoundVoiceId,                  /* CriAtomSoundVoiceId */
		LOG_STRINGS_ITEM_ExAcbName,                     /* Acb Name */
		LOG_STRINGS_ITEM_PlayerPoolPlayerInfoId,        /* CriAtomPlayerPoolPlayerInfoId */
		LOG_STRINGS_ITEM_AisacControl,                  /* Aisac Control */
		LOG_STRINGS_ITEM_TrackNo,                       /* track no */
		LOG_STRINGS_ITEM_Mute,                          /* mute */
		LOG_STRINGS_ITEM_Result,                        /* Result */
		LOG_STRINGS_ITEM_LogRecordMode,                 /* log record mode */
		LOG_STRINGS_ITEM_NumCh,                         /* Num Ch */
		LOG_STRINGS_ITEM_NumLoaders,                    /* Num Loaders */
		LOG_STRINGS_ITEM_NumPlayers,                    /* Num Players */
		LOG_STRINGS_ITEM_Status,                        /* Status */
		LOG_STRINGS_ITEM_PlayingTime,                   /* PlayingTime */
		LOG_STRINGS_ITEM_DspBusSpectra,                 /* DspBusSpectra */
		LOG_STRINGS_ITEM_CpuLoad,                       /* CpuLoad */
		LOG_STRINGS_ITEM_NumUsedVoices,                 /* NumUsedVoices */
		LOG_STRINGS_ITEM_SequencePlaybackPosition,      /* SequencePlaybackPosition */
		LOG_STRINGS_ITEM_CallbackValue,                 /* CallbackValue */
		LOG_STRINGS_ITEM_CallbackString,                /* CallbackString */
		LOG_STRINGS_ITEM_PeakLevel,                     /* PeakLevel */
		LOG_STRINGS_ITEM_RmsLevel,                      /* RmsLevel */
		LOG_STRINGS_ITEM_PeakHoldLevel,                 /* PeakHoldLevel */
		LOG_STRINGS_ITEM_RequestId,                     /* RequestId */
		LOG_STRINGS_ITEM_TargetId,                      /* TargetId */
		LOG_STRINGS_ITEM_Md5,                           /* Md5 */
		LOG_STRINGS_ITEM_GameVariable,                  /* GameVariable */
		LOG_STRINGS_ITEM_GameVariableName,              /* GameVariableName */
		LOG_STRINGS_ITEM_TimeMs,                        /* TimeMs */
		LOG_STRINGS_ITEM_SnapShotName,                  /* SnapShotName */
		LOG_STRINGS_ITEM_AisacControlId,                /* AisacControlId */
		LOG_STRINGS_ITEM_StartTimeMs,                   /* StartTimeMs */
		LOG_STRINGS_ITEM_SelectorName,                  /* SelectorName */
		LOG_STRINGS_ITEM_LabelName,                     /* LabelName */
		LOG_STRINGS_ITEM_BlockName,                     /* BlockName */
		LOG_STRINGS_ITEM_CategoryName,                  /* CategoryName */
		LOG_STRINGS_ITEM_AisacControlName,              /* AisacControlName */
		LOG_STRINGS_ITEM_SettingName,                   /* SettingName */
		LOG_STRINGS_ITEM_CueSheetId,                    /* CueSheetId */
		LOG_STRINGS_ITEM_BusNo,                         /* BusNo */
		LOG_STRINGS_ITEM_FxType,                        /* FxType */
		LOG_STRINGS_ITEM_RemainedLoopCount,             /* RemainedLoopCount */
		LOG_STRINGS_ITEM_SequenceLoopId,                /* SequenceLoopId */
		LOG_STRINGS_ITEM_Ex3dPosVector_Position,        /* 3dPosVector_Position */
		LOG_STRINGS_ITEM_Ex3dPosVector_Velocity,        /* 3dPosVector_Velocity */
		LOG_STRINGS_ITEM_Ex3dPosVector_Forward,         /* 3dPosVector_Forward */
		LOG_STRINGS_ITEM_Ex3dPosVector_Upward,          /* 3dPosVector_Upward */
		LOG_STRINGS_ITEM_Ex3dPosVector_FocusPoint,      /* 3dPosVector_FocusPoint */
		LOG_STRINGS_ITEM_Ex3dPosVector_Cone,            /* 3dPosVector_Cone */
		LOG_STRINGS_ITEM_Ex3dMaxAngleAisacDelta,        /* 3dMaxAngleAisacDelta */
		LOG_STRINGS_ITEM_Ex3dEnablePriorityDecay,       /* 3dEnablePriorityDecay */
		LOG_STRINGS_ITEM_Ex3dDistanceFactor,            /* 3dDistanceFactor */
		LOG_STRINGS_ITEM_Ex3dDistanceFocusLevel,        /* 3dDistanceFocusLevel */
		LOG_STRINGS_ITEM_Ex3dDirectionFocusLevel,       /* 3dDirectionFocusLevel */
		LOG_STRINGS_ITEM_Result3dPos,                   /* Result3dPos */
		LOG_STRINGS_ITEM_ExAiffVoicePoolConfig,         /* CriAtomExAiffVoicePoolConfig */
		LOG_STRINGS_ITEM_SoundRendererType,             /* SoundRendererType */
		LOG_STRINGS_ITEM_ExAt9VoicePoolConfig_PS4,      /* CriAtomExAt9VoicePoolConfig_PS4 */
		LOG_STRINGS_ITEM_AverageServerTime,             /* AverageServerTime */
		LOG_STRINGS_ITEM_AverageServerInterval,         /* AverageServerInterval */
		LOG_STRINGS_ITEM_MaxServerTime,                 /* MaxServerTime */
		LOG_STRINGS_ITEM_MaxServerInterval,             /* MaxServerInterval */
		LOG_STRINGS_ITEM_UserLog,                       /* UserLog */
		LOG_STRINGS_ITEM_ByVoiceGroupLimitation,        /* ByVoiceGroupLimitation */
		LOG_STRINGS_ITEM_ByVoicePoolLimitation,         /* ByVoicePoolLimitation */
		LOG_STRINGS_ITEM_RetryFlag,                     /* RetryFlag */
		LOG_STRINGS_ITEM_BusName,                       /* BusName */
		LOG_STRINGS_ITEM_StreamType,                    /* StreamType */
		LOG_STRINGS_ITEM_MomentaryValue,                /* MomentaryValue */
		LOG_STRINGS_ITEM_ShortTermValue,                /* ShortTermValue */
		LOG_STRINGS_ITEM_IntegratedValue,               /* IntegratedValue */
		LOG_STRINGS_ITEM_TotalBps,                      /* TotalBps */
		LOG_STRINGS_ITEM_NumCues,                       /* num_cues */
		LOG_STRINGS_ITEM_SoundFormat,                   /* SoundFormat *//* (CriAtomSoundFormatのこと) */
		LOG_STRINGS_ITEM_ExAdpcmVoicePoolConfig_WIIU,   /* CriAtomExAdpcmVoicePoolConfig_WIIU */
		LOG_STRINGS_ITEM_AwbName,                       /* AwbName */
		LOG_STRINGS_ITEM_NumStreamAwb,                  /* NumStreamAwb */
		LOG_STRINGS_ITEM_ExPlayback_AllocateModule,     /* ExPlayback_AllocateModule */
		LOG_STRINGS_ITEM_AisacControlValue,             /* AisacControlValue */
		LOG_STRINGS_ITEM_NumAllPlaybacksForReact,       /* NumAllPlaybacksForReact */
		LOG_STRINGS_ITEM_PreviewContext,                /* PreviewContext */
		LOG_STRINGS_ITEM_MaxParameterBlocks,            /* max_parameter_blocks */
		LOG_STRINGS_ITEM_MaxFaders,                     /* max_faders */
		LOG_STRINGS_ITEM_NumBuses,                      /* num_buses */
		LOG_STRINGS_ITEM_MaxRacks,                      /* max_racks */
		LOG_STRINGS_ITEM_OutputChannels4HcaMx,          /* output_channels(HcaMxConfig用) */
		LOG_STRINGS_ITEM_OutputSamplingRate4HcaMx,      /* output_sampling_rate(HcaMxConfig用) */
		LOG_STRINGS_ITEM_SoundRendererType4HcaMx,       /* sound_renderer_type(HcaMxConfig用) */
		LOG_STRINGS_ITEM_SPEAKER_SYSTEM,                /* speaker_system */
		LOG_STRINGS_ITEM_SPEAKER_ANGLE_LEFT,            /* left_speaker_angle */
		LOG_STRINGS_ITEM_SPEAKER_ANGLE_RIGHT,           /* right_speaker_angle */
		LOG_STRINGS_ITEM_SPEAKER_ANGLE_CENTER,          /* center_speaker_angle */
		LOG_STRINGS_ITEM_SPEAKER_ANGLE_LFE,             /* lfe_speaker_angle */
		LOG_STRINGS_ITEM_SPEAKER_ANGLE_SURROUND_LEFT,   /* surround_left_speaker_angle */
		LOG_STRINGS_ITEM_SPEAKER_ANGLE_SURROUND_RIGHT,  /* surround_right_speaker_angle */
		LOG_STRINGS_ITEM_SPEAKER_ANGLE_SURROUND_BACK_LEFT,      /* surround_back_left_speaker_angle */
		LOG_STRINGS_ITEM_SPEAKER_ANGLE_SURROUND_BACK_RIGHT,     /* surround_back_right_speaker_angle */
		LOG_STRINGS_ITEM_PAN_SPEAKER_TYPE,              /* pan_speaker_type */
		LOG_STRINGS_ITEM_VOICE_STOP_REASON,             /* CriAtomVoiceStopReason */
		LOG_STRINGS_ITEM_ExVibrationVoicePoolConfig,    /* CriAtomExVibrationVoicePoolConfig */
		LOG_STRINGS_ITEM_TouchSenceEffectName,          /* TouchSenceEffectName */
		LOG_STRINGS_ITEM_DspName,                       /* DspName */
		LOG_STRINGS_ITEM_DspObject,                     /* DspObject */
		LOG_STRINGS_ITEM_DspSlotNo,                     /* DspSlotNo */
		LOG_STRINGS_ITEM_DspPluginType,                 /* DspPluginType */
		LOG_STRINGS_ITEM_Ex3dPosVector_ListenerTop,     /* 3dPosVector_ListenerTop */
		LOG_STRINGS_ITEM_Playback_Status,               /* Playback_Status */
		LOG_STRINGG_ITEM_InstrumentInstanceCbFunc,
		LOG_STRINGG_ITEM_InstrumentInstanceCbObj,
		LOG_STRINGG_ITEM_AttachInstrumentInstancePlayerHn,
		LOG_STRINGG_ITEM_InstrumentInstance,
		LOG_STRINGS_ITEM_Ex3dTransceiverHn,             /* CriAtomEx3dTransceiverHn */
		LOG_STRINGS_ITEM_Ex3dTransceiverConfig,         /* CriAtomEx3dTransceiverConfig */
		LOG_STRINGS_ITEM_Ex3dRegionHn,                  /* CriAtomExRegionHn */
		LOG_STRINGS_ITEM_Ex3dTransceiverDirectAudioRadius,  /* TransceiverDirectAudioRadius */
		LOG_STRINGS_ITEM_Ex3dTransceiverCrossFadeDistance,  /* TransceiverCrossFadeDistance */
		LOG_STRINGS_ITEM_ProgramNo,                     /* サウンドプログラムバングプレビュー用Program No */
		LOG_STRINGS_ITEM_KeyNo,                         /* サウンドプログラムバングプレビュー用Key No */
		LOG_STRINGS_ITEM_Velocity,                      /* サウンドプログラムバングプレビュー用Velocity */
		LOG_STRINGS_ITEM_PitchBend,                     /* サウンドプログラムバングプレビュー用PitchBend */
		LOG_STRINGS_ITEM_Format,						/* コーデックフォーマット：HCA, ADX, HCA-MX... */
		LOG_STRINGS_ITEM_MaxRhythmTracks,				/* CriAtomAdmPlayerConfig.max_rhythm_tracks */
		LOG_STRINGS_ITEM_MaxMelodyTracks,				/* CriAtomAdmPlayerConfig.max_melody_tracks */
		LOG_STRINGS_ITEM_MaxVocalTracks,				/* CriAtomAdmPlayerConfig.max_vocal_tracks */
		LOG_STRINGS_ITEM_AdmPlayerHn,					/* CriAtomAdmPlayerHn */
		LOG_STRINGS_ITEM_SpeakerMapping,                /* CriAtomExAsrConfig.speaker_mapping */
		LOG_STRINGS_ITEM_AmbisonicsOrderType,           /* CriAtomExAsrConfig.ambisonics_order_type */
		LOG_STRINGS_ITEM_MaxAisacs,						/* CriAtomExConfig.max_aisacs */
		LOG_STRINGS_ITEM_MaxBusSends,					/* CriAtomExConfig.max_bus_sends */
		LOG_STRINGS_ITEM_RackId,						/* CriAtomExAsrRackId */
		LOG_STRINGS_ITEM_GroupNo,						/* Voice Limit Group No. */
		LOG_STRINGS_ITEM_Priority,						/* プライオリティ */
		LOG_STRINGS_ITEM_Solo,							/* Solo */
		LOG_STRINGS_ITEM_Param_Id,						/* Parameter ID */
		LOG_STRINGS_ITEM_Param_Name,                    /* Parameter Name */
		LOG_STRINGS_ITEM_Param_Index,                   /* Parameter Index */
		LOG_STRINGS_ITEM_Param_Float32,					/* ExPlayerParameter_SetParameterFloat32 */
		LOG_STRINGS_ITEM_Param_Sint32,					/* ExPlayerParameter_SetParameterSint32 */
		LOG_STRINGS_ITEM_Param_Uint32,					/* ExPlayerParameter_SetParameterUint32 */
		LOG_STRINGS_ITEM_ExBlockIndex,					/* Block Index */
		LOG_STRINGS_ITEM_PlaybackCancelType,			/* CriAtomExPlaybackCancelType */
		LOG_STRINGS_ITEM_ChangesIgnoredReason,          /* APIを介した状態変更がエラーなしに失敗した理由 */
		LOG_STRINGS_ITEM_ChangedParam,
		LOG_STRINGS_ITEM_ChangedId,
		LOG_STRINGS_ITEM_ChangedName,
		LOG_STRINGS_ITEM_ChangedIndex,
		LOG_STRINGS_ITEM_CategoryId,
		LOG_STRINGS_ITEM_TweenParamType,                /* Tween Parameter Type */
		LOG_STRINGS_ITEM_TweenParameterId,              /* Tween Parameter Id */
		LOG_STRINGS_ITEM_TweenAisacId,                  /* Tween Aisac Id */
		LOG_STRINGS_ITEM_TweenTime,                     /* Tween_MoveTo,From time_ms */
		LOG_STRINGS_ITEM_TweenValue,                    /* Tween_MoveTo,From value */
		LOG_STRINGS_ITEM_Parameter2Hn,					/* Parameter2 Handle */
		LOG_STRINGS_ITEM_LogId,							/* Log ID */
		LOG_STRINGS_ITEM_PacketVersion,					/* Packet Version */
		LOG_STRINGS_ITEM_SelectorIndex,
		LOG_STRINGS_ITEM_LabelIndex,
		LOG_STRINGS_ITEM_GlobalAisacName,
		LOG_STRINGS_ITEM_PanPitch,
		LOG_STRINGS_ITEM_PanAngle,
		LOG_STRINGS_ITEM_PanInteriorDistance,
		LOG_STRINGS_ITEM_PanType,
		LOG_STRINGS_ITEM_PanSpeakerType,
		LOG_STRINGS_ITEM_PanAngleType,
		LOG_STRINGS_ITEM_PanSpread,
		LOG_STRINGS_ITEM_BusSendLevel,
		LOG_STRINGS_ITEM_BusSendLevelOffset,
		LOG_STRINGS_ITEM_CofHigh,
		LOG_STRINGS_ITEM_CofLow,
		LOG_STRINGS_ITEM_BiquadFilterType,
		LOG_STRINGS_ITEM_Frequency,				/* Bi-quad filter frequency */
		LOG_STRINGS_ITEM_Gain,
		LOG_STRINGS_ITEM_QValue,
		LOG_STRINGS_ITEM_Ex3dSourceListHn,
		LOG_STRINGS_ITEM_CurveType,
		LOG_STRINGS_ITEM_CurveStrength,
		LOG_STRINGS_ITEM_EnvelopeSustainLevel,
		LOG_STRINGS_ITEM_GlobalAisacIndex,
		LOG_STRINGS_ITEM_CuePriority,
		LOG_STRINGS_ITEM_DataRequestCallbackFunc,
		LOG_STRINGS_ITEM_UserObject,
		LOG_STRINGS_ITEM_RandomSeed,
		LOG_STRINGS_ITEM_BlockIndex,
		LOG_STRINGS_ITEM_ExPlayerCallback,
		LOG_STRINGS_ITEM_ExPlaybackCallback,
		LOG_STRINGS_ITEM_SamplingRate,
		LOG_STRINGS_ITEM_VoiceControlMethod,
		LOG_STRINGS_ITEM_HcaMxMixerId,
		LOG_STRINGS_ITEM_AsrRackIdArray,
		LOG_STRINGS_ITEM_PlaybackRatio,
		LOG_STRINGS_ITEM_LoopCount,
		LOG_STRINGS_ITEM_MixdownCenterVolumeOffset,
		LOG_STRINGS_ITEM_MixdownLfeVolumeOffset,
		LOG_STRINGS_ITEM_ExPanCallback,
		LOG_STRINGS_ITEM_ChannelsPerTrack,
		LOG_STRINGS_ITEM_SilentMode,
		LOG_STRINGS_ITEM_ExFilterCallback,
		LOG_STRINGS_ITEM_BypassFlag,
		LOG_STRINGS_ITEM_BlockTransitionCallback,
		LOG_STRINGS_ITEM_SpeakerId,
		LOG_STRINGS_ITEM_SendLevelOffset,
		LOG_STRINGS_ITEM_SendLevelGain,
		LOG_STRINGS_ITEM_ExPlayerPlaybackTrackInfoNotificationCallback,
		LOG_STRINGS_ITEM_ExPlaybackEventCallback,
		LOG_STRINGS_ITEM_ChannelConfig,
		LOG_STRINGS_ITEM_InsideAngle,
		LOG_STRINGS_ITEM_OutsideAngle,
		LOG_STRINGS_ITEM_OutsideVolume,
		LOG_STRINGS_ITEM_MinAttenuationDistance,
		LOG_STRINGS_ITEM_MaxAttenuationDistance,
		LOG_STRINGS_ITEM_SourceRadius,
		LOG_STRINGS_ITEM_InteriorDistance,
		LOG_STRINGS_ITEM_DopplerFactor,
		LOG_STRINGS_ITEM_MaxAngleAisacDelta,
		LOG_STRINGS_ITEM_Ex3dSourceRandomPositionConfig,
		LOG_STRINGS_ITEM_Ex3dSourceRandomPositionCalculationCallback,
		LOG_STRINGS_ITEM_ExVectorArray,
		LOG_STRINGS_ITEM_Ex3dSourceRandomPositionResult,
		LOG_STRINGS_ITEM_DopplerMultiplier,
		LOG_STRINGS_ITEM_Ex3dRegionConfig,
		LOG_STRINGS_ITEM_TransceiverRadius,
		LOG_STRINGS_ITEM_DspAfxConfig,
		LOG_STRINGS_ITEM_DspPitchShifterConfig,
		LOG_STRINGS_ITEM_DspTimeStretchConfig,
		LOG_STRINGS_ITEM_ExCueLinkCallback,
		LOG_STRINGS_ITEM_AngleArray,
		LOG_STRINGS_ITEM_ExPlaybackCancelCallback,
		LOG_STRINGS_ITEM_ErrorLevel,
		LOG_STRINGS_ITEM_ExTrackTransitionBySelectorCallback,
		LOG_STRINGS_ITEM_ExVoiceEventCallback,
		LOG_STRINGS_ITEM_ExVoiceInfoCallback,
		LOG_STRINGS_ITEM_ExMonitoringVoiceStopCallback,
		LOG_STRINGS_ITEM_MixerId,
		LOG_STRINGS_ITEM_FrequencyRatio,
		LOG_STRINGS_ITEM_ExAcbHandleCallback,
		LOG_STRINGS_ITEM_ExAcbDetectionInGamePreviewDataCallback,
		LOG_STRINGS_ITEM_ExSequencerEventCallback,
		LOG_STRINGS_ITEM_ExBeatSyncCallback,
		LOG_STRINGS_ITEM_ExStreamingCacheId,
		LOG_STRINGS_ITEM_Ex3dSoundObjectConfig,
		LOG_STRINGS_ITEM_EnableVoiceLimitScope,
		LOG_STRINGS_ITEM_EnableCategoryCueLimitScope,
		LOG_STRINGS_ITEM_SoundObjectHn,
		LOG_STRINGS_ITEM_PanInfoAngle,
		LOG_STRINGS_ITEM_PanInfoDistance,
		LOG_STRINGS_ITEM_PanInfoSpread,
		LOG_STRINGS_ITEM_PanInfoVolume,
		LOG_STRINGS_ITEM_InputChannels,
		LOG_STRINGS_ITEM_BusMatrix,
		LOG_STRINGS_ITEM_SendToBusName,
		LOG_STRINGS_ITEM_SendLevel,
		LOG_STRINGS_ITEM_ParameterValue,
		LOG_STRINGS_ITEM_ExAsrBusAnalyzerInterval,
		LOG_STRINGS_ITEM_ExAsrBusAnalyzerPeakHoldTimeMs,
		LOG_STRINGS_ITEM_ExAsrBusPreFilterCallback,
		LOG_STRINGS_ITEM_ExAsrBusPostFilterCallback,
		LOG_STRINGS_ITEM_ExAsrAfxInterfaceWithVersionPtr,
		LOG_STRINGS_ITEM_ExAsrRackConfig,
		LOG_STRINGS_ITEM_DspSettingName,
		LOG_STRINGS_ITEM_OutputRackId,
		LOG_STRINGS_ITEM_AltRackId,
		LOG_STRINGS_ITEM_NumSamples,
		LOG_STRINGS_ITEM_ExOutputPortHn,
		LOG_STRINGS_ITEM_ExOutputPortName,
		LOG_STRINGS_ITEM_ExOutputPortConfig,
		LOG_STRINGS_ITEM_ExOutputPortType,
		LOG_STRINGS_ITEM_MaxIgnoredCategories,
		LOG_STRINGS_ITEM_Channel,
		LOG_STRINGS_ITEM_ChannelLevel,
		LOG_STRINGS_ITEM_SpatializerType,
		LOG_STRINGS_ITEM_ExAsrSpatializerInterfacePtr,
		LOG_STRINSG_ITEM_ExAsrSpatializerInitializeConfigPtr,
		LOG_STRINGS_ITEM_SendToBusNo,
		LOG_STRINGS_ITEM_Ex3dSourceRandomPositionResultCallback,
		LOG_STRINGS_ITEM_DirectAuiodRadius,
		LOG_STRINGS_ITEM_CrossfadeDistance,
		LOG_STRINGS_ITEM_PreviewRackType,
		LOG_STRINGS_ITEM_Flag,
		LOG_STRINGS_ITEM_ReactName,
		LOG_STRINGS_ITEM_EnableDecrementAisacModulationKey,
		LOG_STRINGS_ITEM_DecrementAisacModulationKey,
		LOG_STRINGS_ITEM_EnableIncrementAisacModulationKey,
		LOG_STRINGS_ITEM_IncrementAisacModulationKey,
		LOG_STRINGS_ITEM_DuckerLevel,
		LOG_STRINGS_ITEM_DuckerTargetType,
		LOG_STRINGS_ITEM_FadeParameterEntryCurveType,
		LOG_STRINGS_ITEM_FadeParameterEntryCurveStrength,
		LOG_STRINGS_ITEM_FadeParameterEntryFadeTimeMs,
		LOG_STRINGS_ITEM_FadeParameterExitCurveType,
		LOG_STRINGS_ITEM_FadeParameterExitCurveStrength,
		LOG_STRINGS_ITEM_FadeParameterExitFadeTimeMs,
		LOG_STRINGS_ITEM_HoldType,
		LOG_STRINGS_ITEM_HoldTimeMs,
		LOG_STRINGS_ITEM_ReactType,
		LOG_STRINGS_ITEM_EnablePausingCue,
		LOG_STRINGS_ITEM_InterfaceName,
		LOG_STRINGS_ITEM_InstrumentName,
		LOG_STRINGS_ITEM_ExInstrumentVoicePoolConfig,
		LOG_STRINGS_ITEM_ExAcbReleasedCallback,
		LOG_STRINGS_ITEM_AtomPlayerCallback,
		LOG_STRINGS_ITEM_EnableAtomSoundDisabledMode,	
		LOG_STRINGS_ITEM_EnableAutoMatchingInPanTypeAuto,
		LOG_STRINGS_ITEM_EnableCategoryOverrideByExPlayer,
		LOG_STRINGS_ITEM_SequencePrepareRatio,
		LOG_STRINGS_ITEM_FsThreadModel,
		LOG_STRINGS_ITEM_NumBinders,
		LOG_STRINGS_ITEM_NumGroupLoaders,
		LOG_STRINGS_ITEM_NumStdioHandles,
		LOG_STRINGS_ITEM_NumInstallers, 
		LOG_STRINGS_ITEM_MaxBinds,
		LOG_STRINGS_ITEM_FsVersion,
		LOG_STRINGS_ITEM_FsVersionString,
		LOG_STRINGS_ITEM_EnableCrcCheck,
		LOG_STRINGS_ITEM_ExAcfLocationInfoType,
		LOG_STRINGS_ITEM_Version,
		LOG_STRINGS_ITEM_VersionEx,
		LOG_STRINGS_ITEM_VersionString, 
		LOG_STRINGS_ITEM_VersionExString,
		LOG_STRINGS_ITEM_ExResourceType,
		LOG_STRINGS_ITEM_BusIndex,
		LOG_STRINGS_ITEM_DistanceFactor,
		LOG_STRINGS_ITEM_ConeOrientation,
		LOG_STRINGS_ITEM_IntervalMs,
		LOG_STRINGS_ITEM_HoldTimeMsSint32,
		LOG_STRINGS_ITEM_ShortTermTimeMs,
		LOG_STRINGS_ITEM_IntegratedTimeMs,
		LOG_STRINGS_ITEM_SampleClipping,
		LOG_STRINGS_ITEM_Marker_AtomExConfig,
		LOG_STRINGS_ITEM_Marker_AsrConfig,
		LOG_STRINGS_ITEM_Marker_HcaMxConfig,
		LOG_STRINGS_ITEM_DspId,
		LOG_STRINGS_ITEM_FollowsOriginalSource,
		LOG_STRINGS_ITEM_Ex3dSourceRandomPositionCalculationType,
		LOG_STRINGS_ITEM_Ex3dSourceRandomPositionCalculationParameters,
		LOG_STRINGS_ITEM_RandomPositionListMaxLength,
		LOG_STRINGS_ITEM_ExPlaybackId_unique64,
		LOG_STRINGS_ITEM_ExPlaybackId_Cause_unique64,
		LOG_STRINGS_ITEM_parent_info_id_unique64,
		LOG_STRINGS_ITEM_SoundVoiceId_unique64,
		LOG_STRINGS_ITEM_TimeMsFloat,
		LOG_STRINGS_ITEM_TimeMsInt16,
		LOG_STRINGS_ITEM_ExMaxIgnoredCategories,
		LOG_STRINGS_ITEM_NumDsp,
		LOG_STRINGS_ITEM_ConfigParameters,
		LOG_STRINGS_ITEM_DefaultParameters,
		LOG_STRINGS_ITEM_AfxInterfacePtr,
		LOG_STRINGS_ITEM_PitchShiftMode,
		LOG_STRINGS_ITEM_WindowSize,
		LOG_STRINGS_ITEM_OverlapTimes,
		LOG_STRINGS_ITEM_PanInfoWideness,
		LOG_STRINGS_ITEM_EnableAudioSyncedTimer,
		LOG_STRINGS_ITEM_VoiceAllocationMethod,
		LOG_STRINGS_ITEM_AcbBinderHn,
		LOG_STRINGS_ITEM_AwbBinderHn,
		LOG_STRINGS_ITEM_ExMp3VoicePoolConfig_PS5,
		LOG_STRINGS_ITEM_ExMp3VoicePoolConfig_PS4,
		LOG_STRINGS_ITEM_NumUsedPlayers,
		LOG_STRINGS_ITEM_StartTimeUs,
		LOG_STRINGS_ITEM_ExStopOption,
	};

	/* ログのパラメータ引数の型 */
	protected enum LogParamTypes {
		TYPE_INT8 = 0,
		TYPE_INT16,
		TYPE_INT32,
		TYPE_INT64,
		TYPE_FLOAT32,
		TYPE_CHAR,
		TYPE_UINTPTR,
		TYPE_GUID,
		TYPE_128,
		TYPE_VECTOR,
		TYPE_INT32ARRAY,
		TYPE_VECTORARRAY,
		TYPE_FLOAT32ARRAY,
	}
	static protected readonly int[] ParamTypeSizes32 = new int[] {
		1,      /* SIZE_INT8 */
		2,      /* SIZE_INT16 */
		4,      /* SIZE_INT32 */
		8,      /* SIZE_INT64 */
		4,      /* SIZE_FLOAT32 */
		2,      /* SIZE_CHAR (String Size) */
		4,      /* SIZE_UINTPTR (Handles) */
		16,     /* SIZE_GUID */
		128,    /* SIZE_128 */
		12,     /* SIZE_VECTOR (Vector3f) */
		2,		/* INT32ARRAY */
		2,		/* VECTORARRAY */
		2,		/* FLOAT32ARRAY */
	};
	static protected readonly int[] ParamTypeSizes64 = new int[] {
		1,      /* SIZE_INT8 */
		2,      /* SIZE_INT16 */
		4,      /* SIZE_INT32 */
		8,      /* SIZE_INT64 */
		4,      /* SIZE_FLOAT32 */
		2,      /* SIZE_CHAR (String Size) */
		8,      /* SIZE_UINTPTR (Handles) */
		16,     /* SIZE_GUID */
		128,    /* SIZE_128 */
		12,     /* SIZE_VECTOR (Vector3f) */
		2,		/* INT32ARRAY */
		2,		/* VECTORARRAY */
		2,		/* FLOAT32ARRAY */
	};

	protected struct LogParam {
		public readonly string name;
		public readonly LogParamTypes type;
		public readonly int size32;
		public readonly int size64;
		public LogParam(string name, LogParamTypes type)
		{
			this.name = name;
			this.type = type;
			this.size32 = CriProfiler.ParamTypeSizes32[(int)type];
			this.size64 = CriProfiler.ParamTypeSizes64[(int)type];
		}
	}

	static protected readonly LogParam[] LogParams = new LogParam[] {
		new LogParam( "#CRIATOMDEF",                    LogParamTypes.TYPE_CHAR ),                  /*   0 */
		new LogParam( "#CRIATOM",                       LogParamTypes.TYPE_CHAR ),                  /*   1 */
		new LogParam( "TimeStamp[usec]",                LogParamTypes.TYPE_INT64 ),                 /*   2 */
		new LogParam( "ThreadId",                       LogParamTypes.TYPE_UINTPTR ),               /*   3 */
		new LogParam( "thread_model",                   LogParamTypes.TYPE_INT8 ),                  /*   4 */
		new LogParam( "server_frequency",               LogParamTypes.TYPE_FLOAT32 ),               /*   5 */
		new LogParam( "parameter_update_interval",      LogParamTypes.TYPE_INT16 ),                 /*   6 */
		new LogParam( "max_virtual_voices",             LogParamTypes.TYPE_INT16 ),                 /*   7 */
		new LogParam( "max_voice_limit_groups",         LogParamTypes.TYPE_INT16 ),                 /*   8 */
		new LogParam( "max_categories",                 LogParamTypes.TYPE_INT16 ),                 /*   9 */
		new LogParam( "max_sequences",                  LogParamTypes.TYPE_INT16 ),                 /*  10 */
		new LogParam( "max_tracks",                     LogParamTypes.TYPE_INT16 ),                 /*  11 */
		new LogParam( "max_track_items",                LogParamTypes.TYPE_INT16 ),                 /*  12 */
		new LogParam( "max_aisac_auto_modulations",     LogParamTypes.TYPE_INT16 ),                 /*  13 */
		new LogParam( "max_pitch",                      LogParamTypes.TYPE_FLOAT32 ),               /*  14 */
		new LogParam( "coordinate_system",              LogParamTypes.TYPE_INT8 ),                  /*  15 */
		new LogParam( "rng_if",                         LogParamTypes.TYPE_UINTPTR ),               /*  16 */
		new LogParam( "fs_config",                      LogParamTypes.TYPE_UINTPTR ),               /*  17 */
		new LogParam( "context",                        LogParamTypes.TYPE_UINTPTR ),               /*  18 */
		new LogParam( "output_channels",                LogParamTypes.TYPE_INT8 ),                  /*  19 */
		new LogParam( "output_sampling_rate",           LogParamTypes.TYPE_INT32 ),                 /*  20 */
		new LogParam( "sound_renderer_type",            LogParamTypes.TYPE_INT8 ),                  /*  21 */
		new LogParam( "num_mixers",                     LogParamTypes.TYPE_INT16 ),                 /*  22 */
		new LogParam( "max_voices",                     LogParamTypes.TYPE_INT16 ),                 /*  23 */
		new LogParam( "max_input_channels",             LogParamTypes.TYPE_INT8 ),                  /*  24 */
		new LogParam( "max_sampling_rate",              LogParamTypes.TYPE_INT32 ),                 /*  25 */
		new LogParam( "identifier",                     LogParamTypes.TYPE_INT32 ),                 /*  26 */
		new LogParam( "max_streams",                    LogParamTypes.TYPE_INT32 ),                 /*  27 */
		new LogParam( "max_bps",                        LogParamTypes.TYPE_INT32 ),                 /*  28 */
		new LogParam( "CriAtomDbasId",                  LogParamTypes.TYPE_INT32 ),                 /*  29 */
		new LogParam( "max_path",                       LogParamTypes.TYPE_INT16 ),                 /*  30 */
		new LogParam( "max_files",                      LogParamTypes.TYPE_INT32 ),                 /*  31 */
		new LogParam( "cache_size",                     LogParamTypes.TYPE_INT32 ),                 /*  32 */
		new LogParam( "CriAtomStreamingCacheId",        LogParamTypes.TYPE_UINTPTR ),               /*  33 */
		new LogParam( "num_voices",                     LogParamTypes.TYPE_INT32 ),                 /*  34 */
		new LogParam( "max_channels",                   LogParamTypes.TYPE_INT8 ),                  /*  35 */
		new LogParam( "streaming_flag",                 LogParamTypes.TYPE_INT8 ),                  /*  36 */
		new LogParam( "decode_latency",                 LogParamTypes.TYPE_INT32 ),                 /*  37 */
		new LogParam( "CriAtomExVoicePoolHn",           LogParamTypes.TYPE_UINTPTR ),               /*  38 */
		new LogParam( "allocation_method",              LogParamTypes.TYPE_INT8 ),                  /*  39 */
		new LogParam( "max_path_strings",               LogParamTypes.TYPE_INT16 ),                 /*  40 */
		new LogParam( "updates_time",                   LogParamTypes.TYPE_INT8 ),                  /*  41 */
		new LogParam( "CriAtomExPlayerHn",              LogParamTypes.TYPE_UINTPTR ),               /*  42 */
		new LogParam( "id",                             LogParamTypes.TYPE_INT32 ),                 /*  43 */
		new LogParam( "parameter_type",                 LogParamTypes.TYPE_INT8 ),                  /*  44 */
		new LogParam( "key",                            LogParamTypes.TYPE_INT64 ),                 /*  45 */
		new LogParam( "CriAtomDecrypterHn",             LogParamTypes.TYPE_UINTPTR ),               /*  46 */
		new LogParam( "work",                           LogParamTypes.TYPE_UINTPTR ),               /*  47 */
		new LogParam( "work_size",                      LogParamTypes.TYPE_INT32 ),                 /*  48 */
		new LogParam( "CriAtomEx3dSourceHn",            LogParamTypes.TYPE_UINTPTR ),               /*  49 */
		new LogParam( "CriAtomEx3dListenerHn",          LogParamTypes.TYPE_UINTPTR ),               /*  50 */
		new LogParam( "CriAtomExPlaybackId",            LogParamTypes.TYPE_INT32 ),                 /*  51 */
		new LogParam( "CriAtomExFaderConfig",           LogParamTypes.TYPE_UINTPTR ),               /*  52 */
		new LogParam( "CriAtomExAcfConfig",             LogParamTypes.TYPE_UINTPTR ),               /*  53 */
		new LogParam( "acf_data",                       LogParamTypes.TYPE_UINTPTR ),               /*  54 */
		new LogParam( "acf_data_size",                  LogParamTypes.TYPE_INT32 ),                 /*  55 */
		new LogParam( "CriFsBinderHn",                  LogParamTypes.TYPE_UINTPTR ),               /*  56 */
		new LogParam( "path",                           LogParamTypes.TYPE_CHAR ),                  /*  57 */
		new LogParam( "acb_data",                       LogParamTypes.TYPE_UINTPTR ),               /*  58 */
		new LogParam( "acb_data_size",                  LogParamTypes.TYPE_INT32 ),                 /*  59 */
		new LogParam( "awb_path",                       LogParamTypes.TYPE_CHAR ),                  /*  60 */
		new LogParam( "awb_id",                         LogParamTypes.TYPE_INT32 ),                 /*  61 */
		new LogParam( "acb_path",                       LogParamTypes.TYPE_CHAR ),                  /*  62 */
		new LogParam( "acb_id",                         LogParamTypes.TYPE_INT32 ),                 /*  63 */
		new LogParam( "CriAtomExAcbHn",                 LogParamTypes.TYPE_UINTPTR ),               /*  64 */
		new LogParam( "sw",                             LogParamTypes.TYPE_INT8 ),                  /*  65 */
		new LogParam( "CriAtomExResumeMode",            LogParamTypes.TYPE_INT8 ),                  /*  66 */
		new LogParam( "error_string",                   LogParamTypes.TYPE_CHAR ),                  /*  67 */
		new LogParam( "CriAtomSoundPlaybackId",         LogParamTypes.TYPE_INT32 ),                 /*  68 */
		new LogParam( "CriAtomSoundPlayerHn",           LogParamTypes.TYPE_UINTPTR ),               /*  69 */
		new LogParam( "CriAtomAwbHn",                   LogParamTypes.TYPE_UINTPTR ),               /*  70 */
		new LogParam( "CriAtomExCueId",                 LogParamTypes.TYPE_INT32 ),                 /*  71 */
		new LogParam( "cue_name",                       LogParamTypes.TYPE_CHAR ),                  /*  72 */
		new LogParam( "CriAtomExCueIndex",              LogParamTypes.TYPE_INT32 ),                 /*  73 */
		new LogParam( "buffer",                         LogParamTypes.TYPE_UINTPTR ),               /*  74 */
		new LogParam( "size",                           LogParamTypes.TYPE_INT32 ),                 /*  75 */
		new LogParam( "CriAtomExWaveId",                LogParamTypes.TYPE_INT32 ),                 /*  76 */
		new LogParam( "CriAtomAwbHn for Memory",        LogParamTypes.TYPE_UINTPTR ),               /*  77 */
		new LogParam( "CriAtomAwbHn for Stream",        LogParamTypes.TYPE_UINTPTR ),               /*  78 */
		new LogParam( "CriAtomExTweenHn",               LogParamTypes.TYPE_UINTPTR ),               /*  79 */
		new LogParam( "CriAtomExConfig",                LogParamTypes.TYPE_UINTPTR ),               /*  80 */
		new LogParam( "CriAtomExAsrConfig",             LogParamTypes.TYPE_UINTPTR ),               /*  81 */
		new LogParam( "CriAtomExHcaMxConfig",           LogParamTypes.TYPE_UINTPTR ),               /*  82 */
		new LogParam( "CriAtomDbasConfig",              LogParamTypes.TYPE_UINTPTR ),               /*  83 */
		new LogParam( "CriAtomStreamingCacheConfig",    LogParamTypes.TYPE_UINTPTR ),               /*  84 */
		new LogParam( "CriAtomExStandardVoicePoolConfig",LogParamTypes.TYPE_UINTPTR ),              /*  85 */
		new LogParam( "CriAtomExAdxVoicePoolConfig",    LogParamTypes.TYPE_UINTPTR ),               /*  86 */
		new LogParam( "CriAtomExAhxVoicePoolConfig",    LogParamTypes.TYPE_UINTPTR ),               /*  87 */
		new LogParam( "CriAtomExHcaVoicePoolConfig",    LogParamTypes.TYPE_UINTPTR ),               /*  88 */
		new LogParam( "CriAtomExHcaMxVoicePoolConfig",  LogParamTypes.TYPE_UINTPTR ),               /*  89 */
		new LogParam( "CriAtomExWaveVoicePoolConfig",   LogParamTypes.TYPE_UINTPTR ),               /*  90 */
		new LogParam( "CriAtomExRawPcmVoicePoolConfig", LogParamTypes.TYPE_UINTPTR ),               /*  91 */
		new LogParam( "CriAtomExPlayerConfig",          LogParamTypes.TYPE_UINTPTR ),               /*  92 */
		new LogParam( "CriAtomExTweenConfig",           LogParamTypes.TYPE_UINTPTR ),               /*  93 */
		new LogParam( "CriAtomDecrypterConfig",         LogParamTypes.TYPE_UINTPTR ),               /*  94 */
		new LogParam( "CriAtomEx3dSourceConfig",        LogParamTypes.TYPE_UINTPTR ),               /*  95 */
		new LogParam( "CriAtomEx3dListenerConfig",      LogParamTypes.TYPE_UINTPTR ),               /*  96 */
		new LogParam( "CriAtomExAdpcmVoicePoolConfig_3DS",LogParamTypes.TYPE_UINTPTR ),             /*  97 */
		new LogParam( "CriAtomExAdpcmVoicePoolConfig_WII",LogParamTypes.TYPE_UINTPTR ),             /*  98 */
		new LogParam( "CriAtomExVagVoicePoolConfig_PSP",LogParamTypes.TYPE_UINTPTR ),               /*  99 */
		new LogParam( "CriAtomExAtrac3VoicePoolConfig_PSP",LogParamTypes.TYPE_UINTPTR ),            /* 100 */
		new LogParam( "CriAtomExVagVoicePoolConfig_VITA",LogParamTypes.TYPE_UINTPTR ),              /* 101 */
		new LogParam( "CriAtomExAt9VoicePoolConfig_VITA",LogParamTypes.TYPE_UINTPTR ),              /* 102 */
		new LogParam( "num_groups",                     LogParamTypes.TYPE_INT16 ),                 /* 103 */
		new LogParam( "voices_per_group",               LogParamTypes.TYPE_INT16 ),                 /* 104 */
		new LogParam( "num_category_groups",            LogParamTypes.TYPE_INT16 ),                 /* 105 */
		new LogParam( "num_categories",                 LogParamTypes.TYPE_INT16 ),                 /* 106 */
		new LogParam( "reserved",                       LogParamTypes.TYPE_INT32 ),                 /* 107 */
		new LogParam( "CriAtomExFaderHn",               LogParamTypes.TYPE_UINTPTR ),               /* 108 */
		new LogParam( "Guid",                           LogParamTypes.TYPE_GUID ),                  /* 109 */
		new LogParam( "parent CriAtomExPlaybackId",     LogParamTypes.TYPE_INT32 ),                 /* 110 */
		new LogParam( "CriAtomPlayerPoolPlayerInfo",    LogParamTypes.TYPE_UINTPTR ),               /* 111 */
		new LogParam( "CriAtomSoundElementHn",          LogParamTypes.TYPE_UINTPTR ),               /* 112 */
		new LogParam( "CriAtomSoundVoiceHn",            LogParamTypes.TYPE_UINTPTR ),               /* 113 */
		new LogParam( "cause CriAtomExPlaybackId",      LogParamTypes.TYPE_INT32 ),                 /* 114 */
		new LogParam( "Index",                          LogParamTypes.TYPE_INT16 ),                 /* 115 */
		new LogParam( "NumAllPlaybacks",                LogParamTypes.TYPE_INT16 ),                 /* 116 */
		new LogParam( "NumPlaybacks",                   LogParamTypes.TYPE_INT16 ),                 /* 117 */
		new LogParam( "categories_per_playback",        LogParamTypes.TYPE_INT8 ),                  /* 118 */
		new LogParam( "enable_voice_priority_decay",    LogParamTypes.TYPE_INT8 ),                  /* 119 */
		new LogParam( "volume",                         LogParamTypes.TYPE_FLOAT32 ),               /* 120 */
		new LogParam( "CriAtomSoundElementId",          LogParamTypes.TYPE_INT32 ),                 /* 121 */
		new LogParam( "CriAtomSoundVoiceId",            LogParamTypes.TYPE_INT32 ),                 /* 122 */
		new LogParam( "Acb Name",                       LogParamTypes.TYPE_CHAR ),                  /* 123 */
		new LogParam( "CriAtomPlayerPoolPlayerInfoId",  LogParamTypes.TYPE_INT32 ),                 /* 124 */
		new LogParam( "Aisac Control",                  LogParamTypes.TYPE_FLOAT32 ),               /* 125 */
		new LogParam( "Track No",                       LogParamTypes.TYPE_INT16 ),                 /* 126 */
		new LogParam( "Mute",                           LogParamTypes.TYPE_INT8 ),                  /* 127 */
		new LogParam( "Result",                         LogParamTypes.TYPE_INT8 ),                  /* 128 */
		new LogParam( "Log Record Mode",                LogParamTypes.TYPE_INT32 ),                 /* 129 */
		new LogParam( "NumCh",                          LogParamTypes.TYPE_INT8 ),                  /* 130 */
		new LogParam( "NumLoaders",                     LogParamTypes.TYPE_INT32 ),                 /* 131 */
		new LogParam( "NumPlayers",                     LogParamTypes.TYPE_INT32 ),                 /* 132 */
		new LogParam( "Status",                         LogParamTypes.TYPE_INT8 ),                  /* 133 */
		new LogParam( "PlayingTime",                    LogParamTypes.TYPE_INT32 ),                 /* 134 */
		new LogParam( "DspBusSpectra",                  LogParamTypes.TYPE_128 ),                   /* 135 */
		new LogParam( "CpuLoad",                        LogParamTypes.TYPE_FLOAT32 ),               /* 136 */
		new LogParam( "NumUsedVoices",                  LogParamTypes.TYPE_INT32 ),                 /* 137 */
		new LogParam( "SequencePlaybackPosition",       LogParamTypes.TYPE_INT64 ),                 /* 138 */
		new LogParam( "CallbackValue",                  LogParamTypes.TYPE_INT32 ),                 /* 139 */
		new LogParam( "CallbackString",                 LogParamTypes.TYPE_CHAR ),                  /* 140 */
		new LogParam( "PeakLevel",                      LogParamTypes.TYPE_FLOAT32 ),               /* 141 */
		new LogParam( "RmsLevel",                       LogParamTypes.TYPE_FLOAT32 ),               /* 142 */
		new LogParam( "PeakHoldLevel",                  LogParamTypes.TYPE_FLOAT32 ),               /* 143 */
		new LogParam( "RequestId",                      LogParamTypes.TYPE_INT32 ),                 /* 144 */
		new LogParam( "TargetId",                       LogParamTypes.TYPE_INT32 ),                 /* 145 */
		new LogParam( "Md5",                            LogParamTypes.TYPE_GUID ),                  /* 146 */
		new LogParam( "GameVariable",                   LogParamTypes.TYPE_FLOAT32 ),               /* 147 */
		new LogParam( "GameVariableName",               LogParamTypes.TYPE_CHAR ),                  /* 148 */
		new LogParam( "TimeMs",                         LogParamTypes.TYPE_INT32 ),                 /* 149 */
		new LogParam( "SnapShotName",                   LogParamTypes.TYPE_CHAR ),                  /* 150 */
		new LogParam( "AisacControlId",                 LogParamTypes.TYPE_INT32 ),                 /* 151 */
		new LogParam( "StartTimeMs",                    LogParamTypes.TYPE_INT64 ),                 /* 152 */
		new LogParam( "SelectorName",                   LogParamTypes.TYPE_CHAR ),                  /* 153 */
		new LogParam( "LabelName",                      LogParamTypes.TYPE_CHAR ),                  /* 154 */
		new LogParam( "BlockName",                      LogParamTypes.TYPE_CHAR ),                  /* 155 */
		new LogParam( "CategoryName",                   LogParamTypes.TYPE_CHAR ),                  /* 156 */
		new LogParam( "AisacControlName",               LogParamTypes.TYPE_CHAR ),                  /* 157 */
		new LogParam( "SettingName",                    LogParamTypes.TYPE_CHAR ),                  /* 158 */
		new LogParam( "CueSheetId",                     LogParamTypes.TYPE_INT32 ),                 /* 159 */
		new LogParam( "BusNo",                          LogParamTypes.TYPE_INT8 ),                  /* 160 */
		new LogParam( "FxType",                         LogParamTypes.TYPE_INT32 ),                 /* 161 */
		new LogParam( "RemainedLoopCount",              LogParamTypes.TYPE_INT32 ),                 /* 162 */
		new LogParam( "SequenceLoopId",                 LogParamTypes.TYPE_INT16 ),                 /* 163 */
		new LogParam( "3dPosVector_Position",           LogParamTypes.TYPE_VECTOR ),                /* 164 */
		new LogParam( "3dPosVector_Velocity",           LogParamTypes.TYPE_VECTOR ),                /* 165 */
		new LogParam( "3dPosVector_Forward",            LogParamTypes.TYPE_VECTOR ),                /* 166 */
		new LogParam( "3dPosVector_Upward",             LogParamTypes.TYPE_VECTOR ),                /* 167 */
		new LogParam( "3dPosVector_FocusPoint",         LogParamTypes.TYPE_VECTOR ),                /* 168 */
		new LogParam( "3dPosVector_Cone",               LogParamTypes.TYPE_VECTOR ),                /* 169 */
		new LogParam( "3dMaxAngleAisacDelta",           LogParamTypes.TYPE_FLOAT32 ),               /* 170 */
		new LogParam( "3dEnablePriorityDecay",          LogParamTypes.TYPE_INT32 ),                 /* 171 */
		new LogParam( "3dDistanceFactor",               LogParamTypes.TYPE_FLOAT32 ),               /* 172 */
		new LogParam( "3dDistanceFocusLevel",           LogParamTypes.TYPE_FLOAT32 ),               /* 173 */
		new LogParam( "3dDirectionFocusLevel",          LogParamTypes.TYPE_FLOAT32 ),               /* 174 */
		new LogParam( "Result3dPos",                    LogParamTypes.TYPE_INT8 ),                  /* 175 */
		new LogParam( "CriAtomExAiffVoicePoolConfig",   LogParamTypes.TYPE_UINTPTR ),               /* 176 */
		new LogParam( "SoundRendererTyoe",              LogParamTypes.TYPE_INT32 ),                 /* 177 */
		new LogParam( "CriAtomExAt9VoicePoolConfig_PS4",LogParamTypes.TYPE_UINTPTR ),               /* 178 */
		new LogParam( "AverageServerTime",              LogParamTypes.TYPE_INT32 ),                 /* 179 */
		new LogParam( "AverageServerInterval",          LogParamTypes.TYPE_INT32 ),                 /* 180 */
		new LogParam( "MaxServerTime",                  LogParamTypes.TYPE_INT32 ),                 /* 181 */
		new LogParam( "MaxServerInterval",              LogParamTypes.TYPE_INT32 ),                 /* 182 */
		new LogParam( "UserLog",                        LogParamTypes.TYPE_CHAR ),                  /* 183 */
		new LogParam( "ByVoiceGroupLimitation",         LogParamTypes.TYPE_INT8 ),                  /* 184 */
		new LogParam( "ByVoicePoolLimitation",          LogParamTypes.TYPE_INT8 ),                  /* 185 */
		new LogParam( "RetryFlag",                      LogParamTypes.TYPE_INT8 ),                  /* 186 */
		new LogParam( "BusName",                        LogParamTypes.TYPE_CHAR ),                  /* 187 */
		new LogParam( "StreamType",                     LogParamTypes.TYPE_INT8 ),                  /* 188 */
		new LogParam( "MomentaryValue",                 LogParamTypes.TYPE_FLOAT32 ),               /* 189 */
		new LogParam( "ShortTermValue",                 LogParamTypes.TYPE_FLOAT32 ),               /* 190 */
		new LogParam( "IntegratedValue",                LogParamTypes.TYPE_FLOAT32 ),               /* 191 */
		new LogParam( "TotalBps",                       LogParamTypes.TYPE_FLOAT32 ),               /* 192 */
		new LogParam( "num_cues",                       LogParamTypes.TYPE_INT32 ),                 /* 193 */
		new LogParam( "SoundFormat",                    LogParamTypes.TYPE_INT32 ),                 /* 194 */
		new LogParam( "CriAtomExAdpcmVoicePoolConfig_WIIU",LogParamTypes.TYPE_UINTPTR ),            /* 195 */
		new LogParam( "AwbName",                        LogParamTypes.TYPE_CHAR ),                  /* 196 */
		new LogParam( "NumStreamAwb",                   LogParamTypes.TYPE_INT32 ),                 /* 197 */
		new LogParam( "ExPlayback_AllocateModule",      LogParamTypes.TYPE_INT8 ),                  /* 198 */
		new LogParam( "AisacControlValue",              LogParamTypes.TYPE_FLOAT32 ),               /* 199 */
		new LogParam( "NumAllPlaybacksForReact",        LogParamTypes.TYPE_INT16 ),                 /* 200 */
		new LogParam( "PreviewContext",                 LogParamTypes.TYPE_INT32 ),                 /* 201 */
		new LogParam( "max_parameter_blocks",           LogParamTypes.TYPE_INT16 ),                 /* 202 */
		new LogParam( "max_faders",                     LogParamTypes.TYPE_INT16 ),                 /* 203 */
		new LogParam( "num_buses",                      LogParamTypes.TYPE_INT16 ),                 /* 204 */
		new LogParam( "max_racks",                      LogParamTypes.TYPE_INT16 ),                 /* 205 */
		new LogParam( "output_channels_4_hcamx",        LogParamTypes.TYPE_INT8 ),                  /* 206 */
		new LogParam( "output_sampling_rate_4_hcamx",   LogParamTypes.TYPE_INT32 ),                 /* 207 */
		new LogParam( "sound_renderer_type_4_hcamx",    LogParamTypes.TYPE_INT8 ),                  /* 208 */
		new LogParam( "speaker_system",                 LogParamTypes.TYPE_INT8 ),                  /* 209 */
		new LogParam( "left_speaker_angle",             LogParamTypes.TYPE_FLOAT32 ),               /* 210 */
		new LogParam( "right_speaker_angle",            LogParamTypes.TYPE_FLOAT32 ),               /* 211 */
		new LogParam( "center_speaker_angle",           LogParamTypes.TYPE_FLOAT32 ),               /* 212 */
		new LogParam( "lfe_speaker_angle",              LogParamTypes.TYPE_FLOAT32 ),               /* 213 */
		new LogParam( "surround_left_speaker_angle",    LogParamTypes.TYPE_FLOAT32 ),               /* 214 */
		new LogParam( "surround_right_speaker_angle",   LogParamTypes.TYPE_FLOAT32 ),               /* 215 */
		new LogParam( "surround_back_left_speaker_angle",LogParamTypes.TYPE_FLOAT32 ),              /* 216 */
		new LogParam( "surround_back_right_speaker_angle",LogParamTypes.TYPE_FLOAT32 ),             /* 217 */
		new LogParam( "pan_speaker_type",               LogParamTypes.TYPE_INT8 ),                  /* 218 */
		new LogParam( "VoiceStopReason",                LogParamTypes.TYPE_INT16 ),                 /* 219 */
		new LogParam( "CriAtomExVibrationVoicePoolConfig",LogParamTypes.TYPE_UINTPTR ),             /* 220 */
		new LogParam( "TouceSenceEffectName",           LogParamTypes.TYPE_CHAR ),                  /* 221 */
		new LogParam( "dsp_name",                       LogParamTypes.TYPE_CHAR ),                  /* 222 */
		new LogParam( "dsp_object",                     LogParamTypes.TYPE_UINTPTR ),               /* 223 */
		new LogParam( "dsp_slot_no",                    LogParamTypes.TYPE_INT32 ),                 /* 224 */
		new LogParam( "dsp_plugin_type",                LogParamTypes.TYPE_INT32 ),                 /* 225 */
		new LogParam( "3dPosVector_ListenerTop",        LogParamTypes.TYPE_VECTOR ),                /* 226 */
		new LogParam( "playback_status",                LogParamTypes.TYPE_INT32 ),                 /* 227 */
		new LogParam( "instrument_instance_callback",   LogParamTypes.TYPE_UINTPTR ),               /* 228 */
		new LogParam( "instrument_instance_callback_obj", LogParamTypes.TYPE_UINTPTR ),             /* 229 */
		new LogParam( "instrument_instance_attach_player",LogParamTypes.TYPE_UINTPTR ),             /* 230 */
		new LogParam( "instrument_instance",            LogParamTypes.TYPE_UINTPTR ),               /* 231 */
		new LogParam( "CriAtomEx3dTransceiverHn",       LogParamTypes.TYPE_UINTPTR ),               /* 232 */
		new LogParam( "CriAtomEx3dTransceiverConfig",   LogParamTypes.TYPE_UINTPTR ),               /* 233 */
		new LogParam( "CriAtomEx3dRegionHn",            LogParamTypes.TYPE_UINTPTR ),               /* 234 */
		new LogParam( "CriAtomEx3dTransceiverDirectAudioRadius", LogParamTypes.TYPE_FLOAT32 ),      /* 235 */
		new LogParam( "CriAtomEx3dTransceiverCrossFadeDistance", LogParamTypes.TYPE_FLOAT32 ),      /* 236 */
		new LogParam( "program_no",                     LogParamTypes.TYPE_INT32 ),                 /* 237 */
		new LogParam( "key_no",                         LogParamTypes.TYPE_INT32 ),                 /* 238 */
		new LogParam( "Velocity",                       LogParamTypes.TYPE_INT16 ),                 /* 239 */
		new LogParam( "PitchBend",                      LogParamTypes.TYPE_INT16 ),                 /* 240 */

		new LogParam("Format",                          LogParamTypes.TYPE_INT32 ),                 /* 241 */ /* CriAtomFormat */
		new LogParam("MaxRhythmTracks",                 LogParamTypes.TYPE_INT16 ),                 /* 242 */
		new LogParam("MaxMelodyTracks",                 LogParamTypes.TYPE_INT16 ),                 /* 243 */
		new LogParam("MaxVocalTracks",                  LogParamTypes.TYPE_INT16 ),                 /* 244 */
		new LogParam("AdmPlayerHn",                     LogParamTypes.TYPE_UINTPTR ),               /* 245 */

		new LogParam("SpeakerMapping",					LogParamTypes.TYPE_INT32 ),                 /* 246 */ /* CriAtomExAsrConfig.speaker_mapping */
		new LogParam("AmbisonicsOrderType",				LogParamTypes.TYPE_INT32 ),                 /* 247 */ /* CriAtomExAsrConfig.ambisonics_order_type */
		new LogParam("MaxAisacs",						LogParamTypes.TYPE_INT8 ),                  /* 248 */ /* CriAtomExConfig.max_aisacs */
		new LogParam("MaxBusSends",						LogParamTypes.TYPE_INT8 ),                  /* 249 */ /* CriAtomExConfig.max_bus_sends */
		new LogParam("RackId",							LogParamTypes.TYPE_INT32 ),                 /* 250 */ /* LOG_STRINGS_ITEM_RackId */
		new LogParam("group_no",						LogParamTypes.TYPE_INT32 ),                 /* 251 */
		new LogParam("priority",						LogParamTypes.TYPE_INT32 ),                 /* 252 */
		new LogParam("Solo",							LogParamTypes.TYPE_INT8 ),                  /* 253 */
		new LogParam("Param_Id",						LogParamTypes.TYPE_INT32 ),                 /* 254 */
		new LogParam("Param_Name",                      LogParamTypes.TYPE_CHAR ),                  /* 255 */
		new LogParam("Param_Index",                		LogParamTypes.TYPE_INT32 ),                 /* 256 */
		new LogParam("Param_Float32",					LogParamTypes.TYPE_FLOAT32 ),               /* 257 */
		new LogParam("Param_Sint32",					LogParamTypes.TYPE_INT32 ),                 /* 258 */
		new LogParam("Param_Uint32",					LogParamTypes.TYPE_INT32 ),                 /* 259 */
		new LogParam("BlockIndex",						LogParamTypes.TYPE_INT32 ),                 /* 260 */
		new LogParam("PlaybackCancelType",				LogParamTypes.TYPE_INT32 ),                 /* 261 */ /* CriAtomExPlaybackCancelType */
		new LogParam("ModificationIgnoredReason",  		LogParamTypes.TYPE_INT32 ),                 /* 262 */
		new LogParam("ModifiedParam",              		LogParamTypes.TYPE_INT32 ),                 /* 263 */
		new LogParam("ModifiedId",                      LogParamTypes.TYPE_INT16 ),                 /* 264 */
		new LogParam("ModifiedName",                    LogParamTypes.TYPE_CHAR ),                  /* 265 */
		new LogParam("ModifiedIndex",              		LogParamTypes.TYPE_INT16 ),                 /* 266 */
		new LogParam("CategoryId",                 		LogParamTypes.TYPE_INT32 ),                 /* 267 */
		new LogParam("TweenParamType",             		LogParamTypes.TYPE_INT32 ),                 /* 268 */
		new LogParam("TweenParamId",               		LogParamTypes.TYPE_INT32 ),                 /* 269 */
		new LogParam("TweenAisacId",               		LogParamTypes.TYPE_INT32 ),                 /* 270 */
		new LogParam("TweenTime",                  		LogParamTypes.TYPE_INT16 ),                 /* 271 */
		new LogParam("TweenValue",                 		LogParamTypes.TYPE_FLOAT32 ),               /* 272 */
		new LogParam("Parameter2Hn",                    LogParamTypes.TYPE_UINTPTR ),               /* 273 */
		new LogParam("LogId",                      		LogParamTypes.TYPE_INT64 ),                 /* 274 */
		new LogParam("PacketVersion",              		LogParamTypes.TYPE_INT64 ),                 /* 275 */
		new LogParam("SelectorIndex",              		LogParamTypes.TYPE_INT16 ),                 /* 276 */
		new LogParam("LabelIndex",                 		LogParamTypes.TYPE_INT16 ),                 /* 277 */
		new LogParam("GlobalAisacName",                 LogParamTypes.TYPE_CHAR ),
		new LogParam("PanPitch",                   		LogParamTypes.TYPE_FLOAT32 ),
		new LogParam("PanAngle",                   		LogParamTypes.TYPE_FLOAT32 ),
		new LogParam("InteriorDistance",           		LogParamTypes.TYPE_FLOAT32 ),
		new LogParam("PanType",                    		LogParamTypes.TYPE_INT32 ),
		new LogParam("PanSpeakerType",             		LogParamTypes.TYPE_INT32 ),
		new LogParam("PanAngleType",               		LogParamTypes.TYPE_INT32 ),
		new LogParam("PanSpread",                  		LogParamTypes.TYPE_FLOAT32 ),
		new LogParam("BusSendLevel",               		LogParamTypes.TYPE_FLOAT32 ),
		new LogParam("LevelOffset",                		LogParamTypes.TYPE_FLOAT32 ),
		new LogParam("CofHigh",                    		LogParamTypes.TYPE_FLOAT32 ),
		new LogParam("CofLow",                     		LogParamTypes.TYPE_FLOAT32 ),
		new LogParam("BiquadFilterType",           		LogParamTypes.TYPE_INT32 ),
		new LogParam("Frequency",                  		LogParamTypes.TYPE_FLOAT32 ),
		new LogParam("Gain",                       		LogParamTypes.TYPE_FLOAT32 ),
		new LogParam("QValue",                     		LogParamTypes.TYPE_FLOAT32 ),
		new LogParam("Ex3dSourceListHn",           		LogParamTypes.TYPE_UINTPTR ),
		new LogParam("CurveType",                  		LogParamTypes.TYPE_INT32 ),
		new LogParam("CurveStrength",              		LogParamTypes.TYPE_FLOAT32 ),
		new LogParam("EnvelopeSustainLevel",       		LogParamTypes.TYPE_FLOAT32 ),
		new LogParam("GlobalAisacIndex",           		LogParamTypes.TYPE_INT16 ),
		new LogParam("CuePriority",                		LogParamTypes.TYPE_INT32 ),
		new LogParam("DataRequestCallbackFunc",    		LogParamTypes.TYPE_UINTPTR ),
		new LogParam("UserObject",                 		LogParamTypes.TYPE_UINTPTR ),
		new LogParam("RandomSeed",                 		LogParamTypes.TYPE_INT32 ),
		new LogParam("BlockIndex",                 		LogParamTypes.TYPE_INT32 ),
		new LogParam("ExPlayerCallback",           		LogParamTypes.TYPE_UINTPTR ),
		new LogParam("ExPlaybackCallback",         		LogParamTypes.TYPE_UINTPTR ),
		new LogParam("SamplingRate",               		LogParamTypes.TYPE_INT32 ),
		new LogParam("VoiceControlMethod",         		LogParamTypes.TYPE_INT32 ),
		new LogParam("HcaMxMixerId",               		LogParamTypes.TYPE_INT32 ),
		new LogParam("AsrRackIdArray",                  LogParamTypes.TYPE_INT32ARRAY ),
		new LogParam("PlaybackRatio",              		LogParamTypes.TYPE_FLOAT32 ),
		new LogParam("LoopCount",                  		LogParamTypes.TYPE_INT32 ),
		new LogParam("MixdownCenterVolumeOffset",  		LogParamTypes.TYPE_FLOAT32 ),
		new LogParam("MixdownLfeVolumeOffset",     		LogParamTypes.TYPE_FLOAT32 ),
		new LogParam("ExPanCallback",              		LogParamTypes.TYPE_UINTPTR ),
		new LogParam("ChannelsPerTrack",                LogParamTypes.TYPE_INT32ARRAY ),
		new LogParam("SilentMode",                 		LogParamTypes.TYPE_INT32 ),
		new LogParam("ExFilterCallback",           		LogParamTypes.TYPE_UINTPTR ),
		new LogParam("BypassFlag",                      LogParamTypes.TYPE_INT8 ),
		new LogParam("BlockTransitionCallback",    		LogParamTypes.TYPE_UINTPTR ),
		new LogParam("SpeakerId",                  		LogParamTypes.TYPE_INT32 ),
		new LogParam("SendLevelOffset",            		LogParamTypes.TYPE_FLOAT32 ),
		new LogParam("SendLevelGain",              		LogParamTypes.TYPE_FLOAT32 ),
		new LogParam("ExPlayerPlaybackTrackInfoNotificationCallback", LogParamTypes.TYPE_UINTPTR ),
		new LogParam("ExPlaybackEventCallback",    		LogParamTypes.TYPE_UINTPTR ),
		new LogParam("ChannelConfig",              		LogParamTypes.TYPE_INT32 ),
		new LogParam("InsideAngle",                		LogParamTypes.TYPE_FLOAT32 ),
		new LogParam("OutsideAngle",               		LogParamTypes.TYPE_FLOAT32 ),
		new LogParam("OutsideVolume",              		LogParamTypes.TYPE_FLOAT32 ),
		new LogParam("MinAttenuationDistance",     		LogParamTypes.TYPE_FLOAT32 ),
		new LogParam("MaxAttenuationDistance",     		LogParamTypes.TYPE_FLOAT32 ),
		new LogParam("SourceRadius",               		LogParamTypes.TYPE_FLOAT32 ),
		new LogParam("InteriorDistance",           		LogParamTypes.TYPE_FLOAT32 ),
		new LogParam("DopplerFactor",              		LogParamTypes.TYPE_FLOAT32 ),
		new LogParam("MaxAngleAisacDelta",         		LogParamTypes.TYPE_FLOAT32 ),
		new LogParam("Ex3dSourceRandomPositionConfig", 	LogParamTypes.TYPE_UINTPTR ),
		new LogParam("Ex3dSourceRandomPositionCalculationCallback", LogParamTypes.TYPE_UINTPTR ),
		new LogParam("ExVectorArray",                   LogParamTypes.TYPE_VECTORARRAY ),
		new LogParam("Ex3dSourceRandomPositionResult", 	LogParamTypes.TYPE_UINTPTR ),
		new LogParam("DopplerMultiplier",          		LogParamTypes.TYPE_FLOAT32 ),
		new LogParam("Ex3dRegionConfig",           		LogParamTypes.TYPE_UINTPTR ),
		new LogParam("TransceiverRadius",          		LogParamTypes.TYPE_FLOAT32 ),
		new LogParam("DspAfxConfig",               		LogParamTypes.TYPE_UINTPTR ),
		new LogParam("DspPitchShifterConfig",      		LogParamTypes.TYPE_UINTPTR ),
		new LogParam("DspTimeStretchConfig",       		LogParamTypes.TYPE_UINTPTR ),
		new LogParam("ExCueLiniCallback",          		LogParamTypes.TYPE_UINTPTR ),
		new LogParam("AngleArray",                      LogParamTypes.TYPE_FLOAT32ARRAY ),
		new LogParam("ExPlaybackCancel",           		LogParamTypes.TYPE_UINTPTR ),
		new LogParam("ErrorLevel",                 		LogParamTypes.TYPE_INT32 ),
		new LogParam("ExTrackTransitionBySelectorCallback", LogParamTypes.TYPE_UINTPTR ),
		new LogParam("ExVoiceEventCallback",       		LogParamTypes.TYPE_UINTPTR ),
		new LogParam("ExVoiceInfoCallback",        		LogParamTypes.TYPE_UINTPTR ),
		new LogParam("ExMonitoringVoiceStopCallback", 	LogParamTypes.TYPE_UINTPTR ),
		new LogParam("MixerId",                    		LogParamTypes.TYPE_INT32 ),
		new LogParam("FrequencyRatio",             		LogParamTypes.TYPE_FLOAT32 ),
		new LogParam("ExAcbHandleCallback",        		LogParamTypes.TYPE_UINTPTR ),
		new LogParam("ExAcbDetectionInGamePreviewDataCallback", LogParamTypes.TYPE_UINTPTR ),
		new LogParam("ExSequencerEventCallback",   		LogParamTypes.TYPE_UINTPTR ),
		new LogParam("ExBeatSyncCallback",         		LogParamTypes.TYPE_UINTPTR ),
		new LogParam("ExStreamingCacheId",         		LogParamTypes.TYPE_UINTPTR ),
		new LogParam("Ex3dSoundObjectConfig",      		LogParamTypes.TYPE_UINTPTR ),
		new LogParam("EnableVoiceLimitScope",           LogParamTypes.TYPE_INT8 ),
		new LogParam("EnableCategoryCueLimitScope",     LogParamTypes.TYPE_INT8 ),
		new LogParam("SoundObjectHn",              		LogParamTypes.TYPE_UINTPTR ),
		new LogParam("PanInfoAngle",               		LogParamTypes.TYPE_FLOAT32 ),
		new LogParam("PanInfoDistance",            		LogParamTypes.TYPE_FLOAT32 ),
		new LogParam("PanInfoSpead",               		LogParamTypes.TYPE_FLOAT32 ),
		new LogParam("PanInfoVolume",              		LogParamTypes.TYPE_FLOAT32 ),
		new LogParam("InpuChannels",               		LogParamTypes.TYPE_INT32 ),
		new LogParam("BusMatrix",                       LogParamTypes.TYPE_FLOAT32ARRAY ),
		new LogParam("SendToBusName",              		LogParamTypes.TYPE_CHAR ),
		new LogParam("SendLevel",                  		LogParamTypes.TYPE_FLOAT32 ),
		new LogParam("ParameterValue",             		LogParamTypes.TYPE_FLOAT32 ),
		new LogParam("ExAsrBusAnalyzerInterval",   		LogParamTypes.TYPE_INT32 ),
		new LogParam("ExAsrBusAnalyzerPeakHoldTimeMs", 	LogParamTypes.TYPE_INT32 ),
		new LogParam("ExAsrBusPreFilterCallback",  		LogParamTypes.TYPE_UINTPTR ),
		new LogParam("ExAsrBusPostFilterCallback", 		LogParamTypes.TYPE_UINTPTR ),
		new LogParam("ExAsrAfxInterfaceWithVersionPtr", LogParamTypes.TYPE_UINTPTR ),
		new LogParam("ExAsrRackConfig",            		LogParamTypes.TYPE_UINTPTR ),
		new LogParam("DspSettingName",             		LogParamTypes.TYPE_CHAR ),
		new LogParam("OutputRackId",               		LogParamTypes.TYPE_INT32 ),
		new LogParam("AltRackId",                  		LogParamTypes.TYPE_INT32 ),
		new LogParam("NumSamples",                 		LogParamTypes.TYPE_INT32 ),
		new LogParam("ExOutputPortHn",             		LogParamTypes.TYPE_UINTPTR ),
		new LogParam("ExOutputPortName",                LogParamTypes.TYPE_INT8 ),
		new LogParam("ExOutputPortConfig",         		LogParamTypes.TYPE_UINTPTR ),
		new LogParam("ExOutputPortType",           		LogParamTypes.TYPE_INT32 ),
		new LogParam("MaxIgnoredCategories",       		LogParamTypes.TYPE_INT32 ),
		new LogParam("Channel",                    		LogParamTypes.TYPE_INT32 ),
		new LogParam("ChannelLevel",               		LogParamTypes.TYPE_FLOAT32 ),
		new LogParam("SpatializerType",            		LogParamTypes.TYPE_INT32 ),
		new LogParam("ExAsrSpatializerInterfacePtr", 	LogParamTypes.TYPE_UINTPTR ),
		new LogParam("ExAsrSpatializerInitializeConfigPtr", LogParamTypes.TYPE_UINTPTR ),
		new LogParam("SendToBusNo",                		LogParamTypes.TYPE_INT32 ),
		new LogParam("Ex3dSourceRandomPositionResultCallback", LogParamTypes.TYPE_UINTPTR ),
		new LogParam("DirectAuiodRadius",          		LogParamTypes.TYPE_FLOAT32 ),
		new LogParam("CrossfadeDistance",          		LogParamTypes.TYPE_FLOAT32 ),
		new LogParam("PreviewRackType",            		LogParamTypes.TYPE_INT32 ),
		new LogParam("Flag",							LogParamTypes.TYPE_INT8 ),                 /* 398 */
		new LogParam("ReactName",						LogParamTypes.TYPE_CHAR ),                 /* 399 */
		new LogParam("EnableDecrementAisacModulationKey", LogParamTypes.TYPE_INT8 ),               /* 400 */
		new LogParam("DecrementAisacModulationKey", 	LogParamTypes.TYPE_INT32 ),                /* 401 */
		new LogParam("EnableIncrementAisacModulationKey", LogParamTypes.TYPE_INT8 ),               /* 402 */
		new LogParam("IncrementAisacModulationKey", 	LogParamTypes.TYPE_INT32 ),                /* 403 */
		new LogParam("DuckerLevel", 					LogParamTypes.TYPE_FLOAT32 ),              /* 404 */
		new LogParam("DuckerTargetType", 				LogParamTypes.TYPE_INT32 ),                /* 405 */
		new LogParam("FadeParameterEntryCurveType", 	LogParamTypes.TYPE_INT32 ),                /* 406 */
		new LogParam("FadeParameterEntryCurveStrength", LogParamTypes.TYPE_FLOAT32 ),              /* 407 */
		new LogParam("FadeParameterEntryFadeTimeMs", 	LogParamTypes.TYPE_INT16 ),                /* 408 */
		new LogParam("FadeParameterExitCurveType", 		LogParamTypes.TYPE_INT32 ),                /* 409 */
		new LogParam("FadeParameterExitCurveStrength", 	LogParamTypes.TYPE_FLOAT32 ),              /* 410 */
		new LogParam("FadeParameterExitFadeTimeMs", 	LogParamTypes.TYPE_INT16 ),                /* 411 */
		new LogParam("HoldType", 						LogParamTypes.TYPE_INT32 ),                /* 412 */
		new LogParam("HoldTimeMs", 						LogParamTypes.TYPE_INT16 ),                /* 413 */
		new LogParam("HoldTime",						LogParamTypes.TYPE_INT32 ),                /* 414 */
		new LogParam("EnablePausingCue", 				LogParamTypes.TYPE_INT8 ),                 /* 415 */
		new LogParam("InterfaceName", 					LogParamTypes.TYPE_CHAR ),                 /* 416 */
		new LogParam("InstrumentName", 					LogParamTypes.TYPE_CHAR ),                 /* 417 */
		new LogParam("ExInstrumentVoicePoolConfig", 	LogParamTypes.TYPE_UINTPTR ),              /* 418 */
		new LogParam("ExAcbReleasedCallback", 			LogParamTypes.TYPE_UINTPTR ),              /* 419 */
		new LogParam("AtomPlayerCallback", 				LogParamTypes.TYPE_UINTPTR ),              /* 420 */
		new LogParam("EnableAtomSoundDisabledMode", 	LogParamTypes.TYPE_INT8 ),                 /* 421 */
		new LogParam("EnableAutoMatchingInPanTypeAuto", LogParamTypes.TYPE_INT8 ),                 /* 422 */
		new LogParam("EnableCategoryOverrideByExPlayer", LogParamTypes.TYPE_INT8 ),                /* 423 */
		new LogParam("SequencePrepareRatio", 			LogParamTypes.TYPE_FLOAT32 ),              /* 424 */
		new LogParam("FsThreadModel", 					LogParamTypes.TYPE_INT32 ),                /* 425 */
		new LogParam("NumBinders", 						LogParamTypes.TYPE_INT32 ),                /* 426 */
		new LogParam("NumGroupLoaders", 				LogParamTypes.TYPE_INT32 ),                /* 427 */
		new LogParam("NumStdioHandles", 				LogParamTypes.TYPE_INT32 ),                /* 428 */
		new LogParam("NumInstallers", 					LogParamTypes.TYPE_INT32 ),                /* 429 */
		new LogParam("MaxBinds", 						LogParamTypes.TYPE_INT32 ),                /* 430 */
		new LogParam("FsVersion", 						LogParamTypes.TYPE_INT32 ),                /* 431 */
		new LogParam("FsVersionString", 				LogParamTypes.TYPE_CHAR ),                 /* 432 */
		new LogParam("EnableCrcCheck", 					LogParamTypes.TYPE_INT8 ),                 /* 433 */
		new LogParam("ExAcfLocationInfoType", 			LogParamTypes.TYPE_INT32 ),                /* 434 */
		new LogParam("Version", 						LogParamTypes.TYPE_INT32 ),                /* 435 */
		new LogParam("VersionEx", 						LogParamTypes.TYPE_INT32 ),                /* 436 */
		new LogParam("VersionString", 					LogParamTypes.TYPE_CHAR ),                 /* 437 */
		new LogParam("VersionExString", 				LogParamTypes.TYPE_CHAR ),                 /* 438 */
		new LogParam("ExResourceType", 					LogParamTypes.TYPE_INT32 ),                /* 439 */
		new LogParam("BusIndex", 						LogParamTypes.TYPE_INT32 ),                /* 440 */
		new LogParam("DistanceFactor", 					LogParamTypes.TYPE_FLOAT32 ),              /* 441 */
		new LogParam("ConeOrientation", 				LogParamTypes.TYPE_VECTOR ),               /* 442 */
		new LogParam("IntervalMs", 						LogParamTypes.TYPE_INT32 ),                /* 443 */
		new LogParam("HoldTimeMsSint32", 				LogParamTypes.TYPE_INT32 ),                /* 444 */
		new LogParam("ShortTermTimeMs", 				LogParamTypes.TYPE_INT32 ),                /* 445 */
		new LogParam("IntegratedTimeMs", 				LogParamTypes.TYPE_INT32 ),                /* 446 */
		new LogParam("SampleClipping",					LogParamTypes.TYPE_INT8 ),                 /* 447 */
		new LogParam("Marker_AtomExConfig", 			LogParamTypes.TYPE_INT8 ),                 /* 448 */
		new LogParam("Marker_AsrConfig", 				LogParamTypes.TYPE_INT8 ),                 /* 449 */
		new LogParam("Marker_HcaMxConfig", 				LogParamTypes.TYPE_INT8 ),                 /* 450 */
		new LogParam("DspID", 							LogParamTypes.TYPE_INT32 ),                /* 451 */
		new LogParam("FollowsOriginalSource", 			LogParamTypes.TYPE_INT8 ),                 /* 452 */
		new LogParam("Ex3dSourceRandomPositionCalculationType", LogParamTypes.TYPE_INT32 ),        /* 453 */
		new LogParam("Ex3dSourceRandomPositionCalculationParameters", LogParamTypes.TYPE_FLOAT32ARRAY ), /* 454 */
		new LogParam("RandomPositionListMaxLength", 	LogParamTypes.TYPE_INT32 ),                /* 456 */
		new LogParam("ExPlaybackId_unique64", 			LogParamTypes.TYPE_INT64 ),                /* 457 */
		new LogParam("cause ExPlaybackId_unique64", 	LogParamTypes.TYPE_INT64 ),                /* 458 */
		new LogParam("parent CriAtomExPlaybackId_unique64", LogParamTypes.TYPE_INT64 ),            /* 459 */
		new LogParam("CriAtomSoundVoiceId_unique64",	LogParamTypes.TYPE_INT64 ),                /* 460 */
	
		new LogParam("TimeMsFloat", 					LogParamTypes.TYPE_FLOAT32 ),              /* 461 */
		new LogParam("TimeMsInt16", 					LogParamTypes.TYPE_INT16 ),                /* 462 */
		new LogParam("MaxIgnoredCategories", 			LogParamTypes.TYPE_INT32 ),                /* 463 */
		new LogParam("NumDsp", 							LogParamTypes.TYPE_INT32 ),                /* 464 */
		new LogParam("ConfigParameters", 				LogParamTypes.TYPE_FLOAT32ARRAY ),         /* 465 */
		new LogParam("DefaultParameters", 				LogParamTypes.TYPE_FLOAT32ARRAY ),         /* 466 */
		new LogParam("AfxInterfacePtr", 				LogParamTypes.TYPE_UINTPTR ),              /* 467 */
		new LogParam("PitchShiftMode", 					LogParamTypes.TYPE_INT32 ),                /* 468 */
		new LogParam("WindowSize", 						LogParamTypes.TYPE_INT32 ),                /* 469 */
		new LogParam("OverlapTimes", 					LogParamTypes.TYPE_INT32 ),                /* 470 */
		new LogParam("PanInfoWideness", 				LogParamTypes.TYPE_FLOAT32 ),              /* 471 */
		new LogParam("EnableAudioSyncedTimer", 			LogParamTypes.TYPE_INT32 ),                /* 472 */
		new LogParam("VoiceAllocationMethod", 			LogParamTypes.TYPE_INT32 ),                /* 473 */
		new LogParam("ACBBinderHn", 					LogParamTypes.TYPE_UINTPTR ),              /* 474 */
		new LogParam("AWBBinderHn", 					LogParamTypes.TYPE_UINTPTR ),              /* 475 */
		new LogParam("ExMp3VoicePoolConfig_PS5", 		LogParamTypes.TYPE_UINTPTR ),              /* 476 */
		new LogParam("ExMp3VoicePoolConfig_PS4", 		LogParamTypes.TYPE_UINTPTR ),              /* 477 */
		new LogParam("NumUsedPlayers",					LogParamTypes.TYPE_INT32 ),                /* 478 */
		new LogParam("StartTimeUs",						LogParamTypes.TYPE_INT64 ),                /* 479 */
		new LogParam("ExStopOption",					LogParamTypes.TYPE_INT32 ),					/* 480 */
	};

	protected const uint LOG_MODE_OFF = 0;
	protected const uint LOG_MODE_PLAYBACK = 1;
	protected const uint LOG_MODE_ERROR = 1 << 1;
	protected const uint LOG_MODE_LOW_LEVEL_PLAYBACK = 1 << 2;
	protected const uint LOG_MODE_SYSTEM_INFO = 1 << 3;
	protected const uint LOG_MODE_HANDLE_INFO = 1 << 4;
	protected const uint LOG_MODE_CUE_LIMIT = 1 << 5;
	protected const uint LOG_MODE_PROBABILITY = 1 << 6;
	protected const uint LOG_MODE_CATEGORY = 1 << 7;
	protected const uint LOG_MODE_EXECUTING_INFORMATION = 1 << 8;
	protected const uint LOG_MODE_3D_INFO = 1 << 9;
	protected const uint LOG_MODE_USER_LOG = 1 << 10;
	protected const uint LOG_MODE_VOICE_VOLUME = 1 << 11;
	protected const uint LOG_MODE_ASR = 1 << 12;
	protected const uint LOG_MODE_ALL = 0xFFFFFFFF;

	protected enum LogTypes {
		NON = 0,
		PLAYBACK,
		ERROR,
		LOW_LEVEL_PLAYBACK,
		SYSTEM_INFORMATION,
		HANDLE_INFORMATION,
		CUE_LIMIT,
		PROBABILITY,
		CATEGORY,
		EXECUTING_INFORMATION,
		INFORMATION_3D,
		USER_LOG,
		VOICE_VOLUME,
		ASR
	};

	protected const uint FORMAT_NONE = 0;
	protected const uint FORMAT_ADX = 1 << 0;
	protected const uint FORMAT_HCA = 1 << 2;
	protected const uint FORMAT_HCA_MX = 1 << 3;
	protected const uint FORMAT_PCM = 1 << 4;
	protected const uint FORMAT_WAVE = 1 << 5;
	protected const uint FORMAT_RAW_PCM = 1 << 6;
	protected const uint FORMAT_AIFF = 1 << 7;
	protected const uint FORMAT_VIBRATION = 1 << 8;
	protected const uint FORMAT_AUDIO_BUFFER = 1 << 9;
	protected const uint FORMAT_SOUND_GENERATOR = 1 << 10;
	protected const uint FORMAT_RAW_PCM_FLOAT = 1 << 11;
	protected const uint FORMAT_INSTRUMENT = 1 << 12;
	protected const uint FORMAT_INPUT_PORT = 1 << 13;

	protected enum StreamTypes {
		UNKNOWN = 0,
		ONMEMORY,
		STREAM,
		ZERO_LATENCY_STREAM,
	}
}

} //namespace CriWare

#endif