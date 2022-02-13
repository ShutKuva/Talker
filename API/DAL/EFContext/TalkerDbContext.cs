using Core;
using Core.Models;
using DAL.Abstractions.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
namespace DAL.EFContext
{
    public class TalkerDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<CustomRole> CustomRoles { get; set; }

        public DbSet<Message> Messages { get; set; }

        public TalkerDbContext(DbContextOptions<TalkerDbContext> options) : base(options)
        {

        }

        //public TalkerDbContext()
        //{

        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("data source=ZHEKA\\SQLEXPRESS;initial catalog=TestEF;trusted_connection=true");
        //}
    }
}
