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
    /// ���湤����
    /// </summary>

    public static class CacheUtil
    {
        /// <summary>
        /// ָ���Ŀջ���ֵ
        /// </summary>
        [Serializable]
        public class NullCache { }

        /// <summary>
        /// ָ���Ŀջ���ֵ
        /// </summary>
        public static readonly NullCache Null = new NullCache();

        /// <summary>
        /// ����ʵ��
        /// </summary>
        public static readonly CacheProvider Provider;

        /// <summary>
        /// ��ǰ��¼��־������
        /// </summary>
        private static int CurrentSecond = 10;

        /// <summary>
        /// ��̬���캯��    
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
        /// ���û���
        /// </summary>
        /// <param name="key">��</param>
        /// <param name="value">ֵ</param>
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
        /// ��ȡ����
        /// </summary>
        /// <param name="key">��</param>
        /// <returns>ֵ</returns>
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
        /// �Ƴ�����
        /// </summary>
        /// <param name="key">��</param>
        /// <returns>ֵ</returns>
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
        /// ����KEY����Ч���滻����¼��־
        /// </summary>
        /// <param name="key">��</param>
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

                //LogHelper.LogTrace("����KEY��Ч��" + key + "/KEY���滻Ϊ��" + newKey);

                return newKey;
            }
            else
            {
                return key;
            }
        }
    }
}
