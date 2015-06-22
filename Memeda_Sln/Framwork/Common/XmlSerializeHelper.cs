using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Linq;
using System.Reflection;
using System.Text; 

namespace Framework.Common
{
    /// <summary>
    /// Xml���л��ͷ�ϵ�л�������
    /// </summary>
    public class XmlSerializeHelper
    {
        /// <summary>
        /// ���л�����ΪXML��GB2312��
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="type"></param>
        /// <returns>���</returns>
        public static string SerialObjectGB(object obj, System.Type type)
        {
            XmlSerializer xs = new XmlSerializer(type);
            MemoryStream stream = new MemoryStream();
            XmlWriterSettings setting = new XmlWriterSettings();
            XmlSerializerNamespaces nameSpace = new XmlSerializerNamespaces();
            {
                nameSpace.Add(String.Empty, String.Empty);
                setting.Encoding = new UTF8Encoding(false);
                setting.Indent = true;
            }

            using (XmlWriter writer = XmlWriter.Create(stream, setting))
            {
                xs.Serialize(writer, obj, nameSpace);
            }

            return Encoding.UTF8.GetString(stream.ToArray()).Replace(
                "encoding=\"utf-8\"", "encoding=\"gb2312\"");
        }

        /// <summary>
        /// ���л�:���������л���XML�ַ���
        /// </summary>
        /// <param name="object">��Ҫ�������л��Ķ���</param>
        /// <param name="type">��������</param>
        /// <returns>���ص�XML�ַ���</returns>
        public static string SerialObject(object obj, System.Type type)
        {
            string retSerialXml = string.Empty;
            MemoryStream StreamRequest = new MemoryStream();
            StreamWriter writer = new StreamWriter(StreamRequest, System.Text.Encoding.UTF8);
            XmlSerializer serializerRequest = new XmlSerializer(type);
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            serializerRequest.Serialize(writer, obj, ns);
            retSerialXml = System.Text.Encoding.UTF8.GetString(StreamRequest.ToArray());
            return retSerialXml;
        }

        /// <summary>
        /// �����л�:��XML�ַ������л��ɶ���
        /// </summary>
        /// <param name="batchXml">XML�ַ���</param>
        /// <param name="type">���������</param>
        /// <returns>���صĶ���</returns>
        public static object SerialXML(string rawXml, System.Type type)
        {
            object objRet;
            MemoryStream StreamResponse = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(rawXml));
            XmlSerializer serializerResponse = new XmlSerializer(type);
            objRet = serializerResponse.Deserialize(StreamResponse);
            return objRet;
        }

        /// <summary>
        /// ���������л����ֽ�����
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputObject"></param>
        /// <returns>���</returns>
        public static byte[] SerialBytes<T>(T inputObject)
        {
            BinaryFormatter Formatter = new BinaryFormatter();
            MemoryStream StreamRequest = new MemoryStream();
            Formatter.Serialize(StreamRequest, inputObject);
            return StreamRequest.ToArray();
        }

        /// <summary>
        /// ���ֽ��������л��ɶ���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="binaryArray"></param>
        /// <returns>���</returns>
        public static T SerialToEntity<T>(byte[] binaryArray)
        {
            MemoryStream StreamRequest = new MemoryStream(binaryArray);
            BinaryFormatter Formatter = new BinaryFormatter();
            return (T)Formatter.Deserialize(StreamRequest);
        }



        /// <summary>
        /// �������л��� XML String
        /// </summary>
        public static string XmlSerialize<T>(T obj)
        {
            string xmlString = string.Empty;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream())
            {
                xmlSerializer.Serialize(ms, obj);
                xmlString = Encoding.UTF8.GetString(ms.ToArray());
            }

            return xmlString;
        }

        /// <summary>
        /// XML String �����л��ɶ���
        /// </summary>
        public static T XmlDeserialize<T>(string xmlString)
        {
            T t = default(T);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (Stream xmlStream = new MemoryStream(Encoding.UTF8.GetBytes(xmlString)))
            {
                using (XmlReader xmlReader = XmlReader.Create(xmlStream))
                {
                    Object obj = xmlSerializer.Deserialize(xmlReader);
                    t = (T)obj;
                }
            }

            return t;
        }
        #region ʵ����XML֮��Ļ���ת��
        /// <summary>
        /// ʵ�弯��ת��Xml
        /// </summary>
        public static string ToXml<T>(IList<T> entities, string rootName = "") where T : new()
        {
            if (entities == null || entities.Count == 0)
            {
                return string.Empty;
            }

            StringBuilder builder = new StringBuilder();
            //builder.AppendLine(XmlResource.XmlHeader);

            XElement element = ToXElement<T>(entities, rootName);
            builder.Append(element.ToString());

            return builder.ToString();
        }
        /// <summary>
        /// ʵ��ת��Xml
        /// </summary>
        public static string ToXml<T>(T entity) where T : new()
        {
            if (entity == null)
            {
                return string.Empty;
            }

            XElement element = ToXElement<T>(entity);

            return element.ToString();
        }

        /// <summary>
        /// ʵ�弯��ת��XElement
        /// </summary>
        public static XElement ToXElement<T>(IList<T> entities, string rootName = "") where T : new()
        {
            if (entities == null || entities.Count == 0)
            {
                return null;
            }

            //if (string.IsNullOrWhiteSpace(rootName))
            //{
            //    rootName = typeof(T).Name + XmlResource.XmlRootNameSuffix;
            //}

            XElement element = new XElement(rootName);

            foreach (T entity in entities)
            {
                element.Add(ToXElement<T>(entity));
            }

            return element;
        }
        /// <summary>
        /// ʵ�弯��ת��XmlDocument
        /// </summary>
        public static XmlDocument ToXmlDocument<T>(IList<T> entities, string rootName = "") where T : new()
        {
            if (entities == null || entities.Count == 0)
            {
                return null;
            }

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(ToXml<T>(entities, rootName));

            return xmlDocument;
        }

        /// <summary>
        /// ʵ��ת��XmlDocument
        /// </summary>
        public static XmlDocument ToXmlDocument<T>(T entity) where T : new()
        {
            if (entity == null)
            {
                return null;
            }

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(ToXml<T>(entity));

            return xmlDocument;
        }
        
        /// <summary>
        /// ʵ��ת��XElement
        /// </summary>
        public static XElement ToXElement<T>(T entity) where T : new()
        {
            if (entity == null)
            {
                return null;
            }

            XElement element = new XElement(typeof(T).Name);
            PropertyInfo[] properties = typeof(T).GetProperties();
            XElement innerElement = null;
            object propertyValue = null;

            foreach (PropertyInfo property in properties)
            {
                propertyValue = property.GetValue(entity, null);
                innerElement = new XElement(property.Name, propertyValue);
                element.Add(innerElement);
            }

            return element;
        }
        /// <summary>
        /// ʵ�弯��ת��XDocument
        /// </summary>
        public static XDocument ToXDocument<T>(IList<T> entities, string rootName = "") where T : new()
        {
            if (entities == null || entities.Count == 0)
            {
                return null;
            }

            return XDocument.Parse(ToXml<T>(entities, rootName));
        }
        #endregion
    }
}
