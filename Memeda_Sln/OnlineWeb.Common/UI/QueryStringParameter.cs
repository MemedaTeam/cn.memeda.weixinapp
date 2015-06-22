using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineWeb.Common.UI
{
    /// <summary>
    /// QueryString参数对象
    /// </summary>
    public class QueryStringParameter
    {
        #region 字段&属性
        private string name;
        /// <summary>
        /// 参数名称
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        private string currentValue;
        /// <summary>
        /// 参数值
        /// </summary>
        public string Value
        {
            get
            {
                return currentValue;
            }
            set
            {
                currentValue = value;
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="currentValue">参数值</param>
        public QueryStringParameter(string name, string currentValue)
        {
            this.name = name;
            this.Value = currentValue;
        }
        #endregion
    }
}
