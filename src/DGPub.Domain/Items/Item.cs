using DGPub.Domain.Core.Models;
using System;

namespace DGPub.Domain.Items
{
    public class Item : Entity<Item>
    {

        protected Item() { }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public override bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Name);
        }

        public static class ItemFactory
        {
            public static Item Create(string name, decimal price)
            {
                return new Item
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Price = price
                };
            }
        }
    }
}
