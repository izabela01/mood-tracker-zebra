
using MoodTracker.Models; 
using System.Linq;

namespace MoodTracker.Data
{


    public static class DbIntializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Look for any moodEntries.
            if (context.MoodEntries.Any())
            {
                return;   // DB has been seeded
            }

            var moodEntries = new MoodEntry[]
             {
                   // int Id , IdentityUser User, DateTime Date, int MoodScore , string Notes, ICollection<MoodLookup> Moods
                    new MoodEntry()
             };

            // Happy Sad Scared Discusted Angry Suprised
            // Id, Name, Description

            var moods = new Mood[]
             {
                new Mood{Id = 1, Name = "Happy", Description = "" },
                new Mood{Id = 2, Name = "Sad", Description = "" },
                new Mood{Id = 3, Name = "Scared", Description = "" },
                new Mood{Id = 4, Name = "Discusted", Description = "" },
                new Mood{Id = 5, Name = "Angry", Description = "" },
                new Mood{Id = 6, Name = "Suprised", Description = ""},
             };

            var moodLookup = new Mood[]
            {


            };

        }
    }

}