using System;

namespace Beebyte.Obfuscator
{
    // TODO(original): replace with the real Beebyte Obfuscator plugin if it is recovered.
    // IDA shows SkipAttribute only chains to System.Attribute..ctor in this build.
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    public sealed class SkipAttribute : Attribute
    {
        public SkipAttribute()
        {
        }
    }
}
