/****************************************************************************
 *
 * Copyright (c) 2021 CRI Middleware Co., Ltd.
 *
 ****************************************************************************/

/*---------------------------
 * Force Load Data with Async Method Defines
 *---------------------------*/
#if UNITY_WEBGL
#define CRIWARE_FORCE_LOAD_ASYNC
#endif

using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

/**
 * \addtogroup CRIWARE_EDITOR_UTILITY
 * @{
 */

/**
 * <summary>
 * CriWare.Editor 名前空間
 * </summary>
 * <remarks>
 * <para header='説明'>
 * UnityEditor 環境で使用できるクラスをまとめた名前空間です。
 * </para>
 * </remarks>
 */
namespace CriWare.Editor {

/**
 * <summary> CRI Atom エディタ専用機能クラス </summary>
 * <remarks>
 * <para header='説明'>
 * CRI Atomのエディタ専用機能をまとめるクラスです。</para>
 * <para header='注意'>
 * AssemblyDefinition版プラグインで上記クラスを使用する場合、<br/>
 * 参照元のアセンブリ定義に以下のアセンブリ定義への参照が必要になります。<br/>
 * - CriMw.CriWare.Editor<br/>
 * - CriMw.CriWare.Runtime<br/>
 * </para>
 * </remarks>
 */
public class CriAtomEditorUtilities
{
	/**
	 * <summary>エディタでCRI Atomライブラリ初期化</summary>
	 * <returns>初期化処理が実行されたか</returns>
	 * <remarks>
	 * <para header='説明'>
	 * Editモード専用のCRI Atomライブラリを初期化するためのメソッドです。<br/>
	 * プロジェクト設定のAtomエディタ設定に従いAtomの初期化を行います。<br/>
	 * プロジェクト設定によっては、現在開いているシーンにあるCriWareInitializer
	 * コンポーネントの設定を使用して初期化を行うことも可能です。</para>
	 * </remarks>
	 */
	public static bool InitializeLibrary() {
		if(EditorApplication.isPlayingOrWillChangePlaymode) { return false; }

		bool settingChanged = CriAtomEditorSettings.Instance.GetChangeStatusOnce() || CriCommonEditorSettings.Instance.GetChangeStatusOnce();
		if (CriAtomPlugin.IsLibraryInitialized() && settingChanged == false) {
			return false;
		}
		if (settingChanged) { 
			CriAtomPlugin.FinalizeLibrary();
		}
		
		CriFsConfig fsConfigEditor = CriCommonEditorSettings.Instance.FsConfig;
		CriAtomConfig atomConfigEditor = CriAtomEditorSettings.Instance.AtomConfig;
		if (CriAtomEditorSettings.Instance.TrySceneSettings == true) {
			CriWareInitializer criInitializer = null;
#if UNITY_2023_1_OR_NEWER
			criInitializer = GameObject.FindAnyObjectByType<CriWareInitializer>();
#else
			criInitializer = GameObject.FindObjectOfType<CriWareInitializer>();
#endif
			if (criInitializer != null) {
				atomConfigEditor = criInitializer.atomConfig;
			} else {
				Debug.Log("[CRIWARE] Atom Preview: No CriWareInitializer component found in current scene. " +
					"Using project settings instead.");
			}
		}
		if (CriCommonEditorSettings.Instance.TryFsSceneSettings == true) {
			CriWareInitializer criInitializer = null;
#if UNITY_2023_1_OR_NEWER
			criInitializer = GameObject.FindAnyObjectByType<CriWareInitializer>();
#else
			criInitializer = GameObject.FindObjectOfType<CriWareInitializer>();
#endif
			if (criInitializer != null) {
				fsConfigEditor = criInitializer.fileSystemConfig;
			} else {
				Debug.Log("[CRIWARE] File System: No CriWareInitializer component found in current scene. " +
					"Using project settings instead.");
			}
		}

		CriWareInitializer.InitializeFileSystem(fsConfigEditor);
		bool initRes = CriWareInitializer.InitializeAtom(atomConfigEditor);

		return initRes;
	}

