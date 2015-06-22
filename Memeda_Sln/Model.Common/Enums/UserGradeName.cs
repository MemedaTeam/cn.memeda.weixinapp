using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Model.Common.Enums
{
    /// <summary>
    /// �û���Ա����
    /// </summary>
    [Serializable]
    public enum UserGradeName
    {
        /// <summary>
        /// ��ͨ��Ա
        /// </summary>
        [Description("��ͨ��Ա")]
        General,
        /// <summary>
        /// ���ƹ��
        /// </summary>
        [Description("���ƹ��")]
        Gold,
        /// <summary>
        /// �׽���
        /// </summary>
        [Description("�׽���")]
        Platinum,
        /// <summary>
        /// ��ʯ���
        /// </summary>
        [Description("��ʯ���")]
        Diamond
    }
}
