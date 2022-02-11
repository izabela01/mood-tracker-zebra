using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MoodTracker.Pages.MoodEntries
{

    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        public PaginatedList(List<T> pageContents, int pageIndex, int totalPages)
        { 
            PageIndex = pageIndex;
            TotalPages = totalPages;
            AddRange(pageContents);
        }

        public bool HasPreviousPage => PageIndex > 0;

        public bool HasNextPage => PageIndex < (TotalPages - 1);

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> query, int pageIndex, int pageSize)
        {
            var totalItems = await query.CountAsync();
            
            var pagesToSkip = (pageIndex) * pageSize;
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            
            var pageContents = await query.Skip(pagesToSkip).Take(pageSize).ToListAsync();
            
            return new PaginatedList<T>(pageContents, pageIndex, totalPages);
        }
    }
}