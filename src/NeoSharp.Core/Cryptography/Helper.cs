﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace NeoSharp.Core.Cryptography
{
    public static class Helper
    {
        public static byte[] ToAesKey(ICrypto crypto, string password)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] passwordHash = crypto.Sha256(crypto.Sha256(passwordBytes));
            Array.Clear(passwordBytes, 0, passwordBytes.Length);
            return passwordHash;
        }

        public static byte[] ToAesKey(ICrypto crypto, SecureString password)
        {
            byte[] passwordBytes = password.ToArray();
            byte[] passwordHash = crypto.Sha256(crypto.Sha256(passwordBytes));
            Array.Clear(passwordBytes, 0, passwordBytes.Length);
            return passwordHash;
        }

        //TODO: Ask Belane how can we use this feature in the wallet module
        // I changed it from internal to public. Probably not a good idea.
        public static byte[] ToArray(this SecureString s)
        {
            if (s == null)
                throw new NullReferenceException();
            if (s.Length == 0)
                return new byte[0];
            List<byte> result = new List<byte>();
            IntPtr ptr = SecureStringMarshal.SecureStringToGlobalAllocAnsi(s);
            try
            {
                int i = 0;
                do
                {
                    byte b = Marshal.ReadByte(ptr, i++);
                    if (b == 0)
                        break;
                    result.Add(b);
                } while (true);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocAnsi(ptr);
            }
            return result.ToArray();
        }
    }
}