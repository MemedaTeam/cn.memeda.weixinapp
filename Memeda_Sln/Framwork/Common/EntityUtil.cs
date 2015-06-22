using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Framework.Common
{
    /// <summary>
    /// ʵ�幤��
    /// </summary>
    public static class EntityUtil
    {
        /// <summary>
        /// ��¡����
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static T Clone<T>(T t) where T : new()
        {
            if (t == null)
            {
                return t;
            }

            using (MemoryStream memStream = new MemoryStream())
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.Clone));
                binaryFormatter.Serialize(memStream, t);
                memStream.Seek(0, SeekOrigin.Begin);

                return (T)binaryFormatter.Deserialize(memStream);
            }
        }
    }
}
