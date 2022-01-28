
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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


            var hasher = new PasswordHasher<IdentityUser>();

            var users = new IdentityUser[]
                {

                            new IdentityUser
                            {
                                Id = "8e445865-a24d-4543-a6c6-9443d048cdb9", // primary key
                                UserName = "user@moodtracker.com",
                                NormalizedUserName = "USER@MOODTRACKER.COM",
                                Email = "user@moodtracker.com",
                                NormalizedEmail = "USER@MOODTRACKER.COM",
                                EmailConfirmed = true,
                                PasswordHash = hasher.HashPassword(null, "Password!123")
                            },

                            new IdentityUser
                            {
                                Id = "8e445975-a24d-4733-a6c6-1000d048yuo9", // primary key
                                UserName = "user2@moodtracker.com",
                                NormalizedUserName = "USER2@MOODTRACKER.COM",
                                Email = "user2@moodtracker.com",
                                EmailConfirmed = true,
                                NormalizedEmail = "USER2@MOODTRACKER.COM",

                                PasswordHash = hasher.HashPassword(null, "Password!123")
                            },

                        new IdentityUser
                        {
                            Id = "8e829213-a24d-8193-a6c6-4000d044pop9", // primary key
                            UserName = "user3@moodtracker.com",
                            NormalizedUserName = "USER3@MOODTRACKER.COM",
                            Email = "user3@moodtracker.com",
                            NormalizedEmail = "USER3@MOODTRACKER.COM",
                            EmailConfirmed = true,
                            PasswordHash = hasher.HashPassword(null, "Password!123")
                        },
                };

            context.Users.AddRange(users);
            context.SaveChanges();

            var moodEntries = new MoodEntry[]
             {
                   // int Id , IdentityUser User, DateTime Date, int MoodScore , string Notes, ICollection<MoodLookup> Moods
                    new MoodEntry{}
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

            context.Moods.AddRange(moods);
            context.SaveChanges();
        }
    }
}