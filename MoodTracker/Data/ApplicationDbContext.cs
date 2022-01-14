using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoodTracker.Models;

namespace MoodTracker.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Mood> Moods { get; set; }
        public DbSet<MoodEntry> MoodEntries { get; set; }
        public DbSet<MoodLookup> MoodLookups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Mood>().ToTable("Mood");
            modelBuilder.Entity<MoodEntry>().ToTable("MoodEntry");
            modelBuilder.Entity<MoodLookup>().ToTable("MoodLookup");
        }


    }
}
