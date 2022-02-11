using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
            ViewData["MoodSliderRange"] =
                typeof(MoodEntry).getAttributeForProperty<RangeAttribute>(nameof(Models.MoodEntry.MoodScore));
            
            return Page();
        }

        [BindProperty]
        public MoodEntry MoodEntry { get; set; }

        public async Task<IActionResult> OnPostAsync(IEnumerable<int> selectedMoods)
        {
            MoodEntry newMoodEntry = new MoodEntry();

            if (await TryUpdateModelAsync(
                newMoodEntry,
                "MoodEntry",
                m => m.Date, m => m.MoodScore, m => m.Notes))
            {
                newMoodEntry.UserId = User.GetId();

                foreach (var moodIdToAdd in selectedMoods)
                {
                    Mood moodToAdd = _context.Moods.Single(mood => mood.Id == moodIdToAdd);
                    newMoodEntry.Moods.Add(moodToAdd);
                }

                _context.MoodEntries.Add(newMoodEntry);

                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
