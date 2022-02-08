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
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using MoodTracker.Extensions;

namespace MoodTracker.Pages.MoodEntries
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public const string DATE_COLUMN_STRING = nameof(MoodEntry.Date);
        public const string SCORE_COLUMN_STRING = nameof(MoodEntry.MoodScore);
        public const string MOOD_TAG_STRING = "MoodTags";
        
        public SortableTable<MoodEntry> MoodEntriesTable { get; set; }
        
        private readonly ApplicationDbContext _context;
        private readonly int _pageSize;

        public IndexModel(ApplicationDbContext context, IConfiguration configuration)
        {
            _pageSize = configuration.GetValue<int>("tablePageSize", 4);
            _context = context;
        }
        
        public async Task OnGetAsync(string sortColumn=DATE_COLUMN_STRING, SortOrder sortOrder=SortOrder.Descending, int pageIndex=0)
        {
            Expression<Func<MoodEntry, object>> keySelector = sortColumn switch
            {
                DATE_COLUMN_STRING => entry => entry.Date,
                SCORE_COLUMN_STRING => entry => entry.MoodScore,
                _ => throw new ArgumentOutOfRangeException(nameof(sortColumn), sortColumn, "sortColumn is invalid")
            };

            IQueryable<MoodEntry> moodEntryQuery = _context.MoodEntries
                .Include(m => m.User)
                .Include(m => m.Moods)
                .Where(m => m.UserId == User.GetId())
                .OrderBy(keySelector, sortOrder);
            
            MoodEntriesTable = new SortableTable<MoodEntry>(new List<Column>()
            {
                new (DATE_COLUMN_STRING, true),
                new (SCORE_COLUMN_STRING, true),
                new (MOOD_TAG_STRING, false)
            }, sortColumn, sortOrder);
            
            MoodEntriesTable.PaginatedList = await PaginatedList<MoodEntry>.CreateAsync(moodEntryQuery, pageIndex, _pageSize);
        }
    }
}
