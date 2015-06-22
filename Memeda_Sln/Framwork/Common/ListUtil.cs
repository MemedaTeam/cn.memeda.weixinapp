using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Common
{
    /// <summary>
    /// 列表工具类
    /// </summary>
    public static class ListUtil
    {
        /// <summary>
        /// 复制列表，而不是引用地址改变
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns>结果</returns>
        public static IList<T> CopyList<T>(IList<T> list)
        {
            if (list != null)
            {
                IList<T> result = new List<T>();
                foreach(T t in list)
                {
                    result.Add(t);
                }

                return result;
            }

            return null;
        }
    }
}
