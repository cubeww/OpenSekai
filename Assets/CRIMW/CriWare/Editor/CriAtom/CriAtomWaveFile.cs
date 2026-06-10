/****************************************************************************
 *
 * Copyright (c) 2021 CRI Middleware Co., Ltd.
 *
 ****************************************************************************/

using System.Runtime.InteropServices;

/**
 * \addtogroup CRIWARE_EDITOR_UTILITY
 * @{
 */

namespace CriWare.Editor {

/**
 * <summary>音声出力をバウンスしファイル書き出しを行うクラスです。</summary>
 * <remarks>
 * <para header='説明'>
 * 本クラスでは、ASRで出力された音声データをバウンスすることができます。
 * </para>
 * </remarks>
 */
public static class CriAtomWaveFile
{

	/**
	 * <summary>バウンスを開始する</summary>
	 * <param name='path'>バウンス出力先ファイルパス</param>
	 * <param name='numChannels'>バウンス出力ファイルのチャンネル数(1,2,4,6,8)</param>
	 * <returns>バウンスが開始できたか</returns>
	 * <remarks>
	 * <para header='説明'>
	 * 指定されたパラメータでバウンスを開始します。<br/>
	 * numChannelsには1,2,4,6,8が指定可能です。<br/>
	 * ASRの出力チャンネル数より、numChannelsが少ない場合は指定されたチャンネル数へ自動的にダウンミックスを行いバウンスを行います。<br/>
	 * ASRの出力チャンネル数より、numChannels数が多い場合は振幅0の波形が記録されます。<br/>
	 * </para>
	 * </remarks>
	 * <seealso cref='CriWare.Editor.CriAtomWaveFile.StopBounce'/>
	 */
	public static bool StartBounce(string path, uint numChannels)
	{
		return criAtomExAsrRack_StartBounce(0, 0, path, numChannels);
	}

	/**
	 * <summary>バウンスを停止する</summary>
	 * <remarks>
	 * <para header='説明'>
	 * CriWare.Editor.CriAtomWaveFile.StartBounce によって開始したバウンスを停止します。
	 * </para>
	 * </remarks>
	 * <seealso cref='CriWare.Editor.CriAtomWaveFile.StartBounce'/>
	 */
	public static void StopBounce()
	{
		criAtomExAsrRack_StopBounce(0, 0);
	}

	/**
	 * <summary>バウンス済み波形時間の取得(ms)</summary>
	 * <returns>バウンス済み波形の長さ</returns>
	 * <remarks>
	 * <para header='説明'>
	 * バウンス済み波形の長さを取得します。単位はミリ秒です。
	 * </para>
	 * </remarks>
	 */
	public static uint GetBounceTime()
	{
		return criAtomWaveFile_GetBounceTime();
	}

	#region DLL Import
#if (UNITY_EDITOR && !UNITY_EDITOR_LINUX) && !CRIWARE_ENABLE_HEADLESS_MODE
	[DllImport(CriWare.Common.pluginName, CallingConvention = CriWare.Common.pluginCallingConvention)]
	private static extern bool criAtomExAsrRack_StartBounce(int rack_id, int bus_no, string path, System.UInt32 num_channels);
	[DllImport(CriWare.Common.pluginName, CallingConvention = CriWare.Common.pluginCallingConvention)]
	private static extern void criAtomExAsrRack_StopBounce(int rack_id, int bus_no);
	[DllImport(CriWare.Common.pluginName, CallingConvention = CriWare.Common.pluginCallingConvention)]
	private static extern System.UInt32 criAtomWaveFile_GetBounceTime();
#else
	private static bool criAtomExAsrRack_StartBounce(int rack_id, int bus_no, string path, System.UInt32 num_channels) { return false; }
	private static void criAtomExAsrRack_StopBounce(int rack_id, int bus_no) { }
	private static System.UInt32 criAtomWaveFile_GetBounceTime() { return 0; }
#endif
		#endregion
	}

} //namespace CriWare.Editor

/**
 * @}
 */

/* end of file */