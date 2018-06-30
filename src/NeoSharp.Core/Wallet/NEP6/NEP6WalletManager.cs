using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NeoSharp.Core.Cryptography;
using NeoSharp.Core.Extensions;
using NeoSharp.Core.Models;
using NeoSharp.Core.Types;
using NeoSharp.Core.Wallet.Helpers;
using NeoSharp.VM;
using Newtonsoft.Json;

namespace NeoSharp.Core.Wallet.NEP6
{
    public class Nep6WalletManager : IWalletManager
    {

        WalletHelper _walletHelper;
        ContractHelper _contractHelper;
        ICrypto _crypto;

        public IWallet Wallet { get; private set; }
        // nep2, publicKey, privateKey
        public IDictionary<String, Tuple<ECPoint, byte[]>> _unlockedAccounts = new Dictionary<String, Tuple<ECPoint, byte[]>>();

        public Nep6WalletManager(ICrypto crypto)
        {
            _crypto = crypto;
            _walletHelper = new WalletHelper(_crypto);
            _contractHelper = new ContractHelper();
        }

        /// <summary>
        /// Creates the wallet.
        /// </summary>
        /// <returns>The wallet.</returns>
        /// <param name="fileInfo">File info.</param>
        public void CreateWallet(FileInfo fileInfo)
        {
            if (fileInfo.Exists)
            {
                throw new ArgumentException("File already exists");
            }

            String walletName = fileInfo.Name.Split('.')[0];
            IWallet wallet = new NEP6Wallet()
            {
                Name = walletName,
                Version = "1.0"
            };

            String json = JsonConvert.SerializeObject(wallet);

            File.WriteAllText(fileInfo.FullName, json);

            Wallet = wallet;
        }

        /// <summary>
        /// Check if Accounts contains a script hash
        /// </summary>
        /// <returns></returns>
        /// <param name="scriptHash"></param>
        public bool Contains(UInt160 scriptHash)
        {
            if (scriptHash == null)
            {
                throw new ArgumentNullException();
            }

            return Wallet.Accounts
               .Where(x => x.Contract.ScriptHash.Equals(scriptHash))
               .FirstOrDefault() != null;

        }

        /// <summary>
        /// Creates the account with random private key.
        /// </summary>
        /// <returns>The account.</returns>
        public IWalletAccount CreateAccount()
        {

            byte[] privateKey = _crypto.GenerateRandomBytes(32);
            IWalletAccount account = Import(privateKey);
            Array.Clear(privateKey, 0, privateKey.Length);
            return account;
        }

        /// <summary>
        /// Remove the account.
        /// </summary>
        /// <param name="scriptHash">Scripthash's account.</param>
        public void DeleteAccount(UInt160 scriptHash)
        {
            if (scriptHash == null)
            {
                throw new ArgumentNullException();
            }
            Wallet.Accounts = Wallet.Accounts.Where(x => !x.Contract.ScriptHash.Equals(scriptHash));
        }

        /// <summary>
        /// Gets the account.
        /// </summary>
        /// <returns>The account.</returns>
        /// <param name="scriptHash">Scripthash's account.</param>
        public IWalletAccount GetAccount(UInt160 scriptHash)
        {
            if (scriptHash == null)
            {
                throw new ArgumentNullException();
            }

            return Wallet.Accounts
                .Where(x => x.Contract.ScriptHash.Equals(scriptHash))
                .FirstOrDefault();
        }

        /// <summary>
        /// Gets the account using public key.
        /// </summary>
        /// <returns>The account.</returns>
        /// <param name="pubkey">Pubkey.</param>
        public IWalletAccount GetAccount(ECPoint pubkey)
        {
            if (pubkey == null)
            {
                throw new ArgumentNullException();
            }

            var scriptHash = _walletHelper.ScriptHashFromPublicKey(pubkey);

            return GetAccount(scriptHash);
        }

