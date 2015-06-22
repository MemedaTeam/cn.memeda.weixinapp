using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace Framework.Common
{
    /// <summary>
    /// 泛型比较器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericComparer<T> : IComparer<T>
    {
        private List<GenericSortInfo> _sortItems;

        /// <summary>
        /// Collection of sorting criteria
        /// </summary>
        public List<GenericSortInfo> SortItems
        {
            get { return _sortItems; }
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public GenericComparer()
        {
            _sortItems = new List<GenericSortInfo>();
        }

        /// <summary>
        /// Constructor that takes a sorting class collection as param
        /// </summary>
        /// <param name="sortClass">
        /// Collection of sorting criteria 
        ///</param>
        public GenericComparer(List<GenericSortInfo> sortItem)
        {
            _sortItems = sortItem;
        }

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="sortProperty">Property to sort on</param>
        /// <param name="sortDirection">Direction to sort</param>
        public GenericComparer(string sortProperty, SortDirection sortDirection)
        {
            _sortItems = new List<GenericSortInfo>();
            _sortItems.Add(new GenericSortInfo(sortProperty, sortDirection));
        }

        /// <summary>
        /// Implementation of IComparer interface to compare to object
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>结果</returns>
        public int Compare(T x, T y)
        {
            if (SortItems.Count == 0)
            {
                return 0;
            }

            return CheckSort(0, x, y);
        }

        /// <summary>
        /// Recursive function to do sorting
        /// </summary>
        /// <param name="sortLevel">Current level sorting at</param>
        /// <param name="myObject1"></param>
        /// <param name="myObject2"></param>
        /// <returns>结果</returns>
        private int CheckSort(int sortLevel, T myObject1, T myObject2)
        {
            int result = 0;

            if (SortItems.Count - 1 < sortLevel)
            {
                return result;
            }

            object valueOf1 = myObject1.GetType().GetProperty(
                 SortItems[sortLevel].Property).GetValue(myObject1, null);

            object valueOf2 = myObject2.GetType().GetProperty(
                SortItems[sortLevel].Property).GetValue(myObject2, null);

            if (SortItems[sortLevel].Direction == SortDirection.Ascending)
            {
                result = ((IComparable)valueOf1).CompareTo((IComparable)valueOf2);
            }
            else
            {
                result = ((IComparable)valueOf2).CompareTo((IComparable)valueOf1);
            }

            if (result == 0)
            {
                result = CheckSort(sortLevel + 1, myObject1, myObject2);
            }

            return result;
        }
    }
}
