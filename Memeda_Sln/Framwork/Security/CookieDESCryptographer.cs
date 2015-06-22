using System;
using System.Collections.Generic;
using System.Text;
//using Ctrip.Common.Utility;


namespace Framework.Security
{
    /// <summary>
    /// Webͨ�Ź��̵ļӽ����࣭����Cookie��WebService�ļ��ܡ�����
    /// </summary>
    public class CookieDESCryptographer
    {
        /// <summary>
        /// DES�ԳƼ����㷨-����- ���㷨��Key ����AppConfig��CookieDesKey������
        /// </summary>
        /// <param name="sourceData">����</param>
        /// <returns>���ܺ������</returns>
        public static string EncryptDES(string sourceData)
        {
            if (string.IsNullOrEmpty(sourceData))
            {
                return string.Empty;
            }

            return Encrypt.EncryptDES(sourceData);
        }

        /// <summary>
        /// DES�ԳƼ����㷨-����--���㷨��Key ����AppConfig��CookieDesKey������
        /// </summary>
        /// <param name="sourceData">����</param>
        /// <returns>���ܺ������</returns>
        public static string DecryptDES(string sourceData)
        {
            if (string.IsNullOrEmpty(sourceData))
            {
                return string.Empty;
            }

            return Encrypt.DecryptDES(sourceData);
        }
    }
}
