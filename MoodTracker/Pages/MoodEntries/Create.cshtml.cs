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
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["Moods"] = _context.Moods;
            return Page();
        }

        [BindProperty]
        public MoodEntry MoodEntry { get; set; }


        public async Task<IActionResult> OnPostAsync(string[] selectedCourses)
        {
            MoodEntry newMoodEntry = new MoodEntry();

            if (await TryUpdateModelAsync(
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
