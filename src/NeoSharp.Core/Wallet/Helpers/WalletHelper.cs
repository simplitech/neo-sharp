﻿using System;
using System.Linq;
using System.Security;
using System.Text;
using NeoSharp.Core.Cryptography;
using NeoSharp.Core.Models;
using NeoSharp.Core.Types;

namespace NeoSharp.Core.Wallet.Helpers
{
    public class WalletHelper
    {
        ICrypto _crypto;
        ContractHelper _contractHelper;

        public WalletHelper(ICrypto cripto)
        {
            _crypto = cripto;
            _contractHelper = new ContractHelper();
        }

        /// <summary>
        /// Decrypts a NEP-2 into a private key.
        /// https://github.com/neo-project/proposals/blob/master/nep-2.mediawiki#decryption-steps
        /// //Decryption steps
        /// 1 - Collect encrypted private key and passphrase from user.
        /// 2 - Derive derivedhalf1 and derivedhalf2 by passing the passphrase and addresshash into scrypt function.
        /// 3 - Decrypt encryptedhalf1 and encryptedhalf2 using AES256Decrypt, merge the two parts and XOR the result with derivedhalf1 to form the plaintext private key.
        /// 4 - Convert that plaintext private key into a NEO address.
        /// 5 - Hash the NEO address, and verify that addresshash from the encrypted private key record matches the hash.If not, report that the passphrase entry was incorrect.
        /// </summary>
        /// <returns>The wif.</returns>
        /// <param name="encryptedPrivateKey">Nep2.</param>
        /// <param name="passphrase">Passphrase.</param>
        public byte[] DecryptWif(string encryptedPrivateKey, SecureString passphrase)
        {
            if (encryptedPrivateKey == null || String.IsNullOrWhiteSpace(encryptedPrivateKey))
            {
                throw new ArgumentNullException(nameof(encryptedPrivateKey));
            }

            if (passphrase == null ||  String.IsNullOrWhiteSpace(passphrase.ToString()))
            {
                throw new ArgumentNullException(nameof(passphrase));
            }

            // Data is encoded in Base58
            // https://github.com/neo-project/proposals/blob/master/nep-2.mediawiki#abstract
            byte[] data = _crypto.Base58CheckDecode(encryptedPrivateKey);

            //2 bytes 0x0142 Object Identifier Prefix (see below)
            //1 byte(flagbyte): always be 0xE0
            //4 bytes: SHA256(SHA256(expected_neo_address))[0...3], used both for typo checking and as salt
            //16 bytes: An AES - encrypted key material record(encryptedhalf1)
            //16 bytes: An AES - encrypted key material record(encryptedhalf2)
            if (data.Length != 39)
            {
                throw new FormatException();
            }

            // Object identifier prefix: 0x0142.
            // These are constant bytes that appear at the beginning of the Base58Check - encoded record, 
            // and their presence causes the resulting string to have a predictable prefix.
            // How the user sees it: 58 characters always starting with '6P'
            // https://github.com/neo-project/proposals/blob/master/nep-2.mediawiki#proposed-specification
            if (data[0] != 0x01 || data[1] != 0x42)
            {
                throw new FormatException();
            }
            // Payload flagbyte, always 0xE0
            if (data[2] != 0xe0)
            {
                throw new FormatException();
            }


            //4 bytes: SHA256(SHA256(expected_neo_address))[0...3], used both for typo checking and as salt
            var addressHash = new byte[4];
            Buffer.BlockCopy(data, 3, addressHash, 0, 4);

            //Passphrase encoded in UTF - 8 and normalized using Unicode Normalization Form C(NFC). 
            //Check with Belane
            var passphraseUtf8String = Helper.ToArray(passphrase);


            //Derive derivedhalf1 and derivedhalf2 by passing the passphrase and addresshash into scrypt function.
            var derivedKey = _crypto.SCrypt(passphraseUtf8String, addressHash, ScryptParameters.Default.N, ScryptParameters.Default.R, ScryptParameters.Default.P, 64);

            //Decrypt encryptedhalf1 and encryptedhalf2 using AES256Decrypt, 
            var derivedhalf1 = derivedKey.Take(32).ToArray();
            var derivedhalf2 = derivedKey.Skip(32).ToArray();

            var encryptedkey = new byte[32];
            Buffer.BlockCopy(data, 7, encryptedkey, 0, 32);

            //merge the two parts and XOR the result with derivedhalf1 to form the plaintext private key.
            var privateKey = XOR(_crypto.AesDecrypt(encryptedkey, derivedhalf2), derivedhalf1);

            //Integrity check. Its necessary to rebuild the contract to get the address
            String address = privateKeyToAddress(privateKey);

            byte[] addressBytes = Encoding.ASCII.GetBytes(address);
            var check = _crypto.Sha256(_crypto.Sha256(addressBytes)).Take(4);

            if (!check.SequenceEqual(addressHash))
            {
                throw new FormatException();
            }

            return privateKey;
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
        /// <param name="privateKey">Private key.</param>
        /// TODO: Complete with ICrypto
        public String EncryptWif(byte[] privateKey, SecureString passphrase)
        {

            //1 - Compute the NEO address(ASCII), and take the first four bytes of SHA256(SHA256()) of it. 
            // Let's call this "addresshash".
            String address = privateKeyToAddress(privateKey);
            byte[] addressBytes = Encoding.ASCII.GetBytes(address);
            var addressHash = _crypto.Sha256(_crypto.Sha256(addressBytes)).Take(4).ToArray();

            //Passphrase encoded in UTF - 8 and normalized using Unicode Normalization Form C(NFC). 
            //TODO: Check if its using UTF-8 (doesn't look like its using any kind of encoding)
            var passphraseUtf8String = Helper.ToArray(passphrase);

            /// 2 - Derive a key from the passphrase using scrypt
            ///     Parameters: passphrase is the passphrase itself encoded in UTF-8 and normalized using Unicode Normalization Form C(NFC). 
            ///     Salt is the addresshash from the earlier step, n = 16384, r = 8, p = 8, length = 64
            ///     Let's split the resulting 64 bytes in half, and call them derivedhalf1 and derivedhalf2.
            byte[] derivedkey = _crypto.SCrypt(passphraseUtf8String, addressHash, ScryptParameters.Default.N, ScryptParameters.Default.R, ScryptParameters.Default.P, 64);
            byte[] derivedhalf1 = derivedkey.Take(32).ToArray();
            byte[] derivedhalf2 = derivedkey.Skip(32).ToArray();


            /// 3 - Do AES256Encrypt(block = privkey[0...15] xor derivedhalf1[0...15], key = derivedhalf2), call the 16-byte result encryptedhalf1
            byte[] encryptedKey = _crypto.AesEncrypt(XOR(privateKey, derivedhalf1), derivedhalf2);

            /// The encrypted private key is the Base58Check-encoded concatenation of the following, which totals 39 bytes without Base58 checksum:
            /// 0x01 0x42 + flagbyte + addresshash + encryptedhalf1 + encryptedhalf2
            byte[] buffer = new byte[39];
            buffer[0] = 0x01;
            buffer[1] = 0x42;
            buffer[2] = 0xe0;

            //Object prefix
            Buffer.BlockCopy(addressHash, 0, buffer, 3, addressHash.Length);
            //Encrypted wif
            Buffer.BlockCopy(encryptedKey, 0, buffer, 7, encryptedKey.Length);

            //TODO: Check if correct & Add unit tests
            return _crypto.Base58CheckEncode(buffer);
        }


        /// <summary>
        /// Auxiliary method to convert ScriptHash to Address
        /// </summary>
        /// <returns>The hash to address.</returns>
        /// <param name="scriptHash">Script hash.</param>
        public String ScriptHashToAddress(UInt160 scriptHash)
        {
            //TODO Add documentation. 
            byte[] data = new byte[21];
            //TODO: Improve this
            data[0] = byte.Parse("23");
            Buffer.BlockCopy(scriptHash.ToArray(), 0, data, 1, 20);
            return _crypto.Base58CheckEncode(data);
        }

        /// <summary>
        /// Script hash from public key.
        /// </summary>
        /// <returns>The script hash.</returns>
        /// <param name="publicKey">Public key.</param>
        public UInt160 ScriptHashFromPublicKey(ECPoint publicKey)
        {
            return _contractHelper.CreateSinglePublicKeyRedeemContract(publicKey).ScriptHash;
        }


        #region Private Methods

        /// <summary>
        /// Privates the key to address.
        /// </summary>
        /// <returns>The key to address.</returns>
        /// <param name="privateKey">Private key.</param>
        private String privateKeyToAddress(byte[] privateKey)
        {
            var pubKeyInBytes = _crypto.ComputePublicKey(privateKey, true);
            ECPoint pubkey = new ECPoint(pubKeyInBytes);
            Contract accountContract = _contractHelper.CreateSinglePublicKeyRedeemContract(pubkey);
            return ScriptHashToAddress(accountContract.ScriptHash);
        }


        //TODO: Double check if this is the best way to do this
        private byte[] XOR(byte[] x, byte[] y)
        {
            if (x.Length != y.Length)
            {
                throw new ArgumentException();
            }

            return x.Zip(y, (a, b) => (byte)(a ^ b)).ToArray();
        }


        #endregion

    }


}
