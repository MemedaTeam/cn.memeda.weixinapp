using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Model.Common.Enums
{
    /// <summary>
    /// ֧��վ����־����
    /// </summary>
    [Serializable]
    public enum LogType
    {
        /// <summary>
        /// ͨ����־
        /// </summary>
        [Description("ͨ����־")]
        General = 0,

        /// <summary>
        /// ������־
        /// </summary>
        [Description("������־")]
        Trace = 1,

        /// <summary>
        /// ������־
        /// </summary>
        [Description("������־")]
        Warning = 2,

        /// <summary>
        /// �쳣��־
        /// </summary>
        [Description("�쳣��־")]
        Exception = 3
    }
}
