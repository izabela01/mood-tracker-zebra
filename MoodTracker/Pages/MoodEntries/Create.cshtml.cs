using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MoodTracker.Data;
using MoodTracker.Models;

namespace MoodTracker.Pages.MoodEntries
{
    public class CreateModel : PageModel
    {
        private readonly MoodTracker.Data.ApplicationDbContext _context;

        public CreateModel(MoodTracker.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public MoodEntry MoodEntry { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.MoodEntries.Add(MoodEntry);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
