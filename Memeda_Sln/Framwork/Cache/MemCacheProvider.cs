using System;
//using Arch.App.Cache;
using Framework.Log;
using Framework.Common;

namespace Framework.Cache
{
    /// <summary>
    /// 基于MemCached的缓存实现
    /// </summary>

    public class MemCacheProvider //: CacheProvider
    {
        //private static readonly ICachable PaymentCache;

        ///// <summary>
        ///// 静态构造函数
        ///// </summary>
        //static MemCacheProvider()
        //{
        //    try
        //    {
        //        PaymentCache = CacheFactory.GetInstance("Payment");
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.LogException(ex);
        //    }
        //}

        ///// <summary>
        ///// 设置缓存
        ///// </summary>
        ///// <param name="key">键</param>
        ///// <param name="value">值</param>
        //public override bool SetData(string key, object value)
        //{
        //    if (value != null)
        //    {
        //        bool success = PaymentCache.Set(key, EntityUtil.Clone(value), new TimeSpan(0, 120, 0));
        //        if (!success)
        //        {
        //            //LogHelper.LogTrace("MemCacheProvider缓存" + key + "的Set方法返回False：" + value);
        //            return Remove(key);
        //        }
        //    }
        //    else
        //    {
        //        return Remove(key);
        //    }

        //    return true;
        //}

        ///// <summary>
        ///// 获取缓存
        ///// </summary>
        ///// <param name="key">键</param>
        ///// <returns>值</returns>
        //public override object GetData(string key)
        //{
        //    return EntityUtil.Clone(PaymentCache.Get(key));
        //}

        ///// <summary>
        ///// 移除缓存
        ///// </summary>
        ///// <param name="key">键</param>
        ///// <returns>值</returns>
        //public override bool Remove(string key)
        //{
        //    bool result = PaymentCache.Set(key, CacheUtil.Null);
        //    if (!result)
        //    {
        //        //LogHelper.LogException("MemCacheProvider缓存" + key + "的Set方法返回False【CacheUtil.Null】");
        //        result = PaymentCache.Delete(key);
        //        if (!result)
        //        {
        //            //LogHelper.LogException("MemCacheProvider缓存" + key + "的Delete方法返回False");
        //            return false;
        //        }
        //    }

        //    return true;
        //}
    }
}
