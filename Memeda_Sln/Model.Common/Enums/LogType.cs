using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Model.Common.Enums
{
    /// <summary>
    /// 支付站点日志类型
    /// </summary>
    [Serializable]
    public enum LogType
    {
        /// <summary>
        /// 通用日志
        /// </summary>
        [Description("通用日志")]
        General = 0,

        /// <summary>
        /// 跟踪日志
        /// </summary>
        [Description("跟踪日志")]
        Trace = 1,

        /// <summary>
        /// 警告日志
        /// </summary>
        [Description("警告日志")]
        Warning = 2,

        /// <summary>
        /// 异常日志
        /// </summary>
        [Description("异常日志")]
        Exception = 3
    }
}
