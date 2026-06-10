/****************************************************************************
 *
 * Copyright (c) CRI Middleware Co., Ltd.
 *
 ****************************************************************************/
#if !UNITY_EDITOR && UNITY_WEBGL
#define CRI_UNSUPPORTED_OUTPUTPORT
#endif

using System;
using System.Runtime.InteropServices;

namespace CriWare {
	/**
	 * <summary>Output Port</summary>
	 * <remarks>
	 * <para header='Description'>The output port is an object that makes it easier to manage and control audio output destinations.<br/>
	 * This is useful when playing on a device's secondary output, or when using platform-specific playback functions.</para>
	 * </remarks>
	 */
	public class CriAtomExOutputPort : CriDisposable
	{
		/**
		 * <summary>Output port type</summary>
		 * <remarks>
		 * <para header='Description'>This value indicates the type of the output port. <br/></para>
		 * </remarks>
		 * <seealso cref='CriWare.CriAtomExOutputPort.Config'/>
		 */
		public enum Type : System.Int32 {
			/**
			 * <summary>Sound Type</summary>
			 * <remarks>
			 * <para header='Description'>Output port type for normal audio playback. <br/></para>
			 * </remarks>
			 */
			Audio = 0,

			/**
			 * <summary>Vibration type</summary>
			 * <remarks>
			 * <para header='Description'>Output port type used to output audio-based vibrations.<br/>
			 * <br/></para>
			 * </remarks>
			 */
			Vibration = 1
		}

		/**
		 * <summary>Maximum length of output port name</summary>
		 * <remarks>
		 * <para header='Description'>Maximum string length that can be specified as the name in <see cref='CriWare.CriAtomExOutputPort.Config.name'/> .</para>
		 * </remarks>
		 * <seealso cref='CriWare.CriAtomExOutputPort.Config'/> 
		 */
		public const uint MaxNameLength = 64;

		/**
		 * <summary>Config structure used to create the output port</summary>
		 * <remarks>
		 * <para header='Description'>A structure for creating an output port.<br/>
		 * It should be passed as the argument of <see cref='CriWare.CriAtomExOutputPort.CriAtomExOutputPort'/>.<br/></para>
		 * </remarks>
		 * <seealso cref='CriWare.CriAtomExOutputPort.CriAtomExOutputPort'/>
		 */
		public struct Config {
			/**
			 * <summary>Output port name</summary>
			 * <remarks>
			 * <para header='Description'>Specify the name of the output port. <br/></para>
			 * <para header='Note'>The length of the string must be less than or equal to <see cref='CriWare.CriAtomExOutputPort.MaxNameLength'/>.<br/>
			 * Once a port name has been set, it cannot be changed.<br/></para>
			 * </remarks>
			 */
			public string name;

			/**
			 * <summary>Output port type</summary>
			 * <remarks>
			 * <para header='Description'>Specify the Output Port by type.</para>
			 * <para header='Note'>Once the type of the output port is specified, it cannot be changed. <br/></para>
			 * </remarks>
			 */
			public CriAtomExOutputPort.Type type;

			/**
			 * <summary>Max number of the ignored Categories of Output Ports</summary>
			 * <remarks>
			 * <para header='Description'>The maximum number of the ignored Categories that can be specified for an output port.</para>
			 * <para header='Note'>This value cannot be changed once it has been specified.<br/></para>
			 * </remarks>
			 */
			public System.UInt32 maxIgnoredCategories;

			/**
			 * <summary>Get the configuration structure with default values</summary>
			 * <returns>Configuration structure with default values</returns>
			 * <remarks>
			 * <para header='Description'>Get the default configuration structure to initialize <see cref='CriWare.CriAtomExOutputPort.CriAtomExOutputPort'/> .</para>
			 * </remarks>
			 */
			public static Config Default() {
				var config = new Config();
				config.name = String.Empty;
				config.type = Type.Audio;
				config.maxIgnoredCategories = 4u;
				return config;
			}
		}

		/**
		 * <summary>Whether the output port object is valid</summary>
		 * <returns>Whether the object is valid</returns>
		 * <remarks>
		 * <para header='Description'>Determine if the output port object is valid.<br/>
		 * It basically checks if the native handle held inside the object has been disposed.<br/></para>
		 * </remarks>
		 * <seealso cref='CriWare.CriAtomExOutputPort.Dispose'/>
		 */
		public bool isAvailable { get { return this.NativeHandle != IntPtr.Zero; } }