        /// <summary>
        /// Instantiates the account and adds to the Accounts
        /// </summary>
        /// <returns>The account.</returns>
        /// <param name="scriptHash">Script hash.</param>
        public IWalletAccount Import(UInt160 scriptHash)
        {
            if (scriptHash == null)
            {
                throw new ArgumentNullException();
            }

            if (scriptHash.Equals(UInt160.Zero))
            {
                throw new ArgumentException();
            }

            //TODO: Load Contract from persistence?
            Code emptyContractCode = new Code
            {
                ScriptHash = scriptHash
            };
            Contract emptyContract = new Contract
            {
                Code = emptyContractCode
            };

            NEP6Account account = new NEP6Account
            {
                Contract = emptyContract
            };

            AddAccount(account);
            return account;
        }

        /// <summary>
        /// Import the account using private key
        /// </summary>
        /// <returns>The account.</returns>
        /// <param name="privateKey">Private key.</param>
        public IWalletAccount Import(byte[] privateKey)
        {
            if (privateKey == null)
            {
                throw new ArgumentNullException();
            }

            if (privateKey.Length == 0)
            {
                throw new ArgumentException();
            }

            NEP6Account account = CreateAccountWithPrivateKey(privateKey);
            AddAccount(account);
            return account;
        }

        /// <summary>
        /// Import the Account using wif.
        /// </summary>
        /// <returns>The import.</returns>
        /// <param name="wif">Wif.</param>
        public IWalletAccount Import(string wif)
        {
            if (String.IsNullOrWhiteSpace(wif))
            {
                throw new ArgumentNullException();
            }

            NEP6Account account = CreateAccountWithPrivateKey(GetPrivateKeyFromWIF(wif));
            AddAccount(account);
            return account;
        }

        /// <summary>
        /// Import the Account using nep2 and passphrase.
        /// </summary>
        /// <returns>The import.</returns>
        /// <param name="nep2">Nep2.</param>
        /// <param name="passphrase">Passphrase.</param>
        public IWalletAccount Import(string nep2, string passphrase)
        {
            byte[] privateKey = _walletHelper.DecryptWif(nep2, passphrase);
            NEP6Account account = CreateAccountWithPrivateKey(privateKey);
            account.Key = nep2;
            AddAccount(account);
            return account;
        }

