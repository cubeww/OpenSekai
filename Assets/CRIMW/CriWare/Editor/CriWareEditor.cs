/****************************************************************************
 *
 * Copyright (c) 2011 CRI Middleware Co., Ltd.
 *
 ****************************************************************************/

using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

[assembly: InternalsVisibleTo("EditorTests")]
namespace CriWare {

public class CriWareEditor : UnityEditor.Editor
{
	[MenuItem("GameObject/CRIWARE/Create CRIWARE Library Initializer", false, 150)]
	public static void CreateCriwareLibraryInitalizer()
	{
		CriWareInitializer[] criWareInitializerList;
#if UNITY_2023_1_OR_NEWER
		criWareInitializerList = FindObjectsByType<CriWareInitializer>(FindObjectsSortMode.InstanceID);
#else
		criWareInitializerList = FindObjectsOfType<CriWareInitializer>();
#endif
		if (criWareInitializerList.Length > 0) {
			Debug.LogError("\"CriWareLibraryInitializer\" already exists.");

			Selection.activeGameObject = criWareInitializerList[0].gameObject;

		} else {
			GameObject go = null;
			go = new GameObject("CriWareLibraryInitializer");

			go.AddComponent<CriWareInitializer>();

			Selection.activeGameObject = go;
		}
	}

	[MenuItem("GameObject/CRIWARE/Create CRIWARE Error Handler", false, 150)]
	public static void CreateCriwareErrorHandler()
	{
		CriWareErrorHandler[] criWareErrorHandlerList;
#if UNITY_2023_1_OR_NEWER
		criWareErrorHandlerList = FindObjectsByType<CriWareErrorHandler>(FindObjectsSortMode.InstanceID);
#else
		criWareErrorHandlerList = FindObjectsOfType<CriWareErrorHandler>();
#endif
		if (criWareErrorHandlerList.Length > 0) {
			Debug.LogError("\"CriWareErrorHandler\" already exists.");

			Selection.activeGameObject = criWareErrorHandlerList[0].gameObject;

		} else {
			GameObject go = null;
			go = new GameObject("CriWareErrorHandler");

			go.AddComponent<CriWareErrorHandler>();

			Selection.activeGameObject = go;
		}
	}
#if UNITY_2019_3_OR_NEWER
	// Initialize CriStaticFieldReloader
	private static CriStaticFieldReloader StaticFieldReloader = new CriStaticFieldReloader(typeof(CriWare.Common).Assembly);

	[InitializeOnLoadMethod]
	private static void CacheStaticFieldInitValue()
	{
		StaticFieldReloader.CacheInitialFieldValue();
	}

	[InitializeOnEnterPlayMode]
	private static void ResetStaticField()
	{
		StaticFieldReloader.ResetStaticField();
	}
#endif

	} // end of class

} //namespace CriWare
/* end of file */
