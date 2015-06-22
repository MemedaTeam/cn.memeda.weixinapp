using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using SystemEnum = System.Enum;

namespace Framework.Common
{
    /// <summary>
    /// 枚举帮助类
    /// </summary>
    public class EnumHelper
    {
        /// <summary>
        /// 根据枚举的int值类型转换为枚举类型
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="currentValue">枚举的int值</param>
        /// <returns>枚举类型</returns>
        public static T GetEnumByValue<T>(int currentValue)
        {
            return (T)SystemEnum.Parse(typeof(T), currentValue.ToString());
        }

        /// <summary>
        /// 根据枚举说明获取枚举类型
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="currentValue">枚举的说明</param>
        /// <returns>枚举值</returns>
        public static T GetEnumByDescription<T>(string currentValue)
        {
            return (T)SystemEnum.Parse(typeof(T), currentValue);
        }

        /// <summary>
        /// 根据枚举的int值获取枚举值的详细说明
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="currentValue">枚举的Int值</param>
        /// <returns>枚举值说明</returns>
        public static string GetEnumDescriptionByValue<T>(int currentValue)
        {
            T currentType = GetEnumByValue<T>(currentValue);
            return GetEnumDescription<T>(currentType);
        }

        /// <summary>
        /// 获取枚举值的说明
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="enumType">枚举类型值</param>
        /// <returns>枚举值说明</returns>
        public static string GetEnumDescription<T>(T enumType)
        {
            Type currentType = enumType.GetType();
            //获取字段信息   
            FieldInfo[] fields = currentType.GetFields();

            if (fields == null || fields.Length < 1)
            {

                return enumType.ToString();
            }

            for (int i = 1; i < fields.Length; i++)
            {
                FieldInfo field = fields[i];

                //判断名称是否相等   
                if (field.Name != enumType.ToString())
                {
                    continue;
                }

                //反射自定义属性   
                object[] attributes = field.GetCustomAttributes(
                    typeof(DescriptionAttribute), false);

                foreach (Attribute attribute in attributes)
                {
                    //类型转换找到一个Description，用Description作为成员名称   
                    DescriptionAttribute currentAttribute =
                        attribute as DescriptionAttribute;

                    if (currentAttribute != null)
                    {
                        return currentAttribute.Description;
                    }
                }
            }

            //如果没有检测到合适的注释，则用默认名称   
            return enumType.ToString();

        }

        /// <summary>
        /// 根据枚举类型返回类型中的所有值，说明
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <returns>枚举类型字典集</returns>
        public static Dictionary<string, string> GetGetEnumDescriptions(Type currentType)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            //获取字段信息   
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

                //反射自定义属性   
                object[] attributes = field.GetCustomAttributes(
                    typeof(DescriptionAttribute), false);

                foreach (Attribute attribute in attributes)
                {
                    //类型转换找到一个Description，用Description作为成员名称   
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