		/**
		 * <summary>Calculating the work area size required for the creation of the output port object</summary>
		 * <param name='config'>Config structure used to create the output port</param>
		 * <returns>Work area size</returns>
		 * <remarks>
		 * <para header='Description'>Calculate the size of the work area required to create the output port object.<br/>
		 * The returned value corresponds to the size of unmanaged memory allocated when calling the constructor of this class.<br/>
		 * <br/>
		 * If the calculation of the work area size fails, this function returns -1.<br/>
		 * Please check the error callback message to find out why the work area size calculation failed.<br/></para>
		 * <para header='Note'>The length of the output port name specified in <see cref='CriWare.CriAtomExOutputPort.Config.name'/><br/>
		 * must be less than or equal to <see cref='CriWare.CriAtomExOutputPort.MaxNameLength'/>.</para>
		 * </remarks>
		 * <seealso cref='CriWare.CriAtomExOutputPort.CriAtomExOutputPort'/>
		 */
		public Int32 CalculateWorkSize(CriAtomExOutputPort.Config config) {
			return NativeMethods.criAtomExOutputPort_CalculateWorkSize(config);
		}

		/**
		 * <summary>Creating the output port object</summary>
		 * <param name='config'>Config structure used to create the output port</param>
		 * <returns>Output port object</returns>
		 * <remarks>
		 * <para header='Description'>Create an output port object.<br/>
		 * Output ports are linked to ASR Racks, and Voices for which an output port is specified will be played in the ASR Rack linked to that output port.<br/></para>
		 * <code>CriAtomExOutputPort CreateOutputPort(int rackId) {
		 * 	var outputPortConfig = CriAtomExOutputPort.Config.Default();
		 * 	outputPortConfig.name = "SampleOutputPort1";
		 * 	var outputPort = new CriAtomExOutputPort(outputPortConfig);
		 * 
		 * 	// Set the AsrRack ID
		 * 	outputPort.SetAsrRackId(rackId);
		 * 
		 * 	return outputPort;
		 * }</code>
		 * <para header='Note'>The output port object set in the ACF file will be automatically created in the ACF when the ACF file is<br/>
		 * registered using <see cref='CriAtomEx.RegisterAcf'/>, so it is not necessary to use this function to create new objects.<br/>
		 * The output port object created like this can be obtained by <see cref='CriWare.CriAtomExAcf.GetOutputPort'/>.<br/>
		 * This function should be used when an additional output port object is required by the application.<br/>
		 * If successful, this function returns the output port object that was created.<br/>
		 * If it fails, null is returned.<br/></para>
		 * <para header='Note'>The output port object created by this function is assigned the following ASR Rack ID based on the type specified in <see cref='CriWare.CriAtomExOutputPort.Config.type'/>:<br/>
		 * - If <see cref='CriWare.CriAtomExOutputPort.Type.Audio'/> is specified: <see cref='CriWare.CriAtomExAsrRack.defaultRackId'/>
		 * - If <see cref='CriWare.CriAtomExOutputPort.Type.Vibration'/> is specified: <see cref='CriWare.CriAtomExAsrRack.IllegalRackId'/>
		 * .
		 * Before using the output port object, be sure to set the appropriate ASR Rack with <see cref='CriWare.CriAtomExOutputPort.SetAsrRackId'/>.<br/>
		 * <br/>
		 * Be sure to discard objects created with this function using <see cref='CriWare.CriAtomExOutputPort.Dispose'/>.</para>
		 * </remarks>
		 * <seealso cref='CriWare.CriAtomExOutputPort.Config'/>
		 * <seealso cref='CriWare.CriAtomExOutputPort.Dispose'/>
		 * <seealso cref='CriWare.CriAtomExAsrRack.rackId'/>
		 * <seealso cref='CriWare.CriAtomExAcf.GetOutputPort'/>
		 * <seealso cref='CriWare.CriAtomExPlayer.AddOutputPort'/>
		 * <seealso cref='CriWare.CriAtomExPlayer.AddPreferredOutputPort'/>
		 */
		public CriAtomExOutputPort(Config config) {
			this.NativeHandle = NativeMethods.criAtomExOutputPort_Create(ref config, IntPtr.Zero, 0);

			CriDisposableObjectManager.Register(this, CriDisposableObjectManager.ModuleType.Atom);
		}

		internal CriAtomExOutputPort(IntPtr existingNativeHandle) {
			this.NativeHandle = existingNativeHandle;
			hasExistingNativeHandle = true;
			CriDisposableObjectManager.Register(this, CriDisposableObjectManager.ModuleType.Atom);
		}

