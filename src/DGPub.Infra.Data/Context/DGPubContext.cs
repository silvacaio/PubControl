using DGPub.Domain.Items;
using DGPub.Domain.Tabs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DGPub.Infra.Data.Context
{
    public class DGPubContext : DbContext
    {
        public DbSet<Item> Item { get; set; }
        public DbSet<Tab> Tab { get; set; }
        public DbSet<ItemTab> ItemTab { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json")
             .Build();

            optionsBuilder.UseSqlite(config.GetConnectionString("SqliteConnectionString"));
        }
    }
}
