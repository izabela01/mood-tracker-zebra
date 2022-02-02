using System;
using System.Linq;
using System.Linq.Expressions;
using MoodTracker.Models;

namespace MoodTracker.Extensions
{
    public static class IQueryableExtensions
    {
        public static IOrderedQueryable<TSource> OrderBy<TSource, TKey>
            (this IQueryable<TSource> queryable, Expression<Func<TSource, TKey>> keySelector, ColumnOrder columnOrder)
        {
            if (columnOrder == ColumnOrder.Ascending) return queryable.OrderBy(keySelector);
            return queryable.OrderByDescending(keySelector);
        }
    }
    
    public enum ColumnOrder
    {
        Ascending,
        Descending
    }
}