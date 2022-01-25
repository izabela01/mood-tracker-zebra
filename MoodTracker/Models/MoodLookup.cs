using System;


namespace MoodTracker.Models
{
    public class MoodLookup
    {
        public int MoodEntryId { get; set; }
        public int MoodId { get; set; }

        public MoodEntry MoodEntry { get; set; }
        public Mood Mood { get; set; }
    }

}
