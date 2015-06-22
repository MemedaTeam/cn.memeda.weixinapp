using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Ctrip.PaymentBase.Model.Common.Enums
{
    /// <summary>
    /// ������, ����Firefox������(�����﷨����, �л���"-"�滻���»���"_", Description�����Ǳ�׼������).
    /// </summary>
    [Serializable]
    public enum Language
    {
        /// <summary>
        /// Ӣ��
        /// </summary>
        [Description("en")]
        en = 0,

        /// <summary>
        /// ����/�й�
        /// </summary>
        [Description("zh-cn")]
        zh_cn = 11,

        /// <summary>
        /// ����/�й�̨��
        /// </summary>
        [Description("zh-tw")]
        zh_tw = 12,

        /// <summary>
        /// ����
        /// </summary>
        [Description("ja")]
        ja = 20,

        /// <summary>
        /// ������
        /// </summary>
        [Description("ko")]
        ko = 30
    }
}
