using System;


namespace MoodTracker.Models
{
    public class MoodLookup
    {
        public string MoodEntryId { get; set; }
        public string MoodId { get; set; }

        public MoodEntry MoodEntry { get; set; }
        public Mood Mood { get; set; }
    }

}
