using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Common
{
    /// <summary>
    /// �б�����
    /// </summary>
    public static class ListUtil
    {
        /// <summary>
        /// �����б����������õ�ַ�ı�
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns>���</returns>
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
