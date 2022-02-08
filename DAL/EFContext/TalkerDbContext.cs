﻿using Core;
using Core.Models;
using DAL.Abstractions.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
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
        public DbSet<User> Users { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<CustomRole> CustomRoles { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Chat> Chats { get; set; }

        public TalkerDbContext(DbContextOptions options) : base(options)
        {

        }

        //public TalkerDbContext()
        //{

        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    /*optionsBuilder.UseLazyLoadingProxies();*/
        //    optionsBuilder.UseSqlServer("data source=DESKTOP-VOR6N2H\\TALKERSQLSERVER;initial catalog=TalkerDb;trusted_connection=true");
        //}
    }
}
