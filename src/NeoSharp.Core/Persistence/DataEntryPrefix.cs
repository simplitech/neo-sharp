﻿namespace NeoSharp.Core.Persistence
{
    public enum DataEntryPrefix : byte
    {
        DataBlock = 0x01,
        DataTransaction = 0x02,
        StAccount = 0x40,
        StCoin = 0x44,
        StSpentCoin = 0x45,
        StValidator = 0x48,
        StAsset = 0x4c,
        StContract = 0x50,
        StStorage = 0x70,
        IxHeaderHashList = 0x80,
        IxValidatorsCount = 0x90,
        SysCurrentBlock = 0xc0,
        SysCurrentHeader = 0xc1,
        SysVersion = 0xf0
    }
}