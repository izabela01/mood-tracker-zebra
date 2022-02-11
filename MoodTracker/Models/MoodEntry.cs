using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MoodTracker.Models
{
    public class MoodEntry
    {
        public MoodEntry()
        {
            Moods = new HashSet<Mood>();
        }

        public int Id { get; set; }

        public string UserId { get; set; }
        public IdentityUser User { get; set; }

        public DateTime Date { get; set; }
        
        [Range(-5,5)]
        public int MoodScore { get; set; }
        public string Notes { get; set; }

        public virtual ICollection<Mood> Moods { get; set; }
    }


}
