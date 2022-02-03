using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoodTracker.Data;
using MoodTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using MoodTracker.Extensions;

namespace MoodTracker.Pages.MoodEntries
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public const string SCORE_COLUMN_STRING = "score";
        public const string DATE_COLUMN_STRING = "date";
        
        private readonly ApplicationDbContext _context;
        
        public IList<MoodEntry> MoodEntry { get;set; }
        public Dictionary<string, SortedColumnData> ColumnDataLookup { get; }

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
            
            ColumnDataLookup = new()
            {
                {DATE_COLUMN_STRING, new SortedColumnData(DATE_COLUMN_STRING)},
                {SCORE_COLUMN_STRING, new SortedColumnData(SCORE_COLUMN_STRING)}
            };
        }
        
        public async Task OnGetAsync(string sortColumn=DATE_COLUMN_STRING, SortOrder sortOrder=SortOrder.Descending)
        {
            var currentColumnData = ColumnDataLookup[sortColumn];
            currentColumnData.Active = true;
            currentColumnData.CurrentSortOrder = sortOrder;
            
            Expression<Func<MoodEntry, object>> keySelector = sortColumn switch
            {
                DATE_COLUMN_STRING => entry => entry.Date,
                SCORE_COLUMN_STRING => entry => entry.MoodScore,
                _ => throw new ArgumentOutOfRangeException(nameof(sortColumn), sortColumn, "sortColumn is invalid")
            };

            IQueryable<MoodEntry> moodEntryQuery = _context.MoodEntries
                .Include(m => m.User)
                .Where(m => m.UserId == User.GetId())
                .OrderBy(keySelector, sortOrder);
            
            MoodEntry = await moodEntryQuery.ToListAsync();
        }
    }
}
