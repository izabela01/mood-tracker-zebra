using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoodTracker.Data;
using MoodTracker.Models;

namespace MoodTracker.Pages.MoodEntries
{
    public class IndexModel : PageModel
    {
        private readonly MoodTracker.Data.ApplicationDbContext _context;

        public IndexModel(MoodTracker.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<MoodEntry> MoodEntry { get;set; }

        public async Task OnGetAsync()
        {
            MoodEntry = await _context.MoodEntries
                .Include(m => m.User).ToListAsync();
        }
    }
}
