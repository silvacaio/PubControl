using DGPub.Domain.Items;
using DGPub.Domain.Tabs;
using DGPub.Infra.Data.Extensions;
using DGPub.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
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
            modelBuilder.AddConfiguration(new ItemMap());

            modelBuilder.Entity<Item>().HasData(
                 Domain.Items.Item.ItemFactory.Create("Cerveja", 5),
                 Domain.Items.Item.ItemFactory.Create("Conhaque", 20),
                 Domain.Items.Item.ItemFactory.Create("Suco", 50),
                 Domain.Items.Item.ItemFactory.Create("Água", 70)
              );


            modelBuilder.AddConfiguration(new ItemTabMap());
            modelBuilder.AddConfiguration(new TabMap());
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json")
             .Build();

            optionsBuilder.UseSqlite(config.GetConnectionString("DefaultConnection"));
        }
    }
}
