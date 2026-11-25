using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CraftWorkshopSystem.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Workshop> Workshops { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Instructor> Instructors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       {
        string connectionString = "Server=localhost\\SQLEXPRESS;Database=CraftWorkshopDb;Trusted_Connection=True;TrustServerCertificate=True;";
        optionsBuilder.UseSqlServer(connectionString);
       }
    }
}