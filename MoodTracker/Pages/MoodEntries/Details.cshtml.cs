using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<IdentityUser> _userManager;

        public DetailsModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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
