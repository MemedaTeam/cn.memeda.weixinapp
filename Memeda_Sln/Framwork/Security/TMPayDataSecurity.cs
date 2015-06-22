using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

//using Ctrip.Common.Security;

namespace Framework.Security
{
    /// <summary>
    /// ��Ʊ�洢���ݰ�ȫ��
    /// ���ص�����ݽ������ݼӽ���
    /// </summary>
    public static class TMPayDataSecurity
    {
        /// <summary>
        /// DES�ԳƼ����㷨-����
        /// </summary>
        /// <param name="sourceData">����</param>
        /// <returns>���ܺ������</returns>
        //public static string EncryptDES(string sourceData)
        //{
        //    if (string.IsNullOrEmpty(sourceData))
        //    {
        //        return string.Empty;
        //    }

        //    Cryptographer cryptographer = new Cryptographer();
        //    return cryptographer.EncryptCommon(sourceData);
        //}

        /// <summary>
        /// DES�ԳƼ����㷨-����
        /// </summary>
        /// <param name="sourceData">����</param>
        /// <returns>���ܺ������</returns>
        //public static string DecryptDES(string sourceData)
        //{
        //    if (string.IsNullOrEmpty(sourceData))
        //    {
        //        return string.Empty;
        //    }

        //    Cryptographer cryptographer = new Cryptographer();
        //    return cryptographer.DecryptCommon(sourceData);
        //}

        /// <summary>
        /// �����ݽ���MD5ɢ��
        /// </summary>
        /// <param name="sourceData">����ǰ������</param>
        /// <returns>���ܺ������</returns>
        public static string DataHashByMD5(string sourceData)
        {
            if (String.IsNullOrEmpty(sourceData))
            {
                throw new ArgumentNullException("The param [sourceData] cann't be null.");
            }

            //�����ݽ���һ�λ��
            sourceData = MixData(sourceData);

            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(sourceData));

            //ת��Ϊ16�����ַ���
            StringBuilder builder = new StringBuilder();
            foreach (byte currentByte in md5Hasher.Hash)
            {

                builder.Append(currentByte.ToString("x2"));
            }

            return builder.ToString();
        }

        /// <summary>
        /// �������
        /// </summary>
        /// <param name="data"></param>
        /// <returns>���</returns>
        private static string MixData(string data)
        {
            //��Ϲ���ԭ���� �� ����λ���� �� ż��λ����
            //ԭ�������Ϊ��12345�����Ϻ������Ϊ��12345 �� 135 �� 24
            int strLen = data.Length;

            string oddData = "", evenData = string.Empty;
            for (int i = 0; i < strLen; i++)
            {
                if (!Convert.ToBoolean(i % 2))
                {
                    //����λ
                    oddData += data.Substring(i, 1);
                }
                else
                {
                    //ż��λ
                    evenData += data.Substring(i, 1);
                }
            }
            return data + oddData + evenData;
        }
    }
}
