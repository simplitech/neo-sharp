using System;
using System.Linq;
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

        public WalletHelper(ICrypto cripto){
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
        public byte[] DecryptWif(string encryptedPrivateKey, string passphrase)
        {
            if (String.IsNullOrWhiteSpace(encryptedPrivateKey))
            {
                throw new ArgumentNullException(nameof(encryptedPrivateKey));
            }

            if (String.IsNullOrWhiteSpace(passphrase))
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
            if(data.Length != 39){
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
            if(data[2] != 0xe0)
            {
                throw new FormatException();
            }


            //4 bytes: SHA256(SHA256(expected_neo_address))[0...3], used both for typo checking and as salt
            var addressHash = new byte[4];
            Buffer.BlockCopy(data, 3, addressHash, 0, 4);

            //Passphrase encoded in UTF - 8 and normalized using Unicode Normalization Form C(NFC). 
            var passphraseUtf8String = Encoding.UTF8.GetBytes(passphrase);


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



        public String ScriptHashToAddress(UInt160 scriptHash)
        {
            //TODO Add documentation. 
            byte[] data = new byte[21];
            //TODO: Improve this
            data[0] = byte.Parse("23");
            Buffer.BlockCopy(scriptHash.ToArray(), 0, data, 1, 20);
            return _crypto.Base58CheckEncode(data);
        }

        private String privateKeyToAddress(byte[] privateKey){
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

        /// <summary>
        /// Script hash from public key.
        /// </summary>
        /// <returns>The script hash.</returns>
        /// <param name="publicKey">Public key.</param>
        public UInt160 ScriptHashFromPublicKey(ECPoint publicKey)
        {
            return _contractHelper.CreateSinglePublicKeyRedeemContract(publicKey).ScriptHash;
        }
    }


}
