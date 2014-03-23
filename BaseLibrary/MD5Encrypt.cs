using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BaseLibrary
{
    public static class MD5Encrypt
    {
        /// <summary>
        /// 将字符串进行MD5加密
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Encrypt(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] encryptedBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(text));
            return BitConverter.ToString(encryptedBytes);
        }
    }
}
