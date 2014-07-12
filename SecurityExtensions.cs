using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EisnelShared
{
    public static class SecurityExtensions
    {
        private const string DEFAULT_VECTOR = "00000000000000000000000000000000";

        public static string EncryptAes(this string plainText, string keyHex, Encoding encoding)
        {
            return new SimplerAES(keyHex, DEFAULT_VECTOR, encoding).Encrypt(plainText);
        }

        public static string DecryptAes(this string cipherText, string keyHex, Encoding encoding)
        {
            return new SimplerAES(keyHex, DEFAULT_VECTOR, encoding).Decrypt(cipherText);
        }

        /// <summary>
        /// By Philipp Sumi
        /// Licensed under The Code Project Open License (CPOL)
        /// http://www.codeproject.com/Articles/36449/String-Encryption-using-DPAPI-and-Extension-Method
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string EncryptDpApi(this string plainText, Encoding encoding)
        {
            // string to bytes
            encoding = encoding ?? new UTF8Encoding();
            var data = encoding.GetBytes(plainText);

            // encrypt data
            byte[] encrypted = ProtectedData.Protect(data, null, DataProtectionScope.CurrentUser);

            //return as base64 string
            return Convert.ToBase64String(encrypted);
        }

        /// <summary>
        /// By Philipp Sumi
        /// Licensed under The Code Project Open License (CPOL)
        /// http://www.codeproject.com/Articles/36449/String-Encryption-using-DPAPI-and-Extension-Method
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string DecryptDpApi(this string cipherText, Encoding encoding)
        {
            // parse base64 string
            byte[] data = Convert.FromBase64String(cipherText);

            // decrypt data
            byte[] decrypted = ProtectedData.Unprotect(data, null, DataProtectionScope.CurrentUser);

            // bytes to string
            encoding = encoding ?? new UTF8Encoding();
            return encoding.GetString(decrypted);
        }
    }
    
    /// <summary>
    /// From a StackOverflow comment: http://stackoverflow.com/a/5518092
    /// </summary>
    public class SimplerAES
    {
        // key: 32, vector: 16
        // private static byte[] key = { 123, 217, 19, 11, 24, 26, 85, 45, 114, 184, 27, 162, 37, 112, 222, 209, 241, 24, 175, 144, 173, 53, 196, 29, 24, 26, 17, 218, 131, 236, 53, 209 };
        // private static byte[] vector = { 146, 64, 191, 111, 23, 3, 113, 119, 231, 121, 221, 112, 79, 32, 114, 156 };
        private ICryptoTransform encryptor, decryptor;
        private Encoding encoder;

        public SimplerAES(string keyHex, string vectorHex, Encoding encoding)
            : this(HexToBytes(keyHex), HexToBytes(vectorHex), encoding)
        {
        }

        public SimplerAES(byte[] key, byte[] vector, Encoding encoding)
        {
            RijndaelManaged rm = new RijndaelManaged();
            encryptor = rm.CreateEncryptor(key, vector);
            decryptor = rm.CreateDecryptor(key, vector);
            encoder = encoding ?? new UTF8Encoding();
        }

        public string Encrypt(string unencrypted)
        {
            return Convert.ToBase64String(Encrypt(encoder.GetBytes(unencrypted)));
        }

        public string Decrypt(string encrypted)
        {
            return encoder.GetString(Decrypt(Convert.FromBase64String(encrypted)));
        }

        public byte[] Encrypt(byte[] buffer)
        {
            return Transform(buffer, encryptor);
        }

        public byte[] Decrypt(byte[] buffer)
        {
            return Transform(buffer, decryptor);
        }

        protected byte[] Transform(byte[] buffer, ICryptoTransform transform)
        {
            MemoryStream stream = new MemoryStream();
            using (CryptoStream cs = new CryptoStream(stream, transform, CryptoStreamMode.Write))
            {
                cs.Write(buffer, 0, buffer.Length);
            }
            return stream.ToArray();
        }

        private static string BytesToHex(byte[] bytes)
        {
            return bytes != null && bytes.Length > 0 ? BitConverter.ToString(bytes) : "";
        }

        /// <summary>
        /// From a StackOverflow comment: http://stackoverflow.com/a/321404
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        private static byte[] HexToBytes(string hex)
        {
            hex = hex ?? "";
            hex = hex.Replace(" ", "");
            hex = hex.Replace("\t", "");
            hex = hex.Trim();

            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
    }
}
