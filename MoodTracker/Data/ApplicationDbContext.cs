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
        public DbSet<MoodLookup> MoodLookups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {


            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Mood>().ToTable("Moods");
            modelBuilder.Entity<MoodEntry>().ToTable("MoodEntries");
            modelBuilder.Entity<MoodLookup>().ToTable("MoodLookups")
                .HasKey(moodLookup => new { moodLookup.MoodEntryId, moodLookup.MoodId });

           // DbIntializer.Initialize(options, modelBuilder);

        }


    }
}
