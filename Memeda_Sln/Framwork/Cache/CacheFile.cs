using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Framework.Cache
{
    /// <summary>
    /// 文件缓存依赖（适合读取配置文件）
    /// </summary>
    public class CacheFile
    {
        /// <summary>
        /// 获取当前应用程序指定cacheKey的Cache对象值
        /// </summary>
        /// <param name="cacheKey">索引键值</param>
        /// <returns>返回缓存对象</returns>
        public static object GetCache(string cacheKey)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            return objCache[cacheKey];
        }

        /// <summary>
        /// 设置以缓存依赖的方式缓存数据
        /// </summary>
        /// <param name="cacheKey">索引键值</param>
        /// <param name="objObject">缓存对象</param>
        /// <param name="cacheDepen">依赖对象</param>
        public static void SetCache(string cacheKey, object objObject, System.Web.Caching.CacheDependency cacheDepen)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(
                cacheKey,
                objObject,
                cacheDepen,
                System.Web.Caching.Cache.NoAbsoluteExpiration, //从不过期
                System.Web.Caching.Cache.NoSlidingExpiration, //禁用可调过期
                System.Web.Caching.CacheItemPriority.Default,
                null);
        }

    }



}
