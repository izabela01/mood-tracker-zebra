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
using MoodTracker.Extensions;

namespace MoodTracker.Pages.MoodEntries
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private const string ASCENDING_STRING = "ascending";
        private const string DESCENDING_STRING = "descending";
        private const string SCORE_STRING = "score";
        private const string DATE_STRING = "date";
        
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<MoodEntry> MoodEntry { get;set; }

        public async Task OnGetAsync(string sortColumn, string sortOrder)
        {
            sortOrder ??= ASCENDING_STRING;
            sortColumn ??= DATE_STRING;
            
            IQueryable<MoodEntry> moodEntryQuery = _context.MoodEntries
                .Include(m => m.User)
                .Where(m => m.UserId == User.GetId());

            ColumnOrder columnOrder = sortOrder switch
            {
                ASCENDING_STRING => ColumnOrder.Ascending,
                DESCENDING_STRING => ColumnOrder.Descending,
                _ => throw new ArgumentOutOfRangeException()
            };

            moodEntryQuery = sortColumn switch
            {
                DATE_STRING => moodEntryQuery.OrderBy(entry => entry.Date, columnOrder),
                SCORE_STRING => moodEntryQuery.OrderBy(entry => entry.MoodScore, columnOrder),
                _ => throw new ArgumentOutOfRangeException()
            };
            
            MoodEntry = await moodEntryQuery.ToListAsync();
        }
    }
}
