using Core;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EFContext
{
    class TalkerDbContext : DbContext
    {
        public readonly SQLDBConnection _sqldbconnection;

        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }

        public TalkerDbContext()
        {

        }

        public TalkerDbContext(IOptions<SQLDBConnection> sqldbconnection)
        {
            _sqldbconnection = sqldbconnection?.Value ?? throw new ArgumentNullException(nameof(sqldbconnection));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_sqldbconnection.ConnectionString);
        }
    }
}
