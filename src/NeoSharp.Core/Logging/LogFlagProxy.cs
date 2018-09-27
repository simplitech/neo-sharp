using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace NeoSharp.Core.Logging
{
    public class LogFlagProxy
    {

        //TODO Avoid static usage. How this can be improved?
        public static readonly Dictionary<LogLevel, LogVerbose> Instance = new Dictionary<LogLevel, LogVerbose>()
        {
            { LogLevel.Trace, LogVerbose.Trace},
            { LogLevel.Debug, LogVerbose.Debug},
            { LogLevel.Information, LogVerbose.Information},
            { LogLevel.Warning, LogVerbose.Warning},
            { LogLevel.Error, LogVerbose.Error},
            { LogLevel.Critical, LogVerbose.Critical},
        };

    }
}
