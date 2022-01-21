using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MoodTracker.Data;
using MoodTracker.Extensions;
using MoodTracker.Models;

namespace MoodTracker.Pages.MoodEntries
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly MoodTracker.Data.ApplicationDbContext _context;

        public CreateModel(MoodTracker.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public MoodEntry MoodEntry { get; set; }


        public async Task<IActionResult> OnPostAsync()
        {
            MoodEntry newMoodEntry = new MoodEntry();

            if (await TryUpdateModelAsync<MoodEntry>(
                newMoodEntry,
                "MoodEntry",
                m => m.Date, s => s.MoodScore, s => s.Notes))
            {
                newMoodEntry.UserId = User.GetId();

                _context.MoodEntries.Add(newMoodEntry);

                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
