using System.Collections.Generic;

namespace MoodTracker.Models
{
    public class Mood
    {
        public Mood()
        {
            MoodEntries = new HashSet<MoodEntry>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<MoodEntry> MoodEntries { get; set; }
    }
}
