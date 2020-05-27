
using CryptographyLayer.EncryptionOperations.Models;
using NotasProject.Services.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CryptographyLayer.EncryptionOperations
{
    public static class EncryptionExtensions
    {
        /// <summary>
        /// Performs decryption of related string with both provided nonce and aeskey
        /// </summary>
        /// <param name="strg">Base64 string to decrypt</param>
        /// <param name="nonce">Base64 encoded nonce</param>
        /// <param name="aesKey">AES key</param>
        /// <returns></returns>
        public static string AesDecrypt(this string strg, string nonce, string aesKey)
        {
            using (AesManaged aes = new AesManaged())
            {
                ICryptoTransform decryptor = aes.CreateDecryptor(Convert.FromBase64String(aesKey), Convert.FromBase64String(nonce));
                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(strg)))
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (StreamReader sr = new StreamReader(cs))
                    return sr.ReadToEnd();
            }
        }
        /// <summary>
        /// Perform AES with 256-bit key and random nonce (returned for later decrypt)
        /// </summary>
        /// <param name="chars"></param>
        /// <param name="aesKey"></param>
        /// <returns></returns>
        public static AesResult AesEncrypt(this string chars, string aesKey)
        {
            byte[] nonce = SetUnique16BitAES();
            using (AesManaged aes = new AesManaged())
            {
                ICryptoTransform encryptor = aes.CreateEncryptor(Convert.FromBase64String(aesKey), nonce);
                using (MemoryStream ms = new MemoryStream())
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter sw = new StreamWriter(cs))
                        sw.Write(chars);
                    return new AesResult() { CipheredData = Convert.ToBase64String(ms.ToArray()), Nonce = Convert.ToBase64String(nonce) };
                }
            }
        }
        private static byte[] SetUnique16BitAES()
        {
            byte[] preVnonce = (DateTime.Now.Ticks * (15)).ToString("X").ConvertTimeStampToRandomizedNonce();
            byte[] nonce = new byte[16];
            Array.Copy(ArrayHelper.Concat<byte>(preVnonce.Reverse().ToArray(), preVnonce), nonce, 16);
            return nonce;
        }
    }
}
