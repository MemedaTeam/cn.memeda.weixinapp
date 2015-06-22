using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Framework.Common
{
    /// <summary>
    /// 字符串工具类
    /// </summary>
    public class StringUtil
    {
        /// <summary>
        /// 重定义数组
        /// </summary>
        /// <param name="origArray">原来的数组长度</param>
        /// <param name="desiredsize">新的宽度</param>
        /// <returns></returns>
        public static Array Redim(Array origArray, Int32 desiredsize)
        {
            //确定每个元素类型           
            Type t = origArray.GetType().GetElementType();
            //创建一个含有期望元素个数的新数组       
            //新数组的类型必须匹配原数组的类型       
            Array newArray = Array.CreateInstance(t, desiredsize);
            //将原数组中的元素拷贝到新数组中       
            Array.Copy(origArray, 0, newArray, 0, Math.Min(origArray.Length, desiredsize));
            return newArray;
        }

        /// <summary>
        /// 判断是否是长整型
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsLong(string s)
        {
            long l;
            return long.TryParse(s, out l);
        }

        /// <summary>
        /// 判断是否是中文
        /// </summary>
        /// <param name="CString"></param>
        /// <returns></returns>
        public static bool IsChina(string CString)
        {
            bool BoolValue = false;
            for (int i = 0; i < CString.Length; i++)
            {
                if (Convert.ToInt32(Convert.ToChar(CString.Substring(i, 1))) < Convert.ToInt32(Convert.ToChar(128)))
                {
                    BoolValue = false;
                    break;
                }
                else
                {
                    return BoolValue = true;
                    break;
                }
            }
            return BoolValue;
        }

        /// <summary>
        /// 获取字符串长度
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static int GetStrLen(object t)
        {
            return System.Text.Encoding.Default.GetByteCount(t.ToString());
        }

        /// <summary>
        /// 缩短字符串
        /// </summary>
        /// <param name="tt">字符串</param>
        /// <param name="len">长度（1个汉字2个长度）</param>
        /// <returns></returns>
        public static string TrimStr(object tt, int len)
        {
            string Txt = tt.ToString();
            //格式化字符串长度，超出部分显示省略号,区分汉字跟字母。汉字2个字节，字母数字一个字节 
            if (System.Text.Encoding.Default.GetByteCount(Txt) <= len)
            {
                //如果长度比需要的长度n小,返回原字符串 
                return Txt;
            }
            else
            {
                string temp = string.Empty;
                int t = 0;
                foreach (char ch in Txt)
                {
                    if (t > len - 3) break;
                    if ((int)ch > 127)
                    {
                        t = t + 2;
                    }
                    else
                    {
                        t++;
                    }
                    temp = temp + ch;
                }
                return temp + "...";
            }
        }

        /// <summary>
        /// 过滤危险HTML
        /// </summary>
        /// <param name="MainTxt"></param>
        /// <returns></returns>
        private static string FormatHTML(string MainTxt)
        {
            //iframe,embed,script,bgsound,object,applet 
            MainTxt = Regex.Replace(MainTxt, "iframe", "&#105;frame", RegexOptions.IgnoreCase);
            MainTxt = Regex.Replace(MainTxt, "embed", "&#101;mbed", RegexOptions.IgnoreCase);
            MainTxt = Regex.Replace(MainTxt, "script", "&#115;cript", RegexOptions.IgnoreCase);
            MainTxt = Regex.Replace(MainTxt, "bgsound", "&#98;gsound", RegexOptions.IgnoreCase);
            MainTxt = Regex.Replace(MainTxt, "object", "&#111;bject", RegexOptions.IgnoreCase);
            MainTxt = Regex.Replace(MainTxt, "applet", "&#97;pplet", RegexOptions.IgnoreCase);
            return MainTxt;
            //MainTxt = Regex.Replace(MainTxt, "<&#115;cript src=""http://v.qiak.com/votejS.aspx?vid=(\d+)"" type=""text/javascript"" ></&#115;cript>", "<script src=""http://v.qiak.com/VoteJS.aspx?VID=$1"" type=""text/javascript"" ></script>", RegexOptions.IgnoreCase) 
        }

        /// <summary>
        /// 格式化UBB
        /// </summary>
        /// <param name="MainTxt"></param>
        /// <returns></returns>
        private static string FormatUBB(string MainTxt)
        {
            MainTxt = Regex.Replace(MainTxt, @"\[vote=(?<x>[^\]]*)\]", "<script src=\"VoteJS.aspx?VID=$1\" type=\"text/javascript\" ></script>", RegexOptions.IgnoreCase);
            MainTxt = Regex.Replace(MainTxt, @"\[url=(?<x>[^\]]*)\](?<y>[^\]]*)\[/url\]", "<a href=\"$1\" target=\"_blank\">$2</a>", RegexOptions.IgnoreCase);
            MainTxt = Regex.Replace(MainTxt, @"\[url\](?<x>[^\]]*)\[/url\]", "<a href=\"$1\" target=\"_blank\">$1</a>", RegexOptions.IgnoreCase);
            MainTxt = Regex.Replace(MainTxt, @"\[email=(?<x>[^\]]*)\](?<y>[^\]]*)\[/email\]", "<a href=\"mailto:$1\">$2</a>", RegexOptions.IgnoreCase);
            MainTxt = Regex.Replace(MainTxt, @"\[email\](?<x>[^\]]*)\[/email\]", "<a href=\"mailto:$1\">$1</a>", RegexOptions.IgnoreCase);
            MainTxt = Regex.Replace(MainTxt, @"\[img](?<x>[^\]]*)\[/img]", "<img src=\"$1\" border=\"0\" alt=\"\" />", RegexOptions.IgnoreCase);
            MainTxt = Regex.Replace(MainTxt, @"\[color=(?<x>[^\]]*)\](?<y>[^\]]*)\[/color\]", "<span style=\"color:$1;\">$2</span>", RegexOptions.IgnoreCase);
            MainTxt = Regex.Replace(MainTxt, @"\[size=1\](?<x>[^\]]*)\[/size\]", "<span class=\"size1\">$1</span>", RegexOptions.IgnoreCase);
            MainTxt = Regex.Replace(MainTxt, @"\[size=2\](?<x>[^\]]*)\[/size\]", "<span class=\"size2\">$1</span>", RegexOptions.IgnoreCase);
            MainTxt = Regex.Replace(MainTxt, @"\[size=3\](?<x>[^\]]*)\[/size\]", "<span class=\"size3\">$1</span>", RegexOptions.IgnoreCase);
            MainTxt = Regex.Replace(MainTxt, @"\[b\](?<x>[^\]]*)\[/b\]", "<b>$1</b>", RegexOptions.IgnoreCase);
            MainTxt = Regex.Replace(MainTxt, @"\[i\](?<x>[^\]]*)\[/i\]", "<i>$1</i>", RegexOptions.IgnoreCase);
            MainTxt = Regex.Replace(MainTxt, @"\[u\](?<x>[^\]]*)\[/u\]", "<u>$1</u>", RegexOptions.IgnoreCase);
            MainTxt = Regex.Replace(MainTxt, @"\[left\](?<x>[^\]]*)\[/left\]", "<span class=\"align-left\">$1</span>", RegexOptions.IgnoreCase);
            MainTxt = Regex.Replace(MainTxt, @"\[right\](?<x>[^\]]*)\[/right\]", "<span class=\"align-right\">$1</span>", RegexOptions.IgnoreCase);
            MainTxt = Regex.Replace(MainTxt, @"\[center\](?<x>[^\]]*)\[/center\]", "<span class=\"align-center\">$1</span>", RegexOptions.IgnoreCase);
            MainTxt = Regex.Replace(MainTxt, @"\[s\](?<x>[^\]]*)\[/s\]", "<span class=\"strike\">$1</span>", RegexOptions.IgnoreCase);
            MainTxt = Regex.Replace(MainTxt, @"\[sup\](?<x>[^\]]*)\[/sup\]", "<span class=\"super\">$1</span>", RegexOptions.IgnoreCase);
            MainTxt = Regex.Replace(MainTxt, @"\[sub\](?<x>[^\]]*)\[/sub\]", "<span class=\"sub\">$1</span>", RegexOptions.IgnoreCase);
            MainTxt = Regex.Replace(MainTxt, @"\[justify\](?<x>[^\]]*)\[/justify\]", "<span class=\"align-justify\">$1</span>", RegexOptions.IgnoreCase);
            MainTxt = Regex.Replace(MainTxt, @"\[code\](?<x>[^\]]*)\[/code\]", "<code>$1</code>", RegexOptions.IgnoreCase);
            MainTxt = Regex.Replace(MainTxt, @"\[quote=(?<x>[^\]]*)\](?<y>[^\]]*)\[/quote\]", "<p class=\"cite\">引用 <cite>$1</cite> 的话</p><blockquote>$2</blockquote>", RegexOptions.IgnoreCase);
            MainTxt = Regex.Replace(MainTxt, @"\[quote\](?<x>[^\]]*)\[/quote\]", "<p class=\"cite\">引用</p><blockquote>$1</blockquote>", RegexOptions.IgnoreCase);
            return MainTxt;
        }

        /// <summary>
        /// 格式化纯文本
        /// </summary>
        /// <param name="MainTxt"></param>
        /// <returns></returns>
        private static string FormatTXT(string MainTxt)
        {
            MainTxt = Regex.Replace(MainTxt, @"((\r\n)|\r|\n)", "<br />", RegexOptions.IgnoreCase);
            return MainTxt;
        }

        /// <summary>
        /// 格式化HTML
        /// </summary>
        /// <param name="MainTxt"></param>
        /// <returns></returns>
        private static string FilterHTML(string MainTxt)
        {
            MainTxt = Regex.Replace(MainTxt, @"<", "&lt;", RegexOptions.IgnoreCase);
            MainTxt = Regex.Replace(MainTxt, @">", "&gt;", RegexOptions.IgnoreCase);
            return MainTxt;
        }

        /// <summary>
        /// 格式化UBB
        /// </summary>
        /// <param name="MainTxt"></param>
        /// <returns></returns>
        private static string FilterUBB(string MainTxt)
        {
            MainTxt = Regex.Replace(MainTxt, @"\[(.|\n|)*?\]", "", RegexOptions.IgnoreCase);
            return MainTxt;
        }

        /// <summary>
        /// 删除HTML
        /// </summary>
        /// <param name="MainTxt"></param>
        /// <returns></returns>
        private static string DELHTML(string MainTxt)
        {
            string pattern = "";
            //替换不需要的页面要素-注释部分
            pattern = @"<!--[\s\S]*?-->";
            //替换不需要的页面要素-样式部分
            pattern += @"|<style[\s\S]*?</style\s*?>";
            //替换不需要的页面要素-脚本部分
            pattern += @"|<script[\s\S]*?</script\s*?>";
            MainTxt = Regex.Replace(MainTxt, pattern, "", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);
            MainTxt = Regex.Replace(MainTxt, @"<\s*br[/\s]*>", "\r\n", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);
            MainTxt = Regex.Replace(MainTxt, @"<[a-zA-Z!/][^>]*>", "", RegexOptions.IgnoreCase);
            MainTxt = Regex.Replace(MainTxt, @"&nbsp;", " ", RegexOptions.IgnoreCase);
            return MainTxt;
        }

        /// <summary>
        /// 格式化时间到分
        /// </summary>
        /// <param name="tt">时间字符串</param>
        /// <returns></returns>
        public static string FTime(object tt)
        {
            try
            {
                DateTime sTime = DateTime.Parse(tt.ToString());
                return sTime.ToString("yyyy-MM-dd HH:mm");
            }
            catch
            {
                return DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            }
        }

        /// <summary>
        /// 格式化时间
        /// </summary>
        /// <param name="tt"></param>
        /// <returns></returns>
        public static string FTimeDetail(object tt)
        {
            try
            {
                DateTime sTime = DateTime.Parse(tt.ToString());
                string Str = sTime.ToString("yyyy年M月d日 H:m");
                switch (sTime.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        Str += "(一)";
                        break;
                    case DayOfWeek.Tuesday:
                        Str += "(二)";
                        break;
                    case DayOfWeek.Wednesday:
                        Str += "(三)";
                        break;
                    case DayOfWeek.Thursday:
                        Str += "(四)";
                        break;
                    case DayOfWeek.Friday:
                        Str += "(五)";
                        break;
                    case DayOfWeek.Saturday:
                        Str += "(六)";
                        break;
                }
                return Str;
            }
            catch
            {
                return DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            }
        }

        /// <summary>
        /// 格式化时间到时间间隔
        /// </summary>
        /// <param name="tt">时间字符串</param>
        /// <returns></returns>
        public static string FTimeInterval(object tt)
        {
            try
            {
                DateTime sTime = DateTime.Parse(tt.ToString());
                DateTime nowTime = DateTime.Now;
                if (sTime.Year == nowTime.Year && sTime.Month == nowTime.Month && sTime.Day == nowTime.Day)
                {
                    return "今天" + sTime.ToString("H:mm");
                }
                else if (sTime.AddDays(1).Year == nowTime.Year && sTime.AddDays(1).Month == nowTime.Month && sTime.AddDays(1).Day == nowTime.Day)
                {
                    return "昨天" + sTime.ToString("H:mm");
                }
                else if (sTime.AddDays(2).Year == nowTime.Year && sTime.AddDays(2).Month == nowTime.Month && sTime.AddDays(2).Day == nowTime.Day)
                {
                    return "前天" + sTime.ToString("H:mm");
                }
                else if (sTime.Year == nowTime.Year)
                {
                    return sTime.ToString("M月d日");
                }
                else
                {
                    return sTime.ToString("yyyy-M-d");
                }
                return sTime.ToString("yyyy-MM-dd HH:mm");
            }
            catch
            {
                return DateTime.Now.ToString("H:m");
            }
        }

        /// <summary>
        /// 格式化时间
        /// </summary>
        /// <param name="tt"></param>
        /// <returns></returns>
        public static string FTimeRelate(object tt)
        {
            try
            {
                DateTime sTime = DateTime.Parse(tt.ToString());
                DateTime nowTime = DateTime.Now;
                TimeSpan time = nowTime.Subtract(sTime);
                if (time.TotalSeconds < 60)
                {
                    return ((int)time.TotalSeconds) + "秒前";
                }
                else if (time.TotalMinutes < 60)
                {
                    return ((int)time.TotalMinutes) + "分钟前";
                }
                else if (time.TotalHours < 24)
                {
                    return ((int)time.TotalHours) + "小时前";
                }
                else if (time.TotalDays < 30)
                {
                    return ((int)time.TotalDays) + "天前";
                }
                else
                {
                    return sTime.ToString("yyyy-M-d");
                }
            }
            catch
            {
                return DateTime.Now.ToString("H:m");
            }
        }

        /// <summary>
        /// 格式化时间只显示小时和分
        /// </summary>
        /// <param name="tt">时间字符串</param>
        /// <returns></returns>
        public static string FTimeHour(object tt)
        {
            try
            {
                DateTime sTime = DateTime.Parse(tt.ToString());
                return sTime.ToString("HH:mm");
            }
            catch
            {
                return DateTime.Now.ToString("HH:mm");
            }
        }

        /// <summary>
        /// 格式化时间到日
        /// </summary>
        /// <param name="tt">时间字符串</param>
        /// <returns></returns>
        public static string FTimeDate(object tt)
        {
            try
            {
                DateTime sTime = DateTime.Parse(tt.ToString());
                return sTime.ToString("yyyy-MM-dd");
            }
            catch
            {
                return DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        /// <summary>
        /// 获取QUERYSTRING的INT
        /// </summary>
        /// <param name="i">字符串</param>
        /// <returns></returns>
        public static int GetQueryInt(object i)
        {
            if (i != null)
            {
                int ii;
                try
                {
                    ii = int.Parse(i.ToString());
                }
                catch (Exception ex)
                {
                    ii = 0;
                }
                return ii;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 获取QUERYSTRING的Long
        /// </summary>
        /// <param name="i">字符串</param>
        /// <returns></returns>
        public static long GetQueryLong(object i)
        {
            if (i != null)
            {
                long ii;
                try
                {
                    ii = long.Parse(i.ToString());
                }
                catch (Exception ex)
                {
                    ii = 0;
                }
                return ii;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 格式化成Decimal类型
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Decimal ParseDecimal(object s)
        {
            try
            {
                return Decimal.Parse(s.ToString());
            }
            catch
            {
                return (Decimal)0.0;
            }
        }

        /// <summary>
        /// 获取QUERYSTRING的BOOL值，如果不为0即为TRUE
        /// </summary>
        /// <param name="b">字符串</param>
        /// <returns></returns>
        public static bool GetQueryBool(object b)
        {
            if (b != null)
            {
                if (b.ToString() != "0" && b.ToString() != "")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取QUERYSTRING的STRING
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns></returns>
        public static string GetQueryStr(object s)
        {
            if (s != null)
            {
                return s.ToString();
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 判断是否是日期型字串
        /// </summary>
        /// <param name="s">日期字符串</param>
        /// <returns></returns>
        public static bool IsDateTime(string s)
        {
            try { System.DateTime.Parse(s); }
            catch { return false; }
            return true;
        }

        /// <summary>
        /// 截取文字限长
        /// </summary>
        /// <param name="tt">文本</param>
        /// <param name="len">截取长度</param>
        /// <returns>截取后文本</returns>
        public static string LimitStr(object tt, int len)
        {
            string Txt = tt.ToString();
            if (Txt.Length <= len)
            {
                return Txt;
            }
            else
            {
                return Txt.Substring(0, len);
            }
        }

        /// <summary>
        /// 清理连续的HTML空格和换行标记
        /// </summary>
        /// <param name="webstring"></param>
        /// <returns></returns>
        public static string ClearSpace(string webstring)
        {
            //模式
            string pattern = "";
            //再次替换多个回车换行符，清理上一步产生的连续空格
            pattern = @"(\s*<br />\s*){2,}";
            webstring = Regex.Replace(webstring, pattern, "<br />\r\n", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);
            //清理连续的HTML空格标记
            pattern = @"&nbsp;{2,}";
            webstring = Regex.Replace(webstring, pattern, " ", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);
            return webstring;
        }

        /// <summary>
        /// 清理邮件文中多余的空格和回车，紧凑显示
        /// </summary>
        /// <param name="TextContent">内容</param>
        /// <returns>清理后的文字</returns>
        public static string GetCleanTextContent(string TextContent)
        {
            string ReturnStr;
            string pattern = @"\s+\n\s+";
            ReturnStr = Regex.Replace(TextContent, pattern, "\r\n", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);
            return ReturnStr;
        }
        /// <summary>
        /// 获得时间的称呼，用来打招呼
        /// </summary>
        /// <returns></returns>
        public static string CallTimeName()
        {
            int Hour = DateTime.Now.Hour;
            if (Hour < 6) { return "凌晨"; }
            else if (Hour < 9) { return "早上"; }
            else if (Hour < 12) { return "上午"; }
            else if (Hour < 14) { return "中午"; }
            else if (Hour < 17) { return "下午"; }
            else if (Hour < 19) { return "傍晚"; }
            else if (Hour < 22) { return "晚上"; }
            else { return "夜里"; }
        }

        /// <summary>
        /// 有长字符（例如中文）
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns></returns>
        public static bool HasWideString(string s)
        {
            if (System.Text.Encoding.Default.GetByteCount(s) > s.Length)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 取左边的指定字数的文字（带省略号）
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="Len">长度</param>
        /// <returns></returns>
        public static string StrLeft(string s, int Len)
        {
            if (s.Length <= Len)
            {
                return s;
            }
            else
            {
                return s.Substring(0, Len - 3) + "...";
            }
        }


        /// <summary>
        /// 截取右边的指定字数的文字（带省略号）
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="Len">长度</param>
        /// <returns></returns>
        public static string StrRight(string s, int Len)
        {
            if (s.Length <= Len)
            {
                return s;
            }
            else
            {
                return "..." + s.Substring(s.Length - Len, Len);
            }
        }

        /// <summary>
        /// 取左边的指定字数的文字
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="Len">长度</param>
        /// <returns></returns>
        public static string Left(string s, int Len)
        {
            if (s.Length <= Len)
            {
                return s;
            }
            else
            {
                return s.Substring(0, Len);
            }
        }

        /// <summary>
        /// 截取右边的指定字数的文字
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="Len">长度</param>
        /// <returns></returns>
        public static string Right(string s, int Len)
        {
            if (s.Length <= Len)
            {
                return s;
            }
            else
            {
                return s.Substring(s.Length - Len, Len);
            }
        }

        /// <summary>
        /// 构造干净的HTML内容，清除页面中的注释等部分，清楚页面中过多的空白，区分回车和不正常回车
        /// </summary>
        /// <param name="webstring">HTML内容</param>
        /// <returns></returns>
        public static string GetCleanHtml(string webstring)
        {
            //模式
            string pattern = "";
            //替换不需要的页面要素-注释部分
            pattern = @"<!--[\s\S]*?-->";
            //替换不需要的页面要素-样式部分
            pattern += @"|<style[\s\S]*?</style(?>\s*)?>";
            //替换不需要的页面要素-脚本部分
            pattern += @"|<script[\s\S]*?</script(?>\s*)?>";
            //替换不需要的页面要素-选择项部分
            pattern += @"|<select[\s\S]*?</select(?>\s*)?>";
            //替换隐藏的DIV区域或SPAN区域
            pattern += @"|<div[^>]*?style\s*=\s*[^>]*?display:none[^>]*?>[\s\S]*?</div>";
            pattern += @"|<span[^>]*?style\s*=\s*[^>]*?display:none[^>]*?>[\s\S]*?</span>";
            //替换不需要的页面要素-标题部分（影响正文阅读）
            pattern += @"|<title[\s\S]*?</title(?>\s*)?>";
            webstring = Regex.Replace(webstring, pattern, "", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);

            //替换一个或多个空字符,顺便清除所有换行符
            pattern = @"(?>\s{1,})";
            webstring = Regex.Replace(webstring, pattern, " ", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);
            //按照网页定义生成回车换行标记
            pattern = @"(?:<(?>br(?>[^>]*)|/tr(?>\s*)|/p(?>\s*)|/div(?>\s*)|/ul(?>\s*))>)+";
            webstring = Regex.Replace(webstring, pattern, "\r\n", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);
            //清理除了A/IMG/BASE/FRAME/IFRAME/AREA/BGSOUND/META之外的HTML标签
            pattern = @"<(?!img|a|/a|frame|iframe|area|[^a-zA-Z\!/])[^>]*?>";
            webstring = Regex.Replace(webstring, pattern, "", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);
            //清理连续的HTML空格标记,还是用传统的字符串替换快
            webstring = webstring.Replace("&nbsp;", " ");
            //再次替换多个回车换行符，清理上一步产生的连续空格
            pattern = @"(?>\s{2,})";
            webstring = Regex.Replace(webstring, pattern, "\r\n", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);
            //pattern = @"&nbsp;{1,}";
            ////webstring = Regex.Replace(webstring, pattern, "&nbsp;", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);
            //webstring = Regex.Replace(webstring, pattern, " ", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);
            //导航栏判定标记
            //pattern = @"(<a\s[^>]+?>[^<>]{0,3}(<img\s[^>]+?>){0,2}[^<>]{0,3}</a\s*>[^<>]{0,4}(<img\s[^>]+?>){0,2}[^<>]{0,4}){5,}(<a\s[^>]+?>[^<>]{0,3}(<img\s[^>]+?>){0,2}[^<>]{0,3}</a\s*>[^<>])";
            pattern = @"(?:<a\s(?>[^>]+)>(?>[^<]{0,3})(?>(?:<img\s(?>[^>]+)>){0,2})(?>[^<]{0,3})</a(?>\s*)>(?>[^<]{0,4})(?>(?:<img\s(?>[^>]+)>){0,2})(?>[^<]{0,4})){5,}(?><a\s(?>[^>]+)>(?>[^<]{0,3})(?>(?:<img\s(?>[^>]+)>){0,2})(?>[^<]{0,3})</a(?>\s*)>)";
            webstring = Regex.Replace(webstring, pattern, "<NaviStart>$0<NaviEnd>", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);

            return webstring;
        }

        /// <summary>
        /// 构造干净的HTML文本内容，清除页面中的注释等部分，清楚页面中过多的空白，区分回车和不正常回车
        /// </summary>
        /// <param name="webstring">HTML内容</param>
        /// <returns></returns>
        public static string GetCleanHtmlTxt(string webstring)
        {
            //模式
            string pattern = "";
            //替换不需要的页面要素-注释部分
            pattern = @"<!--[\s\S]*?-->";
            //替换不需要的页面要素-样式部分
            pattern += @"|<style[\s\S]*?</style(?>\s*)?>";
            //替换不需要的页面要素-脚本部分
            pattern += @"|<script[\s\S]*?</script(?>\s*)?>";
            //替换不需要的页面要素-选择项部分
            pattern += @"|<select[\s\S]*?</select(?>\s*)?>";
            //替换不需要的TEXTBOX要素部分
            pattern += @"|<textarea[\s\S]*?</textarea(?>\s*)?>";
            //替换隐藏的DIV区域或SPAN区域
            pattern += @"|<div[^>]*?style\s*=\s*[^>]*?display:none[^>]*?>[\s\S]*?</div>";
            pattern += @"|<span[^>]*?style\s*=\s*[^>]*?display:none[^>]*?>[\s\S]*?</span>";
            //替换不需要的页面要素-标题部分（影响正文阅读）
            //pattern += @"|<title[\s\S]*?</title(?>\s*)?>";
            webstring = Regex.Replace(webstring, pattern, "", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);

            //替换一个或多个空字符,顺便清除所有换行符
            pattern = @"(?>\s{1,})";
            webstring = Regex.Replace(webstring, pattern, " ", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);
            //按照网页定义生成回车换行标记
            pattern = @"(?:<(?>br(?>[^>]*)|/tr(?>\s*)|/p(?>\s*)|/div(?>\s*)|/ul(?>\s*))>)+";
            webstring = Regex.Replace(webstring, pattern, "\r\n", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);
            //清理剩下的HTML标签
            pattern = @"<[a-zA-Z!/][^>]*>";
            webstring = Regex.Replace(webstring, pattern, "", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);

            try
            {
                webstring = System.Web.HttpUtility.HtmlDecode(webstring);
            }
            catch (Exception)
            {

            }

            //清理连续的HTML空格标记,还是用传统的字符串替换快
            //webstring = webstring.Replace("&nbsp;", " ");
            //再次替换多个回车换行符，清理上一步产生的连续空格
            pattern = @"(?>\s{2,})";
            webstring = Regex.Replace(webstring, pattern, "\r\n", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);


            return webstring;
        }

        /// <summary>
        /// 构造干净的HTML文本内容（不带A标签内容），清除页面中的注释等部分，清楚页面中过多的空白，区分回车和不正常回车
        /// </summary>
        /// <param name="webstring">HTML内容</param>
        /// <returns></returns>
        public static string GetCleanHtmlTxtNoA(string webstring)
        {
            //模式
            string pattern = "";
            //替换不需要的页面要素-注释部分
            pattern = @"<!--[\s\S]*?-->";
            //替换不需要的页面要素-样式部分
            pattern += @"|<style[\s\S]*?</style(?>\s*)?>";
            //替换不需要的页面要素-脚本部分
            pattern += @"|<script[\s\S]*?</script(?>\s*)?>";
            //替换不需要的页面要素-选择项部分
            pattern += @"|<select[\s\S]*?</select(?>\s*)?>";
            //替换不需要的TEXTBOX要素部分
            pattern += @"|<textarea[\s\S]*?</textarea(?>\s*)?>";
            //替换不需要的A要素部分
            pattern += @"|<a[\s\S]*?</a(?>\s*)?>";
            //替换隐藏的DIV区域或SPAN区域
            pattern += @"|<div[^>]*?style\s*=\s*[^>]*?display:none[^>]*?>[\s\S]*?</div>";
            pattern += @"|<span[^>]*?style\s*=\s*[^>]*?display:none[^>]*?>[\s\S]*?</span>";
            //替换不需要的页面要素-标题部分（影响正文阅读）
            //pattern += @"|<title[\s\S]*?</title(?>\s*)?>";
            webstring = Regex.Replace(webstring, pattern, "", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);

            //替换一个或多个空字符,顺便清除所有换行符
            pattern = @"(?>\s{1,})";
            webstring = Regex.Replace(webstring, pattern, " ", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);
            //按照网页定义生成回车换行标记
            pattern = @"(?:<(?>br(?>[^>]*)|/tr(?>\s*)|/p(?>\s*)|/div(?>\s*)|/ul(?>\s*))>)+";
            webstring = Regex.Replace(webstring, pattern, "\r\n", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);
            //清理剩下的HTML标签
            pattern = @"<[a-zA-Z!/][^>]*>";
            webstring = Regex.Replace(webstring, pattern, "", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);

            try
            {
                webstring = System.Web.HttpUtility.HtmlDecode(webstring);
            }
            catch (Exception)
            {

            }

            //清理连续的HTML空格标记,还是用传统的字符串替换快
            //webstring = webstring.Replace("&nbsp;", " ");
            //再次替换多个回车换行符，清理上一步产生的连续空格
            pattern = @"(?>\s{2,})";
            webstring = Regex.Replace(webstring, pattern, "\r\n", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);


            return webstring;
        }
        /// <summary>
        /// 替换XML中导致异常的字符
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static String ReplaceInvaldateCharacter(String text)
        {
            if (text != null)
            {
                char[] data = text.ToCharArray();
                for (int i = 0; i < data.Length; i++)
                {
                    if (!IsXMLCharacter(data[i]))
                    {
                        data[i] = ' ';
                    }
                }
                return new String(data);
            }
            return text;
        }

        /// <summary>
        /// 检查字符是否为合法的xml字符。 
        ///  XML规范中规定了允许的字符范围(http://www.w3.org/TR/REC-xml#dt-character): 
        /// Char ::= #x9 | #xA | #xD | [#x20-#xD7FF] | [#xE000-#xFFFD] | [#x10000-#x10FFFF] 
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private static bool IsXMLCharacter(int c)
        {
            if (c <= 0xD7FF)
            {
                if (c >= 0x20)
                {
                    return true;
                }
                else
                {
                    return c == '\n' || c == '\r' || c == '\t';
                }
            }
            return (c >= 0xE000 && c <= 0xFFFD) || (c >= 0x10000 && c <= 0x10FFFF);
        }

        /// <summary>
        /// 标准的HTMLDECODE可能在碰到类似 &2342341; 而产生溢出错误，需要创造一个安全的方法
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string HtmlDecode(string s)
        {
            try
            {
                return System.Web.HttpUtility.HtmlDecode(s);
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// 将日期格式的字符串转换成日期类型(DateTime)
        /// </summary>
        /// <param name="timeString"></param>
        /// <returns></returns>
        public static DateTime TimeStringToDateTime(string timeString)
        {
            try
            {
                if (!string.IsNullOrEmpty(timeString))
                {
                    return Convert.ToDateTime(timeString);
                }
                else
                {
                    return DateTime.Now;
                }
            }
            catch (Exception)
            {
                return DateTime.Now;
                throw;
            }
        }
        /// <summary>
        /// 转换团购API日期格式转换（描述转日期）
        /// </summary>
        /// <param name="date"></param>
        /// <param name="timeStr"></param>
        /// <returns></returns>
        public static DateTime ReturnDate(long date, string timeStr)
        {
            DateTime dtStr = new DateTime();
            try
            {
                if (date > 0)
                {
                    DateTime dt = new DateTime(1970, 1, 1, 8, 00, 00);
                    dtStr = dt.AddSeconds(date);
                }
                else if (!string.IsNullOrEmpty(timeStr))
                {
                    dtStr = TimeStringToDateTime(timeStr);
                }
                else
                {
                    dtStr = DateTime.Now.Date;
                }
            }
            catch (Exception)
            {
            }
            return dtStr;
        }

        /// <summary>
        /// 将字符转换为html的unicode编码 
        /// </summary>
        /// <param name="unc"></param>
        /// <returns></returns>
        public static string Unicode2HTML(string unc)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in unc.ToCharArray())
            {
                sb.Append(("&#" + (Int32)c + ";"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 从html的unicode编码转换回原始字符的方法
        /// </summary>
        /// <param name="htm"></param>
        /// <returns></returns>
        public static string HTML2Unicode(string htm)
        {
            StringBuilder sb = new StringBuilder();
            htm = htm.Replace(" ", "").Replace("&#", "");
            foreach (string s in htm.Split(";".ToCharArray()))
            {
                if ((s.Length > 0))
                {
                    if (s.StartsWith("x"))
                    {
                        sb.Append(((char)Convert.ToInt32(s.Substring(1), 16)).ToString());
                    }
                    else
                    {
                        sb.Append(Convert.ToChar(Convert.ToInt32(s)));
                    }
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 过滤重复字符
        /// </summary>
        /// <param name="myData">待过滤字符串数组</param>
        /// <returns></returns>
        public static String[] RemoveDup(string[] myData)
        {
            if (myData.Length > 0)
            {
                Array.Sort(myData);//用了数组的排序,个人感觉在数组元素很多的时候能够提高一点效率吧!
                int size = 1; //at least 1 
                for (int i = 1; i < myData.Length; i++)
                {
                    if (myData[i] != myData[i - 1])
                    {
                        size++;
                    }
                }

                String[] myTempData = new String[size];
                int j = 0;
                myTempData[j++] = myData[0];
                for (int i = 1; i < myData.Length; i++)
                {
                    if (myData[i] != myData[i - 1])
                    {
                        myTempData[j++] = myData[i];
                    }
                }
                return myTempData;
            }
            return myData;
        }


        /// <summary>
        /// 判断是否是数字
        /// </summary>
        /// <param name="strInput">输入</param>
        /// <returns></returns>
        public static bool isNumeric(string strInput)
        {
            try
            {
                Convert.ToInt32(strInput);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
