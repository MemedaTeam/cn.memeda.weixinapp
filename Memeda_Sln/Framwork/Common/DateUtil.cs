using System;
using System.Globalization;
using Framework.Log;

namespace Framework.Common
{
    /// <summary>
    /// 日期字符串工具
    /// </summary>
    public static class DateUtil
    {
        /// <summary>
        /// 转换字符串为DateTime类型(支持ddMMyy|ddMMyyyy|yyyyMMdd|yyyyMMddHHmmss|dd-MM-yy|dd-MM-yyyy|yyyy-MM-dd)
        /// </summary>
        /// <param name="text">要转换的字符串</param>
        /// <param name="textFormat">要转换的字符串的格式</param>
        /// <returns></returns>
        public static DateTime ToDateTime(string text, string textFormat)
        {
            //支持ddMMyy|ddMMyyyy|yyyyMMdd|yyyyMMddHHmmss|dd-MM-yy|dd-MM-yyyy|yyyy-MM-dd\
            DateTime result; int day, month, year;
            try
            {
                switch (textFormat)
                {
                    case "ddMMyy":
                        if (text.Trim().Length != 6) throw new Exception("要转换的数据的格式与指定的格式长度不一致！");
                        day = int.Parse(text.Substring(0, 2));
                        month = int.Parse(text.Substring(2, 2));
                        year = int.Parse(text.Substring(4, 2));
                        if (year > 50) year = 1900 + year;
                        else year = 2000 + year;
                        result = new DateTime(year, month, day);
                        break;
                    case "ddMMyyyy":
                        if (text.Trim().Length != 8) throw new Exception("要转换的数据的格式与指定的格式长度不一致！");
                        day = int.Parse(text.Substring(0, 2));
                        month = int.Parse(text.Substring(2, 2));
                        year = int.Parse(text.Substring(4, 4));
                        result = new DateTime(year, month, day);
                        break;
                    case "yyyyMMdd":
                        if (text.Trim().Length != 8) throw new Exception("要转换的数据的格式与指定的格式长度不一致！");
                        result = DateTime.Parse(text.Trim().Insert(6, "-").Insert(4, "-"));
                        break;
                    case "yyyyMMddHHmmss":
                        if (text.Trim().Length != 14) throw new Exception("要转换的数据的格式与指定的格式长度不一致！");
                        result = DateTime.Parse(text.Trim().Insert(12, ":").Insert(10, ":").Insert(8, " ").Insert(6, "-").Insert(4, "-"));
                        break;
                    case "dd-MM-yy":
                        if (text.Trim().Length != 8) throw new Exception("要转换的数据的格式与指定的格式长度不一致！");
                        text = text.Replace("-", "");
                        day = int.Parse(text.Substring(0, 2));
                        month = int.Parse(text.Substring(2, 2));
                        year = int.Parse(text.Substring(4, 2));
                        if (year > 50) year = 1900 + year;
                        else year = 2000 + year;
                        result = new DateTime(year, month, day);
                        break;
                    case "dd-MM-yyyy":
                        if (text.Trim().Length != 10) throw new Exception("要转换的数据的格式与指定的格式长度不一致！");
                        text = text.Replace("-", "");
                        day = int.Parse(text.Substring(0, 2));
                        month = int.Parse(text.Substring(2, 2));
                        year = int.Parse(text.Substring(4, 4));
                        result = new DateTime(year, month, day);
                        break;
                    case "yyyy-MM-dd":
                        if (text.Trim().Length != 10) throw new Exception("要转换的数据的格式与指定的格式长度不一致！");
                        result = DateTime.Parse(text.Trim());
                        break;
                    default:
                        result = DateTime.Parse(text.Trim());
                        break;
                }
            }
            catch
            {
                throw new Exception("不能转换为指定的格式！");
            }

            return result;
        }

        /// <summary>
        /// 转换字符串为DateTime类型(支持ddMMyy|ddMMyyyy|yyyyMMdd|yyyyMMddHHmmss|dd-MM-yy|dd-MM-yyyy|yyyy-MM-dd)
        /// </summary>
        /// <param name="text">要转换的字符串</param>
        /// <param name="textFormat">要转换的字符串的格式</param>
        /// <returns></returns>
        public static DateTime TryParseDateTime(string text, string textFormat)
        {
            if (string.IsNullOrEmpty(text))
            {
                return DateTime.MinValue;
            }

            try
            {
                return DateUtil.ToDateTime(text, textFormat);
            }
            catch(Exception ex)
            {
                return DateTime.MinValue;
            }

        }
        /// <summary>
        /// 获取信用卡有效期
        /// </summary>
        /// <param name="cardValidity">信用卡有效期字符串，格式为yyyyMM;比如201208</param>
        /// <returns>结果</returns>
        public static DateTime GetCreditCardExpirationDate(string cardValidity)
        {
            if (string.IsNullOrEmpty(cardValidity) || cardValidity.Length != 6)
            {
                return DateTime.MinValue;
            }

            string year = cardValidity.Substring(0, 4);
            string month = cardValidity.Substring(4, cardValidity.Length - 4);

            try
            {
                DateTime result = DateTime.Parse(year + "-" + month);
                return result.AddMonths(1).AddDays(-1);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return DateTime.MinValue;
            }
        }
    }
}
