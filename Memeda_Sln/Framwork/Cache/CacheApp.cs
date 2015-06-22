using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Framework.Cache
{
    /// <summary>
    /// 应用程序数据缓存类（适合大数据量且变化不频繁的场合）
    /// </summary>
    public class CacheApp
    {
        /// <summary>
        /// 获取当前应用程序指定cacheKey的cache对象值
        /// </summary>
        /// <param name="cacheKey">索引键值</param>
        /// <returns>返回缓存对象</returns>
        public static object GetCache(string cacheKey)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            return objCache[cacheKey];
        }

        /// <summary>
        /// 设置当前应用程序指定cacheKey的cache对象值
        /// </summary>
        /// <param name="cacheKey">索引键值</param>
        /// <param name="objObject">缓存对象</param>
        public static void SetCache(string cacheKey, object objObject)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(cacheKey, objObject);
        }

        /// <summary>
        /// 设置当前应用程序指定cacheKey的Cache对象值
        /// </summary>
        /// <param name="cacheKey">索引键值</param>
        /// <param name="objObject">缓存对象</param>
        /// <param name="absoluteExpiration">绝对过期时间</param>
        /// <param name="slidingExpiration">最后一次访问所插入对象时与该对象过期时之间的时间间隔</param>
        public static void SetCache(string cacheKey, object objObject, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(cacheKey, objObject, null, absoluteExpiration, slidingExpiration);
        } 

    }

}
