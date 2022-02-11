using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoodTracker.Data;
using MoodTracker.Models;
using MoodTracker.Extensions;

namespace MoodTracker.Pages.MoodEntries
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public MoodEntry MoodEntry { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MoodEntry = await _context.MoodEntries
                .Include(m => m.User)
                .Include(m => m.Moods)
                .Where(m => m.User.Id == User.GetId())
                .FirstOrDefaultAsync(m => m.Id == id);

            if (MoodEntry == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
