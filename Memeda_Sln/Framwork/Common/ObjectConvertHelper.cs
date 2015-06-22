using System;
using System.Collections.Generic;
using System.Reflection;

namespace Framework.Common
{
    /// <summary>
    /// ����ת��������
    /// </summary>
    public class ObjectConvertHelper
    {

        /// <summary>
        /// ��ȿ�¡���󷽷�
        /// </summary>
        /// <typeparam name="T">��������</typeparam>
        /// <param name="source">����</param>
        /// <returns>��¡��Ķ���</returns>
        public static T DeepClone<T>(T source) where T : new()
        {
            return ObjectConvertHelper.ConvertObject<T, T>(source);
        }

        /// <summary>
        /// ת����������Ϊ����һ�����Ͷ���
        /// </summary>
        /// <typeparam name="TSource">��ת����Դ��������</typeparam>
        /// <typeparam name="TResult">ת����Ŀ���������</typeparam>
        /// <param name="source">��ת����Դ����</param>
        /// <returns>ת����Ŀ�����</returns>
        public static TResult ConvertObject<TSource, TResult>(TSource source) where TResult : new()
        {
            TResult result = new TResult();

            Type sourceType = source.GetType();
            Type resultType = result.GetType();

            PropertyInfo[] resultProperties = resultType.GetProperties(
                BindingFlags.Public | BindingFlags.Instance);

            if (resultProperties != null && resultProperties.Length > 0)
            {
                foreach (PropertyInfo resultProperty in resultProperties)
                {
                    if (resultProperty.PropertyType.IsGenericType)
                    {
                        continue;
                    }

                    PropertyInfo sourceProperty = sourceType.GetProperty(resultProperty.Name,
                        BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

                    bool isMatched = sourceProperty != null &&
                            (!sourceProperty.PropertyType.IsGenericType) &&
                            (sourceProperty.PropertyType == resultProperty.PropertyType);

                    if (isMatched)
                    {
                        object currentValue = sourceProperty.GetValue(source, null);
                        resultProperty.SetValue(result, currentValue, null);
                    }

                }
            }
            return result;
        }

        /// <summary>
        /// ת���б����Ϊ����һ���б����
        /// </summary>
        /// <typeparam name="TSource">��ת����Դ��������</typeparam>
        /// <typeparam name="TResult">ת����Ŀ���������</typeparam>
        /// <param name="source">��ת����Դ����</param>
        /// <returns>ת����Ŀ�����</returns>
        public static List<TResult> ConvertList<TSource, TResult>(List<TSource> source) where TResult : new()
        {
            return source.ConvertAll<TResult>(ConvertObject<TSource, TResult>);
        }


        public static String ConvertBoolToTOrF(bool source)
        {
            string result = "F";
            if (source)
            {
                result = "T";
            }
            return result;
        }

        public static int ConvertToWeek<T>(T source)
        {
            DateTime? date = ConvertDateTimeOrNull(source);
            int result = 0;

            if (date.HasValue)
            {
                result = (int)date.Value.DayOfWeek;
                if (result == 0)
                {
                    result = 7;
                }
            }
            return result;
        }

        public static int ConvertToWeek(DayOfWeek week)
        {
            int result = (int)week;

            if (result == 0)
            {
                result = 7;
            }
            return result;
        }

        public static DateTime? ConvertDateTimeOrNull<T>(T source)
        {
            DateTime result;

            if (source == null)
            {
                return null;
            }
            else if (DateTime.TryParse(source.ToString(), out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public static bool ConvertToBool<T>(T source)
        {
            bool result;

            if (source == null)
            {
                return false;
            }
            if (source.ToString().ToUpper().Equals("T"))
            {
                return true;
            }
            if (source.ToString().ToUpper().Equals("F"))
            {
                return false;
            }
            if (Boolean.TryParse(source.ToString().ToLower(), out result))
            {
                return result;
            }
            else
            {
                return false;
            }
        }

        public static string ChangeTelephoneString(string container, string specialString)
        {
            string result = string.Empty;

            if (container.Contains(specialString))
            {
                return string.Empty;
            }
            else
            {
                return container;
            }
        }

        public static string FormatCantonName(string name)
        {
            if (name == null || name.Length == 0)
            {
                return string.Empty;
            }

            int index = name.Length;
            for (int i = 0; i < name.Length; i++)
            {
                if (Char.IsNumber(name[i]) || name[i] == '(' || name[i] == '*' || name[i] == 'N')
                {
                    index = i;
                    break;
                }
            }

            return name.Substring(0, index);
        }

        public static bool IsNumeric(string str)
        {
            if (str == null || str.Length == 0)
                return false;
            System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();
            byte[] bytestr = ascii.GetBytes(str);
            foreach (byte c in bytestr)
            {
                if (c < 48 || c > 57)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="source"></param>
        /// <returns>���</returns>
        public static V ConvertToEnum<T, V>(T source) where V : new()
        {
            Type type = typeof(V);
            V v = new V();

            if (source == null)
            {
                return v;
            }

            try
            {
                string sourceString = source.ToString();
                return (V)Enum.Parse(type, sourceString, true);
            }
            catch
            {
                return v;
            }
        }

        /// <summary>
        /// ת������Ϊint?��
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns>���</returns>
        public static int? ConvertObjectToInt<T>(T source)
        {
            int result;

            if (source == null)
            {
                return null;
            }
            else if (int.TryParse(source.ToString(), out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// ת������Ϊint32��
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns>���</returns>
        public static int ConvertObjectToInt32<T>(T source)
        {
            int result;

            if (source == null)
            {
                return  0;
            }

            else if (int.TryParse(source.ToString(), out result))
            {
                return result;
            }
            else
            {
                return  0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns>���</returns>
        public static byte ConvertToByte<T>(T source)
        {
            byte result;

            if (source == null)
            {
                return default(byte);
            }
            else if (byte.TryParse(source.ToString(), out result))
            {
                return result;
            }
            else
            {
                return default(byte);
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns>���</returns>
        public static int RoundToInt<T>(T source)
        {
            if (source == null)
            {
                return 0;
            }
            int ret;
            if (int.TryParse(source.ToString(), out ret))
            {
                return ret;
            }

            return ParseInt32(Math.Floor(ConvertObjectToDecimalSingle(source) + 0.5M));
        }

        /// <summary>
        /// ת������Ϊdecimal?��
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns>���</returns>
        public static decimal? ConvertObjectToDecimal<T>(T source)
        {
            decimal result;

            if (source == null)
            {
                return null;
            }
            else if (decimal.TryParse(source.ToString(), out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns>���</returns>
        public static decimal ConvertObjectToDecimalSingle<T>(T source)
        {
            decimal? result = ConvertObjectToDecimal(source);

            if (result != null)
            {
                return result.Value;
            }
            else
            {
                return 0M;
            }
        }

        /// <summary>
        /// ת������Ϊint��
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns>���</returns>
        public static int ParseInt32<T>(T source)
        {
            int result;

            if (source == null)
            {
                return 0;
            }
            else if (int.TryParse(String.Format("{0:f0}", source), out result))
            {
                return result;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// ת������ΪDouble��
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns>���</returns>
        public static double ConvertObjectToDouble<T>(T source)
        {
            double result;

            if (source == null)
            {
                return 0;
            }
            else if (double.TryParse(String.Format("{0:f0}", source), out result))
            {
                return result;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns>���</returns>
        public static DateTime ConvertDateTime<T>(T source)
        {
            DateTime result;

            if (source == null)
            {
                return default(DateTime);
            }
            else if (DateTime.TryParse(source.ToString(), out result))
            {
                return result;
            }
            else
            {
                return default(DateTime);
            }
        }

        public static float ConvertToSingle<T>(T source)
        {
            float result;

            if (source == null)
            {
                return 0f;
            }
            else if (float.TryParse(source.ToString(), out result))
            {
                return result;
            }
            else
            {
                return 0f;
            }
        }

        #region Hotel CommonFunc

        /// <summary>
        /// �ѽ��Ϊ0��ת����-1
        /// </summary>
        /// <param name="source"></param>
        /// <returns>���</returns>
        public static Decimal? ChangeZeroToNegative(Decimal? source)
        {
            return source.Equals(0M) ? null : source;
        }

        /// <summary>
        /// ��Ϊ�ղ���Ϊ-1ʱ������font color='red' X /font
        /// Ϊnull��գ�����ConvertToRound�ķ���ֵ 
        /// </summary>
        public static String ConvertToRoundNegative<T>(T source)
        {
            String result = ConvertToRound(source);

            if (!String.IsNullOrEmpty(result))
            {
                return !result.Equals("-1.00") ? result : "X";
            }
            else
            {
                return result;
            }
        }

        /// <summary>
        /// ����Ƿ�Ϊ��������С����
        /// �����������ȥ��������λС��(��:111.00 => 111)
        /// ���򣬱�����λС��(��:111.12 => 111.12)
        /// </summary>
        public static String CheckIsIntAndConvertToInt(String result)
        {
            if (String.IsNullOrEmpty(result))
            {
                return result;
            }
            else if (result.EndsWith("00"))
            {
                return result.Remove(result.Length - 3);
            }
            else
            {
                return result;
            }
        }

        /// <summary>
        /// ��Ϊ��ʱ���������뱣����λС��
        /// Ϊ��ʱ�����ؿ��ַ�
        /// </summary>
        public static String ConvertToRound<T>(T source)
        {
            if (source != null)
            {
                if (!String.Empty.Equals(source.ToString()))
                {
                    decimal result = Math.Round((Convert.ToDecimal(source)), 2, MidpointRounding.AwayFromZero);
                    return result.ToString();
                }
                else { return source.ToString(); }
            }
            else
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// ת���ٷ���ΪС��
        /// �磺15%=>0.15
        /// ΪNull���ʱ�����ؿ��ַ�
        /// </summary>
        public static String ConvertToPercentDecimal<T>(T source)
        {
            if (source != null)
            {
                if (!String.Empty.Equals(source.ToString()))
                {
                    decimal result = Convert.ToDecimal(source) / 100;

                    return result.ToString();
                }
                else { return source.ToString(); }
            }
            else
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// ת���ٷ���
        /// �磺15=>15%
        /// ΪNull���ʱ�����ؿ��ַ�
        /// </summary>
        public static String ConvertToPercentString<T>(T source)
        {
            if (source != null)
            {
                if (!String.Empty.Equals(source.ToString()))
                {
                    decimal result = Convert.ToDecimal(source);

                    if (result == 0)
                    {
                        return "0%";
                    }
                    else
                    {
                        return string.Format("{0:#%}", result / 100);
                    }
                }
                else { return source.ToString(); }
            }
            else
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// Ϊ-1ʱ������Ϊ��
        /// ��Ϊ��ʱ��������λС��
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns>���</returns>
        public static String ConvertToNull<T>(T source)
        {
            decimal? result = ConvertObjectToDecimal(source);

            return result != null ? String.Format("{0:F2}", result) : String.Empty;
        }

        public static String ConvertObjectToString<T>(T source)
        {
            return source == null ? String.Empty : source.ToString();
        }

        public static Boolean IsCheckNullOrEmpty(String source)
        {
            return String.IsNullOrEmpty(source);
        }

        /// <summary>
        /// Check &nbsp;
        /// </summary>
        public static String CovertNBSP(String source)
        {
            return source.ToLower().Equals("&nbsp;") ? String.Empty : source;
        }

        /// <summary>
        /// �����X,��ʾΪ-1
        /// �����"&nbsp;"��ʾΪ��
        /// </summary>
        /// <param name="source"></param>
        /// <returns>���</returns>
        public static String ConvertToEmptyOrSymbolX(String source)
        {
            if (source.ToLower().Equals("x"))
            {
                return "-1";
            }
            else
            {
                return (CovertNBSP(source));
            }
        }

        /// <summary>
        /// ת��������%"
        /// �磺12% => 12
        /// </summary>
        public static String ConvertSymbolPercent(String source)
        {
            if (source.EndsWith("%"))
            {
                source = source.Substring(0, source.Length - 1);
            }
            else
            {
                source = CovertNBSP(source);
            }

            return source;
        }

        public static String ConvertToZero<T>(T source)
        {
            int? result = ConvertObjectToNullableInt(source);

            return result != null ? result.ToString() : String.Empty;
        }

        public static int? ConvertObjectToNullableInt<T>(T source)
        {
            int result;

            if (source.ToString().Equals(String.Empty))
            {
                return null;
            }
            else if (int.TryParse(String.Format("{0:f0}", source), out result))
            {
                return result;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// �����������ʱ0=>7,���򷵻���ֵ
        /// </summary>
        /// <param name="dt">DateTime</param>
        /// <returns>1-7</returns>
        public static Int32 GetDayOfWeek(DateTime dt)
        {
            return (Int32)dt.DayOfWeek == 0 ? 7 : (Int32)dt.DayOfWeek;
        }


        /// <summary>
        /// �õ����ڵ�Ӣ�ĸ�ʽ
        /// </summary>
        public static string FormatDateTime(DateTime date)
        {
            System.Globalization.DateTimeFormatInfo en = System.Globalization.CultureInfo.CreateSpecificCulture("en").DateTimeFormat;
            return date.ToString("MMM-dd-yyyy", en);
        }

        #endregion

        public static int ConvertToFormatInt<T>(T source, int provider)
        {
            try
            {
                return Convert.ToInt32(source.ToString(), provider);
            }
            catch
            {
                return 0;
            }
        }

        public static string ConvertToTrimString<T>(T source)
        {
            string result = source.ToString();

            if (!String.IsNullOrEmpty(result))
            {
                result = result.Trim();
            }
            return result;
        }

        /// <summary>
        /// �ַ����ָ�������
        /// </summary>
        /// <param name="current">�ָ��ַ���</param>
        /// <param name="separator">�ָ���</param>
        /// <returns>����</returns>
        public static string[] SplitStringToArray(string current, char separator)
        {
            if (string.IsNullOrEmpty(current))
            {
                return null;
            }

            return current.Split(separator);
        }
    }
}
