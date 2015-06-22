using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;

namespace Business.Common
{
    public class GetHTML
    {
        public static CookieContainer GetCookieByStr(string cookiestr, string host)
        {
            Uri bbsRoot = new Uri(host);
            CookieContainer cookieContainer = new CookieContainer();
            string[] cookies = cookiestr.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < cookies.Length; i++)
            {
                string sCookie = cookies[i].Trim();
                int sIndex = sCookie.IndexOf("=");
                string cookie_name = sCookie.Substring(0, sIndex);
                string cookie_value = sCookie.Substring(sIndex + 1, sCookie.Length - sIndex - 1);
                Cookie Cookies1 = new Cookie(cookie_name, cookie_value);
                cookieContainer.Add(bbsRoot, Cookies1);
            }
            return cookieContainer;
        }
        public static string GetMessage(string Url, CookieContainer cc)
        {
            HttpWebResponse Result = null;
            string OutPutString = "";

            try
            {
                HttpWebRequest Req = (HttpWebRequest)WebRequest.Create(Url);

                Req.Method = "get";

                Req.CookieContainer = cc;

                Result = (HttpWebResponse)Req.GetResponse();

                StreamReader ReceiveStream = new StreamReader(Result.GetResponseStream(), System.Text.Encoding.GetEncoding("utf-8"));
                try
                {
                    OutPutString = ReceiveStream.ReadToEnd();
                }
                catch
                {
                }

                return OutPutString;
            }
            catch (Exception e)
            {
                e.ToString();
                return "";
            }
            finally
            {
                if (Result != null)
                {
                    Result.Close();
                }
            }
        }
        public static string GetHtml(string url, string encodingName)
        {
            HttpWebResponse response = null;

            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(new System.Uri(url));
                req.Method = "get";
                req.Accept = "*/* ";
                req.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 1.1.4322)";
                response = (HttpWebResponse)req.GetResponse();

                Encoding encoding = encodingName.Trim() == "" ? Encoding.GetEncoding(936) : Encoding.GetEncoding(encodingName.Trim());
                StreamReader receiveStream = new StreamReader(response.GetResponseStream(), encoding);

                return receiveStream.ReadToEnd();
            }
            catch (Exception ex)
            {
                ex.ToString();
                return "";
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
        }
        public static string CutHtml(string str, string startRegex, string endRegex)
        {
            try
            {
                str = Regex.Replace(str, "^.*?" + startRegex, string.Empty, RegexOptions.Singleline);
                str = Regex.Replace(str, endRegex + ".*?$", string.Empty, RegexOptions.Singleline);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return str;
        }
        public static ArrayList alParseInfo(string content, string prefix, string postfix)
        {
            try
            {
                string patt = prefix + @"(?<parse>.*?)" + postfix;

                MatchCollection mc = Regex.Matches(content, patt, RegexOptions.Singleline);

                ArrayList al = new ArrayList();

                for (int i = 0; i < mc.Count; i++)
                {
                    if (mc[i].Success)
                    {
                        al.Add(mc[i].Groups["parse"].Value.Trim());
                    }
                }

                return al;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
