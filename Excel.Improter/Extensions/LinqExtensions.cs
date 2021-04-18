using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Excel.Improter.Extensions
{
    public static class LinqExtensions
    {
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query,bool condition,Expression<Func<T,bool>> expression)
        {
            if (condition)
            {
                return query.Where(expression);
            }
            return query;
        }
    }
}
