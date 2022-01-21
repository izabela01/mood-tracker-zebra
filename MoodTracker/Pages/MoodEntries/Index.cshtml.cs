using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoodTracker.Data;
using MoodTracker.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace MoodTracker.Pages.MoodEntries
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<MoodEntry> MoodEntry { get;set; }

        public async Task OnGetAsync()
        {
            // No need for null checks, due to unreachable code path when user is not logged in which is forced by the Authorize attribute
            string currentUserId = _userManager.GetUserId(User);

            MoodEntry = await _context.MoodEntries
                .Include(m => m.User)
                .Where(m => m.UserId == currentUserId)
                .ToListAsync();
        }
    }
}
