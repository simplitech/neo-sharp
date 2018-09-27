using System;
using System.Numerics;
using NeoSharp.Core.Models;
using NeoSharp.Core.Types;

namespace NeoSharp.Core.SmartContract.NEP5
{
    public interface INEP5Token
    {
        /// <summary>
        /// NEP-5 Smart Contract
        /// </summary>
        /// <value>The token contract.</value>
        Contract TokenContract { get; }
        /// <summary>
        /// Token Symbol
        /// </summary>
        /// <returns>Token symbol.</returns>
        string Symbol();
        /// <summary>
        /// Token Name
        /// </summary>
        /// <returns>Token name.</returns>
        string Name();
        /// <summary>
        /// Total Supply
        /// </summary>
        /// <returns>Token total supply</returns>
        BigInteger TotalSupply();
        /// <summary>
        /// Decimals
        /// </summary>
        /// <returns>Decimals used by the token</returns>
        byte Decimals();
        /// <summary>
        /// Retrieves the balance of the desired account
        /// </summary>
        /// <returns>The of.</returns>
        /// <param name="addressScriptHash">Address scripthash.</param>
        BigInteger BalanceOf(UInt160 addressScriptHash);
    }
}