		~CriAtomExOutputPort() {
			Dispose(false);
		}

		/**
		 * <summary>Discarding the output port object</summary>
		 * <remarks>
		 * <para header='Description'>Discard the output port object <br/></para>
		 * <para header='Note'>Output port objects that have been added to a player using the following functions cannot be directly destroyed:<br/>
		 * - <see cref='CriWare.CriAtomExPlayer.AddOutputPort'/>
		 * - <see cref='CriWare.CriAtomExPlayer.AddPreferredOutputPort'/>
		 * .
		 * <br/>
		 * You can destroy an output port object by first removing it from the player, using the functions below:<br/>
		 * - <see cref='CriWare.CriAtomExPlayer.RemoveOutputPort'/>
		 * - <see cref='CriWare.CriAtomExPlayer.RemovePreferredOutputPort'/>
		 * .</para>
		 * </remarks>
		 * <seealso cref='CriWare.CriAtomExOutputPort.CriAtomExOutputPort'/>
		 * <seealso cref='CriWare.CriAtomExOutputPort.isAvailable'/>
		 * <seealso cref='CriWare.CriAtomExOutputPort.IsDestroyable'/>
		 */
		public override void Dispose() {
			Dispose(true);
		}

		protected virtual void Dispose(bool disposing) {
			CriDisposableObjectManager.Unregister(this);
			if (hasExistingNativeHandle) {
				this.NativeHandle = IntPtr.Zero;
			}

			if (isAvailable) {
				NativeMethods.criAtomExOutputPort_Destroy(NativeHandle);
				this.NativeHandle = IntPtr.Zero;
			}

			if (disposing) {
				GC.SuppressFinalize(this);
			}
		}

		/**
		 * <summary>Specification of ASR Rack ID</summary>
		 * <param name='rackId'>ASR Rack ID</param>
		 * <remarks>
		 * <para header='Description'>Specify the ASR Rack as the output port.<br/>
		 * A Voice for which an output port is specified will be played via the ASR Rack specified for that output port.</para>
		 * <para header='Note'>The appropriate ASR Rack must always be specified with this function, both for output port objects created during ACF file registration (<see cref='CriWare.CriAtomExAcf.GetOutputPort'/>)<br/>
		 * or with <see cref='CriWare.CriAtomExOutputPort.CriAtomExOutputPort'/>.
		 * .<br/>
		 * <br/>
		 * There may be restrictions on the ASR Rack that can be specified depending on the type of output port and other factors.<br/>
		 * Please refer to the manual for details.</para>
		 * <para header='Note'>Current playback will not be affected when the ASR rack ID of the output port is changed by this function.</para>
		 * </remarks>
		 * <seealso cref='CriWare.CriAtomExAsrRack.rackId'/>
		 * <seealso cref='CriWare.CriAtomExAcf.GetOutputPort'/>
		 */
		public void SetAsrRackId(Int32 rackId) {
			NativeMethods.criAtomExOutputPort_SetAsrRackId(NativeHandle, rackId);
		}


		/**
		 * <summary>Setting the channel level of the output port vibration type</summary>
		 * <param name='channel'>Channel index (0 = L, 1 = R)</param>
		 * <param name='level'>Level (0 ~ 2.0)</param>
		 * <remarks>
		 * <para header='Description'>Set the output level to each channel of the output device of vibration type. <br/></para>
		 * <para header='Note'>The output port of vibration type operates on 2 channels and a -3dB downmix is applied if the final output device is mono.<br/>
		 * The value set with this function will be reflected immediately, even while audio is playing.</para>
		 * </remarks>
		 * <seealso cref='CriWare.CriAtomExOutputPort.SetMonauralMix'/>
		 */
		public void SetVibrationChannelLevel(Int32 channel, Single level) {
			NativeMethods.criAtomExOutputPort_SetVibrationChannelLevel(NativeHandle, channel, level);
		}

