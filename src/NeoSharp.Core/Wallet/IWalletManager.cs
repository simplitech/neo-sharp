﻿using System;
using System.IO;
using System.Security;
using NeoSharp.Core.Cryptography;
using NeoSharp.Core.Types;

namespace NeoSharp.Core.Wallet
{
    public interface IWalletManager
    {
        /// <summary>
        /// the open wallet 
        /// </summary>
        IWallet Wallet { get; }

        /// <summary>
        /// Creates the wallet and saves the instance.
        /// </summary>
        /// <returns>The wallet.</returns>
        /// <param name="fileInfo">File info.</param>
        void CreateWallet(FileInfo fileInfo);

        /// <summary>
        /// Check if Accounts contains a script hash
        /// </summary>
        /// <returns></returns>
        /// <param name="scriptHash"></param>
        bool Contains(UInt160 scriptHash);

        /// <summary>
        /// Creates the account with random private key.
        /// </summary>
        /// <returns>The account.</returns>
        IWalletAccount CreateAccount(SecureString password);

        /// <summary>
        /// Remove the account.
        /// </summary>
        /// <param name="scriptHash">Scripthash's account.</param>
        void DeleteAccount(UInt160 scriptHash);

        /// <summary>
        /// Gets the account.
        /// </summary>
        /// <returns>The account.</returns>
        /// <param name="scriptHash">Scripthash's account.</param>
        IWalletAccount GetAccount(UInt160 scriptHash);

        /// <summary>
        /// Gets the account using public key.
        /// </summary>
        /// <returns>The account.</returns>
        /// <param name="pubkey">Pubkey.</param>
        IWalletAccount GetAccount(ECPoint pubkey);

        /// <summary>
        /// Import the account using script hash
        /// </summary>
        /// <returns>The account.</returns>
        /// <param name="scriptHash">Script hash.</param>
        IWalletAccount ImportScriptHash(UInt160 scriptHash);

        /// <summary>
        /// Import the account using private key
        /// </summary>
        /// <returns>The account.</returns>
        /// <param name="privateKey">Private key.</param>
        IWalletAccount ImportPrivateKey(byte[] privateKey, SecureString password);

        /// <summary>
        /// Import the Account using wif.
        /// </summary>
        /// <returns>The account.</returns>
        /// <param name="wif">Wif.</param>
        IWalletAccount ImportWif(string wif, SecureString password);

        /// <summary>
        /// Load a wallet using a fileInfo.
        /// </summary>
        /// <param name="fileInfo">File info.</param>
        void Load(FileInfo fileInfo);

        /// <summary>
        /// Unlocks all accounts of the loaded wallet with a password
        /// </summary>
        /// <param name="password">Password.</param>
        void UnlockAllAccounts(SecureString password);

        /// <summary>
        /// Unlocks an account of the specified nep2key.
        /// </summary>
        /// <param name="nep2Key">Nep2 key.</param>
        /// <param name="password">Password.</param>
        void UnlockAccount(String nep2Key, SecureString password);

        /// <summary>
        /// Import the Account using nep2 and passphrase.
        /// </summary>
        /// <returns>The account.</returns>
        /// <param name="nep2">Nep2.</param>
        /// <param name="password">Passphrase.</param>
        IWalletAccount ImportEncryptedWif(String nep2, SecureString password);

        /// <summary>
        /// Verifies the password.
        /// </summary>
        /// <returns><c>true</c>, if password was verifyed, <c>false</c>
        /// otherwise.</returns>
        /// <param name="walletAccout">Wallet accout.</param>
        /// <param name="password">Password.</param>
        bool VerifyPassword(IWalletAccount walletAccout, SecureString password);


        /// <summary>
        /// Saves the wallet. It will be save in the file used
        /// to create this wallet.
        /// </summary>
        void SaveWallet();

        /// <summary>
        /// Close wallet.
        /// </summary>
        void Close();
    }
}
