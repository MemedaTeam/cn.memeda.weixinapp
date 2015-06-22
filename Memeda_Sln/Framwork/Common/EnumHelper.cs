using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using SystemEnum = System.Enum;

namespace Framework.Common
{
    /// <summary>
    /// ö�ٰ�����
    /// </summary>
    public class EnumHelper
    {
        /// <summary>
        /// ����ö�ٵ�intֵ����ת��Ϊö������
        /// </summary>
        /// <typeparam name="T">ö������</typeparam>
        /// <param name="currentValue">ö�ٵ�intֵ</param>
        /// <returns>ö������</returns>
        public static T GetEnumByValue<T>(int currentValue)
        {
            return (T)SystemEnum.Parse(typeof(T), currentValue.ToString());
        }

        /// <summary>
        /// ����ö��˵����ȡö������
        /// </summary>
        /// <typeparam name="T">ö������</typeparam>
        /// <param name="currentValue">ö�ٵ�˵��</param>
        /// <returns>ö��ֵ</returns>
        public static T GetEnumByDescription<T>(string currentValue)
        {
            return (T)SystemEnum.Parse(typeof(T), currentValue);
        }

        /// <summary>
        /// ����ö�ٵ�intֵ��ȡö��ֵ����ϸ˵��
        /// </summary>
        /// <typeparam name="T">ö������</typeparam>
        /// <param name="currentValue">ö�ٵ�Intֵ</param>
        /// <returns>ö��ֵ˵��</returns>
        public static string GetEnumDescriptionByValue<T>(int currentValue)
        {
            T currentType = GetEnumByValue<T>(currentValue);
            return GetEnumDescription<T>(currentType);
        }

        /// <summary>
        /// ��ȡö��ֵ��˵��
        /// </summary>
        /// <typeparam name="T">ö������</typeparam>
        /// <param name="enumType">ö������ֵ</param>
        /// <returns>ö��ֵ˵��</returns>
        public static string GetEnumDescription<T>(T enumType)
        {
            Type currentType = enumType.GetType();
            //��ȡ�ֶ���Ϣ   
            FieldInfo[] fields = currentType.GetFields();

            if (fields == null || fields.Length < 1)
            {

                return enumType.ToString();
            }

            for (int i = 1; i < fields.Length; i++)
            {
                FieldInfo field = fields[i];

                //�ж������Ƿ����   
                if (field.Name != enumType.ToString())
                {
                    continue;
                }

                //�����Զ�������   
                object[] attributes = field.GetCustomAttributes(
                    typeof(DescriptionAttribute), false);

                foreach (Attribute attribute in attributes)
                {
                    //����ת���ҵ�һ��Description����Description��Ϊ��Ա����   
                    DescriptionAttribute currentAttribute =
                        attribute as DescriptionAttribute;

                    if (currentAttribute != null)
                    {
                        return currentAttribute.Description;
                    }
                }
            }

            //���û�м�⵽���ʵ�ע�ͣ�����Ĭ������   
            return enumType.ToString();

        }

        /// <summary>
        /// ����ö�����ͷ��������е�����ֵ��˵��
        /// </summary>
        /// <typeparam name="T">ö������</typeparam>
        /// <returns>ö�������ֵ伯</returns>
        public static Dictionary<string, string> GetGetEnumDescriptions(Type currentType)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            //��ȡ�ֶ���Ϣ   
            FieldInfo[] fields = currentType.GetFields();
            if (fields == null || fields.Length < 1)
            {

                return result;
            }

            for (int i = 1; i < fields.Length; i++)
            {
                FieldInfo field = fields[i];

                string currentKey = ((int)SystemEnum.Parse(currentType, field.Name)).ToString();
                string currentValue = field.Name;

                //�����Զ�������   
                object[] attributes = field.GetCustomAttributes(
                    typeof(DescriptionAttribute), false);

                foreach (Attribute attribute in attributes)
                {
                    //����ת���ҵ�һ��Description����Description��Ϊ��Ա����   
                    DescriptionAttribute currentAttribute =
                        attribute as DescriptionAttribute;

                    if (currentAttribute != null)
                    {
                        currentValue = currentAttribute.Description;
                    }
                }

                if (!result.ContainsKey(currentKey))
                {
                    result.Add(currentKey, currentValue);
                }
            }

            return result;
        }
    }
}
