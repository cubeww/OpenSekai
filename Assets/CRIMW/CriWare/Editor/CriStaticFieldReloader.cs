/****************************************************************************
 *
 * Copyright (c) 2023 CRI Middleware Co., Ltd.
 *
 ****************************************************************************/

#if UNITY_2019_3_OR_NEWER
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using CriWare.Editor;

namespace CriWare {
	internal class CriStaticFieldReloader
	{
		private Dictionary<FieldInfo, object> initialFieldValue;
		private bool isEnabled = false;
		private List<Type> reloadTargetTypes = new List<Type>();

		public CriStaticFieldReloader(Assembly assembly)
		{
			foreach (Type type in assembly.GetTypes())
			{
				reloadTargetTypes.Add(type);
			}
		}

		public void CacheInitialFieldValue()
		{
			if (EditorSettings.enterPlayModeOptionsEnabled && EditorSettings.enterPlayModeOptions == EnterPlayModeOptions.DisableDomainReload)
			{
				if(CriCommonEditorSettings.Instance.EnableStaticFieldReload)
				{
					isEnabled = true;
				}
			}

			if (isEnabled)
			{
				initialFieldValue = RegisterInitialFieldDict();
			}
		}

		public void ResetStaticField()
		{
			if (EditorSettings.enterPlayModeOptionsEnabled)
			{
				if(!CriCommonEditorSettings.Instance.EnableStaticFieldReload)
				{
					Debug.LogWarning("[CRIWARE] CriStaticFieldReloader is not enabled, static fields will not be reloaded when re-entering playmode.");
				}
			}

			if (isEnabled)
			{
				CompareAndResetStaticFieldValue(initialFieldValue);
			}
		}

		private List<FieldInfo> GetReloadTargetFieldInfos()
		{
			List<FieldInfo> result = new List<FieldInfo>();

			foreach (Type type in reloadTargetTypes)
			{
				List<FieldInfo> fields = type.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
				.Where(field => !field.IsLiteral && !field.IsInitOnly) // IsLiteral = const; IsInitOnly = readOnly;
				.ToList();

				result.AddRange(fields);
			}

			return result;
		}

		private Dictionary<FieldInfo, object> RegisterInitialFieldDict()
		{
			Dictionary<FieldInfo, object> result = new Dictionary<FieldInfo, object>() { };
			List<FieldInfo> asmDefault = GetReloadTargetFieldInfos();
			foreach (FieldInfo field in asmDefault)
			{
				result.Add(field, field.GetValue(null));
			}
			return result;
		}

		private void CompareAndResetStaticFieldValue(Dictionary<FieldInfo, object> DefaultFieldInfo)
		{
			List<FieldInfo> asmCurrent = GetReloadTargetFieldInfos();
			foreach (FieldInfo field in asmCurrent)
			{
				if (!DefaultFieldInfo.ContainsKey(field))
				{
					Debug.LogWarning("[CRIWARE][Internal] CriStaticFieldReloader tried to reload uncached field: " + field.DeclaringType.ToString() + field.Name);
					continue;
				}

				object defaultValue = DefaultFieldInfo[field];
				if(field.GetValue(null) != defaultValue)
				{
					field.SetValue(null, defaultValue);
				}
			}
		}

	} // End of class CriStaticFieldReloader

} // namespace CriWare
#endif