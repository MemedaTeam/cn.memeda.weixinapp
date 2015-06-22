using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Ctrip.PaymentBase.Model.Common.Enums
{
    /// <summary>
    /// 多语言, 参照Firefox的语言(由于语法限制, 中划线"-"替换成下划线"_", Description里面是标准的语言).
    /// </summary>
    [Serializable]
    public enum Language
    {
        /// <summary>
        /// 英语
        /// </summary>
        [Description("en")]
        en = 0,

        /// <summary>
        /// 汉语/中国
        /// </summary>
        [Description("zh-cn")]
        zh_cn = 11,

        /// <summary>
        /// 汉语/中国台湾
        /// </summary>
        [Description("zh-tw")]
        zh_tw = 12,

        /// <summary>
        /// 日语
        /// </summary>
        [Description("ja")]
        ja = 20,

        /// <summary>
        /// 朝鲜语
        /// </summary>
        [Description("ko")]
        ko = 30
    }
}
