using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Tools
{
    public class EncryptionAndDecryption
    {
        /// <summary>
        /// 加密一个字符串
        /// </summary>
        /// <param name="yw">原文</param>
        /// <param name="key">密钥</param>
        /// <returns>返回加密后的字符串</returns>
        public static string Encryption(string yw, string key)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] bsmy = md5.ComputeHash(Encoding.UTF8.GetBytes(key));
            TripleDESCryptoServiceProvider t = new TripleDESCryptoServiceProvider();
            t.Key = bsmy;
            t.Mode = CipherMode.ECB;
            byte[] bs = t.CreateEncryptor().TransformFinalBlock(Encoding.UTF8.GetBytes(yw), 0, Encoding.UTF8.GetBytes(yw).Length);
            return Convert.ToBase64String(bs);
        }
        /// <summary>
        /// 解密一个字符串
        /// </summary>
        /// <param name="mw">密文</param>
        /// <param name="key">密钥</param>
        /// <returns>返回原文</returns>
        public static string Decryption(string mw, string key)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] bsmy = md5.ComputeHash(Encoding.UTF8.GetBytes(key));
            TripleDESCryptoServiceProvider t = new TripleDESCryptoServiceProvider();
            t.Key = bsmy;
            t.Mode = CipherMode.ECB;
            byte[] bs = t.CreateDecryptor().TransformFinalBlock(Convert.FromBase64String(mw), 0, Convert.FromBase64String(mw).Length);
            return Encoding.UTF8.GetString(bs);
        }
    }
}
