using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Model.Common.Enums
{
    /// <summary>
    /// 用户会员级别
    /// </summary>
    [Serializable]
    public enum UserGradeName
    {
        /// <summary>
        /// 普通会员
        /// </summary>
        [Description("普通会员")]
        General,
        /// <summary>
        /// 金牌贵宾
        /// </summary>
        [Description("金牌贵宾")]
        Gold,
        /// <summary>
        /// 白金贵宾
        /// </summary>
        [Description("白金贵宾")]
        Platinum,
        /// <summary>
        /// 钻石贵宾
        /// </summary>
        [Description("钻石贵宾")]
        Diamond
    }
}
