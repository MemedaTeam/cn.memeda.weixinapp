using System;
//using Arch.App.Cache;
using Framework.Log;
using Framework.Common;

namespace Framework.Cache
{
    /// <summary>
    /// ����MemCached�Ļ���ʵ��
    /// </summary>

    public class MemCacheProvider //: CacheProvider
    {
        //private static readonly ICachable PaymentCache;

        ///// <summary>
        ///// ��̬���캯��
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
        ///// ���û���
        ///// </summary>
        ///// <param name="key">��</param>
        ///// <param name="value">ֵ</param>
        //public override bool SetData(string key, object value)
        //{
        //    if (value != null)
        //    {
        //        bool success = PaymentCache.Set(key, EntityUtil.Clone(value), new TimeSpan(0, 120, 0));
        //        if (!success)
        //        {
        //            //LogHelper.LogTrace("MemCacheProvider����" + key + "��Set��������False��" + value);
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
        ///// ��ȡ����
        ///// </summary>
        ///// <param name="key">��</param>
        ///// <returns>ֵ</returns>
        //public override object GetData(string key)
        //{
        //    return EntityUtil.Clone(PaymentCache.Get(key));
        //}

        ///// <summary>
        ///// �Ƴ�����
        ///// </summary>
        ///// <param name="key">��</param>
        ///// <returns>ֵ</returns>
        //public override bool Remove(string key)
        //{
        //    bool result = PaymentCache.Set(key, CacheUtil.Null);
        //    if (!result)
        //    {
        //        //LogHelper.LogException("MemCacheProvider����" + key + "��Set��������False��CacheUtil.Null��");
        //        result = PaymentCache.Delete(key);
        //        if (!result)
        //        {
        //            //LogHelper.LogException("MemCacheProvider����" + key + "��Delete��������False");
        //            return false;
        //        }
        //    }

        //    return true;
        //}
    }
}
