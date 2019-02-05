using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Homework04DbContext : DbContext
    {
        public Homework04DbContext() : base("Homework04DbConnectionString")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Homework04DbContext, Migrations.Configuration>());
        }
        public DbSet<Enemy> Enemies { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Armor> Armors { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
    }
}
