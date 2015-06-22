using System;
using Framework.Log;
using Framework.Common;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;

namespace Framework.Cache
{
    /// <summary>
    /// 基于企业库的缓存实现
    /// </summary>

    public class EntCacheProvider : CacheProvider
    {
        private static readonly CacheManager cachemanager;

        /// <summary>
        /// 静态构造函数
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
        /// 设置缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
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
        /// 获取缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public override object GetData(string key)
        {
            return EntityUtil.Clone(cachemanager.GetData(key));
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public override bool Remove(string key)
        {
            cachemanager.Remove(key);
            return true;
        }
    }
}