        /// <summary>
        /// Verifies the password.
        /// </summary>
        /// <returns><c>true</c>, if password was verifyed, <c>false</c> otherwise.</returns>
        /// <param name="walletAccout">Wallet accout.</param>
        /// <param name="password">Password.</param>
        public bool VerifyPassword(IWalletAccount walletAccout, string password)
        {
            if (walletAccout == null)
            {
                throw new ArgumentException();
            }

            try
            {
                _walletHelper.DecryptWif(walletAccout.Key, password);
            }

            catch (FormatException)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Gets the private key from wif.
        /// </summary>
        /// <returns>The private key from wif.</returns>
        /// <param name="wif">Wif.</param>
        private byte[] GetPrivateKeyFromWIF(string wif)
        {
            if (wif == null)
            {
                throw new ArgumentNullException();
            }

            byte[] data = _crypto.Base58CheckDecode(wif);

            if (data.Length != 34 || data[0] != 0x80 || data[33] != 0x01)
            {
                throw new FormatException();
            }

            byte[] privateKey = new byte[32];
            Buffer.BlockCopy(data, 1, privateKey, 0, privateKey.Length);
            Array.Clear(data, 0, data.Length);
            return privateKey;
        }


        public void SaveWallet()
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Adds the or replace an account in the system.
        /// If the account with that scriptHash already exists, 
        /// it's going to be replaced
        /// </summary>
        /// <param name="account">Account.</param>
        private void AddOrReplaceAccount(IWalletAccount account)
        {
            IWalletAccount currentAccount = TryGetAccount(account.Contract.ScriptHash);
            if (currentAccount == null)
            {
                AddAccount(account);
            }
            else
            {
                //Account exists. Clone it.
                IWalletAccount clonedAccount = CloneAndUpdateAccount(account);
                RemoveAccount(clonedAccount);
                AddAccount(clonedAccount);
            }

            SaveWallet();
        }


        private void RemoveAccount(IWalletAccount account)
        {
            var lstAccounts = Wallet.Accounts.ToList();
            lstAccounts.Remove(account);
            Wallet.Accounts = lstAccounts;
        }

        /// <summary>
        /// Retrieves the account from the Account list using the script hash/
        /// Replaces the account information with those provided in the newAccountInformation parameter
        /// Returns the account but does not save it
        /// This should be used when the user wants to update a current account,
        /// like replacing the paraters from the contract or replacing the label.
        /// </summary>
        /// <returns>The account.</returns>
        /// <param name="newAccountInformation">Account script hash.</param>
        private IWalletAccount CloneAndUpdateAccount(IWalletAccount newAccountInformation)
        {
            if (newAccountInformation.Contract.ScriptHash == null)
            {
                throw new ArgumentException("Invalid ScriptHash");
            }

            var currentAccountWithScriptHash = TryGetAccount(newAccountInformation.Contract.ScriptHash);

            if (currentAccountWithScriptHash == null)
            {
                throw new ArgumentException("Account not found.");
            }

            var clonedAccount = new NEP6Account();
            clonedAccount.Label = newAccountInformation.Label;
            clonedAccount.IsDefault = newAccountInformation.IsDefault;
            clonedAccount.Lock = newAccountInformation.Lock;
            clonedAccount.Contract = newAccountInformation.Contract;

            return clonedAccount;
        }

        /// <summary>
        /// Adds the account to the Account list.
        /// </summary>
        /// <param name="account">Account.</param>
        private void AddAccount(IWalletAccount account)
        {
            if (account.Contract.ScriptHash == null)
            {
                throw new ArgumentException("Invalid Script Hash");
            }

            //TODO: Is there any other kind of validation? May the label be null?
            //Check with the guys from coz.

            var currentAccountWithScriptHash = TryGetAccount(account.Contract.ScriptHash);

            if (currentAccountWithScriptHash == null)
            {
                var lstAccounts = Wallet.Accounts.ToList();
                lstAccounts.Add(account);
                Wallet.Accounts = lstAccounts;
            }

        }

        /// <summary>
        /// Creates the NEP6Account with private key.
        /// </summary>
        /// <returns>The NEP6Account</returns>
        /// <param name="privateKey">Private key.</param>
        private NEP6Account CreateAccountWithPrivateKey(byte[] privateKey, String label = null)
        {

            var publicKeyInBytes = _crypto.ComputePublicKey(privateKey, true);
            var publicKeyInEcPoint = new ECPoint(publicKeyInBytes);
            Contract contract = _contractHelper.CreateSinglePublicKeyRedeemContract(publicKeyInEcPoint);

            NEP6Account account = new NEP6Account()
            {
                Contract = contract,
                Label = label
            };

            account.Key = GetNep2FromPublicKeyAndPrivateKey(publicKeyInEcPoint, privateKey);
            UnlockAccount(account.Key, publicKeyInEcPoint, privateKey);
            return account;
        }

        /// <summary>
        /// Tries the get account from the Account list
        /// </summary>
        /// <returns>The account found with that ScriptHash</returns>
        /// <param name="accountScriptHash">Account ScriptHash.</param>
        private IWalletAccount TryGetAccount(UInt160 accountScriptHash)
        {
            return Wallet?.Accounts
                          .Where(x => x.Contract.ScriptHash.Equals(accountScriptHash))
                          .FirstOrDefault();
        }

        /// <summary>
        /// Load a wallet using a fileInfo.
        /// </summary>
        /// <param name="fileInfo">File info.</param>
        public void Load(FileInfo fileInfo)
        {
            if (!fileInfo.Exists)
            {
                throw new ArgumentException("File not found");
            }

            String json = File.ReadAllText(fileInfo.FullName);

            Wallet = JsonConvert.DeserializeObject<NEP6Wallet>(json);
        }

        /// <summary>
        /// Unlocks all accounts of the loaded wallet with a password
        /// </summary>
        /// <param name="password">Password.</param>
        public void UnlockAllAccounts(String password)
        {
            Wallet?.Accounts.ForEach(account => UnlockAccount(account.Key, password));
        }

        /// <summary>
        /// Close wallet.
        /// </summary>
        public void Close()
        {
            Wallet = null;
            _unlockedAccounts.Clear();
        }

        /// <summary>
        /// Unlocks an account of the specified nep2key.
        /// </summary>
        /// <param name="nep2Key">Nep2 key.</param>
        /// <param name="password">Password.</param>
        public void UnlockAccount(String nep2Key, String password)
        {
            var privateKey = _walletHelper.DecryptWif(nep2Key, password);
            var publicKeyInBytes = _crypto.ComputePublicKey(privateKey, true);
            var publicKeyEcPoint = new ECPoint(publicKeyInBytes);
            UnlockAccount(nep2Key, publicKeyEcPoint, privateKey);
        }

        private void UnlockAccount(String nep2Key, ECPoint publicKey, byte[] privateKey)
        {
            if (nep2Key == null)
            {
                throw new ArgumentException("Invalid NEP-2 Key");
            }

            var entry = new Tuple<ECPoint, byte[]>(publicKey, privateKey);

            if (_unlockedAccounts.ContainsKey(nep2Key))
            {
                _unlockedAccounts[nep2Key] = entry;
            }
            else
            {
                _unlockedAccounts.Add(nep2Key, entry);
            }
        }


        /// <summary>
        /// Creates a protected private key following NEP-2
        /// https://github.com/neo-project/proposals/blob/master/nep-2.mediawiki#encryption-steps
        /// 1 - Compute the NEO address (ASCII), and take the first four bytes of SHA256(SHA256()) of it. Let's call this "addresshash".
        /// 2 - Derive a key from the passphrase using scrypt
        ///     Parameters: passphrase is the passphrase itself encoded in UTF-8 and normalized using Unicode Normalization Form C(NFC). Salt is the addresshash from the earlier step, n = 16384, r = 8, p = 8, length = 64
        ///     Let's split the resulting 64 bytes in half, and call them derivedhalf1 and derivedhalf2.
        /// 3 - Do AES256Encrypt(block = privkey[0...15] xor derivedhalf1[0...15], key = derivedhalf2), call the 16-byte result encryptedhalf1
        /// 4 - Do AES256Encrypt(block = privkey[16...31] xor derivedhalf1[16...31], key = derivedhalf2), call the 16-byte result encryptedhalf2
        /// The encrypted private key is the Base58Check-encoded concatenation of the following, which totals 39 bytes without Base58 checksum:
        /// 0x01 0x42 + flagbyte + addresshash + encryptedhalf1 + encryptedhalf2
        /// </summary>
        /// <returns>The nep2 from public key and private key.</returns>
        /// <param name="publicKey">Public key.</param>
        /// <param name="privateKey">Private key.</param>
        /// TODO: Complete with ICrypto
        private String GetNep2FromPublicKeyAndPrivateKey(ECPoint publicKey, byte[] privateKey)
        {
            //Placeholder
            var contract = _contractHelper.CreateSinglePublicKeyRedeemContract(publicKey);
            return contract.ScriptHash.ToString();
            //string address = _crypto.ToAddress(contract.ScriptHash);
            //byte[] addresshash = Encoding.ASCII.GetBytes(address).Sha256().Sha256().Take(4).ToArray();
            //byte[] derivedkey = _crypto.DeriveKey(Encoding.UTF8.GetBytes(passphrase), addresshash, N, r, p, 64);
            //byte[] derivedhalf1 = derivedkey.Take(32).ToArray();
            //byte[] derivedhalf2 = derivedkey.Skip(32).ToArray();
            //byte[] encryptedkey = XOR(PrivateKey, derivedhalf1).AES256Encrypt(derivedhalf2);
            //byte[] buffer = new byte[39];
            //buffer[0] = 0x01;
            //buffer[1] = 0x42;
            //buffer[2] = 0xe0;
            //Buffer.BlockCopy(addresshash, 0, buffer, 3, addresshash.Length);
            //Buffer.BlockCopy(encryptedkey, 0, buffer, 7, encryptedkey.Length);
        }

    }
}