using Core.Entities;
using Data.Mappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contexts
{
    public class LFEDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Host> Hosts { get; set; }

        public DbSet<Dish> Dishes { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public LFEDbContext() { }

        public LFEDbContext(DbContextOptions<LFEDbContext> options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=LAPTOP-OF-KIET\SQLEXPRESS;Initial Catalog=LocalFoodExp;Integrated Security=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CategoryMap).Assembly);
        }
    }
}
