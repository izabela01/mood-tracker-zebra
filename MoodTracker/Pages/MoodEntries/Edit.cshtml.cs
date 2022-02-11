using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoodTracker.Data;
using MoodTracker.Extensions;
using MoodTracker.Models;

namespace MoodTracker.Pages.MoodEntries
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MoodEntry MoodEntry { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["Moods"] = _context.Moods;
            ViewData["MoodSliderRange"] =
                typeof(MoodEntry).getAttributeForProperty<RangeAttribute>(nameof(Models.MoodEntry.MoodScore));

            MoodEntry = await _context.MoodEntries
                .Include(m => m.User)
                .Include(m => m.Moods)
                .Where(m => m.UserId == User.GetId())
                .FirstOrDefaultAsync(m => m.Id == id);

            if (MoodEntry == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id, int[] selectedMoods)
        {
            MoodEntry moodEntryToUpdate = await _context.MoodEntries
                .Include(moodEntry => moodEntry.Moods)
                .Where(moodEntry => moodEntry.UserId == User.GetId())
                .Where(moodEntry => moodEntry.Id == id)
                .FirstAsync();

            if (moodEntryToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync(
                moodEntryToUpdate,
                "MoodEntry",
                m => m.Date, m => m.MoodScore, m => m.Notes))
            {
                List<int> currentlyEnabledMoodIds = new List<int>();

                // Remove tags that are enabled now but not given in post request
                foreach (var currentlyEnabledMood in moodEntryToUpdate.Moods)
                {
                    currentlyEnabledMoodIds.Add(currentlyEnabledMood.Id);

                    if(!selectedMoods.Contains(currentlyEnabledMood.Id))
                    {
                        moodEntryToUpdate.Moods.Remove(currentlyEnabledMood);
                    }
                }

                // Add mood tags
                foreach (var selectedMoodId in selectedMoods)
                {
                    var moodToAdd = _context.Moods.Single(mood => mood.Id == selectedMoodId);

                    if (moodToAdd != null && !currentlyEnabledMoodIds.Contains(selectedMoodId))
                    {
                        moodEntryToUpdate.Moods.Add(moodToAdd);
                    }
                }

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MoodEntryExists(MoodEntry.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

            }

            var a = string.Join(",",
                    ModelState.Values.Where(E => E.Errors.Count > 0)
                    .SelectMany(E => E.Errors)
                    .Select(E => E.ErrorMessage + E.Exception)
                    .ToArray());

            var b = ModelState.Values.Where(E => E.Errors.Count > 0);
            return RedirectToPage("./Index");
        }

        private bool MoodEntryExists(int id)
        {
            return _context.MoodEntries.Any(e => e.Id == id);
        }
    }
}
