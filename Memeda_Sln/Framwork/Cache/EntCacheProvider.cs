using System;
using Framework.Log;
using Framework.Common;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;

namespace Framework.Cache
{
    /// <summary>
    /// ������ҵ��Ļ���ʵ��
    /// </summary>

    public class EntCacheProvider : CacheProvider
    {
        private static readonly CacheManager cachemanager;

        /// <summary>
        /// ��̬���캯��
        /// </summary>
        static EntCacheProvider()
        {
            try
            {
                cachemanager = (CacheManager)CacheFactory.GetCacheManager();
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
        public override bool SetData(string key, object value)
        {
            if (value != null)
            {
                cachemanager.Add(key, EntityUtil.Clone(value), CacheItemPriority.Normal, null, new SlidingTime(TimeSpan.FromMinutes(120)));
            }
            else
            {
                Remove(key);
            }

            return true;
        }

        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <param name="key">��</param>
        /// <returns>ֵ</returns>
        public override object GetData(string key)
        {
            return EntityUtil.Clone(cachemanager.GetData(key));
        }

        /// <summary>
        /// �Ƴ�����
        /// </summary>
        /// <param name="key">��</param>
        /// <returns>ֵ</returns>
        public override bool Remove(string key)
        {
            cachemanager.Remove(key);
            return true;
        }
    }
}
