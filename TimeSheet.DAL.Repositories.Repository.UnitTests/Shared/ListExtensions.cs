using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeSheet.DAL.Repositories.Repository.UnitTests
{
    public static class ListExtensions
    {
        public static bool isEqualTo<T>(this List<T> list1, List<T> list2)
        {
            if (list1.Count() != list2.Count())
            {
                return false;
            }

            for (int index = 0; index < list1.Count(); index++)
            {
                if (list1[index].ToString() != list2[index].ToString())
                {
                    return false;
                }
            }

            return true;
        }
    }
}
