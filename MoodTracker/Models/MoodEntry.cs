using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace MoodTracker.Models
{
    public class MoodEntry
    {
        public IdentityUser User { get; set; }
        public string ID { get; set; }
        public string UserID { get; set; }
        public DateTime Date { get; set; }
        public int MoodScore { get; set; }
        public string Notes { get; set; }


        public ICollection<MoodLookup> Moods { get; set; }

    }


}