	/**
	 * <summary>エディタでの音声プレビュー再生専用ACB読込</summary>
	 * <returns>読み込まれたACBデータのインスタンス</returns>
	 * <remarks>
	 * <para header='説明'>Editモード専用のACB読込のメソッドです。<br/>
	 * AtomBrowser、CriAtomSourceインスペクターなどでのプレビュー再生に用いられます。<br/>
	 * Unityエディタ拡張でACBデータ読込みを行う場合はこのメソッドを使ってください。</para>
	 * </remarks>
	 */
	public static CriAtomExAcb LoadAcbFile(CriFsBinder binder, string acbPath, string awbPath) {
		if (CriWare.Common.IsStreamingAssetsPath(acbPath)) {
			acbPath = System.IO.Path.Combine(CriWare.Common.streamingAssetsPath, acbPath);
		}

		awbPath = string.IsNullOrEmpty(awbPath) ? null : awbPath;
		if (awbPath != null && CriWare.Common.IsStreamingAssetsPath(awbPath)) {
			awbPath = System.IO.Path.Combine(CriWare.Common.streamingAssetsPath, awbPath);
		}

#if CRIWARE_FORCE_LOAD_ASYNC
		bool loadAwbOnMemory = false;
#if UNITY_WEBGL
		loadAwbOnMemory = true;
#endif
		using (var asyncLoader = CriAtomExAcbLoader.LoadAcbFileAsync(binder, acbPath, awbPath, loadAwbOnMemory)) {
			if (asyncLoader == null) {
				return null;
			}

			// forced synchronization of asynchronous process in Edit Mode
			int loopBreakingLimit = 2000, curLoopCnt = 0;
			while (curLoopCnt < loopBreakingLimit) {
				var status = asyncLoader.GetStatus();
				if (status == CriAtomExAcbLoader.Status.Complete) {
					return asyncLoader.MoveAcb();
				} else if (status == CriAtomExAcbLoader.Status.Error) {
					return null;
				}
				System.Threading.Thread.Sleep(1);
				curLoopCnt++;
			}

			Debug.LogWarning("[CRIWARE] Timed out loading AWB for previewing. The AWB file might be too large. AWB file path: " + awbPath);

			return null;
		}
#else
		return CriAtomExAcb.LoadAcbFile(binder, acbPath, awbPath);
#endif
	}

	/**
	 * <summary>エディタプレビュー専用音声プレイヤー</summary>
	 * <remarks>
	 * <para header='説明'>
	 * EditモードでADX音声をプレビュー再生するためのクラスです。<br/>
	 * ACBデータのロードは外部で管理する必要があります。</para>
	 * </remarks>
	 */
	public class PreviewPlayer : CriDisposable {
		public CriAtomExPlayer player { get; private set; }
		private bool finalizeSuppressed = false;
		private bool isPlayerReady = false;

		private void Initialize() {
			CriAtomEditorUtilities.InitializeLibrary();
			if (CriAtomPlugin.IsLibraryInitialized() == false) { return; }

			player = new CriAtomExPlayer();
			if (player == null) { return; }

			player.SetPanType(CriAtomEx.PanType.Pan3d);
			player.UpdateAll();

			isPlayerReady = true;

			CriDisposableObjectManager.Register(this, CriDisposableObjectManager.ModuleType.Atom);

			if (finalizeSuppressed) {
				GC.ReRegisterForFinalize(this);
			}
		}

		/**
		 * <summary>プレイヤーの初期化</summary>
		 */
		public PreviewPlayer() {
			Initialize();
		}

		/**
		 * <summary>プレイヤーの破棄</summary>
		 */
		public override void Dispose() {
			this.dispose();
			GC.SuppressFinalize(this);
			finalizeSuppressed = true;
		}

		private void dispose() {
			CriDisposableObjectManager.Unregister(this);
			if (player != null) {
				player.Dispose();
				player = null;
			}
			this.isPlayerReady = false;
		}

		~PreviewPlayer() {
			this.dispose();
		}
		
		/**
		 * <summary>音声データを設定して再生</summary>
		 * <param name='acb'>ACBデータ</param>
		 * <param name='cueName'>キュー名</param>
		 * <remarks>
		 * <para header='説明'>
		 * ACBデータとキュー名を指定して再生します。</para>
		 * </remarks>
		 */
		public void Play(CriAtomExAcb acb, string cueName) {
			if (isPlayerReady == false) {
				this.Initialize();
			}

			if (acb != null) {
				if (player != null) {
					player.SetCue(acb, cueName);
					player.Start();
				} else {
					Debug.LogWarning("[CRIWARE] Player is not ready. Please try reloading the inspector / editor window");
				}
			} else {
				Debug.LogWarning("[CRIWARE] ACB data is not set for previewing playback");
			}
		}

		/**
		 * <summary>再生を停止</summary>
		 * <param name='withoutRelease'>リリース時間なしで停止させるかどうか</param>
		 * <remarks>
		 * <para header='説明'>
		 * 再生中のすべての音声を停止します。</para>
		 * </remarks>
		 */
		public void Stop(bool withoutRelease = false) {
			if (player != null) {
				if (withoutRelease) {
					player.StopWithoutReleaseTime();
				} else {
					player.Stop();
				}
			}
		}

		/**
		 * <summary>プレイヤーのパラメータをリセット</summary>
		 */
		public void ResetPlayer() {
			player.SetVolume(1f);
			player.SetPitch(0);
			player.Loop(false);
		}
	}

} // end of class

} //namespace CriWare.Editor

/**
 * @}
 */

/* end of file */