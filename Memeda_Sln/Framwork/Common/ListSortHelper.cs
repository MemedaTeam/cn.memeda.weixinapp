using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Framework.Common
{
    /// <summary>
    /// ����List���������
    /// </summary>
    public class ListSortHelper
    {
        /// <summary>
        /// ����List���򷽷�
        /// </summary>
        /// <typeparam name="T">���Ͷ���</typeparam>
        /// <param name="listToSort">����ǰ�Ķ���</param>
        /// <param name="sortExpression">�����б�</param>
        /// <param name="sortDirection">������</param>
        /// <returns>�����Ķ���</returns>
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
        /// ����List���򷽷�
        /// </summary>
        /// <typeparam name="T">���Ͷ���</typeparam>
        /// <param name="listToSort">����ǰ�Ķ���</param>
        /// <param name="sortExpression">������</param>
        /// <param name="sortDirection">������</param>
        /// <returns>�����Ķ���</returns>
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
