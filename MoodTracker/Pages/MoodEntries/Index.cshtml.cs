using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoodTracker.Data;
using MoodTracker.Models;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<IdentityUser> _userManager;
        
        public IndexModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<MoodEntry> MoodEntry { get;set; }
        public string DateColumnTitle { get; set; }
        public string ScoreColumnTitle { get; set; }
        public SortOrder CurrentSortOrder { get; set; }

        
        public async Task OnGetAsync(string sortColumn=DATE_COLUMN_STRING, SortOrder sortOrder=SortOrder.Descending)
        {
            string orderIcon = sortOrder switch
            {
                SortOrder.Ascending => " ▲",
                SortOrder.Descending => " ▼",
                _ => " [UNSORTED]"
            };
            
            CurrentSortOrder = sortOrder;
            DateColumnTitle = DATE_COLUMN_STRING + (sortColumn==DATE_COLUMN_STRING ? orderIcon : "");
            ScoreColumnTitle = SCORE_COLUMN_STRING + (sortColumn==SCORE_COLUMN_STRING ? orderIcon : "");

            IQueryable<MoodEntry> moodEntryQuery = _context.MoodEntries
                .Include(m => m.User)
                .Where(m => m.UserId == User.GetId());
            
            moodEntryQuery = sortColumn switch
            {
                DATE_COLUMN_STRING => moodEntryQuery.OrderBy(entry => entry.Date, sortOrder),
                SCORE_COLUMN_STRING => moodEntryQuery.OrderBy(entry => entry.MoodScore, sortOrder),
                _ => throw new ArgumentOutOfRangeException()
            };
            
            MoodEntry = await moodEntryQuery.ToListAsync();
        }
    }
}
