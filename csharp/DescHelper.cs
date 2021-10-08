using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SeaCodeLib.Common
{
    /// <summary>
    /// Desc加密解密
    /// </summary>
    public class DescHelper
    {
        /// <summary>
        /// Desc加密
        /// </summary>
        /// <param name="pToEncrypt">需要加密的值</param>
        /// <param name="sKey">加密key(8个字符，64位)</param>
        /// <returns></returns>
        public static string Encrypt(string pToEncrypt, string sKey= "F3u7!##5")
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
            return ret.ToString();

        }

        /// <summary>
        /// Desc解密
        /// </summary>
        /// <param name="pToDecrypt">需要加密的值</param>
        /// <param name="sKey">加密key(8个字符，64位)</param>
        /// <returns></returns>
        public static string Decrypt(string pToDecrypt, string sKey= "F3u7!##5")
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
            for (int x = 0; x < pToDecrypt.Length / 2; x++)
            {
                int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }

            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            StringBuilder ret = new StringBuilder();

            return System.Text.Encoding.Default.GetString(ms.ToArray());
        }
    }
}
