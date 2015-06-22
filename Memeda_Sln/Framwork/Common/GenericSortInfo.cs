using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace Framework.Common
{
    /// <summary>
    /// 泛型排序实体类
    /// </summary>
    public class GenericSortInfo
    {
        private string _sortProperty;

        public string Property
        {
            get { return _sortProperty; }
            set { _sortProperty = value; }
        }
        private SortDirection _sortDirection;

        public SortDirection Direction
        {
            get { return _sortDirection; }
            set { _sortDirection = value; }
        }

        public GenericSortInfo(string sortProperty, SortDirection sortDirection)
        {
            _sortProperty = sortProperty;
            _sortDirection = sortDirection;
        }
    }
}
