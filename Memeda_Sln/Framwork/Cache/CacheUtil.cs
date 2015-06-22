using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using Framework.Log;
using Framework.Security;

namespace Framework.Cache
{
    /// <summary>
    /// 缓存工具类
    /// </summary>

    public static class CacheUtil
    {
        /// <summary>
        /// 指定的空缓存值
        /// </summary>
        [Serializable]
        public class NullCache { }

        /// <summary>
        /// 指定的空缓存值
        /// </summary>
        public static readonly NullCache Null = new NullCache();

        /// <summary>
        /// 缓存实现
        /// </summary>
        public static readonly CacheProvider Provider;

        /// <summary>
        /// 当前记录日志的秒数
        /// </summary>
        private static int CurrentSecond = 10;

        /// <summary>
        /// 静态构造函数    
        /// </summary>
        static CacheUtil()
        {
            try
            {
                //string enableMemCached = ConfigurationManager.AppSettings["EnableMemCached"] ?? "1";
                ////LogHelper.LogTrace("EnableMemCached=" + enableMemCached);
                //if (enableMemCached.Equals("1"))
                //{
                //    //Provider = new MemCacheProvider();
                //}
                //else
                //{
                    Provider = new EntCacheProvider();
                //}
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public static bool SetData(string key, object value)
        {
            try
            {
                string newKey = VerifyAndUpdateKey(key);
                return Provider.SetData(newKey, value);
            }
            catch(Exception ex)
            {
                LogHelper.LogException(ex);
            }

            return false;
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public static object GetData(string key)
        {
            DateTime currentDate = DateTime.Now;
            try
            {
                string newKey = VerifyAndUpdateKey(key);

                object result = Provider.GetData(newKey);

                return result;
            }
            catch(Exception ex)
            {
                LogHelper.LogException(ex);
                return null;
            }
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public static bool Remove(string key)
        {
            try
            {
                string newKey = VerifyAndUpdateKey(key);
                return Provider.Remove(newKey);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }

            return false;
        }

        /// <summary>
        /// 缓存KEY若无效则替换并记录日志
        /// </summary>
        /// <param name="key">键</param>
        private static string VerifyAndUpdateKey(string key)
        {
            if (key.IndexOf("_") != -1)
            {
                key = key.Replace("_", "X");
            }

            Regex regex = new Regex(@"^[a-zA-Z0-9]+$");
            if (key.Length > 128 || !regex.IsMatch(key))
            {
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(key));
                string newKey = BitConverter.ToString(hashBytes).Replace("-", "").ToUpper();

                //LogHelper.LogTrace("缓存KEY无效：" + key + "/KEY被替换为：" + newKey);

                return newKey;
            }
            else
            {
                return key;
            }
        }
    }
}
