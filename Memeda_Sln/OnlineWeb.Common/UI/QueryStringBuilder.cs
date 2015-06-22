using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Web;

namespace OnlineWeb.Common.UI
{
    /// <summary>
    /// QueryString　构造器类
    /// </summary>
    public class QueryStringBuilder
    {
        #region 字段&属性

        private Dictionary<string, string> currentQueryStrings;

        /// <summary>
        /// 参数索引值
        /// </summary>
        /// <param name="key">参数索引</param>
        /// <returns>参数索引值</returns>
        public string this[string key]
        {
            get
            {
                if (this.currentQueryStrings.ContainsKey(key))
                {
                    return this.currentQueryStrings[key];
                }
                return string.Empty;
            }
        }
        /// <summary>
        /// 参数长度
        /// </summary>
        public int ParameterCount
        {
            get
            {
                return this.currentQueryStrings.Count;
            }
        }

        #endregion

        #region 方法
        /// <summary>
        /// 构造函数
        /// </summary>
        public QueryStringBuilder()
        {
            this.currentQueryStrings = new Dictionary<string, string>(
                StringComparer.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="queryString">参数集合</param>
        public QueryStringBuilder(NameValueCollection queryString)
            : this()
        {
            if (queryString != null)
            {
                foreach (string name in queryString.AllKeys)
                {
                    this.currentQueryStrings.Add(name, queryString[name]);
                }
            }
        }

        /// <summary>
        /// 添加QueryString
        /// </summary>
        /// <param name="paramName">参数名称</param>
        /// <param name="paramValue">参数值</param>
        public void AddQueryString(string paramName, string paramValue)
        {
            if (!string.IsNullOrEmpty(paramName) &&
                !currentQueryStrings.ContainsKey(paramName))
            {
                this.currentQueryStrings[paramName] = paramValue;
            }
        }

        /// <summary>
        /// 移除QueryString
        /// </summary>
        /// <param name="paramName">参数名</param>
        public void RemoveQueryString(string paramName)
        {
            if (!string.IsNullOrEmpty(paramName))
            {

                this.currentQueryStrings.Remove(paramName);
            }
        }

        /// <summary>
        /// 重写ToString方法
        /// </summary>
        /// <returns>结果</returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (KeyValuePair<string, string> pair in this.currentQueryStrings)
            {
                if (!string.IsNullOrEmpty(pair.Value))
                {
                    builder.Append(HttpUtility.UrlEncodeUnicode(pair.Key));
                    builder.Append("=");
                    builder.Append(HttpUtility.UrlEncodeUnicode(pair.Value));
                    builder.Append("&");
                }
            }
            return builder.ToString().TrimEnd(new char[] { '&' });
        }

        #endregion
    }

}
