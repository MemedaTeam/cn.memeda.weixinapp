using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineWeb.Common.UI
{
    /// <summary>
    /// Url页面地址构造类
    /// </summary>
    public static class UrlBuilder
    {
        #region 方法
        /// <summary>
        /// 页面Url构造方法
        /// </summary>
        /// <param name="currentUrl">页面Url</param>
        /// <param name="parameters">QueryString参数</param>
        /// <returns>结果</returns>
        public static string BuildUrl(string currentUrl, List<QueryStringParameter> parameters)
        {
            if (string.IsNullOrEmpty(currentUrl))
            {
                return string.Empty;
            }

            QueryStringBuilder builder = new QueryStringBuilder();
            foreach (QueryStringParameter parameter in parameters)
            {
                if (!(string.IsNullOrEmpty(parameter.Name) ||
                      string.IsNullOrEmpty(parameter.Value)))
                {
                    builder.AddQueryString(parameter.Name, parameter.Value);
                }
            }

            return (currentUrl + "/" + builder.ToString().TrimStart(new char[] { '/' }));
        }

        /// <summary>
        /// 页面Url构造方法.
        /// </summary>
        /// <param name="currentUrl">页面Url<</param>
        /// <param name="queryString">QueryString参数, in the format of param1, value1, param2, value2, ...</param>
        /// <returns>结果</returns>
        public static string BuildUrl(string currentUrl, params string[] queryString)
        {
            QueryStringBuilder builder = new QueryStringBuilder();
            for (int i = 0; i < queryString.Length; i += 2)
            {
                if (i >= queryString.Length - 1)
                {
                    break;
                }
                else
                {
                    builder.AddQueryString(queryString[i], queryString[i + 1]);
                }
            }

            return (currentUrl + "/" + builder.ToString().TrimStart(new char[] { '/' }));

        }

        #endregion
    }
}
