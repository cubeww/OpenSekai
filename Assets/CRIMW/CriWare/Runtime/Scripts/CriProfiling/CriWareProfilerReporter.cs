/****************************************************************************
 *
 * Copyright (c) CRI Middleware Co., Ltd.
 *
 ****************************************************************************/
#if CRIWARE_PROFILING_CORE_1_OR_NEWER && !CRI_DISABLE_PROFILER_EXTENSION
#define CRI_USE_PROFILER_EXTENSION 
#endif

using System;
using System.Collections.Generic;

#if CRI_USE_PROFILER_EXTENSION
using Unity.Profiling;
#endif

namespace CriWare {
    internal struct CriDurationMeasurementScope: IDisposable
    {
#if CRI_USE_PROFILER_EXTENSION
        ProfilerMarker.AutoScope scope;
#endif
        public CriDurationMeasurementScope(string name)
        {
#if CRI_USE_PROFILER_EXTENSION
            scope = new ProfilerMarker(name).Auto();
#endif
        }

        public void Dispose()
        {
#if CRI_USE_PROFILER_EXTENSION
            scope.Dispose();
#endif
        }
    }

#if CRI_USE_PROFILER_EXTENSION
    internal class CriWareProfilerReporter
    {
        public enum TargetReporterName
        {
            Atom,
            Mana,
            Fs,
            Total
        }

        private List<CriWareMemoryReporter> memoryReporters = null;
        private CriWareMemoryReporter totalReporter = null;
        public CriWareProfilerReporter()
        {
            memoryReporters = new List<CriWareMemoryReporter>();

            CriWareMemoryReporter atom = new CriWareMemoryReporter(nameof(TargetReporterName.Atom), () => {
                return CriAtomPlugin.isInitialized ? CriWare.Common.GetAtomMemoryUsage() : 0;
            } );
            memoryReporters.Add(atom);

            CriWareMemoryReporter mana = new CriWareMemoryReporter(nameof(TargetReporterName.Mana), () => {
                return CriManaPlugin.isInitialized ? CriWare.Common.GetManaMemoryUsage() : 0;
            });
            memoryReporters.Add(mana);

            CriWareMemoryReporter fs = new CriWareMemoryReporter(nameof(TargetReporterName.Fs), () => {
                return CriFsPlugin.isInitialized ? CriWare.Common.GetFsMemoryUsage() : 0;
            });
            memoryReporters.Add(fs);

            totalReporter = new CriWareMemoryReporter(nameof(TargetReporterName.Total), null);
        }

        public void UpdateMeasure()
        {
            uint total = 0;
            for (int i = 0; i < memoryReporters.Count; i++)
            {
               total += memoryReporters[i].UpdateValue();
            }

            totalReporter.UpdateValue(total);
        }
    }

    internal class CriWareMemoryReporter
    {
        private ProfilerCounterValue<uint> profilerCounter = default;
        private Func<uint> updateFunc = null;
        public CriWareMemoryReporter(string profilerLabel, Func<uint> profileUpdateFunc)
        {
            profilerCounter = new ProfilerCounterValue<uint>(ProfilerCategory.Memory, profilerLabel, ProfilerMarkerDataUnit.Bytes, ProfilerCounterOptions.FlushOnEndOfFrame);
            updateFunc = profileUpdateFunc;
        }

        public uint UpdateValue()
        {
            if (updateFunc == null) { 
                return 0; 
            }

            profilerCounter.Value = updateFunc();
            return profilerCounter.Value;
        }

        public void UpdateValue(uint value) 
        {
            profilerCounter.Value = value;
        }
    }
#endif
}  //namespace CriWare