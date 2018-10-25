using System;
using NeoSharp.Core.Cryptography;
using NeoSharp.Core.Models;
using NeoSharp.Core.Types;
using NeoSharp.Types;

namespace NeoSharp.Core.SmartContract
{
    public interface IContractFactory
    {
        Contract LoadSmartContract(UInt160 contractScriptHash);
        Contract CreateSinglePublicKeyRedeemContract(ECPoint publicKey);
        Contract CreateMultiplePublicKeyRedeemContract(int numberOfRequiredPublicKeys, ECPoint[] publicKeys);
    }
}
