using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Cache
{
    /// <summary>
    /// �������
    /// </summary>
    public abstract class CacheProvider
    {
        /// <summary>
        /// ���û���
        /// </summary>
        /// <param name="key">��</param>
        /// <param name="value">ֵ</param>
        public abstract bool SetData(string key, object value);

        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <param name="key">��</param>
        /// <returns>ֵ</returns>
        public abstract object GetData(string key);

        /// <summary>
        /// �Ƴ�����
        /// </summary>
        /// <param name="key">��</param>
        /// <returns>ֵ</returns>
        public abstract bool Remove(string key);
    }
}
