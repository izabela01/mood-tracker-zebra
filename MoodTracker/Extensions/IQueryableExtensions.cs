using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Data.SqlClient;
using MoodTracker.Models;

namespace MoodTracker.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<TSource> OrderBy<TSource, TKey>
            (this IQueryable<TSource> queryable, Expression<Func<TSource, TKey>> keySelector, SortOrder sortOrder)
        {
            IQueryable<TSource> queryToReturn = sortOrder switch
            {
                SortOrder.Ascending => queryable.OrderBy(keySelector),
                SortOrder.Descending => queryable.OrderByDescending(keySelector),
                _ => queryable
            };

            return queryToReturn;
        }
    }
}