using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Cache
{
    /// <summary>
    /// 缓存基类
    /// </summary>
    public abstract class CacheProvider
    {
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public abstract bool SetData(string key, object value);

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public abstract object GetData(string key);

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public abstract bool Remove(string key);
    }
}
