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
using MoodTracker.Extensions;

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
           MoodEntry = await _context.MoodEntries
                .Include(m => m.User)
                .Where(m => m.UserId == User.GetId())
                .ToListAsync();
        }
    }
}
