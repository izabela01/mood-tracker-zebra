using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoodTracker.Models;
using MoodTracker.Data;


namespace MoodTracker.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {


        public  ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Mood> Moods { get; set; }
        public DbSet<MoodEntry> MoodEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {


            base.OnModelCreating(modelBuilder);


            modelBuilder
                .Entity<MoodEntry>()
                .ToTable("MoodEntries")
                .HasMany(moodEntry => moodEntry.Moods)
                .WithMany(mood => mood.MoodEntries)
                .UsingEntity(j => j.ToTable("MoodLookup"));

            modelBuilder
                .Entity<Mood>()
                .ToTable("Moods");


        }


    }
}