		/**
		 * <summary>With/Without monaural downmix for output port of vibration type</summary>
		 * <param name='monauralMix'>With monaural mix (CRI_TRUE = Enabled, CRI_FALSE = Disabled)</param>
		 * <remarks>
		 * <para header='Description'>The output port of vibration type operates on 2 channels, so if the input data has more than 2 channels,<br/>
		 * or 3D pan is set, the output will be transmitted to the left and right of the vibration device. <br/>
		 * By enabling monaural mixing, you can eliminate these effects by downmixing to monaural before outputting to the vibration device. <br/>
		 * You can also use <see cref='CriWare.CriAtomExOutputPort.SetVibrationChannelLevel'/> to set the level sent to the vibration device after the monaural mixing. <br/></para>
		 * <para header='Note'>The value set by this function is immediately reflected, even during the audio playback.</para>
		 * </remarks>
		 * <seealso cref='CriWare.CriAtomExOutputPort.SetVibrationChannelLevel'/>
		 */
		public void SetMonauralMix(Boolean monauralMix) {
			NativeMethods.criAtomExOutputPort_SetMonauralMix(NativeHandle, monauralMix);
		}

		/**
		 * <summary>Discard the output port object</summary>
		 * <returns>Whether it can be discarded</returns>
		 * <remarks>
		 * <para header='Description'>Determine whether the output port object can be discarded.<br/>
		 * If true is returned, the object can be discarded by calling <see cref='CriWare.CriAtomExOutputPort.Dispose'/> .<br/>
		 * If false is returned, the output port object must first be removed from the player with one of the functions below:<br/>
		 * - <see cref='CriWare.CriAtomExPlayer.RemoveOutputPort'/>
		 * - <see cref='CriWare.CriAtomExPlayer.RemovePreferredOutputPort'/>
		 * .</para>
		 * </remarks>
		 * <seealso cref='CriWare.CriAtomExOutputPort.Dispose'/>
		 */
		public bool IsDestroyable() {
			return NativeMethods.criAtomExOutputPort_IsDestroyable(NativeHandle);
		}

		internal IntPtr NativeHandle { get; private set; }
		private bool hasExistingNativeHandle = false;

		private partial class NativeMethods {
#if !CRI_ENABLE_HEADLESS_MODE && !CRI_UNSUPPORTED_OUTPUTPORT
			[DllImport(CriWare.Common.pluginName, CallingConvention = CriWare.Common.pluginCallingConvention)]
			internal static extern Int32 criAtomExOutputPort_CalculateWorkSize([In] CriAtomExOutputPort.Config config);

			[DllImport(CriWare.Common.pluginName, CallingConvention = CriWare.Common.pluginCallingConvention)]
			internal static extern IntPtr criAtomExOutputPort_Create(ref CriAtomExOutputPort.Config config, IntPtr work, Int32 workSize);

			[DllImport(CriWare.Common.pluginName, CallingConvention = CriWare.Common.pluginCallingConvention)]
			internal static extern void criAtomExOutputPort_Destroy(IntPtr outputPort);

			[DllImport(CriWare.Common.pluginName, CallingConvention = CriWare.Common.pluginCallingConvention)]
			internal static extern void criAtomExOutputPort_SetAsrRackId(IntPtr outputPort, Int32 rackId);

			[DllImport(CriWare.Common.pluginName, CallingConvention = CriWare.Common.pluginCallingConvention)]
			internal static extern void criAtomExOutputPort_SetVibrationChannelLevel(IntPtr outputPort, Int32 channel, Single level);

			[DllImport(CriWare.Common.pluginName, CallingConvention = CriWare.Common.pluginCallingConvention)]
			internal static extern void criAtomExOutputPort_SetMonauralMix(IntPtr outputPort, Boolean monauralMix);

			[DllImport(CriWare.Common.pluginName, CallingConvention = CriWare.Common.pluginCallingConvention)]
			internal static extern bool criAtomExOutputPort_IsDestroyable(IntPtr outputPort);
#else
			internal static Int32 criAtomExOutputPort_CalculateWorkSize([In] CriAtomExOutputPort.Config config)
			{
				return default(Int32);
			}

			internal static IntPtr criAtomExOutputPort_Create(ref CriAtomExOutputPort.Config config, IntPtr work, Int32 workSize)
			{
				return default(IntPtr);
			}

			internal static void criAtomExOutputPort_Destroy(IntPtr outputPort)
			{
				return;
			}

			internal static void criAtomExOutputPort_SetAsrRackId(IntPtr outputPort, Int32 rackId)
			{
				return;
			}

			internal static void criAtomExOutputPort_SetVibrationChannelLevel(IntPtr outputPort, Int32 channel, Single level)
			{
				return;
			}

			internal static void criAtomExOutputPort_SetMonauralMix(IntPtr outputPort, Boolean monauralMix)
			{
				return;
			}

			internal static bool criAtomExOutputPort_IsDestroyable(IntPtr outputPort)
			{
				return default(bool);
			}
#endif
		}
	}
} // namespace CriWare