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
    public class DeleteModel : PageModel
    {
        private readonly MoodTracker.Data.ApplicationDbContext _context;

        public DeleteModel(MoodTracker.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MoodEntry = await _context.MoodEntries.FindAsync(id);

            if (MoodEntry != null)
            {
                _context.MoodEntries.Remove(MoodEntry);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
