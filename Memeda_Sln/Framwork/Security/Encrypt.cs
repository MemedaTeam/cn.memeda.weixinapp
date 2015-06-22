using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Framework.Security
{
    /// <summary>
    /// 对称加密、解密算法
    /// </summary>
    public class Encrypt
    {
        private static SymmetricAlgorithm mobjCryptoService = null;
        private static string Key = string.Empty;

        #region 对称加密类的构造函数

        /// <summary>
        /// 对称加密类的构造函数
        /// </summary>
        public Encrypt()
        {
            mobjCryptoService = new RijndaelManaged();
            Key = "Guz(%&hj7x89H$yuBI0456FtmaT5&fvHUFCy76*h%(HilJ$lhj!y6&(*jkP87jH7";
        }

        #endregion

        /// <summary>
        /// 获得密钥
        /// </summary>
        /// <returns>密钥</returns>
        private static byte[] GetLegalKey()
        {
            string sTemp = Key;
            mobjCryptoService.GenerateKey();

            byte[] bytTemp = mobjCryptoService.Key;
            int KeyLength = bytTemp.Length;

            if (sTemp.Length > KeyLength)
            {
                sTemp = sTemp.Substring(0, KeyLength);
            }
            else if (sTemp.Length < KeyLength)
            {
                sTemp = sTemp.PadRight(KeyLength, ' ');
            }

            return ASCIIEncoding.ASCII.GetBytes(sTemp);
        }

        /// <summary>
        /// 获得初始向量IV
        /// </summary>
        /// <returns>初试向量IV</returns>
        private static byte[] GetLegalIV()
        {
            string sTemp = "E4ghj*Ghg7!rNIfb&95GUY86GfghUb#er57HBh(u%g6HJ($jhWk7&!hg4ui%$hjk";
            mobjCryptoService.GenerateIV();
            byte[] bytTemp = mobjCryptoService.IV;
            int IVLength = bytTemp.Length;

            if (sTemp.Length > IVLength)
            {
                sTemp = sTemp.Substring(0, IVLength);
            }
            else if (sTemp.Length < IVLength)
            {
                sTemp = sTemp.PadRight(IVLength, ' ');
            }

            return ASCIIEncoding.ASCII.GetBytes(sTemp);
        }

        /// <summary>
        /// 加密方法
        /// </summary>
        /// <param name="Source">待加密的串</param>
        /// <returns>经过加密的串</returns>
        public static string EncryptDES(string Source)
        {
            byte[] bytIn = UTF8Encoding.UTF8.GetBytes(Source);
            MemoryStream ms = new MemoryStream();

            mobjCryptoService.Key = GetLegalKey();
            mobjCryptoService.IV = GetLegalIV();

            ICryptoTransform encrypto = mobjCryptoService.CreateEncryptor();
            CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);
            cs.Write(bytIn, 0, bytIn.Length);
            cs.FlushFinalBlock();
            ms.Close();
            byte[] bytOut = ms.ToArray();

            return Convert.ToBase64String(bytOut);
        }

        /// <summary>
        /// 解密方法
        /// </summary>
        /// <param name="Source">待解密的串</param>
        /// <returns>经过解密的串</returns>
        public static string DecryptDES(string Source)
        {
            byte[] bytIn = Convert.FromBase64String(Source);
            MemoryStream ms = new MemoryStream(bytIn, 0, bytIn.Length);

            mobjCryptoService.Key = GetLegalKey();
            mobjCryptoService.IV = GetLegalIV();

            ICryptoTransform encrypto = mobjCryptoService.CreateDecryptor();
            CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cs);

            return sr.ReadToEnd();
        }

        /// <summary>
        /// 用户名、密码、IP合并后使用MD5加密，即使密码泄露也更难暴力破解或字典查询破解
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="PSW">密码</param>
        /// <returns></returns>
        public static string EncryptUserPassword(string UserName, string PSW)
        {
            return EncryptMD5(UserName.ToLower() + PSW.ToLower());
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="source">待加密字符串</param>
        /// <returns></returns>
        public static string EncryptMD5(string source)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            byte[] str1 = Encoding.UTF8.GetBytes(source);
            byte[] str2 = md5.ComputeHash(str1, 0, str1.Length);
            md5.Clear();
            (md5 as IDisposable).Dispose();

            return Convert.ToBase64String(str2);
        }

        /// <summary>
        /// 编码URL为BASE64
        /// </summary>
        /// <param name="code">编码的URL</param>
        /// <returns>BASE64编码后的页面</returns>
        public static string EncodeBase64(string code)
        {
            try
            {
                string encode = string.Empty;
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(code);
                //由于在本地保存缓存文件的时候文件名不能超过255，而有些URL地址很大，所以需要进行反转操作
                //Array.Reverse(bytes);
                try
                {
                    encode = Convert.ToBase64String(bytes);
                }
                catch
                {
                    encode = code;
                }
                return encode;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 获取随机密码
        /// </summary>
        /// <param name="codeLen">密码长度</param>
        /// <returns></returns>
        public static string GetRndPsw(int codeLen)
        {
            Random rnd = new Random();
            return String.Format("{0:D" + codeLen + "}", rnd.Next((int)System.Math.Pow((double)10, (double)codeLen)));
        }

    }
}
