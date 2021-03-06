﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Save_A_Soul.Models;
using Microsoft.Extensions.Configuration;
namespace Save_A_Soul.Contexts
{
    public class Context : DbContext
    {
        public Context (DbContextOptions<Context> options)
            : base(options)
        {
        }

        public static bool isMigration = true;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (isMigration)
                //optionsBuilder.UseSqlServer(@"Server=.;Database=DBSaveASoul;Trusted_Connection=true;");
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Context-good;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Adoption>().HasKey(c => new { c.AnimalId, c.UserId });
            modelBuilder.Entity<Favorite>().HasKey(c => new { c.AnimalId, c.UserId });
        }


        public DbSet<Address> Addresses { get; set; }
        public DbSet<Adoption> Adoptions { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Shelter> Shelters { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
