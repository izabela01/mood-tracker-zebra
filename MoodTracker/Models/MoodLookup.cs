using System;


namespace MoodTracker.Models
{
    public class MoodLookup
    {
        public string ID { get; set; }
        public string MoodEntryID { get; set; }
        public string MoodID { get; set; }

        public MoodEntry MoodEntry { get; set; }
        public Mood Mood { get; set; }

    }

}
