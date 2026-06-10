/****************************************************************************
 *
 * Copyright (c) CRI Middleware Co., Ltd.
 *
 ****************************************************************************/

#if CRIWARE_PROFILING_CORE_1_OR_NEWER
using CriWare;
using System;
using Unity.Profiling;
using Unity.Profiling.Editor;

namespace CriWare.Editor {
    [Serializable]
    [ProfilerModuleMetadata("CRIWARE Memory Usage")]
    internal class CriWareProfilerModule : ProfilerModule
    {
        static string[] targetReporterNames = Enum.GetNames(typeof(CriWareProfilerReporter.TargetReporterName));

        static ProfilerCounterDescriptor[] constructProfilerBase()
        {
            ProfilerCounterDescriptor[] profileBase = new ProfilerCounterDescriptor[targetReporterNames.Length];
            for (int i = 0; i < targetReporterNames.Length; i++)
            {
                profileBase[i] = new ProfilerCounterDescriptor(targetReporterNames[i], ProfilerCategory.Memory);
            }

            return profileBase;
        }

        public CriWareProfilerModule() : base(constructProfilerBase()) { }
    }
} //namespace CriWare.Editor
#endif