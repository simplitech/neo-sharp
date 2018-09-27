using System;
namespace NeoSharp.Core.Logging
{
    [Flags]
    public enum LogVerbose : byte
    {
        Off = 0,

        Trace = 1,
        Debug = 2,
        Information = 4,
        Warning = 8,
        Error = 16,
        Critical = 32,

        All = Trace | Debug | Information | Warning | Error | Critical
    }
}
