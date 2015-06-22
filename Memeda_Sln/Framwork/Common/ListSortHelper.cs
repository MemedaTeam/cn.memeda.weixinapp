using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Framework.Common
{
    /// <summary>
    /// 泛型List排序帮助类
    /// </summary>
    public class ListSortHelper
    {
        /// <summary>
        /// 泛型List排序方法
        /// </summary>
        /// <typeparam name="T">泛型对象</typeparam>
        /// <param name="listToSort">排序前的对象</param>
        /// <param name="sortExpression">排序列表</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns>排序后的对象</returns>
        public static List<T> Sort<T>(List<T> listToSort,
            List<string> sortExpression,List<SortDirection> sortDirection)
        {
            //check parameters           
            if (sortExpression.Count != sortDirection.Count ||
                sortExpression.Count == 0 ||
                sortDirection.Count == 0)
            {
                throw new ArgumentException("Invalid sort arguments!");
            }

            //get myComparer
            GenericComparer<T> myComparer = new GenericComparer<T>();
            for (int i = 0; i < sortExpression.Count; i++)
            {
                GenericSortInfo sortItem = new GenericSortInfo(sortExpression[i], sortDirection[i]);
                myComparer.SortItems.Add(sortItem);
            }

            listToSort.Sort(myComparer);

            return listToSort;

        }

        /// <summary>
        /// 泛型List排序方法
        /// </summary>
        /// <typeparam name="T">泛型对象</typeparam>
        /// <param name="listToSort">排序前的对象</param>
        /// <param name="sortExpression">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns>排序后的对象</returns>
        public static List<T> Sort<T>(List<T> listToSort,
            string sortExpression,SortDirection sortDirection)
        {
            //check parameters
            if (sortExpression == null || sortExpression == string.Empty)
            {
                return listToSort;
            }

            GenericComparer<T> myComparer = new GenericComparer<T>();
            myComparer.SortItems.Add(new GenericSortInfo(sortExpression, sortDirection));
            listToSort.Sort(myComparer);

            return listToSort;
        }
    }
}
