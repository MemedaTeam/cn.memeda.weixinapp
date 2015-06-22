using System;
using System.Security.Cryptography;
using System.Text;

namespace Framework.Security
{
    /// <summary>
    /// 支付平台-安全工具类
    /// </summary>
    /// <history>
    ///     <date>2011-03-23</date>
    ///     <programmer>taoqingxue</programmer>
    /// </history>
    public static class SecurityUtil
    {
        /// <summary>
        /// 创建签名
        /// </summary>
        /// <param name="text">明文</param>
        /// <param name="merchantKey">商户密钥</param>
        /// <returns>签名</returns>
        public static string CreateSign(string text, string merchantKey)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(text + "&Key=" + merchantKey));

            return BitConverter.ToString(hashBytes).Replace("-", "").ToUpper();
        }

        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="text">明文</param>
        /// <param name="merchantKey">商户密钥</param>
        /// <param name="sign">原始签名</param>
        /// <returns>true表示成功，false表示失败</returns>
        public static bool VerifySign(string text, string merchantKey, string sign)
        {
            return SecurityUtil.CreateSign(text, merchantKey).Equals(sign);
        }
    }
}