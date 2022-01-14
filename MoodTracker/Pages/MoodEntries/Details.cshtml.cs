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
    public class DetailsModel : PageModel
    {
        private readonly MoodTracker.Data.ApplicationDbContext _context;

        public DetailsModel(MoodTracker.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public MoodEntry MoodEntry { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MoodEntry = await _context.MoodEntries
                .Include(m => m.User).FirstOrDefaultAsync(m => m.ID == id);

            if (MoodEntry == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
